using Library.Infrastructure.LibraryDBContext;
using Library.Models.Models;
using Library.Models.ViewModels;
using Library.Services.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Library.Services.IRepositories
{
    public class AuthRepository : IAuthRepository
    {

        private readonly IRepository<Users> _usersRepository;
        private readonly IRepository<Roles> _rolesRepository;
        private readonly LibraryDbContext _libraryDbContext;
        private IConfiguration _config;
        public AuthRepository(IRepository<Roles> rolesRepository, IRepository<Users> usersRepository, LibraryDbContext libraryDbContext,
            IConfiguration config
            )
        {
            _config = config;
            _rolesRepository = rolesRepository;
            _usersRepository = usersRepository;
            _libraryDbContext = libraryDbContext;

        }
        public async Task<AuthReturnDto> AuthenticateUserAsync(UsersDto dto)
        {

            AuthReturnDto result = new AuthReturnDto();
            var data =  _libraryDbContext.Users.FirstOrDefault(c =>
                                    (c.UserName == dto.UserName)
                                  && c.Password == dto.Password);

            if (data == null) { result.ErrMsg = "Invalid Username/Password!"; return result; }
       

            var userData = await _usersRepository.GetByIdAsync(data.UserId);
            var roleIdList = _libraryDbContext.RoleAssigns.Where(x => x.UserId == data.UserId).Select(x => x.RoleId).ToList();
            var roles = _libraryDbContext.Roles.Where(m=> roleIdList.Contains(m.RoleId)).ToList();

            // generating jwt token
            var tokenString = GenerateJSONWebToken(data.UserName, (int)data.UserId, roles);
            result.Token = tokenString;
            result.UserName = data.UserName;
            result.UserId = data.UserId;

            return result;
        }


        private string GenerateJSONWebToken(string userName, int userId,List<Roles> roles )
        {
            var data = _config["Jwt:Key"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim("UserName", userName),
                new Claim("UserId", userId.ToString()),
                new Claim("UserRoles",JsonConvert.SerializeObject(roles).ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(10),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
