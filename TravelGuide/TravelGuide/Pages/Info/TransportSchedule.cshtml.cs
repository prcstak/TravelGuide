using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TravelGuide.Pages
{
    public class TransportSchedule : PageModel
    {
        [BindProperty]
        public List<Schedule> Schedule { get; set; } 
        public Schedule LSchedule { get; set; }
        public void OnGet()
        {
            Schedule = new List<Schedule>(){new Schedule("15:10", "fg","ffg")};
            for (int i = 0; i < 10; i++)
            {
                this.Schedule.Add(new Schedule($"{i}:10", "fg","ffg"));
            }
        }

        public void OnPost()
        {
            Schedule.Remove(Schedule[0]);
        }
    }

    public class Schedule
    {
        public string Time { get; set; }
        public string From { get; set; }
        public string To { get; set; }

        public Schedule(string time, string from, string to)
        {
            Time = time;
            From = from;
            To = to;
        }
    }
}