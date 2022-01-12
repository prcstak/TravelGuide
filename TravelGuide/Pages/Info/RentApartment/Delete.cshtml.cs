using System.IO;
using System.Text.Json;
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
                var mas = JsonSerializer.Deserialize<Images>(Advertisement.Address);
                foreach (var v in mas.List)
                {
                    DeleteImg(v);
                }
                db.Advertisement.Remove(Advertisement);
                await db.SaveChangesAsync();
            }
            return RedirectToPage("./RentalApartment");
        }
        
        private void DeleteImg(string file)
        {
            var path = _appEnvironment.WebRootPath + @"\images\RentImg\" + file;
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }

        public void OnPost(int? id)
        {
            
            
        }
    }
}