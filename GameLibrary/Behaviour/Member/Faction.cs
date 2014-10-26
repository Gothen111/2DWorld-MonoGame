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
    public class Faction : Behaviour<Faction, FactionEnum>
    {
        public Faction(FactionEnum _type) : base(_type)
        {

        }

        public Faction(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        {
            Faction faction = GameLibrary.Factory.BehaviourFactory.behaviourFactory.getFaction(this.type);
            if (faction != null)
            {
                this.behaviour = faction.BehaviourMember;
            }
            else
            {
                Logger.Logger.LogErr("Faction " + this.type.ToString() + " nicht in der Factory gefunden. Clientbehaviourfactory wurde nicht vom Server geupdatet");
            }
        }
    }
}
