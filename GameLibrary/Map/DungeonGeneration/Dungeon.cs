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
using GameLibrary.Map.Region;
#endregion

#region Using Statements Class Specific
#endregion


namespace GameLibrary.Map.DungeonGeneration
{
    [Serializable()]
    public class Dungeon : Region.Region
    {
        public Dungeon(String _Name, Vector3 _Position, Vector3 _Size, RegionEnum _RegionEnum, World.World _ParentWorld)
            : base(_Name, (int)_Position.X, (int)_Position.Y, _Size, _RegionEnum, _ParentWorld)
        {
            //TODO: FILL
        }

        public Dungeon(SerializationInfo info, StreamingContext ctxt) 
            : base(info, ctxt)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
        }

        public virtual void createDungeon()
        {
            //TODO: FILL
            this.fillWith(null);
        }

        protected void fillWith(Enum _Enum)
        {
            //TODO: FILL
        }

        protected bool isPlaceFree(Vector3 _Position, Vector3 _Size)
        {
            //TODO: FILL
            return true;
        }

        protected Room.Room createRoom(Vector3 _Position, Vector3 _Size, Enum _Floor, Enum _Wall)
        {
            //TODO: FILL
            Room.Room var_Room = new Room.Room(_Position, _Size);
            return var_Room;
        }
    }
}
