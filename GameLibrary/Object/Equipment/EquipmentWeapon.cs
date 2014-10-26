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
using GameLibrary.Factory.FactoryEnums;
#endregion

#region Using Statements Class Specific
#endregion

namespace GameLibrary.Object.Equipment
{
    [Serializable()]
    public class EquipmentWeapon : EquipmentObject
    {
        private WeaponEnum weaponEnum;

        public WeaponEnum WeaponEnum
        {
            get { return weaponEnum; }
            set { weaponEnum = value; }
        }

        private int normalDamage;

        public int NormalDamage
        {
            get { return normalDamage; }
            set { normalDamage = value; }
        }

        private List<Map.World.SearchFlags.Searchflag> searchFlags;

        public List<Map.World.SearchFlags.Searchflag> SearchFlags
        {
            get { return searchFlags; }
            set { searchFlags = value; }
        }

        private List<Attack.Attack> attacks;

        public List<Attack.Attack> Attacks
        {
            get { return attacks; }
            set { attacks = value; }
        }

        public EquipmentWeapon()
        {
            searchFlags = new List<Map.World.SearchFlags.Searchflag>();
            attacks = new List<Attack.Attack>();
        }

        public EquipmentWeapon(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        {
            this.weaponEnum = (WeaponEnum)info.GetValue("weaponEnum", typeof(WeaponEnum));

            this.normalDamage = (int)info.GetValue("normalDamage", typeof(int));

            this.searchFlags = (List<Map.World.SearchFlags.Searchflag>)info.GetValue("searchFlags", typeof(List<Map.World.SearchFlags.Searchflag>));

            this.attacks = (List<Attack.Attack>)info.GetValue("attacks", typeof(List<Attack.Attack>));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("weaponEnum", weaponEnum, typeof(WeaponEnum));

            info.AddValue("normalDamage", normalDamage, typeof(int));

            info.AddValue("searchFlags", this.searchFlags, typeof(List<Map.World.SearchFlags.Searchflag>));

            info.AddValue("attacks", this.attacks, typeof(List<Attack.Attack>));

            base.GetObjectData(info, ctxt);
        }

        public override void update(GameTime _GameTime)
        {
            foreach (Attack.Attack var_Attack in this.attacks)
                var_Attack.update();
        }

        public bool isAttackReady(Attack.AttackType _AttackType)
        {
            foreach (Attack.Attack var_Attack in this.attacks)
            {
                if (var_Attack.isAttackReady() && var_Attack.AttackType.Equals(_AttackType))
                    return true;
            }
            return false;
        }

        public bool executeAttack(Attack.AttackType _AttackType)
        {
            foreach (Attack.Attack var_Attack in this.attacks)
            {
                if (var_Attack.isAttackReady() && var_Attack.AttackType.Equals(_AttackType))
                {
                    var_Attack.executeAttack();
                    return true;
                }
            }
            return false;
        }

        public Attack.Attack getAttack(Attack.AttackType _AttackType)
        {
            foreach (Attack.Attack var_Attack in this.attacks)
            {
                if (var_Attack.AttackType.Equals(_AttackType))
                {
                    return var_Attack;
                }
            }
            Logger.Logger.LogErr("Keine Waffe für Typ " + _AttackType.ToString() + " festgelegt.");
            return null;
        }

    }
}
