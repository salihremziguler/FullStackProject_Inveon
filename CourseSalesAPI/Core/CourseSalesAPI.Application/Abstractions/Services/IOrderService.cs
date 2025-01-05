using CourseSalesAPI.Application.DTOs.Order;
using CourseSalesAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSalesAPI.Application.Abstractions.Services
{
    public interface IOrderService
    {
        /// <summary>
        /// Sipariş oluşturur.
        /// </summary>
        Task<Order> CreateOrderAsync(string userId, string paymentId, decimal totalPrice, string shippingAddress, string billingAddress, List<OrderItem> items);

        /// <summary>
        /// Kullanıcıya ait tüm siparişleri getirir.
        /// </summary>
        Task<List<Order>> GetOrdersByUserAsync(string userId);

        /// <summary>
        /// Belirtilen siparişi getirir.
        /// </summary>
        Task<Order> GetOrderByIdAsync(string orderId);
    }
}