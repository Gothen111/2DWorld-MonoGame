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
using GameLibrary.Object.Body;
#endregion

namespace GameLibrary.Object.Animation.Animations
{
    public class StandAnimation : AnimatedObjectAnimation
    {
        public StandAnimation()
        {

        }

        public StandAnimation(BodyPart _BodyPart)
            : base(_BodyPart, -1, -1)
        {

        }

        //TODO: Problem, man braucht wahrscheinlich 2 standanimationenne, einmal fpü enviomnet und einmal für creature...
        /*
        public override Rectangle sourceRectangle()
        {
            if (this.BodyPart.StandartTextureShift.X != 0)
            {
                return base.sourceRectangle();
            }
            else
            {
                return new Rectangle((int)this.BodyPart.Size.X, this.directionDrawY() * (int)this.BodyPart.Size.Y, (int)this.BodyPart.Size.X, (int)this.BodyPart.Size.Y);
            }      
        }*/
    }
}
