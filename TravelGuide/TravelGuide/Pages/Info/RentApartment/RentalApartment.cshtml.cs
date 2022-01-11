using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using DataBase.Migrations;
using DataBase.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace TravelGuide.Pages
{
    public class RentalApartment : PageModel
    {
        private readonly DataBase.Access.Context db;

        public IWebHostEnvironment _appEnvironment;

        [BindProperty]
        public Filter Filter { get; set; } = new Filter()
        {
            Rooms = new SelectList(new List<int> {1, 2, 3, 4, 5}),
            Duration = new SelectList(new List<string> {"сутки", "неделя", "месяц"})
        };

        public string Result { get; set; }
        public RentalApartment(DataBase.Access.Context _db, IWebHostEnvironment appEnvironment)
        {
            db = _db;
            _appEnvironment = appEnvironment;
        }
        
        public List<Аdvertisement> Advertisements { get; set; }

        public async Task OnGet(string? dur, int? room)
        {
            Advertisements = await db.Advertisement
                .Include(p => p.Person)
                .ToListAsync();
            var roomResult = "любое";
            if (dur != null)
            {
                Advertisements = Advertisements
                    .Where(w => w.Duration == dur).ToList();
            }
            else
            {
                dur = "любой";
            }
            if (room != null)
            {
                Advertisements = Advertisements
                    .Where(w => w.Rooms == room).ToList();
                roomResult = room.ToString();
            }
            ViewData.Add("dur", dur);
            ViewData.Add("room", roomResult);
            Result = $"Все объявления по запросу: Срок сдачи: {dur}" + "\n" + $"Количество комнат: {roomResult}";
        }
    }

    public class Filter
    {
        public SelectList Rooms { get; set; }
        public SelectList Duration { get; set; }
    }
}