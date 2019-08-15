using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wumpus.Model;

namespace Wumpus.AI
{
    public class Logic
    {
        BoxStatus[][] mapTemp;
        List<Player> backTracking = new List<Player>();
        public Logic()
        {
            mapTemp = new BoxStatus[10][];
            for(int i = 0; i < 10; i++)
            {
                mapTemp[i] = new BoxStatus[10];
            }
            for(int i = 0; i < 10; i++)
            {
                for(int j = 0; j < 10; j++)
                {
                    mapTemp[i][j] = new BoxStatus();
                }
            }
        }

        public bool gameDie(Map mapData)
        {
            if (mapData.map[mapData.player.locationX][mapData.player.locationY].Pit || mapData.map[mapData.player.locationX][mapData.player.locationY].Wumpus) return true;
            return false;
        }

        public void processLogic(Map mapData)
        {
            int x = mapData. player.locationX;
            int y = mapData.player.locationY;
            mapTemp[x][y].Visiable = true;
            BoxStatus box = mapData.map[x][y];

            /*   
             *    |   |                  | P |    
             * ---|---|---            ---|---|---
             *    | B |         =>     P |   | P    
             * ---|---|---            ---|---|---
             *    |   |                  | P | 
             */
            if (box.Breeze == true)
            {
                if (isPit(x+1,y))
                {
                    mapTemp[x + 1][y].Pit = true;
                }

                if (isPit(x - 1, y))
                {
                    mapTemp[x - 1][y].Pit = true;
                }

                if (isPit(x, y+1))
                {
                    mapTemp[x][y+1].Pit = true;
                }

                if (isPit(x, y-1))
                {
                    mapTemp[x][y - 1].Pit = true;
                }
            }

            /*   
             *    |   |                     | W |    
             * ---|---|---               ---|---|---
             *    | S |         =>        W |   | W    
             * ---|---|---               ---|---|---
             *    |   |                     | W | 
             */
            if (box.Stench == true)
            {
                if (isWumpus(x+1,y))
                {
                    mapTemp[x + 1][y].Wumpus = true;
                }

                if (isWumpus(x - 1, y))
                {
                    mapTemp[x - 1][y].Wumpus = true;
                }

                if (isWumpus(x, y+1))
                {
                    mapTemp[x][y + 1].Wumpus = true;
                }

                if (isWumpus(x, y-1))
                {
                    mapTemp[x][y - 1].Wumpus = true;
                }
            }

            /*
             * noB(x,y) and noS(x,y) -> no{ P(x+1,y),P(x-1,y),P(x,y+1),P(x,y-1)}, no{ W(x+1,y),W(x-1,y),W(x,y+1),W(x,y-1) }
             */
            if (box.Stench == false && box.Breeze == false)
            {
                if (noWumpus(x+1,y))
                {
                    mapTemp[x + 1][y].Wumpus = false;
                }

                if (noWumpus(x - 1, y))
                {
                    mapTemp[x - 1][y].Wumpus = false;
                }

                if (noWumpus(x, y+1))
                {
                    mapTemp[x][y + 1].Wumpus = false;
                }

                if (noWumpus(x, y-1))
                {
                    mapTemp[x][y - 1].Wumpus = false;
                }

                if (noPit(x+1,y))
                {
                    mapTemp[x + 1][y].Pit = false;
                }

                if (noPit(x - 1, y))
                {
                    mapTemp[x - 1][y].Pit = false;
                }

                if (noPit(x, y+1))
                {
                    mapTemp[x][y + 1].Pit = false;
                }

                if (noPit(x, y-1))
                {
                    mapTemp[x][y - 1].Pit = false;
                }
            }
            /* 
             *  B |   | B               | P |    
             * ---|---|---           ---|---|---
             *    | B |         =>    P |   | P    
             * ---|---|---           ---|---|---
             *  B |   | B               | P | 
             */
            if (box.Breeze == true)
            {
                if (isBreeze(x-1,y+1) && isBreeze(x-1,y-1))
                {
                    mapTemp[x - 1][y].Pit = true;
                    mapTemp[x - 1][y].Pit = false;

                }

                if (isBreeze(x-1,y+1) && isBreeze(x + 1,y + 1))
                {
                    mapTemp[x][y+1].Pit = true;
                    mapTemp[x][y+1].Pit = false;
                }

                if (isBreeze(x + 1,y - 1) && isBreeze( x + 1,y + 1))
                {
                    mapTemp[x+1][y].Pit = true;
                    mapTemp[x+1][y].Pit = false;
                }

                if (isBreeze(x - 1,y - 1) && isBreeze(x + 1,y - 1))
                {
                    mapTemp[x-1][y].Pit = true;
                    mapTemp[x-1][y].Pit = false;
                }
            }

            /* 
           *  S |   | S               | W |    
           * ---|---|---           ---|---|---
           *    | S |         =>    W |   | W    
           * ---|---|---           ---|---|---
           *  S |   | S               | W | 
           */
            if (box.Stench == true)
            {
                if (isStench(x - 1,y + 1) && isStench( x - 1,y - 1))
                {
                    mapTemp[x - 1][y].Wumpus = true;
                    mapTemp[x - 1][y].Wumpus = false;
                }

                if (isStench(x - 1, y + 1) && isStench(x + 1, y + 1))
                {
                    mapTemp[x][y + 1].Wumpus = true;
                    mapTemp[x][y + 1].Wumpus = false;
                }

                if (isStench(x + 1, y - 1) && isStench(x + 1, y + 1))
                {
                    mapTemp[x + 1][y].Wumpus = true;
                    mapTemp[x + 1][y].Wumpus = false;
                }

                if (isStench(x - 1, y - 1) && isStench(x + 1, y - 1))
                {
                    mapTemp[x - 1][y].Wumpus = true;
                    mapTemp[x - 1][y].Wumpus = false;
                }
            }

        }

