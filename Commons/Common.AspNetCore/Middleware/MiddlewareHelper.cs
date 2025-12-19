using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.AspNetCore.Middleware;

public class MiddlewareHelper
{
    public static bool IsFileResponse(string contentType)
    {
        var isFileResponse = contentType != null && (
            contentType.StartsWith("application/octet-stream") ||
            contentType.StartsWith("application/pdf") ||
            contentType.StartsWith("image/") ||
            contentType.StartsWith("application/vnd.") ||
            contentType.StartsWith("multipart/form-data")
        );
        return isFileResponse;
    }
}