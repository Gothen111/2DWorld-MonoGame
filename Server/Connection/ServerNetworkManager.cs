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
using GameLibrary.Connection;
using Lidgren.Network;
using GameLibrary.Connection.Message;
#endregion

namespace Server.Connection
{
    public class ServerNetworkManager : NetworkManager
    {
        private NetServer netServer;

        public override void Start(String _Ip, String _Port)
        {
 	        base.Start(_Ip, _Port);

            var config = new NetPeerConfiguration("2DWorld")
            {
                Port = Int32.Parse(_Port),//"14242"
                //SimulatedMinimumLatency = 0.2f,
                //SimulatedLoss = 0.1f
            };
            /*config.EnableMessageType(NetIncomingMessageType.WarningMessage);
            config.EnableMessageType(NetIncomingMessageType.VerboseDebugMessage);
            config.EnableMessageType(NetIncomingMessageType.ErrorMessage);
            config.EnableMessageType(NetIncomingMessageType.Error);
            config.EnableMessageType(NetIncomingMessageType.DebugMessage);
            config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);*/
            netServer = new NetServer(config);
            netServer.Start();

            this.serverClients = new List<Client>();
        }

        public override NetIncomingMessage ReadMessage()
        {
            return netServer.ReadMessage();
        }

        public override void SendMessage(IGameMessage _IGameMessage, GameMessageImportance _Importance)
        {
            NetOutgoingMessage om = netServer.CreateMessage();
            om.Write((byte)_IGameMessage.MessageType);
            _IGameMessage.Encode(om);

            netServer.SendToAll(om, _Importance == GameMessageImportance.VeryImportant ? NetDeliveryMethod.ReliableOrdered : NetDeliveryMethod.Unreliable); // ReliableUnordered
        }

        public override void SendMessageToClient(IGameMessage _IGameMessage, Client _Client)
        {
            NetOutgoingMessage om = netServer.CreateMessage();
            om.Write((byte)_IGameMessage.MessageType);
            _IGameMessage.Encode(om);
            foreach (NetConnection connection in this.netServer.Connections)
            {
                if (connection.RemoteEndPoint == _Client.IPEndPoint)
                {
                    this.netServer.SendMessage(om, connection, NetDeliveryMethod.ReliableOrdered); // ReliableUnordered // Unreliable
                    break;
                }
            }
        }

        public override void UpdateSendingEvents()
        {
            for (int i = 0; i < this.LastIndex; i++)
            {
                //if (EventList[i] != null)
                //{
                IGameMessage var_IGameMessage = EventList[i].IGameMessage;
                GameMessageImportance var_Importance = EventList[i].Importance;

                SendMessage(var_IGameMessage, var_Importance);

                //TODO: Position des Objektes fehlt natürich noch ;D
                //this.sendMessageToClientsInRange(var_IGameMessage, var_Importance);

                //}
                //Event.EventList.Remove(EventList[i]);
                //i -= 1;
            }
            this.LastIndex = 0;
        }

        //TODO: Position des Objektes fehlt natürich noch ;D
        private void sendMessageToClientsInRange(IGameMessage _IGameMessage, GameMessageImportance _GameMessageImportance)
        {
            int var_Range = 500;

            foreach (Client var_Client in this.serverClients)
            {
                if (var_Client.PlayerObject != null)
                {
                    int var_Distance = (int) Math.Sqrt(Math.Pow(var_Client.PlayerObject.Position.X, 2) + Math.Pow(var_Client.PlayerObject.Position.Y, 2));
                    if (var_Distance <= var_Range)
                    {
                        this.SendMessageToClient(_IGameMessage, var_Client);
                    }
                }
            }
        }

        public override void update()
        {
            base.update();
            if (this.netServer != null)
            {
                ServerMessageManager.ProcessNetworkMessages();
            }
        }
    }
}
