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

namespace GameLibrary.Map.Region.Regions
{
    public class Grassland : Region
    {
        public Grassland(String _Name, Vector3 _Position, Vector3 _Size, RegionEnum _RegionEnum, Dimension.Dimension _ParentDimension)
            : base(_Name, (int)_Position.X, (int)_Position.Y, _Size, _RegionEnum, _ParentDimension)
        {

        }
    }
}
