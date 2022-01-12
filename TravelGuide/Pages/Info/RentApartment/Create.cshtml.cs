using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataBase.Migrations;
using System.Text.Json;
using DataBase.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TravelGuide.Pages.Account;

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
            return Page();
        }

        public async Task<IActionResult> OnPost(IFormFileCollection files)
        {
            if (files.Count == 0)
            {
                checkMessage = "Прикрепите фотографию";
                return Page();
            }
            
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
            var add = new Аdvertisement { };
            var paths = await GetJson(files);
            if (paths == checkMessage)
                return Page();
            add.Address = paths;
            add.Info += Advertisement.Info;
            add.Duration = Advertisement.Duration;
            add.Rooms = Advertisement.Rooms;
            add.Person = person;
            db.Advertisement.Add(add);
            
            db.SaveChanges();

            return RedirectToPage("/Info/Hostel");
        }

        private async Task<string> GetJson(IFormFileCollection files)
        {
            Images imgs = new Images();
            foreach (var file in files)
            {
                if (!check.Contains(file.ContentType))
                {
                    checkMessage = "Фото должны быть в формате .jpg или .png";
                    return checkMessage;
                }
                imgs.List.Add(file.FileName);
                await UploadImg(file);
            }
            string jsonString = JsonSerializer.Serialize(imgs);
            return jsonString;
        }

        private async Task UploadImg(IFormFile file)
        {
            var path = _appEnvironment.WebRootPath + @"\images\RentImg\" + file.FileName;
            using (var fileStream = new FileStream( path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }
    }

    public class Images
    {
        public List<string> List { get; set; } = new List<string>();
    }
}