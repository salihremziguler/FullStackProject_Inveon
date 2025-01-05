using CourseSalesAPI.Application.Abstractions.Services;
using CourseSalesAPI.Application.Feautures.Commands.Order.CreateOrder;
using CourseSalesAPI.Application.Feautures.Queries.Order.GetAllOrders;
using CourseSalesAPI.Application.Feautures.Queries.Order.GetOrderById;
using CourseSalesAPI.Application.Repositories;
using CourseSalesAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseSalesAPI.API.Controllers
{
    [Authorize(AuthenticationSchemes = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderReadRepository _orderReadRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrdersController(IOrderReadRepository orderReadRepository, IHttpContextAccessor httpContextAccessor)
        {
            _orderReadRepository = orderReadRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("UserOrders")]
        public async Task<IActionResult> GetUserOrders()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst("id").Value;

            var orders = await _orderReadRepository.Table
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Course)
                .ToListAsync();

            var response = orders.Select(o => new
            {
                OrderId = o.Id,
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalAmount,
                Items = o.OrderItems.Select(oi => new
                {
                    CourseName = oi.Course.Name,
                    Price = oi.UnitPrice
                })
            });

            return Ok(response);
        }
    }
}