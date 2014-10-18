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
using Utility.Corpus;
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
            this.Position = _Position;
            this.Size = _Size;

            this.Bounds = new Cube(this.Position, this.Size);

            int var_Length = (int)(_Size.X * _Size.Y);
            this.blocks = new Block.Block[var_Length];

            /*Vector3 var_Pos = new Vector3((int)(this.Position.X / Block.Block.BlockSize), (int)(this.Position.Y / Block.Block.BlockSize), (int)(this.Position.Z / Block.Block.BlockSize));
            Vector3 var_Size = this.Size;//new Vector3((int)(this.Size.X / Block.Block.BlockSize), (int)(this.Size.Y / Block.Block.BlockSize), (int)(this.Size.Z / Block.Block.BlockSize));

            int r = Utility.Random.Random.GenerateGoodRandomNumber(0, 255);
            int g = Utility.Random.Random.GenerateGoodRandomNumber(0, 255);
            int b = Utility.Random.Random.GenerateGoodRandomNumber(0, 255);

            Color var_RoomColor = new Color(r,g,b);

            for (int x = 0; x < var_Size.X; x++)
            {
                for (int y = 0; y < var_Size.Y; y++)
                {
                    Block.Block var_Block = World.World.world.getBlockAtCoordinate((var_Pos + new Vector3(x,y,0))*Block.Block.BlockSize);
                    var_Block.drawColor = var_RoomColor;
                    this.setBlockAtPosition(new Vector3(x, y, 0), var_Block);
                }
            }*/
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

        public bool intersects(Room _Room)
        {
            return this.Bounds.intersects(_Room.Bounds);
        }
    }
}
