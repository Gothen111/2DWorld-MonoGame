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

        private List<Region.Region> regions;

        private QuadTree<Object.Object> quadTreeObject;

        public QuadTree<Object.Object> QuadTreeObject
        {
            get { return quadTreeObject; }
            set { quadTreeObject = value; }
        }

        private List<PlayerObject> playerObjects;

        private List<Object.Object> objectsToUpdate;

        private int objectsToUpdateCounter;

        private List<Chunk.Chunk> chunksOutOfRange;

        private Block.Block[] blocksToDraw;

        private float worldTime;

        private float worldTimeMax;

        #endregion

        #region Constructors

        public World(String _Name)
        {
            this.Name = _Name;

            regions = new List<Region.Region>();

            this.playerObjects = new List<PlayerObject>();

            this.quadTreeObject = new QuadTree<Object.Object>(new Vector3(32, 32, 0), 20);

            this.objectsToUpdate = new List<Object.Object>();

            this.objectsToUpdateCounter = 0;

            this.blocksToDraw = new Block.Block[Setting.Setting.blockDrawRange * Setting.Setting.blockDrawRange];

            this.worldTimeMax = 2400;
            this.worldTime = 1300;//this.worldTimeMax;

            Logger.Logger.LogInfo("Welt " + _Name + " wurde erstellt!");
        }

        public World(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        {
            this.playerObjects = new List<PlayerObject>();
            this.regions = new List<Region.Region>();
            this.quadTreeObject = new QuadTree<Object.Object>(new Vector3(32, 32, 0), 20);
            this.objectsToUpdate = new List<Object.Object>();

            this.objectsToUpdateCounter = 0;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
        }

        #endregion      
    }
}
