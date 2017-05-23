using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Drawing;


namespace Makiyan_cursovaya_sem2.Data
{
    [DataContract]
    public class Level
    {
        public static List<Level> levels = new List<Level>();

        [DataMember]
        public Guid Id { get; private set; }
        [DataMember]
        public string Name { get; set; }

        public Dictionary<Point, BaseGameElement> elements { get; private set; }

        public BaseGameElement GetGameElement(Point inPoint)
        {
            return elements[inPoint];
        }

        public void SetGameElement(Point inPoint, BaseGameElement value)
        {
            elements[inPoint] = value;
        }


        public Level(string name = null)
        {
            Id = Guid.NewGuid();
            Name = name;
            elements = new Dictionary<Point, BaseGameElement>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
