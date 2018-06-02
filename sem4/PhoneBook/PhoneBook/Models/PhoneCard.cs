using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace PhoneBook.Models
{
    public class PhoneCard
    {
        public int ID { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Display(Name = "First name")]
        public string LastName { get; set; }
        [Display(Name = "Birth date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class PhoneCardDBContext : DbContext
    {
        public DbSet<PhoneCard> Cards { get; set; }
    }
}