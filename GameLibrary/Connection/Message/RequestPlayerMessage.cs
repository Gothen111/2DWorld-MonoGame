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
using GameLibrary.Object;
#endregion

namespace GameLibrary.Connection.Message
{
    public class RequestPlayerMessage : IGameMessage
    {
        #region Constructors and Destructors

        public RequestPlayerMessage(NetIncomingMessage im)
        {
            this.Decode(im);
        }

        public RequestPlayerMessage(PlayerObject _PlayerObject)
        {
            this.PlayerObject = _PlayerObject;
            this.MessageTime = NetTime.Now;
        }

        #endregion

        #region Properties

        public PlayerObject PlayerObject { get; set; }

        public double MessageTime { get; set; }


        #endregion

        #region Public Methods

        public EIGameMessageType MessageType
        {
            get { return EIGameMessageType.RequestPlayerMessage; }
        }

        public void Decode(NetIncomingMessage im)
        {
            this.PlayerObject = Utility.Serialization.Serializer.DeserializeObjectFromString<PlayerObject>(im.ReadString());
            this.MessageTime = im.ReadDouble();
        }

        public void Encode(NetOutgoingMessage om)
        {
            om.Write(Utility.Serialization.Serializer.SerializeObjectToString(this.PlayerObject));
            om.Write(this.MessageTime);
        }

        #endregion
    }
}
