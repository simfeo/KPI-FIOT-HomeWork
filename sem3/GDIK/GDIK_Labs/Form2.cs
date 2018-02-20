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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            this.CenterToParent();
        }

        OpenFileDialog openFileDialog = new OpenFileDialog();
        List<TextBox> inputQuestionsList = new List<TextBox>();
        List<TextBox> inputAnswersList = new List<TextBox>();

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            inputQuestionsList.Add(textBox1);
            inputQuestionsList.Add(textBox3);
            inputQuestionsList.Add(textBox5);
            inputQuestionsList.Add(textBox7);
            inputQuestionsList.Add(textBox9);
            if (String.IsNullOrEmpty(textBox1.Text ?? textBox3.Text ?? textBox5.Text ?? textBox7.Text ?? textBox9.Text))
            {
                MessageBox.Show("All questions should be filled!");
            }
            else
            {
                FileStream file1 = new FileStream("Questions1.txt", FileMode.Create, FileAccess.ReadWrite);
                StreamWriter wrQuestion = new StreamWriter(file1, Encoding.UTF8);
                var a = inputQuestionsList.Last();
                foreach (var str in inputQuestionsList)
                {
                    if (str.Equals(a))
                    {
                        wrQuestion.Write(str.Text);
                    }
                    else
                    {
                        wrQuestion.WriteLine(str.Text);
                    }
                }
                wrQuestion.Close();
                MessageBox.Show("Question are saved to \"Questions1.txt\".");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox2.Text ?? textBox4.Text ?? textBox6.Text ?? textBox8.Text ?? textBox10.Text))
            {
                MessageBox.Show("All answers should be filled!");
            }
            else
            {
                inputAnswersList.Add(textBox2);
                inputAnswersList.Add(textBox4);
                inputAnswersList.Add(textBox6);
                inputAnswersList.Add(textBox8);
                inputAnswersList.Add(textBox10);
                FileStream file2 = new FileStream("Answers1.txt", FileMode.Create, FileAccess.ReadWrite);
                StreamWriter wrAnswer = new StreamWriter(file2, Encoding.UTF8);
                var a = inputAnswersList.Last();
                foreach (var str in inputAnswersList)
                {
                    if (str.Equals(a))
                    {
                        wrAnswer.Write(str.Text);
                    }
                    else
                    {
                        wrAnswer.WriteLine(str.Text);
                    }
                }
                wrAnswer.Close();
                MessageBox.Show("Answer are saved to \"Answers1.txt\".");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "TXT|*.txt";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamReader reader = new StreamReader(openFileDialog.FileName, Encoding.GetEncoding(1251));
                string strQuestion = reader.ReadToEnd();
                string[] questions = strQuestion.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                if (questions.Length > 5)
                {
                    MessageBox.Show("Should be more then 5 questions!");
                }
                else
                {
                    inputQuestionsList.Add(textBox1);
                    inputQuestionsList.Add(textBox3);
                    inputQuestionsList.Add(textBox5);
                    inputQuestionsList.Add(textBox7);
                    inputQuestionsList.Add(textBox9);
                    for (int i = 0; i < questions.Length; i++)
                    {
                        inputQuestionsList[i].Text = questions[i];
                    }
                    reader.Close();
                    MessageBox.Show("Questions are loaded");
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "TXT|*.txt";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamReader reader = new StreamReader(openFileDialog.FileName, Encoding.GetEncoding(1251));
                string strAnswer = reader.ReadToEnd();
                string[] answers = strAnswer.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                if (answers.Length > 5)
                {
                    MessageBox.Show("Should be more then 5 answers!");
                }
                else
                {
                    inputAnswersList.Add(textBox2);
                    inputAnswersList.Add(textBox4);
                    inputAnswersList.Add(textBox6);
                    inputAnswersList.Add(textBox8);
                    inputAnswersList.Add(textBox10);
                    for (int i = 0; i < answers.Length; i++)
                    {
                        inputAnswersList[i].Text = answers[i];
                    }
                    reader.Close();
                    MessageBox.Show("Answers are loaded");
                }
            }
        }
    }
}
