using Asp.Versioning.Builder;
using Basket.API.Features.Baskets.AddBasketItem;
using Basket.API.Features.Baskets.ApplyDiscountCoupon;
using Basket.API.Features.Baskets.DeleteBasketItem;
using Basket.API.Features.Baskets.GetBasket;
using Basket.API.Features.Baskets.RemoveDiscountCoupon;

namespace Basket.API.Features.Baskets;

public static class BasketEndpointExt
{
    public static void AddBasketGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet) {
        app.MapGroup("api/v{version:apiVersion}/baskets").WithTags("Baskets")
            .WithApiVersionSet(apiVersionSet)
            .AddBasketItemGroupItemEndpoint()
            .DeleteBasketItemGroupItemEndpoint()
            .GetBasketItemGroupEndpoint()
            .ApplyDiscountCouponGroupItemEndpoint()
            .RemoveDiscountCouponGroupItemEndpoint();
    }
}