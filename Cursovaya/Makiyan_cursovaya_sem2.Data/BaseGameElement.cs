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

        public Dictionary<Point, BaseGameElement> CollidesWith()
        {
            if (IsStatic)
                return new Dictionary<Point, BaseGameElement>();

            List<BaseGameElement> el = new List<BaseGameElement>();
            Level lv = null;
            foreach (Level l in Level.levels)
            {
                if (l.Id == LevelId)
                {
                    lv = l;
                    break;
                }
            }

            if (lv == null)
                return new Dictionary<Point, BaseGameElement>();

            Dictionary<Point, BaseGameElement> res = new Dictionary<Point, BaseGameElement>();
            foreach ( KeyValuePair<Point, BaseGameElement> mm in lv.elements)
            {
                if (mm.Key != InitialPoint && mm.Value.IsStatic == false)
                {
                    res[mm.Key] = mm.Value;
                }
            }

            return res;
        }
    }
}
