using System.Linq;
using System.Threading.Tasks;
using DataBase.Access;
using DataBase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace TravelGuide.Pages.Account
{
    public class Profile : PageModel
    {
        private readonly DataBase.Access.Context db;
        public Profile(DataBase.Access.Context _db)
        {
            db = _db;
        }
        
        public Person Person { get; set; } 
        
        public async Task<IActionResult> OnGet(int? id)
        {
            Person = await db.Person.Include(u=>u.Role).FirstOrDefaultAsync(p => p.Id == id);
            if (Person == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}