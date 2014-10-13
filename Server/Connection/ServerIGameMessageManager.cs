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
using GameLibrary.Connection.Message;
using Lidgren.Network;
using GameLibrary.Factory;
using GameLibrary.Configuration;
using GameLibrary.Object;
using GameLibrary.Connection;
#endregion

namespace Server.Connection
{
    class ServerIGameMessageManager
    {
        public static void OnClientSendIGameMessage(EIGameMessageType _EIGameMessageType, NetIncomingMessage _NetIncomingMessage)
        {
            var var_gameMessageType = _EIGameMessageType;
            switch (var_gameMessageType)
            {
                case EIGameMessageType.RequestPlayerMessage:
                    handleRequestPlayerMessage(_NetIncomingMessage);
                    break;
                case EIGameMessageType.RequestWorldMessage:
                    handleRequestWorldMessage(_NetIncomingMessage);
                    break;
                case EIGameMessageType.RequestRegionMessage:
                    handleRequestRegionMessage(_NetIncomingMessage);
                    break;
                case EIGameMessageType.RequestChunkMessage:
                    handleRequestChunkMessage(_NetIncomingMessage);
                    break;
                case EIGameMessageType.PlayerCommandMessage:
                    handlePlayerCommandMessage(_NetIncomingMessage);
                    break;               
                case EIGameMessageType.RequestBlockMessage:
                    handleRequestBlockMessage(_NetIncomingMessage);
                    break;
                case EIGameMessageType.RequestLivingObjectMessage:
                    handleRequestLivingObjectMessage(_NetIncomingMessage);
                    break;
                case EIGameMessageType.CreatureInventoryItemPositionChangeMessage:
                    handleCreatureInventoryItemPositionChangeMessage(_NetIncomingMessage);
                    break;
                case EIGameMessageType.CreatureEquipmentToInventoryMessage:
                    handleCreatureEquipmentToInventoryMessage(_NetIncomingMessage);
                    break;
                case EIGameMessageType.CreatureInventoryToEquipmentMessage:
                    handleCreatureInventoryToEquipmentMessage(_NetIncomingMessage);
                    break;
                case EIGameMessageType.UpdateObjectPositionMessage:
                    handleUpdateObjectPositionMessage(_NetIncomingMessage);
                    break;
            }
        }

        private static void handleRequestPlayerMessage(NetIncomingMessage _Im)
        {
            var message = new RequestPlayerMessage(_Im);

            var timeDelay = (float)(NetTime.Now - _Im.SenderConnection.GetLocalTime(message.MessageTime));

            PlayerObject var_PlayerObject = CreatureFactory.creatureFactory.createPlayerObject(message.PlayerObject);
            var_PlayerObject.Position = new Vector3(0, Utility.Random.Random.GenerateGoodRandomNumber(0,100), 0);

            GameLibrary.Map.World.World.world.addPlayerObject(var_PlayerObject);

            Client var_Client = Configuration.networkManager.getClient(_Im.SenderEndPoint);
            var_Client.PlayerObject = var_PlayerObject;
            Configuration.networkManager.SendMessageToClient(new UpdatePlayerMessage(var_PlayerObject), var_Client);
            Configuration.networkManager.SendMessageToClient(new UpdateRacesMessage(GameLibrary.Factory.BehaviourFactory.behaviourFactory.Races), var_Client);
            Configuration.networkManager.SendMessageToClient(new UpdateFactionsMessage(GameLibrary.Factory.BehaviourFactory.behaviourFactory.Factions), var_Client);

            GameLibrary.Camera.Camera.camera.setTarget(var_PlayerObject);
        }

        private static void handleRequestWorldMessage(NetIncomingMessage _Im)
        {
            var message = new RequestWorldMessage(_Im);

            var timeDelay = (float)(NetTime.Now - _Im.SenderConnection.GetLocalTime(message.MessageTime));

            Client var_Client = Configuration.networkManager.getClient(_Im.SenderEndPoint);
            Configuration.networkManager.SendMessageToClient(new UpdateWorldMessage(GameLibrary.Map.World.World.world), var_Client);
        }

