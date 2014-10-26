#region Using Statements Standard
using System;
using System.Linq;
using System.Text;
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
using GameLibrary.Object;
using GameLibrary.Map.World;
using GameLibrary.Map.Region;
using GameLibrary.Map.Chunk;
using GameLibrary.Map.Block;
using GameLibrary.Enums;
using GameLibrary.Map.DungeonGeneration;
#endregion

namespace GameLibrary.Factory
{
    public class ChunkFactory
    {
        public static ChunkFactory chunkFactory = new ChunkFactory();

        public Chunk createChunk(int _PosX, int _PosY, RegionEnum _RegionEnum, List<Enum> _Layer, Region _ParentRegion)
        {
            switch (_RegionEnum)
            {
                case RegionEnum.Grassland:
                    {
                        return createChunkGrassland(_PosX, _PosY, _Layer, _ParentRegion);
                    }
                case RegionEnum.Snowland:
                    {
                        return createChunkSnowland(_PosX, _PosY, _Layer, _ParentRegion);
                    }
                case RegionEnum.Lavaland:
                    {
                        return createChunkLavaland(_PosX, _PosY, _Layer, _ParentRegion);
                    }
                case RegionEnum.Dungeon:
                    {
                        return createChunkDungeon(_PosX, _PosY, _Layer, _ParentRegion);
                    }
            }
            return null;
        }

        public Chunk generateChunk(Chunk _Chunk)
        {
            switch (_Chunk.ChunkEnum)
            {
                case ChunkEnum.Grassland:
                    {
                        return generateChunkGrassland(_Chunk);
                    }
                case ChunkEnum.Snowland:
                    {
                        return generateChunkSnowland(_Chunk);
                    }
                case ChunkEnum.Lavaland:
                    {
                        return generateChunkLavaland(_Chunk);
                    }
                case ChunkEnum.Dungeon:
                    {
                        return generateChunkDungeon(_Chunk);
                    }
            }
            return null;
        }

        private Chunk createChunkGrassland(int _PosX, int _PosY, List<Enum> _Layer, Region _ParentRegion)
        {
            Chunk var_Result;

            var_Result = new Chunk("Chunk", _PosX, _PosY, _ParentRegion);
            var_Result.ChunkEnum = ChunkEnum.Grassland;
            this.fillChunkWithBlock(var_Result, BlockEnum.Ground1);

            var_Result.setAllNeighboursOfBlocks();

            if (Configuration.Configuration.isHost || Configuration.Configuration.isSinglePlayer)
            {
                //generateWall(var_Result, Utility.Random.Random.GenerateGoodRandomNumber(0, Chunk.chunkSizeX), Utility.Random.Random.GenerateGoodRandomNumber(0, Chunk.chunkSizeY));
                //generateSecondLayer(var_Result, _Layer);
            }
            else
            {
                //generateFlowers(var_Result);
            }

            //var_Result.setBlockAtCoordinate(new Vector3(_PosX + 32, _PosY + 32, 0), new GameLibrary.Map.Block.Blocks.TeleportBlock(new Vector3(_PosX + 32, _PosY + 32, 0), BlockEnum.Ground2, var_Result, _ParentRegion.Dungeons[0].getBlockAtCoordinate(Vector3.Zero), true));
             

            return var_Result;
        }

        private Chunk createChunkSnowland(int _PosX, int _PosY, List<Enum> _Layer, Region _ParentRegion)
        {
            Chunk var_Result;

            var_Result = new Chunk("Chunk", _PosX, _PosY, _ParentRegion);
            var_Result.ChunkEnum = ChunkEnum.Snowland;
            this.fillChunkWithBlock(var_Result, BlockEnum.Ground1);

            var_Result.setAllNeighboursOfBlocks();
            if (Configuration.Configuration.isHost || Configuration.Configuration.isSinglePlayer)
            {
                //generateWall(var_Result, Utility.Random.Random.GenerateGoodRandomNumber(0, Chunk.chunkSizeX), Utility.Random.Random.GenerateGoodRandomNumber(0, Chunk.chunkSizeY));
                //generateSecondLayer(var_Result, _Layer);
            }
            else
            {
            }

            return var_Result;
        }

