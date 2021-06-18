using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Models
{
    public class RegisterModel
    {
        [MinLength(3)]
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [MinLength(6)]
        public string Password { get; set; }
    }
}