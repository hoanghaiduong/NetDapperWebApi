using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetDapperWebApi.DTO;
using NetDapperWebApi.Entities;
using NetDapperWebApi.Models;

namespace NetDapperWebApi.Common.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> SignInAsync(AuthDTO dto);
        Task<User> SignUpAsync(AuthDTO dto);
    }
}