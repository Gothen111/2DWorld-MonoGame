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
using GameLibrary.Object;
#endregion

namespace GameLibrary.Commands.CommandTypes
{
    public class WalkLeftCommand : Command
    {
        private LivingObject walkActor;

        public LivingObject WalkActor
        {
            get { return walkActor; }
            set { walkActor = value; }
        }

        public WalkLeftCommand(LivingObject _walkActor)
        {
            this.walkActor = _walkActor;
        }

        public override void doCommand()
        {
            GameLibrary.Configuration.Configuration.commandManager.handleWalkLeftCommand(walkActor);
        }

        public override void stopCommand()
        {
            GameLibrary.Configuration.Configuration.commandManager.stopWalkLeftCommand(walkActor);
        }
    }
}
