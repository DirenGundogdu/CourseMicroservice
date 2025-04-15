using Asp.Versioning.Builder;
using File.API.Features.Files.Delete;
using File.API.Features.Files.Upload;

namespace File.API.Features.Files;

public static class FileEndpointExt
{
    public static void AddFileGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet) {
        app.MapGroup("api/v{version:apiVersion}/files").WithTags("Files")
            .WithApiVersionSet(apiVersionSet)
            .UploadFileCommandGroupItemEndpoint()
            .DeleteFileGroupItemEndpoint();
    }
}