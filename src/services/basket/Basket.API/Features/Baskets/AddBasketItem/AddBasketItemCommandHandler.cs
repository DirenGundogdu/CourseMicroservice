using System.Text.Json;
using Basket.API.Const;
using Basket.API.DTOs;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Shared;
using Shared.Services;

namespace Basket.API.Features.Baskets.AddBasketItem;

public class AddBasketItemCommandHandler(IDistributedCache distributedCache,IIdentityService identityService) : IRequestHandler<AddBasketItemCommand,ServiceResult>
{

    public async Task<ServiceResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken) {
        Guid userId = identityService.GetUserId;
        var cacheKey = string.Format(BasketConst.BasketCacheKey, userId);

        var basketAsString = await distributedCache.GetStringAsync(cacheKey, token: cancellationToken);

        BasketDto? currentBasket;

        var newBasketItem = new BasketItemDto(request.CourseId, request.CourseName, request.ImageUrl, request.CoursePrice, null);

        if (string.IsNullOrEmpty(basketAsString))
        {
            currentBasket = new BasketDto(userId, [newBasketItem]);
            await CreateCacheAsync(currentBasket, cacheKey, cancellationToken);
            return ServiceResult.SuccessAsNoContent();
        }
        
        currentBasket = JsonSerializer.Deserialize<BasketDto>(basketAsString);
        var existingBasketItem = currentBasket.Items.FirstOrDefault(x => x.Id == request.CourseId);

        if (existingBasketItem is not null)
        {
            currentBasket.Items.Remove(existingBasketItem);
        }
        currentBasket.Items.Add(newBasketItem);

        await CreateCacheAsync(currentBasket, cacheKey, cancellationToken);
        return ServiceResult.SuccessAsNoContent();
    }
    private async Task CreateCacheAsync(BasketDto basket, string cacheKey, CancellationToken cancellationToken) {
        var basketAsString = JsonSerializer.Serialize(basket);
        await distributedCache.SetStringAsync(cacheKey, basketAsString, token:cancellationToken);
    }
}