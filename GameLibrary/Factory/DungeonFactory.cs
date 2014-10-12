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
using GameLibrary.Map.Region;
using GameLibrary.Map.DungeonGeneration;
using GameLibrary.Map.World;
#endregion

namespace GameLibrary.Factory
{
    public enum DungeonType
    {
        Cave
    }

    public class DungeonFactory
    {
        public static Region createDungeon(Vector3 _Position, Vector3 _Size, DungeonType _DungeonType, World _ParentWorld)
        {
            Dungeon var_Dungeon = null;

            switch (_DungeonType)
            {
                case DungeonType.Cave:
                    var_Dungeon = new CaveDungeon("", _Position, _Size, RegionEnum.Grassland, _ParentWorld);
                    break;
            }
            if (var_Dungeon != null)
            {
                var_Dungeon.createDungeon();
                return var_Dungeon;
            }
            return null;
        }
    }
}
