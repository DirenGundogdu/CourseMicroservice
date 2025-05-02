using AutoMapper;
using Order.Application.Features.Orders.Create;
using Order.Domain.Entities;

namespace Order.Application.Features.Orders;

public class OrderMapping : Profile
{
    public OrderMapping() {
        CreateMap<OrderItem, OrderItemDto>().ReverseMap();
    }
    
}