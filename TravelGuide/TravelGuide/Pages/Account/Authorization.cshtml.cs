using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using DataBase.Access;
using DataBase.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace TravelGuide.Pages.Account
{
    public class AuthorizationModel : PageModel
    { 
        private readonly DataBase.Access.PersonContext db;
        public AuthorizationModel(DataBase.Access.PersonContext _db)
        {
            db = _db;
        }
        [BindProperty]
        public UserInfo UserInfo { get; set; }
        
        public string PassVerification { get; set; }
        public void OnGet()
        {
            if (PassVerification != null)
            {
                
            }
                
        }

        public async Task<IActionResult> OnPost()
        {
            string s = null;
            if (ModelState.IsValid)
            {
                Person person = await db.Person.FirstOrDefaultAsync(u => u.Email == UserInfo.Email  && u.Password == UserInfo.Password);
                if (person != null)
                {
                    await Authentication.Authenticate(person, HttpContext);
                    return RedirectToPage("/Index");
                }
                else
                {
                    PassVerification = "Неверно введен логин или пароль";
                }
            }

            return Page();
        }
    }

    public class UserInfo
    {
        [EmailAddress(ErrorMessage = "Некорректный Email-адрес")]
        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Не указан пароль")]
        public string Password { get; set; }
        
        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }
    }
}