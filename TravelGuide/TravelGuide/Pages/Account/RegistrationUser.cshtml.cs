using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using DataBase.Access;
using DataBase.Models;
using Microsoft.AspNetCore.Http;
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
        private readonly DataBase.Access.Context db;
        public RegistrationUser(DataBase.Access.Context _db)
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
                    if (GetInfo.Email == "admin@admin")
                    {
                        db.Roles.AddRange(new []{
                            new Role { Id = 1, Name = "admin"}, 
                            new Role {Id = 2, Name = "user"}
                            });
                        await db.SaveChangesAsync();
                        Role adminRole = await db.Roles.FirstOrDefaultAsync(r => r.Name == "admin");
                        if (adminRole != null)
                            person.Role = adminRole;
                    }
                    else
                    {
                        Role userRole = await db.Roles.FirstOrDefaultAsync(r => r.Name == "user");
                        if (userRole != null)
                            person.Role = userRole;
                    }
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
                else
                {
                    ModelState.AddModelError("GetInfo.Email","Пользователь уже существует");
                }
            }
            return Page();
        }
    }
}