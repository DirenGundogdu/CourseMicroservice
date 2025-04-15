using Shared;

namespace File.API.Features.Files.Upload;

public record UploadFileCommand(IFormFile File):IRequestByServiceResult<UploadFileCommandResponse>;