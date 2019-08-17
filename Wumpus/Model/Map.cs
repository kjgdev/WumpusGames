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
                    for (int i = 0; i < 10; i++)
                    {
                        textMap[row][i] = value[i];
                    }
                    row++;
                }
            }
            catch { }

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
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
        public void addBreeze(int x, int y)
        {
            if (x < 0 || x > 9 || y < 0 || y > 9) return;
            map[x][y].Breeze = true;
        }

        public void addStench(int x, int y)
        {
            if (x < 0 || x > 9 || y < 0 || y > 9) return;
            map[x][y].Stench = true;
        }
        public void randomMap(int numberWumpus, int numberPit, int numberGold)
        {
            Random random = new Random();

            int x;
            int y;

            for (int i = 0; i < numberWumpus; i++)
            {
                do
                {
                    x = random.Next(0, 9);
                    y = random.Next(0, 9);
                }
                while (map[x][y].Wumpus == true || map[x][y].Pit == true || map[x][y].Gold == true);

                map[x][y].Wumpus = true;
                addStench(x + 1, y);
                addStench(x - 1, y);
                addStench(x, y + 1);
                addStench(x, y - 1);
            }

            for (int i = 0; i < numberPit; i++)
            {
                do
                {
                    x = random.Next(0, 9);
                    y = random.Next(0, 9);
                }
                while (map[x][y].Wumpus == true || map[x][y].Pit == true || map[x][y].Gold == true);

                map[x][y].Pit = true;
                addBreeze(x + 1, y);
                addBreeze(x - 1, y);
                addBreeze(x, y + 1);
                addBreeze(x, y - 1);
            }

            for (int i = 0; i < numberPit; i++)
            {
                do
                {
                    x = random.Next(0, 9);
                    y = random.Next(0, 9);
                }
                while (map[x][y].Wumpus == true || map[x][y].Pit == true || map[x][y].Gold == true);

                map[x][y].Gold = true;
            }
            do
            {
                x = random.Next(0, 9);
                y = random.Next(0, 9);
            }
            while (map[x][y].Wumpus == true || map[x][y].Pit == true || map[x][y].Gold == true || map[x][y].Breeze == true || map[x][y].Stench == true);

            map[x][y].Player = true;
            player.locationX = x;
            player.locationY = y;
        }
    }
}
