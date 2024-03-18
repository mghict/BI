using Microsoft.AspNetCore.Http;
using Moneyon.PowerBi.Domain.Model.Common;

namespace Moneyon.PowerBi.Domain.Model.Modeling;

public class TicketDetailDto
{
    public long Id { get; set; }

    /// <summary>
    /// پاسخ تیکت
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// لیست مستندات تیکت
    /// </summary>
    //public IEnumerable<DocumentWithContentDto>? Documents { get; set; }

    /// <summary>
    /// کد کاربر ثبت کننده
    /// </summary>
    public string Person { get; set; }

    /// <summary>
    /// تاریخ ثبت اطلاعات
    /// </summary>
    public DateTime CreateOn { get; set; }

    public bool IsOwner { get; set; }
}

public class TicketDetailShortDto
{
    public long Id { get; set; }

    /// <summary>
    /// پاسخ تیکت
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// لیست مستندات تیکت
    /// </summary>
    public IEnumerable<DocumentWithContentDto>? Documents { get; set; }

    /// <summary>
    /// کد کاربر ثبت کننده
    /// </summary>
    public string Person { get; set; }

    /// <summary>
    /// تاریخ ثبت اطلاعات
    /// </summary>
    public DateTime CreateOn { get; set; }

    public bool IsOwner { get; set; }
}

public class TicketDetailCreateDto
{

    /// <summary>
    /// پاسخ تیکت
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// کد تیکت
    /// </summary>
    public long TicketId { get; set; }

    /// <summary>
    /// لیست مستندات تیکت
    /// </summary>
    //public IEnumerable<TicketDetailsDocumentCreateDto>? Documents { get; set; }
    public TicketDetailsDocumentCreateDto? Document { get; set; }
}

public class TicketDetailsDocumentCreateDto
{
    public IFormFile File { get; set; }
    public DocumentType Type { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public long TicketDetailId { get; set; }
    public async Task<Document> ToDocumentAsync()
    {
        var doc = await File.ToDocumentAsync();
        doc.Type = Type;
        doc.Title = Title;
        doc.Description = Description;
        doc.TicketDetailId = TicketDetailId;
        return doc;
    }
}
