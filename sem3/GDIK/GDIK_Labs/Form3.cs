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
    public partial class Form3 : Form
    {
        public OpenFileDialog openFiles;
        public SaveFileDialog saveFiles;
        private string fileName = "Untitled";
        public bool m_DocumentChanged = false;


        public Form3()
        {
            InitializeComponent();
            this.CenterToParent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Crypt";

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFiles = new OpenFileDialog();
            openFiles.Title = "Encrypter";
            openFiles.Filter = "Text file(*.txt)|*.txt";
            if (openFiles.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Clear();
                TextReader reader = new StreamReader(openFiles.FileName, Encoding.Default);
                var text = reader.ReadToEnd();
                reader.Close();
                richTextBox1.Text = text;
                fileName = openFiles.FileName;
            }

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MenuFileSaveAs();
        }

        private void MenuFileSaveAs()
        {
            try
            {
                saveFiles = new SaveFileDialog();
                saveFiles.Title = "Save";
                saveFiles.Filter = "Text file(*.txt)|*.txt";

                if (saveFiles.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter streamwriter =
                      new System.IO.StreamWriter(saveFiles.FileName, false,
                       System.Text.Encoding.GetEncoding("utf-8"));
                    m_DocumentChanged = false;
                    streamwriter.Write(this.richTextBox1.Text);
                    streamwriter.Close();
                }
                MessageBox.Show("File saved successfuly", "Info", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (Exception ex) { MessageBox.Show("Error while files saving! " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            m_DocumentChanged = true;
        }

        private void Empty_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox2.Clear();
            textBox1.Clear();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void encrypt_Click_1(object sender, EventArgs e)
        {
            int ii = 0;
            string outtext = "";
            string text = Convert.ToString(richTextBox1.Text);
            char[] key = textBox1.Text.ToCharArray();
            char[] alf = { 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Є', 'Ж', 'З', 'И', 'І', 'Ї', 'Й', 'К', 'Л', 'М', 'Н', 'О',
                'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ю', 'Я', 'Ь', 'а', 'б', 'в', 'г', 'д', 'е', 'є', 'ж',
                'з', 'и', 'і', 'ї', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ю', 'я', 'ь' };

            try
            {
                for (int i = 0; i < text.Length; i++)
                {
                    for (int j = 0; j < alf.Length; j++)
                    {
                        if (text[i] == alf[j])
                        {
                            for (int jj = 0; jj < alf.Length; jj++)
                            {
                                if (key[ii] == alf[jj])
                                {
                                    int temp = (j + jj) % alf.Length;
                                    outtext += alf[temp].ToString();
                                    break;
                                }
                            }
                            ii++;

                            if (ii >= key.Length)
                            {
                                ii = 0;
                            }
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show(@"Whoops... something went definetly wrong");
            }
            richTextBox2.AppendText(outtext);
        }
        private void Decrypt_Click(object sender, EventArgs e)
        {
            int ii = 0;
            string outtext = "";
            string text = Convert.ToString(richTextBox1.Text);
            char[] key = textBox1.Text.ToCharArray();
            char[] alf = { 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Є', 'Ж', 'З', 'И', 'І', 'Ї', 'Й', 'К', 'Л', 'М', 'Н', 'О',
                'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ю', 'Я', 'Ь', 'а', 'б', 'в', 'г', 'д', 'е', 'є', 'ж',
                'з', 'и', 'і', 'ї', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ю', 'я', 'ь' };

            try
            {
                for (int i = 0; i < text.Length; i++)
                {
                    for (int j = 0; j < alf.Length; j++)
                    {

                        if (text[i] == alf[j])
                        {
                            for (int jj = 0; jj < alf.Length; jj++)
                            {
                                if (key[ii] == alf[jj])
                                {
                                    int temp = (j - jj);
                                    if (temp < 0) { temp = temp + 64; }
                                    outtext += alf[temp].ToString();
                                    break;
                                }
                            }
                            ii++;
                            if (ii >= key.Length) { ii = 0; }
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show(@"Hmm, something went wrong, please check input data.");
            }
            richTextBox2.AppendText(outtext);
        }

        private void menuStrip1_ItemClicked(object sender, EventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
