using CourseSalesAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSalesAPI.Domain.Entities
{
    public class Course:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public string Category { get; set; }

      
        public ICollection<BasketItem> BasketItems { get; set; }

        public string Image { get; set; }

    }
}
