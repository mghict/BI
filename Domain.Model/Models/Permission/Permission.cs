using Moneyon.PowerBi.Domain.Model.Modeling;
using Moneyon.PowerBi.Domain.Modeling;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moneyon.PowerBi.Domain.Model.Modeling;
public class Permission:AppEntity
{
    public Permission()
    {
        Roles=new HashSet<Role>();
        //Packages=new HashSet<Package>();
    }
    public int PermisionId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public PermissionType Type { get; set; }

    public long? ReportId { get; set; }

    //[ForeignKey("ReportId")]
    public Report? Report { get; set; }

    public virtual ICollection<Role> Roles { get; set; }
    public ICollection<Package> Packages { get; set; }
}
