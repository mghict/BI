using AutoMapper;
using Moneyon.Common.Data;
using Moneyon.PowerBi.Domain.Model.Common;
using Moneyon.PowerBi.Domain.Modeling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moneyon.PowerBi.Domain.Model.Modeling;

/// <summary>w
/// سند
/// </summary>
public class Document : AppEntity,IAudit
{
    public long? PersonId { get; set; }
    public Guid Guid { get; set; }
    public DocumentType Type { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }

    /// <summary>
    /// In Bytes
    /// </summary>
    public long Size { get; set; }
    public string OriginalFileName { get; set; }
    public string ContentType { get; set; }
    public DocumentContent Content { get; set; }
    public Person? Person { get; set; }
    public DateTime CreateOn { get ; set ; }
    public DateTime? ModifiedOn { get ; set ; }

    public long? TicketDetailId { get; set; }
    public TicketDetail? TicketDetail { get; set; }

    public Document()
    {
        Guid = Guid.NewGuid();
    }

   
    public Document(string originalFileName, string contentType, byte[] content)
        :this()
    {
        OriginalFileName = originalFileName;
        ContentType = contentType;
        Size = content.Length;
        Content = new DocumentContent() { Value = content };
    }

    public Document(DocumentType type, string originalFileName, string contentType, byte[] content)
        : this(originalFileName, contentType, content)
    {
        Type = type;
    }
}