        private Chunk createChunkLavaland(int _PosX, int _PosY, List<Enum> _Layer, Region _ParentRegion)
        {
            Chunk var_Result;

            var_Result = new Chunk("Chunk", _PosX, _PosY, _ParentRegion);
            var_Result.ChunkEnum = ChunkEnum.Lavaland;
            this.fillChunkWithBlock(var_Result, BlockEnum.Ground1);

            var_Result.setAllNeighboursOfBlocks();
            if (Configuration.Configuration.isHost || Configuration.Configuration.isSinglePlayer)
            {
                //generateWall(var_Result, Utility.Random.Random.GenerateGoodRandomNumber(0, Chunk.chunkSizeX), Utility.Random.Random.GenerateGoodRandomNumber(0, Chunk.chunkSizeY));
                //generateSecondLayer(var_Result, _Layer);
            }
            else 
            { 
            }

            return var_Result;
        }

        private Chunk createChunkDungeon(int _PosX, int _PosY, List<Enum> _Layer, Region _ParentRegion)
        {
            Chunk var_Result;

            var_Result = new Chunk("Chunk", _PosX, _PosY, _ParentRegion);
            var_Result.ChunkEnum = ChunkEnum.Dungeon;
            this.fillChunkWithBlock(var_Result, BlockEnum.Ground2);

            var_Result.setAllNeighboursOfBlocks();
            if (Configuration.Configuration.isHost || Configuration.Configuration.isSinglePlayer)
            {
            }
            else
            {
            }

            return var_Result;
        }

        private Chunk generateChunkGrassland(Chunk _Chunk)
        {
            if (Configuration.Configuration.isHost || Configuration.Configuration.isSinglePlayer)
            {
                generatePaths(_Chunk);
                generateExits(_Chunk);
            }
            else
            {
            }

            return _Chunk;
        }

        private Chunk generateChunkSnowland(Chunk _Chunk)
        {
            if (Configuration.Configuration.isHost || Configuration.Configuration.isSinglePlayer)
            {
                generatePaths(_Chunk);
            }
            else
            {
            }

            return _Chunk;
        }

        private Chunk generateChunkLavaland(Chunk _Chunk)
        {
            if (Configuration.Configuration.isHost || Configuration.Configuration.isSinglePlayer)
            {
                generatePaths(_Chunk);
            }
            else
            {
            }

            return _Chunk;
        }

        private Chunk generateChunkDungeon(Chunk _Chunk)
        {
            if (Configuration.Configuration.isHost || Configuration.Configuration.isSinglePlayer)
            {
            }
            else
            {
            }

            return _Chunk;
        }

        private Chunk fillChunkWithBlock(Chunk _Chunk, BlockEnum _BlockEnum)
        {
            for(int x = 0; x < _Chunk.Size.X; x++)
            {
                for(int y = 0; y < _Chunk.Size.Y; y++)
                {
                    Block var_Block = new Block((int)_Chunk.Position.X + x * Block.BlockSize, (int)_Chunk.Position.Y + y * Block.BlockSize, _BlockEnum, _Chunk);
                    if (!_Chunk.setBlockAtPosition(x, y, var_Block))
                    {
                        Logger.Logger.LogErr("RegionFactory->fillChunkWithBlock(...) : Platzierung nicht möglich!");
                    }
                    else
                    {
                        if (Configuration.Configuration.isHost || Configuration.Configuration.isSinglePlayer)
                        {
                        }
                        else
                        {
                            var_Block.requestFromServer();
                        }
                    }
                }
            }
            return _Chunk;
        }

        private void generateSecondLayer(Chunk _Chunk, List<Enum> _Layer)
        {
            int var_Count = Utility.Random.Random.GenerateGoodRandomNumber(0, (int)((_Chunk.Size.X * _Chunk.Size.Y) * 0.1)); //  / 100
            for (int i = 0; i < var_Count; i++)
            {
                int var_EnumId = Utility.Random.Random.GenerateGoodRandomNumber(0, _Layer.Count);
                int var_PosX = Utility.Random.Random.GenerateGoodRandomNumber(0, (int)_Chunk.Size.X);
                int var_PosY = Utility.Random.Random.GenerateGoodRandomNumber(0, (int)_Chunk.Size.Y);
                rekursiveSetBlock(_Chunk, _Layer.ElementAt(var_EnumId), var_PosX, var_PosY, 100, 20);
            }
        }

