using Microsoft.EntityFrameworkCore;
using Order.Application.Contracts.Repositories;

namespace Order.Persistence.Repositories;

public class OrderRepository(AppDbContext context) : GenericRepository<Guid,Domain.Entities.Order>(context), IOrderRepository
{
 public Task<List<Domain.Entities.Order>> GetOrderByBuyerId(Guid buyerId) => context.Orders.Include(x => x.OrderItems).Where(x => x.BuyerId == buyerId).OrderByDescending(x => x.Created).ToListAsync();
}