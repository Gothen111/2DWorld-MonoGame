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
    public class UpdateObjectHealthMessage : IGameMessage
    {
        #region Constructors and Destructors

        public UpdateObjectHealthMessage(NetIncomingMessage im)
        {
            this.Decode(im);
        }

        public UpdateObjectHealthMessage(Object.LivingObject _LivingObject)
        {
            this.Id = _LivingObject.Id;
            this.MessageTime = NetTime.Now;
            this.Health = _LivingObject.HealthPoints;
            this.MaxHealth = _LivingObject.MaxHealthPoints;
        }

        #endregion

        #region Properties

        public int Id { get; set; }

        public double MessageTime { get; set; }

        public float Health { get; set; }

        public float MaxHealth { get; set; }

        public EIGameMessageType MessageType
        {
            get { return EIGameMessageType.UpdateObjectHealthMessage; }
        }

        #endregion

        #region Public Methods

        public void Decode(NetIncomingMessage im)
        {
            this.Id = im.ReadInt32();
            this.MessageTime = im.ReadDouble();
            this.Health = im.ReadFloat();
            this.MaxHealth = im.ReadFloat();
        }

        public void Encode(NetOutgoingMessage om)
        {
            om.Write(this.Id);
            om.Write(this.MessageTime);
            om.Write(this.Health);
            om.Write(this.MaxHealth);
        }

        #endregion
    }
}
