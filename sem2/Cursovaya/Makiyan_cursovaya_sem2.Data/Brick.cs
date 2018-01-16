using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Makiyan_cursovaya_sem2.Data
{
    [DataContract(Name ="Brick")]
    [KnownType(typeof(BaseGameElement))]
    public class Brick : BaseGameElement
    {
        public Brick(Point initPoint) : base("brick", true, initPoint)
        {

        }
    }
}
