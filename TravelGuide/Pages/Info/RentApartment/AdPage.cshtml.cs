using System.Threading.Tasks;
using DataBase.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace TravelGuide.Pages.Info.RentApartment
{
    public class AdPage : PageModel
    {
        private readonly DataBase.Access.Context db;

        public IWebHostEnvironment _appEnvironment;

        public AdPage(DataBase.Access.Context _db, IWebHostEnvironment appEnvironment)
        {
            db = _db;
            _appEnvironment = appEnvironment;
        }
        
        public Аdvertisement Advertisement { get; set; }
        
        public async Task OnGet(int? id)
        {
            Advertisement = await db.Advertisement.Include(i => i.Person).FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}