        private static void handleRequestRegionMessage(NetIncomingMessage _Im)
        {
            var message = new RequestRegionMessage(_Im);

            var timeDelay = (float)(NetTime.Now - _Im.SenderConnection.GetLocalTime(message.MessageTime));

            Client var_Client = Configuration.networkManager.getClient(_Im.SenderEndPoint);

            GameLibrary.Map.Region.Region var_Region = GameLibrary.Map.World.World.world.getRegionAtPosition(message.Position);

            if (var_Region != null)
            {
                Configuration.networkManager.SendMessageToClient(new UpdateRegionMessage(var_Region), var_Client);
            }
            else
            {
                GameLibrary.Logger.Logger.LogErr("ServerIGameMessageManager->handleRequestRegionMessage(...): Region an Position X: " + message.Position.X + " Y: " + message.Position.Y + " existiert nicht!");
            }
        }

        private static void handleRequestChunkMessage(NetIncomingMessage _Im)
        {
            var message = new RequestChunkMessage(_Im);

            var timeDelay = (float)(NetTime.Now - _Im.SenderConnection.GetLocalTime(message.MessageTime));

            Client var_Client = Configuration.networkManager.getClient(_Im.SenderEndPoint);

            GameLibrary.Map.Chunk.Chunk var_Chunk = GameLibrary.Map.World.World.world.getChunkAtPosition(message.Position);

            //GameLibrary.Logger.Logger.LogDeb("Client Requested Chunk at X: " + message.Position.X + " Y: " + message.Position.Y);

            if (var_Chunk != null)
            {
                //Configuration.networkManager.SendMessageToClient(new UpdateRegionMessage((GameLibrary.Model.Map.Region.Region)var_Chunk.Parent), var_Client);
                Configuration.networkManager.SendMessageToClient(new UpdateChunkMessage(var_Chunk), var_Client);

                /*foreach (AnimatedObject var_AnimatedObject in var_Chunk.getAllObjectsInChunk())
                {
                    Configuration.networkManager.SendMessageToClient(new UpdateObjectMessage(var_AnimatedObject), var_Client);
                }*/
            }
            else
            {
                GameLibrary.Logger.Logger.LogErr("ServerIGameMessageManager->handleRequestChunkMessage(...): Chunk an Position X: " + message.Position.X + " Y: " + message.Position.Y + " existiert nicht!");
            }
        }

        private static void handleRequestBlockMessage(NetIncomingMessage _Im)
        {
            var message = new RequestBlockMessage(_Im);

            var timeDelay = (float)(NetTime.Now - _Im.SenderConnection.GetLocalTime(message.MessageTime));

            Client var_Client = Configuration.networkManager.getClient(_Im.SenderEndPoint);

            //GameLibrary.Model.Map.Chunk.Chunk var_Chunk = GameLibrary.Model.Map.World.World.world.getChunkAtPosition(message.Position.X, message.Position.Y);
            GameLibrary.Map.Block.Block var_Block = GameLibrary.Map.World.World.world.getBlockAtCoordinate(message.Position);

            //GameLibrary.Logger.Logger.LogDeb("Client Requested Block at X: " + message.Position.X + " Y: " + message.Position.Y);

            if (var_Block != null)
            {
                //Configuration.networkManager.SendMessageToClient(new UpdateChunkMessage((GameLibrary.Model.Map.Chunk.Chunk)var_Block.Parent), var_Client);
                Configuration.networkManager.SendMessageToClient(new UpdateBlockMessage(var_Block), var_Client);
                List<GameLibrary.Object.Object> var_Copy = new List<GameLibrary.Object.Object>(var_Block.Objects);
                foreach (AnimatedObject var_AnimatedObject in var_Copy)
                {
                    Configuration.networkManager.SendMessageToClient(new UpdateObjectMessage(var_AnimatedObject), var_Client);
                }
            }
            else
            {
                GameLibrary.Logger.Logger.LogErr("ServerIGameMessageManager->handleRequestBlockMessage(...): Block an Position X: " + message.Position.X + " Y: " + message.Position.Y + " existiert nicht!");
            }
        }

