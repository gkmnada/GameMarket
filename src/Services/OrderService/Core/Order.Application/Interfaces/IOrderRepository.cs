﻿using Order.Domain.Entities;

namespace Order.Application.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Domain.Entities.Order>> GetOrderByUserAsync(string id);
        Task CreateOrderAsync(Domain.Entities.Order entity);
        Task UpdateIsPaidAsync(List<Domain.Entities.Order> entity);
    }
}
