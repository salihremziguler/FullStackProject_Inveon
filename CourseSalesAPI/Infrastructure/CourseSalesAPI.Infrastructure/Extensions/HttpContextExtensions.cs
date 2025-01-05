using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CourseSalesAPI.Infrastructure.Extensions
{
    public static class HttpContextExtensions
    {
        public static string? GetUsernameFromToken(this HttpContext httpContext)
        {
            return httpContext.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        }
    }
}