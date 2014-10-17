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
    public class EnvironmentDecoratorItem : DecoratorItem
    {
        private EnvironmentEnum environmentEnum;
        public EnvironmentDecoratorItem(EnvironmentEnum _EnvironmentEnum, int _MinObjects, int _MaxObjects, int _RandomFactor, RegionEnum _RegionEnum)
            : base(_MinObjects, _MaxObjects, _RandomFactor, _RegionEnum)
        {
            this.environmentEnum = _EnvironmentEnum;
        }

        public override void decorateChunk(Chunk _Chunk)
        {
            //TODO: Füge CHunk Objectk hinzu. Dafür muss man aber noch anquadtree uso .. da nochw as überlegen. ob nach chunk  dfecoate alle objete in qudtree kommen ..
            base.decorateChunk(_Chunk);
            int var_Count = this.getCount();
            for (int i = 0; i < var_Count; i++)
            {
                Factory.EnvironmentFactory.environmentFactory.createEnvironmentObject(this.RegionEnum, this.environmentEnum);
            }
        }
    }
}
