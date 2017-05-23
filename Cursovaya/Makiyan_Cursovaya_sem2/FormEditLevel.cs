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
    public partial class FormEditLevel : Form
    {
        private Level currentLevel;
        public FormEditLevel(Level lv = null)
        {
            InitializeComponent();
            if (lv == null)
                currentLevel = new Level();
            else
                currentLevel = lv;
        }

        private void FormEditLevel_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            currentLevel.Name = name.Text;
            Level.levels.Add(currentLevel);
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void buttonAddGameElement_Click(object sender, EventArgs e)
        {
            new FormEditGameElement().ShowDialog();
        }
    }
}
