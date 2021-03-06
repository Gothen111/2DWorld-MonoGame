﻿#region Using Statements Standard
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
using Utility;
using Utility.Corpus;
#endregion

namespace GameLibrary
{
    [Serializable()]
    public class WorldElement : /*GameLibrary.Gui.Component,*/ ISerializable
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private Vector3 size;

        public Vector3 Size
        {
            get { return size; }
            set { size = value; this.boundsChanged(); }
        }

        private Vector3 position;

        public Vector3 Position
        {
            get { return position; }
            set { this.oldPosition = position; position = value; this.boundsChanged(); }
        }

        private Vector3 oldPosition;

        public Vector3 OldPosition
        {
            get { return oldPosition; }
            set { oldPosition = value; }
        }

        private Cube bounds;

        public Cube Bounds
        {
            get { return bounds; }
            set { bounds = value; }
        }

        private String name;

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        private double lastUpdateTime;

        public double LastUpdateTime
        {
            get { return lastUpdateTime; }
            set { lastUpdateTime = value; }
        }

        private Color drawColor;

        public Color DrawColor
        {
            get { return drawColor; }
            set { drawColor = value; }
        }

        public WorldElement()
           :base()
        {
        }

        public WorldElement(SerializationInfo info, StreamingContext ctxt)
            : this()
        {
            this.id = (int)info.GetValue("id", typeof(int));
            this.size = (Vector3)info.GetValue("size", typeof(Vector3));
            this.position = (Vector3)info.GetValue("position", typeof(Vector3));
            this.name = (String)info.GetValue("name", typeof(String));
            this.Bounds = (Cube)info.GetValue("bounds", typeof(Cube));
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("id", this.id);
            info.AddValue("size", this.size, typeof(Vector3));
            info.AddValue("position", this.position, typeof(Vector3));
            info.AddValue("name", this.name);
            info.AddValue("bounds", this.Bounds, typeof(Cube));
        }

        protected virtual void boundsChanged()
        {
            this.bounds = new Cube(this.position, this.size);
        }
    }
}

