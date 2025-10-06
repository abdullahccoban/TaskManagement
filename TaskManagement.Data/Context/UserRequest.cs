using System;
using System.Collections.Generic;

namespace TaskManagement.Data.Context;

public partial class UserRequest
{
    public int Id { get; set; }

    public int GroupId { get; set; }

    public int UserId { get; set; }

    public string? Message { get; set; }

    public virtual Group Group { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
