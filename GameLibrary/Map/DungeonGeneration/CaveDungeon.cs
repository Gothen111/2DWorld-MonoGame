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
#endregion

namespace GameLibrary.Map.DungeonGeneration
{
    [Serializable()]
    public class CaveDungeon : Dungeon
    {
        public CaveDungeon(String _Name, Vector3 _Position, Vector3 _Size, RegionEnum _RegionEnum, World.World _ParentWorld)
            : base(_Name, _Position, _Size, _RegionEnum, _ParentWorld)
        {
        }

        public CaveDungeon(SerializationInfo info, StreamingContext ctxt) 
            : base(info, ctxt)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
        }
    }
}