        private void rekursiveSetBlock(Chunk _Chunk, Enum _Enum, int _PosX, int _PosY, int _Chance, int _ChanceToDecrease)
        {
            int var_Chance = Utility.Random.Random.GenerateGoodRandomNumber(0, 100);

            if (var_Chance <= _Chance)
            {
                if (_PosX >= 0 && _PosX <= (_Chunk.Size.X-1))
                {
                    if (_PosY >= 0 && _PosY <= (_Chunk.Size.Y - 1))
                    {
                        Block var_Block = _Chunk.getBlockAtPosition(_PosX, _PosY);
                        if (var_Block.Layer[(int)BlockLayerEnum.Layer2] != BlockEnum.Nothing || _Chance <= 0)
                        {
                            return;
                        }
                        if (var_Block.Layer[0]  == BlockEnum.Wall)
                        {
                            return;
                        }

                        _Chunk.getBlockAtPosition(_PosX, _PosY).setLayerAt(_Enum, BlockLayerEnum.Layer2);
                        rekursiveSetBlock(_Chunk, _Enum, _PosX + 1, _PosY, (_Chance - _ChanceToDecrease), _ChanceToDecrease * Utility.Random.Random.GenerateGoodRandomNumber(1, 3));
                        rekursiveSetBlock(_Chunk, _Enum, _PosX - 1, _PosY, (_Chance - _ChanceToDecrease), _ChanceToDecrease * Utility.Random.Random.GenerateGoodRandomNumber(1, 3));
                        rekursiveSetBlock(_Chunk, _Enum, _PosX, _PosY + 1, (_Chance - _ChanceToDecrease), _ChanceToDecrease * Utility.Random.Random.GenerateGoodRandomNumber(1, 3));
                        rekursiveSetBlock(_Chunk, _Enum, _PosX, _PosY - 1, (_Chance - _ChanceToDecrease), _ChanceToDecrease * Utility.Random.Random.GenerateGoodRandomNumber(1, 3));
                    }
                }
            }
        }

