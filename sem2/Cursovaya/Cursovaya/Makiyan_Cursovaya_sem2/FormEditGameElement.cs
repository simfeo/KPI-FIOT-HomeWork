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
    public partial class FormEditGameElement : Form
    {

        private Level level;
        private Point poinClass;
        private List<String> availableTypes = new List < String >{ "Brick", "User", "Enemy" };
        public FormEditGameElement(Level crLevel, Point p)
        {
            InitializeComponent();
            listBoxType.DataSource = availableTypes;
            level = crLevel;
            poinClass = p;
            if (poinClass != new Point(-1,-1))
            {
                BaseGameElement bs = level.GetGameElement(poinClass);

                Point dummyPoint = new Point(-1, -1);

                if (bs.Name == new Brick(dummyPoint).Name)
                {
                    listBoxType.SetSelected(0, true);
                }
                else if (bs.Name == new User(dummyPoint).Name)
                {
                    listBoxType.SetSelected(1, true);
                }
                else
                {
                    listBoxType.SetSelected(2, true);
                }

                numericUpDownX.Value = poinClass.X;
                numericUpDownY.Value = poinClass.Y;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Point p = new Point((int)numericUpDownX.Value, (int)numericUpDownY.Value);

            string type = (string)listBoxType.SelectedItem;

            BaseGameElement gameEl = null;

            
            if (type == "Brick")
            { gameEl = new Brick(p); }
            else if (type == "User")
            { gameEl = new User(p); }
            else if (type == "Enemy")
            { gameEl = new Enemy(p); }
            gameEl.LevelId = level.Id;
            level.SetGameElement(gameEl);
            Close();
        }

        private void listBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
