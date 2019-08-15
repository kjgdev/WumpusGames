using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Wumpus.Model
{
    public class Map
    {
        public BoxStatus[][] map = new BoxStatus[10][];
        public Player player = new Player();

        public Map()
        {
            map = new BoxStatus[10][];
            for (int i = 0; i < 10; i++)
            {
                map[i] = new BoxStatus[10];
            }
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    map[i][j] = new BoxStatus();
                }
            }
        }

        public void insertResourceMap(string path)
        {
            
            string[][] textMap = new string[10][];
            for (int i = 0; i < 10; i++)
            {
                textMap[i] = new string[10];
            }

            var rd = new StreamReader(@path);
            int row = 0;

            try
            {
                while (!rd.EndOfStream)
                {
                    var line = rd.ReadLine();
                    var value = line.Split('.');
                    for(int i = 0; i < 10; i++)
                    {
                        textMap[row][i] = value[i];
                    }
                    row++;
                }
            }
            catch { }

            for(int i = 0; i < 10; i++)
            {
                for(int j = 0; j < 10; j++)
                {
                    string str = textMap[j][i];
                    switch (str)
                    {
                        case "G":
                            map[i][j].Gold = true;
                            break;
                        case "P":
                            map[i][j].Pit = true;
                            break;
                        case "W":
                            map[i][j].Wumpus = true;
                            break;
                        case "S":
                            map[i][j].Stench = true;
                            break;
                        case "B":
                            map[i][j].Breeze = true;
                            break;
                        case "BS":
                            map[i][j].Stench = true;
                            map[i][j].Breeze = true;
                            break;
                        case "SG":
                            map[i][j].Stench = true;
                            map[i][j].Gold = true;
                            break;
                        case "BG":
                            map[i][j].Gold = true;
                            map[i][j].Breeze = true;
                            break;
                        case "BSG":
                            map[i][j].Gold = true;
                            map[i][j].Breeze = true;
                            map[i][j].Stench = true;
                            break;
                        case "A":
                            player.locationX = i;
                            player.locationY = j;
                            break;
                    }
                }
            }
        }
    }
}
