namespace TaskManagement.Services;

public class TaskDto
{
    public int Id { get; set; }
    public required string Title { get; set; } 
    public string? Desc { get; set; }
    public int GroupId { get; set; }
    public string? GroupName { get; set; }
    public int UserId { get; set; }
    public string? Username { get; set; }
    public int StatusId { get; set; }
    public string? StatusName { get; set; } 
    public bool IsDeleted { get; set; } 
    public int GroupTaskNumber { get; set; }
    public string? DisplayTaskCode { get; set; }
}
