using Moneyon.Common.Data;
using Moneyon.Common.File.SpreadSheet;
using Moneyon.PowerBi.Domain.Model.Modeling;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Moneyon.PowerBi.Domain.Modeling;

/// <summary>
/// گروه حساب
/// </summary>
public class AccountGroup : AppEntity
{
    //public int Id { get; set; }

    [MaxLength(150)]
    public string Name { get; set; }
    public long? ParentId { get; set; }
    public AccountGroupKey? Key { get; set; }

    /// <summary>
    /// آیا ماهیت حساب های این گروه «بدهکار» است؟
    /// </summary>
    public bool HasDebitNature { get; set; }

    /// <summary>
    /// آیا ماهیت حساب های این گروه «بستانکار» است؟
    /// </summary>
    public bool HasCreditNature { get => !HasDebitNature; }

    public ICollection<Account> Accounts { get; set; }
    public ICollection<AccountGroup> Children { get; set; }

    public AccountGroup? Parent { get; set; }

    public AccountGroup()
    {
        Accounts = new List<Account>();
    }
}

public class AccountGroupDto
{
    [WorkSheetCoulmnAttribute(Header = "نام گروه")]
    public string Name { get; set; }
    public AccountGroupDto Parent { get; set; }
}
