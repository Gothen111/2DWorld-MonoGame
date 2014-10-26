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
#endregion

namespace GameLibrary.Map.Block
{
    public enum BlockEnum
    {
        Nothing,
        Ground1,
        Ground2,
        Gras,
        Stone,
        Dirt,
        Sand,
        Ice,
        Desert,
        Forest,
        Wall,
        Hill1_Center,
        Hill1_Corner1,
        Hill1_Corner2,
        Hill1_Corner3,
        Hill1_Corner4,
        Hill1_Left,
        Hill1_Right,
        Hill1_Top,
        Hill1_Bottom,
        Hill1_InsideCorner1,
        Hill1_InsideCorner2,
        Hill1_InsideCorner3,
        Hill1_InsideCorner4
        //Water,
        //Lava,
        //Swamp
    }
}
