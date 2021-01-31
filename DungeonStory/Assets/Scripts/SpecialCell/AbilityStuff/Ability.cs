using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.SpecialCell.AbilityStuff
{
    public class Ability
    {
        public Action Action { get; private set; }
        public string Name { get; private set; }
        public string Desc { get; private set; }
        public bool Abailable { get; private set; }

        public Ability(Action action, string name, string desc, bool abailable)
        {
            Action = action;
            Name = name;
            Desc = desc;
            Abailable = abailable;
        }
    }
}
