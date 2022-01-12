using DataBase.Access;
using DataBase.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace TravelGuide.Pages.Account
{
    public class AuthorizationModel : PageModel
    { 
        private readonly DataBase.Access.Context db;
        public AuthorizationModel(DataBase.Access.Context _db)
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
            if (ModelState.IsValid)
            {
                Person person = await db.Person.
                    Include(u=>u.Role).
                    FirstOrDefaultAsync(u => u.Email == UserInfo.Email 
                                             && u.Password == Hash.GetHash(UserInfo.Password));
                if (person != null)
                {
                    if (UserInfo.RememberMe)
                    {
                        await Authentication.Authenticate(person, HttpContext);
                    }
                    else
                    {
                        HttpContext.Session.SetInt32("id", person.Id);
                    }
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