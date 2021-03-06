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
using GameLibrary.Map.Region;
#endregion

#region Using Statements Class Specific
#endregion

namespace GameLibrary.Map.World
{
    public partial class World
    {
        #region Dimension

        public Dimension.Dimension getDimensionById(int _DimensionId)
        {
            foreach (Dimension.Dimension var_Dimension in this.dimensions)
            {
                if (var_Dimension.Id == _DimensionId)
                {
                    return var_Dimension;
                }
            }

            return this.createDimension(_DimensionId);
        }

        public Dimension.Dimension createDimension()
        {
            Dimension.Dimension var_NewDimension = new Dimension.Dimension(this);
            this.dimensions.Add(var_NewDimension);

            return var_NewDimension;
        }

        public Dimension.Dimension createDimension(int _Id)
        {
            Dimension.Dimension var_NewDimension = new Dimension.Dimension(_Id, this);
            this.dimensions.Add(var_NewDimension);

            return var_NewDimension;
        }

        public Dimension.Dimension getNextFreeDimension()
        {
            return this.createDimension();
        }

        #endregion
    }
}
