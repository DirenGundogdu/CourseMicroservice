using System.Text.Json.Serialization;

namespace Basket.API.DTOs;

public record BasketDto
{
    [JsonIgnore] public bool IsApplyDicount => DiscountRate is > 0 && !string.IsNullOrEmpty(Coupon);
    public List<BasketItemDto> Items { get; set; } = new();
    public float? DiscountRate { get; set; }
    public string? Coupon { get; set; }


    public decimal TotalPrice => Items.Sum(x => x.Price);

    public decimal? TotalPriceWithAppliedDiscount => !IsApplyDicount ? null : Items.Sum(x => x.PriceByApplyDiscountRate);
    
    public BasketDto() {
    }

    public BasketDto( List<BasketItemDto> items) {
        Items = items;
    }

}