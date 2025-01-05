using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSalesAPI.Application.ViewModels.Basket
{
    public class VM_Update_BasketItem
    {
        public string CourseItemId { get; set; }
        public int Quantity { get; set; }
    }
}