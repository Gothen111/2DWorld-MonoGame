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
using GameLibrary.Enums;
#endregion

namespace GameLibrary.Map.Block.Blocks
{
    [Serializable()]
    public class TeleportBlock : Block
    {
        private Vector3 destinationLocation;

        private bool toDungeon;

        private int dungeonId;

        public int DungeonId
        {
            get { return dungeonId; }
            set { dungeonId = value; }
        }

        public TeleportBlock(Vector3 _Position, BlockEnum _BlockEnum, Chunk.Chunk _ParentChunk, Vector3 _Destination, bool _ToDungeon, int _DungeonId)
            : base((int)_Position.X, (int)_Position.Y, _BlockEnum, _ParentChunk)
        {
            this.destinationLocation = _Destination;
            this.toDungeon = _ToDungeon;
            this.dungeonId = _DungeonId;
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
            var_Object.teleportTo(this.destinationLocation, this.toDungeon, this.dungeonId);
        }
    }
}
