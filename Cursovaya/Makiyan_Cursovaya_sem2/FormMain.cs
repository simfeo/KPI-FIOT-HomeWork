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
using System.Runtime.Serialization;
using System.Xml;
using System.IO;

namespace Makiyan_Cursovaya_sem2
{
    public partial class FormMain : Form
    {
        public static List<Level> levels = new List<Level>();
        public FormMain()
        {
            InitializeComponent();
            try
            {
                FormMain.levels = (List<Level>)Load("level.xml", typeof(List<Level>));
                RefreshLevelsList();
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new FormEditLevel().ShowDialog();
            RefreshLevelsList();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormMain.levels.Remove((Level)LevelsList.SelectedItem);
            RefreshLevelsList();
        }

        private void LevelsList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Level lv = null;
                string name = LevelsList.SelectedItem.ToString();

                foreach (Level l in FormMain.levels)
                {
                    if (l.Name == name)
                    {
                        lv = l;
                        break;
                    }
                }

                new FormEditLevel(lv).ShowDialog();
                RefreshLevelsList();
            }
            catch { }
        }


        
        private void Save(object obj, string filaName)
        {
            DataContractSerializer dcs = new DataContractSerializer(obj.GetType());
            XmlWriter xmlw = XmlWriter.Create(filaName);
            dcs.WriteObject(xmlw, obj);
            xmlw.Close();
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Save(FormMain.levels, "level.xml");
        }

        private object Load(string filaName, Type t)
        {
            if (File.Exists("level.xml"))
            {
                DataContractSerializer dcs = new DataContractSerializer(t);
                XmlReader xmlr = XmlReader.Create(filaName);
                object res = dcs.ReadObject(xmlr);
                xmlr.Close();
                return res;
            }
            return new List<Level>();
        }

    }
}
