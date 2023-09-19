using Microsoft.AspNetCore.Mvc;
using ProductAppAPI.Entities;
using ProductWebAPI.DTOs.UserDTOs;

namespace ProductAppAPI.ServiceContracts
{
    public interface IUsersService
    {
        Task<IActionResult> Authenticate(UserValidateDTO user);

        Task<IActionResult> RegisterUser(User user);

    }
}
