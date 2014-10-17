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
#endregion

namespace GameLibrary.Object.Equipment
{
    [Serializable()]
    public class EquipmentArmor : EquipmentObject
    {
        private ArmorEnum armorEnum;

        public ArmorEnum ArmorEnum
        {
          get { return armorEnum; }
          set { armorEnum = value; }
        }

        private int normalArmor;

        public int NormalArmor
        {
            get { return normalArmor; }
            set { normalArmor = value; }
        }

        private List<Map.World.SearchFlags.Searchflag> searchFlags;

        public List<Map.World.SearchFlags.Searchflag> SearchFlags
        {
            get { return searchFlags; }
            set { searchFlags = value; }
        }

        public EquipmentArmor()
        {
            searchFlags = new List<Map.World.SearchFlags.Searchflag>();
        }

        public EquipmentArmor(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        {
            this.armorEnum = (ArmorEnum)info.GetValue("armorEnum", typeof(ArmorEnum));

            this.normalArmor = (int)info.GetValue("normalArmor", typeof(int));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("armorEnum", armorEnum, typeof(ArmorEnum));

            info.AddValue("normalArmor", normalArmor, typeof(int));

            base.GetObjectData(info, ctxt);
        }

        public override void update(GameTime _GameTime)
        {

        }
    }
}
