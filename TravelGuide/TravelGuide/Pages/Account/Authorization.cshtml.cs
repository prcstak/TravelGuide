using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TravelGuide.Pages.Account
{
    public class AuthorizationModel : PageModel
    {
        [BindProperty]
        public Profile Profile { get; set; }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            
        }

    }

    public class Profile
    {
        [EmailAddress(ErrorMessage = "Некорректный Email-адрес")]
        [Required(ErrorMessage = "Необходимо ввести Email")]
        public string Email { get; set; }
        
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$", ErrorMessage = "Неверный пароль")]
        [Required(ErrorMessage = "Необходимо ввести пароль")]
        public string Password { get; set; }
    }
}