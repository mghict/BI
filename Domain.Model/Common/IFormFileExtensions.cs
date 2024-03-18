using Microsoft.AspNetCore.Http;
using Moneyon.PowerBi.Domain.Model.Modeling;

namespace Moneyon.PowerBi.Domain.Model.Common;

public static class IFormFileExtensions
{
    public static async Task<Document> ToDocumentAsync(this IFormFile formFile)
    {
        byte[] content;
        using (var ms = new MemoryStream())
        {
            await formFile.CopyToAsync(ms);
            content = ms.ToArray();
        }

        return new Document(formFile.FileName, formFile.ContentType, content)
        {
        };
    }

    public static async Task<Tuple<string, string, byte[]>> ToDocumentValuesAsync(this IFormFile formFile)
    {
        byte[] content;
        using (var ms = new MemoryStream())
        {
            await formFile.CopyToAsync(ms);
            content = ms.ToArray();
        }

        return new Tuple<string, string, byte[]>(formFile.FileName, formFile.ContentType, content);
    }
}