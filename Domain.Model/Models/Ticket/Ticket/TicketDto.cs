using Microsoft.AspNetCore.Http;
using Moneyon.Common.Extensions;
using Moneyon.Common.File.SpreadSheet;
using Moneyon.PowerBi.Domain.Model.Common;
using System.Text.Json.Serialization;

namespace Moneyon.PowerBi.Domain.Model.Modeling;

public class TicketDto
{

    public long Id { get; set; }

    /// <summary>
    /// کد رهگیری
    /// </summary>
    public long TrackingCode { get; set; }

    /// <summary>
    /// بخش تیکت
    /// </summary>
    public string TicketCategory { get; set; }

    /// <summary>
    /// عنوان تیکت
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// وضعیت تیکت
    /// </summary>
    public string Status { get; set; }
    public int StatusCode { get; set; }
    /// <summary>
    /// تاریخ ایجاد
    /// </summary>
    public DateTime CreateOn { get; set; }


    /// <summary>
    ///  کاربر ثبت کننده
    /// </summary>
    public string CreatedBy { get; set; }

    /// <summary>
    /// تاریخ آخرین تغییر
    /// </summary>
    public DateTime? ModifiedOn { get; set; }

    /// <summary>
    /// آخرین کاربر ویرایش کننده
    /// </summary>
    public string LastUserModified { get; set; }


    public IEnumerable<TicketDetailDto>? TicketDetails { get; set; }

}

public class TicketShortDto
{

    public long Id { get; set; }

    /// <summary>
    /// کد رهگیری
    /// </summary>
    [WorkSheetCoulmnAttribute(Header = "کد پیگیری")]
    public long TrackingCode { get; set; }

    /// <summary>
    /// بخش تیکت
    /// </summary>
    [WorkSheetCoulmnAttribute(Header = "دسته بندی")]
    public string TicketCategory { get; set; }

    /// <summary>
    /// عنوان تیکت
    /// </summary>
    [WorkSheetCoulmnAttribute(Header = "عنوان")]
    public string Title { get; set; }

    /// <summary>
    /// وضعیت تیکت
    /// </summary>
    [WorkSheetCoulmnAttribute(Header = "وضعیت")]
    public string Status { get; set; }
    public int StatusCode { get; set; }

    /// <summary>
    /// تاریخ ایجاد
    /// </summary>
    [WorkSheetCoulmnAttribute(Header = "تاریخ ایجاد")]
    [JsonIgnore]
    public string CreateOnPersian => CreateOn.ToJalaliDateTime();

    public DateTime CreateOn { get; set; }


    /// <summary>
    ///  کاربر ثبت کننده
    /// </summary>
    [WorkSheetCoulmnAttribute(Header = "کاربر ایجاد کننده")]
    public string CreatedBy { get; set; }

    /// <summary>
    /// تاریخ آخرین تغییر
    /// </summary>
    [WorkSheetCoulmnAttribute(Header = "تاریخ ویرایش")]
    public DateTime? ModifiedOn { get; set; }

    /// <summary>
    /// آخرین کاربر ویرایش کننده
    /// </summary>
    [WorkSheetCoulmnAttribute(Header = "کاربر ویرایش کننده")]
    public string LastUserModified { get; set; }

}

public class CreateTicketDto
{

    /// <summary>
    /// بخش تیکت
    /// </summary>
    public int TicketCategoryId { get; set; }

    /// <summary>
    /// عنوان تیکت
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// شرح تیکت
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// لیست مستندات تیکت
    /// </summary>
    //public IEnumerable<TicketDocumentCreateDto>? Documents { get; set; }
    //public TicketDocumentCreateDto? Documents { get; set; }

}
public class ReplyTicketDto
{
    /// <summary>
    /// شرح تیکت
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// لیست مستندات تیکت
    /// </summary>
    //public IEnumerable<TicketDocumentCreateDto>? Documents { get; set; }
    public TicketDocumentCreateDto? Documents { get; set; }
}
public class TicketChangeDto
{
    public long Id { get; set; }
}

public class TicketDocumentCreateDto
{
    public IFormFile File { get; set; }
    public DocumentType Type { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public async Task<Document> ToDocumentAsync()
    {
        if (this.File is null)
        {
            return null;
        }

        var doc = await File.ToDocumentAsync();
        doc.Type = Type;
        doc.Title = Title;
        doc.Description = Description;
        return doc;
    }
}

public class TicketChangeStatusDto : TicketChangeDto
{
    public TicketStatus Status { get; set; }
}
