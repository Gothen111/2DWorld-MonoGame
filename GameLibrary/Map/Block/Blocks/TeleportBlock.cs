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
#endregion

#region Using Statements Class Specific
#endregion

namespace GameLibrary.Map.Block.Blocks
{
    [Serializable()]
    public class TeleportBlock : Block
    {
        private Vector3 destinationLocation;
        private Block destinationBlock;

        private bool teleportToBlock;

        private bool toDungeon;

        public TeleportBlock(Vector3 _Position, BlockEnum _BlockEnum, Chunk.Chunk _ParentChunk, Vector3 _Destination, bool _ToDungeon)
            : base((int)_Position.X, (int)_Position.Y, _BlockEnum, _ParentChunk)
        {
            this.destinationLocation = _Destination;
            this.teleportToBlock = false;
            this.toDungeon = _ToDungeon;
        }

        public TeleportBlock(Vector3 _Position, BlockEnum _BlockEnum, Chunk.Chunk _ParentChunk, Block _DestinationBlock, bool _ToDungeon)
            : base((int)_Position.X, (int)_Position.Y, _BlockEnum, _ParentChunk)
        {
            this.destinationBlock = _DestinationBlock;
            this.teleportToBlock = true;
            this.toDungeon = _ToDungeon;
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
            if (teleportToBlock)
            {
                if (this.destinationBlock != null)
                {
                    //var_Object.teleportTo(this.destinationBlock.Position + new Vector3(Block.BlockSize / 2, Block.BlockSize / 2, 0), this.toDungeon);
                    var_Object.teleportTo(this.destinationBlock, this.toDungeon);
                }
            }
            else
            {
                var_Object.teleportTo(this.destinationLocation, this.toDungeon);
            }
        }
    }
}
