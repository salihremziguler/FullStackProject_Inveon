using CourseSalesAPI.Application.Repositories;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CourseSalesAPI.Application.Feautures.Commands.Course.CreateCourse
{
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommandRequest, CreateCourseCommandResponse>
    {
        private readonly ICourseWriteRepository _courseWriteRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CreateCourseCommandHandler(ICourseWriteRepository courseWriteRepository, IWebHostEnvironment webHostEnvironment)
        {
            _courseWriteRepository = courseWriteRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<CreateCourseCommandResponse> Handle(CreateCourseCommandRequest request, CancellationToken cancellationToken)
        {
            // Resim dosyasını işleme ve yükleme
            string imagePath = null;
            if (request.Image != null)
            {
                // Yükleme dizini
                string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/courses");
                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                // Benzersiz bir dosya adı oluştur
                string fileName = $"{Guid.NewGuid()}{Path.GetExtension(request.Image.FileName)}";
                string filePath = Path.Combine(uploadPath, fileName);

                // Dosyayı kaydet
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.Image.CopyToAsync(stream);
                }

                // Kaydedilen dosyanın yolunu ayarla
                imagePath = $"/uploads/courses/{fileName}";
            }

            // Kurs bilgilerini kaydet
            await _courseWriteRepository.AddAsync(new()
            {
                Name = request.Name,
                Price = request.Price,
                Description = request.Description,
                Category = request.Category,
                Image = imagePath // Resim yolu veritabanına kaydediliyor
            });

            await _courseWriteRepository.SaveAsync();

            return new CreateCourseCommandResponse();
        }
    }
}
