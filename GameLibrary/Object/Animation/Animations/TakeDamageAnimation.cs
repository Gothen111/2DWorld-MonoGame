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
    public class TakeDamageAnimation : AnimatedObjectAnimation
    {
        public TakeDamageAnimation()
        {

        }

        public TakeDamageAnimation(BodyPart _BodyPart)
            : base(_BodyPart, 0, 20)
        {

        }

        public override Vector3 drawPositionExtra()
        {
            return base.drawPositionExtra() - new Vector3(0, ((float)this.Animation / (float)(this.AnimationMax)) * 6, 0);
        }

        public override Color drawColor()
        {
            if (this.Animation > this.AnimationMax - 5)
            {
                return new Color(255, 160, 160);
            }
            else
            {
                return base.drawColor();
            }
        }
    }
}
