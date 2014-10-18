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
    public class NpcDecoratorItem : DecoratorItem
    {
        private RaceEnum raceEnum; 
        private FactionEnum factionEnum; 
        private CreatureEnum creatureEnum;
        private GenderEnum genderEnum;

        public NpcDecoratorItem(RaceEnum _RaceEnum, int _MinObjects, int _MaxObjects, int _RandomFactor, RegionEnum _RegionEnum)
            : base(_MinObjects, _MaxObjects, _RandomFactor, _RegionEnum)
        {
            this.raceEnum = _RaceEnum;
        }

        public override void decorateChunk(Chunk _Chunk)
        {
            base.decorateChunk(_Chunk);
            int var_Count = this.getCount();
            for (int i = 0; i < var_Count; i++)
            {
                NpcObject var_NpcObject = CreatureFactory.creatureFactory.createNpcObject(this.raceEnum, FactionEnum.Beerdrinker, CreatureEnum.Archer, GenderEnum.Male);

                int var_X = Utility.Random.Random.GenerateGoodRandomNumber(1, (int)_Chunk.Size.X * (Block.Block.BlockSize) - 1);
                int var_Y = Utility.Random.Random.GenerateGoodRandomNumber(1, (int)_Chunk.Size.Y * (Block.Block.BlockSize) - 1);

                var_NpcObject.Position = new Vector3(var_X + _Chunk.Position.X, var_Y + _Chunk.Position.Y, 0);

                Block.Block var_Block = _Chunk.getBlockAtCoordinate(var_NpcObject.Position);

                if (var_Block != null)
                {
                    if (var_Block.IsWalkAble && var_Block.Layer[1] == BlockEnum.Nothing)
                    {
                        var_Block.addObject(var_NpcObject);
                        if (_Chunk.Parent != null)
                        {
                            if (_Chunk.Parent is Dungeon)
                            {
                                ((Dungeon)_Chunk.Parent).QuadTreeObject.Insert(var_NpcObject);
                            }
                            else
                            {
                                ((World.World)_Chunk.Parent.Parent).QuadTreeObject.Insert(var_NpcObject);
                            }
                        }
                    }
                }
            }
        }
    }
}