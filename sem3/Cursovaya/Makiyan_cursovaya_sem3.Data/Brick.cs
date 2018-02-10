using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MarketInfo.Data
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
