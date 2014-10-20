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

namespace Utility.Object
{
    public struct Light
    {
        public Vector3 Position;
        public Color LightColor;
        public float LightRange;

        public Light(Vector3 _Position, Color _Color, float _Range)
        {
            this.Position = _Position;
            this.LightColor = _Color;
            this.LightRange = _Range;
        }
    }
}
