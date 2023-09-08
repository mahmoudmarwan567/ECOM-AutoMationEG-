using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enities.DTOs
{
    public class UserRegistrationDTO
    {
        [Key]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Name field is required")]
        public string Name { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password not matching")]
        [NotMapped]
        public string ConfirmPassword { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
