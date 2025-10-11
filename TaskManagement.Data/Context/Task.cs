using System;
using System.Collections.Generic;

namespace TaskManagement.Data.Context;

public partial class Task
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Desc { get; set; }

    public int GroupId { get; set; }

    public int UserId { get; set; }

    public int StatusId { get; set; }

    public bool IsDeleted { get; set; }

    public int GroupTaskNumber { get; set; }

    public virtual Group Group { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
