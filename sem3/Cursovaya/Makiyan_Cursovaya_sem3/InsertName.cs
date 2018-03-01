using MazeMain.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MazeMain
{
    public partial class InsertName : Form
    {
        private int m_time;
        public InsertName(int time)
        {
            InitializeComponent();
            label2.Text = ""+time;
            m_time = time;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            if (name.Length > 19)
            {
                MessageBox.Show("Too long name");
            }
            else
            {
                DB_logics db = new DB_logics();
                db.insertWinner(textBox1.Text, m_time);
                Close();
            }
        }
    }
}
