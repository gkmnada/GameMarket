using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Order.Application.Interfaces;
using Order.Persistence.Context;
using System.Security.Claims;

namespace Order.Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderContext _context;

        public OrderRepository(OrderContext context)
        {
            _context = context;
        }

        public async Task CreateOrderAsync(Domain.Entities.Order entity)
        {
            await _context.Orders.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Domain.Entities.Order>> GetOrderByUserAsync(string id)
        {
            var values = await _context.Orders.Where(x => x.UserID == id && x.IsPaid == false).ToListAsync();
            return values;
        }

        public async Task<bool> UpdateIsPaidAsync(Domain.Entities.Order entity)
        {
            var result = await _context.Orders.FirstOrDefaultAsync(x => x.OrderID == entity.OrderID);
            result.IsPaid = true;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
