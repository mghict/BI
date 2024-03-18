using Microsoft.AspNetCore.Mvc;
using Moneyon.PowerBi.Domain.Model.Modeling;

namespace Moneyon.PowerBi.API.Common.Extensions;

public static class DocumentDtoExtensions
{
    //public static void SetAdminScopeUrls(this DocumentDto document, IUrlHelper urlHelper)
    //{
    //    document.Url = urlHelper.RouteUrl("adminDocumentDownload", new { documentId = document.Guid })!;
    //    document.ThumbnailUrl = urlHelper.RouteUrl("adminDocumentDownload", new { documentId = document.Guid })!;
    //}

    public static void SetUserScopeUrls(this DocumentDto document, IUrlHelper urlHelper)
    {
        document.Url = urlHelper.RouteUrl("userDocumentDownload", new { documentId = document.Guid.ToString() })!;
        document.ThumbnailUrl = urlHelper.RouteUrl("userDocumentDownload", new { documentId = document.Guid.ToString() })!;
    }


}
