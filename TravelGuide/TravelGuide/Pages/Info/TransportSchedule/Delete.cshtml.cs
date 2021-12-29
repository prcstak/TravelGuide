using System.Threading.Tasks;
using DataBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TravelGuide.Pages.Info.TransportSchedule
{
    public class Delete : PageModel
    {
        [BindProperty]
        public Schedule Schedule { get; set; }
        
        private readonly DataBase.Access.Context db;
        public Delete(DataBase.Access.Context _db)
        {
            db = _db;
        }
        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPost(int? id)
        {
            Schedule = await db.Schedule.FindAsync(id);

            if (User != null)
            {
                db.Schedule.Remove(Schedule);
                await db.SaveChangesAsync();
            }

            return RedirectToPage("./TransportScheduleAdmin");
        }
    }
}