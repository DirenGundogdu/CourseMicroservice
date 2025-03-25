using Catalog.API.Features.Categories.Create;
using Catalog.API.Features.Categories.GetAll;

namespace Catalog.API.Features.Categories;

public static class CategoryEndpointExt
{
    public static void AddCategoryGroupEndpointExt(this WebApplication app) {
        app.MapGroup("api/categories")
            .WithTags("Categories")
            .CreateCategoryGroupItemEndpoint()
            .GetAllCategoryGroupItemEndpoint()
            .GetByIdCategoryGroupItemEndpoint();
    }
}