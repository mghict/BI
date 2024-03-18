using AutoMapper;
using Moneyon.Common.Data;
using Moneyon.PowerBi.Domain.Modeling;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moneyon.PowerBi.Domain.Model.Modeling;

public class TicketDetail : AppEntity
{

    /// <summary>
    /// متن تیکت
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// لیست مستندات تیکت
    /// </summary>
    public IEnumerable<Document> Documents { get; set; }

    /// <summary>
    /// کد کاربر ثبت کننده
    /// </summary>
    public long PersonId { get; set; }

    [ForeignKey("PersonId")]
    public Person Person { get; set; }

    /// <summary>
    /// تاریخ ثبت اطلاعات
    /// </summary>
    public DateTime CreateOn { get; set; }

    /// <summary>
    /// تیکت والد
    /// </summary>
    public long TicketId { get; set; }

    [ForeignKey("TicketId")]
    public Ticket Ticket { get; set; }
    public bool IsOwner { get; set; } = true;

}
