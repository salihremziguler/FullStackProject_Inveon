﻿using CourseSalesAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSalesAPI.Application.Feautures.Commands.AppUSer.RefreshToken
{
    public class RefreshTokenLoginCommandResponse
    {
        public Token Token { get; set; }
    }
}