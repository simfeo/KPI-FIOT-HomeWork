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
            {
                currentLevel = lv;
                name.Text = currentLevel.Name;
            }
            Level.tempLevel = new Level( currentLevel);
            refreshGameElementsList();
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
                string sName = name.Text.Trim();
                bool shouldAddLevel = true;
                bool shouldClose = true;
                foreach (Level l in FormMain.levels)
                {
                    if (sName == l.Name)
                    {
                        shouldAddLevel = false;
                        if (currentLevel.Id == l.Id)
                        {
                            currentLevel = new Level(Level.tempLevel);
                            int index = FormMain.levels.IndexOf(l);
                            FormMain.levels[index] = currentLevel;
                        }
                        else
                        {
                            shouldClose = false;
                            MessageBox.Show("Another level with same name already exists");
                        }
                        break;
                    }
                    else if (currentLevel.Id == l.Id)
                    {
                        shouldAddLevel = false;
                        Level.tempLevel.Name = sName;
                        currentLevel = new Level(Level.tempLevel);
                        int index = FormMain.levels.IndexOf(l);
                        FormMain.levels[index] = currentLevel;
                        break;
                    }
                }
                if (shouldClose)
                {
                    currentLevel.Name = sName;
                }
                if (shouldAddLevel)
                {
                    Level.tempLevel.Name = sName;
                    currentLevel = new Level(Level.tempLevel);
                    FormMain.levels.Add(currentLevel);
                }
                if (shouldClose)
                {
                    Close();
                }
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
            try
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

                Level.tempLevel.RemoveEelement(point);
                refreshGameElementsList();
            }
            catch
            { }
        }

        private void buttonAddGameElement_Click(object sender, EventArgs e)
        {
            new FormEditGameElement(Level.tempLevel, new Point(-1, -1)).ShowDialog();
            refreshGameElementsList();
        }

        private void buttonEditGameElement_Click(object sender, EventArgs e)
        {
            try
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

                new FormEditGameElement(Level.tempLevel, point).ShowDialog();
                refreshGameElementsList();
            }
            catch
            { }
        }

        private void refreshGameElementsList()
        {
            listBoxGameObjects.DataSource = null;
            List<string> gameElementsList = new List<string>();
            foreach ( BaseGameElement el in Level.tempLevel.elements)
            {
                gameElementsList.Add(el.InitialPoint.ToString() + "::" + el.Name);
            }
            listBoxGameObjects.DataSource = gameElementsList;
        }

        private void buttonStat_Click(object sender, EventArgs e)
        {
            try
            {
                string pointAndName = listBoxGameObjects.SelectedItem.ToString();
                if (pointAndName.Trim().Length == 0)
                {
                    return;
                }

                string pointName = pointAndName.Split(':')[0];
                foreach (char i in "{XY=}")
                {
                    string ss = "" + i;
                    pointName = pointName.Replace(ss, "");
                }
                string[] coords = pointName.Split(',');
                Point point = new Point(int.Parse(coords[0]), int.Parse(coords[1]));

                new FormElementsStat(Level.tempLevel, point).ShowDialog();
            }
            catch { }
        }

        private void name_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
