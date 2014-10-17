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
    public class RaceFlag : Searchflag
    {
        private Behaviour.Member.Race race;

        public RaceFlag(Behaviour.Member.Race _Race) : base()
        {
            this.race = _Race;
        }

        public RaceFlag(GameLibrary.Enums.RaceEnum _RaceEnum)
        {
            this.race = GameLibrary.Factory.BehaviourFactory.behaviourFactory.getRace(_RaceEnum);
        }

        public override Boolean hasFlag(GameLibrary.Object.Object _Object)
        {
            if (_Object is GameLibrary.Object.RaceObject)
            {
                return (_Object as GameLibrary.Object.RaceObject).Race == this.race;
            }
            else
            {
                return false;
            }
        }
    }
}
