using CourseSalesAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSalesAPI.Application.Abstractions.Token
{
    public interface ITokenHandler
    {
         Task<DTOs.Token > CreateAccessToken(int second, AppUser appUser);
        string CreateRefreshToken();
    }
}