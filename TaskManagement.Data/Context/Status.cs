using System;
using System.Collections.Generic;

namespace TaskManagement.Data.Context;

public partial class Status
{
    public int Id { get; set; }

    public string Status1 { get; set; } = null!;

    public int GroupId { get; set; }

    public virtual Group Group { get; set; } = null!;

    public virtual ICollection<GroupTask> GroupTasks { get; set; } = new List<GroupTask>();
}
