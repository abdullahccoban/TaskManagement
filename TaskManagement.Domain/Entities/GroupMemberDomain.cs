namespace TaskManagement.Domain;

public class GroupMemberDomain
{
    public int Id { get; private set; }

    public int GroupId { get; private set; }

    public int UserId { get; private set; }

    public string? Role { get; private set; }

    public GroupMemberDomain(int groupId, int userId, string? role = "GroupUser")
    {
        if (groupId == 0) throw new ArgumentException("Group Id boş olamaz");
        if (userId == 0) throw new ArgumentException("User Id boş olamaz");

        GroupId = groupId;
        UserId = userId;
        Role = role;    
    }
}
