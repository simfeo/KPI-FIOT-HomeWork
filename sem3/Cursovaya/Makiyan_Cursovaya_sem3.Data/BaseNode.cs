using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace MazeMain.Data
{
    public class BaseNode
    {
        protected Panel m_parent;
        protected Label m_label;
        protected int m_x, m_y;

        public BaseNode(Panel parent, int x, int y)
        {
            m_parent = parent;
            if (x < 0 || x > 29 || y < 0 || y > 29)
                throw new Exception("out of bunds");

            m_label = new Label();
            m_label.Parent = m_parent;
            m_label.Location = new System.Drawing.Point(x * 20, y * 20);
            m_label.Text = "";
            m_label.AutoSize = false;
            m_label.Size = new System.Drawing.Size(20, 20);
            m_label.BackColor = Color.Black;

            m_x = x;
            m_y = y;
        }

        public virtual Label GetLabel ()
        {
            return m_label;
        }


        public Point GetXY()
        {
            return new Point(m_x, m_y);
        }

        public virtual void SetNewLocation(int x, int y)
        {
            throw new Exception("This class has constant location");
        }
    }
}