        private static void handlePlayerCommandMessage(NetIncomingMessage _Im)
        {
            var message = new PlayerCommandMessage(_Im);

            var timeDelay = (float)(NetTime.Now - _Im.SenderConnection.GetLocalTime(message.MessageTime));

            GameLibrary.Object.PlayerObject var_PlayerObject = GameLibrary.Map.World.World.world.getPlayerObject(message.Id);

            switch(message.ECommandType)
            {
                case GameLibrary.Commands.ECommandType.WalkDownCommand:
                    GameLibrary.Configuration.Configuration.commandManager.handleWalkDownCommand(var_PlayerObject);
                    break;
                case GameLibrary.Commands.ECommandType.WalkTopCommand:
                    GameLibrary.Configuration.Configuration.commandManager.handleWalkUpCommand(var_PlayerObject);
                    break;
                case GameLibrary.Commands.ECommandType.WalkLeftCommand:
                    GameLibrary.Configuration.Configuration.commandManager.handleWalkLeftCommand(var_PlayerObject);
                    break;
                case GameLibrary.Commands.ECommandType.WalkRightCommand:
                    GameLibrary.Configuration.Configuration.commandManager.handleWalkRightCommand(var_PlayerObject);
                    break;
                case GameLibrary.Commands.ECommandType.StopWalkDownCommand:
                    GameLibrary.Configuration.Configuration.commandManager.stopWalkDownCommand(var_PlayerObject);
                    break;
                case GameLibrary.Commands.ECommandType.StopWalkTopCommand:
                    GameLibrary.Configuration.Configuration.commandManager.stopWalkUpCommand(var_PlayerObject);
                    break;
                case GameLibrary.Commands.ECommandType.StopWalkLeftCommand:
                    GameLibrary.Configuration.Configuration.commandManager.stopWalkLeftCommand(var_PlayerObject);
                    break;
                case GameLibrary.Commands.ECommandType.StopWalkRightCommand:
                    GameLibrary.Configuration.Configuration.commandManager.stopWalkRightCommand(var_PlayerObject);
                    break;
                case GameLibrary.Commands.ECommandType.AttackCommand:
                    GameLibrary.Configuration.Configuration.commandManager.handleAttackCommand(var_PlayerObject);
                    break;
            }
            //TODO: Hier nochmal überlegen :/ ob man das hier sendet. damit keine animation o ä hängt
            //Configuration.networkManager.addEvent(new GameLibrary.Connection.Message.UpdateObjectPositionMessage((LivingObject)var_PlayerObject), GameLibrary.Connection.GameMessageImportance.VeryImportant);
        }
        private static void handleRequestLivingObjectMessage(NetIncomingMessage _Im)
        {
            var message = new RequestLivingObjectMessage(_Im);

            var timeDelay = (float)(NetTime.Now - _Im.SenderConnection.GetLocalTime(message.MessageTime));

            Client var_Client = Configuration.networkManager.getClient(_Im.SenderEndPoint);

            GameLibrary.Object.LivingObject var_LivingObject = (GameLibrary.Object.LivingObject) GameLibrary.Map.World.World.world.getObject(message.Id);
            if (var_LivingObject != null)
            {
                Configuration.networkManager.SendMessageToClient(new UpdateObjectMessage(var_LivingObject), var_Client);
            }
            else
            {
                GameLibrary.Logger.Logger.LogErr("ServerIGameMessageManager->handleRequestLivingObjectMessage(...) LivingObject mit Id " + message.Id + " existiert nicht!");
            }
        }

