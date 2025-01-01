using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSalesAPI.Domain.Entities
{
    public class CourseImageFile:File
    {
       // public bool Showcase { get; set; }
        public ICollection<Course> Courses { get; set; }


    }
}
