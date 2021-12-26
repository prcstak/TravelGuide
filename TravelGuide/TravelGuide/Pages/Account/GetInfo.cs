using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace TravelGuide.Pages.Account
{
    public class GetInfo
    {
        [EmailAddress(ErrorMessage = "Некорректный Email-адрес")]
        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }
        
        public string? PhoneNumber { get; set; }
        
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$", ErrorMessage = "Слишком простой пароль")]
        [Required(ErrorMessage = "Не указан пароль")]
        public string Password { get; set; }
        
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string PasswordConfirm { get; set; }
    }
}