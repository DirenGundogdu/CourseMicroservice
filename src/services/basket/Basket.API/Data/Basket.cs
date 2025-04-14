using System.Text.Json.Serialization;

namespace Basket.API.Data;

public class Basket
{
    public Guid UserId { get; set; }
    public List<BasketItem> Items { get; set; } = new();
    
    public float? DiscountRate { get; set; }
    public string? Coupon { get; set; }

   [JsonIgnore] public bool IsApplyDicount => DiscountRate is > 0 && !string.IsNullOrEmpty(Coupon);
    
   [JsonIgnore] public decimal TotalPrice => Items.Sum(x => x.Price);

   [JsonIgnore] public decimal? TotalPriceWithAppliedDiscount => !IsApplyDicount ? null : Items.Sum(x => x.PriceByApplyDiscountRate);

    public Basket() {
    }
    public Basket(Guid userId, List<BasketItem> items) {
        UserId = userId;
        Items = items;
    }

    public void ApplyNewDiscount(string coupon, float discountRate) {
        Coupon = coupon;
        DiscountRate = discountRate;

        foreach (var basket in Items)
        {
            basket.PriceByApplyDiscountRate = basket.Price * (decimal)(1 - discountRate);
        }
    }

    public void ApplyAvailableDiscount() {
        
        if(!IsApplyDicount) return;
        
        foreach (var basket in Items)
        {
            basket.PriceByApplyDiscountRate = basket.Price * (decimal)(1 - DiscountRate!);
        }
    }
    
    public void ClearDiscount() {
        Coupon = null;
        DiscountRate = null;

        foreach (var basket in Items)
        {
            basket.PriceByApplyDiscountRate = null;
        }
    }
}