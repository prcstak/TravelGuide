using System;
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

        public void OnGet()
        {

        }

        public async Task OnPost(IFormFileCollection files)
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
            foreach (var file in files)
            {
                paths += file.FileName + ";";
                using (var fileStream = new FileStream(
                    _appEnvironment.WebRootPath + @"\images\RentImg\" + file.FileName,
                    FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }

            var add = new Аdvertisement
            {
                Address = paths.Remove(paths.Length - 1),
                Info = Advertisement.Info,
                Duration = Advertisement.Duration,
                Rooms = Advertisement.Rooms,
                Person = person
            };
            db.Advertisement.Add(add);
            db.SaveChanges();
        }
    }
}