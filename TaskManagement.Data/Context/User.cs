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

    public DateTime CreatedAt { get; set; }

    public DateTime? LastLogin { get; set; }

    public string Role { get; set; } = "User";

    public virtual ICollection<GroupMember> GroupMembers { get; set; } = new List<GroupMember>();

    public virtual ICollection<GroupTask> GroupTasks { get; set; } = new List<GroupTask>();

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
}
