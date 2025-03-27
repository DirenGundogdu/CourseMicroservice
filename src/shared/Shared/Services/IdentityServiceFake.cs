namespace Shared.Services;

public class IdentityServiceFake : IIdentityService
{

    public Guid GetUserId => Guid.Parse("f6d42ce1-2352-4e59-9564-98472a25ee33");
    public string Username => "Testuser";
}