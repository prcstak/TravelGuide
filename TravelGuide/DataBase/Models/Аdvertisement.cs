using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace DataBase.Models
{
    public class Аdvertisement
    {
        public int Id { get; set; }
        public string Info { get; set; }
        public string Address { get; set; }
        public string Duration { get; set; }
        public int Rooms { get; set; }

        public int? PersonId { get; set; }
        public Person Person { get; set; }
    }
}