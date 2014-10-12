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

namespace GameLibrary.Object.Equipment.Attack
{
    [Serializable()]
    public class Attack : ISerializable
    {
        #region Attributes

        private float damageMultiplicator;

        public float DamageMultiplicator
        {
          get { return damageMultiplicator; }
          set { damageMultiplicator = value; }
        }

        private int range;

        public int Range
        {
            get { return range; }
            set { range = value; }
        }

        private float attackSpeed;

        public float AttackSpeed
        {
            get { return attackSpeed; }
            set { attackSpeed = value; }
        }

        private float attackSpeedMax;

        public float AttackSpeedMax
        {
            get { return attackSpeedMax; }
            set { attackSpeedMax = value; }
        }

        private AttackType attackType;

        internal AttackType AttackType
        {
            get { return attackType; }
            set { attackType = value; }
        }

        #endregion

        #region Constructors

        public Attack()
        {

        }

        public Attack(int _Range) : this()
        {
            this.Range = _Range;
        }

        public Attack(int _Range, float _DamageMultiplicator)
            : this(_Range)
        {
            this.DamageMultiplicator = _DamageMultiplicator;
        }

        public Attack(int _Range, float _DamageMultiplicator, float _AttackSpeed)
            : this(_Range, _DamageMultiplicator)
        {
            this.AttackSpeed = 0;
            this.AttackSpeedMax = _AttackSpeed;
        }

        public Attack(int _Range, float _DamageMultiplicator, float _AttackSpeed, AttackType _AttackType)
            : this(_Range, _DamageMultiplicator, _AttackSpeed)
        {
            this.AttackType = _AttackType;
        }

        #endregion

        #region Serialisierung

        public Attack(SerializationInfo info, StreamingContext ctxt)
        {
            this.range = (int)info.GetValue("range", typeof(int));
            this.attackSpeed = (float)info.GetValue("attackSpeed", typeof(float));
            this.attackSpeedMax = (float)info.GetValue("attackSpeedMax", typeof(float));
            this.damageMultiplicator = (float)info.GetValue("damageMultiplicator", typeof(float));
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("range", range, typeof(int));
            info.AddValue("attackSpeed", attackSpeed, typeof(float));
            info.AddValue("attackSpeedMax", attackSpeedMax, typeof(float));
            info.AddValue("damageMultiplicator", damageMultiplicator, typeof(float));
        }

        #endregion

        #region Methoden

        public virtual Rectangle getAttackArea()
        {
            return new Rectangle(0, 0, this.Range, this.Range);
        }

        public virtual void update()
        {
            if (attackSpeed < attackSpeedMax)
            {
                attackSpeed++;
            }
        }

        public Boolean isAttackReady()
        {
            return attackSpeed >= attackSpeedMax;
        }

        public virtual void executeAttack()
        {
            attackSpeed = 0;
        }

        #endregion
    }
}
