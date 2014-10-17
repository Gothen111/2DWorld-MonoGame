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
using GameLibrary.Enums;
#endregion

#region Using Statements Class Specific
#endregion

namespace GameLibrary.Map.Chunk.Decorator
{
    public class DecoratorItem
    {
        private int minObjects;

        public int MinObjects
        {
            get { return minObjects; }
        }

        private int maxObjects;

        public int MaxObjects
        {
          get { return maxObjects; }
        }

        private int randomFactor;

        public int RandomFactor
        {
            get { return randomFactor; }
        }

        private RegionEnum regionEnum;

        public RegionEnum RegionEnum
        {
            get { return regionEnum; }
        }

        public DecoratorItem(int _MinObjects, int _MaxObjects, int _RandomFactor, RegionEnum _RegionEnum)
        {
            this.minObjects = _MinObjects;
            this.maxObjects = _MaxObjects;
            this.randomFactor = _RandomFactor;
            this.regionEnum = _RegionEnum;
        }

        protected int getCount()
        {
            return Utility.Random.Random.GenerateGoodRandomNumber(this.minObjects, this.maxObjects);
        }

        public virtual void decorateChunk(Chunk _Chunk)
        {
        }
    }
}
