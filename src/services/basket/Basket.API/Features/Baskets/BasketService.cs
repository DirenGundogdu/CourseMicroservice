using System.Text.Json;
using Basket.API.Const;
using Microsoft.Extensions.Caching.Distributed;
using Shared.Services;

namespace Basket.API.Features.Baskets;

public class BasketService(IIdentityService identityService,IDistributedCache distributedCache)
{
    private string GetCacheKey() => string.Format(BasketConst.BasketCacheKey, identityService.GetUserId);
    
    public Task<string?> GetBasketFromCache(CancellationToken cancellationToken) {
        return distributedCache.GetStringAsync(GetCacheKey(), token: cancellationToken);
    }
    
    public async Task CreateBasketCacheAsync(Data.Basket basket, CancellationToken cancellationToken) {
        var basketAsString = JsonSerializer.Serialize(basket);
        await distributedCache.SetStringAsync(GetCacheKey(), basketAsString, token:cancellationToken);
    }
}