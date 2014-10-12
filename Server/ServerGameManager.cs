using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLibrary.Connection;
using GameLibrary.Configuration;
using GameLibrary;
using Server.Commands;
using Server.Connection;

namespace Server
{
    public class ServerGameManager : GameManager
    {
        public override void startDedicatedServer()
        {
            Configuration.isSinglePlayer = false;
            Configuration.isHost = true;
            Configuration.isDedicatedServer = true;

            Configuration.commandManager = new ServerCommandManager();
            Configuration.networkManager = new ServerNetworkManager();

            Configuration.networkManager.client = new GameLibrary.Connection.Client();

            GameLibrary.Map.World.World.world = new GameLibrary.Map.World.World("Welt");
        }
    }
}
