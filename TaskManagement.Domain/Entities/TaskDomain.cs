namespace TaskManagement.Domain;

public class TaskDomain
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Desc { get; set; }
    public int GroupId { get; set; }    
    public int UserId { get; set; }
    public int StatusId { get; set; }  
    public bool IsDeleted { get; set; }
    public int GroupTaskNumber { get; set; }

    public TaskDomain(string title, int groupId, int statusId, int userId, int groupTaskNumber, string? desc = null, bool isDeleted = false, int id = 0)
    {
        if (string.IsNullOrWhiteSpace(title)) 
            throw new ArgumentException("Title boş olamaz.");
        
        if (groupId <= 0) 
            throw new ArgumentException("Geçerli bir grup seçilmelidir.");
        
        if (userId <= 0) 
            throw new ArgumentException("Görev bir kullanıcıya atanmalıdır.");
        
        if (statusId <= 0) 
            throw new ArgumentException("Task için geçerli bir durum seçilmelidir.");
        
        Title = title;
        Desc = desc;
        GroupId = groupId;
        UserId = userId;
        StatusId = statusId;
        IsDeleted = isDeleted; 
        GroupTaskNumber = groupTaskNumber;      

        if(id > 0)
            Id = id;         
    }

    public void Update(string title, string? desc, int userId, int statusId)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Task başlığı boş olamaz");
        if (userId <= 0)
            throw new ArgumentException("Görev bir kullanıcıya atanmalıdır");
        if (statusId <= 0)
            throw new ArgumentException("Task için geçerli bir durum seçilmelidir");

        Title = title;
        Desc = desc;
        UserId = userId;
        StatusId = statusId;
    }

    public void UpdateStatus(int statusId)
    {
        if (statusId <= 0)
            throw new ArgumentException("Task için geçerli bir durum seçilmelidir");

        StatusId = statusId;
    }

    public void SoftDelete()
    {
        IsDeleted = true;
    }
}
