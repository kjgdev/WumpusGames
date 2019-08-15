using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wumpus.Model
{
    public class Player
    {
        public int locationX;
        public int locationY;

        public Player setLocation(int x, int y)
        {
            Player player = new Player();
            player.locationX = x;
            player.locationY = y;
            return player;
        }
    }
}
