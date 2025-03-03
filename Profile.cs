using System.ComponentModel.DataAnnotations;

public class Profile
{
    public Guid Id { get; set; }
    [Required]
    public string? FirstName { get; set; }
    [Required]
    public string? LastName { get; set; }
    [Required]
    [EmailAddress]
    public string? Email {get; set; }
    [Required]
    [Phone]
    public string? PhoneNumber { get; set; }
    public bool IsDeleted { get; set; } = false;
}