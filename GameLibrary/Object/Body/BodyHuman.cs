#region Using Statements Standard
using System;
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
#endregion

namespace GameLibrary.Object.Body
{
    [Serializable()]
    public class BodyHuman: Body
    {
        private BodyPart hair;

        public BodyPart Hair
        {
            get { return hair; }
            set { hair = value; }
        }

        private BodyPart head;

        public BodyPart Head
        {
            get { return head; }
            set { head = value; }
        }

        private BodyPart armLeft;

        public BodyPart ArmLeft
        {
            get { return armLeft; }
            set { armLeft = value; }
        }

        private BodyPart armRight;

        public BodyPart ArmRight
        {
            get { return armRight; }
            set { armRight = value; }
        }

        public BodyHuman()
            :base()
        {
            this.hair = new BodyPart(2, new Vector3(0, 0, 0), this.BodyColor, "");  
            this.armLeft = new BodyPart(1, new Vector3(0, 0, 0), this.BodyColor, "");
            this.armLeft.AcceptedItemTypes.Add(Factory.FactoryEnums.ItemEnum.Weapon);

            this.BodyParts.Add(this.hair);
            this.BodyParts.Add(this.armLeft);
        }

        public BodyHuman(SerializationInfo info, StreamingContext ctxt)
            :base(info, ctxt)
        {
            this.hair = (BodyPart)info.GetValue("hair", typeof(BodyPart));
            this.BodyParts.Add(this.hair);
            this.armLeft = (BodyPart)info.GetValue("armLeft", typeof(BodyPart));
            this.BodyParts.Add(this.armLeft);
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
            info.AddValue("hair", this.hair, typeof(BodyPart));
            info.AddValue("armLeft", this.armLeft, typeof(BodyPart));
        }
    }
}
