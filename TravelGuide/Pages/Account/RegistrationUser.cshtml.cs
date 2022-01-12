using DataBase.Access;
using DataBase.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace TravelGuide.Pages.Account
{
    public class RegistrationUser : PageModel
    {
        private readonly DataBase.Access.Context db;

        public RegistrationUser(DataBase.Access.Context _db)
        {
            db = _db;
        }

        [BindProperty] public GetInfo GetInfo { get; set; }

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
                    person = new Person()
                    {
                        Email = GetInfo.Email,
                        Password = Hash.GetHash(GetInfo.Password),
                        PhoneNumber = GetInfo.PhoneNumber
                    };
                    Role userRole = await db.Roles.FirstOrDefaultAsync(r => r.Name == "user");
                    if (userRole != null)
                        person.Role = userRole;
                    db.Person.Add(person);
                    await db.SaveChangesAsync();
                    if (GetInfo.RememberMe)
                    {
                        await Authentication.Authenticate(person, HttpContext);
                    }
                    else
                    {
                        HttpContext.Session.SetInt32("id", person.Id);
                    }

                    return RedirectToPage("/Index");
                }
                ModelState.AddModelError("GetInfo.Email", "Пользователь уже существует");
            }
            return Page();
        }
    }
}