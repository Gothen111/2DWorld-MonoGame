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
using GameLibrary.Enums;
#endregion

namespace GameLibrary.Behaviour.Member
{
    [Serializable()]
    public class Race : Behaviour<Race, RaceEnum>
    {
        public Race(RaceEnum _type) : base(_type)
        {
            
        }

        public Race(SerializationInfo info, StreamingContext ctxt) : base(info, ctxt)
        {
            Race race = GameLibrary.Factory.BehaviourFactory.behaviourFactory.getRace(this.type);
            if (race != null)
            {
                this.behaviour = race.BehaviourMember;
            }
            else
            {
                Logger.Logger.LogErr("Race " + this.type.ToString() + " nicht in der Factory gefunden. Clientbehaviourfactory wurde nicht vom Server geupdatet");
            }
        }
    }
}
