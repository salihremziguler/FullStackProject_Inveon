using CourseSalesAPI.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSalesAPI.Application.Feautures.Commands.Course.RemoveCourse
{
    public class RemoveCourseCommandHandler : IRequestHandler<RemoveCourseCommandRequest, RemoveCourseCommandResponse>
    {
        readonly ICourseWriteRepository _courseWriteRepository;

        public RemoveCourseCommandHandler(ICourseWriteRepository courseWriteRepository)
        {
            _courseWriteRepository = courseWriteRepository;
        }

        public async Task<RemoveCourseCommandResponse> Handle(RemoveCourseCommandRequest request, CancellationToken cancellationToken)
        {
            await _courseWriteRepository.RemoveAsync(request.Id);
            await _courseWriteRepository.SaveAsync();
            return new();
        }
    }
}
