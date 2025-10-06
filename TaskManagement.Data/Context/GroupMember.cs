using System;
using System.Collections.Generic;

namespace TaskManagement.Data.Context;

public partial class GroupMember
{
    public int Id { get; set; }

    public int GroupId { get; set; }

    public int UserId { get; set; }
    
    public string? Role { get; set; }

    public virtual Group Group { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