        private void generateWall(Chunk _Chunk, int _PosX, int _PosY)
        {
            int var_Steps = 112;
            int var_StepsUp = var_Steps/4;
            int var_StepsLeft = var_Steps/4;
            int var_StepsRight = var_Steps/4;
            int var_StepsDown = var_Steps/4;

            int var_ComeFrom = -1; //0=Up,1=Left,2=Right,3=Down
            _Chunk.getBlockAtPosition(_PosX, _PosY).setFirstLayer(BlockEnum.Wall);

            List<Block> blocksWithWall = new List<Block>();
            while (var_StepsUp + var_StepsLeft + var_StepsRight + var_StepsDown > 0)
            {
                List<int> var_GoTo = new List<int>();
                var_GoTo.AddRange(new List<int>() { 0, 1, 2, 3 });
                var_GoTo.Remove(var_ComeFrom);
                if (var_StepsUp == 0 || _PosY < 0)
                {
                    var_GoTo.Remove(0);
                }
                if (var_StepsLeft == 0 || _PosX < 0)
                {
                    var_GoTo.Remove(1);
                }
                if (var_StepsRight == 0 || _PosX >= Chunk.chunkSizeX)
                {
                    var_GoTo.Remove(2);
                }
                if (var_StepsDown == 0 || _PosY >= Chunk.chunkSizeY)
                {
                    var_GoTo.Remove(3);
                }
                if (var_GoTo.Count == 0)
                {
                    var_GoTo.Add(var_ComeFrom);
                }
                int var_Rand = Utility.Random.Random.GenerateGoodRandomNumber(0, var_GoTo.Count);

                int var_Choice = var_GoTo.ElementAt(var_Rand);

                if (var_Choice == 0 && var_StepsUp>0)
                {
                    _PosX += 0;
                    _PosY -= 1;
                    var_ComeFrom = 3;
                    var_StepsUp -= 1;
                }
                else if (var_Choice == 1 && var_StepsLeft > 0)
                {
                    _PosX -= 1;
                    _PosY += 0;
                    var_ComeFrom = 2;
                    var_StepsLeft -= 1;
                }
                else if (var_Choice == 2 && var_StepsRight > 0)
                {
                    _PosX += 1;
                    _PosY += 0;
                    var_ComeFrom = 1;
                    var_StepsRight -= 1;
                }
                else if (var_Choice == 3 && var_StepsDown > 0)
                {
                    _PosX += 0;
                    _PosY += 1;
                    var_ComeFrom = 0;
                    var_StepsDown -= 1;
                }
                Block var_Block = _Chunk.getBlockAtPosition(_PosX, _PosY);
                if (var_Block != null)
                {
                    var_Block.setFirstLayer(BlockEnum.Wall);
                    blocksWithWall.Add(var_Block);
                }
            }

            for (int x = 0; x < blocksWithWall.Count / 2; x++)
            {
                Block var_Block1 = blocksWithWall.ElementAt(x);
                Block var_Block2 = blocksWithWall.ElementAt(blocksWithWall.Count - x - 1);
                while (!var_Block1.Equals(var_Block2))
                {
                    int moveHorizontal = 0;
                    int moveVertical = 0;
                    if (var_Block1.Position.X < var_Block2.Position.X)
                    {
                        moveHorizontal = 1;
                    }
                    else if (var_Block1.Position.X > var_Block2.Position.X)
                    {
                        moveHorizontal = -1;
                    }
                    if (var_Block1.Position.Y < var_Block2.Position.Y)
                    {
                        moveVertical = 1;
                    }
                    else if (var_Block1.Position.Y > var_Block2.Position.Y)
                    {
                        moveVertical = -1;
                    }

                    if (moveHorizontal == 1 && moveVertical == 1)
                    {
                        var_Block1 = var_Block1.RightNeighbour.BottomNeighbour as Block;
                    }
                    else if (moveHorizontal == 1 && moveVertical == 0)
                    {
                        var_Block1 = var_Block1.RightNeighbour as Block;
                    }
                    else if (moveHorizontal == 1 && moveVertical == -1)
                    {
                        var_Block1 = var_Block1.RightNeighbour.TopNeighbour as Block;
                    }
                    else if (moveHorizontal == 0 && moveVertical == 1)
                    {
                        var_Block1 = var_Block1.BottomNeighbour as Block;
                    }
                    else if (moveHorizontal == 0 && moveVertical == -1)
                    {
                        var_Block1 = var_Block1.TopNeighbour as Block;
                    }
                    else if (moveHorizontal == -1 && moveVertical == 1)
                    {
                        var_Block1 = var_Block1.LeftNeighbour.BottomNeighbour as Block;
                    }
                    else if (moveHorizontal == -1 && moveVertical == 0)
                    {
                        var_Block1 = var_Block1.LeftNeighbour as Block;
                    }
                    else if (moveHorizontal == -1 && moveVertical == -1)
                    {
                        var_Block1 = var_Block1.LeftNeighbour.TopNeighbour as Block;
                    }


                    if (var_Block1 != null)
                    {
                        var_Block1.setFirstLayer(BlockEnum.Wall);
                    }
                    else
                    {
                        Logger.Logger.LogErr("Wallgenerierung hat Chunkgrenzen übersprungen und kann somit nicht ausgefüllt werden");
                        return;
                    }
                }
            }
        }

