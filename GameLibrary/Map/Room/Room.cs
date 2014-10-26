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

namespace GameLibrary.Map.Room
{
    [Serializable()]
    public class Room : Box
    {
        Block.Block[] blocks;

        public Room(Vector3 _Position, Vector3 _Size)
            :base()
        {
        }

        public Room(SerializationInfo info, StreamingContext ctxt) 
            : base(info, ctxt)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
        }

        public void setBlockAtPosition(Vector3 _Position, Block.Block _Block)
        {
            int var_Position = (int)(_Position.X + _Position.Y * this.Size.X);
            this.blocks[var_Position] = _Block;
        }

        public Block.Block getBlockAtPosition(Vector3 _Position)
        {
            int var_Position = (int)(_Position.X + _Position.Y * this.Size.X);
            return this.blocks[var_Position];
        }
    }
}
