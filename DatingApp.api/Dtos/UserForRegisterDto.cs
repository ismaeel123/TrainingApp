using System.ComponentModel.DataAnnotations;

namespace DatingApp.api.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(8,MinimumLength=4,ErrorMessage="mynf3sh gheer mabeen 4 we 8 ya 7obbby")]
        public string Password { get; set; }
    }
}