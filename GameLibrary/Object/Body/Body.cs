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
using GameLibrary.Object.ObjectEnums;
using GameLibrary.Object.Animation.Animations;
using GameLibrary.Enums;
#endregion

namespace GameLibrary.Object.Body
{
    [Serializable()]
    public class Body : ISerializable
    {
        private List<BodyPart> bodyParts;

        public List<BodyPart> BodyParts
        {
            get { return bodyParts; }
            set { bodyParts = value; }
        }

        private Color bodyColor;

        public Color BodyColor
        {
            get { return bodyColor; }
            set { bodyColor = value; }
        }

        private BodyPart mainBody;

        public BodyPart MainBody
        {
            get { return mainBody; }
            set { mainBody = value; }
        }

        private float lightLevel;

        public float LightLevel
        {
            get { return lightLevel; }
            set { lightLevel = value; }
        }

        public Body()
        {         
            this.bodyParts = new List<BodyPart>();
            this.bodyColor = Color.White;

            this.mainBody = new BodyPart(0, new Vector3(0, 0, 0), this.bodyColor, "");
            this.mainBody.AcceptedItemTypes.Add(ItemEnum.Armor);
            this.bodyParts.Add(this.mainBody);
        }

        public Body(SerializationInfo info, StreamingContext ctxt)
        {
            this.bodyParts = new List<BodyPart>();
            this.mainBody = (BodyPart)info.GetValue("mainBody", typeof(BodyPart));
            this.bodyParts.Add(this.mainBody);
            this.bodyColor = (Color)info.GetValue("bodyColor", typeof(Color));
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("mainBody", this.mainBody, typeof(BodyPart));
            info.AddValue("bodyColor", this.bodyColor, typeof(Color));
        }

        protected virtual void onStopWalk()
        {
            //TODO: es soll ja nicht alles sofot gestoppt werden. Z.b beim angriff oä.
            //this.mainBody.Animation = new StandAnimation(this.mainBody);
            foreach (BodyPart var_BodyPart in this.bodyParts)
            {
                var_BodyPart.Animation = new StandAnimation(var_BodyPart);
            }
        }

        public void stopWalk()
        {
            if (this.mainBody.Animation.finishedAnimation() || this.mainBody.Animation is MoveAnimation)
            {
                this.onStopWalk();
            }
        }

        protected virtual void onWalk(Vector3 _Velocity)
        {
            //this.mainBody.Animation = new MoveAnimation(this.mainBody, _Velocity);
            foreach (BodyPart var_BodyPart in this.bodyParts)
            {
                var_BodyPart.Animation = new MoveAnimation(var_BodyPart, _Velocity);
            }
        }

        public void walk(Vector3 _Velocity)
        {
            if (this.mainBody.Animation is MoveAnimation)
            {
            }
            else
            {
                if (this.mainBody.Animation.finishedAnimation())
                {
                    this.onWalk(_Velocity);
                }
            }
        }

        public virtual Equipment.EquipmentWeapon attack()
        {
            this.mainBody.Animation = new AttackAnimation(this.mainBody);
            foreach (BodyPart var_BodyPart in this.bodyParts)
            {
                if (var_BodyPart.Equipment != null)
                {
                    if (var_BodyPart.Equipment is Equipment.EquipmentWeapon)
                    {
                        return (Equipment.EquipmentWeapon)var_BodyPart.Equipment;
                    }
                }
            }
            return null;
        }

        public virtual int getDefence()
        {
            int var_Result = 1;
            foreach (BodyPart var_BodyPart in this.bodyParts)
            {
                if (var_BodyPart.Equipment != null)
                {
                    if (var_BodyPart.Equipment is Equipment.EquipmentArmor)
                    {
                        var_Result += ((Equipment.EquipmentArmor)var_BodyPart.Equipment).NormalArmor;
                    }
                }
            }
            return var_Result;
        }

        public void setDirection(DirectionEnum _Direction)
        {
            foreach (BodyPart var_BodyPart in this.bodyParts)
            {
                var_BodyPart.Direction = _Direction;
            }
        }

        public EquipmentObject getEquipmentAt(int _EquipmentPosition)
        {
            foreach (BodyPart var_BodyPart in this.bodyParts)
            {
                if (var_BodyPart.Id == _EquipmentPosition)
                {
                    return var_BodyPart.Equipment;
                }
            }
            return null;
        }

        public bool containsEquipmentObject(EquipmentObject _EquipmentObject)
        {
            foreach (BodyPart var_BodyPart in this.bodyParts)
            {
                if (var_BodyPart.Equipment == _EquipmentObject)
                {
                    return true;
                }
            }
            return false;
        }

        public bool setEquipmentObject(EquipmentObject _EquipmentObject)
        {
            foreach (BodyPart var_BodyPart in this.bodyParts)
            {
                if (var_BodyPart.Id == _EquipmentObject.PositionInInventory)
                {
                    var_BodyPart.setEquipmentObject(_EquipmentObject);
                }
            }
            return false;
        }

        public bool removeEquipment(EquipmentObject _EquipmentObject)
        {
            foreach (BodyPart var_BodyPart in this.bodyParts)
            {
                if(var_BodyPart.Equipment!=null)
                {
                    if (var_BodyPart.Equipment.Equals(_EquipmentObject))
                    {
                        var_BodyPart.Equipment = null;
                        return true;
                    }
                }
            }
            return false;
        }

        public void setColor(Color _Color)
        {
            this.bodyColor = _Color;
            foreach (BodyPart var_BodyPart in this.bodyParts)
            {
                var_BodyPart.DrawColor = _Color;
            }
        }

        public void setLightLevel(float _LightLevel)
        {
            this.lightLevel = _LightLevel;
            foreach (BodyPart var_BodyPart in this.bodyParts)
            {
                var_BodyPart.LightLevel = _LightLevel;
            }
        }

        public void setSize(Vector3 _Size)
        {
            foreach (BodyPart var_BodyPart in this.bodyParts)
            {
                var_BodyPart.Size = _Size;
            }
        }

        public void update(GameTime _GameTime)
        {
            foreach (BodyPart var_BodyPart in this.bodyParts)
            {
                var_BodyPart.update(_GameTime);
            }
        }

        public void draw(GraphicsDevice _GraphicsDevice, SpriteBatch _SpriteBatch, Vector2 _BodyCenter)
        {
            this.draw(null, _GraphicsDevice, _SpriteBatch, _BodyCenter);
        }

        public virtual void draw(Object _Object, GraphicsDevice _GraphicsDevice, SpriteBatch _SpriteBatch, Vector2 _BodyCenter)
        {
            foreach (BodyPart var_BodyPart in this.bodyParts)
            {
                var_BodyPart.draw(_GraphicsDevice, _SpriteBatch, _BodyCenter);
            }
        }
    }
}
