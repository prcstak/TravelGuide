using System.Collections.Generic;
using System.Threading.Tasks;
using DataBase.Migrations;
using DataBase.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace TravelGuide.Pages
{
    public class RentalApartment : PageModel
    {
        private readonly DataBase.Access.Context db;

        public IWebHostEnvironment _appEnvironment;

        public RentalApartment(DataBase.Access.Context _db, IWebHostEnvironment appEnvironment)
        {
            db = _db;
            _appEnvironment = appEnvironment;
        }
        
        public List<Аdvertisement> Advertisements { get; set; }

        public async Task OnGet()
        {
            Advertisements = await db.Advertisement.ToListAsync();
        }
    }
}