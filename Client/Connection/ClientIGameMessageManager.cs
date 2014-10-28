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
#endregion



namespace Client.Connection
{
    class ClientIGameMessageManager
    {
        public static void OnClientSendIGameMessage(EIGameMessageType _EIGameMessageType, NetIncomingMessage _NetIncomingMessage)
        {
            var var_gameMessageType = _EIGameMessageType;

            switch (var_gameMessageType)
            {
                case EIGameMessageType.UpdatePlayerMessage:
                    handleUpdatePlayerMessage(_NetIncomingMessage);
                    break;
                case EIGameMessageType.UpdateWorldMessage:
                    handleUpdateWorldMessage(_NetIncomingMessage);
                    break;
                case EIGameMessageType.UpdateRegionMessage:
                    handleUpdateRegionMessage(_NetIncomingMessage);
                    break;
                case EIGameMessageType.UpdateChunkMessage:
                    handleUpdateChunkMessage(_NetIncomingMessage);
                    break;
                case EIGameMessageType.UpdateBlockMessage:
                    handleUpdateBlockMessage(_NetIncomingMessage);
                    break;  
                case EIGameMessageType.UpdateObjectMessage:
                    handleUpdateObjectMessage(_NetIncomingMessage);
                    break;
                case EIGameMessageType.UpdatePreEnvironmentObjectMessage:
                    handleUpdatePreEnvironmentObjectMessage(_NetIncomingMessage);
                    break;            
                case EIGameMessageType.UpdateObjectPositionMessage:
                    handleUpdateObjectPositionMessage(_NetIncomingMessage);
                    break;
                case EIGameMessageType.UpdateObjectMovementMessage:
                    handleUpdateObjectMovementMessage(_NetIncomingMessage);
                    break;
                case EIGameMessageType.UpdateObjectHealthMessage:
                    handleUpdateObjectHealthMessage(_NetIncomingMessage);
                    break;
                case EIGameMessageType.RemoveObjectMessage:
                    handleRemoveObjectMessage(_NetIncomingMessage);
                    break;
                case EIGameMessageType.UpdateCreatureInventoryMessage:
                    handleUpdateCreatureInventoryMessage(_NetIncomingMessage);
                    break;
                case EIGameMessageType.UpdateAnimatedObjectBodyMessage:
                    handleUpdateAnimatedObjectBodyMessage(_NetIncomingMessage);
                    break;
                case EIGameMessageType.UpdateRacesMessage:
                    handleUpdateRacesMessage(_NetIncomingMessage);
                    break;

            }
        }


        private static void handleUpdatePlayerMessage(NetIncomingMessage _Im)
        {
            var message = new UpdatePlayerMessage(_Im);

            var timeDelay = (float)(NetTime.Now - _Im.SenderConnection.GetLocalTime(message.MessageTime));

            if (Configuration.networkManager.client != null)
            {
                if (Configuration.networkManager.client.ClientStatus == GameLibrary.Connection.EClientStatus.RequestedPlayerPosition)
                {
                    Configuration.networkManager.client.PlayerObject = message.PlayerObject;
                    Configuration.networkManager.client.ClientStatus = GameLibrary.Connection.EClientStatus.RequestWorld;
                }
            }
        }

        private static void handleUpdateWorldMessage(NetIncomingMessage _Im)
        {
            var message = new UpdateWorldMessage(_Im);

            var timeDelay = (float)(NetTime.Now - _Im.SenderConnection.GetLocalTime(message.MessageTime));

            GameLibrary.Map.World.World var_World = message.World;

            if (Configuration.networkManager.client != null)
            {
                if (Configuration.networkManager.client.ClientStatus == GameLibrary.Connection.EClientStatus.RequestedWorld)
                {
                    if (GameLibrary.Map.World.World.world == null)
                    {
                        GameLibrary.Map.World.World.world = message.World;
                        Configuration.networkManager.client.ClientStatus = GameLibrary.Connection.EClientStatus.JoinWorld;
                    }
                }
            }
        }

        private static void handleUpdateRegionMessage(NetIncomingMessage _Im)
        {
            var message = new UpdateRegionMessage(_Im);
            var timeDelay = (float)(NetTime.Now - _Im.SenderConnection.GetLocalTime(message.MessageTime));

            if (Configuration.networkManager.client != null)
            {
                if (true)
                {
                    GameLibrary.Map.Region.Region var_Region = GameLibrary.Map.World.World.world.getDimensionById(message.DimensionId).getRegionAtPosition(message.Position);
                    if (var_Region != null)
                    {
                        if (var_Region.IsRequested)
                        {
                            var_Region.RegionEnum = message.RegionEnum;
                            var_Region.IsRequested = false;
                        }
                    }
                }
                else
                {
                    GameLibrary.Logger.Logger.LogDeb("Region sollte hinzugefügt werden, ist allerdings schon vorhanden -> Benutze UpdateChunkMessage");
                }
            }
        }

