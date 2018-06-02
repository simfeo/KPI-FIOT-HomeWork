using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBook.Models
{
    public class PhoneDBWrapper
    {
        private PhoneCardDBContext db = new PhoneCardDBContext();


        public PhoneDBWrapper()
        { }

        ~PhoneDBWrapper()
        {
            db.Dispose();
        }

        public IEnumerable<PhoneCard> GetPhoneCards()
        {
            return db.Cards;
        }
    }
}