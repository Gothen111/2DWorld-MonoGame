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
    public class PlayerCommandMessage : IGameMessage
    {
        #region Constructors and Destructors

        public PlayerCommandMessage(NetIncomingMessage im)
        {
            this.Decode(im);
        }

        public PlayerCommandMessage(Object.PlayerObject _PlayerObject, Commands.ECommandType _ECommandType)
        {
            this.Id = _PlayerObject.Id;
            this.MessageTime = NetTime.Now;
            this.ECommandType = _ECommandType;
        }

        #endregion

        #region Properties

        public int Id { get; set; }

        public double MessageTime { get; set; }

        public Commands.ECommandType ECommandType{ get; set; }

        #endregion

        #region Public Methods

        public EIGameMessageType MessageType
        {
            get { return EIGameMessageType.PlayerCommandMessage; }
        }

        public void Decode(NetIncomingMessage im)
        {
            this.Id = im.ReadInt32();
            this.MessageTime = im.ReadDouble();
            this.ECommandType = (Commands.ECommandType) im.ReadInt32();
        }

        public void Encode(NetOutgoingMessage om)
        {
            om.Write(this.Id);
            om.Write(this.MessageTime);
            om.Write((int)this.ECommandType);
        }

        #endregion
    }
}
