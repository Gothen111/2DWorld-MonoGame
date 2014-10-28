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
using GameLibrary.Object;
#endregion

namespace GameLibrary.Map.World
{
    public partial class World
    {
        #region Player

        public PlayerObject getPlayerObject(int _Id)
        {
            foreach (PlayerObject var_PlayerObject in playerObjects)
            {
                if (var_PlayerObject.Id == _Id)
                {
                    return var_PlayerObject;
                }
            }
            return null;
        }

        public bool containsPlayerObject(Object.PlayerObject _PlayerObject)
        {
            return this.playerObjects.Contains(_PlayerObject);
        }

        public void addPlayerObject(Object.PlayerObject _PlayerObject)
        {
            this.addPlayerObject(_PlayerObject, false);
        }

        public void addPlayerObject(Object.PlayerObject _PlayerObject, bool _OnlyToPlayerList)
        {
            if (!containsPlayerObject(_PlayerObject))
            {
                this.playerObjects.Add(_PlayerObject);

                if (!_OnlyToPlayerList)
                {

                    this.addObject(_PlayerObject);
                }
            }
        }

        #endregion
    }
}
