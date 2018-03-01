using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MazeMain.Data
{
    public class MazeLogic
    {
        private int[][] gridFinal;

        public static int NORTH = 1;
        public static int SOUTH = 2;
        public static int EAST = 4;
        public static int WEST = 8;

        private const bool HORIZONTAL = false;
        private const bool VERTICAL = true;
        private const int GRID_SIZE = 14;
        private Panel m_panel;
        private UserNode m_user;
        private BaseNode m_finish;
        private List<BaseNode> m_availableNodes;
        

        public MazeLogic(Panel panel)
        {
            m_panel = panel;
        }

        public void createMaze()
        {
            makeDraftGrid();
            genMazeNewWay();
            drawMaze();
        }

        public bool checkIsEnd()
        {
            return m_user.GetXY() == m_finish.GetXY();
        }


        public void makeMove(string direction)
        {
            Point point = m_user.GetXY();
            if (direction == "Up")
            {
                int newY = point.Y - 1;
                if (newY > 0 && gridFinal[point.X][newY] ==0)
                {
                    m_user.SetNewLocation(point.X, newY);
                }
            }
            else if (direction == "Down")
            {
                int newY = point.Y + 1;
                if (newY < GRID_SIZE*2 && gridFinal[point.X][newY] == 0)
                {
                    m_user.SetNewLocation(point.X, newY);
                }
            }
            else if (direction == "Left")
            {
                int newX = point.X - 1;
                if (newX >= 0 && gridFinal[newX][point.Y] == 0)
                {
                    m_user.SetNewLocation(newX, point.Y);
                }
            }
            else if (direction == "Right")
            {
                int newX = point.X + 1;
                if (newX <= GRID_SIZE*2 && gridFinal[newX][point.Y] == 0)
                {
                    m_user.SetNewLocation(newX, point.Y);
                }
            }
            else
            {
                throw new Exception("wrong direction!");
            }
        }
        private void drawMaze()
        {
            m_panel.Controls.Clear();
            m_availableNodes = new List<BaseNode>();
            for (int i = 0; i < gridFinal.Length; ++i)
            {
                for (int j = 0; j < gridFinal[i].Length; ++j)
                {
                    if (gridFinal[i][j] == 1)
                    {
                        m_availableNodes.Add( new BaseNode(m_panel, i, j));
                    }
                }
            }
            m_user = new UserNode(m_panel, 0, 1);
            m_finish = new FinishNode(m_panel, GRID_SIZE * 2, GRID_SIZE*2 - 1);
        }

        private void genMazeNewWay()
        {
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
    }
}
