using System;
using System.Collections.Generic;

namespace TaskManagement.Data.Context;

public partial class Status
{
    public int Id { get; set; }

    public string Status1 { get; set; } = null!;

    public int GroupId { get; set; }

    public virtual Group Group { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
