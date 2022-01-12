using System.Collections.Generic;
using System.Threading.Tasks;
using DataBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TravelGuide.Pages.Info.TransportSchedule;

namespace TravelGuide.Pages.Info.TransportSchedule
{
    public class Edit : PageModel
    {
        public Schedule Schedules { get; set; }
        [BindProperty]
        public ScheduleView GetSchedule { get; set; }
        
        private readonly DataBase.Access.Context db;
        public Edit(DataBase.Access.Context _db)
        {
            db = _db;
        }
        
        public async Task OnGet(int? id)
        {
            Schedules = await db.Schedule.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IActionResult> OnPost(int? id)
        {
            Schedules = await db.Schedule.FirstOrDefaultAsync(s => s.Id == id);
            Schedules.From = GetSchedule.From;
            Schedules.To = GetSchedule.To;
            Schedules.Time = GetSchedule.Time;
            db.Attach(Schedules).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToPage("/Info/TransportSchedule/TransportSchedule");
        }
    }
}