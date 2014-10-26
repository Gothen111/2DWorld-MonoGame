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
using GameLibrary.Collison;
using GameLibrary.Object;
using GameLibrary.Connection;
#endregion

namespace GameLibrary.Map.World
{
    public partial class World
    {
        #region quadTreeObject

        //TODO: Die Methode muss weg. Irwie muss man immer erst an die dimension!!!
        public Object.Object getObject(int _Id)
        {
            foreach (Dimension.Dimension var_Dimension in this.dimensions)
            {
                if (var_Dimension.getObject(_Id) != null)
                {
                    return var_Dimension.getObject(_Id);
                }
            }
            return null;
        }

        public Object.Object getObject(int _DimensionId, int _Id)
        {
            Dimension.Dimension var_Dimension = this.getDimensionById(_DimensionId);
            if (var_Dimension != null)
            {
                return var_Dimension.getObject(_Id);
            }
            return null;
        }

        public Region.Region getRegionObjectIsIn(GameLibrary.Object.Object _Object)
        {
            Dimension.Dimension var_Dimension = this.getDimensionById(_Object.DimensionId);
            if (var_Dimension != null)
            {
                return var_Dimension.getRegionObjectIsIn(_Object);
            }
            return null;
        }

        public Object.Object addObject(Object.Object _Object)
        {
            return addObject(_Object, true);
        }

        public Object.Object addObject(Object.Object _Object, Boolean insertInQuadTree)
        {
            Dimension.Dimension var_Dimension = this.getDimensionById(_Object.DimensionId);
            Region.Region var_Region = var_Dimension.getRegionObjectIsIn(_Object);
            return addObject(_Object, insertInQuadTree, var_Region);
        }

        public Object.Object addObject(Object.Object _Object, Boolean _InsertInQuadTree, Region.Region _Region)
        {
            Dimension.Dimension var_Dimension = this.getDimensionById(_Object.DimensionId);
            if (var_Dimension != null)
            {
                return var_Dimension.addObject(_Object, _InsertInQuadTree, _Region);
            }
            return null;
        }

        public Object.Object addPreEnvironmentObject(Object.Object _Object)
        {
            Dimension.Dimension var_Dimension = this.getDimensionById(_Object.DimensionId);
            Region.Region var_Region = var_Dimension.getRegionObjectIsIn(_Object);
            if (var_Dimension != null)
            {
                return var_Dimension.addPreEnvironmentObject(_Object, var_Region);
            }
            return null;
        }

        public bool removeObjectFromWorld(Object.Object _Object)
        {
            Dimension.Dimension var_Dimension = this.getDimensionById(_Object.DimensionId);
            if (var_Dimension != null)
            {
                var_Dimension.removeObjectFromWorld(_Object);
            }
            return false;
        }

        #endregion
    }
}
