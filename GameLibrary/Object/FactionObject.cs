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
using GameLibrary.Factory.FactoryEnums;
using GameLibrary.Behaviour.Member;
#endregion

namespace GameLibrary.Object
{
    [Serializable()]
    public class FactionObject : RaceObject
    {
        private FactionEnum factionEnum;

        public FactionEnum FactionEnum
        {
            get { return factionEnum; }
            set { factionEnum = value; }
        }

        public Faction Faction
        {
            get { return GameLibrary.Factory.BehaviourFactory.behaviourFactory.getFaction(this.FactionEnum); }
        }

        public FactionObject()
        {
        }


        public FactionObject(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        {
            this.factionEnum = (FactionEnum)info.GetValue("factionEnum", typeof(FactionEnum));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
            info.AddValue("factionEnum", this.factionEnum, typeof(FactionEnum));
        }

        public override void update(GameTime _GameTime)
        {
            base.update(_GameTime);
        }
    }
}
