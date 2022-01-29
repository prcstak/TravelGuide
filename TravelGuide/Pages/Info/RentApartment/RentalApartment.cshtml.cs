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
        
        [BindProperty]
        public Filter Filter { get; set; } = new Filter()
        {
            Rooms = new SelectList(new List<int> {1, 2, 3, 4, 5}),
            Duration = new SelectList(new List<string> {"сутки", "неделя", "месяц"})
        };
        
        
        public List<Аdvertisement> Advertisements { get; set; }

        public async Task OnGet()
        {
            
        }
    }

    public class Filter
    {
        public SelectList Rooms { get; set; }
        public SelectList Duration { get; set; }
    }
}