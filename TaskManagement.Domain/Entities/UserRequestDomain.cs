namespace TaskManagement.Domain;

public class UserRequestDomain
{
    public int UserId { get; private set; }
    public int GroupId { get; private set; } 
    public string? Message { get; private set; }

    public UserRequestDomain(int userId, int groupId, string? message)
    {
        if (userId == 0) throw new ArgumentException("User Id boş olamaz");
        if (groupId == 0) throw new ArgumentException("Group Id boş olamaz");
        
        UserId = userId;
        GroupId = groupId;
        Message = message;
    }
}