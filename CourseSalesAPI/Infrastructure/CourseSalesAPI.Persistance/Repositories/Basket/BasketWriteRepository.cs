﻿using CourseSalesAPI.Application.Repositories;
using CourseSalesAPI.Domain.Entities;
using CourseSalesAPI.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSalesAPI.Persistance.Repositories
{
    public class BasketWriteRepository : WriteRepository<Basket>, IBasketWriteRepository
    {
        public BasketWriteRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}