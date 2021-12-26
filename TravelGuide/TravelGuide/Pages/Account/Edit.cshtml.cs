using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using DataBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace TravelGuide.Pages.Account
{
    public class Edit : PageModel
    {
        private readonly DataBase.Access.PersonContext db;
        public Edit(DataBase.Access.PersonContext _db)
        {
            db = _db;
        }
        
        [BindProperty]
        public EditedInfo GetInfo { get; set; }
        [BindProperty]
        public Person Person { get; set; } 
        
        public async Task<IActionResult> OnGet(int? id)
        {
            Person = await db.Person.FirstOrDefaultAsync(p => p.Id == id);

            return Page();
        }

        public async Task<IActionResult> OnPost(int? id)
        {
            /*if (GetInfo.Email != null)
            {
                Person.Email = GetInfo.Email;
            }

            if (GetInfo.PhoneNumber != null)
            {
                Person.PhoneNumber = GetInfo.PhoneNumber;
            }

            if (GetInfo.Password != null)
            {
                Person.Password = GetInfo.Password;
            }

            db.Attach(Person).State = EntityState.Modified;
            await db.SaveChangesAsync();

            await Authentication.Authenticate(Person, HttpContext);
            
            return RedirectToPage("/Account/Profile",new  {id = Person.Id});*/
            throw new NotImplementedException();
        }
    }
    
    public class EditedInfo
    {
        [EmailAddress(ErrorMessage = "Некорректный Email-адрес")]
        public string? Email { get; set; }
        
        public string? PhoneNumber { get; set; }
        
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$", ErrorMessage = "Слишком простой пароль")]
        public string? Password { get; set; }
        
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string? PasswordConfirm { get; set; }
    }
}