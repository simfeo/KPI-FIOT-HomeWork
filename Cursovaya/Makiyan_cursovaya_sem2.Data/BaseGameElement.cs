using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Drawing;
using System.Text;

namespace Makiyan_cursovaya_sem2.Data
{
    public class BaseGameElement
    {
        [DataMember]
        public string Name { get; private set; }

        public Boolean IsStatic { get; private set; }

        public Point InitialPoint { get; private set; }

        public Guid LevelId { get; set; }

        public BaseGameElement(string name, Boolean stat, Point initPoint)
        {
            Name = name;
            IsStatic = stat;
            InitialPoint = initPoint;
        }

        public virtual Dictionary<Point, BaseGameElement> CollidesWith()
        {
            return new Dictionary<Point, BaseGameElement>();
        }
    }
}
