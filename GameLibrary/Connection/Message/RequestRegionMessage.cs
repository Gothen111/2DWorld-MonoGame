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
#endregion

namespace GameLibrary.Connection.Message
{
    public class RequestRegionMessage : IGameMessage
    {
        #region Constructors and Destructors

        public RequestRegionMessage(NetIncomingMessage im)
        {
            this.Decode(im);
        }

        public RequestRegionMessage(Vector3 _Position)
        {
            this.MessageTime = NetTime.Now;
            this.Position = _Position;
        }

        #endregion

        #region Properties

        public double MessageTime { get; set; }

        public Vector3 Position { get; set; }

        #endregion

        #region Public Methods

        public EIGameMessageType MessageType
        {
            get { return EIGameMessageType.RequestRegionMessage; }
        }

        public void Decode(NetIncomingMessage im)
        {
            this.MessageTime = im.ReadDouble();
            this.Position = Lidgren.MonoGame.ReadVector3(im);
        }

        public void Encode(NetOutgoingMessage om)
        {
            om.Write(this.MessageTime);
            Lidgren.MonoGame.WriteVector3(this.Position, om);
        }

        #endregion
    }
}