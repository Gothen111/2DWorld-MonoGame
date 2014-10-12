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
using GameLibrary.Object.Inventory;
#endregion

namespace GameLibrary.Connection.Message
{
    public class UpdateCreatureInventoryMessage : IGameMessage
    {
        #region Constructors and Destructors

        public UpdateCreatureInventoryMessage(NetIncomingMessage im)
        {
            this.Decode(im);
        }

        public UpdateCreatureInventoryMessage(int _Id, Inventory _Inventory)
        {
            this.MessageTime = NetTime.Now;
            this.Id = _Id;
            this.Inventory = _Inventory;
        }

        #endregion

        #region Properties

        public double MessageTime { get; set; }

        public int Id { get; set; }

        public Inventory Inventory { get; set; }


        #endregion

        #region Public Methods

        public EIGameMessageType MessageType
        {
            get { return EIGameMessageType.UpdateCreatureInventoryMessage; }
        }

        public void Decode(NetIncomingMessage im)
        {
            this.MessageTime = im.ReadDouble();
            this.Id = im.ReadInt32();
            this.Inventory = Utility.Serialization.Serializer.DeserializeObjectFromString<Inventory>(im.ReadString());
        }

        public void Encode(NetOutgoingMessage om)
        {
            om.Write(this.MessageTime);
            om.Write(this.Id);
            om.Write(Utility.Serialization.Serializer.SerializeObjectToString(this.Inventory));
        }

        #endregion
    }
}
