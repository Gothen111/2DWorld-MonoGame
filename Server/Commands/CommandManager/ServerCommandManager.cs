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
using GameLibrary.Commands;
using GameLibrary.Object;
#endregion

namespace Server.Commands
{
    class ServerCommandManager : CommandManager
    {
        public override void handleWalkUpCommand(LivingObject actor)
        {
            actor.MoveUp = true;
        }
        public override void stopWalkUpCommand(LivingObject actor)
        {
            actor.MoveUp = false;
        }

        public override void handleWalkDownCommand(LivingObject actor)
        {
            actor.MoveDown = true;
        }
        public override void stopWalkDownCommand(LivingObject actor)
        {
            actor.MoveDown = false;
        }

        public override void handleWalkLeftCommand(LivingObject actor)
        {
            actor.MoveLeft = true;
        }
        public override void stopWalkLeftCommand(LivingObject actor)
        {
            actor.MoveLeft = false;
        }

        public override void handleWalkRightCommand(LivingObject actor)
        {
            actor.MoveRight = true;
        }
        public override void stopWalkRightCommand(LivingObject actor)
        {
            actor.MoveRight = false;
        }

        public override void handleAttackCommand(LivingObject actor)
        {
            if (actor is CreatureObject)
                (actor as CreatureObject).attack();
        }
    }
}
