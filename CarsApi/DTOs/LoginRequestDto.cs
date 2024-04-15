using System.ComponentModel.DataAnnotations;

namespace CarsApi.DTOs
    {
    public class LoginRequestDto
        {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }




        }
    }
