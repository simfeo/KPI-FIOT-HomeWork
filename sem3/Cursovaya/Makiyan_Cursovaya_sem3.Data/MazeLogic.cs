using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.IO;
using System.Xml;

namespace MazeMain.Data
{
    public class MazeLogic
    {
        public static int NORTH = 1;
        public static int SOUTH = 2;
        public static int EAST = 4;
        public static int WEST = 8;

        private const bool HORIZONTAL = false;
        private const bool VERTICAL = true;
        private const int GRID_SIZE = 14;
        private Boolean loadSucessFully;
        private Panel m_panel;
        private UserNode m_user;
        private BaseNode m_finish;
        private List<BaseNode> m_availableNodes;
        private MazeInfo mazeInfo;
        private DateTime startTime;


        public MazeLogic(Panel panel)
        {
            m_panel = panel;
            try
            {
                //FormMain.levels = (List<Level>)Load("level.xml", typeof(List<Level>));
                mazeInfo = (MazeInfo)Load("maze.xml", typeof(MazeInfo));
            }
            catch
            {
            }

            if (mazeInfo.gridFinal != null)
            {
                loadSucessFully = true;
            }
            else
            {
                loadSucessFully = false;
            }

        }

        public Boolean IsLoadSuccessfully()
        {
            return loadSucessFully;
        }

        private object Load(string fileName, Type t)
        {
            if (File.Exists(fileName))
            {
                DataContractSerializer dcs = new DataContractSerializer(t);
                XmlReader xmlr = XmlReader.Create(fileName);
                object res = dcs.ReadObject(xmlr);
                xmlr.Close();
                return res;
            }
            return new MazeInfo();
        }

        public void Save()
        {
            mazeInfo.timeSpent += DateTime.Now - startTime;
            DataContractSerializer dcs = new DataContractSerializer(mazeInfo.GetType());
            XmlWriter xmlw = XmlWriter.Create("maze.xml");
            dcs.WriteObject(xmlw, mazeInfo);
            xmlw.Close();
        }

        public void loadMaze()
        {
            if (mazeInfo.completed)
            {
                createMaze();
                return;
            }
            startTime = DateTime.Now;
            m_panel.Controls.Clear();
            m_availableNodes = new List<BaseNode>();
            for (int i = 0; i < mazeInfo.gridFinal.Length; ++i)
            {
                for (int j = 0; j < mazeInfo.gridFinal[i].Length; ++j)
                {
                    if (mazeInfo.gridFinal[i][j] == 1)
                    {
                        m_availableNodes.Add(new BaseNode(m_panel, i, j));
                    }
                }
            }
            m_user = new UserNode(m_panel, mazeInfo.userPos[0], mazeInfo.userPos[1]);
            m_finish = new FinishNode(m_panel, GRID_SIZE * 2, GRID_SIZE * 2 - 1);
        }

        public void createMaze()
        {
            mazeInfo.timeSpent = DateTime.Now - DateTime.Now;
            startTime = DateTime.Now;
            mazeInfo.completed = false;
            makeDraftGrid();
            genMazeNewWay();
            drawMaze();
        }

        public bool checkIsEnd()
        {
            mazeInfo.completed = m_user.GetXY() == m_finish.GetXY();
            if (mazeInfo.completed)
            {
                mazeInfo.timeSpent += DateTime.Now - startTime;
            }
            return mazeInfo.completed;
        }

        public int GetSpentTime()
        {
            return (int)mazeInfo.timeSpent.TotalSeconds;
        }


        public Boolean makeMove(string direction)
        {
            Point point = m_user.GetXY();
            if (direction == "Up")
            {
                int newY = point.Y - 1;
                if (newY > 0 && mazeInfo.gridFinal[point.X][newY] ==0)
                {
                    m_user.SetNewLocation(point.X, newY);
                }
            }
            else if (direction == "Down")
            {
                int newY = point.Y + 1;
                if (newY < GRID_SIZE*2 && mazeInfo.gridFinal[point.X][newY] == 0)
                {
                    m_user.SetNewLocation(point.X, newY);
                }
            }
            else if (direction == "Left")
            {
                int newX = point.X - 1;
                if (newX >= 0 && mazeInfo.gridFinal[newX][point.Y] == 0)
                {
                    m_user.SetNewLocation(newX, point.Y);
                }
            }
            else if (direction == "Right")
            {
                int newX = point.X + 1;
                if (newX <= GRID_SIZE*2 && mazeInfo.gridFinal[newX][point.Y] == 0)
                {
                    m_user.SetNewLocation(newX, point.Y);
                }
            }
            else
            {
                throw new Exception("wrong direction!");
            }
            mazeInfo.userPos[0] = m_user.GetXY().X;
            mazeInfo.userPos[1] = m_user.GetXY().Y;
            return !(m_user.GetXY() == point);
        }
        private void drawMaze()
        {
            m_panel.Controls.Clear();
            m_availableNodes = new List<BaseNode>();
            for (int i = 0; i < mazeInfo.gridFinal.Length; ++i)
            {
                for (int j = 0; j < mazeInfo.gridFinal[i].Length; ++j)
                {
                    if (mazeInfo.gridFinal[i][j] == 1)
                    {
                        m_availableNodes.Add( new BaseNode(m_panel, i, j));
                    }
                }
            }
            m_user = new UserNode(m_panel, 0, 1);
            mazeInfo.userPos[0] = 0;
            mazeInfo.userPos[1] = 1;
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
            mazeInfo.gridFinal = new int[GRID_SIZE * 2 + 1][];
            mazeInfo.userPos = new int[2];
            for (int i = 0; i < mazeInfo.gridFinal.Length; ++i)
                mazeInfo.gridFinal[i] = new int[GRID_SIZE * 2 + 1];

            for (int i = 0; i < mazeInfo.gridFinal.Length; ++i)
            {
                for (int j = 0; j < mazeInfo.gridFinal[i].Length; ++j)
                    mazeInfo.gridFinal[i][j] = 0;
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
            mazeInfo.gridFinal[0][1] = 0;
            mazeInfo.gridFinal[mazeInfo.gridFinal.Length - 1][mazeInfo.gridFinal[mazeInfo.gridFinal.Length - 1].Length - 2] = 0;
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
                    mazeInfo.gridFinal[x - 1][y] = 1;
                    mazeInfo.gridFinal[x - 1][y - 1] = 1;
                    mazeInfo.gridFinal[x - 1][y + 1] = 1;
                    break;
                case MazeNode.South:
                    mazeInfo.gridFinal[x + 1][y] = 1;
                    mazeInfo.gridFinal[x + 1][y - 1] = 1;
                    mazeInfo.gridFinal[x + 1][y + 1] = 1;
                    break;
                case MazeNode.West:
                    mazeInfo.gridFinal[x][y - 1] = 1;
                    mazeInfo.gridFinal[x - 1][y - 1] = 1;
                    mazeInfo.gridFinal[x + 1][y - 1] = 1;
                    break;
                case MazeNode.East:
                    mazeInfo.gridFinal[x][y + 1] = 1;
                    mazeInfo.gridFinal[x - 1][y + 1] = 1;
                    mazeInfo.gridFinal[x + 1][y + 1] = 1;
                    break;
            }
        }
    }
}
