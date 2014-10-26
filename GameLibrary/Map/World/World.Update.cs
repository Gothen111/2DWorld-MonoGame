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
using GameLibrary.Object;
#endregion

#region Using Statements Class Specific
#endregion

namespace GameLibrary.Map.World
{
    public partial class World
    {
        #region Update

        public override void update(GameTime _GameTime)
        {
            base.update(_GameTime);

            foreach (Dimension.Dimension var_Dimension in this.dimensions)
            {
                List<PlayerObject> var_PlayerObjects = new List<PlayerObject>();
                foreach (PlayerObject var_PlayerObject in this.playerObjects)
                {
                    if (var_PlayerObject.DimensionId == var_Dimension.Id)
                    {
                        var_PlayerObjects.Add(var_PlayerObject);
                    }
                }
                var_Dimension.setCurrentPlayerObjects(var_PlayerObjects);
                var_Dimension.update(_GameTime);
            }
        }

        #endregion
    }
}
