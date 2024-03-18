using Moneyon.PowerBi.Domain.Model.Common;
using Moneyon.PowerBi.Domain.Modeling;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moneyon.PowerBi.Domain.Model.Modeling;

public class Subscription : AppEntity,IAudit
{
    public SubscriptionType SubscriptionType { get; set; }
    public bool? Active { get; set; }
    public long? PackageId { get; set; }
    public long OwnerId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? ExpireDate { get; set; }
    public long? PurchesId { get; set; }


    public Package? Package { get; set; }
    public Person Owner { get; set; }
    
    [ForeignKey("PurchesId")]
    public Purches Purches { get; set; }
    public DateTime CreateOn { get ; set ; }
    public DateTime? ModifiedOn { get ; set; }

    //public ICollection<Report>? Reports { get; set; }

    public Subscription( SubscriptionType type ,Person user , Package package , bool active)
    {
        SubscriptionType = type;
        Active = active;
        Package = package;
        StartDate = DateTime.Now;
        ExpireDate = DateTime.Now.AddMonths(package.TimePeriod);
        Owner = user;
        OwnerId = user.Id;
        PackageId = package.Id;
        
    }

    public Subscription(SubscriptionType type, Person user, DateTime? expireDate ,  bool active)
    {
        SubscriptionType = type;
        Active = active;
        StartDate= DateTime.Now;
        ExpireDate= expireDate;
        Owner = user;
        OwnerId = user.Id;
    }

    public Subscription()
    {
            
    }
}
