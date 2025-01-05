﻿using CourseSalesAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSalesAPI.Application.Repositories
{
    public interface IInvoiceFileWriteRepository : IWriteRepository<InvoiceFile>
    {
    }
}