using Asp.Versioning.Builder;
using Catalog.API.Features.Categories.Create;
using Catalog.API.Features.Categories.GetAll;

namespace Catalog.API.Features.Categories;

public static class CategoryEndpointExt
{
    public static void AddCategoryGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet) {
        app.MapGroup("api/v{version:apiVersion}/categories")
            .WithTags("Categories")
            .WithApiVersionSet(apiVersionSet)
            .CreateCategoryGroupItemEndpoint()
            .GetAllCategoryGroupItemEndpoint()
            .GetByIdCategoryGroupItemEndpoint();
    }
}