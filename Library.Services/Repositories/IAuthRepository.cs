using Library.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Repositories
{
    public interface IAuthRepository
    {
        Task<AuthReturnDto> AuthenticateUserAsync(UsersDto dto);
    }
}
