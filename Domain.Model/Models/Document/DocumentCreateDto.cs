using Microsoft.AspNetCore.Http;
using Moneyon.PowerBi.Common.Attributes;
using Moneyon.PowerBi.Domain.Model.Common;
using System.ComponentModel.DataAnnotations;

namespace Moneyon.PowerBi.Domain.Model.Modeling;

public class DocumentCreateDto
{
    public DocumentType Type { get; set; }

    [Required(ErrorMessage = "تصویر الزامی است")]
    [DataType(DataType.Upload)]
    [MaxFileSize(10 * 1024 * 1024)]
    [AllowedExtensions(new string[] { ".jpeg", ".jpg", ".png", ".pdf", ".rar", ".zip", ".svg" })]
    public IFormFile File { get; set; }

    public string? Title { get; set; }
    public string? Description { get; set; }

    public virtual async Task<Document> ToDocumentAsync()
    {
        var doc = await File.ToDocumentAsync();
        doc.Type = Type;
        doc.Title = Title;
        doc.Description = Description;
        return doc;
    }

}

public class DocumentTicketCreateDto: DocumentCreateDto
{
    public long TicketDetailsId { get; set; }

    public override async Task<Document> ToDocumentAsync()
    {
        var doc = await File.ToDocumentAsync();
        doc.Type = Type;
        doc.Title = Title;
        doc.Description = Description;
        doc.TicketDetailId = TicketDetailsId;
        return doc;
    }
}
