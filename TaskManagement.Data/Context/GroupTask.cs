using System;
using System.Collections.Generic;

namespace TaskManagement.Data.Context;

public partial class GroupTask
{
    public int Id { get; set; }

    public int GroupId { get; set; }

    public int TaskId { get; set; }

    public int StatusId { get; set; }

    public int UserId { get; set; }

    public virtual Group Group { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;

    public virtual Task Task { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
