using System;
using System.Linq;
using DataBase.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TravelGuide.Pages
{
    public class TransportSchedule : PageModel
    {
        [BindProperty]
        public List<Schedule> Schedules { get; set; }
        
        private readonly DataBase.Access.Context db;
        public TransportSchedule(DataBase.Access.Context _db)
        {
            db = _db;
        }
        
        public Person Person { get; set; }
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
            Person = db.Person
                .Include(p => p.Role)
                .FirstOrDefault(p => p.Id == id);
            if (Person?.RoleId == 1)
            {
                return RedirectToPage("./TransportScheduleAdmin");
            }
            Schedules =  await db.Schedule.ToListAsync();
            return Page();
        }

        public void OnPost()
        {
            
        }
    }
}