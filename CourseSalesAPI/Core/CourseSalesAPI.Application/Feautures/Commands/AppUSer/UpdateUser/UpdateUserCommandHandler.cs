﻿using CourseSalesAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CourseSalesAPI.Application.Feautures.Commands.AppUSer.UpdateUser
{
    public static class HttpContextExtensions
    {
        public static string? GetUsernameFromToken(this HttpContext httpContext)
        {
            return httpContext.User?.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
        }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommandRequest, UpdateUserCommandResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateUserCommandHandler(UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UpdateUserCommandResponse> Handle(UpdateUserCommandRequest request, CancellationToken cancellationToken)
        {
            // Kullanıcıyı JWT'den bul
            var username = _httpContextAccessor.HttpContext.GetUsernameFromToken();
            if (username == null)
            {
                return new UpdateUserCommandResponse
                {
                    IsSuccess = false,
                    Message = "Unauthorized access"
                };
            }

            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return new UpdateUserCommandResponse
                {
                    IsSuccess = false,
                    Message = "User not found"
                };
            }

            // Kullanıcı adını güncelle
            user.UserName = request.NameSurname ?? user.UserName;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new UpdateUserCommandResponse
                {
                    IsSuccess = true,
                    Message = "Username updated successfully"
                };
            }

            return new UpdateUserCommandResponse
            {
                IsSuccess = false,
                Message = "Failed to update username"
            };
        }
    }
}