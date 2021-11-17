using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TravelGuide.Pages.Account
{
    public class RegistrationUser : PageModel
    {
        [BindProperty]
        public GetInfo GetInfo { get; set; }
        
        public void OnGet()
        {
            
        }
    }

    public class GetInfo
    {
        [EmailAddress(ErrorMessage = "Некорректный Email-адрес")]
        [Required(ErrorMessage = "Необходимо ввести Email")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Полезная информация для аккаунта")]
        public string PhoneNumber { get; set; }
        
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$", ErrorMessage = "Неверный пароль")]
        [Required(ErrorMessage = "Необходимо придумать пароль")]
        public string Password { get; set; }
        
        [Compare(nameof(Password), ErrorMessage = "Пароли не совпадают")]
        [Required(ErrorMessage = "Необходимо повторить ввод пароль")]
        public string PasswordConfirm { get; set; }
    }
}