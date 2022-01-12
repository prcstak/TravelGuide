using System.Threading.Tasks;
using DataBase.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace TravelGuide.Pages.Info.RentApartment
{
    public class Delete : PageModel
    {
        private readonly DataBase.Access.Context db;

        private IWebHostEnvironment _appEnvironment;
        
        [BindProperty]
        public Аdvertisement Advertisement { get; set; }

        public Delete(DataBase.Access.Context _db, IWebHostEnvironment appEnvironment)
        {
            db = _db;
            _appEnvironment = appEnvironment;
        }
        public async Task<IActionResult> OnGet(int? id)
        {
            Advertisement = await db.Advertisement.FirstOrDefaultAsync(a => a.Id == id);
            if (Advertisement != null)
            {
                db.Advertisement.Remove(Advertisement);
                await db.SaveChangesAsync();
            }
            return RedirectToPage("./RentalApartment");
        }

        public void OnPost(int? id)
        {
            
            
        }
    }
}