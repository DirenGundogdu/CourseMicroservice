using System.Net;
using System.Text.Json;
using Basket.API.Const;
using Basket.API.DTOs;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Shared;
using Shared.Services;

namespace Basket.API.Features.Baskets.GetBasket;

public class GetBasketQueryHandler(IDistributedCache distributedCache,IIdentityService identityService) : IRequestHandler<GetBasketQuery,ServiceResult<BasketDto>>
{
    public async Task<ServiceResult<BasketDto>> Handle(GetBasketQuery request, CancellationToken cancellationToken) {
        Guid userId =  identityService.GetUserId;
        var cacheKey = string.Format(BasketConst.BasketCacheKey, userId);

        var basketAsString = await distributedCache.GetStringAsync(cacheKey, token: cancellationToken);

        if (string.IsNullOrEmpty(basketAsString))
        {
            return ServiceResult<BasketDto>.Error("Basket not found", HttpStatusCode.NotFound);
        }

        var basket = JsonSerializer.Deserialize<BasketDto>(basketAsString)!;
        
        return ServiceResult<BasketDto>.SuccessAsOk(basket);

    }
}