using Assets.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.SpecialCell.AbilityStuff
{
    public class Ability
    {
        public string Name { get; private set; }
        public string Desc { get; private set; }
        public bool Abailable { get; private set; }
        public bool IsForceEndTurn { get; private set; } = true;

        private Action Action { get; set; }

        public Ability(Action action, string name, string desc, bool abailable, bool isForceEndTurn = true)
        {
            Action = action;
            Name = name;
            Desc = desc;
            Abailable = abailable;
            IsForceEndTurn = isForceEndTurn;
        }

        public void RunAction()
        {
            Action();
            if (IsForceEndTurn)
            {
                CoreObjectHelper.GetMainController().EndTurn();
            }
        }
    }
}
