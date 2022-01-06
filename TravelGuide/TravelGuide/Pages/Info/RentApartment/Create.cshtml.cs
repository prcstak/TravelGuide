using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DataBase.Migrations;
using DataBase.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace TravelGuide.Pages.Info.RentApartment
{
    public class Create : PageModel
    {
        private readonly DataBase.Access.Context db;

        private IWebHostEnvironment _appEnvironment;

        [BindProperty] 
        public Аdvertisement Advertisement { get; set; }

        public Create(DataBase.Access.Context _db, IWebHostEnvironment appEnvironment)
        {
            db = _db;
            _appEnvironment = appEnvironment;
        }

        private List<string> check = new List<string>() {"image/jpeg", "image/png"};
        public string checkMessage;

        public IActionResult OnGet()
        {
            if (!(User.Identity.IsAuthenticated || HttpContext.Session.GetInt32("id") != null))
            {
                return RedirectToPage("/Account/Authorization");
            }
            else
            {
                return Page();
            }
        }

        public async Task<IActionResult> OnPost(IFormFileCollection files)
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

            var person = await db.Person.FirstOrDefaultAsync(p => p.Id == id);

            string paths = "";
            var add = new Аdvertisement
                { };
            foreach (var file in files)
            {
                if (!check.Contains(file.ContentType))
                {
                    checkMessage = "Фото должны быть в формате .jpg или .png";
                    return Page();
                }

                paths += file.FileName + ";";
                using (var fileStream = new FileStream(
                    _appEnvironment.WebRootPath + @"\images\RentImg\" + file.FileName,
                    FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }

            add.Address = paths.Remove(paths.Length - 1);
            add.Info += Advertisement.Info;
            add.Duration = Advertisement.Duration;
            add.Rooms = Advertisement.Rooms;
            add.Person = person;
            db.Advertisement.Add(add);
            db.SaveChanges();

            return RedirectToPage("/Info/RentApartment/RentalApartment");
        }
    }
}