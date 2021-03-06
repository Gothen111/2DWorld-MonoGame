﻿#region Using Statements Standard
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
                        Dimension.Dimension var_Dimension = this.getDimensionById(_Target.DimensionId);

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
                                Block.Block var_Block = var_Dimension.getBlockAtCoordinate(var_Position);
                                if (var_Block != null)
                                {
                                    if (var_Block.IsRequested)
                                    {
                                    }
                                    else
                                    {
                                        if (Setting.Setting.drawBlocks)
                                        {
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
