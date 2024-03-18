using Moneyon.PowerBi.Domain.Model.Modeling;

namespace Moneyon.PowerBi.Domain.Modeling;

public class PersonShortDto
{
    public Guid Id { get; set; }
    public string MobileNumber { get; set; }
    public string DisplayName { get; set; }
}
public class PersonDto
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string NationalCode { get; set; }
    public string? Email { get; set; }
    public string MobileNumber { get; set; }
    public string DisplayName { get; set; }
    //public ICollection<AddressDto>? Addresses { get; set; }
    public AddressDto? Addresses { get; set; }

    public ICollection<ShortRoleDto>? Roles { get; set; }
}

public class EditPersonRoleDto
{
    public Guid PersonId { get; set; }
    public IEnumerable<long>? Roles { get; set; }
}

public class EditProfileDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; } = string.Empty;
    //public ICollection<AddressCreateDto>? Addresses { get; set; }
    public AddressCreateDto Addresses { get; set; }
    //public ICollection<long>? Roles { get; set; }
}
