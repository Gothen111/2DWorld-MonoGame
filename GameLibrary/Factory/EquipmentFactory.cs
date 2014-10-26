#region Using Statements Standard
using System;
using System.Linq;
using System.Text;
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
using GameLibrary.Object;
using GameLibrary.Map.World;
using GameLibrary.Map.Region;
using GameLibrary.Map.Chunk;
using GameLibrary.Map.Block;
using GameLibrary.Factory.FactoryEnums;
using GameLibrary.Object.Equipment;
using GameLibrary.Object.Equipment.Attack;
#endregion

namespace GameLibrary.Factory
{
    public class EquipmentFactory
    {
        public static EquipmentFactory equipmentFactory = new EquipmentFactory();

        private EquipmentFactory()
        {
        }

        public EquipmentObject createEquipmentObject()
        {
            EquipmentObject equipmentObject = new EquipmentObject();
            equipmentObject.Scale = 1;
            equipmentObject.Velocity = new Vector3(0, 0, 0);

            return equipmentObject;
        }

        public EquipmentWeapon createEquipmentWeaponObject(WeaponEnum _WeaponEnum)
        {
            EquipmentWeapon equipmentWeaponObject = new EquipmentWeapon();
            equipmentWeaponObject.Scale = 1;
            equipmentWeaponObject.Velocity = new Vector3(0, 0, 0);
            equipmentWeaponObject.StackMax = 1;
            equipmentWeaponObject.Size = new Microsoft.Xna.Framework.Vector3(32, 32, 0);
            equipmentWeaponObject.ItemEnum = ItemEnum.Weapon;

            switch (_WeaponEnum)
            {
                case WeaponEnum.Sword:
                    {           
                        equipmentWeaponObject.NormalDamage = 4;
                        equipmentWeaponObject.WeaponEnum = _WeaponEnum;
                        Attack var_Attack = new Attack(50, 1.0f, 20.0f, AttackType.Front);
                        equipmentWeaponObject.Attacks.Add(var_Attack);
                        equipmentWeaponObject.Body.MainBody.TexturePath = "Character/Sword";
                        equipmentWeaponObject.ItemIconGraphicPath = "Object/Item/Small/Sword1";
                        //equipmentWeaponObject.SearchFlags.Add(new Map.World.SearchFlags.());
                        break;
                    }
                case WeaponEnum.Spear:
                    {
                        equipmentWeaponObject.NormalDamage = 2;
                        equipmentWeaponObject.WeaponEnum = _WeaponEnum;
                       Attack var_Attack = new Attack(80, 1.0f, 30.0f, AttackType.Front);
                        equipmentWeaponObject.Attacks.Add(var_Attack);
                        
                        break;
                    }
                case WeaponEnum.Paper:
                    {
                        equipmentWeaponObject.NormalDamage = 5;
                        equipmentWeaponObject.WeaponEnum = _WeaponEnum;
                        Attack var_Attack = new Attack(80, 1.0f, 20.0f, AttackType.Front);
                        equipmentWeaponObject.Attacks.Add(var_Attack);
                        
                        break;
                    }
            }

            return equipmentWeaponObject;
        }

        public EquipmentArmor createEquipmentArmorObject(ArmorEnum _ArmorEnum)
        {
            EquipmentArmor equipmentArmorObject = new EquipmentArmor();
            equipmentArmorObject.Scale = 1;
            equipmentArmorObject.Velocity = new Vector3(0, 0, 0);
            equipmentArmorObject.StackMax = 1;
            equipmentArmorObject.Size = new Microsoft.Xna.Framework.Vector3(32, 32, 0);
            equipmentArmorObject.ArmorEnum = _ArmorEnum;
            equipmentArmorObject.ItemEnum = ItemEnum.Armor;

            switch (_ArmorEnum)
            {
                case ArmorEnum.Chest:
                    {
                        equipmentArmorObject.NormalArmor = 1;
                        equipmentArmorObject.ItemIconGraphicPath = "Object/Item/Small/Cloth1";
                        equipmentArmorObject.Body.MainBody.TexturePath = "Character/Cloth1";
                        break;
                    }
                case ArmorEnum.GoldenArmor:
                    equipmentArmorObject.NormalArmor = 10;
                        equipmentArmorObject.ItemIconGraphicPath = "Object/Item/Small/Cloth1";
                        equipmentArmorObject.Body.MainBody.TexturePath = "Character/GoldenArmor";
                    break;
            }

            return equipmentArmorObject;
        }
    }
}
