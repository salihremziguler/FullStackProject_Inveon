using CourseSalesAPI.Application.Repositories;
using CourseSalesAPI.Domain.Entities;
using CourseSalesAPI.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSalesAPI.Persistance.Repositories
{
    public class OrderItemReadRepository : ReadRepository<OrderItem>, IOrderItemReadRepository
    {
        public OrderItemReadRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
