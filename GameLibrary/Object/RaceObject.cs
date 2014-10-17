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
using GameLibrary.Behaviour.Member;
#endregion

namespace GameLibrary.Object
{
    public class RaceObject : CreatureObject
    {
        private RaceEnum raceEnum;

        public RaceEnum RaceEnum
        {
            get { return raceEnum; }
            set { raceEnum = value; }
        }

        public Race Race
        {
            get { return GameLibrary.Factory.BehaviourFactory.behaviourFactory.getRace(this.RaceEnum); }
        }

        public RaceObject() :base()
        {
        }

        public RaceObject(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        {
            this.raceEnum = (RaceEnum)info.GetValue("raceEnum", typeof(RaceEnum));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
            info.AddValue("raceEnum", this.raceEnum, typeof(RaceEnum));
        }

        public override void update(GameTime _GameTime)
        {
            base.update(_GameTime);
        }
    }
}
