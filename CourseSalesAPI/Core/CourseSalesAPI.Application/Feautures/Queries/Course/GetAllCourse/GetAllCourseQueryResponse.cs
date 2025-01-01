using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSalesAPI.Application.Feautures.Queries.Course.GetAllCourse
{
    public class GetAllCourseQueryResponse
    {
        public int TotalCourseCount { get; set; }
        public object Courses { get; set; }
    }
}
