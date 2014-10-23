﻿#region Using Statements Standard
using System;
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

namespace GameLibrary.Map.Chunk.Road
{
    public class RoadPair
    {
        public bool RoadEnd;

        public Vector3 Position;
        public Vector3 Destination;

        public RoadPair Exit;
    }
}
