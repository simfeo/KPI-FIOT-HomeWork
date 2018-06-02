using System;
using System.Data.Entity;

namespace PhoneBook.Models
{
    public class PhoneCard
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class PhoneCardDBContext : DbContext
    {
        public DbSet<PhoneCard> Cards { get; set; }
    }
}