        private void generatePaths(Chunk _Chunk)
        {
            List<Vector3> var_PossiblePathEntries = new List<Vector3>();
            if (_Chunk.LeftNeighbour != null)
            {
                foreach (Vector3 var_Entry in ((Chunk)_Chunk.LeftNeighbour).PathEntries)
                {
                    if (var_Entry.X >= _Chunk.Position.X - (Block.BlockSize + 5))
                    {
                        var_PossiblePathEntries.Add(var_Entry + new Vector3(Block.BlockSize,0,0));
                    }
                }
            }
            if (_Chunk.RightNeighbour != null)
            {
                foreach (Vector3 var_Entry in ((Chunk)_Chunk.RightNeighbour).PathEntries)
                {
                    if (var_Entry.X <= _Chunk.Bounds.Right + (Block.BlockSize + 5))
                    {
                        var_PossiblePathEntries.Add(var_Entry + new Vector3(-Block.BlockSize, 0, 0));
                    }
                }
            }
            if (_Chunk.TopNeighbour != null)
            {
                foreach (Vector3 var_Entry in ((Chunk)_Chunk.TopNeighbour).PathEntries)
                {
                    if (var_Entry.Y >= _Chunk.Position.Y - (Block.BlockSize + 5))
                    {
                        var_PossiblePathEntries.Add(var_Entry + new Vector3(0, Block.BlockSize, 0));
                    }
                }
            }
            if (_Chunk.BottomNeighbour != null)
            {
                foreach (Vector3 var_Entry in ((Chunk)_Chunk.BottomNeighbour).PathEntries)
                {
                    if (var_Entry.Y <= _Chunk.Bounds.Bottom + (Block.BlockSize + 5))
                    {
                        var_PossiblePathEntries.Add(var_Entry + new Vector3(0, -Block.BlockSize, 0));
                    }
                }
            }

            if(var_PossiblePathEntries.Count == 0)
            {
                var_PossiblePathEntries.Add(_Chunk.Position);
            }

            foreach (Vector3 var_Entry in var_PossiblePathEntries)
            {
                Block var_BlockEntry =_Chunk.getBlockAtCoordinate(var_Entry);
                if (var_BlockEntry != null)
                {
                    var_BlockEntry.setLayerAt(BlockEnum.Ground1, BlockLayerEnum.Layer2);
                    _Chunk.PathEntries.Add(var_Entry);
                }

                Block var_BlockExit = _Chunk.getBlockAtCoordinate(var_Entry + new Vector3(0, (Chunk.chunkSizeY - 1) * Block.BlockSize, 0));
                if (var_BlockExit != null)
                {
                    var_BlockExit.setLayerAt(BlockEnum.Ground1, BlockLayerEnum.Layer2);
                    _Chunk.PathEntries.Add(var_BlockExit.Position);
                }
                this.generatePathFromTwoPoints(_Chunk, var_BlockEntry, var_BlockExit);
            }
        }

        private void generatePathFromTwoPoints(Chunk _Chunk, Block _Entry, Block _Exit)
        {
            /*Vector3 var_Entry = _Entry.Position;
            Vector3 var_Exit = _Exit.Position;

            while (var_Entry != var_Exit)
            {
                if (var_Entry.X > var_Exit.X)
                {
                    var_Entry.X 
                }
            }*/
        }

        private void generateExits(Chunk _Chunk)
        {
            Region var_Region = (Region)_Chunk.Parent;
            if (var_Region != null)
            {
                foreach(Dungeon var_Dungeon in var_Region.Dungeons)
                {
                    foreach(Block var_Exit in var_Dungeon.Exits)
                    {
                        Block var_Block = new GameLibrary.Map.Block.Blocks.TeleportBlock(var_Exit.Position, BlockEnum.Ground1, _Chunk, var_Exit.Position, true, var_Dungeon.DungeonId);
                        var_Block.DrawColor = Color.Blue;
                        _Chunk.setBlockAtCoordinate(var_Exit.Position, var_Block);
                    }
                }
            }
        }

