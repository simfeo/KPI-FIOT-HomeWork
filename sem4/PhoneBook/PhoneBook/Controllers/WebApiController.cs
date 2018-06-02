using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PhoneBook.Controllers
{
    public class WebApiController : ApiController
    {
        PhoneCard[] phoneCards = new PhoneCard[]
            {
                new PhoneCard {ID = 1, FirstName = "Dummy", LastName = "Dummy2", BirthDate = new DateTime(2008, 3, 9, 16, 5, 7, 123), Address = "Somwhere beyound the see", PhoneNumber="+123456789" },
                new PhoneCard {ID = 2, FirstName = "Duck", LastName = "McLaren", BirthDate = new DateTime(1989, 3, 9, 16, 5, 7, 123), Address = "Meme land, 13, fl. 123", PhoneNumber="+5454654" },
                new PhoneCard {ID = 3, FirstName = "Donald", LastName = "McDonald", BirthDate = new DateTime(2000, 3, 9, 16, 5, 7, 123), Address = "Hell, second ring.", PhoneNumber="+7878787" }
            };

        private IEnumerable<PhoneCard> GetPhoneCards()
        {
            PhoneDBWrapper dBWrapper = new PhoneDBWrapper();
            return dBWrapper.GetPhoneCards();
        }



        public IEnumerable<PhoneCard> GetAllProducts()
        {
            return GetPhoneCards();
        }

        public IHttpActionResult GetCard(int id)
        {
            var product = GetPhoneCards().FirstOrDefault((p) => p.ID == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}


