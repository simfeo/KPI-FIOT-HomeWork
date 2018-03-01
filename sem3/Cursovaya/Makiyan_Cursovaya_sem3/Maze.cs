using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using MazeMain.Data;

namespace MazeMain
{
    public partial class Maze : Form
    {
        private MazeLogic m_logics;
        public Maze()
        {
            InitializeComponent();
            m_logics = new MazeLogic(panel1);
            if (m_logics.IsLoadSuccessfully())
            {
                m_logics.loadMaze();
            }
            else
            { 
                m_logics.createMaze();
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            m_logics.createMaze();
        }

        
        private void Maze_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        private void Maze_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void Maze_KeyUp(object sender, KeyEventArgs e)
        {
            Boolean result = false;
            if (e.KeyCode == Keys.Up)
            {
                result = m_logics.makeMove("Up");
            }
            else if (e.KeyCode == Keys.Down)
            {
                result = m_logics.makeMove("Down");
            }
            else if (e.KeyCode == Keys.Left)
            {
                result = m_logics.makeMove("Left");
            }
            else if (e.KeyCode == Keys.Right)
            {
                result = m_logics.makeMove("Right");
            }
            
            e.Handled = true;
            e.SuppressKeyPress = true;
            if (m_logics.checkIsEnd())
            {
                System.IO.Stream str = MazeMain.Properties.Resources.end;
                SoundPlayer snd = new SoundPlayer(str);
                snd.Play();

                DialogResult dialog = MessageBox.Show("Congrats! You've complited maze!" +
                    "\n Press Ok, to add exit game" +
                    "\n or Canctl to play new one. ", "CONGRATS!", MessageBoxButtons.OKCancel);

                if (dialog == DialogResult.OK)
                {
                    InsertName ins = new InsertName(m_logics.GetSpentTime());
                    ins.ShowDialog();
                    this.Close();
                }
                else if (dialog == DialogResult.Cancel)
                {
                    m_logics.createMaze();
                }
            }
            else if (result)
            {
                System.IO.Stream str = MazeMain.Properties.Resources.move;
                SoundPlayer snd = new SoundPlayer(str);
                snd.Play();
            }
        }

        private void Maze_Load(object sender, EventArgs e)
        {

        }

        private void Maze_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }
        
        private void button1_Leave(object sender, EventArgs e)
        {
            panel1.Focus();
        }

        private void button2_Leave(object sender, EventArgs e)
        {
            panel1.Focus();
        }

        private void panel1_Leave(object sender, EventArgs e)
        {
            panel1.Focus();
        }

        private void Maze_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_logics.Save();
        }
    }
}
