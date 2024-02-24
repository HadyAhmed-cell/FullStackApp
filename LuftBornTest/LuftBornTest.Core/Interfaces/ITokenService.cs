using LuftBornTest.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuftBornTest.Core.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user, UserManager<AppUser> manager);
    }
}