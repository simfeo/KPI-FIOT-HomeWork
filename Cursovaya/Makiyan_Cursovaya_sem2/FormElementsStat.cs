using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Makiyan_cursovaya_sem2.Data;

namespace Makiyan_Cursovaya_sem2
{
    public partial class FormElementsStat : Form
    {
        private Level currentLevel;
        private Point point;
        

        public FormElementsStat(Level currentLevel, Point point)
        {
            InitializeComponent();
            this.currentLevel = currentLevel;
            this.point = point;
            BaseGameElement bs = currentLevel.GetGameElement(point);

            name.Text = bs.Name;

            labelXCoord.Text = this.point.X.ToString();
            labelYCoord.Text = this.point.Y.ToString();

            listBoxCollide.DataSource = null;
            List<string> gameElementsList = new List<string>();
            foreach ( BaseGameElement el in bs.CollidesWith())
            {
                gameElementsList.Add(el.InitialPoint.ToString() + "::" + el.Name);
            }
            listBoxCollide.DataSource = gameElementsList;
        }

        private void listBoxCollide_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
