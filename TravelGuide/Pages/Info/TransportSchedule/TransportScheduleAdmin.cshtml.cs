using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataBase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TravelGuide.Pages.Info.TransportSchedule;

namespace TravelGuide.Pages
{
    public class TransportScheduleAdmin : PageModel
    {
        [BindProperty]
        public List<Schedule> Schedules { get; set; }
        
        private readonly DataBase.Access.Context db;
        public TransportScheduleAdmin(DataBase.Access.Context _db)
        {
            db = _db;
        }
        
        public Person Person { get; set; }
        
        public async Task OnGet()
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
            Schedules =  await db.Schedule.ToListAsync();
            Person = await db.Person.FirstOrDefaultAsync(p => p.Id == id);
        }

        public void OnPost()
        {
            
        }
    }
}