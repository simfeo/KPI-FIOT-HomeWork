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
        private List<String> availableTypes = new List < String >{ "Brick", "User", "Enemy" };
        public FormEditGameElement()
        {
            InitializeComponent();
            listBoxType.DataSource = availableTypes;
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

        }

        private void listBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
