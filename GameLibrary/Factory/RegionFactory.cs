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
using GameLibrary.Map.Dimension;
using GameLibrary.Map.Region;
using GameLibrary.Map.Chunk;
using GameLibrary.Enums;
using GameLibrary.Map.Chunk.Decorator;
using GameLibrary.Map.Block;
using GameLibrary.Map.Dungeon;
using GameLibrary.Map.Dungeon.Dungeons;
#endregion

namespace GameLibrary.Factory
{
    public class RegionFactory
    {
        public static Region generateRegion(String _Name, int _PosX, int _PosY, RegionEnum _RegionEnum, Dimension _ParentDimension)
        {
            switch (_RegionEnum)
            {
                case RegionEnum.Grassland:
                    {
                        return generateRegionGrassland(_Name, _PosX, _PosY, _ParentDimension);
                    }
                case RegionEnum.Snowland:
                    {
                        return generateRegionSnowland(_Name, _PosX, _PosY, _ParentDimension);
                    }
                case RegionEnum.Lavaland:
                    {
                        return generateRegionLavaland(_Name, _PosX, _PosY, _ParentDimension);
                    }
                case RegionEnum.Dungeon:
                    {
                        return createDungeon(_Name, new Vector3(_PosX, _PosY, 0), new Vector3(Region.regionSizeX, Region.regionSizeY, 0), DungeonEnum.Cave, _ParentDimension);
                    }
            }
            return null;
        }

        public static Dungeon createDungeon(String _Name, Vector3 _Position, Vector3 _Size, DungeonEnum _DungeonEnum, Dimension _ParentDimension)
        {
            return generateRegionDungeon("", _Position, _Size,  _DungeonEnum, _ParentDimension);
        }

        private static Region generateRegionGrassland(String _Name, int _PosX, int _PosY, Dimension _ParentDimension)
        {
            Region var_Result;

            var_Result = new Region(_Name, _PosX, _PosY, new Vector3(Region.regionSizeX, Region.regionSizeY, 0), RegionEnum.Grassland, _ParentDimension);

            //FarmFactory.farmFactory.generateFarms(var_Result, 1, 0);

            fillWithChunks(var_Result);

            Logger.Logger.LogInfo("Region " + var_Result.Name + " wurde erstellt!");

            return var_Result;
        }

        private static Region generateRegionSnowland(String _Name, int _PosX, int _PosY, Dimension _ParentDimension)
        {
            Region var_Result;

            var_Result = new Region(_Name, _PosX, _PosY, new Vector3(Region.regionSizeX, Region.regionSizeY, 0), RegionEnum.Snowland, _ParentDimension);

            fillWithChunks(var_Result);

            Logger.Logger.LogInfo("Region " + var_Result.Name + " wurde erstellt!");

            return var_Result;
        }

        private static Region generateRegionLavaland(String _Name, int _PosX, int _PosY, Dimension _ParentDimension)
        {
            Region var_Result;

            var_Result = new Region(_Name, _PosX, _PosY, new Vector3(Region.regionSizeX, Region.regionSizeY, 0), RegionEnum.Lavaland, _ParentDimension);

            fillWithChunks(var_Result);

            Logger.Logger.LogInfo("Region " + var_Result.Name + " wurde erstellt!");

            return var_Result;
        }

        private static Dungeon generateRegionDungeon(String _Name, Vector3 _Position, Vector3 _Size, DungeonEnum _DungeonEnum, Dimension _ParentDimension)
        {
            Dungeon var_Result;

            var_Result = DungeonFactory.createDungeon(_Position, _Size, _DungeonEnum, _ParentDimension);

            fillWithChunks(var_Result);

            Logger.Logger.LogInfo("Region " + var_Result.Name + " wurde erstellt!");

            return var_Result;
        }

        private static void fillWithChunks(Region _Region)
        {
            int var_Width = (int)_Region.Size.X;
            int var_Heigth = (int)_Region.Size.Y;

            int var_SizeX = Chunk.chunkSizeX * Block.BlockSize;
            int var_SizeY = Chunk.chunkSizeY * Block.BlockSize;

            for (int x = 0; x < var_Width; x++)
            {
                for (int y = 0; y < var_Heigth; y++)
                {
                    _Region.createChunkAt(new Vector3(_Region.Position.X + var_SizeX * x, (int)_Region.Position.Y + var_SizeY * y, 0));
                }
            }
        }

        private static void addChunkToRegion(Region _Region, int _PosX, int _PosY, Chunk _ChunkToAdd)
        {
            if (_Region.setChunkAtPosition(new Vector3(_PosX, _PosY, 0), _ChunkToAdd))
            {
            }
            else
            {
                Logger.Logger.LogErr("RegionFactory->addChunkToRegion(...) : Chunk kann der Region " + _Region.Name + " nicht hinzufügt werden!");
            }
        }

        public static Chunk createChunkInRegion(Region _Region, int _PosX, int _PosY)
        {
            Chunk var_Chunk = null;
            var_Chunk = ChunkFactory.chunkFactory.createChunk((int)(_PosX), (int)(_PosY), _Region.RegionEnum, RegionDependency.regionDependency.getLayer(_Region.RegionEnum), _Region);

            if (var_Chunk != null)
            {
                addChunkToRegion(_Region, _PosX, _PosY, var_Chunk);
            }  

            return var_Chunk;
        }
    }
}
