using System.Text.Json;
using Basket.API.DTOs;
using MediatR;
using Shared;

namespace Basket.API.Features.Baskets.ApplyDiscountCoupon;

public class ApplyDiscountCouponCommandHandler(BasketService basketService):IRequestHandler<ApplyDiscountCouponCommand,ServiceResult>
{

    public async Task<ServiceResult> Handle(ApplyDiscountCouponCommand request, CancellationToken cancellationToken) {

        var basketAsJson = await basketService.GetBasketFromCache(cancellationToken);

        if (string.IsNullOrWhiteSpace(basketAsJson))
        {
            return ServiceResult<BasketDto>.Error("Basket not found", System.Net.HttpStatusCode.NotFound);
        }

        var basket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson);
        if (!basket.Items.Any())
        {
            return ServiceResult<BasketDto>.Error("Basket item not found", System.Net.HttpStatusCode.NotFound);
        }


        basket.ApplyNewDiscount(request.Coupon,request.DiscountRate);

        await basketService.CreateBasketCacheAsync(basket, cancellationToken);
        
        return ServiceResult.SuccessAsNoContent();

    }
}