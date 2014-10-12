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
#endregion

namespace GameLibrary.Map.DungeonGeneration
{
    [Serializable()]
    public class CaveDungeon : Dungeon
    {
        public CaveDungeon(String _Name, Vector3 _Position, Vector3 _Size, RegionEnum _RegionEnum, World.World _ParentWorld)
            : base(_Name, _Position, _Size, _RegionEnum, _ParentWorld)
        {
        }

        public CaveDungeon(SerializationInfo info, StreamingContext ctxt) 
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

            int var_Width = 9;
            int var_Heigth = 9;

            for (int x = 0; x < var_Width; x++)
            {
                for (int y = 0; y < var_Heigth; y++)
                {
                    GameLibrary.Factory.RegionFactory.regionFactory.createChunkInRegion(this, (int)var_Position.X + var_SizeX * x, (int)var_Position.Y + var_SizeY * y);
                }
            }

            //Block.Block var_Block = var_Chunk.getBlockAtCoordinate(var_Position);
            //var_Block.setFirstLayer(Block.BlockEnum.Ground1);
            //this.FillMapRoom(this.Position, new Vector3(3,3,0), this.Position, this.Position);

            this.generateDungeon(var_Width, var_Heigth);
        }


        // 1 == Floor ! 0 == Wall ! 2 == StairUp ! 3 == Treasure
        private void generateDungeon(int _Width, int _Heigth)
        {
            int var_Width = _Width * 10;
            int var_Heigth = _Heigth * 10;
            
            int[,] var_Map = this.generateMap(var_Width, var_Heigth, 2);
            this.placeStairUp(var_Width, var_Heigth, var_Map);
            this.placeTreasure(var_Width, var_Heigth, var_Map);
            for (int x = 0; x < var_Width; x++)
            {
                for (int y = 0; y < var_Heigth; y++)
                {
                    if (var_Map[x, y] == 1)
                    {
                        Block.Block var_Block = World.World.world.getBlockAtCoordinate(this.Position + new Vector3(x, y, 0) * Block.Block.BlockSize);
                        var_Block.setFirstLayer(Block.BlockEnum.Ground1);
                    }
                    else if (var_Map[x, y] == 2)
                    {
                        Block.Block var_Block = World.World.world.getBlockAtCoordinate(this.Position + new Vector3(x, y, 0) * Block.Block.BlockSize);
                        var_Block.setFirstLayer(Block.BlockEnum.Ground1);
                        var_Block.drawColor = Color.Green;
                    }
                    else if (var_Map[x, y] == 3)
                    {
                        Block.Block var_Block = World.World.world.getBlockAtCoordinate(this.Position + new Vector3(x, y, 0) * Block.Block.BlockSize);
                        var_Block.setFirstLayer(Block.BlockEnum.Ground1);
                        var_Block.drawColor = Color.Yellow;
                    }
                }
            }
        }

        public int[,] generateMap(int _Width, int _Heigth, int _Steps)
        {
            //Create a new map
            int[,] cellmap = new int[_Width,_Heigth];
            //Set up the map with random values
            cellmap = initialiseMap(_Width, _Heigth, cellmap);
            //And now run the simulation for a set number of steps
            for (int i = 0; i < _Steps; i++)
            {
                cellmap = doSimulationStep(_Width, _Heigth, cellmap);
            }

            return cellmap;
        }

        public int[,] initialiseMap(int _Width, int _Heigth, int[,] map)
        {
            float chanceToStartAlive = 0.45f;

            Random var_Random = new Random();
            for (int x = 0; x < _Width; x++)
            {
                for (int y = 0; y < _Heigth; y++)
                {
                    if (var_Random.NextDouble() < chanceToStartAlive)
                    {
                        map[x, y] = 0;
                    }
                    else
                    {
                        map[x, y] = 1;
                    }
                }
            }
            return map;
        }

        public int countAliveNeighbours(int[,] map, int x, int y)
        {
            int count = 0;
            for(int i=-1; i<2; i++)
            {
                for(int j=-1; j<2; j++)
                {
                    int neighbour_x = x+i;
                    int neighbour_y = y+j;
                    //If we're looking at the middle point
                    if(i == 0 && j == 0)
                    {
                        //Do nothing, we don't want to add ourselves in!
                    }
                    //In case the index we're looking at it off the edge of the map
                    else if(neighbour_x < 0 || neighbour_y < 0 || neighbour_x >= map.GetLength(0) || neighbour_y >= map.GetLength(1))
                    {
                        count = count + 1;
                    }
                    //Otherwise, a normal check of the neighbour
                    else if (map[neighbour_x, neighbour_y] == 0)
                    {
                        count = count + 1;
                    }
                }
            }
            return count;
        }

        public int[,] doSimulationStep(int _Width, int _Heigth, int[,] oldMap)
        {
            int var_DeathLimit = 4;
            int var_BirthLimit = 3;
            int[,] newMap = new int[_Width, _Heigth];
            //Loop over each row and column of the map
            for(int x=0; x < oldMap.GetLength(0); x++){
                for (int y = 0; y < oldMap.GetLength(1); y++)
                {
                    int nbs = countAliveNeighbours(oldMap, x, y);
                    //The new value is based on our simulation rules
                    //First, if a cell is alive but has too few neighbours, kill it.
                    if(oldMap[x,y] == 0)
                    {
                        if (nbs < var_DeathLimit)
                        {
                            newMap[x,y] = 1;
                        }
                        else{
                            newMap[x,y] = 0;
                        }
                    } //Otherwise, if the cell is dead now, check if it has the right number of neighbours to be 'born'
                    else
                    {
                        if (nbs > var_BirthLimit)
                        {
                            newMap[x,y] = 0;
                        }
                        else{
                            newMap[x,y] = 1;
                        }
                    }
                }
            }
            return newMap;
        }

        public void placeTreasure(int _Width, int _Heigth, int[,] _Map)
        {
            //How hidden does a spot need to be for treasure?
            //I find 5 or 6 is good. 6 for very rare treasure.
            int var_Limit = 6;
            int var_LimitMax = 7;

            float var_Factor = 0.25f;

            Random var_Random = new Random();

            for (int x = 0; x < _Width; x++)
            {
                for (int y = 0; y < _Heigth; y++)
                {
                    if (_Map[x, y] == 1)
                    {
                        int nbs = countAliveNeighbours(_Map, x, y);
                        if (nbs >= var_Limit && nbs < var_LimitMax)
                        {
                            if (var_Random.NextDouble() < var_Factor)
                            {
                                _Map[x, y] = 3;
                            }
                        }
                    }
                }
            }
        }

        public void placeStairUp(int _Width, int _Heigth, int[,] _Map)
        {
            //How hidden does a spot need to be for treasure?
            //I find 5 or 6 is good. 6 for very rare treasure.
            int var_Limit = 6;
            int var_LimitMax = 7;

            float var_Factor = 0.25f;

            Random var_Random = new Random();

            for (int x = 0; x < _Width; x++)
            {
                for (int y = 0; y < _Heigth; y++)
                {
                    if (_Map[x, y] == 1)
                    {
                        int nbs = countAliveNeighbours(_Map, x, y);
                        if (nbs >= var_Limit && nbs < var_LimitMax)
                        {
                            if (var_Random.NextDouble() < var_Factor)
                            {
                                _Map[x, y] = 2;
                            }
                        }
                    }
                }
            }
        }
    }
}
