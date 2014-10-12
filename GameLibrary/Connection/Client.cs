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
using System.Net;
using GameLibrary.Object;
#endregion

namespace GameLibrary.Connection
{
    public class Client
    {
        IPEndPoint iPEndPoint;

        public IPEndPoint IPEndPoint
        {
            get { return iPEndPoint; }
            set { iPEndPoint = value; }
        }

        PlayerObject playerObject;

        public PlayerObject PlayerObject
        {
            get { return playerObject; }
            set { playerObject = value; }
        }

        private EClientStatus clientStatus;

        public EClientStatus ClientStatus
        {
            get { return clientStatus; }
            set { clientStatus = value; }
        }

        /// <summary>Erzegt einen Clienten für den Server
        /// <para>IPEndPoint _IPEndPoint</para>
        /// </summary>
        public Client(IPEndPoint _IPEndPoint)
        {
            this.iPEndPoint = _IPEndPoint;
            this.clientStatus = EClientStatus.Connected;
        }

        /// <summary>Erzeugt den Clienten für den Clienten
        /// </summary>
        public Client()
        {
            this.clientStatus = EClientStatus.Connected;
        }
    }
}
