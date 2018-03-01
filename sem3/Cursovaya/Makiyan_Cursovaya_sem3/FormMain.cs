using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Xml;
using System.IO;
using System.Threading;


namespace MazeMain
{
    public partial class FormMain : Form
    {
        
        public FormMain()
        {
            FormSplashScreen sp = new FormSplashScreen();
            sp.ShowDialog();

            InitializeComponent();
            
            BringToFront();
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Maze mz = new Maze();
            Hide();
            mz.ShowDialog();
            Show();
        }
    }
}
