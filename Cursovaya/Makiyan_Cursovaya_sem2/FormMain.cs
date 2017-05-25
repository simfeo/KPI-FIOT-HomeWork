using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Makiyan_cursovaya_sem2.Data;

namespace Makiyan_Cursovaya_sem2
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new FormEditLevel().ShowDialog();
            RefreshLevelsList();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Level.levels.Remove((Level)LevelsList.SelectedItem);
            RefreshLevelsList();
        }

        private void LevelsList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Level lv = null;
                string name = LevelsList.SelectedItem.ToString();

                foreach (Level l in Level.levels)
                {
                    if (l.Name == name)
                    {
                        lv = l;
                        break;
                    }
                }

                new FormEditLevel(lv).ShowDialog();
                RefreshLevelsList();
            }
            catch { }
        }
    }
}
