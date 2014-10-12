#region Using Statements Standard
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
using GameLibrary.Path.AStar;
using GameLibrary.Map.Block;
#endregion

namespace GameLibrary.Path
{
    public class PathNode : IPathNode<System.Object>
    {
        public Int32 X { get; set; }
        public Int32 Y { get; set; }
        public Boolean IsWall { get; set; }
        public Block block { get; set; }

        public bool IsWalkable(System.Object unused)
        {
            return !IsWall;
        }
    }
}
