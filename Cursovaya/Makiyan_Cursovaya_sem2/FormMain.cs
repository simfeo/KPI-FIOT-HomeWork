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
    }
}
