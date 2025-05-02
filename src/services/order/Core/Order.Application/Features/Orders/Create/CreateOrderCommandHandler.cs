using System.Net;
using MediatR;
using Order.Application.Contracts.Repositories;
using Order.Application.Contracts.UnitOfWork;
using Order.Domain.Entities;
using Shared;
using Shared.Services;

namespace Order.Application.Features.Orders.Create;

public class CreateOrderCommandHandler(IOrderRepository orderRepository,IGenericRepository<int,Address> addressRepository,IIdentityService identityService,IUnitOfWork unitOfWork) : IRequestHandler<CreateOrderCommand,ServiceResult>
{
    public async Task<ServiceResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken) {

        if (!request.Items.Any())
        {
            return ServiceResult.Error("Order items cannot be empty","Order must have at least one item",HttpStatusCode.BadRequest);
        }
        
        
        var newAddress = new Address() {
            Province = request.Address.Province,
            District = request.Address.District,
            Street = request.Address.Street,
            ZipCode = request.Address.ZipCode,
            Line = request.Address.Line
        };

        var order = Domain.Entities.Order.CreateUnPaidOrder(identityService.GetUserId, request.DiscountRate, newAddress.Id);
        
        order.Address = newAddress;
        
        foreach (var orderItem in request.Items)
        {
            order.AddOrderItem(orderItem.ProductId,orderItem.ProductName,orderItem.UnitPrice);
        }
        
        orderRepository.Add(order);
        await unitOfWork.SaveChangeAsync(cancellationToken);
        
        var paymentId = Guid.Empty;
        
        order.SetPaidStatus(paymentId);
        
        orderRepository.Update(order);
        await unitOfWork.SaveChangeAsync(cancellationToken);

        
        return ServiceResult.SuccessAsNoContent();
    }
}