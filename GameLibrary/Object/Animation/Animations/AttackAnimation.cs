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
    public class AttackAnimation : AnimatedObjectAnimation
    {
        private int currentFrame;

        public AttackAnimation()
        {

        }

        public AttackAnimation(BodyPart _BodyPart)
            : base(_BodyPart, 0, 10)
        {
            this.currentFrame = 0;
        }

        public override void update()
        {
            base.update();
            if (this.Animation <= 0)
            {
                this.currentFrame = 2; // Right
            }
            else if (this.Animation <= this.AnimationMax / 2)
            {
                this.currentFrame = 0; // Left
            }
        }
        public override Rectangle sourceRectangle()
        {
            int var_DrawX = this.currentFrame;

            return new Rectangle(var_DrawX*32,this.directionDrawY()*32,32,32);
        }

        public override string graphicPath()
        {
            return base.graphicPath() + "_Attack";
        }
    }
}