        private static void handleUpdateChunkMessage(NetIncomingMessage _Im)
        {
            var message = new UpdateChunkMessage(_Im);

            var timeDelay = (float)(NetTime.Now - _Im.SenderConnection.GetLocalTime(message.MessageTime));
        }

        private static void handleUpdateBlockMessage(NetIncomingMessage _Im)
        {
            var message = new UpdateBlockMessage(_Im);

            var timeDelay = (float)(NetTime.Now - _Im.SenderConnection.GetLocalTime(message.MessageTime));
        }

        private static void handleUpdateObjectMessage(NetIncomingMessage _Im)
        {
            var message = new UpdateObjectMessage(_Im);

            var timeDelay = (float)(NetTime.Now - _Im.SenderConnection.GetLocalTime(message.MessageTime));
            if (GameLibrary.Map.World.World.world != null)
            {
                GameLibrary.Object.Object var_Object = (GameLibrary.Object.Object)(GameLibrary.Map.World.World.world.getObject(message.Id) ?? GameLibrary.Map.World.World.world.addObject(message.Object));//CreatureFactory.creatureFactory.createNpcObject(message.Id, RaceEnum.Human, FactionEnum.Castle_Test, CreatureEnum.Chieftain, GenderEnum.Male));
                var_Object.Position = message.Position;
                var_Object.NextPosition = message.Position;
            }
        }

        private static void handleUpdatePreEnvironmentObjectMessage(NetIncomingMessage _Im)
        {
            var message = new UpdatePreEnvironmentObjectMessage(_Im);

            var timeDelay = (float)(NetTime.Now - _Im.SenderConnection.GetLocalTime(message.MessageTime));
            if (GameLibrary.Map.World.World.world != null)
            {
                GameLibrary.Object.Object var_Object = GameLibrary.Map.World.World.world.addPreEnvironmentObject(message.Object);//(GameLibrary.Object.Object)(GameLibrary.Map.World.World.world.getObject(message.Id) ?? GameLibrary.Map.World.World.world.addObject(message.Object));//CreatureFactory.creatureFactory.createNpcObject(message.Id, RaceEnum.Human, FactionEnum.Castle_Test, CreatureEnum.Chieftain, GenderEnum.Male));
                var_Object.Position = message.Position;
            }
        }

        private static void handleUpdateObjectPositionMessage(NetIncomingMessage _Im)
        {
            var message = new UpdateObjectPositionMessage(_Im);

            var timeDelay = (float)(NetTime.Now - _Im.SenderConnection.GetLocalTime(message.MessageTime));

            if (GameLibrary.Map.World.World.world != null)
            {
                GameLibrary.Object.LivingObject var_LivingObject = (GameLibrary.Object.LivingObject)GameLibrary.Map.World.World.world.getObject(message.Id);
                if (var_LivingObject != Configuration.networkManager.client.PlayerObject)
                {
                    if (var_LivingObject != null)
                    {
                        if (var_LivingObject.LastUpdateTime < message.MessageTime)
                        {
                            //if (Configuration.networkManager.client.PlayerObject != var_LivingObject)
                            //{
                            //var_LivingObject.Position += (message.Velocity * timeDelay);
                            var_LivingObject.NextPosition = message.Position;
                            var_LivingObject.MoveUp = message.MoveUp;
                            var_LivingObject.MoveDown = message.MoveDown;
                            var_LivingObject.MoveLeft = message.MoveLeft;
                            var_LivingObject.MoveRight = message.MoveRight;
                            //}
                            var_LivingObject.checkChangedBlock();

                            var_LivingObject.LastUpdateTime = message.MessageTime;
                        }
                    }
                    else
                    {
                        //GameLibrary.Logger.Logger.LogErr("Object mit Id: " + message.Id + " konnte nicht im Quadtree gefunden werden -> Position wird nicht geupdatet");
                        Configuration.networkManager.addEvent(new GameLibrary.Connection.Message.RequestLivingObjectMessage(message.Id), GameLibrary.Connection.GameMessageImportance.UnImportant);
                    }
                }
            }
        }

        private static void handleUpdateObjectMovementMessage(NetIncomingMessage _Im)
        {
            var message = new UpdateObjectMovementMessage(_Im);

            var timeDelay = (float)(NetTime.Now - _Im.SenderConnection.GetLocalTime(message.MessageTime));

            if (GameLibrary.Map.World.World.world != null)
            {
                GameLibrary.Object.LivingObject var_LivingObject = (GameLibrary.Object.LivingObject)GameLibrary.Map.World.World.world.getObject(message.Id);
                if (var_LivingObject != null)
                {
                    if (var_LivingObject.LastUpdateTime < message.MessageTime)
                    {
                        if (Configuration.networkManager.client.PlayerObject != var_LivingObject)
                        {
                            var_LivingObject.MoveUp = message.MoveUp;
                            var_LivingObject.MoveDown = message.MoveDown;
                            var_LivingObject.MoveLeft = message.MoveLeft;
                            var_LivingObject.MoveRight = message.MoveRight;
                        }
                        var_LivingObject.LastUpdateTime = message.MessageTime;
                    }
                }
                else
                {
                    //GameLibrary.Logger.Logger.LogErr("Object mit Id: " + message.Id + " konnte nicht im Quadtree gefunden werden -> Position wird nicht geupdatet");
                    Configuration.networkManager.addEvent(new GameLibrary.Connection.Message.RequestLivingObjectMessage(message.Id), GameLibrary.Connection.GameMessageImportance.UnImportant);
                }
            }
        }

