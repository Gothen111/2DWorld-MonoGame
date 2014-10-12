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
    class ClientMessageManager
    {
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
                        GameLibrary.Logger.Logger.LogInfo(im.ReadString());
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

                        ClientIGameMessageManager.OnClientSendIGameMessage(gameMessageType, im);

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
            //GameLibrary.Connection.NetworkManager.client = new GameLibrary.Connection.Client();
            //Event.EventList.Add(new Event(new RequestPlayerMessage("Fred"), GameMessageImportance.VeryImportant));
        }

        /// <summary>
        /// Bearbeitet falls ein Client aus welchem Grund auch immer Disconnected
        /// </summary>
        public static void OnClientDisconnectFromServer(IPEndPoint _IPEndPoint)
        {
        }
    }
}
