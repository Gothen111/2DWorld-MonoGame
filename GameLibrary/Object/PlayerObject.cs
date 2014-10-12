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
using GameLibrary.Map.World;
#endregion

#region Using Statements Class Specific
#endregion

namespace GameLibrary.Object
{
    [Serializable()]
    public class PlayerObject : FactionObject
    {
        public PlayerObject() :base()
        {
        }

        public PlayerObject(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
        }

        public override void onChangedBlock()
        {
            base.onChangedBlock();
        }

        public override void onChangedChunk()
        {
            base.onChangedChunk();
            if (Configuration.Configuration.isHost)
            {
                World.world.checkPlayerObjectNeighbourChunks(this);
            }
            else
            {   
            }   
        }
    }
}
