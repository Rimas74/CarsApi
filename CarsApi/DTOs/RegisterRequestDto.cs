using System.ComponentModel.DataAnnotations;

namespace CarsApi.DTOs
    {
    public class RegisterRequestDto
        {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string[] Roles { get; set; }


        }
    }
