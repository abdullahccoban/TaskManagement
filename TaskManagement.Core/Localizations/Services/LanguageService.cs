using Microsoft.AspNetCore.Http;

namespace TaskManagement.Core;

public class LanguageService : ILanguageService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LanguageService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetLanguage()
    {
        var lang = _httpContextAccessor.HttpContext?.Request.Headers["Accept-Language"].FirstOrDefault();
        return string.IsNullOrWhiteSpace(lang) ? "tr" : lang.ToLower();
    }
}
