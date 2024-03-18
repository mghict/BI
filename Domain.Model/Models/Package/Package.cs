using Moneyon.PowerBi.Domain.Modeling;
using System.ComponentModel.DataAnnotations;

namespace Moneyon.PowerBi.Domain.Model.Modeling;

public class Package : AppEntity
{
    public Package()
    {
        Subscriptions = new HashSet<Subscription>();    
        Reports=new HashSet<Permission>();
        //Payments=new HashSet<Payment>(); 
    }

    [MaxLength(150)]
    [Required]
    public string Name { get; set; }

    [MaxLength(255)]
    public string? Descreption { get; set; }

    /// <summary>
    ///  بازه اعتباری بسته به ماه
    /// </summary>
    public int TimePeriod { get; set; }
    public long Price { get; set; }
    public int Discount { get; set; } = 0;
    public long PackageAmount => Price - (Discount*Price/100);
    public bool ShowInHomePage { get; set; } = true;
    public bool IsSuperiorDiscount {  get; set; } = false;
    public AllowdUserCount AllowdUserCount { get; set; }
    public bool Active { get; set; }

    public virtual ICollection<Subscription>? Subscriptions { get; set; }
    public virtual ICollection<Permission>? Reports { get; set; }
    //public virtual ICollection<Payment>? Payments { get; set; }

}
