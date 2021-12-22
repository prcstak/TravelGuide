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
using Microsoft.EntityFrameworkCore;

namespace TravelGuide.Pages.Account
{
    public class AuthorizationModel : PageModel
    { 
        private readonly PersonContext db;
        public AuthorizationModel(PersonContext _db)
        {
            db = _db;
        }
        [BindProperty]
        public Person Profile { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                Person person = await db.Person.FirstOrDefaultAsync(u => u.Email == Profile.Email  /*&& u.Password == Profile.Password*/);
                if (person != null)
                {
                    await Authentication.Authenticate(Profile.Email, HttpContext);
                    return RedirectToPage("/Index");
                }
            }
            return Page();
        }
    }
}