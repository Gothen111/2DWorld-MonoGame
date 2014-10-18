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
using GameLibrary.Object;
#endregion

namespace GameLibrary.Map.World
{
    public partial class World
    {
        #region drawing
        public void draw(GraphicsDevice _GraphicsDevice, SpriteBatch _SpriteBatch, LivingObject _Target) // LIVINGOBJEKT
        {
            if (Setting.Setting.drawWorld)
            {
                if (_Target != null)
                {
                    if (_Target.CurrentBlock != null)
                    {
                        int var_DrawSizeX = Setting.Setting.blockDrawRange;
                        int var_DrawSizeY = Setting.Setting.blockDrawRange;

                        List<Object.Object> var_PreEnviornmentObjectsToDraw = new List<Object.Object>();
                        List<Object.Object> var_ObjectsToDraw = new List<Object.Object>();

                        for (int x = 0; x < var_DrawSizeX; x++)
                        {
                            for (int y = 0; y < var_DrawSizeY; y++)
                            {
                                Vector3 var_Position = new Vector3(_Target.CurrentBlock.Position.X + (-var_DrawSizeX / 2 + x) * Block.Block.BlockSize, _Target.CurrentBlock.Position.Y + (-var_DrawSizeY / 2 + y) * Block.Block.BlockSize, 0);
                                if(var_Position.X>0 && var_Position.Y > 0)
                                {

                                }
                                Block.Block var_Block = null;

                                if (_Target.IsInDungeon)
                                {
                                    if (_Target.getRegionIsIn() != null)
                                    {
                                        var_Block = _Target.getRegionIsIn().getBlockAtCoordinate(var_Position);
                                    }
                                }
                                else
                                {
                                    var_Block = this.getBlockAtCoordinate(var_Position);
                                }
                                if (var_Block != null)
                                {
                                    if (var_Block.IsRequested)
                                    {
                                    }
                                    else
                                    {
                                        if (Setting.Setting.drawBlocks)
                                        {
                                            /*float var_Distance = Vector3.Distance(_Target.Position, var_Block.Position);
                                            int var_MaxDistance = 320;
                                            if (var_Distance >= var_MaxDistance)
                                            {
                                                var_Block.LightLevel = 0.0f;
                                            }
                                            else if (var_Block.Layer[0] == Enums.BlockEnum.Ground2)
                                            {
                                                var_Block.LightLevel = (1 - (var_Distance / (var_MaxDistance / 4)));
                                            }
                                            else
                                            {
                                                var_Block.LightLevel = (1 - (var_Distance / var_MaxDistance));
                                            }*/
                                            var_Block.drawBlock(_GraphicsDevice, _SpriteBatch);
                                        }
                                        var_PreEnviornmentObjectsToDraw.AddRange(var_Block.ObjectsPreEnviorment);
                                        var_ObjectsToDraw.AddRange(var_Block.Objects);
                                    }
                                }
                                else
                                {
                                }
                            }
                        }
                        if (Setting.Setting.drawPreEnvironmentObjects)
                        {
                            var_PreEnviornmentObjectsToDraw.Sort(new Ressourcen.ObjectPositionComparer());
                            foreach (AnimatedObject var_AnimatedObject in var_PreEnviornmentObjectsToDraw)
                            {
                                var_AnimatedObject.draw(_GraphicsDevice, _SpriteBatch, Vector3.Zero, Color.White);
                            }
                        }
                        if (Setting.Setting.drawObjects)
                        {
                            var_ObjectsToDraw.Sort(new Ressourcen.ObjectPositionComparer());
                            foreach (AnimatedObject var_AnimatedObject in var_ObjectsToDraw)
                            {
                                var_AnimatedObject.draw(_GraphicsDevice, _SpriteBatch, Vector3.Zero, Color.White);
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }
}
