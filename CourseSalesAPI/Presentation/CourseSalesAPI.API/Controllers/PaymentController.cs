using CourseSalesAPI.Application.Abstractions.Services;
using CourseSalesAPI.Application.Repositories;
using CourseSalesAPI.Application.ViewModels.Paymet;
using CourseSalesAPI.Domain.Entities;
using CourseSalesAPI.Domain.Entities.Identity;
using CourseSalesAPI.Persistance.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace CourseSalesAPI.API.Controllers
{
    [Authorize(AuthenticationSchemes = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly IPurchasedCourseWriteRepository _purchasedCourseWriteRepository;
        private readonly IPurchasedCourseReadRepository _purchasedCourseReadRepository;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public PaymentController(IBasketService basketService, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IPurchasedCourseWriteRepository purchasedCourseWriteRepository, IPurchasedCourseReadRepository purchasedCourseReadRepository, UserManager<AppUser> userManager)
        {
            _basketService = basketService;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _purchasedCourseWriteRepository = purchasedCourseWriteRepository;
            _purchasedCourseReadRepository = purchasedCourseReadRepository;
            _userManager = userManager;
        }

        [HttpPost("Pay")]
        public async Task<IActionResult> MakePayment([FromBody] VM_PaymentRequest paymentRequest)
        {
            try
            {
                var basketItems = await _basketService.GetBasketItemsAsync();

                if (basketItems == null || !basketItems.Any())
                {
                    return BadRequest("Sepet boş.");
                }

                decimal totalAmount = basketItems.Sum(item => item.Course.Price * item.Quantity);

                StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];

                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)(totalAmount * 100),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" },
                };

                var service = new PaymentIntentService();
                var paymentIntent = service.Create(options);

                // Ödeme başarılıysa satın alma kaydını oluştur
               
                    var username = _httpContextAccessor.HttpContext.User?.Identity?.Name;
                    var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == username);

                    foreach (var item in basketItems)
                    {
                        var purchasedCourse = new PurchasedCourse
                        {
                            Id = Guid.NewGuid(),
                            UserId = user.Id,
                            CourseId = item.CourseId,
                            PurchaseDate = DateTime.UtcNow
                        };
                        await _purchasedCourseWriteRepository.AddAsync(purchasedCourse);
                    }

                    await _purchasedCourseWriteRepository.SaveAsync();
                

                return Ok(new
                {
                    ClientSecret = paymentIntent.ClientSecret,
                    PaymentIntentId = paymentIntent.Id,
                    TotalAmount = totalAmount
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Bir hata oluştu: {ex.Message}" });
            }
        }


        [HttpGet("PurchasedCourses")]
        public async Task<IActionResult> GetPurchasedCourses()
        {
            var username = _httpContextAccessor.HttpContext.User?.Identity?.Name;
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == username);

            var purchasedCourses = await _purchasedCourseReadRepository.Table
                .Where(pc => pc.UserId == user.Id)
                .Include(pc => pc.Course)
                .ToListAsync();

            var response = purchasedCourses.Select(pc => new
            {
                CourseName = pc.Course.Name,
                PurchaseDate = pc.PurchaseDate,
                Price = pc.Course.Price
            });

            return Ok(response);
        }





    }
}