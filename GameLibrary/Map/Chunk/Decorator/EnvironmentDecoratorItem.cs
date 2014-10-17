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
using GameLibrary.Enums;
using GameLibrary.Object;
using GameLibrary.Map.DungeonGeneration;
using GameLibrary.Factory;
#endregion

namespace GameLibrary.Map.Chunk.Decorator
{
    public class EnvironmentDecoratorItem : DecoratorItem
    {
        private EnvironmentEnum environmentEnum;

        private bool isPreEnvironment;

        public EnvironmentDecoratorItem(EnvironmentEnum _EnvironmentEnum, bool _IsPreEnvironment, int _MinObjects, int _MaxObjects, int _RandomFactor, RegionEnum _RegionEnum)
            : base(_MinObjects, _MaxObjects, _RandomFactor, _RegionEnum)
        {
            this.environmentEnum = _EnvironmentEnum;
            this.isPreEnvironment = _IsPreEnvironment;
        }

        public override void decorateChunk(Chunk _Chunk)
        {
            base.decorateChunk(_Chunk);
            int var_Count = this.getCount();
            for (int i = 0; i < var_Count; i++)
            {
                EnvironmentObject var_EnvironmentObject = EnvironmentFactory.environmentFactory.createEnvironmentObject(this.RegionEnum, this.environmentEnum);

                int var_X = Utility.Random.Random.GenerateGoodRandomNumber(1, (int)_Chunk.Size.X * (Block.Block.BlockSize) - 1);
                int var_Y = Utility.Random.Random.GenerateGoodRandomNumber(1, (int)_Chunk.Size.Y * (Block.Block.BlockSize) - 1);

                var_EnvironmentObject.Position = new Vector3(var_X + _Chunk.Position.X, var_Y + _Chunk.Position.Y, 0);

                Block.Block var_Block = _Chunk.getBlockAtCoordinate(var_EnvironmentObject.Position);

                if (var_Block != null)
                {
                    if (var_Block.IsWalkAble && var_Block.Layer[1] == BlockEnum.Nothing)
                    {
                        if (this.isPreEnvironment)
                        {
                            var_Block.addPreEnvironmentObject(var_EnvironmentObject);
                        }
                        else
                        {
                            var_Block.addObject(var_EnvironmentObject);
                            if (_Chunk.Parent != null)
                            {
                                if (_Chunk.Parent is Dungeon)
                                {
                                    ((Dungeon)_Chunk.Parent).QuadTreeObject.Insert(var_EnvironmentObject);
                                }
                                else
                                {
                                    ((World.World)_Chunk.Parent.Parent).QuadTreeObject.Insert(var_EnvironmentObject);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