        private static void handleCreatureInventoryItemPositionChangeMessage(NetIncomingMessage _Im)
        {
            var message = new CreatureInventoryItemPositionChangeMessage(_Im);

            var timeDelay = (float)(NetTime.Now - _Im.SenderConnection.GetLocalTime(message.MessageTime));

            GameLibrary.Object.Object var_Object = GameLibrary.Map.World.World.world.getObject(message.Id);
            if (var_Object != null)
            {
                if (var_Object is GameLibrary.Object.CreatureObject)
                {
                    ((GameLibrary.Object.CreatureObject)var_Object).Inventory.changeItemPosition((GameLibrary.Object.CreatureObject)var_Object, message.OldPosition, message.NewPosition);
                }
            }
        }

        private static void handleCreatureEquipmentToInventoryMessage(NetIncomingMessage _Im)
        {
            var message = new CreatureEquipmentToInventoryMessage(_Im);

            var timeDelay = (float)(NetTime.Now - _Im.SenderConnection.GetLocalTime(message.MessageTime));

            GameLibrary.Object.Object var_Object = GameLibrary.Map.World.World.world.getObject(message.Id);
            if (var_Object != null)
            {
                if (var_Object is GameLibrary.Object.CreatureObject)
                {
                    ((GameLibrary.Object.CreatureObject)var_Object).setItemFromEquipmentToInventory(message.EquipmentPosition, message.InventoryPosition);
                }
            }
        }

        private static void handleCreatureInventoryToEquipmentMessage(NetIncomingMessage _Im)
        {
            var message = new CreatureInventoryToEquipmentMessage(_Im);

            var timeDelay = (float)(NetTime.Now - _Im.SenderConnection.GetLocalTime(message.MessageTime));

            GameLibrary.Object.Object var_Object = GameLibrary.Map.World.World.world.getObject(message.Id);
            if (var_Object != null)
            {
                if (var_Object is GameLibrary.Object.CreatureObject)
                {
                    ((GameLibrary.Object.CreatureObject)var_Object).setItemFromInventoryToEquipment(message.InventoryPosition, message.EquipmentPosition);
                }
            }
        }

        private static void handleUpdateObjectPositionMessage(NetIncomingMessage _Im)
        {
            var message = new UpdateObjectPositionMessage(_Im);

            var timeDelay = (float)(NetTime.Now - _Im.SenderConnection.GetLocalTime(message.MessageTime));

            GameLibrary.Object.LivingObject var_LivingObject = (GameLibrary.Object.LivingObject)GameLibrary.Map.World.World.world.getObject(message.Id);
                

            Client var_Client = Configuration.networkManager.getClient(_Im.SenderEndPoint);

            if (var_Client.PlayerObject == var_LivingObject)
            {
                if (var_LivingObject != null)
                {
                    if (var_LivingObject.LastUpdateTime < message.MessageTime)
                    {
                        var_LivingObject.Position = message.Position;
                        //var_LivingObject.MoveUp = message.MoveUp;
                        //var_LivingObject.MoveDown = message.MoveDown;
                        //var_LivingObject.MoveLeft = message.MoveLeft;
                        //var_LivingObject.MoveRight = message.MoveRight;
                        var_LivingObject.checkChangedBlock();

                        var_LivingObject.LastUpdateTime = message.MessageTime;
                    }
                }
                else
                {
                    //GameLibrary.Logger.Logger.LogErr("Object mit Id: " + message.Id + " konnte nicht im Quadtree gefunden werden -> Position wird nicht geupdatet");
                    //GameLibrary.Connection.Event.EventList.Add(new GameLibrary.Connection.Event(new GameLibrary.Connection.Message.RequestLivingObjectMessage(message.Id), GameLibrary.Connection.GameMessageImportance.UnImportant));
                }
            }
        }
    }
}
