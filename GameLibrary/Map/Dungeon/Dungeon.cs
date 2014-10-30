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
using GameLibrary.Map.Region;
using Utility.Corpus;
using GameLibrary.Collison;
using GameLibrary.Enums;
#endregion


namespace GameLibrary.Map.Dungeon
{
    [Serializable()]
    public class Dungeon : Region.Region
    {
        private List<Room.Room> rooms;

        public List<Room.Room> Rooms
        {
            get { return rooms; }
            set { rooms = value; }
        }

        private List<Block.Block> exits;

        public List<Block.Block> Exits
        {
            get { return exits; }
            set { exits = value; }
        }

        public Dungeon(String _Name, Vector3 _Position, Vector3 _Size, RegionEnum _RegionEnum, Dimension.Dimension _ParentDimension)
            : base(_Name, (int)_Position.X, (int)_Position.Y, _Size, _RegionEnum, _ParentDimension)
        {
            this.rooms = new List<Room.Room>();
            this.exits = new List<Block.Block>();
        }

        public Dungeon(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
        }

        public override void createRegion()
        {
            base.createRegion();
            this.createDungeon();
        }

        protected virtual void createDungeon()
        {
        }
    }
}
