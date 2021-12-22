using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using DataBase.Access;
using DataBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace TravelGuide.Pages.Account
{
    public class RegistrationUser : PageModel
    {
        private readonly PersonContext db;
        public RegistrationUser(PersonContext _db)
        {
            db = _db;
        }
        
        [BindProperty]
        public Person GetInfo { get; set; }
        
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
                    db.Person.Add(
                        new Person()
                        {
                            Email = GetInfo.Email,
                            Password = GetInfo.Password,
                            PhoneNumber = GetInfo.PhoneNumber
                        });
                    await db.SaveChangesAsync();
                    await Authentication.Authenticate(GetInfo.Email, HttpContext);
                    return RedirectToPage("/Index");
                }
            }

            return Page();
        }
    }
}