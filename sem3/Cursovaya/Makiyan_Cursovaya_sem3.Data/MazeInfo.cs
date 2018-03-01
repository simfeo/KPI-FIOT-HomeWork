using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MazeMain.Data
{
    [DataContract]
    class MazeInfo
    {
        [DataMember]
        public Boolean completed;
        [DataMember]
        public int[][] gridFinal;
        [DataMember]
        public int[] userPos;
        [DataMember]
        public TimeSpan timeSpent;
    }
}
