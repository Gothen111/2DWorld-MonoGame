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
#endregion
namespace GameLibrary.Map.World
{
    [Serializable()]
    public partial class World : Box, ISerializable
    {
        #region Attribute
        public static World world;

        private List<Dimension.Dimension> dimensions;

        private List<PlayerObject> playerObjects;

        #endregion

        #region Constructors

        public World(String _Name)
        {
            this.Name = _Name;

            this.dimensions = new List<Dimension.Dimension>();

            this.getNextFreeDimension();

            this.playerObjects = new List<PlayerObject>();

            Logger.Logger.LogInfo("Welt " + _Name + " wurde erstellt!");
        }

        public World(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        {
            this.playerObjects = new List<PlayerObject>();
            this.dimensions = new List<Dimension.Dimension>();
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
        }

        #endregion      
    }
}
