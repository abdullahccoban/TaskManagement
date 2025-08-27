using TaskManagement.Data.Context;

namespace TaskManagement.Services;

public interface IUserService
{
    Task<List<UserDto>> GetAllAsync();
    Task<UserDto?> GetByIdAsync(int id);
    Task<UserDto> CreateAsync(User user);
    Task<UserDto> RegisterAsync(string name, string email, string password);
    Task<string?> LoginAsync(string email, string password);
}
