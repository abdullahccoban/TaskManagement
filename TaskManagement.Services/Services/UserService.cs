using AutoMapper;
using Microsoft.Extensions.Configuration;
using TaskManagement.Core;
using TaskManagement.Data;
using TaskManagement.Data.Context;
using TaskManagement.Domain;

namespace TaskManagement.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repo;
    private readonly ILanguageService _langService;
    private readonly IConfiguration _config;
    private readonly IMapper _mapper;

    public UserService(IUserRepository repo, ILanguageService langService, IConfiguration config, IMapper mapper)
    {
        _repo = repo;
        _langService = langService;
        _config = config;
        _mapper = mapper;
    }

    public async Task<UserDto> CreateAsync(Data.Context.User user)
    {
        var dbUser = await _repo.AddAsync(user);
        return _mapper.Map<UserDto>(dbUser);
    }

    public async Task<List<UserDto>> GetAllAsync() 
    {
        var userList = await _repo.GetAllAsync(); 
        return _mapper.Map<List<UserDto>>(userList);
    }

    public async Task<UserDto?> GetByIdAsync(int id)
    {
        var user = await _repo.GetByIdAsync(id); 
        return _mapper.Map<UserDto>(user);
    }

    public async Task<string?> LoginAsync(string email, string password)
    {
        var user = await _repo.GetByEmailAsync(email);

        if (user == null)
            throw new Exception(ErrorMessages.InvalidCredentials(_langService.GetLanguage()));

        var valid = HashHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt);

        if (!valid)
            throw new Exception(ErrorMessages.InvalidCredentials(_langService.GetLanguage()));

        user.LastLogin = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
        await _repo.UpdateAsync(user);

        return JwtHelper.GenerateJwtToken(user.Id, user.Email, _config);
    }

    public async Task<UserDto> RegisterAsync(string name, string email, string password)
    {
        var user = new UserDomain(name, email);

        if (await _repo.GetByEmailAsync(email) != null)
            throw new Exception(ErrorMessages.UserAlreadyExists(_langService.GetLanguage()));

        HashHelper.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

        var dbUser = _mapper.Map<User>(user);
        dbUser.PasswordHash = passwordHash;
        dbUser.PasswordSalt = passwordSalt;
        var userResult = await _repo.AddAsync(dbUser);

        return _mapper.Map<UserDto>(userResult);
    }
}
