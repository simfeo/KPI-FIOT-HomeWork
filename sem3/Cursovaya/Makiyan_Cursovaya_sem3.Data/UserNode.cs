using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MazeMain.Data
{
    class UserNode : FinishNode
    {
        public UserNode (Panel parent, int x, int y)
            : base(parent, x, y)
        {
            m_label.BackColor = System.Drawing.Color.Green;
        }

        public override void SetNewLocation (int x, int y)
        {
            m_x = x;
            m_y = y;
            m_label.Location = new System.Drawing.Point(x * 20, y * 20);
        }
    }
}
