using System.ComponentModel.DataAnnotations;

namespace TravelGuide.Pages.Info.TransportSchedule
{
    public class ScheduleView
    {
        [Required(ErrorMessage = "Укажите время отправления")]
            public string Time { get; set; }
            
            [Required(ErrorMessage = "Укажите отсановку")]
            public string From { get; set; }

            [Required(ErrorMessage = "Укажите остановку")]
            public string To { get; set; }
    }
}