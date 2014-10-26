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
using GameLibrary.Enums;
#endregion

namespace GameLibrary.Factory
{
    public class DungeonFactory
    {
        public static Dungeon createDungeon(Vector3 _Position, Vector3 _Size, DungeonEnum _DungeonEnum, World _ParentWorld, int _DungeonId)
        {
            Dungeon var_Dungeon = null;

            switch (_DungeonEnum)
            {
                case DungeonEnum.Cave:
                    var_Dungeon = new CaveDungeon("", _Position, _Size, RegionEnum.Dungeon, _ParentWorld, _DungeonId);
                    break;
                case DungeonEnum.Room:
                    var_Dungeon = new RoomDungeon("", _Position, _Size, RegionEnum.Dungeon, _ParentWorld, _DungeonId);
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
