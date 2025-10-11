namespace TaskManagement.Services;

public class UpdateTaskDto
{
    public int Id { get; set; }
    public required string Title { get; set; } 
    public string? Desc { get; set; }
    public int GroupId { get; set; }
    public int UserId { get; set; }
    public int StatusId { get; set; }
    public int GroupTaskNumber { get; set; }

}
