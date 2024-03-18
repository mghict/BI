namespace Moneyon.PowerBi.Domain.Model.Modeling;

/// <summary>
/// محتوای سند
/// </summary>
public class DocumentContent : AppEntity
{
    public byte[] Value { get; set; }
    public Document Document { get; set; }
}
