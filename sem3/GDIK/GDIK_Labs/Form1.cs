using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GDIK_Labs
{
    public partial class MainForm: Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.CenterToParent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            new Form2().ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            new Form3().ShowDialog();
            this.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Hide();
            this.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Hide();
            new Form5().ShowDialog();
            this.Show();
        }

        private void HomeWork_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
