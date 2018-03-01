using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MazeMain.Data
{
    public class FinishNode: BaseNode
    {
        public FinishNode(Panel parent, int x, int y)
            : base (parent, x, y)
        {
            m_label.BackColor = System.Drawing.Color.Blue;
            m_x = x;
            m_y = y;
        }

    }
}
