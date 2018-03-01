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
    public partial class Statistics : Form
    {
        public Statistics()
        {
            InitializeComponent();
            DB_logics db = new DB_logics();
            string res = db.ReadTop5Winners();
            label2.Text = res;
        }
    }
}
