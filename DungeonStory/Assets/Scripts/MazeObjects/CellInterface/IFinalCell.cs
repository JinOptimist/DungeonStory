using Assets.Scripts.SpecialCell.AbilityStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.SpecialCell
{
    /// <summary>
    /// Just a mark, that this script on the top of the hierarchy
    /// </summary>
    public interface IFinalCell
    {
        /// <summary>
        /// The ability was trigger when a character moves to the current cell
        /// </summary>
        Ability DefaultAbility { get; set; }

        List<Ability> Abilities { get; set; }
    }
}
