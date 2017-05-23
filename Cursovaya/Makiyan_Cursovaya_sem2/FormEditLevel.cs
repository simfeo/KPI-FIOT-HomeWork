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
        public FormEditLevel()
        {
            InitializeComponent();
        }

        private void FormEditLevel_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Level nl = new Level();
            nl.Name = name.Text;
            Level.levels.Add(nl);
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
