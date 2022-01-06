using System;
using System.Threading.Tasks;
using DataBase.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace TravelGuide.Pages.Info.RentApartment
{
    public class Edit : PageModel
    {
        private readonly DataBase.Access.Context db;

        private IWebHostEnvironment _appEnvironment;

        [BindProperty] public Аdvertisement InfoAdvertisement { get; set; }

        public Edit(DataBase.Access.Context _db, IWebHostEnvironment appEnvironment)
        {
            db = _db;
            _appEnvironment = appEnvironment;
        }

        public async Task<IActionResult> OnGet(int? id)
        {
            if (!(User.Identity.IsAuthenticated || HttpContext.Session.GetInt32("id") != null))
            {
                return RedirectToPage("/Account/Authorization");
            }
            InfoAdvertisement = await db.Advertisement.FirstOrDefaultAsync(p => p.Id == id);
            return Page();
        }

        public async Task<IActionResult> OnPost(int? id)
        {
            var ad = await db.Advertisement.FirstOrDefaultAsync(d => d.Id == id);
            ad.Info = InfoAdvertisement.Info;
            ad.Duration = InfoAdvertisement.Duration;
            ad.Rooms = InfoAdvertisement.Rooms;

            db.Attach(ad).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return RedirectToPage("./RentalApartment");
        }
    }
}