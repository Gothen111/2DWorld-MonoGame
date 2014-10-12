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

namespace GameLibrary.Object.Body
{
    [Serializable()]
    public class BodyPlant: Body
    {
        public BodyPlant()
            :base()
        {
        }

        public BodyPlant(SerializationInfo info, StreamingContext ctxt)
            :base(info, ctxt)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
        }

        public override void draw(Object _Object, GraphicsDevice _GraphicsDevice, SpriteBatch _SpriteBatch, Vector2 _BodyCenter)
        {
            //String var_RegionType = ((Region)_Object.CurrentBlock.Parent.Parent).RegionEnum.ToString();
            //_SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture["Region/" + var_RegionType + "/" + var_RegionType], var_DrawPosition, new Rectangle((int)(var_Enum-1) * BlockSize, (int)(var_Layer) * BlockSize, BlockSize, BlockSize), var_Color);
        }
    }
}
