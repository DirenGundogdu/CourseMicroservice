using Basket.API.DTOs;
using Shared;

namespace Basket.API.Features.Baskets.GetBasket;

public record GetBasketQuery : IRequestByServiceResult<BasketDto>;