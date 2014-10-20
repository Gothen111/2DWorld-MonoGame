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
using GameLibrary.Enums;
using GameLibrary.Object.ObjectEnums;
using GameLibrary.Object.Animation.Animations;
#endregion

namespace GameLibrary.Object.Body
{
    [Serializable()]
    public class BodyPart : ISerializable
    {
        private Vector3 position;

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }

        private Color drawColor;

        public Color DrawColor
        {
            get { return drawColor; }
            set { drawColor = value; }
        }

        private String texturePath;

        public String TexturePath
        {
            get { return texturePath; }
            set { texturePath = value; }
        }

        private Animation.AnimatedObjectAnimation animation;

        public Animation.AnimatedObjectAnimation Animation
        {
            get { return animation; }
            set { animation = value; }
        }

        private DirectionEnum direction;

        public DirectionEnum Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        private Vector3 size;

        public Vector3 Size
        {
            get { return size; }
            set { size = value; }
        }

        private Vector2 standartTextureShift;

        public Vector2 StandartTextureShift
        {
            get { return standartTextureShift; }
            set { standartTextureShift = value; }
        }

        private float scale;

        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        private EquipmentObject equipment;

        public EquipmentObject Equipment
        {
            get { return equipment; }
            set { equipment = value; }
        }

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private List<ItemEnum> acceptedItemTypes;

        public List<ItemEnum> AcceptedItemTypes
        {
            get { return acceptedItemTypes; }
            set { acceptedItemTypes = value; }
        }

        private float lightLevel;

        public float LightLevel
        {
            get { return lightLevel; }
            set { lightLevel = value; }
        }

        public BodyPart(int _Id, Vector3 _Position, Color _Color, String _TexturePath)
        {
            this.position = _Position;
            this.drawColor = _Color;
            this.texturePath = _TexturePath;
            this.direction = DirectionEnum.Down;
            this.animation = new StandAnimation(this);
            this.scale = 1.0f;
            this.size = new Vector3(32, 32, 0);
            this.equipment = null;
            this.id = _Id;
            this.acceptedItemTypes = new List<ItemEnum>();
            this.standartTextureShift = new Vector2(0, 0);
            this.lightLevel = 1.0f;
        }

        public BodyPart(SerializationInfo info, StreamingContext ctxt)
        {
            this.position = (Vector3)info.GetValue("position", typeof(Vector3));
            this.drawColor = (Color)info.GetValue("drawColor", typeof(Color));
            this.texturePath = (String)info.GetValue("texturePath", typeof(String));
            this.standartTextureShift = (Vector2)info.GetValue("standartTextureShift", typeof(Vector2));
            this.scale = (float)info.GetValue("scale", typeof(float));
            this.size = (Vector3)info.GetValue("size", typeof(Vector3));
            this.equipment = (EquipmentObject)info.GetValue("equipment", typeof(EquipmentObject));
            this.id = (int)info.GetValue("id", typeof(int));
            this.acceptedItemTypes = (List<ItemEnum>)info.GetValue("acceptedItemTypes", typeof(List<ItemEnum>));

            this.animation = new StandAnimation(this);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("position", this.position, typeof(Vector3));
            info.AddValue("drawColor", this.drawColor, typeof(Color));
            info.AddValue("texturePath", this.texturePath, typeof(String));
            info.AddValue("standartTextureShift", this.standartTextureShift, typeof(Vector2));
            info.AddValue("scale", this.scale, typeof(float));
            info.AddValue("size", this.size, typeof(Vector3));
            info.AddValue("equipment", this.equipment, typeof(EquipmentObject));
            info.AddValue("id", this.id, typeof(int));
            info.AddValue("acceptedItemTypes", this.acceptedItemTypes, typeof(List<ItemEnum>));
        }

        public void update(GameTime _GameTime)
        {
            this.animation.update();
            if (this.equipment != null)
            {
                this.equipment.update(_GameTime);
            }
        }

        public bool setEquipmentObject(EquipmentObject _EquipmentObject)
        {
            if (this.equipment != null)
            {
                return false;
            }
            else
            {
                this.equipment = _EquipmentObject;
                return true;
            }
        }

        /*public void setAnimation(Animation.AnimatedObjectAnimation _Animation)
        {
            this.animation = _Animation;
            if(this.equipment!=null)
            {
                this.equipment.A
        }*/

        private void drawEquipment(Microsoft.Xna.Framework.Graphics.GraphicsDevice _GraphicsDevice, Microsoft.Xna.Framework.Graphics.SpriteBatch _SpriteBatch, Vector2 _BodyCenter)
        {
            if(this.equipment != null)
            {
                this.equipment.Position = new Vector3(_BodyCenter, 0);

                int var_AmountRed = (int)(this.drawColor.R * this.LightLevel);
                int var_AmountGreen = (int)(this.drawColor.G * this.LightLevel);
                int var_AmountBlue = (int)(this.drawColor.B * this.LightLevel);
                Color var_DrawColor = new Color(var_AmountRed, var_AmountGreen, var_AmountBlue);

                this.equipment.drawWearingEquipment(_GraphicsDevice, _SpriteBatch, var_DrawColor, this.animation);
            }
        }

        public void draw(Microsoft.Xna.Framework.Graphics.GraphicsDevice _GraphicsDevice, Microsoft.Xna.Framework.Graphics.SpriteBatch _SpriteBatch, Vector2 _BodyCenter)
        {
            Vector2 var_Position = new Vector2(this.position.X + _BodyCenter.X, this.position.Y + _BodyCenter.Y);

            int var_AmountRed = (int)(this.drawColor.R * this.LightLevel);
            int var_AmountGreen = (int)(this.drawColor.G * this.LightLevel);
            int var_AmountBlue = (int)(this.drawColor.B * this.LightLevel);
            Color var_DrawColor = new Color(var_AmountRed, var_AmountGreen, var_AmountBlue);

            if (!this.animation.graphicPath().Equals(""))
            {
                _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture[this.animation.graphicPath()], var_Position, this.animation.sourceRectangle(), var_DrawColor, 0f, Vector2.Zero, new Vector2(this.scale, this.scale), SpriteEffects.None, 1.0f);
            }
            this.drawEquipment(_GraphicsDevice, _SpriteBatch, _BodyCenter);
        }
    }
}
