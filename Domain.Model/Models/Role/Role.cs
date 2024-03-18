using Moneyon.PowerBi.Domain.Model.Common;

namespace Moneyon.PowerBi.Domain.Model.Modeling;

public class Role : AppEntity,IEnable,IAudit
{
    public const string SuperAdmin = "SuperAdmin";
    public const string User = "User";

    public Role()
    {
        Users=new HashSet<User>();
        Permissions=new HashSet<Permission>();
    }
    public string Title { get; set; }
    public string? Description { get; set; }
    
    public virtual ICollection<User> Users { get; set; }
    public virtual ICollection<Permission> Permissions { get; set; }
    public bool IsEnabled { get ; set ; }=true;
    public DateTime CreateOn { get ; set; }
    public DateTime? ModifiedOn { get ; set ; }
}

public class ShortRoleDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public bool IsEnabled { get; set; }
}

public class RoleDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public bool IsEnabled { get; set; }
    public ICollection<PermissionDto>? Permissions { get; set; }
}

public class RoleCreateDto
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public ICollection<long>? PermissionId { get; set; }
}

public class RoleUpdateDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public ICollection<long>? PermissionId { get; set; }
    public bool IsEnabled { get; set; }
}


