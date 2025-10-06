using System;
using System.Collections.Generic;

namespace TaskManagement.Data.Context;

public partial class Group
{
    public int Id { get; set; }

    public string GroupName { get; set; } = null!;

    public int CreatedUserId { get; set; }

    public virtual User CreatedUser { get; set; } = null!;

    public virtual ICollection<GroupMember> GroupMembers { get; set; } = new List<GroupMember>();

    public virtual ICollection<GroupTask> GroupTasks { get; set; } = new List<GroupTask>();

    public virtual ICollection<Status> Statuses { get; set; } = new List<Status>();

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    public virtual ICollection<UserRequest> UserRequests { get; set; } = new List<UserRequest>();
}
