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
using GameLibrary.Object.Body;
#endregion

namespace GameLibrary.Connection.Message
{
    public class UpdateAnimatedObjectBodyMessage : IGameMessage
    {
        #region Constructors and Destructors

        public UpdateAnimatedObjectBodyMessage(NetIncomingMessage im)
        {
            this.Decode(im);
        }

        public UpdateAnimatedObjectBodyMessage(int _Id, Body _Body)
        {
            this.MessageTime = NetTime.Now;
            this.Id = _Id;
            this.Body = _Body;
        }

        #endregion

        #region Properties

        public double MessageTime { get; set; }

        public int Id { get; set; }

        public Body Body { get; set; }


        #endregion

        #region Public Methods

        public EIGameMessageType MessageType
        {
            get { return EIGameMessageType.UpdateAnimatedObjectBodyMessage; }
        }

        public void Decode(NetIncomingMessage im)
        {
            this.MessageTime = im.ReadDouble();
            this.Id = im.ReadInt32();
            this.Body = Utility.Serialization.Serializer.DeserializeObjectFromString<Body>(im.ReadString());
        }

        public void Encode(NetOutgoingMessage om)
        {
            om.Write(this.MessageTime);
            om.Write(this.Id);
            om.Write(Utility.Serialization.Serializer.SerializeObjectToString(this.Body));
        }

        #endregion
    }
}
