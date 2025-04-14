using Shared;

namespace Basket.API.Features.Baskets.ApplyDiscountCoupon;

public record ApplyDiscountCouponCommand(string Coupon, float DiscountRate) : IRequestByServiceResult;