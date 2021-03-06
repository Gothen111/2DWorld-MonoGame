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
using GameLibrary.Map.Chunk;
using GameLibrary.Map.Region;
#endregion

namespace GameLibrary.Connection.Message
{
    public class UpdateChunkMessage : IGameMessage
    {
        #region Constructors and Destructors

        public UpdateChunkMessage(NetIncomingMessage im)
        {
            this.Decode(im);
        }

        public UpdateChunkMessage(Chunk _Chunk)
        {
            this.DimensionId = ((Region)_Chunk.Parent).getParent().Id;
            this.MessageTime = NetTime.Now;
            this.Position = _Chunk.Position;
            this.Chunk = _Chunk;
        }

        #endregion

        #region Properties

        public int DimensionId { get; set; }

        public double MessageTime { get; set; }

        public Vector3 Position { get; set; }

        public Chunk Chunk;

        #endregion

        #region Public Methods

        public EIGameMessageType MessageType
        {
            get { return EIGameMessageType.UpdateChunkMessage; }
        }

        public void Decode(NetIncomingMessage im)
        {
            this.DimensionId = im.ReadInt32();
            this.MessageTime = im.ReadDouble();

            this.Position = Lidgren.MonoGame.ReadVector3(im);

            this.Chunk = Map.World.World.world.getDimensionById(this.DimensionId).getChunkAtPosition(this.Position);
        }

        public void Encode(NetOutgoingMessage om)
        {
            om.Write(this.DimensionId);
            om.Write(this.MessageTime);

            Lidgren.MonoGame.WriteVector3(this.Position, om);
        }

        #endregion
    }
}
