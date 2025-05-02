using System.Text;
using MassTransit;

namespace Order.Domain.Entities;

public class Order : BaseEntity<Guid>
{
    public string Code { get; set; } = null!;
    public DateTime Created { get; set; }
    public Guid BuyerId { get; set; }
    public OrderStatus Status { get; set; }
    public int AddressId { get; set; }
    public decimal TotalPrice { get; set; }
    public float? DiscountRate { get; set; }
    public Guid PaymentId { get; set; }
    public List<OrderItem> OrderItems { get; set; } = new();
    public Address Address { get; set; } = null!;
    
    public static string GenerateCode() {
        var random = new Random();
        var orderCode = new StringBuilder(10);
        for (int i = 0; i < 10; i++)
        {
            orderCode.Append(random.Next(0, 10));
        }
        return orderCode.ToString();
    }

    public static Order CreateUnPaidOrder(Guid buyerId, float? discountRate, int addressId) {
        return new Order() {
            Id = NewId.NextGuid(),
            BuyerId = buyerId,
            Created = DateTime.Now,
            Code = GenerateCode(),
            Status = OrderStatus.WaitingForPayment,
            AddressId = addressId,
            TotalPrice = 0,
            DiscountRate = discountRate,
        };
    }
    
    public static Order CreateUnPaidOrder(Guid buyerId, float? discountRate) {
        return new Order() {
            Id = NewId.NextGuid(),
            BuyerId = buyerId,
            Created = DateTime.Now,
            Code = GenerateCode(),
            Status = OrderStatus.WaitingForPayment,
            TotalPrice = 0,
            DiscountRate = discountRate,
        };
    }

    public void AddOrderItem(Guid productId, string productName, decimal unitPrice) {
        var orderItem = new OrderItem();
        orderItem.SetItem(productId, productName, unitPrice);
        OrderItems.Add(orderItem);
        CalculateTotalPrice();
    }
    
    private void CalculateTotalPrice() {
        TotalPrice = OrderItems.Sum(x => x.UnitPrice);
        if (DiscountRate.HasValue)
        {
            TotalPrice -= TotalPrice * (decimal)DiscountRate.Value / 100;
        }
    }
    public void ApplyDiscount(float discountRate) {
        if (discountRate <= 0 || discountRate > 100)
        {
            throw new ArgumentOutOfRangeException("Discount percentage must be between 0 and 100.");
        }
        this.DiscountRate = discountRate;
        CalculateTotalPrice();
    }

    public void SetPaidStatus(Guid paymentId) {
        Status = OrderStatus.Paid;
        this.PaymentId = paymentId;
    }
}

public enum OrderStatus
{
    WaitingForPayment = 1,
    Paid = 2,
    Canceled = 3,
}