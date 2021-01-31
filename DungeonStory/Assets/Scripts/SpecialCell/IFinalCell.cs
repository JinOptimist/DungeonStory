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
        List<Ability> Abilities { get; set; }
    }
}
