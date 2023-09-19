using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductWebAPI.DTOs.UserDTOs
{
    public class UserValidateDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
