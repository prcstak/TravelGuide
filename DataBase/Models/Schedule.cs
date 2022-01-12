using System.ComponentModel.DataAnnotations;

namespace DataBase.Models
{
    public class Schedule
    {
        public  int Id { get; set; }
        [Required(ErrorMessage = "Укажите время отправления")]
        public string Time { get; set; }
        [Required(ErrorMessage = "Укажите отсановку")]
        public string From { get; set; }
        [Required(ErrorMessage = "Укажите остановку")]
        public string To { get; set; }
    }
}