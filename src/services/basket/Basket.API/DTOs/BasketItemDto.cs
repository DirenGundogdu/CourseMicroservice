namespace Basket.API.DTOs;

public record BasketItemDto(Guid Id, string Name,string ImageUrl,decimal Price,decimal? PriceByApplyDiscountRate);