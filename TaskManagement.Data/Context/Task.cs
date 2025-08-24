using System;
using System.Collections.Generic;

namespace TaskManagement.Data.Context;

public partial class Task
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public bool? IsCompleted { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
