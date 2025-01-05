// CourseSalesAPI.Persistence/Services/OrderService.cs
using CourseSalesAPI.Application.Abstractions.Services;
using CourseSalesAPI.Application.Repositories;
using CourseSalesAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseSalesAPI.Persistence.Services
{
    public class OrderService { } /*: IOrderService
    {
        private readonly IOrderWriteRepository _orderWriteRepository;
        private readonly IOrderReadRepository _orderReadRepository;
        private readonly IOrderItemWriteRepository _orderItemWriteRepository;

        public OrderService(
            IOrderWriteRepository orderWriteRepository,
            IOrderReadRepository orderReadRepository,
            IOrderItemWriteRepository orderItemWriteRepository)
        {
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;
            _orderItemWriteRepository = orderItemWriteRepository;
        }

        public async Task<Order> CreateOrderAsync(
            string userId,
            string paymentId,
            decimal totalPrice,
            string shippingAddress,
            string billingAddress,
            List<OrderItem> items)
        {
            // Yeni sipariş oluştur
            var order = new Order
            {
                OrderNumber = Guid.NewGuid(),
                UserId = userId,
                PaymentId = paymentId,
                TotalPrice = totalPrice,
                ShippingAddress = shippingAddress,
                BillingAddress = billingAddress,
                OrderDate = DateTime.UtcNow
            };

            // Siparişi kaydet
            await _orderWriteRepository.AddAsync(order);
            await _orderWriteRepository.SaveAsync();

            // Sipariş kalemlerini (OrderItems) kaydet
            foreach (var item in items)
            {
                item.OrderId = order.Id;
                await _orderItemWriteRepository.AddAsync(item);
            }
            await _orderItemWriteRepository.SaveAsync();

            return order;
        }

        public async Task<List<Order>> GetOrdersByUserAsync(string userId)
        {
            // Kullanıcıya ait siparişleri getir
            return await _orderReadRepository
                .GetWhere(o => o.UserId == userId)
                .Include(o => o.OrderItems) // Sipariş kalemlerini de getir
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(string orderId)
        {
            // Belirtilen ID'ye ait siparişi getir
            return await _orderReadRepository
                .GetWhere(o => o.Id.ToString() == orderId)
                .Include(o => o.OrderItems) // Sipariş kalemlerini de getir
                .FirstOrDefaultAsync();
        }
    }*/
}
