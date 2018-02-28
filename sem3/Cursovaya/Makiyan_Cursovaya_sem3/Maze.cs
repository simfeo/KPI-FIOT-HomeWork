using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MazeMain.Data;

namespace MazeMain
{
    public partial class Maze : Form
    {
        public Maze()
        {
            InitializeComponent();
            makeDraftGrid();
            genMazeNewWay();
            drawMaze();
        }

        int[][] gridStatistics;
        int[][] gridFinal;

        public static int NORTH = 1;
        public static int SOUTH = 2;
        public static int EAST = 4;
        public static int WEST = 8;

        private const bool HORIZONTAL = false;
        private const bool VERTICAL = true;
        private const int GRID_SIZE = 14;
        private static Random rand = new Random();
        MazeNode[,] nodes;
        Label user;


        private Label makeLabel(int x, int y)
        {
            if (x < 0 || x > 29 || y < 0 || y > 29)
                throw new Exception("out of bunds");

            Label ll = new Label();
            ll.Parent = panel1;
            ll.Location = new System.Drawing.Point(x * 20, y * 20);
            ll.Text = "";
            ll.AutoSize = false;
            ll.Size = new System.Drawing.Size(20, 20);
            ll.BackColor = Color.Black;

            return ll;
        }

        private void makeCopy()
        {
            makeLabel(0, 0);
            makeLabel(1, 1);
            makeLabel(29, 29);
        }

        private void drawMaze()
        {
            panel1.Controls.Clear();
            for (int i = 0; i < gridFinal.Length; ++i)
            {
                for (int j = 0; j < gridFinal[i].Length; ++j)
                {
                    if (gridFinal[i][j] == 1)
                    {
                        makeLabel(i, j);
                    }
                }
            }
            user = makeLabel(0, 1);
            user.BackColor = Color.Green;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            makeDraftGrid();
            genMazeNewWay();
            drawMaze();
        }



        private void genMazeNewWay()
        {
            /*
            // Figure out the drawing geometry.
            int wid = int.Parse(txtWidth.Text);
            int hgt = int.Parse(txtHeight.Text);

            CellWid = picMaze.ClientSize.Width / (wid + 2);
            CellHgt = picMaze.ClientSize.Height / (hgt + 2);
            if (CellWid > CellHgt) CellWid = CellHgt;
            else CellHgt = CellWid;
            Xmin = (picMaze.ClientSize.Width - wid * CellWid) / 2;
            Ymin = (picMaze.ClientSize.Height - hgt * CellHgt) / 2;
            */
            // Build the maze nodes.
            MazeNode[,] nodes = MakeNodes(14, 14);

            // Build the spanning tree.
            FindSpanningTree(nodes[0, 0]);

            DisplayMaze(nodes);

        }

        // Make the network of MazeNodes.
        private MazeNode[,] MakeNodes(int wid, int hgt)
        {
            // Make the nodes.
            MazeNode[,] nodes = new MazeNode[hgt, wid];

            for (int x = 0; x < hgt; ++x)
            {
                for (int y = 0; y < wid; y++)
                {
                    nodes[x, y] = new MazeNode();
                }
            }

            // Initialize the nodes' neighbors.
            for (int r = 0; r < hgt; r++)
            {
                for (int c = 0; c < wid; c++)
                {
                    if (r > 0)
                        nodes[r, c].Neighbors[MazeNode.North] = nodes[r - 1, c];
                    if (r < hgt - 1)
                        nodes[r, c].Neighbors[MazeNode.South] = nodes[r + 1, c];
                    if (c > 0)
                        nodes[r, c].Neighbors[MazeNode.West] = nodes[r, c - 1];
                    if (c < wid - 1)
                        nodes[r, c].Neighbors[MazeNode.East] = nodes[r, c + 1];
                }
            }

            // Return the nodes.
            return nodes;
        }

