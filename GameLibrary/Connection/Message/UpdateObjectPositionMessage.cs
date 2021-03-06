﻿#region Using Statements Standard
using System;
using System.Linq;
using System.Text;
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
using Lidgren.Network;
using GameLibrary.Object;
#endregion

namespace GameLibrary.Connection.Message
{
    public class UpdateObjectPositionMessage : IGameMessage
    {
        #region Constructors and Destructors

        public UpdateObjectPositionMessage(NetIncomingMessage im)
        {
            this.Decode(im);
        }

        public UpdateObjectPositionMessage(Object.Object _Object)
        {
            this.Id = _Object.Id;
            this.MessageTime = NetTime.Now;
            this.Position = _Object.Position;
            this.Velocity = _Object.Velocity;

            if (_Object is AnimatedObject)
            {
                this.MoveUp = ((AnimatedObject)_Object).MoveUp;
                this.MoveDown = ((AnimatedObject)_Object).MoveDown;
                this.MoveLeft = ((AnimatedObject)_Object).MoveLeft;
                this.MoveRight = ((AnimatedObject)_Object).MoveRight;
            }
        }

        #endregion

        #region Properties

        public int Id { get; set; }

        public double MessageTime { get; set; }

        public Vector3 Position { get; set; }

        public Vector3 Velocity { get; set; }

        public bool MoveUp { get; set; }

        public bool MoveDown { get; set; }

        public bool MoveLeft { get; set; }

        public bool MoveRight { get; set; }

        public EIGameMessageType MessageType
        {
            get { return EIGameMessageType.UpdateObjectPositionMessage; }
        }

        #endregion

        #region Public Methods

        public void Decode(NetIncomingMessage im)
        {
            this.Id = im.ReadInt32();
            this.MessageTime = im.ReadDouble();
            this.Position = Lidgren.MonoGame.ReadVector3(im);
            this.Velocity = Lidgren.MonoGame.ReadVector3(im);
            this.MoveUp = im.ReadBoolean();
            this.MoveDown = im.ReadBoolean();
            this.MoveLeft = im.ReadBoolean();
            this.MoveRight = im.ReadBoolean();
        }

        public void Encode(NetOutgoingMessage om)
        {
            om.Write(this.Id);
            om.Write(this.MessageTime);
            Lidgren.MonoGame.WriteVector3(this.Position, om);
            Lidgren.MonoGame.WriteVector3(this.Velocity, om);
            om.Write(this.MoveUp);
            om.Write(this.MoveDown);
            om.Write(this.MoveLeft);
            om.Write(this.MoveRight);
        }

        #endregion
    }
}
