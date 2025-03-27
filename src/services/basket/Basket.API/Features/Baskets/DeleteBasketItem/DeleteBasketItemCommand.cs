using Shared;

namespace Basket.API.Features.Baskets.DeleteBasketItem;

public record DeleteBasketItemCommand(Guid Id) : IRequestByServiceResult;