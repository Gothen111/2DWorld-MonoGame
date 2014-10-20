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

namespace GameLibrary.Object
{
    [Serializable()]
    public class EquipmentObject : ItemObject
    {
        public EquipmentObject()
            : base()
        {

        }

        public EquipmentObject(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        {

        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
        }

        public override void update(GameTime _GameTime)
        {
            base.update(_GameTime);
        }

        public virtual void drawWearingEquipment(Microsoft.Xna.Framework.Graphics.GraphicsDevice _GraphicsDevice, Microsoft.Xna.Framework.Graphics.SpriteBatch _SpriteBatch, Microsoft.Xna.Framework.Color _Color, Animation.AnimatedObjectAnimation _Animation)
        {
            try
            {
                _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture[this.Body.MainBody.TexturePath], new Microsoft.Xna.Framework.Vector2(this.Position.X, this.Position.Y), _Animation.sourceRectangle(), _Color/*_Animation.drawColor()*/, 0f, Microsoft.Xna.Framework.Vector2.Zero, new Microsoft.Xna.Framework.Vector2(this.Scale, this.Scale), Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 1.0f);
            }
            catch (Exception e)
            {
            }
        }
    }
}
