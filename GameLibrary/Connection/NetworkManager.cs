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
using System.Threading;
using Lidgren.Network;
using GameLibrary.Connection.Message;
using System.Net;
using GameLibrary.Object;
#endregion

namespace GameLibrary.Connection
{
    public class NetworkManager
    {
        public Client client;
        public List<Client> serverClients;

        Thread workerThread;

        public int LastIndex;
        public int LastIndexMax;
        public Event[] EventList;

        public NetworkManager()
        {
            this.LastIndex = 0;
            this.LastIndexMax = 100;
            this.EventList = new Event[this.LastIndexMax];
        }

        public virtual void Start(String _Ip, String _Port)
        {
            if (!Configuration.Configuration.isSinglePlayer)
            {
                this.workerThread = new Thread(this.updateThread);
                this.workerThread.Start();
            }
        }

        public virtual void Connect(String _Ip, String _Port)
        {
        }

        public virtual void Disconnect()
        {
        }

        public virtual NetIncomingMessage ReadMessage()
        {
            return null;
        }

        public virtual void Recycle(NetIncomingMessage im)
        {
        }

        public virtual NetOutgoingMessage CreateMessage()
        {
            return null;
        }

        public virtual void SendMessage(IGameMessage _IGameMessage, GameMessageImportance _Importance)
        {
        }

        public virtual void SendMessageToClient(IGameMessage _IGameMessage, Client _Client)
        {
        }

        public virtual void Dispose()
        {
        }

        public void addEvent(IGameMessage _IGameMessage)
        {
            this.addEvent(_IGameMessage, GameMessageImportance.UnImportant);
        }

        public virtual void addEvent(IGameMessage _IGameMessage, GameMessageImportance _GameMessageImportance)
        {
            Event var_Event = new Event(_IGameMessage, _GameMessageImportance);
            if (this.LastIndex < this.LastIndexMax)
            {
                this.EventList[this.LastIndex] = var_Event;
                this.LastIndex += 1;
            }
        }

        public virtual void UpdateSendingEvents()
        {
            for (int i = 0; i < this.LastIndex; i++)
            {
                //if (EventList[i] != null)
                //{
                    IGameMessage var_IGameMessage = EventList[i].IGameMessage;
                    GameMessageImportance var_Importance = EventList[i].Importance;

                    SendMessage(var_IGameMessage, var_Importance);
                //}
                //Event.EventList.Remove(EventList[i]);
                //i -= 1;
            }
            this.LastIndex = 0;
        }

        public void updateThread()
        {
            while (true)
            {
                this.update();
                Thread.Sleep(1);
            }
        }

        public virtual void update()
        {
            this.UpdateSendingEvents();
        }

        public void addClient(Client _Client)
        {
            serverClients.Add(_Client);
        }

        public void removeClient(Client _Client)
        {
            serverClients.Remove(_Client);
        }

        public Client getClient(IPEndPoint _IPEndPoint)
        {
            foreach (Client var_Client in serverClients)
            {
                if (var_Client.IPEndPoint.Equals(_IPEndPoint))
                {
                    return var_Client;
                }
            }
            return null;
        }

        public Client getClient(PlayerObject _PlayerObject)
        {
            foreach (Client var_Client in serverClients)
            {
                if (var_Client.PlayerObject != null)
                {
                    if (var_Client.PlayerObject.Equals(_PlayerObject))
                    {
                        return var_Client;
                    }
                }
            }
            return null;
        }
    }
}
