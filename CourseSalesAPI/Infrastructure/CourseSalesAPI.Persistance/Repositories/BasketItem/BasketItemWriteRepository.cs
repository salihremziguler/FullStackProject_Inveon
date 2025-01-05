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
    public class BasketItemWriteRepository : WriteRepository<BasketItem>, IBasketItemWriteRepository
    {
        public BasketItemWriteRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}