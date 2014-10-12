using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLibrary.Connection;
using GameLibrary.Configuration;
using GameLibrary;
using Client.Commands;
using Client.Connection;

namespace Client
{
    public class ClientGameManager : GameManager
    {
        public override void startSinglePlayerGame()
        {
            Configuration.isSinglePlayer = true;
            Configuration.isHost = false;
            Configuration.isDedicatedServer = false;

            Configuration.commandManager = new ClientCommandManager();
            Configuration.networkManager = new ClientNetworkManager();

            Configuration.networkManager.client = new GameLibrary.Connection.Client();

            GameLibrary.Map.World.World.world = new GameLibrary.Map.World.World("Welt");
        }

        /*public override void startMultiPlayerGame()
        {
            Configuration.isSinglePlayer = false;
            Configuration.isHost = true;
            Configuration.isDedicatedServer = false;

            Configuration.commandManager = new ClientCommandManager();
            Configuration.networkManager = new ClientNetworkManager();

            Configuration.networkManager.client = new GameLibrary.Connection.Client();

            GameLibrary.Map.World.World.world = new GameLibrary.Map.World.World("Welt");
        }*/

        public override void connectToServer()
        {
            Configuration.isSinglePlayer = false;
            Configuration.isHost = false;
            Configuration.isDedicatedServer = false;

            Configuration.commandManager = new ClientCommandManager();
            Configuration.networkManager = new ClientNetworkManager();

            Configuration.networkManager.client = new GameLibrary.Connection.Client();
        }
    }
}
