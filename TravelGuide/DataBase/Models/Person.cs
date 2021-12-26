using System.ComponentModel.DataAnnotations;

namespace DataBase.Models
{
    public class Person
    {
        public int Id { get; set; }
        
        public string Email { get; set; }
        
        public string? PhoneNumber { get; set; }
        
        public string Password { get; set; }
        
    }
}