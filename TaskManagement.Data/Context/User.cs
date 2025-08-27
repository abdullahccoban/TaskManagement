using System;
using System.Collections.Generic;

namespace TaskManagement.Data.Context;

public partial class User
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public byte[] PasswordHash { get; set; } = null!;
    public byte[] PasswordSalt { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    private DateTime? _lastLogin;
    public DateTime? LastLogin
    {
        get => _lastLogin;
        set => _lastLogin = value.HasValue 
            ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc) 
            : null;
    }
    
    public string Role { get; set; } = "User";

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
