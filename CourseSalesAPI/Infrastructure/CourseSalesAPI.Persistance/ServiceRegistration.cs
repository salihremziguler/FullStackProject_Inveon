using CourseSalesAPI.Application.Abstractions.Services;
using CourseSalesAPI.Application.Abstractions.Services.Authentication;
using CourseSalesAPI.Application.Repositories;
using CourseSalesAPI.Domain.Entities.Identity;
using CourseSalesAPI.Persistance.Context;
using CourseSalesAPI.Persistance.Repositories;

using CourseSalesAPI.Persistance.Services;
using CourseSalesAPI.Persistence.Services;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSalesAPI.Persistance
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {

            services.AddIdentity<AppUser, AppRole>(
                options =>
                {
                    options.Password.RequiredLength = 6;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = true;
                }).AddEntityFrameworkStores<ApplicationDbContext>()
                .AddErrorDescriber<CustomIdentityErrorDescriber>();


            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.ConnectionString));
            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            services.AddScoped<ICourseWriteRepository, CourseWriteRepository>();
            services.AddScoped<ICourseReadRepository, CourseReadRepository>();
          
            services.AddScoped<IBasketItemReadRepository, BasketItemReadRepository>();
            services.AddScoped<IBasketItemWriteRepository, BasketItemWriteRepository>();
            services.AddScoped<IBasketReadRepository, BasketReadRepository>();
            services.AddScoped<IBasketWriteRepository, BasketWriteRepository>();
     
            services.AddScoped<IOrderItemReadRepository, OrderItemReadRepository>();
            services.AddScoped<IOrderItemWriteRepository, OrderItemWriteRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IPurchasedCourseWriteRepository, PurchasedCourseWriteRepository>();
            services.AddScoped<IPurchasedCourseReadRepository, PurchasedCourseReadRepository>();


            services.AddScoped<IRoleService, RoleService>();
       
           





            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IExternalAuthentication, AuthService>();
            services.AddScoped<IInternalAuthentication, AuthService>();
           // services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IPaymentService, PaymentService>();



        }



    }
}
