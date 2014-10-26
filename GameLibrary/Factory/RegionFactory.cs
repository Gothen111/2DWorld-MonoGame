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
#endregion

namespace GameLibrary.Factory
{
    public class RegionFactory
    {
        public static RegionFactory regionFactory = new RegionFactory();

        public Region generateRegion(String _Name, int _PosX, int _PosY, RegionEnum _RegionEnum, Dimension _ParentDimension)
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
            }
            return null;
        }

        private Region generateRegionGrassland(String _Name, int _PosX, int _PosY, Dimension _ParentDimension)
        {
            Region var_Result;

            var_Result = new Region(_Name, _PosX, _PosY, new Vector3(Region.regionSizeX, Region.regionSizeY, 0), RegionEnum.Grassland, _ParentDimension);

            //FarmFactory.farmFactory.generateFarms(var_Result, 1, 0);

            Logger.Logger.LogInfo("Region " + var_Result.Name + " wurde erstellt!");

            return var_Result;
        }

        private Region generateRegionSnowland(String _Name, int _PosX, int _PosY, Dimension _ParentDimension)
        {
            Region var_Result;

            var_Result = new Region(_Name, _PosX, _PosY, new Vector3(Region.regionSizeX, Region.regionSizeY, 0), RegionEnum.Snowland, _ParentDimension);

            Logger.Logger.LogInfo("Region " + var_Result.Name + " wurde erstellt!");

            return var_Result;
        }

        private Region generateRegionLavaland(String _Name, int _PosX, int _PosY, Dimension _ParentDimension)
        {
            Region var_Result;

            var_Result = new Region(_Name, _PosX, _PosY, new Vector3(Region.regionSizeX, Region.regionSizeY, 0), RegionEnum.Lavaland, _ParentDimension);

            Logger.Logger.LogInfo("Region " + var_Result.Name + " wurde erstellt!");

            return var_Result;
        }

        private void addChunkToRegion(Region _Region, int _PosX, int _PosY, Chunk _ChunkToAdd)
        {
            if (_Region.setChunkAtPosition(new Vector3(_PosX, _PosY, 0), _ChunkToAdd))
            {
            }
            else
            {
                Logger.Logger.LogErr("RegionFactory->addChunkToRegion(...) : Chunk kann der Region " + _Region.Name + " nicht hinzufügt werden!");
            }
        }

        public Chunk createChunkInRegion(Region _Region, int _PosX, int _PosY)
        {
            Chunk var_Chunk = null;
            var_Chunk = ChunkFactory.chunkFactory.createChunk((int)(_PosX), (int)(_PosY), _Region.RegionEnum, RegionDependency.regionDependency.getLayer(_Region.RegionEnum), _Region);

            if (var_Chunk != null)
            {
                Decorator.decorator.decorateChunk(var_Chunk);
                this.addChunkToRegion(_Region, _PosX, _PosY, var_Chunk);

                //ChunkFactory.chunkFactory.generateChunk(var_Chunk);
            }  

            return var_Chunk;
        }
    }
}
