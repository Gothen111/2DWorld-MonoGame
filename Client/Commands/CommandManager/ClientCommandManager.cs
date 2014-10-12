#region Using Statements Standard
using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
#endregion

#region Using Statements Class Specific
using GameLibrary.Connection;
using Lidgren.Network;
using System.Net;
using GameLibrary.Connection.Message;
using GameLibrary.Configuration;
using Microsoft.Xna.Framework.Input;
using GameLibrary.Commands;
using GameLibrary.Object;
#endregion


namespace Client.Commands
{
    public class ClientCommandManager : CommandManager
    {
        public override void handleWalkUpCommand(LivingObject actor)
        {
            if (!actor.MoveUp)
            {
                actor.MoveUp = true;
                if (!Configuration.isSinglePlayer)
                {
                    Configuration.networkManager.addEvent(new GameLibrary.Connection.Message.PlayerCommandMessage(actor as PlayerObject, ECommandType.WalkTopCommand), GameMessageImportance.VeryImportant);
                }
            }
        }
        public override void stopWalkUpCommand(LivingObject actor)
        {
            if (actor.MoveUp)
            {
                actor.MoveUp = false;
                if (!Configuration.isSinglePlayer)
                {
                    Configuration.networkManager.addEvent(new GameLibrary.Connection.Message.PlayerCommandMessage(actor as PlayerObject, ECommandType.StopWalkTopCommand), GameMessageImportance.VeryImportant);
                }
            }
        }

        public override void handleWalkDownCommand(LivingObject actor)
        {
            if (!actor.MoveDown)
            {
                actor.MoveDown = true;
                if (!Configuration.isSinglePlayer)
                {
                    Configuration.networkManager.addEvent(new GameLibrary.Connection.Message.PlayerCommandMessage(actor as PlayerObject, ECommandType.WalkDownCommand), GameMessageImportance.VeryImportant);
                }
            }
        }
        public override void stopWalkDownCommand(LivingObject actor)
        {
            if (actor.MoveDown)
            {
                actor.MoveDown = false;
                if (!Configuration.isSinglePlayer)
                {
                    Configuration.networkManager.addEvent(new GameLibrary.Connection.Message.PlayerCommandMessage(actor as PlayerObject, ECommandType.StopWalkDownCommand), GameMessageImportance.VeryImportant);
                }
            }
        }

        public override void handleWalkLeftCommand(LivingObject actor)
        {
            if (!actor.MoveLeft)
            {
                actor.MoveLeft = true;
                if (!Configuration.isSinglePlayer)
                {
                    Configuration.networkManager.addEvent(new GameLibrary.Connection.Message.PlayerCommandMessage(actor as PlayerObject, ECommandType.WalkLeftCommand), GameMessageImportance.VeryImportant);
                }
            }
        }
        public override void stopWalkLeftCommand(LivingObject actor)
        {
            if (actor.MoveLeft)
            {
                actor.MoveLeft = false;
                if (!Configuration.isSinglePlayer)
                {
                    Configuration.networkManager.addEvent(new GameLibrary.Connection.Message.PlayerCommandMessage(actor as PlayerObject, ECommandType.StopWalkLeftCommand), GameMessageImportance.VeryImportant);
                }
            }
        }

        public override void handleWalkRightCommand(LivingObject actor)
        {
            if (!actor.MoveRight)
            {
                actor.MoveRight = true;
                if (!Configuration.isSinglePlayer)
                {
                    Configuration.networkManager.addEvent(new GameLibrary.Connection.Message.PlayerCommandMessage(actor as PlayerObject, ECommandType.WalkRightCommand), GameMessageImportance.VeryImportant);
                }
            }
        }
        public override void stopWalkRightCommand(LivingObject actor)
        {
            if (actor.MoveRight)
            {
                actor.MoveRight = false;
                if (!Configuration.isSinglePlayer)
                {
                    Configuration.networkManager.addEvent(new GameLibrary.Connection.Message.PlayerCommandMessage(actor as PlayerObject, ECommandType.StopWalkRightCommand), GameMessageImportance.VeryImportant);
                }
            }
        }

        public override void handleAttackCommand(LivingObject actor)
        {
            actor.attack();//actor.attackLivingObject(null, 0); //TODO: Noch Response einbauen, dass Attackanimation nur dann gestartet wird, wenn ein Objekt getroffen wurde
            if (!Configuration.isSinglePlayer)
            {
                Configuration.networkManager.addEvent(new GameLibrary.Connection.Message.PlayerCommandMessage(actor as PlayerObject, ECommandType.AttackCommand), GameMessageImportance.VeryImportant);
            }
        }
    }
}
