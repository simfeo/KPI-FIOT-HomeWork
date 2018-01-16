using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Makiyan_Cursovaya_sem2
{
    public partial class FormSplashScreen : Form
    {
        public FormSplashScreen()
        {
            InitializeComponent();
            new Thread(() =>
            {
                Thread.Sleep(3000);
                this.Invoke(new MethodInvoker(delegate { Close(); }));
            }).Start();
        }
    }
}
