using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wumpus.Model
{
    public class BoxStatus
    {
        public bool Wumpus;
        public bool Pit;
        public bool Breeze;
        public bool Stench;
        public bool Gold;
        public bool Visiable;
        public bool Player;

        public BoxStatus()
        {
            this.Wumpus = false;
            this.Pit = false;
            this.Breeze = false;
            this.Stench = false;
            this.Gold = false;
            this.Visiable = false;
            this.Player = false;
        }
    }
}
