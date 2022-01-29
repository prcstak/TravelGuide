using System;
using System.Linq;
using DataBase.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TravelGuide.Pages
{
    public class TransportSchedule : PageModel
    {
        [BindProperty] public List<Schedule> Schedules { get; set; }


        public void OnGet()
        {
            Schedules = new List<Schedule>()
            {
                new Schedule() {From = "Спортивная", To = "Комбинат Здоровье", Time = "10:00"},
                new Schedule() {From = "Спортивная", To = "Комбинат Здоровье", Time = "11:00"},
                new Schedule() {From = "Спортивная", To = "Комбинат Здоровье", Time = "12:00"},
                new Schedule() {From = "Комбинат Здоровье", To = "Спортивная", Time = "10:20"},
                new Schedule() {From = "Комбинат Здоровье", To = "Спортивная", Time = "11:20"},
                new Schedule() {From = "Комбинат Здоровье", To = "Спортивная", Time = "12:20"},
            };
        }

        public void OnPost()
        {
        }
    }
}