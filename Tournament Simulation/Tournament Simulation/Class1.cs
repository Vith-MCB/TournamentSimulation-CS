using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teams
{
    internal class Team
    {
        public string name;
        public int strength;
        public int points = 0;
        public int matches = 0;
        public int goalsMade = 0;
        public int goalsTaken = 0;

        //Using constructors to create teams at the "Program.cs" file
        public Team(string teamName, int strength)
        {
            name = teamName;
            this.strength = strength;
        }
    }
}
