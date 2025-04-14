using Asp.Versioning.Builder;
using Discount.API.Features.Discounts.Create;
using Discount.API.Features.Discounts.GetDiscountByCode;

namespace Discount.API.Features.Discounts;

public static class DiscountEndpointExt
{
    public static void AddDiscountGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet) {
        app.MapGroup("api/v{version:apiVersion}/discounts").WithTags("Discounts")
            .WithApiVersionSet(apiVersionSet)
            .CreateDiscountGroupItemEndpoint()
            .GetDiscountByCodeGroupItemEndpoint();
    }
}