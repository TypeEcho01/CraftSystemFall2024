using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraftSystemFall2024
{
    public class Player: Person
    {
        public Player(string name): base(name) 
        { 
            
        }

        public override string About()
        {
            return "Player: " + base.About();
        }
    }
}