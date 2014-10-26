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
using GameLibrary.Factory.FactoryEnums;
#endregion

namespace GameLibrary.Factory
{
    public class ItemFactory
    {
        public static ItemFactory itemFactory = new ItemFactory();

        private ItemFactory()
        {
        }

        public ItemObject createItemObject(ItemEnum _ItemEnum)
        {
            ItemObject var_ItemObject = new ItemObject();
            var_ItemObject.ItemEnum = _ItemEnum;
            var_ItemObject.Scale = 1;
            var_ItemObject.Velocity = new Vector3(0, 0, 0);         

            switch (_ItemEnum)
            {
                case ItemEnum.GoldCoin:
                    {
                        var_ItemObject.Body.MainBody.TexturePath = "Character/GoldCoin";
                        var_ItemObject.ItemIconGraphicPath = "Character/GoldCoin";
                        var_ItemObject.Size = new Microsoft.Xna.Framework.Vector3(16, 16, 0);
                        var_ItemObject.StackMax = 5;
                        var_ItemObject.OnlyFromPlayerTakeAble = true;
                        break;
                    }
            }
            return var_ItemObject;
        }
    }
}
