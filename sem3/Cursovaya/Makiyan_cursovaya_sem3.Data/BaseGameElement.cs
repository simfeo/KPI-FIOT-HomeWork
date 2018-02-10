using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Drawing;
using System.Text;

namespace MarketInfo.Data
{
    [DataContract]
    [KnownType(typeof(Brick))]
    [KnownType(typeof(BaseMovingGameElement))]
    [KnownType(typeof(Enemy))]
    [KnownType(typeof(User))]
    public class BaseGameElement
    {
        [DataMember]
        public string Name { get; private set; }

        [DataMember]
        public Boolean IsStatic { get; private set; }

        [DataMember]
        public Point InitialPoint { get; private set; }

        [DataMember]
        public Guid LevelId { get; set; }

        public BaseGameElement(string name, Boolean stat, Point initPoint)
        {
            Name = name;
            IsStatic = stat;
            InitialPoint = initPoint;
        }

        public virtual List<BaseGameElement> CollidesWith()
        {
            return new List<BaseGameElement>();
        }
    }
}
