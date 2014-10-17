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
using GameLibrary.Map.Region;
using GameLibrary.Enums;
#endregion

namespace GameLibrary.Connection.Message
{
    public class UpdateRegionMessage : IGameMessage
    {
        #region Constructors and Destructors

        public UpdateRegionMessage(NetIncomingMessage im)
        {
            this.Decode(im);
        }

        public UpdateRegionMessage(Region _Region)
        {
            this.Id = _Region.Id;
            this.MessageTime = NetTime.Now;
            //this.Region = _Region;
            this.Position = _Region.Position;
            this.RegionEnum = _Region.RegionEnum;
        }

        #endregion

        #region Properties

        public int Id { get; set; }

        public double MessageTime { get; set; }

        //public Model.Map.Region.Region Region { get; set; }

        public RegionEnum RegionEnum { get; set; }

        public Vector3 Position { get; set; }

        #endregion

        #region Public Methods

        public EIGameMessageType MessageType
        {
            get { return EIGameMessageType.UpdateRegionMessage; }
        }

        public void Decode(NetIncomingMessage im)
        {
            this.Id = im.ReadInt32();
            this.MessageTime = im.ReadDouble();

            //this.Region = Utility.Serializer.DeserializeObjectFromString<Model.Map.Region.Region>(im.ReadString());
            //this.Region.Parent = Model.Map.World.World.world;

            this.Position = Lidgren.MonoGame.ReadVector3(im);

            this.RegionEnum = (RegionEnum)im.ReadInt32();
        }

        public void Encode(NetOutgoingMessage om)
        {
            om.Write(this.Id);
            om.Write(this.MessageTime);

            //om.Write(Utility.Serializer.SerializeObjectToString(this.Region));

            Lidgren.MonoGame.WriteVector3(this.Position, om);

            om.Write((int)this.RegionEnum);
        }

        #endregion
    }
}
