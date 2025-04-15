using Shared;

namespace File.API.Features.Files.Delete;

public record DeleteFileCommand(string FileName):IRequestByServiceResult;