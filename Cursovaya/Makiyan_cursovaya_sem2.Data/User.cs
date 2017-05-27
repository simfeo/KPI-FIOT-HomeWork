using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Makiyan_cursovaya_sem2.Data
{
    [DataContract(Name = "User")]
    public class User : BaseMovingGameElement
    {
        public User(Point initPoint) : base("user", initPoint)
        {
        }
    }
}
