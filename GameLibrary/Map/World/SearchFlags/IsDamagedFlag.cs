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
    public class IsDamagedFlag : Searchflag
    {
        public override Boolean hasFlag(GameLibrary.Object.Object _Object)
        {
            if (_Object is Object.LivingObject)
            {
                return ((Object.LivingObject)_Object).HealthPoints < ((Object.LivingObject)_Object).MaxHealthPoints;
            }
            else
            {
                return false;
            }
        }
    }
}
