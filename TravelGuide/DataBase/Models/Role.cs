using System.Collections.Generic;

namespace DataBase.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Person> Users { get; set; }
        public Role()
        {
            Users = new List<Person>();
        }
    }
}