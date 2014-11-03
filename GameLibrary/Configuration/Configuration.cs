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
using GameLibrary.Commands;
using GameLibrary.Connection;
#endregion

namespace GameLibrary.Configuration
{
    public class Configuration
    {
        public static bool isSinglePlayer;
        public static bool isHost;
        public static bool isDedicatedServer;

        public static GameManager gameManager;
        public static CommandManager commandManager;
        public static NetworkManager networkManager;

        public static int maxMapSize = 1000; //In Blöcken!!!
        public static int seed = 1;
    }
}
