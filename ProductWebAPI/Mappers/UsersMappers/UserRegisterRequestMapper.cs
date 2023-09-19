using ProductAppAPI.Entities;
using ProductAppAPI.Mappers;
using ProductWebAPI.DTOs.UserDTOs;

namespace ProductWebAPI.Mappers.UsersMappers
{
    public class UserRegisterRequestMapper : IMapper<UserRegisterDTO, User>
    {
        public User Map(UserRegisterDTO entity)
        {
            return new()
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                UserName = entity.UserName,
                Email = entity.Email,
                Password = entity.Password
            };
        }
    }
}
