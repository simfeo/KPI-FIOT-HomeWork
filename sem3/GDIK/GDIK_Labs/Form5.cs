using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GDIK_Labs
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            this.CenterToParent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Text files|*.txt";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(dialog.FileName, Encoding.Default);
                string s = sr.ReadLine();
                var x = from c in s
                        group c by c into g
                        let count = g.Count()
                        orderby g.Key ascending
                        select new
                        {
                            Value = g.Key,
                            Count = count,
                        };

                foreach (var count in x)
                {
                    string a = (String.Format("For symbol '{0}' there are {1} occurences", count.Value, count.Count));
                    richTextBox1.Text += a + "\r";
                }
                string b = (String.Format("Total symbols count: {0}", s.Length));
                richTextBox1.Text += b + "\r";
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (richTextBox1 != null && richTextBox1.Text != "")
            {
                StreamWriter streamwriter = new StreamWriter(@"Result.txt", false, Encoding.GetEncoding("utf-8"));

                streamwriter.WriteLine(this.richTextBox1.Text);
                streamwriter.Close();
                MessageBox.Show("Saved to file \"Result.txt\"");
            }
            else
            {
                MessageBox.Show("Nothing to save (((");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string curFile = @"Result.txt";
            if (File.Exists(curFile) == true)
            {
                File.Delete("Result.txt");
                MessageBox.Show("File deleted successfully");
            }
            else
            {
                MessageBox.Show("Nothing to delete (((( ");
            }
            
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
           
            MessageBox.Show("This dialog calculates how many times each symbol occurs in file,\n and provides statistics per symbol");
        }
    }
}

