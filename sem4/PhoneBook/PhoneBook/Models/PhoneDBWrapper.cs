using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            Dispose();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public IEnumerable<PhoneCard> GetPhoneCards()
        {
            return db.Cards;
        }

        public PhoneCard Find(int? id)
        {
            return db.Cards.Find(id);
        }

        public void Add(PhoneCard card)
        {
            db.Cards.Add(card);
        }

        public void Remove(PhoneCard card)
        {
            db.Cards.Remove(card);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void ChangeState(PhoneCard card, EntityState state)
        {
            db.Entry(card).State = state;
        }
    }
}