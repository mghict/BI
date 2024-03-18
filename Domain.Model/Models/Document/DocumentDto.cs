namespace Moneyon.PowerBi.Domain.Model.Modeling;

public class DocumentDto
{
    //public int Id { get; set; }
    public DocumentType Type { get; set; }
    public Guid Guid { get; set; }
    public string OriginalFileName { get; set; }
    public string FileName => OriginalFileName;
    public string ContentType { get; set; }
    public string? Url { get; set; }
    public string? ThumbnailUrl { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
}
