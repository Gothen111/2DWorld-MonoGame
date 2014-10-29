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
using GameLibrary.Connection.Message;
using GameLibrary.Connection;
using GameLibrary.Enums;
using GameLibrary.Map.World.SearchFlags;
#endregion

#region Using Statements Class Specific
#endregion

namespace GameLibrary.Map.Block.Blocks
{
    [Serializable()]
    public class TeleportBlock : Block
    {
        private Vector3 destinationLocation;
        private int dimensionId;

        private List<Searchflag> allowedFlags;

        public TeleportBlock(Vector3 _Position, BlockEnum _BlockEnum, Chunk.Chunk _ParentChunk, Vector3 _Destination, int _DimensionId)
            : base((int)_Position.X, (int)_Position.Y, _BlockEnum, _ParentChunk)
        {
            this.destinationLocation = _Destination;
            this.dimensionId = _DimensionId;

            this.allowedFlags = new List<Searchflag>();
            this.allowedFlags.Add(new PlayerObjectFlag());
        }

        public TeleportBlock(SerializationInfo info, StreamingContext ctxt) 
            :base(info, ctxt)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
        }

        public override void onObjectEntersBlock(Object.Object var_Object)
        {
            base.onObjectEntersBlock(var_Object);
            foreach (Searchflag var_Searchflag in this.allowedFlags)
            {
                if (var_Searchflag.hasFlag(var_Object))
                {
                    var_Object.teleportTo(this.destinationLocation, this.dimensionId);
                    return;
                }
            }
        }
    }
}
