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
    public class UpdateObjectMessage : IGameMessage
    {
        #region Constructors and Destructors

        public UpdateObjectMessage(NetIncomingMessage im)
        {
            this.Decode(im);
        }

        public UpdateObjectMessage(Object.Object _Object)
        {
            this.Id = _Object.Id;
            this.MessageTime = NetTime.Now;
            this.Position = _Object.Position;
            /*this.MoveUp = _LivingObject.MoveUp;
            this.MoveDown = _LivingObject.MoveDown;
            this.MoveLeft = _LivingObject.MoveLeft;
            this.MoveRight = _LivingObject.MoveRight;*/
            this.Object = _Object;
        }

        #endregion

        #region Properties

        public int Id { get; set; }

        public double MessageTime { get; set; }

        public Vector3 Position { get; set; }

        /*public bool MoveUp { get; set; }

        public bool MoveDown { get; set; }

        public bool MoveLeft { get; set; }

        public bool MoveRight { get; set; }*/

        public Object.Object Object { get; set; }

        #endregion

        #region Public Methods

        public EIGameMessageType MessageType
        {
            get { return EIGameMessageType.UpdateObjectMessage; }
        }

        public void Decode(NetIncomingMessage im)
        {
            this.Id = im.ReadInt32();
            this.MessageTime = im.ReadDouble();
            this.Position = Lidgren.MonoGame.ReadVector3(im);
            /*this.MoveUp = im.ReadBoolean();
            this.MoveDown = im.ReadBoolean();
            this.MoveLeft = im.ReadBoolean();
            this.MoveRight = im.ReadBoolean();*/
            this.Object = Utility.Serialization.Serializer.DeserializeObjectFromString<Object.Object>(im.ReadString());
        }

        public void Encode(NetOutgoingMessage om)
        {
            om.Write(this.Id);
            om.Write(this.MessageTime);
            Lidgren.MonoGame.WriteVector3(this.Position, om);
            /*om.Write(this.MoveUp);
            om.Write(this.MoveDown);
            om.Write(this.MoveLeft);
            om.Write(this.MoveRight);*/
            om.Write(Utility.Serialization.Serializer.SerializeObjectToString(this.Object));
        }

        #endregion
    }
}
