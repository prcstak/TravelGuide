using System.Collections.Generic;
using DataBase.Migrations;

namespace DataBase.Models
{
    public class Person
    {
        public int Id { get; set; }
        
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public string Password { get; set; }
        
        public ICollection<Аdvertisement> Ads { get; set; }
        public Person()
        {
            Ads = new List<Аdvertisement>();
        }
        
        public int? RoleId { get; set; }
        public Role Role { get; set; }
    }
}