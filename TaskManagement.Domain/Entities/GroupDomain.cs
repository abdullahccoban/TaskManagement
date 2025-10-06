namespace TaskManagement.Domain;

public class GroupDomain
{
    public int Id { get; private set; }
    public string GroupName { get; private set; }
    public int CreatedUserId { get; private set; }  

    public GroupDomain(string groupName, int createdUserId)
    {
        if (string.IsNullOrWhiteSpace(groupName)) throw new ArgumentException("Grup adı boş olamaz");
        if (createdUserId == 0) throw new ArgumentException("User Id boş olamaz");

        GroupName = groupName;      
        CreatedUserId = createdUserId;  
    }
}
