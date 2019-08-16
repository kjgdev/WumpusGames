using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wumpus.AI;
using Wumpus.Model;

namespace Wumpus
{
    public partial class Form1 : Form
    {
        public Map mapData;
        Logic logic;
        int score;

        public Form1()
        {
            InitializeComponent();
            mapData = new Map();
            logic = new Logic();
            score = 0;
             cbSpeed.SelectedIndex = 0;
            cbGold.SelectedIndex = 0;
            cbW.SelectedIndex = 0;
            cbP.SelectedIndex = 0;
            mapData.randomMap(int.Parse(cbGold.SelectedItem.ToString()), int.Parse(cbW.SelectedItem.ToString()), int.Parse(cbP.SelectedItem.ToString()));
            drawMap();
           
        }

        public void drawMap()
        {
            string pathImage = "";
            int x = mapData.player.locationX;
            int y = mapData.player.locationY;
            mapData.map[x][y].Player = true;
            mapData.map[x][y].Visiable = true;            

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (mapData.map[i][j].Visiable == true)
                    {
                        foreach (Control pan in panel109.Controls)
                        {
                            if (pan.GetType() == typeof(Panel) && pan.TabIndex == (j + i * 10 + 9))
                            {
                                if (mapData.map[i][j].Player == true)
                                {
                                    pathImage = "\\Resource\\player.png";
                                }
                                else
                                {
                                    if (mapData.map[i][j].Breeze == true && mapData.map[i][j].Stench == true) pathImage = "\\Resource\\breeze+stench.png";
                                    else if (mapData.map[i][j].Breeze == true && mapData.map[i][j].Gold == true) pathImage = "\\Resource\\breeze+gold.png";
                                    else if (mapData.map[i][j].Stench == true && mapData.map[i][j].Gold == true) pathImage = "\\Resource\\stench+gold.png";
                                    else if (mapData.map[i][j].Stench == true && mapData.map[i][j].Gold == true && mapData.map[i][j].Breeze) pathImage = "\\Resource\\breeze+stench+gold.png";
                                    else if (mapData.map[i][j].Breeze == true) pathImage = "\\Resource\\breeze.png";
                                    else if (mapData.map[i][j].Stench == true) pathImage = "\\Resource\\stench.png";
                                    else if (mapData.map[i][j].Gold == true) pathImage = "\\Resource\\gold.png";
                                    else if (mapData.map[i][j].Pit == true) pathImage = "\\Resource\\pit.png";
                                    else if (mapData.map[i][j].Wumpus == true) pathImage = "\\Resource\\monster.png";
                                    else pathImage = "\\Resource\\white.png";
                                }
                                pan.BackColor = Color.White;
                                pan.BackgroundImageLayout = ImageLayout.Stretch;
                                pan.BackgroundImage = Image.FromFile(Application.StartupPath + pathImage);
                            }
                        }
                    }
                    else
                    {
                        foreach (Control pan in panel109.Controls)
                        {
                            if (pan.GetType() == typeof(Panel) && pan.TabIndex == (j + i * 10 + 9))
                            {
                                pan.BackColor = Color.Gray;
                                pan.BackgroundImage = null;
                                //pan.BackgroundImageLayout = ImageLayout.Stretch;
                                //pan.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Resource\\gray.png");
                            }
                        }
                    }
                }

            }
            mapData.map[x][y].Player = false;
            tbState.Text = "";
            if (mapData.map[x][y].Breeze) tbState.Text += "Breeze ";
            if (mapData.map[x][y].Stench) tbState.Text += "Stench ";
            if (mapData.map[x][y].Gold) tbState.Text += "Gold";
            if (mapData.map[x][y].Wumpus) tbState.Text += "Wumpus";
            if (mapData.map[x][y].Pit) tbState.Text += "Pit";

        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            if (logic.checkGold(mapData)) score += 100;
            tbScore.Text = score.ToString();
            drawMap();
            if (!logic.processGo(mapData) || logic.gameDie(mapData))
            {
                timer1.Enabled = false;
                DialogResult result = MessageBox.Show("End Game!", "!!", MessageBoxButtons.RetryCancel);
                if(result == DialogResult.Retry)
                {
                    new Form1();
                    Button1_Click(sender, e);
                }
                if(result == DialogResult.Cancel) { Close(); }
               
            }
        }
        private void BtnAuto_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            mapData = new Map();
            logic = new Logic();
            score = 0;
            mapData.randomMap(int.Parse(cbGold.SelectedItem.ToString()), int.Parse(cbW.SelectedItem.ToString()), int.Parse(cbP.SelectedItem.ToString()));
            drawMap();
        }

        private void ImportMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                mapData = new Map();
                logic = new Logic();
                score = 0;
                mapData.insertResourceMap(fileDialog.FileName);
                drawMap();
            }            
        }

        private void CbSpeed_SelectedIndexChanged(object sender, EventArgs e)
        {
            timer1.Interval = int.Parse(cbSpeed.SelectedItem.ToString());
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Project Wumpus Game")
        }

        private void HelpToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }
    }
}
