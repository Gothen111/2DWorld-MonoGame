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
        private int dungeonId;

        public int DungeonId
        {
            get { return dungeonId; }
            set { dungeonId = value; }
        }

        private QuadTree<Object.Object> quadTreeObject;

        public QuadTree<Object.Object> QuadTreeObject
        {
            get { return quadTreeObject; }
            set { quadTreeObject = value; }
        }

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

        public Dungeon(String _Name, Vector3 _Position, Vector3 _Size, RegionEnum _RegionEnum, World.World _ParentWorld, int _DungeonId)
            : base(_Name, (int)_Position.X, (int)_Position.Y, _Size, _RegionEnum, _ParentWorld)
        {
            this.quadTreeObject = new QuadTree<Object.Object>(new Vector3(32, 32, 0), 20);
            this.rooms = new List<Room.Room>();
            this.exits = new List<Block.Block>();
            this.dungeonId = _DungeonId;
        }

        public Dungeon(SerializationInfo info, StreamingContext ctxt) 
            : base(info, ctxt)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
        }

        public virtual void createDungeon()
        {
            //TODO: FILL
            //this.fillWith(null);
        }

        protected void fillWith(Enum _Enum)
        {
            //TODO: FILL
        }

        protected bool isPlaceFree(Vector3 _Position, Vector3 _Size)
        {
            Cube var_Cube = new Cube(_Position, _Size);
            foreach (Room.Room var_Room in this.rooms)
            {
                if (var_Cube.intersects(new Cube(var_Room.Position, var_Room.Size)))
                {
                    return false;
                }
            }

            if (_Position.X < 0 || _Position.X >= 320 - _Size.X*32)
            {
                return false;
            }
            if (_Position.Y < 0 || _Position.Y >= 320 - _Size.Y * 32)
            {
                return false;
            }
            return true;
        }

        protected Room.Room createRoom(Vector3 _Position, Vector3 _Size)//, Enum _Floor, Enum _Wall)
        {
            Room.Room var_Room = new Room.Room(_Position, _Size);
            this.rooms.Add(var_Room);
            return var_Room;
        }

        protected void FillMapRoom(Vector3 _Position, Vector3 _Size, Vector3 _Entrance, Vector3 _Exit)
        {
            FillMapRoomRekusive(_Position, _Size, _Entrance, _Exit);
        }

        private void FillMapRoomRekusive(Vector3 _Position, Vector3 _Size, Vector3 _Entrance, Vector3 _Exit)
        {
            if (this.isPlaceFree(_Position, _Size))
            {
                createRoom(_Position, _Size);

                int var_Start = Utility.Random.Random.GenerateGoodRandomNumber(0, 3);
                int var_End = Utility.Random.Random.GenerateGoodRandomNumber(var_Start, 3);

                for (int i = var_Start; i <= var_End; i++)
                {
                    int var_Number = Utility.Random.Random.GenerateGoodRandomNumber(0, 3);

                    int var_Size = Utility.Random.Random.GenerateGoodRandomNumber(1, 5);

                    if (var_Number == 0)
                    {
                        if (this.isPlaceFree(_Position + new Vector3(0, -var_Size - (int)_Size.Y, 0) * Block.Block.BlockSize, new Vector3(var_Size, var_Size, 0)))
                            FillMapRoomRekusive(_Position + new Vector3(0, -var_Size - (int)_Size.Y, 0) * Block.Block.BlockSize, new Vector3(var_Size, var_Size, 0), _Position, _Position + new Vector3(0, -var_Size - (int)_Size.Y, 0) * Block.Block.BlockSize);
                        else
                            var_Number = 1;
                    }

                    if (var_Number == 1)
                    {
                        if (this.isPlaceFree(_Position + new Vector3(var_Size + (int)_Size.X, 0, 0) * Block.Block.BlockSize, new Vector3(var_Size, var_Size, 0)))
                            FillMapRoomRekusive(_Position + new Vector3(var_Size + (int)_Size.X, 0, 0) * Block.Block.BlockSize, new Vector3(var_Size, var_Size, 0), _Position, _Position + new Vector3(var_Size + (int)_Size.X, 0, 0) * Block.Block.BlockSize);
                        else
                            var_Number = 2;
                    }

                    if (var_Number == 2)
                    {
                        if (this.isPlaceFree(_Position + new Vector3(0, var_Size + (int)_Size.Y, 0) * Block.Block.BlockSize, new Vector3(var_Size, var_Size, 0)))
                            FillMapRoomRekusive(_Position + new Vector3(0, var_Size + (int)_Size.Y, 0) * Block.Block.BlockSize, new Vector3(var_Size, var_Size, 0), _Position, _Position + new Vector3(0, var_Size + (int)_Size.Y, 0) * Block.Block.BlockSize);
                        else
                            var_Number = 3;
                    }

                    if (var_Number == 3)
                    {
                        if (this.isPlaceFree(_Position + new Vector3(-var_Size - (int)_Size.X, 0, 0) * Block.Block.BlockSize, new Vector3(var_Size, var_Size, 0)))
                            FillMapRoomRekusive(_Position + new Vector3(-var_Size - (int)_Size.X, 0, 0) * Block.Block.BlockSize, new Vector3(var_Size, var_Size, 0), _Position, _Position + new Vector3(-var_Size - (int)_Size.X, 0, 0) * Block.Block.BlockSize);
                        //else
                        //var_Number = 3;
                    }
                }
            }

            /*int r = Utility.Random.Random.GenerateGoodRandomNumber(0, 255);
            int g = Utility.Random.Random.GenerateGoodRandomNumber(0, 255);
            int b = Utility.Random.Random.GenerateGoodRandomNumber(0, 255);

            Color var_RoomColor = new Color(r, g, b);

            for (int y = (int)_Entrance.Y; y <= (int)_Exit.Y; y++)
            {
                for (int x = (int)_Entrance.X; x <= (int)_Exit.X; x++)
                {
                    
                }
            }

            for (int y = (int)_Exit.Y; y <= (int)_Entrance.Y; y++)
            {
                for (int x = (int)_Exit.X; x <= (int)_Entrance.X; x++)
                {
                    
                }
            }*/
        }
    }
}
