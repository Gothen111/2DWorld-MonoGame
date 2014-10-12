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

namespace Utility.Corpus
{
    public class Circle
    {
        private Vector3 position;

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }

        private float radius;

        public float Radius
        {
            get { return radius; }
            set { radius = value; }
        }

        public Circle()
        {

        }

        public Circle(Vector3 _Position, float _Radius)
        {
            this.position = _Position;
            this.radius = _Radius;
        }
    }
}
