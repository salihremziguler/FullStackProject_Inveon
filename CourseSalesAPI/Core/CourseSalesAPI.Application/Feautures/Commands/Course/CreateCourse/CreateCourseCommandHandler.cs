using CourseSalesAPI.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSalesAPI.Application.Feautures.Commands.Course.CreateCourse
{
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommandRequest, CreateCourseCommandResponse>
    {
        readonly ICourseWriteRepository _productWriteRepository;
       // readonly IProductHubService _productHubService;

        public CreateCourseCommandHandler(ICourseWriteRepository productWriteRepository)
        {
            _productWriteRepository = productWriteRepository;
          //  _productHubService = productHubService;
        }

        public async Task<CreateCourseCommandResponse> Handle(CreateCourseCommandRequest request, CancellationToken cancellationToken)
        {
            await _productWriteRepository.AddAsync(new()
            {
                Name = request.Name,
                Price = request.Price,
                Description = request.Description,
                Category = request.Category,
            });
            await _productWriteRepository.SaveAsync();
          //  await _productHubService.ProductAddedMessageAsync($"{request.Name} isminde ürün eklenmiştir.");
            return new();
        }
    }
}
