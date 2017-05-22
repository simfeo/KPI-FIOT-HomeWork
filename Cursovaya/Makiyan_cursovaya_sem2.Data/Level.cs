using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


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

        public Level(string name = null)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
