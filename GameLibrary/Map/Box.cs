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
#endregion

namespace GameLibrary.Map
{
    [Serializable()]
    public class Box : WorldElement
    {
        private Box parent;

        public Box Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        private bool isRequested;

        public bool IsRequested
        {
            get { return isRequested; }
            set { isRequested = value; }
        }

        private bool hasReceived;

        public bool HasReceived
        {
            get { return hasReceived; }
            set { hasReceived = value; }
        }

        #region neighbours
        private Box topNeighbour;

        public Box TopNeighbour
        {
            get { return topNeighbour; }
            set { topNeighbour = value; }
        }
        private Box leftNeighbour;

        public Box LeftNeighbour
        {
            get { return leftNeighbour; }
            set { leftNeighbour = value; }
        }
        private Box rightNeighbour;

        public Box RightNeighbour
        {
            get { return rightNeighbour; }
            set { rightNeighbour = value; }
        }
        private Box bottomNeighbour;

        public Box BottomNeighbour
        {
            get { return bottomNeighbour; }
            set { bottomNeighbour = value; }
        }

        private int requestedTimer;
        private int requestedTimerMax;

        #endregion

        public Box()
            :base()
        {
            this.isRequested = false;
            this.hasReceived = false;

            this.requestedTimerMax = 10;
            this.requestedTimer = this.requestedTimerMax;
        }

        public Box(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
        }

        public virtual void update(GameTime _GameTime)
        {
            if (!Configuration.Configuration.isHost && !Configuration.Configuration.isSinglePlayer)
            {
                if (this.isRequested && !this.parent.isRequested)
                {
                    if (this.requestedTimer <= 0)
                    {
                        this.requestFromServer();
                        this.requestedTimer = this.requestedTimerMax;
                    }
                    else
                    {
                        this.requestedTimer -= 1;
                    }
                }
            }
        }

        protected virtual Box getNeighbourBox(Vector3 _Position)
        {
            return null;
        }

        public virtual void setNeighbours()
        {
            Vector3 var_Size = this.Bounds.Size + new Vector3(1, 1, 1);

            Vector3 var_Position = new Vector3(this.Position.X - var_Size.X, this.Position.Y, this.Position.Z);

            Box var_LeftNeighbour = this.getNeighbourBox(var_Position);
            if (var_LeftNeighbour != null)
            {
                this.leftNeighbour = var_LeftNeighbour;
                var_LeftNeighbour.rightNeighbour = this;
            }

            var_Position = new Vector3(this.Position.X + var_Size.X, this.Position.Y, this.Position.Z);

            Box var_RightNeighbour = this.getNeighbourBox(var_Position);
            if (var_RightNeighbour != null)
            {
                this.rightNeighbour = var_RightNeighbour;
                var_RightNeighbour.leftNeighbour = this;
            }

            var_Position = new Vector3(this.Position.X, this.Position.Y - var_Size.Y, this.Position.Z);

            Box var_TopNeighbour = this.getNeighbourBox(var_Position);
            if (var_TopNeighbour != null)
            {
                this.topNeighbour = var_TopNeighbour;
                var_TopNeighbour.bottomNeighbour = this;
            }

            var_Position = new Vector3(this.Position.X, this.Position.Y + var_Size.Y, this.Position.Z);

            Box var_BottomNeighbour = this.getNeighbourBox(var_Position);
            if (var_BottomNeighbour != null)
            {
                this.bottomNeighbour = var_BottomNeighbour;
                var_BottomNeighbour.topNeighbour = this;
            }
        }

        public virtual void requestFromServer()
        {
            if (!Configuration.Configuration.isSinglePlayer)
            {
                this.isRequested = true;
            }
        }
    }
}
