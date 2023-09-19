using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Text;
using ProductAppAPI.Entities;
using ProductAppAPI.ServiceContracts;
using ProductListApp.Persistence.Contexts;
using ProductAppAPI.Helpers;
using ProductWebAPI.DTOs.UserDTOs;

namespace ProductAppAPI.Services
{
    public class UsersService : IUsersService
    {

        private readonly ProductListAppDbContext context;

        public UsersService(ProductListAppDbContext dbContext)
        {
            this.context = dbContext;
        }

        public async Task<IActionResult> Authenticate(UserValidateDTO userObject)
        {
            if (userObject == null)
            {
                throw new Exception();
            }
            var user = await context.Users
                .FirstOrDefaultAsync(user => user.UserName == userObject.UserName);

            if (user == null)
            {
                throw new Exception(message: "Credentials are not correct!");
            }

            if (!PasswordHasher.VerifyPassword(userObject.Password, user.Password))
            {
                throw new Exception(message: "Credentials are not correct!");
            }

            user.Token = CreateJwt(user);
            return new OkObjectResult(new
            {
                Token = user.Token,
                Message = "Login Success!"
            });
        }

        public async Task<IActionResult> RegisterUser(User user)
        {
            if (user == null)
            {
                throw new Exception();
                return new BadRequestResult();
            }

            if (await CheckUserNameExistAsync(user.UserName))
            {
                throw new Exception(message: "Username has already exist!");
                return new BadRequestObjectResult(new { message = "" });
            }

            if (await CheckEmailExistAsync(user.Email))
            {
                throw new Exception(message : "Email has already exist!");
                return new BadRequestObjectResult(new { message = "Email has already exist!" });
            }

            var passwordMessage = CheckPasswordStrength(user.Password);
            if (!string.IsNullOrEmpty(passwordMessage))
                throw new Exception(message: passwordMessage.ToString());
            //return new BadRequestObjectResult(new { Message = passwordMessage.ToString() });

            user.Password = PasswordHasher.HashPassword(user.Password);
            user.Role = "User";
            user.Token = string.Empty;

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return new OkObjectResult(new
            {
                Message = "User Registered!"
            });
        }

        private Task<bool> CheckUserNameExistAsync(string userName)
            => context.Users.AnyAsync(user => user.UserName == userName);

        private Task<bool> CheckEmailExistAsync(string email)
            => context.Users.AnyAsync(user => user.Email == email);

        private static string CheckPasswordStrength(string pass)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (pass.Length < 9)
            {
                stringBuilder.Append("Minimum password length should be 8" + Environment.NewLine);
            }
            if (!(Regex.IsMatch(pass, "[a-z]") && Regex.IsMatch(pass, "[A-Z]") && Regex.IsMatch(pass, "[0-9]")))
            {
                stringBuilder.Append("Password should be AlphaNumeric" + Environment.NewLine);
            }
            if (!Regex.IsMatch(pass, "[<,>,@,!,#,$,%,^,&,*,(,),_,+,\\[,\\],{,},?,:,;,|,',\\,.,/,~,`,-,=]"))
            {
                stringBuilder.Append("Password should contain special charcter" + Environment.NewLine);
            }
            return stringBuilder.ToString();
        }

        private string CreateJwt(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("veryverysecret......");
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Name,$"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
            });

            var creadentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = creadentials
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);

        }

    }
}