        // Build a spanning tree with the indicated root node.
        private void FindSpanningTree(MazeNode root)
        {
            Random rand = new Random();

            // Set the root node's predecessor so we know it's in the tree.
            root.Predecessor = root;

            // Make a list of candidate links.
            List<MazeLink> links = new List<MazeLink>();

            // Add the root's links to the links list.
            foreach (MazeNode neighbor in root.Neighbors)
            {
                if (neighbor != null)
                    links.Add(new MazeLink(root, neighbor));
            }

            // Add the other nodes to the tree.
            while (links.Count > 0)
            {
                // Pick a random link.
                int link_num = rand.Next(0, links.Count);
                MazeLink link = links[link_num];
                links.RemoveAt(link_num);

                // Add this link to the tree.
                MazeNode to_node = link.ToNode;
                link.ToNode.Predecessor = link.FromNode;

                // Remove any links from the list that point
                // to nodes that are already in the tree.
                // (That will be the newly added node.)
                for (int i = links.Count - 1; i >= 0; i--)
                {
                    if (links[i].ToNode.Predecessor != null)
                        links.RemoveAt(i);
                }

                // Add to_node's links to the links list.
                foreach (MazeNode neighbor in to_node.Neighbors)
                {
                    if ((neighbor != null) && (neighbor.Predecessor == null))
                        links.Add(new MazeLink(to_node, neighbor));
                }
            }
        }


        private void makeDraftGrid()
        {
            gridFinal = new int[GRID_SIZE * 2 + 1][];
            for (int i = 0; i < gridFinal.Length; ++i)
                gridFinal[i] = new int[GRID_SIZE * 2 + 1];

            for (int i = 0; i < gridFinal.Length; ++i)
            {
                for (int j = 0; j < gridFinal[i].Length; ++j)
                    gridFinal[i][j] = 0;
            }
        }

        private void DisplayMaze(MazeNode[,] nodes)
        {
            for (int r = 0; r < GRID_SIZE; r++)
            {
                for (int c = 0; c < GRID_SIZE; c++)
                {
                    DrawWalls(nodes[r, c], r * 2 + 1, c * 2 + 1);
                }
            }
            gridFinal[0][1] = 0;
            gridFinal[gridFinal.Length - 1][gridFinal[gridFinal.Length - 1].Length - 2] = 0;
        }

        private void DrawWalls(MazeNode node, int x, int y)
        {
            for (int side = 0; side < 4; side++)
            {
                if ((node.Neighbors[side] == null) ||
                    ((node.Neighbors[side].Predecessor != node) &&
                     (node.Neighbors[side] != node.Predecessor)))
                {
                    DrawWall(node, side, x, y);
                }
            }
        }

        // Draw one side of our bounding box.
        private void DrawWall(MazeNode node, int side, int x, int y)
        {
            switch (side)
            {
                case MazeNode.North:
                    gridFinal[x - 1][y] = 1;
                    gridFinal[x - 1][y - 1] = 1;
                    gridFinal[x - 1][y + 1] = 1;
                    break;
                case MazeNode.South:
                    gridFinal[x + 1][y] = 1;
                    gridFinal[x + 1][y - 1] = 1;
                    gridFinal[x + 1][y + 1] = 1;
                    break;
                case MazeNode.West:
                    gridFinal[x][y - 1] = 1;
                    gridFinal[x - 1][y - 1] = 1;
                    gridFinal[x + 1][y - 1] = 1;
                    break;
                case MazeNode.East:
                    gridFinal[x][y + 1] = 1;
                    gridFinal[x - 1][y + 1] = 1;
                    gridFinal[x + 1][y + 1] = 1;
                    break;
            }
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
            if (e.KeyCode == Keys.Up)
            {
                user.Location = new System.Drawing.Point(user.Location.X, user.Location.Y - 20);
            }
            else if (e.KeyCode == Keys.Down)
            {
                user.Location = new System.Drawing.Point(user.Location.X, user.Location.Y + 20);
            }
            else if (e.KeyCode == Keys.Left)
            {
                user.Location = new System.Drawing.Point(user.Location.X - 20, user.Location.Y);
            }
            else if (e.KeyCode == Keys.Right)
            {
                user.Location = new System.Drawing.Point(user.Location.X + 20, user.Location.Y);
            }
            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        private void Maze_Load(object sender, EventArgs e)
        {

        }

        private void Maze_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        private void button1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void button1_KeyUp(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        private void button2_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        private void button2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void button2_KeyUp(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        private void button1_Leave(object sender, EventArgs e)
        {
            panel1.Focus();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            makeDraftGrid();
            genMazeNewWay();
            drawMaze();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Leave(object sender, EventArgs e)
        {
            panel1.Focus();
        }

        private void panel1_Leave(object sender, EventArgs e)
        {
            panel1.Focus();
        }
    }
}
