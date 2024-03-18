using Moneyon.Common.Data;
using Moneyon.PowerBi.Common.Extensions;
using Moneyon.PowerBi.Domain.Model.Common;
using Moneyon.PowerBi.Domain.Model.Modeling;
using Moneyon.PowerBi.Domain.Modeling;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moneyon.PowerBi.Domain.Model.Modeling;

public class Ticket : AppEntity,IAudit
{
    public Ticket()
    {
        TrackingCode = System.DateTime.UtcNow.GenerateDateTimeSecurityCode();
    }



    /// <summary>
    /// کد رهگیری
    /// </summary>
    public long TrackingCode { get; set; }

    /// <summary>
    /// بخش تیکت
    /// </summary>
    public long TicketCategoryId { get; set; }

    [ForeignKey("TicketCategoryId")]
    public TicketCategory TicketCategory { get; set; }

    /// <summary>
    /// عنوان تیکت
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// وضعیت تیکت
    /// </summary>
    public TicketStatus Status { get; set; }

    /// <summary>
    /// تاریخ ایجاد
    /// </summary>
    public DateTime CreateOn { get; set; }


    /// <summary>
    ///  کاربر ثبت کننده
    /// </summary>
    public long CreatedById { get; set; }
    public Person CreatedBy { get; set; }

    /// <summary>
    /// تاریخ آخرین تغییر
    /// </summary>
    public DateTime? ModifiedOn { get; set; }

    /// <summary>
    /// آخرین کاربر ویرایش کننده
    /// </summary>
    public long? LastUserModifiedId { get; set; }
    public Person? LastUserModified { get; set; }

    public IEnumerable<TicketDetail>? TicketDetails { get; set; }

}
