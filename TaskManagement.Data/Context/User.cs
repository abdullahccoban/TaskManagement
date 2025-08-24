using System;
using System.Collections.Generic;

namespace TaskManagement.Data.Context;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
