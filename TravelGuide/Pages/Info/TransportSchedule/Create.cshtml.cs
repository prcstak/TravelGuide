using System.Collections.Generic;
using System.Threading.Tasks;
using DataBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TravelGuide.Pages.Info.TransportSchedule
{
    public class Create : PageModel
    {
        [BindProperty]
        public ScheduleView Schedules { get; set; }
        
        private readonly DataBase.Access.Context db;
        public Create(DataBase.Access.Context _db)
        {
            db = _db;
        }
        
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            var time = new Schedule()
            {
                Time = Schedules.Time,
                From = Schedules.From,
                To = Schedules.To
            };

            db.Schedule.Add(time);
            await db.SaveChangesAsync();
            return RedirectToPage("/Info/TransportSchedule/TransportSchedule");
        }
    }
}