using Moneyon.PowerBi.Common.Extensions;
using Moneyon.PowerBi.Domain.Model.Common;
using Moneyon.PowerBi.Domain.Modeling;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security;

namespace Moneyon.PowerBi.Domain.Model.Modeling;

public class User :AppEntity,IAudit
{
    public string UserName { get; set; }
    public string Password { get; set; }
    //public DateTime CreateDate { get; set; }=DateTime.Now;
    public bool IsActive { get; set; } 
    public ICollection<Role>? Roles { get; set; }
    public ICollection<OTP>? OTPs { get; set; }
    
    public ICollection<UsersToken> Token { get; set; }
    public long PersonId { get; set; }
    public virtual Person Person { get; set; }

    public DateTime CreateOn { get ; set; }
    public DateTime? ModifiedOn { get; set ; }

    public User()
    {
            
    }

    public User(string userName, string pass, bool? active = false)
    {
        UserName = userName;
        Password = pass.Hash();
        IsActive = active.Value;
    }

    public User(string userName, string pass,List<Role> roles, bool? active = false)
    {
        UserName = userName;
        Password = pass.Hash();
        IsActive = active.Value;
        Roles = roles;
    }


    public User(Person person, string userName, string pass, List<Role> roles, bool? active = false)
    {
        Person = person;
        UserName = userName;
        Password = pass.Hash();
        IsActive = active.Value;
        Roles = roles;
    }
    public List<string> GetPermission()
    {
        List<string> permissions = new List<string>();
        if(Roles is null ||  Roles.Count == 0) 
            return permissions;

        foreach (Role role in Roles!)
        {
            if(role.Permissions is null ||  role.Permissions.Count == 0) continue;

            foreach(Permission permission in role.Permissions!)
            {
                if (permissions.Contains(permission.PermisionId.ToString()))
                    continue;

                permissions.Add(permission.PermisionId.ToString());
            }
        }

        if(Person.Subscription is not null)
        {
            foreach (var sub in Person!.Subscription!)
            {
                if (sub.ExpireDate! >= DateTime.Now.Date && sub.Active == true)
                {

                    var package = sub.Package;
                    if (!package.Active) continue;

                    foreach (var report in package!.Reports)
                    {
                        if (permissions.Contains(report.PermisionId.ToString()))
                            continue;

                        permissions.Add(report.PermisionId.ToString());
                    }
                }
            }
        }
        

        return permissions;
    }
    public List<string> GetRoles()
    {
        List<string> roles = new List<string>();
        if (Roles is null || Roles.Count == 0)
            return roles;

        foreach (Role role in Roles!)
        {
            if (role.Permissions is null || role.Permissions.Count == 0) continue;
            roles.Add(role.Title);
        }

        return roles;
    }
}
