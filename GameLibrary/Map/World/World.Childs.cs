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
using GameLibrary.Map.Region;
using GameLibrary.Enums;
#endregion

#region Using Statements Class Specific
#endregion

namespace GameLibrary.Map.World
{
    public partial class World
    {
        #region Region

        public Region.Region getRegion(int _Id)
        {
            foreach (Region.Region var_Region in regions)
            {
                if (var_Region.Id == _Id)
                {
                    return var_Region;
                }
            }
            return null;
        }

        public bool addRegion(Region.Region _Region)
        {
            if (!containsRegion(_Region.Id))
            {
                this.regions.Add(_Region);

                if (GameLibrary.Configuration.Configuration.isHost || GameLibrary.Configuration.Configuration.isSinglePlayer)
                {
                    this.saveRegion(_Region);
                }
                else
                {

                }

                return false;
            }
            else
            {
                Logger.Logger.LogErr("World->addRegion(...) : Region mit Id: " + _Region.Id + " schon vorhanden!");
                return false;
            }
        }

        public bool containsRegion(int _Id)
        {
            if (this.getRegion(_Id) != null)
            {
                return true;
            }
            return false;
        }

        public bool containsRegion(Region.Region _Region)
        {
            return containsRegion(_Region.Id);
        }

        public Region.Region getRegionAtPosition(Vector3 _Position)
        {
            foreach (Region.Region var_Region in this.regions)
            {
                if (var_Region.Bounds.Left <= _Position.X && var_Region.Bounds.Right >= _Position.X)
                {
                    if (var_Region.Bounds.Top <= _Position.Y && var_Region.Bounds.Bottom >= _Position.Y)
                    {
                        return var_Region;
                    }
                }
            }
            return null;
        }

        public Region.Region loadRegion(Vector3 _Position)
        {
            String var_Path = "Save/" + _Position.X + "_" + _Position.Y + "/RegionInfo.sav";
            if (System.IO.File.Exists(var_Path))
            {
                Region.Region var_Region = (Region.Region)Utility.IO.IOManager.LoadISerializeAbleObjectFromFile(var_Path);
                var_Region.Parent = this;
                return var_Region;
            }
            return null;
        }

        public void saveRegion(Region.Region _Region)
        {
            String var_Path = "Save/" + _Region.Position.X + "_" + _Region.Position.Y + "/RegionInfo.sav";
            Utility.IO.IOManager.SaveISerializeAbleObjectToFile(var_Path, _Region);
        }

        public Region.Region createRegionAt(Vector3 _Position)
        {
            _Position = Region.Region.parsePosition(_Position);

            Region.Region var_Region = this.loadRegion(_Position);
            if (var_Region == null)
            {
                int var_RegionType = 0;//Utility.Random.Random.GenerateGoodRandomNumber(0, Enum.GetValues(typeof(RegionEnum)).Length - 1);
                var_Region = GameLibrary.Factory.RegionFactory.regionFactory.createRegion("Region", (int)_Position.X, (int)_Position.Y, (RegionEnum)var_RegionType, this);
            }
            this.addRegion(var_Region);

            return var_Region;
        }

        #endregion

        #region Chunk

        public Chunk.Chunk getChunkAtPosition(Vector3 _Position)
        {
            Region.Region var_Region = this.getRegionAtPosition(_Position);
            if (var_Region != null)
            {
                return var_Region.getChunkAtPosition(_Position);
            }
            return null;
        }

        public Chunk.Chunk createChunkAt(Vector3 _Position)
        {
            Region.Region var_Region = this.getRegionAtPosition(_Position);
            if (var_Region != null)
            {
                return var_Region.createChunkAt(_Position);
            }
            return null;
        }

        public bool removeChunk(Chunk.Chunk _Chunk)
        {
            Region.Region var_Region = this.getRegionAtPosition(_Chunk.Position);
            if (var_Region != null)
            {
                return var_Region.removeChunk(_Chunk);
                //Chunk.Chunk var_Chunk = var_Region.getChunkAtPosition(_Chunk.Position);
                //return var_Region.Chunks.Remove(var_Chunk);
            }
            return false;
        }

        #endregion

        #region Block

        public Block.Block getBlockAtCoordinate(Vector3 _Position)
        {
            Region.Region var_Region = World.world.getRegionAtPosition(_Position);
            if (var_Region != null)
            {
                return var_Region.getBlockAtCoordinate(_Position);
            }
            return null;
        }

        public bool setBlockAtCoordinate(Vector3 _Position, Block.Block _Block)
        {
            Region.Region var_Region = World.world.getRegionAtPosition(_Position);
            if (var_Region != null)
            {
                return var_Region.setBlockAtCoordinate(_Position, _Block);
            }
            return false;
        }

        #endregion
    }
}
