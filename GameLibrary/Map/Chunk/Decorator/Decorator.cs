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
using GameLibrary.Enums;
#endregion

namespace GameLibrary.Map.Chunk.Decorator
{
    public class Decorator
    {
        public static Decorator decorator = new Decorator();
        private Dictionary<RegionEnum, List<DecoratorItem>> decoratorItems;

        public Decorator()
        {
            this.decoratorItems = new Dictionary<RegionEnum, List<DecoratorItem>>();

            List<DecoratorItem> var_DecoratorItems_Grassland = new List<DecoratorItem>();
            //var_DecoratorItems_Grassland.Add(new EnvironmentDecoratorItem(EnvironmentEnum.Tree_Normal_1, false, 1, 10, 0, RegionEnum.Grassland, true));
            //var_DecoratorItems_Grassland.Add(new EnvironmentDecoratorItem(EnvironmentEnum.Flower_1, true, 30, 50, 0, RegionEnum.Grassland, true));
            //var_DecoratorItems_Grassland.Add(new NpcDecoratorItem(RaceEnum.Human, 1, 2, 0, RegionEnum.Grassland, true));
            //var_DecoratorItems_Grassland.Add(new ItemDecoratorItem(ItemEnum.GoldCoin, 1, 2, 0, RegionEnum.Grassland));
            //var_DecoratorItems_Grassland.Add(new EquipmentDecoratorItem(ItemEnum.Armor, 1, 2, 0, RegionEnum.Grassland));
            //var_DecoratorItems_Grassland.Add(new EquipmentDecoratorItem(ItemEnum.Weapon, 1, 2, 0, RegionEnum.Grassland));
            this.decoratorItems.Add(RegionEnum.Grassland, var_DecoratorItems_Grassland);

            List<DecoratorItem> var_DecoratorItems_Snowland = new List<DecoratorItem>();
            var_DecoratorItems_Snowland.Add(new EnvironmentDecoratorItem(EnvironmentEnum.Tree_Normal_1, false, 1, 5, 0, RegionEnum.Snowland, true));
            var_DecoratorItems_Snowland.Add(new EnvironmentDecoratorItem(EnvironmentEnum.Flower_1, true, 10, 25, 0, RegionEnum.Snowland, true));
            var_DecoratorItems_Snowland.Add(new NpcDecoratorItem(RaceEnum.Human, 1, 2, 0, RegionEnum.Snowland, true));
            this.decoratorItems.Add(RegionEnum.Snowland, var_DecoratorItems_Snowland);

            List<DecoratorItem> var_DecoratorItems_Lavaland = new List<DecoratorItem>();
            this.decoratorItems.Add(RegionEnum.Lavaland, var_DecoratorItems_Lavaland);

            List<DecoratorItem> var_DecoratorItems_Dungeon = new List<DecoratorItem>();
            //var_DecoratorItems_Dungeon.Add(new EquipmentDecoratorItem(ItemEnum.Weapon, 2, 4, 0, RegionEnum.Dungeon));
            this.decoratorItems.Add(RegionEnum.Dungeon, var_DecoratorItems_Dungeon);
        }

        public List<DecoratorItem> getDecoratorItems(RegionEnum _RegionEnum)
        {
            return this.decoratorItems[_RegionEnum];
        }

        public void decorateChunk(Chunk _Chunk)
        {
            RegionEnum var_RegionEnum = ((Region.Region)_Chunk.Parent).RegionEnum;

            foreach (DecoratorItem var_DecoratorItem in this.getDecoratorItems(var_RegionEnum))
            {
                var_DecoratorItem.decorateChunk(_Chunk);
            }
        }
    }
}
