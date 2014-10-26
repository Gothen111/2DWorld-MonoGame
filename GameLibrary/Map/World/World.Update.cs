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
using GameLibrary.Object;
#endregion

#region Using Statements Class Specific
#endregion

namespace GameLibrary.Map.World
{
    public partial class World
    {
        #region update-Methoden

        public override void update(GameTime _GameTime)
        {
            base.update(_GameTime);

            bool var_NewUpdateObjectsList = false;

            if (this.objectsToUpdateCounter <= 0)
            {
                this.objectsToUpdate = new List<Object.Object>();
                var_NewUpdateObjectsList = true;
                this.objectsToUpdateCounter = 50;
            }
            else
            {
                this.objectsToUpdateCounter -= 1;
            }

            this.chunksOutOfRange = new List<Chunk.Chunk>();
            foreach (Region.Region var_Region in this.regions)
            {
                foreach (Chunk.Chunk var_Chunk in var_Region.Chunks)
                {
                    if (var_Chunk != null)
                    {
                        var_Chunk.update(_GameTime);
                        this.chunksOutOfRange.Add(var_Chunk);
                    }
                }
            }

            int var_SizeBefore = this.chunksOutOfRange.Count;

            this.updatePlayerObjectsNeighborhood(_GameTime, var_NewUpdateObjectsList);

            int var_SizeAfter = this.chunksOutOfRange.Count;
            //Console.Write("Chunks: ");
            //Console.WriteLine(var_SizeBefore - var_SizeAfter);
            /*Console.WriteLine("------------------------");
            for (int y = -7; y <= 7; y++)
            {
                for (int x = -7; x <= 7; x++)
                {
                    if (this.getRegionAtPosition(x * 320 * 2, y * 320 * 2) != null)
                    {
                        Console.Write("X");
                    }
                    else
                    {
                        Console.Write("0");
                    }
                }
                Console.WriteLine();
            }*/

            foreach(Region.Region var_Region in this.regions)
            {
                var_Region.update(_GameTime);
            }

            foreach (Chunk.Chunk var_Chunk in this.chunksOutOfRange)
            {
                foreach(Object.Object var_Object in var_Chunk.getAllObjectsInChunk())
                {
                    this.quadTreeObject.Remove(var_Object);
                }
                this.removeChunk(var_Chunk);
            }

            //Console.WriteLine(this.quadTreeObject.GetQuadObjectCount());




            //TODO: Mache Kopie der Liste!!!! Falls objekte steben usw ;)
            try
            {
                foreach (Object.Object var_Object in this.objectsToUpdate)
                {
                    var_Object.update(_GameTime);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }


        private void updatePlayerObjectsNeighborhood(GameTime _GameTime, bool _NewUpdateObjectsList)
        {
            List<PlayerObject> var_Copy = new List<PlayerObject>(this.playerObjects);
            foreach (Object.PlayerObject var_PlayerObject in var_Copy)
            {
                this.updatePlayerObjectNeighborhood(_GameTime, var_PlayerObject, _NewUpdateObjectsList);
            }
        }

        private void createPlayerObjectNeighbourChunk(Vector3 _NewChunkPosition)
        {
            Chunk.Chunk var_Chunk = this.getChunkAtPosition(_NewChunkPosition);
            if (var_Chunk == null)
            {
                if (Configuration.Configuration.isHost)
                {
                    Region.Region var_Region = World.world.getRegionAtPosition(_NewChunkPosition)
                                             ?? World.world.createRegionAt(_NewChunkPosition);
                    if (var_Region != null)
                    {
                        var_Chunk = var_Region.getChunkAtPosition(_NewChunkPosition)
                                    ?? var_Region.createChunkAt(_NewChunkPosition);
                    }
                }
                else
                {
                }
            }
        }

        private void updatePlayerObjectNeighborhood(GameTime _GameTime, Object.PlayerObject _PlayerObject, bool _NewUpdateObjectsList)
        {
            List<Chunk.Chunk> var_ChunksToRemove = new List<Chunk.Chunk>();
            foreach (Chunk.Chunk var_Chunk in this.chunksOutOfRange)
            {
                if (Vector3.Distance(var_Chunk.Position, new Vector3(_PlayerObject.Position.X, _PlayerObject.Position.Y, 0)) <= (Setting.Setting.blockDrawRange * Block.Block.BlockSize))//(Setting.Setting.blockDrawRange * 4 * Block.Block.BlockSize))//Chunk.Chunk.chunkSizeX * Block.Block.BlockSize * 3)
                {
                    var_ChunksToRemove.Add(var_Chunk);
                }
            }

            foreach (Chunk.Chunk var_Chunk in var_ChunksToRemove)
            {
                this.chunksOutOfRange.Remove(var_Chunk);
            }

            if (_PlayerObject != null && _NewUpdateObjectsList)
            {
                Vector3 var_PlayerPos = _PlayerObject.Position;

                if (_PlayerObject.CurrentBlock != null)
                {
                    var_PlayerPos = _PlayerObject.CurrentBlock.Position;
                }
                    int var_DrawSizeX = Setting.Setting.blockDrawRange;
                    int var_DrawSizeY = Setting.Setting.blockDrawRange;

                    for (int x = 0; x < var_DrawSizeX; x++)
                    {
                        for (int y = 0; y < var_DrawSizeY; y++)
                        {
                            Vector3 var_Position = new Vector3(var_PlayerPos.X + (-var_DrawSizeX / 2 + x) * Block.Block.BlockSize, var_PlayerPos.Y + (-var_DrawSizeY / 2 + y) * Block.Block.BlockSize, 0);
                            Block.Block var_Block = this.getBlockAtCoordinate(var_Position);
                            if (var_Block != null)
                            {
                                var_Block.update(_GameTime);
                            }
                            else
                            {
                                Region.Region var_Region = this.getRegionAtPosition(var_Position);
                                if (var_Region == null)
                                {
                                    var_Region = this.createRegionAt(var_Position);
                                }
                                else
                                {
                                    Chunk.Chunk var_Chunk = this.getChunkAtPosition(var_Position);
                                    if (var_Chunk == null)
                                    {
                                        var_Chunk = this.createChunkAt(var_Position);
                                    }
                                }
                            }
                        }
                    }
                //}
            }

            if (_NewUpdateObjectsList)
            {
                List<Object.Object> var_Objects = this.getObjectsInRange(_PlayerObject.Position, 400);
                foreach (Object.Object var_Object in var_Objects)
                {
                    if (!this.objectsToUpdate.Contains(var_Object))
                    {
                        this.objectsToUpdate.Add(var_Object);
                    }
                }
            }
        }
        #endregion
    }
}
