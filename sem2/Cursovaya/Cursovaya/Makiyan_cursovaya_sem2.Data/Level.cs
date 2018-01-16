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
        public static Level tempLevel = new Level();

        [DataMember]
        public Guid Id { get; private set; }
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public List<BaseGameElement> elements { get; private set; }

        public BaseGameElement GetGameElement(Point inPoint)
        {
            foreach (BaseGameElement el in elements)
            {
                if (el.InitialPoint == inPoint)
                    return el;
            }
            return null;
        }

        public void SetGameElement(BaseGameElement gameEl)
        {
            BaseGameElement e = null;
            foreach (BaseGameElement el in elements)
            {
                if (el.InitialPoint == gameEl.InitialPoint)
                {
                    e = el;
                    break;
                }
            }
            if (e != null)
            {
                int index = elements.IndexOf(e);
                elements[index] = gameEl;
            }
            else
            {
                elements.Add(gameEl);
            }
        }

        public void RemoveEelement(Point p)
        {
            BaseGameElement e = null; 
            foreach (BaseGameElement el in elements)
            {
                if (el.InitialPoint == p)
                {
                    e = el;
                    break;
                }
            }
            if (e != null)
            {
                elements.Remove(e);
            }
        }

        public Level(string name = null)
        {
            Id = Guid.NewGuid();
            Name = name;
            elements = new List<BaseGameElement>();
        }

        public Level(Level orig)
        {
            Id = orig.Id;
            Name = orig.Name;
            elements = new List<BaseGameElement>();
            foreach ( BaseGameElement el in orig.elements)
            {
                elements.Add(el);
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
