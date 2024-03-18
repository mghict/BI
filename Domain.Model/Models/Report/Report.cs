using DocumentFormat.OpenXml.Wordprocessing;
using Moneyon.PowerBi.Domain.Model.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moneyon.PowerBi.Domain.Model.Modeling;

public class Report : AppEntity,IAudit,IEnable
{
    [MaxLength(255)]
    public string Title { get; set; }

    [MaxLength(150)]
    public string LatinName { get; set; }

    public string URL { get; set; }

    //public ICollection<Subscription>? Subscriptions { get; set; } 
    //public ICollection<Package>? Packages { get; set; }
    public long PermissionId { get; set; }

    //[ForeignKey("PermissionId")]
    public Permission Permission { get; set; }

    //--------------------------------------------------
    public bool IsEnabled { get ; set ; }
    public DateTime CreateOn { get ; set ; }
    public DateTime? ModifiedOn { get; set; }

    public Report(string title, string url)
    {
        Title = title;
        URL = url;
    }

    public Report()
    {
            
    }
}
