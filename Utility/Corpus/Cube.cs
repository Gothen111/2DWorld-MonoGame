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
#endregion

namespace Utility.Corpus
{
    [Serializable()]
    public class Cube : ISerializable
    {
        private Vector3 position;

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }

        private Vector3 size;

        public Vector3 Size
        {
            get { return size; }
            set { size = value; }
        }

        public Cube(Vector3 _Position, Vector3 _Size)
        {
            this.position = _Position;
            this.size = _Size;
        }

        public Cube(float _X, float _Y, float _Z, float _SizeX, float _SizeY, float _SizeZ)
        {
            this.position = new Vector3(_X, _Y, _Z);
            this.size = new Vector3(_SizeX, _SizeY, _SizeZ);
        }

        public Cube(SerializationInfo info, StreamingContext ctxt)
        {
            this.position = (Vector3)info.GetValue("position", typeof(Vector3));
            this.size = (Vector3)info.GetValue("size", typeof(Vector3));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("position", this.position, typeof(Vector3));
            info.AddValue("size", this.size, typeof(Vector3));
        }

        public float Left
        {
            get { return this.position.X; }
        }

        public float Right
        {
            get { return this.position.X + this.size.X; }
        }

        public float Top
        {
            get { return this.position.Y; }
        }

        public float Bottom
        {
            get { return this.position.Y + this.size.Y; }
        }

        public float Front
        {
            get { return this.position.Z; }
        }

        public float Back
        {
            get { return this.position.Z + this.size.Z; }
        }

        public float X
        {
            get { return this.position.X; }
        }

        public float Y
        {
            get { return this.position.Y; }
        }

        public float Z
        {
            get { return this.position.Z; }
        }

        public float Width
        {
            get { return this.size.X; }
        }

        public float Height
        {
            get { return this.size.Y; }
        }

        public float Length
        {
            get { return this.size.Z; }
        }

        public Rectangle Rectangle
        {
            get { return new Rectangle((int)this.position.X, (int)this.position.Y, (int)this.size.X, (int)this.size.Y); }
        }

        public Vector3 Center
        {
            get { return new Vector3(this.Left + (Math.Abs(this.Right) - Math.Abs(this.Left)) / 2, this.Top + (Math.Abs(this.Bottom) - Math.Abs(this.Top)) / 2, this.Front + (Math.Abs(this.Back) - Math.Abs(this.Front)) / 2); }
        }

        public bool intersects(Cube var_Cube)
        {
            if(var_Cube.Right >= this.Left && var_Cube.Left <= this.Right)
            {
                if (var_Cube.Bottom >= this.Top && var_Cube.Top <= this.Bottom)
                {
                    if (var_Cube.Back >= this.Front && var_Cube.Front <= this.Back)
                    {
                        return true;
                    }
                }
            }

            if (this.Right >= var_Cube.Left && this.Left <= var_Cube.Right)
            {
                if (this.Bottom >= var_Cube.Top && this.Top <= var_Cube.Bottom)
                {
                    if (this.Back >= var_Cube.Front && this.Front <= var_Cube.Back)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool intersects(Vector3 _Position)
        {
            if (_Position.X >= this.Left && _Position.X <= this.Right)
            {
                if (_Position.Y >= this.Top && _Position.Y <= this.Bottom)
                {
                    if (_Position.Z >= this.Front && _Position.Z <= this.Back)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
