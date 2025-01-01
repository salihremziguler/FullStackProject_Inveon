using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSalesAPI.Application.Feautures.Queries.Course.GetByIdCourse
{
    public class GetByIdCourseQueryRequest:IRequest<GetByIdCourseQueryResponse>
    {
        public string Id { get; set; }
}
}
