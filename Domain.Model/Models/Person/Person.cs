using Moneyon.PowerBi.Domain.Model.Modeling;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moneyon.PowerBi.Domain.Modeling;

public class Person : AppEntity
{
    public static Guid PlatformCode= Guid.Parse("54E2C6AD-B374-4B81-A6CE-710B72998859");

    [MaxLength(250)]
    public string? FirstName { get; set; }
    [MaxLength(250)]
    public string? LastName { get; set; }

    [MaxLength(15)]
    public string NationalCode { get; set; }

    [MaxLength(150)]
    public string? Email { get; set; }

    [MaxLength(20)]
    public string MobileNumber { get; set; }
    public Guid Code { get; set; }= Guid.NewGuid();

    public virtual User? User { get; set; }
    public virtual ICollection<Account>? Accounts { get; set; }
    public virtual ICollection<Payment>? Payment { get; set; }
    public virtual ICollection<Subscription>? Subscription { get; set; }
    public virtual ICollection<Purches>? Purches { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [MaxLength(500)]
    public string DisplayName
    {
        get=>(string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName)) ? MobileNumber:$"{FirstName} {LastName}";
        private set { }
    }
    public ICollection<Address>? Addresses { get; set; }

}
