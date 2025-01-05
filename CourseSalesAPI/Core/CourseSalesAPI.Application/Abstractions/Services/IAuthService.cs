using CourseSalesAPI.Application.Abstractions.Services.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSalesAPI.Application.Abstractions.Services
{
    public interface IAuthService : IExternalAuthentication, IInternalAuthentication
    {
       // Task PasswordResetAsnyc(string email);
       // Task<bool> VerifyResetTokenAsync(string resetToken, string userId);
    }
}