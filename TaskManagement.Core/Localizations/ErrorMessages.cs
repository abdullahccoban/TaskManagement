namespace TaskManagement.Core;

public static class ErrorMessages
{
    public static string UserAlreadyExists(string? lang = "tr")
    {
        return lang switch
        {
            "en" => "A user with this email already exists.",
            "tr" => "Bu email ile kayıtlı bir kullanıcı zaten mevcut.",
            _ => "Bu email ile kayıtlı bir kullanıcı zaten mevcut."
        };
    }

    public static string InvalidCredentials(string? lang = "tr")
    {
        return lang switch
        {
            "en" => "Invalid email or password.",
            "tr" => "Geçersiz email veya şifre.",
            _ => "Geçersiz email veya şifre."
        };
    }
}
