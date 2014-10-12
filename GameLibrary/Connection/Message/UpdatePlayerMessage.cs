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
    public class UpdatePlayerMessage : IGameMessage
    {
        #region Constructors and Destructors

        public UpdatePlayerMessage(NetIncomingMessage im)
        {
            this.Decode(im);
        }

        public UpdatePlayerMessage(Object.PlayerObject _PlayerObject)
        {
            this.MessageTime = NetTime.Now;
            this.PlayerObject = _PlayerObject;
        }

        #endregion

        #region Properties

        public double MessageTime { get; set; }

        public Object.PlayerObject PlayerObject { get; set; }


        #endregion

        #region Public Methods

        public EIGameMessageType MessageType
        {
            get { return EIGameMessageType.UpdatePlayerMessage; }
        }

        public void Decode(NetIncomingMessage im)
        {
            this.MessageTime = im.ReadDouble();
            this.PlayerObject = Utility.Serialization.Serializer.DeserializeObjectFromString<Object.PlayerObject>(im.ReadString());
        }

        public void Encode(NetOutgoingMessage om)
        {
            om.Write(this.MessageTime);
            om.Write(Utility.Serialization.Serializer.SerializeObjectToString(this.PlayerObject));
        }

        #endregion
    }
}
