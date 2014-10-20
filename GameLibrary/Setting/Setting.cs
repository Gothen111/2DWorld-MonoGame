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
#endregion

namespace GameLibrary.Setting
{
    public class Setting
    {
        public static String logInstance;

        public static bool drawWorld = true;
        public static bool drawBlocks = true;
        public static int blockDrawRange = 40;
        public static bool drawObjects = true;

        public static bool createPreEnvironmentObjects = true;
        public static bool drawPreEnvironmentObjects = true;

        public static bool debugMode = true;

        public static int resolutionX = 1024;
        public static int resolutionY = 768;

        public static bool light = true;
        public static bool goodLight = true;

        public static bool lightOne = false;
        public static bool lightTwo = true;
    }
}