        /*private void generateWall(Chunk _Chunk)
        {
            int var_Count = 10;
            for (int i = 0; i < var_Count; i++)
            {
                int var_PosX = Utility.Random.Random.GenerateGoodRandomNumber(0, (int)_Chunk.Size.X);
                int var_PosY = Utility.Random.Random.GenerateGoodRandomNumber(0, (int)_Chunk.Size.Y);
                rekursiveSetWall(_Chunk, BlockEnum.Wall, var_PosX, var_PosY, 100, 5);
            }
        }

        private void rekursiveSetWall(Chunk _Chunk, Enum _Enum, int _PosX, int _PosY, int _Chance, int _ChanceToDecrease)
        {
            int var_Chance = Utility.Random.Random.GenerateGoodRandomNumber(0, 100);

            if (var_Chance <= _Chance)
            {
                if (_PosX >= 0 && _PosX <= (_Chunk.Size.X - 1))
                {
                    if (_PosY >= 0 && _PosY <= (_Chunk.Size.Y - 1))
                    {
                        if (_Chance <= 0)
                        {
                            return;
                        }

                        _Chunk.getBlockAtPosition(_PosX, _PosY).setFirstLayer(_Enum);
                        rekursiveSetWall(_Chunk, _Enum, _PosX + 1, _PosY, (_Chance - _ChanceToDecrease), _ChanceToDecrease * Utility.Random.Random.GenerateGoodRandomNumber(1, 3));
                        rekursiveSetWall(_Chunk, _Enum, _PosX - 1, _PosY, (_Chance - _ChanceToDecrease), _ChanceToDecrease * Utility.Random.Random.GenerateGoodRandomNumber(1, 3));
                        rekursiveSetWall(_Chunk, _Enum, _PosX, _PosY + 1, (_Chance - _ChanceToDecrease), _ChanceToDecrease * Utility.Random.Random.GenerateGoodRandomNumber(1, 3));
                        rekursiveSetWall(_Chunk, _Enum, _PosX, _PosY - 1, (_Chance - _ChanceToDecrease), _ChanceToDecrease * Utility.Random.Random.GenerateGoodRandomNumber(1, 3));
                    }
                }
            }
        }*/

