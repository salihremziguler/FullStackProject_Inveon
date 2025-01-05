using CourseSalesAPI.Application.Feautures.Commands.Course.CreateCourse;
using CourseSalesAPI.Application.Feautures.Commands.Course.RemoveCourse;
using CourseSalesAPI.Application.Feautures.Commands.Course.UpdateCourse;

using CourseSalesAPI.Application.Feautures.Queries.Course.GetAllCourse;
using CourseSalesAPI.Application.Feautures.Queries.Course.GetByIdCourse;

using CourseSalesAPI.Application.Repositories;
using CourseSalesAPI.Application.RequestParameters;
using CourseSalesAPI.Application.ViewModels;
using CourseSalesAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CourseSalesAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ICourseReadRepository _courseReadRepository;
        private readonly ICourseWriteRepository _courseWriteRepository;
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment webHostEnvironment;

        public CatalogController(ICourseReadRepository courseReadRepository, ICourseWriteRepository courseWriteRepository, IMediator mediator, IWebHostEnvironment webHostEnvironment)
        {
            _courseReadRepository = courseReadRepository;
            _courseWriteRepository = courseWriteRepository;
            _mediator = mediator;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllCourseQueryRequest getAllCourseQueryRequest)
        {

       GetAllCourseQueryResponse response=   await  _mediator.Send(getAllCourseQueryRequest); 
        return Ok(response);



            /*await _courseWriteRepository.AddRangeAsync(new()
            {
                new(){Id=Guid.NewGuid(),Category="a",CreatedDate=DateTime.Now,Description="B",Name="C",Price=13 } });

            await _courseWriteRepository.SaveAsync();*/
        }

       [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute] GetByIdCourseQueryRequest getByIdCourseQueryRequest)
        {
            GetByIdCourseQueryResponse response = await _mediator.Send(getByIdCourseQueryRequest);
            return Ok(response);
        }

        /* [HttpGet("{id}")]
         public async Task<IActionResult> GetCourseById([FromRoute] Guid id)
         {
             var course = await _courseReadRepository.GetByIdAsync(id.ToString());
             if (course == null)
                 return NotFound("Course not found.");

             return Ok(new
             {
                 course.Id,
                 course.Name,
                 course.Description,
                 course.Price,
                 course.Category,
                 course.Image
             });
         }*/


      [HttpPost("UploadImage/{id}")]
[Consumes("multipart/form-data")]
public async Task<IActionResult> UploadImage([FromRoute] Guid id, [FromForm] UploadImageRequest request)
{
    var course = await _courseReadRepository.GetByIdAsync(id.ToString());
    if (course == null)
        return NotFound("Course not found.");

    string uploadPath = Path.Combine(webHostEnvironment.WebRootPath, "uploads/courses");
    if (!Directory.Exists(uploadPath))
        Directory.CreateDirectory(uploadPath);

    string fileName = $"{Guid.NewGuid()}{Path.GetExtension(request.Image.FileName)}";
    string filePath = Path.Combine(uploadPath, fileName);

    using (var stream = new FileStream(filePath, FileMode.Create))
    {
        await request.Image.CopyToAsync(stream);
    }

    course.Image = $"/uploads/courses/{fileName}";
    await _courseWriteRepository.SaveAsync();

    return Ok(new { ImagePath = course.Image });
}




        [HttpPost("AddCourseWithImage")]
        public async Task<IActionResult> Post(CreateCourseCommandRequest createCourseCommandRequest)
        {
         CreateCourseCommandResponse response=await   _mediator.Send(createCourseCommandRequest);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateCourseCommandRequest updateCourseCommandRequest)
        {
            UpdateCourseCommandResponse response = await _mediator.Send(updateCourseCommandRequest);
            return Ok();
        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] RemoveCourseCommandRequest removeCourseCommandRequest)
        {
            RemoveCourseCommandResponse response = await _mediator.Send(removeCourseCommandRequest);
            return Ok();
        }

       /* [HttpPost("[action]")]
        public async Task<IActionResult> Upload([FromQuery] UploadCourseImageCommandRequest uploadCourseImageCommandRequest)
        {
            uploadCourseImageCommandRequest.Files = Request.Form.Files;
            UploadCourseImageCommandResponse response = await _mediator.Send(uploadCourseImageCommandRequest);
            return Ok();
        }
       */

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload2()
        {
            string uploadPath = Path.Combine(webHostEnvironment.WebRootPath, "resource/product-images");

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            Random random = new Random();
            foreach(IFormFile file in Request.Form.Files)
            {
                string fullPath=Path.Combine(uploadPath, $"{random.Next()}{Path.GetExtension(file.FileName)}");
                using FileStream fileStream=new(fullPath,FileMode.Create,FileAccess.Write,FileShare.None,1024*1024,useAsync:false);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
            }
            return Ok();


        }


       /* [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductImages([FromRoute] GetCourseImagesQueryRequest getCourseImagesQueryRequest)
        {
            List<GetCourseImagesQueryResponse> response = await _mediator.Send(getCourseImagesQueryRequest);
            return Ok(response);
        }
       */


        [HttpGet("GetCourses")] // Endpoint: api/Catalog/GetCourses
        public async  Task<IActionResult> GetCourses([FromQuery] GetAllCourseQueryRequest getAllCourseQueryRequest)
        {
            GetAllCourseQueryResponse response = await _mediator.Send(getAllCourseQueryRequest);
            return Ok(response);



           /* var courses = _courseReadRepository.GetAll();
            return Ok(courses);*/
        }

        [HttpGet("GetCourseById/{id}")]
        public async Task<IActionResult> GetCourseById(string id)
        {
            var product= await _courseReadRepository.GetByIdAsync(id);
            return Ok(product);

        }


       /* [HttpPost("AddCourse")]
        public async Task<IActionResult> AddCourse([FromBody] Course request)
        {
            if (request == null)
            {
                return BadRequest("Invalid course data.");
            }

            var newCourse = new Course
            {
               
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Category = request.Category,
              
            };

            await _courseWriteRepository.AddAsync(newCourse);
            await _courseWriteRepository.SaveAsync();

            return Ok(newCourse);
        }*/
    }



}

