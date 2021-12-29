using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using DataBase.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace TravelGuide.Pages.Account
{
    public class Edit : PageModel
    {
        private readonly DataBase.Access.Context db;

        public Edit(DataBase.Access.Context _db)
        {
            db = _db;
        }

        [BindProperty] public GetInfo GetInfo { get; set; }
        [BindProperty] public Person Person { get; set; }

        public async Task<IActionResult> OnGet()
        {
            int? id;
            if (User.Identity.IsAuthenticated)
            {
                id = Int32.Parse(User.Identity.Name);
            }

            else
            {
                id = HttpContext.Session.GetInt32("id");
            }
            Person = await db.Person.FirstOrDefaultAsync(p => p.Id == id);

            return Page();
        }

        public async Task<IActionResult> OnPost(int? id)
        {
            Person = await db.Person.Include(u=>u.Role).FirstOrDefaultAsync(u => u.Id == id);
            Person.PhoneNumber = GetInfo.PhoneNumber;
            Person.FirstName = GetInfo.FirstName;
            Person.LastName = GetInfo.LastName;
            db.Attach(Person).State = EntityState.Modified;
            await db.SaveChangesAsync();

            await Authentication.Authenticate(Person, HttpContext);

            return RedirectToPage("/Account/Profile", new {id = Person.Id});
        }
    }
}