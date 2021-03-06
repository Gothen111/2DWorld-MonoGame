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
using GameLibrary.Map.Region;
using GameLibrary.Enums;
#endregion

namespace GameLibrary.Connection.Message
{
    public class UpdateRegionMessage : IGameMessage
    {
        #region Constructors and Destructors

        public UpdateRegionMessage(NetIncomingMessage im)
        {
            this.Decode(im);
        }

        public UpdateRegionMessage(Region _Region)
        {
            this.DimensionId = _Region.getParent().Id;
            this.MessageTime = NetTime.Now;
            this.Position = _Region.Position;
            this.RegionEnum = _Region.RegionEnum;
        }

        #endregion

        #region Properties

        public int DimensionId { get; set; }

        public double MessageTime { get; set; }

        public RegionEnum RegionEnum { get; set; }

        public Vector3 Position { get; set; }

        #endregion

        #region Public Methods

        public EIGameMessageType MessageType
        {
            get { return EIGameMessageType.UpdateRegionMessage; }
        }

        public void Decode(NetIncomingMessage im)
        {
            this.DimensionId = im.ReadInt32();
            this.MessageTime = im.ReadDouble();

            this.Position = Lidgren.MonoGame.ReadVector3(im);

            this.RegionEnum = (RegionEnum)im.ReadInt32();
        }

        public void Encode(NetOutgoingMessage om)
        {
            om.Write(this.DimensionId);
            om.Write(this.MessageTime);

            //om.Write(Utility.Serializer.SerializeObjectToString(this.Region));

            Lidgren.MonoGame.WriteVector3(this.Position, om);

            om.Write((int)this.RegionEnum);
        }

        #endregion
    }
}
