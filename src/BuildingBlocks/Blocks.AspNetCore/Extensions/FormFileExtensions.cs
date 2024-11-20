using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;

namespace Blocks.AspNetCore;

public static class FormFileExtensions
{
    public const string DefaultFileContentType = "application/octet-stream";

    public static string GetContentType(this IFormFile file)
    {
        if (!string.IsNullOrEmpty(file.ContentType) && file.ContentType != DefaultFileContentType)
            return file.ContentType;

        new FileExtensionContentTypeProvider().TryGetContentType(file.FileName, out var contentType);
        return contentType ?? DefaultFileContentType;
    }

    public static string GetExtension(this IFormFile file)
    {
        if (string.IsNullOrEmpty(file.FileName))
            return string.Empty; //todo default extension

        return Path.GetExtension(file.FileName).Replace(".", "").ToLower();
    }

    public static string GetFileContentType(this IFormFile file)
    {
        new FileExtensionContentTypeProvider().TryGetContentType(file.FileName, out var contentType);
        return contentType ?? DefaultFileContentType;
    }

    public static string ToFormatterNumber(this int number, int formattedNumber)
    {
        return number.ToString().PadLeft(formattedNumber, '0');
    }

}
