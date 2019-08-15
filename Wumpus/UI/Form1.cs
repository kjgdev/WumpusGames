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
        public Map mapData = new Map();
        Logic logic = new Logic();
        int score = 0;
        Player posStart;

        public Form1()
        {
            InitializeComponent();
            getMap();
            drawMap();
            posStart = mapData.player;
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            mapData.player = logic.processGo(mapData);
            if (logic.checkGold(mapData)) score += 100;
            tbScore.Text = score.ToString();
            drawMap();
            if (posStart == mapData.player) btnPlay.Visible = false;
        }

        public void getMap()
        {
            mapData.insertResourceMap(Application.StartupPath + "\\map.txt");
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
                                //pan.BackColor = Color.White;
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
                                pan.BackgroundImageLayout = ImageLayout.Stretch;
                                pan.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Resource\\gray.png");
                            }
                        }
                    }
                }

            }
            mapData.map[x][y].Player = false;
        }

        private void BtnAuto_Click(object sender, EventArgs e)
        {
            do
            {
                BtnPlay_Click(sender, e);
                Thread.Sleep(300);
            } while (posStart != mapData.player);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            mapData = new Map();
            logic = new Logic();
            score = 0;
            mapData.randomMap();
            drawMap();
            posStart = mapData.player;
        }
    }
}
