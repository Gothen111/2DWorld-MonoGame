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
        private List<Object> objects;

        public List<Object> Objects
        {
            get { return objects; }
            set { objects = value; }
        }
        private Vector3 velocity;

        public Vector3 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public Object()
        {
            this.collisionBounds = new List<Rectangle>();
            this.objects = new List<Object>();
        }

        public Object(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        {
            this.Id = (int)info.GetValue("Id", typeof(int));

            this.velocity = (Vector3)info.GetValue("velocity", typeof(Vector3));

            this.boundsChanged();

            this.objects = (List<Object>)info.GetValue("objects", typeof(List<Object>));

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
            info.AddValue("Id", this.Id, typeof(int));

            info.AddValue("velocity", this.velocity, typeof(Vector3));

            info.AddValue("objects", this.objects, typeof(List<Object>));

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
    }
}
