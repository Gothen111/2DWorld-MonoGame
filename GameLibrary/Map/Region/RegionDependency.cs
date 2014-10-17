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

namespace GameLibrary.Map.Region
{
    public class RegionDependency
    {
        public static RegionDependency regionDependency = new RegionDependency();
        private Dictionary<RegionEnum, List<Enum>> layer;

        private RegionDependency()
        {
            this.layer = new Dictionary<RegionEnum, List<Enum>>();

            List<Enum> var_Layer_Grassland = new List<Enum>();
            var_Layer_Grassland.Add(BlockEnum.Ground1);
            var_Layer_Grassland.Add(BlockEnum.Ground2);
            this.layer.Add(RegionEnum.Grassland, var_Layer_Grassland);

            List<Enum> var_Layer_Snowland = new List<Enum>();
            var_Layer_Snowland.Add(BlockEnum.Ground1);
            var_Layer_Snowland.Add(BlockEnum.Ground2);
            this.layer.Add(RegionEnum.Snowland, var_Layer_Snowland);

            List<Enum> var_Layer_Lavaland = new List<Enum>();
            var_Layer_Lavaland.Add(BlockEnum.Ground1);
            var_Layer_Lavaland.Add(BlockEnum.Ground2);
            this.layer.Add(RegionEnum.Lavaland, var_Layer_Lavaland);

            List<Enum> var_Layer_Dungeon = new List<Enum>();
            this.layer.Add(RegionEnum.Dungeon, var_Layer_Dungeon);
        }

        public List<Enum> getLayer(RegionEnum _RegionEnum)
        {
            return layer[_RegionEnum];
        }
    }
}
