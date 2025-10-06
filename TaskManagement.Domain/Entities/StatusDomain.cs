namespace TaskManagement.Domain;

public class StatusDomain
{
    public int Id { get; private set; }
    public string Status { get; private set; }
    public int GroupId { get; private set; }  

    public StatusDomain(string status, int groupId, int id = 0)
    {
        if (string.IsNullOrWhiteSpace(status)) throw new ArgumentException("Status adı boş olamaz");
        if (groupId == 0) throw new ArgumentException("Group Id boş olamaz");

        Status = status;      
        GroupId = groupId;  

        if(id != 0)
            Id = id;
    }
}
