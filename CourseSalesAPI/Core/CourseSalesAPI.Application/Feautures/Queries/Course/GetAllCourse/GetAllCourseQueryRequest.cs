using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSalesAPI.Application.Feautures.Queries.Course.GetAllCourse
{
    public class GetAllCourseQueryRequest : IRequest<GetAllCourseQueryResponse>
    {
        
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 15;
    }
}
