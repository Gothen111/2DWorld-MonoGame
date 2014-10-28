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
using GameLibrary.Map.Dimension;
using GameLibrary.Map.World;
#endregion

#region Using Statements Class Specific
#endregion

namespace GameLibrary.Object
{
    [Serializable()]
    public class Object : WorldElement
    {
        private static int lastId = 0;
        private int getId()
        {
            return lastId++;
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

        private Vector3 nextPosition;

        public Vector3 NextPosition
        {
            get { return nextPosition; }
            set { nextPosition = value; }
        }

        private Vector3 velocity;

        public Vector3 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        private int dimensionId;

        public int DimensionId
        {
            get { return dimensionId; }
            set { dimensionId = value; }
        }

        public Object()
        {
            this.Id = this.getId();
            this.collisionBounds = new List<Rectangle>();
        }

        public Object(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        {
            this.velocity = (Vector3)info.GetValue("velocity", typeof(Vector3));

            this.boundsChanged();

            List<Utility.Corpus.Square> var_List = (List<Utility.Corpus.Square>)info.GetValue("collisionBounds", typeof(List<Utility.Corpus.Square>));
            this.collisionBounds = new List<Rectangle>();

            foreach (Utility.Corpus.Square var_Square in var_List)
            {
                this.collisionBounds.Add(var_Square.Rectangle);
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
            info.AddValue("velocity", this.velocity, typeof(Vector3));

            List<Utility.Corpus.Square> var_List = new List<Utility.Corpus.Square>();
            foreach (Rectangle var_Rectangle in this.collisionBounds)
            {
                var_List.Add(new Utility.Corpus.Square(var_Rectangle));
            }

            info.AddValue("collisionBounds", var_List, typeof(List<Utility.Corpus.Square>)); //???
        }

        public virtual void update(GameTime _GameTime)
        {
        }

        protected override void boundsChanged()
        {
            base.boundsChanged();
            this.Bounds = new Cube(new Vector3(this.Position.X - this.Size.X / 2, this.Position.Y - this.Size.Y, 0), this.Size);
        }

        public virtual bool teleportTo(Vector3 _Position)
        {
            //TODO: Hat noch Bugs, wenn map noch nicht da ist :/ also block gleich null.... da muss man sich was überlegen :)
            GameLibrary.Map.Block.Block var_Block = this.getDimensionIsIn().getBlockAtCoordinate(_Position);
            if(var_Block!=null)
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

            return false;
        }

        public Dimension getDimensionIsIn()
        {
            return World.world.getDimensionById(this.dimensionId);
        }
    }
}
