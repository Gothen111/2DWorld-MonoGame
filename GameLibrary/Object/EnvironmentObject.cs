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
using GameLibrary.Enums;
#endregion

namespace GameLibrary.Object
{
    [Serializable()]
    public class EnvironmentObject : LivingObject
    {
        private EnvironmentEnum environmentEnum;

        public EnvironmentEnum EnvironmentEnum
        {
            get { return environmentEnum; }
            set { environmentEnum = value; }
        }

        public EnvironmentObject() :base()
        {
            this.CanBeEffected = false;
        }

        public EnvironmentObject(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
        }

        public override void update(GameTime _GameTime)
        {
            base.update(_GameTime);
        }
    }
}
