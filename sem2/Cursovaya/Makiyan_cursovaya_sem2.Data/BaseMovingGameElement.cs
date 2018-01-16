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

        public override List<BaseGameElement> CollidesWith()
        {
            List<BaseGameElement> el = new List<BaseGameElement>();
            Level lv = Level.tempLevel;

            List<BaseGameElement> res = new List< BaseGameElement>();
            foreach (BaseGameElement mm in lv.elements)
            {
                if (mm.InitialPoint != InitialPoint)
                {
                    res.Add(mm);
                }
            }

            return res;
        }
    }
}
