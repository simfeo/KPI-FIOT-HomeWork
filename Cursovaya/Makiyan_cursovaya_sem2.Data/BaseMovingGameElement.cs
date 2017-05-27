using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;

namespace Makiyan_cursovaya_sem2.Data
{
    [DataContract]
    public class BaseMovingGameElement : BaseGameElement
    {
        public BaseMovingGameElement(string name, Point initPoint) : base(name, false, initPoint)
        {
        }

        public override Dictionary<Point, BaseGameElement> CollidesWith()
        {
            List<BaseGameElement> el = new List<BaseGameElement>();
            Level lv = Level.tempLevel;
            
            Dictionary<Point, BaseGameElement> res = new Dictionary<Point, BaseGameElement>();
            foreach (KeyValuePair<Point, BaseGameElement> mm in lv.elements)
            {
                if (mm.Key != InitialPoint)
                {
                    res[mm.Key] = mm.Value;
                }
            }

            return res;
        }
    }
}
