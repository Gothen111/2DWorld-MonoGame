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
