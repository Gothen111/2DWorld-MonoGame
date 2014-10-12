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
using GameLibrary.Map.World;
#endregion

namespace GameLibrary.Connection.Message
{
    public class UpdateWorldMessage : IGameMessage
    {
        #region Constructors and Destructors

        public UpdateWorldMessage(NetIncomingMessage im)
        {
            this.Decode(im);
        }

        public UpdateWorldMessage(World _World)
        {
            this.MessageTime = NetTime.Now;
            this.World = _World;
        }

        #endregion

        #region Properties

        public double MessageTime { get; set; }

        public World World { get; set; }

        #endregion

        #region Public Methods

        public EIGameMessageType MessageType
        {
            get { return EIGameMessageType.UpdateWorldMessage; }
        }

        public void Decode(NetIncomingMessage im)
        {
            this.MessageTime = im.ReadDouble();
            this.World = Utility.Serialization.Serializer.DeserializeObjectFromString<World>(im.ReadString());
        }

        public void Encode(NetOutgoingMessage om)
        {
            om.Write(this.MessageTime);
            om.Write(Utility.Serialization.Serializer.SerializeObjectToString(this.World));
        }

        #endregion
    }
}
