namespace TaskManagement.Services;

public class StatusDto
{
    public int Id { get; set; }
    public required string Status { get; set; }
    public int GroupId { get; set; }

}
