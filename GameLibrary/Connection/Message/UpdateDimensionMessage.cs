#region Using Statements Standard
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
using GameLibrary.Enums;
using GameLibrary.Map.Dimension;
#endregion

namespace GameLibrary.Connection.Message
{
    public class UpdateDimensionMessage : IGameMessage
    {
        #region Constructors and Destructors

        public UpdateDimensionMessage(NetIncomingMessage im)
        {
            this.Decode(im);
        }

        public UpdateDimensionMessage(Dimension _Dimension)
        {
            this.DimensionId = _Dimension.Id;
            this.MessageTime = NetTime.Now;
        }

        #endregion

        #region Properties

        public int DimensionId { get; set; }

        public double MessageTime { get; set; }

        #endregion

        #region Public Methods

        public EIGameMessageType MessageType
        {
            get { return EIGameMessageType.UpdateDimensionMessage; }
        }

        public void Decode(NetIncomingMessage im)
        {
            this.DimensionId = im.ReadInt32();
            this.MessageTime = im.ReadDouble();
        }

        public void Encode(NetOutgoingMessage om)
        {
            om.Write(this.DimensionId);
            om.Write(this.MessageTime);
        }

        #endregion
    }
}
