namespace Moneyon.PowerBi.Domain.Modeling;

//public class ShortPersonDto
//{
//    public Guid Id { get; set; }
//    public string Mobile { get; set; }
//    public string DisplayName { get; set; }
//}

public class ShortPersonDto
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? NationalCode { get; set; }
    public string? Email { get; set; }
    public string? MobileNumber { get; set; }
    public string? DisplayName { get; set; }
    public string? UserName { get; set; }
    public bool IsActive { get; set; }

}