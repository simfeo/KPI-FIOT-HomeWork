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
        private Level currentLevel;
        public FormEditLevel(Level lv = null)
        {
            InitializeComponent();
            if (lv == null)
                currentLevel = new Level();
            else
                currentLevel = lv;
        }

        private void FormEditLevel_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (name.Text.Trim().Length == 0)
            {
                name.Text = "";
                MessageBox.Show("Level name shouldn't be empty");
            }
            else
            {
                currentLevel.Name = name.Text.Trim();
                Level.levels.Add(currentLevel);
                Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void buttonAddGameElement_Click(object sender, EventArgs e)
        {
            new FormEditGameElement(currentLevel, new Point(-1, -1)).ShowDialog();
            refreshGameElementsList();
        }

        private void buttonEditGameElement_Click(object sender, EventArgs e)
        {
            string pointAndName = listBoxGameObjects.SelectedItem.ToString();
            string pointName = pointAndName.Split(':')[0];
            foreach (char i in "{XY=}")
            {
                string ss = "" + i;
                pointName = pointName.Replace(ss, "");
            }
            string[] coords = pointName.Split(',');
            Point point = new Point(int.Parse(coords[0]), int.Parse(coords[1]));

            new FormEditGameElement(currentLevel, point).ShowDialog();
            refreshGameElementsList();
        }

        private void refreshGameElementsList()
        {
            listBoxGameObjects.DataSource = null;
            List<string> gameElementsList = new List<string>();
            foreach (KeyValuePair<Point, BaseGameElement> kv in currentLevel.elements)
            {
                gameElementsList.Add(kv.Key.ToString() + "::" + kv.Value.Name);
            }
            listBoxGameObjects.DataSource = gameElementsList;
        }
    }
}
