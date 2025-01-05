using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSalesAPI.Application.ViewModels
{
    public class UploadImageRequest
    {
        public IFormFile Image { get; set; }
    }
}