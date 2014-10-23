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
using GameLibrary.Enums;
using GameLibrary.Map.Chunk.Decorator;
#endregion

namespace GameLibrary.Factory
{
    public class RegionFactory
    {
        public static RegionFactory regionFactory = new RegionFactory();

        public Region createRegion(String _Name, int _PosX, int _PosY, RegionEnum _RegionEnum, World _ParentWorld)
        {
            Region var_Region = null;
            switch (_RegionEnum)
            {
                case RegionEnum.Grassland:
                    {
                        var_Region = createRegionGrassland(_Name, _PosX, _PosY, _ParentWorld);
                        break;
                    }
                case RegionEnum.Snowland:
                    {
                        var_Region = createRegionSnowland(_Name, _PosX, _PosY, _ParentWorld);
                        break;
                    }
                case RegionEnum.Lavaland:
                    {
                        var_Region = createRegionLavaland(_Name, _PosX, _PosY, _ParentWorld);
                        break;
                    }
            }

            if(var_Region!=null)
            {
                //var_Region.Dungeons.Add(DungeonFactory.createDungeon(new Vector3(_PosX, _PosY, 0), new Vector3(Region.regionSizeX, Region.regionSizeY, 0), DungeonEnum.Room, _ParentWorld, var_Region.Dungeons.Count));
                //var_Region.Dungeons.Add(DungeonFactory.createDungeon(new Vector3(_PosX, _PosY, 0), new Vector3(Region.regionSizeX, Region.regionSizeY, 0), DungeonEnum.Cave, _ParentWorld, var_Region.Dungeons.Count));
            }

            return var_Region;
        }

        private Region createRegionGrassland(String _Name, int _PosX, int _PosY, World _ParentWorld)
        {
            Region var_Result;

            var_Result = new Region(_Name, _PosX, _PosY, new Vector3(Region.regionSizeX, Region.regionSizeY, 0),  RegionEnum.Grassland, _ParentWorld);

            //FarmFactory.farmFactory.generateFarms(var_Result, 1, 0);

            Logger.Logger.LogInfo("Region " + var_Result.Name + " wurde erstellt!");

            return var_Result;
        }

        private Region createRegionSnowland(String _Name, int _PosX, int _PosY, World _ParentWorld)
        {
            Region var_Result;

            var_Result = new Region(_Name, _PosX, _PosY, new Vector3(Region.regionSizeX, Region.regionSizeY, 0), RegionEnum.Snowland, _ParentWorld);

            Logger.Logger.LogInfo("Region " + var_Result.Name + " wurde erstellt!");

            return var_Result;
        }

        private Region createRegionLavaland(String _Name, int _PosX, int _PosY, World _ParentWorld)
        {
            Region var_Result;

            var_Result = new Region(_Name, _PosX, _PosY, new Vector3(Region.regionSizeX, Region.regionSizeY, 0), RegionEnum.Lavaland, _ParentWorld);

            Logger.Logger.LogInfo("Region " + var_Result.Name + " wurde erstellt!");

            return var_Result;
        }

        public Chunk createChunkInRegion(Region _Region, int _PosX, int _PosY)
        {
            Chunk var_Chunk = null;
            var_Chunk = ChunkFactory.chunkFactory.createChunk((int)(_PosX), (int)(_PosY), _Region.RegionEnum, RegionDependency.regionDependency.getLayer(_Region.RegionEnum), _Region);
            return var_Chunk;
        }
    }
}
