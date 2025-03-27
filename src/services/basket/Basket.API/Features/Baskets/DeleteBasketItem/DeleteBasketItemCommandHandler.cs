using System.Net;
using System.Text.Json;
using Basket.API.Const;
using Basket.API.DTOs;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Shared;
using Shared.Services;

namespace Basket.API.Features.Baskets.DeleteBasketItem;

public class DeleteBasketItemCommandHandler(IDistributedCache distributedCache,IIdentityService identityService):IRequestHandler<DeleteBasketItemCommand,ServiceResult>
{

    public async Task<ServiceResult> Handle(DeleteBasketItemCommand request, CancellationToken cancellationToken) {
        Guid userId =  identityService.GetUserId;
        var cacheKey = string.Format(BasketConst.BasketCacheKey, userId);

        var basketAsString = await distributedCache.GetStringAsync(cacheKey, token: cancellationToken);

        if (string.IsNullOrEmpty(basketAsString))
        {
            return ServiceResult.Error("Basket not found", HttpStatusCode.NotFound);
        }

        var currentBasket = JsonSerializer.Deserialize<BasketDto>(basketAsString);

        var basketItemToDelete = currentBasket!.Items.FirstOrDefault(x => x.Id == request.Id);

        if (basketItemToDelete is null)
        {
            return ServiceResult.Error("Basket not found", HttpStatusCode.NotFound);
        }

        currentBasket.Items.Remove(basketItemToDelete);

        basketAsString = JsonSerializer.Serialize(currentBasket);
        await distributedCache.SetStringAsync(cacheKey,basketAsString, token: cancellationToken);

        return ServiceResult.SuccessAsNoContent();

    }
}