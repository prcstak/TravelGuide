using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataBase.Models;
using Microsoft.EntityFrameworkCore;
using TravelGuide.Pages.Info.TransportSchedule;

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
        public async Task<IActionResult> OnGet(int? id)
        {
            Person = db.Person
                .Include(p => p.Role)
                .FirstOrDefault(p => p.Id == id);
            if (Person?.RoleId == 2)
            {
                return RedirectToPage("/Info/TransportSchedule/TransportScheduleAdmin");
            }
            Schedules =  await db.Schedule.ToListAsync();
            
            return Page();

        }

        public void OnPost()
        {
            
        }
    }
}