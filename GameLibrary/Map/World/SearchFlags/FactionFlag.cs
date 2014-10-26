#region Using Statements Standard
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.Runtime.Serialization;
#endregion

#region Using Statements Class Specific
#endregion

namespace GameLibrary.Map.World.SearchFlags
{
    public class FactionFlag : Searchflag
    {
        private Behaviour.Member.Faction faction;

        public FactionFlag(Behaviour.Member.Faction _Faction) : base()
        {
            this.faction = _Faction;
        }

        public FactionFlag(GameLibrary.Factory.FactoryEnums.FactionEnum _FactionEnum)
        {
            this.faction = GameLibrary.Factory.BehaviourFactory.behaviourFactory.getFaction(_FactionEnum);
        }

        public override Boolean hasFlag(GameLibrary.Object.Object _Object)
        {
            if (_Object is GameLibrary.Object.FactionObject)
            {
                return (_Object as GameLibrary.Object.FactionObject).Faction == this.faction;
            }
            else
            {
                return false;
            }
        }
    }
}
