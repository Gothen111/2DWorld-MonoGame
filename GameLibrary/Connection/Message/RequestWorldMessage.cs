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
#endregion

namespace GameLibrary.Connection.Message
{
    public class RequestWorldMessage : IGameMessage
    {
        #region Constructors and Destructors

        public RequestWorldMessage(NetIncomingMessage im)
        {
            this.Decode(im);
        }

        public RequestWorldMessage()
        {
            this.MessageTime = NetTime.Now;
        }

        #endregion

        #region Properties

        public double MessageTime { get; set; }

        #endregion

        #region Public Methods

        public EIGameMessageType MessageType
        {
            get { return EIGameMessageType.RequestWorldMessage; }
        }

        public void Decode(NetIncomingMessage im)
        {
            this.MessageTime = im.ReadDouble();
        }

        public void Encode(NetOutgoingMessage om)
        {
            om.Write(this.MessageTime);
        }

        #endregion
    }
}
