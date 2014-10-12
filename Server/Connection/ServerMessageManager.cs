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
using Lidgren.Network;
using GameLibrary.Configuration;
using System.Net;
using GameLibrary.Connection;
using GameLibrary.Connection.Message;
#endregion

namespace Server.Connection
{
    class ServerMessageManager
    {
        /// <summary>
        /// Update für jeden Tick
        /// </summary>
        public void Update()
        {
            ProcessNetworkMessages();
        }

        /// <summary>
        /// Bearbeitet Netzwerk Messages
        /// </summary>
        public static void ProcessNetworkMessages()
        {
            NetIncomingMessage im;
            while ((im = Configuration.networkManager.ReadMessage()) != null)
            {
                switch (im.MessageType)
                {
                    case NetIncomingMessageType.VerboseDebugMessage:
                    case NetIncomingMessageType.DebugMessage:
                    case NetIncomingMessageType.WarningMessage:
                    case NetIncomingMessageType.ErrorMessage:
                        GameLibrary.Logger.Logger.LogInfo(im.SenderEndPoint + im.ReadString());
                        break;
                    case NetIncomingMessageType.StatusChanged:
                        switch ((NetConnectionStatus)im.ReadByte())
                        {
                            case NetConnectionStatus.Connected:
                                GameLibrary.Logger.Logger.LogInfo(im.SenderEndPoint + " Connected");

                                OnClientConnectToServer(im.SenderEndPoint);

                                break;
                            case NetConnectionStatus.Disconnected:
                                GameLibrary.Logger.Logger.LogInfo(im.SenderEndPoint + " Disconnected");

                                OnClientDisconnectFromServer(im.SenderEndPoint);

                                break;
                            case NetConnectionStatus.RespondedAwaitingApproval:
                                im.SenderConnection.Approve();
                                break;
                        }
                        break;
                    case NetIncomingMessageType.Data:
                        var gameMessageType = (EIGameMessageType)im.ReadByte();

                        ServerIGameMessageManager.OnClientSendIGameMessage(gameMessageType, im);

                        break;
                }
                Configuration.networkManager.Recycle(im);
            }
        }

        /// <summary>
        /// Bearbeitet falls ein Client Connected
        /// </summary>
        public static void OnClientConnectToServer(IPEndPoint _IPEndPoint)
        {
            //GameLibrary.Connection.Event.EventList.Add(new GameLibrary.Connection.Event(new UpdateChunkMessage(GameLibrary.Model.Map.World.World.world.getRegion(0).getChunk(0)), GameLibrary.Connection.GameMessageImportance.VeryImportant));
            Client var_Client = new Client(_IPEndPoint);
            Configuration.networkManager.addClient(var_Client);
            //ServerNetworkManager.serverNetworkManager.SendMessageToClient(new UpdateRegionMessage(GameLibrary.Model.Map.World.World.world.getRegion(0)), var_Client);
        }

        /// <summary>
        /// Bearbeitet falls ein Client aus welchem Grund auch immer Disconnected
        /// </summary>
        public static void OnClientDisconnectFromServer(IPEndPoint _IPEndPoint)
        {
            Client var_Client = Configuration.networkManager.getClient(_IPEndPoint);
            Configuration.networkManager.removeClient(var_Client);
        }
    }
}
