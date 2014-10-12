using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Microsoft.Xna.Framework;

namespace Utility.Corpus
{
    [Serializable()]
    public class Square : ISerializable
    {
        private int x;

        public int X
        {
            get { return x; }
            set { x = value; }
        }
        private int y;

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        private int width;

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        private int height;

        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        public Rectangle Rectangle
        {
            get { return new Rectangle(this.x,this.y,this.width,this.height); }
        }

        public Square()
        {
        }

        public Square(Rectangle _Rectangle)
        {
            this.x = _Rectangle.X;
            this.y = _Rectangle.Y;
            this.width = _Rectangle.Width;
            this.height = _Rectangle.Height;
        }

        public Square(SerializationInfo info, StreamingContext ctxt)
        {
            this.x = (int)info.GetValue("x", typeof(int));
            this.y = (int)info.GetValue("y", typeof(int));
            this.width = (int)info.GetValue("width", typeof(int));
            this.height = (int)info.GetValue("height", typeof(int));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("x", this.x, typeof(int));
            info.AddValue("y", this.y, typeof(int));
            info.AddValue("width", this.width, typeof(int));
            info.AddValue("height", this.height, typeof(int));
        }
    }
}
