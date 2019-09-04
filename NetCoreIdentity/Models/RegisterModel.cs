using System.ComponentModel.DataAnnotations;

namespace NetCoreIdentity.Models
{
    public class RegisterModel
    {
        [Required]
        public string Password { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string EMail { get; set; }
    }
}