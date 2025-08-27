namespace TaskManagement.Domain;

public class UserDomain
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Role { get; private set; } = "User";
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? LastLogin { get; private set; }

    public UserDomain(string name, string email) 
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name boş olamaz");
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email boş olamaz");

        Name = name;
        Email = email;
    }

    public void UpdateLastLogin()
    {
        LastLogin = DateTime.UtcNow;
    }
}
