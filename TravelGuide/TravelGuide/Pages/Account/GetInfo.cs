using System.ComponentModel.DataAnnotations;

namespace TravelGuide.Pages.Account
{
    public class GetInfo
    {
        [EmailAddress(ErrorMessage = "Некорректный Email-адрес")]
        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Номер необходим для создания объявлений")]
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$", ErrorMessage = "Слишком простой пароль")]
        [Required(ErrorMessage = "Не указан пароль")]
        public string Password { get; set; }
        
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string PasswordConfirm { get; set; }
        
        public bool RememberMe { get; set; }
    }
}