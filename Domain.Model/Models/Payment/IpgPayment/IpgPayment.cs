using Moneyon.PowerBi.Domain.Modeling;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moneyon.PowerBi.Domain.Model.Modeling;

[Table("Payments")]
public class IpgPayment:Payment
{
    internal IpgPayment()
    {
        
    }
    public IpgPayment(long ownerId, long accountId, long amount,long packageId, string? description) :
        base(ownerId,accountId,amount,packageId,description)
    {
    }
}


public class IpgPaymentCreateDto
{
    public long Amount { get; set; }
    public long PackageId { get; set; }
    public long AccountId { get; set; }

    public IpgPaymentCreateDto(long amount, long packageId, long accountId)
    {
        Amount = amount;
        PackageId = packageId;
        AccountId = accountId;
    }
}
