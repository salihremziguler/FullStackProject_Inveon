using CourseSalesAPI.Application.Abstractions.Services;
using CourseSalesAPI.Application.Repositories;
using CourseSalesAPI.Domain.Entities.Identity;
using CourseSalesAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CourseSalesAPI.Application.Feautures.Commands.Order.CreateOrder
{
    public class CreateOrderCommandHandler { }
        /* : IRequestHandler<CreateOrderCommandRequest, CreateOrderCommandResponse>
    {
        private readonly IBasketService _basketService;
        private readonly IPaymentService _paymentService;
        private readonly IOrderWriteRepository _orderWriteRepository;
        private readonly IOrderItemWriteRepository _orderItemWriteRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public CreateOrderCommandHandler(
            IBasketService basketService,
            IPaymentService paymentService,
            IOrderWriteRepository orderWriteRepository,
            IOrderItemWriteRepository orderItemWriteRepository,
            IHttpContextAccessor httpContextAccessor,
            UserManager<AppUser> userManager)
        {
            _basketService = basketService;
            _paymentService = paymentService;
            _orderWriteRepository = orderWriteRepository;
            _orderItemWriteRepository = orderItemWriteRepository;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<CreateOrderCommandResponse> Handle(
            CreateOrderCommandRequest request,
            CancellationToken cancellationToken)
        {
            // 1) Sepeti al
            var basket = _basketService.GetUserActiveBasket;
            if (basket == null)
            {
                return new CreateOrderCommandResponse
                {
                    IsSuccess = false,
                    Message = "Aktif sepet bulunamadı."
                };
            }

            // 2) Sepet içindeki ürünleri getir
            var basketItems = await _basketService.GetBasketItemsAsync();
            if (basketItems == null || !basketItems.Any())
            {
                return new CreateOrderCommandResponse
                {
                    IsSuccess = false,
                    Message = "Sepetiniz boş."
                };
            }

            // 3) Toplam fiyatı hesapla
            decimal totalPrice = 0;
            foreach (var item in basketItems)
            {
                if (item.Course == null)
                    throw new Exception("Course yüklenemedi.");

                totalPrice += item.Course.Price * item.Quantity;
            }

            // 4) Ödeme işlemini gerçekleştir (PaymentService)
            string paymentId;
            try
            {
                paymentId = await _paymentService.ProcessPaymentAsync(totalPrice);
            }
            catch (Exception ex)
            {
                return new CreateOrderCommandResponse
                {
                    IsSuccess = false,
                    Message = $"Ödeme sırasında hata: {ex.Message}"
                };
            }

            // 5) Sipariş oluştur
            var username = _httpContextAccessor.HttpContext.User.Identity.Name;
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == username);

            var newOrder = new Domain.Entities.Order
            {
                OrderNumber = Guid.NewGuid(),
                PaymentId = paymentId,
                TotalPrice = totalPrice,
                OrderDate = DateTime.UtcNow,
                User = user,
                UserId = user.Id,
                ShippingAddress = request.ShippingAddress,
                BillingAddress = request.BillingAddress
            };

            await _orderWriteRepository.AddAsync(newOrder);
            await _orderWriteRepository.SaveAsync();

            // 5.1) OrderItem kayıtları
            foreach (var item in basketItems)
            {
                var orderItem = new OrderItem
                {
                    OrderId = newOrder.Id,
                    CourseId = item.Course.Id,
                    CourseName = item.Course.Name,
                    Quantity = item.Quantity,
                    UnitPrice = item.Course.Price
                };
                await _orderItemWriteRepository.AddAsync(orderItem);
            }
            await _orderItemWriteRepository.SaveAsync();

            // 6) Sepeti temizle veya statüsünü değiştir (İsteğe bağlı)
            // Örneğin, _basketService içinde “sepeti sıfırla” gibi bir metot olabilir.

            return new CreateOrderCommandResponse
            {
                IsSuccess = true,
                Message = "Sipariş başarıyla oluşturuldu.",
                OrderNumber = newOrder.OrderNumber,
                PaymentId = newOrder.PaymentId
            };
        }
    }*/
}