        private void generateHeightMap(Chunk _Chunk)
        {
            int var_Min = 0;
            int var_Max = 10;
            int var_RandomRange = 5;

            float[,] var_HeightMap = new float[(int)_Chunk.Size.X, (int)_Chunk.Size.Y];

            for (int x = 0; x < var_HeightMap.GetLength(0); x++)
            {
                for (int y = 0; y < var_HeightMap.GetLength(1); y++)
                {
                    var_HeightMap[x, y] = var_Min + Utility.Random.Random.GenerateGoodRandomNumber(0, var_RandomRange);
                }
            }

            for (int x = 0; x < var_HeightMap.GetLength(0); x++)
            {
                for (int y = 0; y < var_HeightMap.GetLength(1); y++)
                {
                    float var_Add = 0;
                    if (x - 1 >= 0)
                    {
                        var_Add += var_HeightMap[x - 1, y];
                    }
                    if (x + 1 < var_HeightMap.GetLength(0))
                    {
                        var_Add += var_HeightMap[x + 1, y];
                    }
                    if (y - 1 >= 0)
                    {
                        var_Add += var_HeightMap[x, y - 1];
                    }
                    if (y + 1 < var_HeightMap.GetLength(1))
                    {
                        var_Add += var_HeightMap[x, y + 1];
                    }
                    var_HeightMap[x, y] = var_Add / 4 + Utility.Random.Random.GenerateGoodRandomNumber(0, var_RandomRange) / 2;
                    if (var_HeightMap[x, y] > var_Max)
                    {
                        var_HeightMap[x, y] = var_Max;
                    }

                    if (var_HeightMap[x, y] >= 3)
                    {
                        Block var_Block = _Chunk.getBlockAtPosition(x, y);
                        var_Block.Height = 1;
                    }
                }
            }
            /*
            for (int x = 0; x < var_HeightMap.GetLength(0); x++)
            {
                for (int y = 0; y < var_HeightMap.GetLength(1); y++)
                {
                    if (var_HeightMap[x, y] >= 3)
                    {
                        Block var_Block = _Chunk.getBlockAtPosition(x, y);
                        var_Block.Height = 1;

                        if (var_Block.RightNeighbour != null)
                        {
                            if (((Block)var_Block.RightNeighbour).Height < var_Block.Height)
                            {
                                var_Block.Layer[0] = BlockEnum.Hill1_Right;
                            }
                        }

                        if (var_Block.LeftNeighbour != null)
                        {
                            if (((Block)var_Block.LeftNeighbour).Height < var_Block.Height)
                            {
                                var_Block.Layer[0] = BlockEnum.Hill1_Left;
                            }
                        }

                        if (var_Block.TopNeighbour != null)
                        {
                            if (((Block)var_Block.TopNeighbour).Height < var_Block.Height)
                            {
                                _Chunk.getBlockAtPosition(x, y).Layer[0] = BlockEnum.Hill1_Top;
                                if (var_Block.LeftNeighbour != null)
                                {
                                    if (((Block)var_Block.LeftNeighbour).Height < var_Block.Height)
                                    {
                                        var_Block.Layer[0] = BlockEnum.Hill1_Corner1;
                                    }
                                }
                                if (var_Block.RightNeighbour != null)
                                {
                                    if (((Block)var_Block.RightNeighbour).Height < var_Block.Height)
                                    {
                                        var_Block.Layer[0] = BlockEnum.Hill1_Corner2;
                                    }
                                }
                            }
                        }

                        if (var_Block.BottomNeighbour != null)
                        {
                            if (((Block)var_Block.BottomNeighbour).Height < var_Block.Height)
                            {
                                _Chunk.getBlockAtPosition(x, y).Layer[0] = BlockEnum.Hill1_Bottom;
                                if (var_Block.LeftNeighbour != null)
                                {
                                    if (((Block)var_Block.LeftNeighbour).Height < var_Block.Height)
                                    {
                                        var_Block.Layer[0] = BlockEnum.Hill1_Corner4;
                                    }
                                }
                                if (var_Block.RightNeighbour != null)
                                {
                                    if (((Block)var_Block.RightNeighbour).Height < var_Block.Height)
                                    {
                                        var_Block.Layer[0] = BlockEnum.Hill1_Corner3;
                                    }
                                }
                            }
                        }

                        if (var_Block.Layer[0] == BlockEnum.Ground1)
                        {
                            var_Block.Layer[0] = BlockEnum.Hill1_Center;
                        }
                    }
                }
            }

            for (int x = 0; x < var_HeightMap.GetLength(0); x++)
            {
                for (int y = 0; y < var_HeightMap.GetLength(1); y++)
                {
                    if (var_HeightMap[x, y] >= 3)
                    {
                        Block var_Block = _Chunk.getBlockAtPosition(x, y);

                        if (var_Block.BottomNeighbour != null)
                        {
                            if (((Block)var_Block.BottomNeighbour).Layer[0] == BlockEnum.Hill1_Right)
                            {
                                if (var_Block.RightNeighbour != null)
                                {
                                    if (((Block)var_Block.RightNeighbour).Layer[0] == BlockEnum.Hill1_Bottom)
                                    {
                                        var_Block.Layer[0] = BlockEnum.Hill1_InsideCorner1;
                                    }
                                }
                            }
                            if (((Block)var_Block.BottomNeighbour).Layer[0] == BlockEnum.Hill1_Left)
                            {
                                if (var_Block.LeftNeighbour != null)
                                {
                                    if (((Block)var_Block.LeftNeighbour).Layer[0] == BlockEnum.Hill1_Bottom)
                                    {
                                        var_Block.Layer[0] = BlockEnum.Hill1_InsideCorner2;
                                    }
                                }
                            }
                        }

                        if (var_Block.TopNeighbour != null)
                        {
                            if (((Block)var_Block.TopNeighbour).Layer[0] == BlockEnum.Hill1_Left)
                            {
                                if (var_Block.LeftNeighbour != null)
                                {
                                    if (((Block)var_Block.LeftNeighbour).Layer[0] == BlockEnum.Hill1_Top)
                                    {
                                        var_Block.Layer[0] = BlockEnum.Hill1_InsideCorner3;
                                    }
                                }
                            }

                            if (((Block)var_Block.TopNeighbour).Layer[0] == BlockEnum.Hill1_Right)
                            {
                                if (var_Block.RightNeighbour != null)
                                {
                                    if (((Block)var_Block.RightNeighbour).Layer[0] == BlockEnum.Hill1_Top)
                                    {
                                        var_Block.Layer[0] = BlockEnum.Hill1_InsideCorner4;
                                    }
                                }
                            }
                        }
                    }
                }
            }*/

            //AUSGABE
            for (int y = 0; y < var_HeightMap.GetLength(1); y++)
            {
                for (int x = 0; x < var_HeightMap.GetLength(0); x++)
                {
                    Console.Write( (int)var_HeightMap[x, y] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
