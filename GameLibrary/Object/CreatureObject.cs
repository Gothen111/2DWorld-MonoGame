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
using GameLibrary.Map.World;
using GameLibrary.Connection;
#endregion

#region Using Statements Class Specific
#endregion

namespace GameLibrary.Object
{
    [Serializable()]
    public class CreatureObject : LivingObject
    {
        private Inventory.Inventory inventory;

        public Inventory.Inventory Inventory
        {
            get { return inventory; }
            set { inventory = value; }
        }

        //protected Skill skill;

        public CreatureObject()
        {
            this.inventory = new Inventory.Inventory();
        }

        public CreatureObject(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        { 
            this.inventory = (Inventory.Inventory)info.GetValue("inventory", typeof(Inventory.Inventory));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("inventory", inventory, typeof(Inventory.Inventory));

            base.GetObjectData(info, ctxt);
        }

        public override void update(GameTime _GameTime)
        {
            base.update(_GameTime);
        }

        public override void attack()
        {
            base.attack();
            this.swingWeapon(GameLibrary.Object.Equipment.Attack.AttackType.Front);
        }

        public void swingWeapon(Equipment.Attack.AttackType _AttackType)
        {
            GameLibrary.Object.Equipment.EquipmentWeapon var_EquipmentWeaponForAttack = this.Body.attack();
            if (var_EquipmentWeaponForAttack != null && var_EquipmentWeaponForAttack.isAttackReady(_AttackType))
	        {
                List<Object> var_Objects = this.getDimensionIsIn().getObjectsInRange(this.Position, var_EquipmentWeaponForAttack.getAttack(_AttackType).Range, var_EquipmentWeaponForAttack.SearchFlags);
                var_Objects.Remove(this);
                foreach (Object var_Object in var_Objects)
                {
                    if (var_Object is LivingObject)
                    {
                        this.attackLivingObject((LivingObject)var_Object, var_EquipmentWeaponForAttack.NormalDamage);
                    }
                }
                if (var_Objects.Count > 0)
                {
                    var_EquipmentWeaponForAttack.executeAttack(_AttackType);
                }
            }
        }
        public override float calculateDamage(float _DamageAmount)
        {
            _DamageAmount = _DamageAmount / ((float)this.Body.getDefence());
            return _DamageAmount;
        }

        public void addItemObjectToInventory(ItemObject _ItemObject)
        {
            if(this.inventory.addItemObjectToInventory(_ItemObject))
            {
                Configuration.Configuration.networkManager.addEvent(new GameLibrary.Connection.Message.UpdateCreatureInventoryMessage(this.Id, this.inventory), GameMessageImportance.VeryImportant);
            }
        }

        public void guiSetItemToInventory(ItemObject _ItemObject, int _FieldId)
        {
            this.inventory.itemDropedInInventory(this, _ItemObject, _FieldId);
            this.inventory.InventoryChanged = true;
        }

        public void setItemFromEquipmentToInventory(int _EquipmentPosition, int _InventoryPosition)
        {
            EquipmentObject var_ToRemove = this.Body.getEquipmentAt(_EquipmentPosition);
            if (this.inventory.addItemObjectToInventoryAt(var_ToRemove, _InventoryPosition))
            {
                this.Body.removeEquipment(var_ToRemove);
                Configuration.Configuration.networkManager.addEvent(new GameLibrary.Connection.Message.UpdateCreatureInventoryMessage(this.Id, this.inventory), GameMessageImportance.VeryImportant);
                Configuration.Configuration.networkManager.addEvent(new GameLibrary.Connection.Message.UpdateAnimatedObjectBodyMessage(this.Id, this.Body), GameMessageImportance.VeryImportant);                    
            }
        }

        public void guiSetItemToEquipment(ItemObject _ItemObject, int _FieldId)
        {
            if (_ItemObject.PositionInInventory != -1)
            {
                if(this.inventory.Items.Contains(_ItemObject))
                {
                    Configuration.Configuration.networkManager.addEvent(new GameLibrary.Connection.Message.CreatureInventoryToEquipmentMessage(this.Id, _ItemObject.PositionInInventory, _FieldId), GameMessageImportance.VeryImportant);
                }
                else
                {
                    //Kommt nicht aus dem Inventar, sondern woadners he. Aus anderem Inv., ode Equip.
                }
            }
            else
            {
                // Kommt aus der world ;)
            }
        }

        public void setItemFromInventoryToEquipment(int _InventoryPosition, int _EquipmentPosition)
        {
            ItemObject var_ToRemove = null;
            foreach (ItemObject var_ItemObject in this.inventory.Items)
            {
                if (var_ItemObject.PositionInInventory == _InventoryPosition)
                {
                    if (var_ItemObject is EquipmentObject)
                    {
                        var_ItemObject.PositionInInventory = _EquipmentPosition;
                        this.Body.setEquipmentObject((EquipmentObject)var_ItemObject);
                        var_ToRemove = var_ItemObject;
                    }
                }
            }
            if (var_ToRemove != null)
            {
                this.inventory.Items.Remove(var_ToRemove);
                Configuration.Configuration.networkManager.addEvent(new GameLibrary.Connection.Message.UpdateCreatureInventoryMessage(this.Id, this.inventory), GameMessageImportance.VeryImportant);
                Configuration.Configuration.networkManager.addEvent(new GameLibrary.Connection.Message.UpdateAnimatedObjectBodyMessage(this.Id, this.Body), GameMessageImportance.VeryImportant);                    
            }
        }

        public override void draw(Microsoft.Xna.Framework.Graphics.GraphicsDevice _GraphicsDevice, Microsoft.Xna.Framework.Graphics.SpriteBatch _SpriteBatch, Microsoft.Xna.Framework.Vector3 _DrawPositionExtra, Microsoft.Xna.Framework.Color _Color)
        {
            //TODO: An das Attribut Scale anpassen
            Vector2 var_PositionShadow = new Vector2(this.Bounds.X + _DrawPositionExtra.X, this.Bounds.Y + _DrawPositionExtra.Y);
             _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture["Character/Shadow"], var_PositionShadow, Color.White);

             Vector2 var_PositionState = new Vector2(this.Position.X, this.Position.Y) + new Vector2(-13, -7);
             //if (this.IsHovered)
             //{
                 if (this is PlayerObject)
                 {
                     _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture["Character/CreatureState"], var_PositionState, Color.BlueViolet);//Color.DarkOrange);
                 }
                 else
                 {
                     _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture["Character/CreatureState"], var_PositionState, Color.Red);
                 }
             //}
            base.draw(_GraphicsDevice, _SpriteBatch, _DrawPositionExtra, _Color);
        }
    }
}
