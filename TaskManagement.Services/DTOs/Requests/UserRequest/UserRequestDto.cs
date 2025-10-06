namespace TaskManagement.Services;

public class UserRequestDto
{
    public int Id { get; set; } 
    public int UserId { get; set; }
    public int GroupId { get; set; }    
    public string? Message { get; set; } 
    public string? Username { get; set; }
}
