using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using DataBase.Access;
using DataBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using TravelGuide.Pages.Account; 

namespace TravelGuide.Pages.Account
{
    public class RegistrationUser : PageModel
    {
        private readonly DataBase.Access.PersonContext db;
        public RegistrationUser(DataBase.Access.PersonContext _db)
        {
            db = _db;
        }
        
        [BindProperty]
        public GetInfo GetInfo { get; set; }
        
        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                Person person = await db.Person.FirstOrDefaultAsync(u => u.Email == GetInfo.Email);
                if (person == null)
                {
                    // добавляем пользователя в бд
                    person = new Person()
                    {
                        Email = GetInfo.Email,
                        Password = Hash.GetHash(GetInfo.Password),
                        PhoneNumber = GetInfo.PhoneNumber
                    };
                    db.Person.Add(person);
                        
                    await db.SaveChangesAsync();
                    await Authentication.Authenticate(person, HttpContext);
                    return RedirectToPage("/Index");
                }
                else
                {
                    ModelState.AddModelError("GetInfo.Email","Пользователь уже существует");
                }
            }
            return Page();
        }
    }
}