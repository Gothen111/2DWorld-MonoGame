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
using GameLibrary.Factory;
#endregion

namespace GameLibrary.Map.Chunk.Decorator
{
    public class ItemDecoratorItem : DecoratorItem
    {
        private ItemEnum itemEnum;

        public ItemDecoratorItem(ItemEnum _ItemEnum, int _MinObjects, int _MaxObjects, int _RandomFactor, RegionEnum _RegionEnum, bool _OnlyHost)
            : base(_MinObjects, _MaxObjects, _RandomFactor, _RegionEnum, _OnlyHost)
        {
            this.itemEnum = _ItemEnum;
        }

        public override void onDecorateChunk(Chunk _Chunk)
        {
            base.onDecorateChunk(_Chunk);
            int var_Count = this.getCount();
            for (int i = 0; i < var_Count; i++)
            {
                ItemObject var_itemObject = ItemFactory.itemFactory.createItemObject(this.itemEnum);

                int var_X = Utility.Random.Random.GenerateGoodRandomNumber(1, (int)_Chunk.Size.X * (Block.Block.BlockSize) - 1);
                int var_Y = Utility.Random.Random.GenerateGoodRandomNumber(1, (int)_Chunk.Size.Y * (Block.Block.BlockSize) - 1);

                var_itemObject.Position = new Vector3(var_X + _Chunk.Position.X, var_Y + _Chunk.Position.Y, 0);
                var_itemObject.NextPosition = var_itemObject.Position;

                Block.Block var_Block = _Chunk.getBlockAtCoordinate(var_itemObject.Position);

                if (var_Block != null)
                {
                    if (var_Block.IsWalkAble && var_Block.Layer[1] == BlockEnum.Nothing)
                    {
                        if (_Chunk.Parent != null)
                        {
                            if (_Chunk.Parent != null)
                            {
                                //((Region.Region)_Chunk.Parent).getParent().addObject(var_itemObject, true, (Region.Region)_Chunk.Parent);
                            }
                            var_Block.addObject(var_itemObject);
                        }
                    }
                }
            }
        }
    }
}
