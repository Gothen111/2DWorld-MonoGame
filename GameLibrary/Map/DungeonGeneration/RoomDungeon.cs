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
using GameLibrary.Enums;
#endregion

namespace GameLibrary.Map.DungeonGeneration
{
    [Serializable()]
    public class RoomDungeon : Dungeon
    {
        public RoomDungeon(String _Name, Vector3 _Position, Vector3 _Size, RegionEnum _RegionEnum, World.World _ParentWorld, int _DungeonId)
            : base(_Name, _Position, _Size, _RegionEnum, _ParentWorld, _DungeonId)
        {
        }

        public RoomDungeon(SerializationInfo info, StreamingContext ctxt) 
            : base(info, ctxt)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
        }

        public override void createDungeon()
        {
            base.createDungeon();
            Vector3 var_Position = this.Position;

            int var_SizeX = Chunk.Chunk.chunkSizeX * Block.Block.BlockSize;
            int var_SizeY = Chunk.Chunk.chunkSizeY * Block.Block.BlockSize;

            int var_Width = 3;
            int var_Heigth = 3;

            for (int x = 0; x < var_Width; x++)
            {
                for (int y = 0; y < var_Heigth; y++)
                {
                    GameLibrary.Factory.RegionFactory.regionFactory.createChunkInRegion(this, (int)var_Position.X + var_SizeX * x, (int)var_Position.Y + var_SizeY * y);
                }
            }

            this.generateDungeon(var_Width, var_Heigth);

            this.setAllNeighboursOfChunks();
        }


        // 0 == Wall ! 1 == Floor  ! 2 == StairUp ! 3 == Treasure
        private void generateDungeon(int _Width, int _Heigth)
        {
            int var_Width = _Width * 10;
            int var_Heigth = _Heigth * 10;
            
            int[,] var_Map = new int[var_Width, var_Heigth];
            this.placeRooms(var_Map, this.Rooms);
            //this.placeStairUp(var_Width, var_Heigth, var_Map);
            //this.placeTreasure(var_Width, var_Heigth, var_Map);
            for (int x = 0; x < var_Width; x++)
            {
                for (int y = 0; y < var_Heigth; y++)
                {
                    if (var_Map[x, y] == 1)
                    {
                        Block.Block var_Block = this.getBlockAtCoordinate(this.Position + new Vector3(x, y, 0) * Block.Block.BlockSize);
                        var_Block.setFirstLayer(BlockEnum.Ground1);
                        //var_Block.DrawColor = Color.Green;
                    }
                    else if (var_Map[x, y] == 2)
                    {
                        Block.Block var_Block = this.getBlockAtCoordinate(this.Position + new Vector3(x, y, 0) * Block.Block.BlockSize);
                        var_Block.setFirstLayer(BlockEnum.Ground1);
                        //var_Block.DrawColor = Color.Red;
                    }
                    else if (var_Map[x, y] == 3)
                    {
                        Block.Block var_Block = this.getBlockAtCoordinate(this.Position + new Vector3(x, y, 0) * Block.Block.BlockSize);
                        var_Block = new Block.Blocks.TeleportBlock(this.Position + new Vector3(x, y, 0) * Block.Block.BlockSize, BlockEnum.Ground1, (Chunk.Chunk)var_Block.Parent, this.Position + new Vector3(x, y, 0) * Block.Block.BlockSize, false, 0);
                        var_Block.setFirstLayer(BlockEnum.Ground1);
                        var_Block.DrawColor = Color.Yellow;
                        this.setBlockAtCoordinate(this.Position + new Vector3(x, y, 0) * Block.Block.BlockSize, var_Block);
                        this.Exits.Add(var_Block);
                    }
                }
            }
        }

        private void hCorridor(int[,] _Map, int _StartX, int _EndX, int _Y) 
        {
		    for (int x = Math.Min(_StartX, _EndX); x < Math.Max(_StartX, _EndX); x++) {
			    _Map[x, _Y] = 1;
		    }
	    }

        private void vCorridor(int[,] _Map, int _X, int _StartY, int _EndY) 
        {
		    for (int y = Math.Min(_StartY, _EndY); y < Math.Max(_StartY, _EndY); y++) {
			    _Map[_X, y] = 1;
		    }
	    }

        private void placeRooms(int[,] _Map, List<Room.Room> _Rooms) 
        {
	        Vector3 var_NewCenter = new Vector3();

            int var_RoomCount = 20;

            int var_MinRoomSize = 2;
            int var_MaxRoomSize = 5;

	        // randomize values for each room
	        for (int r = 0; r < var_RoomCount; r++) 
            {
                int w = var_MinRoomSize + Utility.Random.Random.GenerateGoodRandomNumber(var_MinRoomSize, var_MaxRoomSize - var_MinRoomSize);
                int h = var_MinRoomSize + Utility.Random.Random.GenerateGoodRandomNumber(var_MinRoomSize, var_MaxRoomSize - var_MinRoomSize);
		        int x = Utility.Random.Random.GenerateGoodRandomNumber(0, _Map.GetLength(0)-w-1);
		        int y = Utility.Random.Random.GenerateGoodRandomNumber(0, _Map.GetLength(1)-h-1);

		        // create room with randomized values
		        Room.Room var_NewRoom = new Room.Room(new Vector3(x,y,0),new Vector3(w,h,0));

		        bool var_Failed = false;
                foreach(Room.Room var_Room in _Rooms)
                {
                    if (var_NewRoom.intersects(var_Room)) 
                    {
				        var_Failed = true;
				        break;
			        }
                }
		        if (!var_Failed) 
                {
			        // local function to carve out new room
			        //createRoom(newRoom);

			        // store center for new room
			        var_NewCenter = var_NewRoom.Bounds.Center;

			        if(_Rooms.Count != 0)
                    {
				        // store center of previous room
                        Vector3 var_PrevCenter = _Rooms[_Rooms.Count - 1].Bounds.Center;

				        // carve out corridors between rooms based on centers
				        // randomly start with horizontal or vertical corridors
				        if (Utility.Random.Random.GenerateGoodRandomNumber(1,2) == 1) 
                        {
					        hCorridor(_Map, (int)var_PrevCenter.X, (int)var_NewCenter.X, (int)var_PrevCenter.Y);
                            vCorridor(_Map, (int)var_NewCenter.X, (int)var_PrevCenter.Y, (int)var_NewCenter.Y);
				        } 
                        else 
                        {
                            vCorridor(_Map,(int)var_PrevCenter.X, (int)var_PrevCenter.Y, (int)var_NewCenter.Y);
                            hCorridor(_Map, (int)var_PrevCenter.X, (int)var_NewCenter.X, (int)var_NewCenter.Y);
					    }
			        }
			     }
                if (!var_Failed)
                {
                    _Rooms.Add(var_NewRoom);
                    for (int x1 = x; x1 < w + x; x1++)
                    {
                        for (int y1 = y; y1 < h + y; y1++)
                        {
                            _Map[x1, y1] = 2;
                        }
                    }

                    _Map[(int)var_NewCenter.X, (int)var_NewCenter.Y] = 3;
                }
		    }
	    }
    }
}
