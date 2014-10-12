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
    public class CreatureEquipmentToInventoryMessage : IGameMessage
    {
        #region Constructors and Destructors

        public CreatureEquipmentToInventoryMessage(NetIncomingMessage im)
        {
            this.Decode(im);
        }

        public CreatureEquipmentToInventoryMessage(int _Id, int _EquipmentPosition, int _InventoryPosition)
        {
            this.MessageTime = NetTime.Now;
            this.Id = _Id;
            this.EquipmentPosition = _EquipmentPosition;
            this.InventoryPosition = _InventoryPosition;
        }

        #endregion

        #region Properties

        public double MessageTime { get; set; }

        public int Id { get; set; }

        public int EquipmentPosition { get; set; }

        public int InventoryPosition { get; set; }


        #endregion

        #region Public Methods

        public EIGameMessageType MessageType
        {
            get { return EIGameMessageType.CreatureEquipmentToInventoryMessage; }
        }

        public void Decode(NetIncomingMessage im)
        {
            this.MessageTime = im.ReadDouble();
            this.Id = im.ReadInt32();
            this.EquipmentPosition = im.ReadInt32();
            this.InventoryPosition = im.ReadInt32();

        }

        public void Encode(NetOutgoingMessage om)
        {
            om.Write(this.MessageTime);
            om.Write(this.Id);
            om.Write(this.EquipmentPosition);
            om.Write(this.InventoryPosition);
        }

        #endregion
    }
}
