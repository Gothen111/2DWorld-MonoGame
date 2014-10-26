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

            if (this.worldTime <= 0)
            {
                this.worldTime = this.worldTimeMax;
            }
            else
            {
                this.worldTime -= 1;
            }

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

        bool test = true;

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

            if (_PlayerObject != null)// && _NewUpdateObjectsList)
            {
                Vector3 var_PlayerPos = _PlayerObject.Position;

                if (_PlayerObject.CurrentBlock != null)
                {
                    var_PlayerPos = _PlayerObject.CurrentBlock.Position;
                }
                    int var_DrawSizeX = Setting.Setting.blockDrawRange;
                    int var_DrawSizeY = Setting.Setting.blockDrawRange;

                    this.blocksToDraw = new Block.Block[var_DrawSizeX * var_DrawSizeY];

                    for (int x = 0; x < var_DrawSizeX; x++)
                    {
                        for (int y = 0; y < var_DrawSizeY; y++)
                        {
                            Vector3 var_Position = new Vector3(var_PlayerPos.X + (-var_DrawSizeX / 2 + x) * Block.Block.BlockSize, var_PlayerPos.Y + (-var_DrawSizeY / 2 + y) * Block.Block.BlockSize, 0);
                            Block.Block var_Block = null;
                            
                            if(_PlayerObject.IsInDungeon)
                            {
                                if (_PlayerObject.getRegionIsIn() != null)
                                {
                                    var_Block = _PlayerObject.getRegionIsIn().getBlockAtCoordinate(var_Position);
                                }
                            }
                            else
                            {
                                var_Block = this.getBlockAtCoordinate(var_Position);
                            }


                            if (var_Block != null)
                            {
                                int var_ArrayPos = x + y * var_DrawSizeX;
                                this.blocksToDraw[var_ArrayPos] = var_Block;
                                var_Block.LightColor = Color.White;
                                if (Setting.Setting.light)
                                {
                                    if (Setting.Setting.lightOne)
                                    {
                                        if (var_Block.Position == Block.Block.parsePosition(_PlayerObject.Position))
                                        {
                                            var_Block.LightLevel = 1.1f;
                                            var_Block.LightColor = Color.Red;
                                        }
                                        else if (var_Block.Position == Block.Block.parsePosition(new Vector3(0, 0, 0)))
                                        {
                                            var_Block.LightLevel = 1.1f;
                                            var_Block.LightColor = Color.Orange;
                                        }
                                        else
                                        {
                                            var_Block.LightAdmit = 0.01f;
                                            var_Block.LightLevel = var_Block.LightAdmit;
                                            if (var_Block.Layer[0] == Enums.BlockEnum.Ground2)
                                            {
                                                var_Block.LightAbsorb = 0.2f;
                                            }
                                            else
                                            {
                                                var_Block.LightAbsorb = 0.0f;
                                            }
                                        }
                                    }
                                    if (Setting.Setting.lightTwo)
                                    {
                                        if (var_Block.Layer[0] == Enums.BlockEnum.Ground2)
                                        {
                                            var_Block.LightAbsorb = 0.2f;
                                        }
                                        else
                                        {
                                            var_Block.LightAbsorb = 0.0f;
                                        }
                                    }
                                }
                                else
                                {
                                    var_Block.LightLevel = 1.0f;
                                }
                                //var_Block.update(_GameTime);
                            }
                            else
                            {
                                if (_NewUpdateObjectsList)
                                {
                                    Region.Region var_Region = null;
                                    var_Region = this.getRegionAtPosition(var_Position);

                                    if (_PlayerObject.IsInDungeon)
                                    {
                                        //if (var_Region != null)
                                        //{
                                        var_Region = _PlayerObject.getRegionIsIn();//var_Region.Dungeons[_PlayerObject.DungeonId];
                                        //}
                                    }

                                    if (var_Region == null)
                                    {
                                        if (!_PlayerObject.IsInDungeon)
                                        {
                                            var_Region = this.createRegionAt(var_Position);
                                        }
                                        else if (_PlayerObject.IsInDungeon && _PlayerObject.getRegionIsIn() == null)
                                        {
                                            if (Region.Region.parsePosition(var_Position).Equals(Region.Region.parsePosition(_PlayerObject.Position)))
                                            {
                                                var_Region = this.createRegionAt(var_Position);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (!_PlayerObject.IsInDungeon)
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
                        }
                    }
            }

            
            if (_NewUpdateObjectsList)
            {
                List<Object.Object> var_Objects = this.getObjectsInRange(_PlayerObject.Position, _PlayerObject.getQuadTreeIsIn().Root, 400);
                foreach (Object.Object var_Object in var_Objects)
                {
                    if (!this.objectsToUpdate.Contains(var_Object))
                    {
                        this.objectsToUpdate.Add(var_Object);
                    }
                }
            }

            if (Setting.Setting.light)
            {
                if (Setting.Setting.lightOne)
                {
                    for (int i = 0; i < this.blocksToDraw.Length; i++)
                    {
                        if (this.blocksToDraw[i] != null)
                        {
                            this.blocksToDraw[i].calculateLightLevelLeft();
                        }
                    }
                    for (int i = this.blocksToDraw.Length - 1; i >= 0; i--)
                    {
                        if (this.blocksToDraw[i] != null)
                        {
                            this.blocksToDraw[i].calculateLightLevelRight();
                        }
                    }
                    for (int i = 0; i < this.blocksToDraw.Length; i++)
                    {
                        if (this.blocksToDraw[i] != null)
                        {
                            this.blocksToDraw[i].calculateLightLevelTop();
                        }
                    }
                    for (int i = this.blocksToDraw.Length - 1; i >= 0; i--)
                    {
                        if (this.blocksToDraw[i] != null)
                        {
                            this.blocksToDraw[i].calculateLightLevelBottom();
                        }
                    }

                    if (Setting.Setting.goodLight)
                    {

                        for (int i = 0; i < this.blocksToDraw.Length; i++)
                        {
                            if (this.blocksToDraw[i] != null)
                            {
                                this.blocksToDraw[i].calculateLightLevel();
                            }
                        }

                        for (int i = 0; i < this.blocksToDraw.Length; i++)
                        {
                            if (this.blocksToDraw[i] != null)
                            {
                                this.blocksToDraw[i].calculateLightLevelLeft();
                            }
                        }
                        for (int i = this.blocksToDraw.Length - 1; i >= 0; i--)
                        {
                            if (this.blocksToDraw[i] != null)
                            {
                                this.blocksToDraw[i].calculateLightLevelRight();
                            }
                        }
                        for (int i = 0; i < this.blocksToDraw.Length; i++)
                        {
                            if (this.blocksToDraw[i] != null)
                            {
                                this.blocksToDraw[i].calculateLightLevelTop();
                            }
                        }
                        for (int i = this.blocksToDraw.Length - 1; i >= 0; i--)
                        {
                            if (this.blocksToDraw[i] != null)
                            {
                                this.blocksToDraw[i].calculateLightLevelBottom();
                            }
                        }



                        /*for (int i = 0; i < this.blocksToDraw.Length; i++)
                        {
                            if (this.blocksToDraw[i] != null)
                            {
                                if (this.blocksToDraw[i].LightLevel >= 0.1f)
                                {
                                    this.blocksToDraw[i].LightLevel = Math.Min(1, this.blocksToDraw[i].LightLevel * 2);
                                }
                            }
                        }*/
                    }
                }
                if (Setting.Setting.lightTwo)
                {
                    List<Utility.Object.Light> var_Lights = new List<Utility.Object.Light>();

                    float varAmbientLight = Math.Abs(this.worldTime - this.worldTimeMax / 2) / (this.worldTimeMax / 2);
                    //Console.WriteLine(varAmbientLight);
                    //var_Lights.Add(new Utility.Object.Light(_PlayerObject.Position, Color.Red, 320));
                    //var_Lights.Add(new Utility.Object.Light(new Vector3(0,0,0), Color.Blue, 320));

                    for (int i = 0; i < this.blocksToDraw.Length; i++)
                    {
                        if (this.blocksToDraw[i] != null)
                        {
                            this.blocksToDraw[i].NextLightLevel = varAmbientLight;//0.0f;
                            for(int z = 0; z < this.blocksToDraw[i].Objects.Count; z++)
                            {
                                this.blocksToDraw[i].Objects[z].LightLevel = this.blocksToDraw[i].LightLevel;
                                if (this.blocksToDraw[i].Objects[z] is CreatureObject)
                                {
                                    if (this.blocksToDraw[i].Objects[z] is PlayerObject)
                                    {
                                        var_Lights.Add(new Utility.Object.Light(this.blocksToDraw[i].Objects[z].Position, Color.White, 320));
                                    }
                                    else
                                    {
                                        var_Lights.Add(new Utility.Object.Light(this.blocksToDraw[i].Objects[z].Position, Color.White, 200));
                                    }
                                }
                            }

                            for (int z = 0; z < this.blocksToDraw[i].ObjectsPreEnviorment.Count; z++)
                            {
                                this.blocksToDraw[i].ObjectsPreEnviorment[z].LightLevel = this.blocksToDraw[i].LightLevel;
                                if (this.blocksToDraw[i].ObjectsPreEnviorment[z] is CreatureObject)
                                {
                                    var_Lights.Add(new Utility.Object.Light(this.blocksToDraw[i].ObjectsPreEnviorment[z].Position, Color.Red, 200));
                                }
                            }
                        }
                    }

                    for (int v = 0; v < var_Lights.Count; v++)
                    {
                        for (int i = 0; i < this.blocksToDraw.Length; i++)
                        {
                            if (this.blocksToDraw[i] != null)
                            {
                                float var_Distance = Vector3.Distance(var_Lights[v].Position, this.blocksToDraw[i].Position);
                                float var_MaxDistance = var_Lights[v].LightRange;
                                if (var_Distance >= var_MaxDistance)
                                {
                                }
                                else
                                {
                                    //this.blocksToDraw[i].LightLevel = (1 - (var_Distance / var_MaxDistance));
                                    //this.blocksToDraw[i].NextLightLevel = (1 - (var_Distance / var_MaxDistance));
                                    //Console.WriteLine(this.countAbsorb(var_LightPosition, this.blocksToDraw[i].Position, i % Setting.Setting.blockDrawRange, i/Setting.Setting.blockDrawRange, Setting.Setting.blockDrawRange));
                                    
                                    float var_NextLight = Math.Max(0, (1 - (var_Distance / var_MaxDistance)) - this.countAbsorb(var_Lights[v].Position, this.blocksToDraw[i].Position, i % Setting.Setting.blockDrawRange, i / Setting.Setting.blockDrawRange, Setting.Setting.blockDrawRange));
                                    float var_CurrentLight = this.blocksToDraw[i].LightLevel;
                                    this.blocksToDraw[i].NextLightLevel = Math.Min(1, this.blocksToDraw[i].NextLightLevel + var_NextLight);
                                    //this.blocksToDraw[i].LightColor = Color.Lerp(this.blocksToDraw[i].LightColor * var_CurrentLight, var_Lights[v].LightColor * var_NextLight, 0.5f);
                                }
                            }
                        }
                    }

                    for (int i = 0; i < this.blocksToDraw.Length; i++)
                    {
                        if (this.blocksToDraw[i] != null)
                        {
                            this.blocksToDraw[i].update(_GameTime);
                        }
                    }
                }
            }
        }

        private float countAbsorb(Vector3 _LightPosition, Vector3 _BlockPosition, int _X, int _Y, int _Size)
        {
            float var_Result = this.blocksToDraw[_X + _Y * _Size].LightAbsorb;

            _LightPosition = Block.Block.parsePosition(_LightPosition) / Block.Block.BlockSize;
            _BlockPosition = Block.Block.parsePosition(_BlockPosition) / Block.Block.BlockSize;

            while(_BlockPosition != _LightPosition)
            {
                if (_BlockPosition.X < _LightPosition.X)
                {
                    _BlockPosition.X += 1;
                    _X += 1;
                    int i = (_X) + (_Y)*_Size;
                    if (i >= 0 && this.blocksToDraw[i] != null)
                    {
                        var_Result += this.blocksToDraw[i].LightAbsorb;
                    }
                }
                if (_BlockPosition.X > _LightPosition.X)
                {
                    _BlockPosition.X -= 1;
                    _X -= 1;
                    int i = (_X) + (_Y) * _Size;
                    if (i >= 0 && this.blocksToDraw[i] != null)
                    {
                        var_Result += this.blocksToDraw[i].LightAbsorb;
                    }
                }
                if (_BlockPosition.Y < _LightPosition.Y)
                {
                    _BlockPosition.Y += 1;
                    _Y += 1;
                    int i = (_X) + (_Y) * _Size;
                    if (i >= 0 && this.blocksToDraw[i] != null)
                    {
                        var_Result += this.blocksToDraw[i].LightAbsorb;
                    }
                }
                if (_BlockPosition.Y > _LightPosition.Y)
                {
                    _BlockPosition.Y -= 1;
                    _Y -= 1;
                    int i = (_X) + (_Y) * _Size;
                    if (i >= 0 && this.blocksToDraw[i] != null)
                    {
                        var_Result += this.blocksToDraw[i].LightAbsorb;
                    }
                }
            }

            return var_Result;
        }
        #endregion
    }
}
