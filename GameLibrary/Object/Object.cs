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
using Utility.Corpus;
using GameLibrary.Map.World;
using GameLibrary.Collison;
using GameLibrary.Map.DungeonGeneration;
using GameLibrary.Map.Region;
using GameLibrary.Map.Block;
#endregion

#region Using Statements Class Specific
#endregion

namespace GameLibrary.Object
{
    [Serializable()]
    public class Object : WorldElement
    {
        public static int _id = 0;
        private int id = _id++;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private List<Rectangle> collisionBounds;

        public List<Rectangle> CollisionBounds
        {
            get { return collisionBounds; }
            set { collisionBounds = value; }
        }

        private Map.Block.Block currentBlock;

        public Map.Block.Block CurrentBlock
        {
            get { return currentBlock; }
            set { currentBlock = value; }
        }

        private Vector3 velocity;

        public Vector3 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        private bool isInDungeon;

        public bool IsInDungeon
        {
            get { return isInDungeon; }
            set { isInDungeon = value; }
        }

        private int dungeonId;

        public int DungeonId
        {
            get { return dungeonId; }
            set { dungeonId = value; }
        }

        public Object()
        {
            this.collisionBounds = new List<Rectangle>();
            this.isInDungeon = false;
        }

        public Object(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        {
            this.Id = (int)info.GetValue("Id", typeof(int));

            this.velocity = (Vector3)info.GetValue("velocity", typeof(Vector3));

            this.boundsChanged();

            List<Utility.Corpus.Square> var_List = (List<Utility.Corpus.Square>)info.GetValue("collisionBounds", typeof(List<Utility.Corpus.Square>));
            this.collisionBounds = new List<Rectangle>();

            foreach (Utility.Corpus.Square var_Square in var_List)
            {
                this.collisionBounds.Add(var_Square.Rectangle);
            }

            this.isInDungeon = (bool)info.GetValue("isInDungeon", typeof(bool));
            this.dungeonId = (int)info.GetValue("dungeonId", typeof(int));

        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
            info.AddValue("Id", this.Id, typeof(int));

            info.AddValue("velocity", this.velocity, typeof(Vector3));

            List<Utility.Corpus.Square> var_List = new List<Utility.Corpus.Square>();
            foreach (Rectangle var_Rectangle in this.collisionBounds)
            {
                var_List.Add(new Utility.Corpus.Square(var_Rectangle));
            }

            info.AddValue("collisionBounds", var_List, typeof(List<Utility.Corpus.Square>));

            info.AddValue("isInDungeon", this.isInDungeon, typeof(bool));
            info.AddValue("dungeonId", this.dungeonId, typeof(int));
        }

        public virtual void update(GameTime _GameTime)
        {
        }

        protected override void boundsChanged()
        {
            base.boundsChanged();
            this.Bounds = new Cube(new Vector3(this.Position.X - this.Size.X / 2, this.Position.Y - this.Size.Y, 0), this.Size);
        }

        public virtual bool teleportTo(Vector3 _Position, bool _ToDungeon)
        {
            this.isInDungeon = _ToDungeon;
            //TODO: Hat noch Bugs, wenn map noch nicht da ist :/ also block gleich null.... da muss man sich was überlegen :)
            /*GameLibrary.Map.Block.Block var_Block = GameLibrary.Map.World.World.world.getBlockAtCoordinate(_Position);
            if (var_Block != null)
            {
                this.currentBlock.removeObject(this);
                this.currentBlock = var_Block;
                this.currentBlock.addObject(this);
                this.Position = _Position;

                if (Configuration.Configuration.isHost)
                {
                    Configuration.Configuration.networkManager.addEvent(new GameLibrary.Connection.Message.UpdateObjectPositionMessage(this), GameLibrary.Connection.GameMessageImportance.VeryImportant);
                }

                return true;
            }
            else
            {*/
                this.currentBlock.removeObject(this);
                this.currentBlock = null;
                this.Position = _Position;

                if (Configuration.Configuration.isHost)
                {
                    Configuration.Configuration.networkManager.addEvent(new GameLibrary.Connection.Message.UpdateObjectPositionMessage(this), GameLibrary.Connection.GameMessageImportance.VeryImportant);
                }

                return true;
            //}
            return false;
        }

        public virtual bool teleportTo(Block _Block, bool _ToDungeon)
        {
            this.isInDungeon = _ToDungeon;
            this.currentBlock.removeObject(this);
            this.currentBlock = _Block;
            this.currentBlock.addObject(this);
            this.Position = _Block.Position;

            Region var_Region = this.getRegionIsIn();
            if(var_Region != null)
            {
                if (var_Region is Dungeon)
                {
                    ((Dungeon)var_Region).QuadTreeObject.Insert(this);
                }
            }

            if (Configuration.Configuration.isHost)
            {
                Configuration.Configuration.networkManager.addEvent(new GameLibrary.Connection.Message.UpdateObjectPositionMessage(this), GameLibrary.Connection.GameMessageImportance.VeryImportant);
            }

            return true;
        }

        public QuadTree<Object> getQuadTreeIsIn()
        {
            if (this.currentBlock != null)
            {
                if (this.currentBlock.Parent != null)
                {
                    if (this.currentBlock.Parent.Parent != null)
                    {
                        if (this.currentBlock.Parent.Parent is Dungeon)
                        {
                            return ((Dungeon)this.currentBlock.Parent.Parent).QuadTreeObject;
                        }
                    }
                }
            }
            return World.world.QuadTreeObject;
        }

        public Region getRegionIsIn()
        {
            if (this.currentBlock != null)
            {
                if (this.currentBlock.Parent != null)
                {
                    if (this.currentBlock.Parent.Parent != null)
                    {
                        return (Region)this.currentBlock.Parent.Parent;
                    }
                }
            }
            return null;
        }
    }
}