        private static void handleUpdateObjectHealthMessage(NetIncomingMessage _Im)
        {
            var message = new UpdateObjectHealthMessage(_Im);

            var timeDelay = (float)(NetTime.Now - _Im.SenderConnection.GetLocalTime(message.MessageTime));

            if (GameLibrary.Map.World.World.world != null)
            {
                GameLibrary.Object.LivingObject var_LivingObject = (GameLibrary.Object.LivingObject)GameLibrary.Map.World.World.world.getObject(message.Id);
                if (var_LivingObject != null)
                {
                    var_LivingObject.HealthPoints = message.Health;
                    var_LivingObject.MaxHealthPoints = message.MaxHealth;
                    var_LivingObject.damage(0);
                }
                else
                {
                    //GameLibrary.Logger.Logger.LogErr("Object mit Id: " + message.Id + " konnte nicht im Quadtree gefunden werden -> Health wird nicht geupdatet");
                    Configuration.networkManager.addEvent(new GameLibrary.Connection.Message.RequestLivingObjectMessage(message.Id), GameLibrary.Connection.GameMessageImportance.UnImportant);
                }
            }
        }

        private static void handleRemoveObjectMessage(NetIncomingMessage _Im)
        {
            var message = new RemoveObjectMessage(_Im);

            var timeDelay = (float)(NetTime.Now - _Im.SenderConnection.GetLocalTime(message.MessageTime));

            GameLibrary.Object.Object var_Object = GameLibrary.Map.World.World.world.getObject(message.Id);
            if (var_Object != null)
            {
                GameLibrary.Map.World.World.world.removeObjectFromWorld(var_Object);
            }
            else
            {
                GameLibrary.Logger.Logger.LogErr("Object mit Id: " + message.Id + " konnte nicht im Quadtree gefunden werden -> Wurde nicht gelöscht");
            }
        }

        private static void handleUpdateCreatureInventoryMessage(NetIncomingMessage _Im)
        {
            var message = new UpdateCreatureInventoryMessage(_Im);

            var timeDelay = (float)(NetTime.Now - _Im.SenderConnection.GetLocalTime(message.MessageTime));

            GameLibrary.Object.Object var_Object = GameLibrary.Map.World.World.world.getObject(message.Id);
            if (var_Object != null)
            {
                if (var_Object is GameLibrary.Object.CreatureObject)
                {
                    ((GameLibrary.Object.CreatureObject)var_Object).Inventory = message.Inventory;
                }
            }
            else
            {
                //GameLibrary.Logger.Logger.LogErr("Object mit Id: " + message.Id + " konnte nicht im Quadtree gefunden werden -> Inventar nicht geupdatet");
                Configuration.networkManager.addEvent(new GameLibrary.Connection.Message.RequestLivingObjectMessage(message.Id), GameLibrary.Connection.GameMessageImportance.UnImportant);
            }
        }

        private static void handleUpdateAnimatedObjectBodyMessage(NetIncomingMessage _Im)
        {
            var message = new UpdateAnimatedObjectBodyMessage(_Im);

            var timeDelay = (float)(NetTime.Now - _Im.SenderConnection.GetLocalTime(message.MessageTime));

            GameLibrary.Object.Object var_Object = GameLibrary.Map.World.World.world.getObject(message.Id);
            if (var_Object != null)
            {
                if (var_Object is GameLibrary.Object.CreatureObject)
                {
                    ((GameLibrary.Object.CreatureObject)var_Object).Body = message.Body;
                }
            }
            else
            {
                //GameLibrary.Logger.Logger.LogErr("Object mit Id: " + message.Id + " konnte nicht im Quadtree gefunden werden -> Equipment nicht geupdatet");
                Configuration.networkManager.addEvent(new GameLibrary.Connection.Message.RequestLivingObjectMessage(message.Id), GameLibrary.Connection.GameMessageImportance.UnImportant);
            }
        }

        private static void handleUpdateRacesMessage(NetIncomingMessage _Im)
        {
            var message = new UpdateRacesMessage(_Im);

            var timeDelay = (float)(NetTime.Now - _Im.SenderConnection.GetLocalTime(message.MessageTime));

            GameLibrary.Factory.BehaviourFactory.behaviourFactory.Races = message.races;
        }
    }
}
