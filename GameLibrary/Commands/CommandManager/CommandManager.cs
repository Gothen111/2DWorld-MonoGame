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

namespace GameLibrary.Commands
{
    public abstract class CommandManager
    {
        public abstract void handleWalkUpCommand(LivingObject actor);
        public abstract void stopWalkUpCommand(LivingObject actor);

        public abstract void handleWalkDownCommand(LivingObject actor);
        public abstract void stopWalkDownCommand(LivingObject actor);

        public abstract void handleWalkLeftCommand(LivingObject actor);
        public abstract void stopWalkLeftCommand(LivingObject actor);

        public abstract void handleWalkRightCommand(LivingObject actor);
        public abstract void stopWalkRightCommand(LivingObject actor);

        public abstract void handleAttackCommand(LivingObject actor);
    }
}