        public bool processGo(Map mapData)
        {
            int x = mapData.player.locationX;
            int y = mapData.player.locationY;
            mapData.map[x][y].Player = false;
            processLogic(mapData);
            List<Player> playerOptimal = new List<Player>();

            if (checkWarning(x + 1, y)) { playerOptimal.Add(new Player().setLocation(x + 1, y)); }
            if (checkWarning(x - 1, y)) { playerOptimal.Add(new Player().setLocation(x - 1, y)); }
            if (checkWarning(x, y+1)) { playerOptimal.Add(new Player().setLocation(x, y+1)); }
            if (checkWarning(x, y-1)) { playerOptimal.Add(new Player().setLocation(x, y-1)); }
            
            Random random = new Random();
            
            int n = 0;
            if(playerOptimal.Count == 0)
            {
                if (backTracking.Count >1)
                {
                    playerOptimal.Add(new Player().setLocation(backTracking[backTracking.Count - 2].locationX, backTracking[backTracking.Count - 2].locationY));
                    backTracking.RemoveAt(backTracking.Count-1);
                }
                else return false;
            }
            else
            {
                n = random.Next(0, playerOptimal.Count - 1);
                backTracking.Add(playerOptimal[n]);
            }
            mapData.player = playerOptimal[n];
            return true;
            
        }

        public bool checkGold(Map mapData)
        {
            if (mapData.map[mapData.player.locationX][mapData.player.locationY].Gold)
            {
                mapData.map[mapData.player.locationX][mapData.player.locationY].Gold = false;
                return true;
            }
            return false;
        }

    

        bool checkWarning(int x,int y)
        {
            if (x < 0 || x > 9 || y < 0 || y > 9) return false;
            if (mapTemp[x][y].Visiable == false && mapTemp[x][y].Pit == false && mapTemp[x][y].Wumpus == false) return true;
            return false;
        }

        bool isPit(int x,int y)
        {
            if (x < 0 || x > 9 || y < 0 || y > 9) return false;
            if (mapTemp[x][y].Visiable == false && mapTemp[x][y].Pit == false) return true;
            return false;
        }

        bool isWumpus(int x,int y)
        {
            if (x < 0 || x > 9 || y < 0 || y > 9) return false;
            if (mapTemp[x][y].Visiable == false && mapTemp[x][y].Wumpus == false) return true;
            return false;
        }

        bool noWumpus(int x,int y)
        {
            if (x < 0 || x > 9 || y < 0 || y > 9) return false;
            if (mapTemp[x][y].Visiable == false && mapTemp[x][y].Wumpus == true) return true;
            return false;
        }

        bool noPit(int x,int y)
        {
            if (x < 0 || x > 9 || y < 0 || y > 9) return false;
            if (mapTemp[x][y].Visiable == false && mapTemp[x][y].Pit == true) return true;
            return false;
        }

        bool isBreeze(int x, int y)
        {
            if (x < 0 || x > 9 || y < 0 || y > 9) return false;
            if (mapTemp[x][y].Visiable == true && mapTemp[x][y].Breeze == true) return true;
            return false;
        }

        bool isStench(int x,int y)
        {
            if (x < 0 || x > 9 || y < 0 || y > 9) return false;
            if (mapTemp[x][y].Visiable == true && mapTemp[x][y].Stench == true) return true;
            return false;
        }
    }
}
