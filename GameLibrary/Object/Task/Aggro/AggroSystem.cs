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
#endregion

namespace GameLibrary.Object.Task.Aggro
{
    [Serializable()]
    public class AggroSystem<E>
    {
        Dictionary<E, float> aggroItems;

        public Dictionary<E, float> AggroItems
        {
            get { return aggroItems; }
            set { aggroItems = value; }
        }

        public AggroSystem()
        {
            aggroItems = new Dictionary<E, float>();
        }

        public AggroSystem(SerializationInfo info, StreamingContext ctxt)
        {
            this.aggroItems = (Dictionary<E, float>)info.GetValue("aggroItems", typeof(Dictionary<E, float>));
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {

            info.AddValue("aggroItems", this.aggroItems, typeof(Dictionary<E, float>));
        }

        public Boolean ContainsUnit(E unit)
        {
            return aggroItems.ContainsKey(unit);
        }

        public void addUnit(E unit, float aggro)
        {
            if (!ContainsUnit(unit))
            {
                aggroItems.Add(unit, aggro);
                sortDictionary();
            }
        }

        private void sortDictionary()
        {
            aggroItems.OrderBy(key => key.Value);
        }

        public void removeUnit(E unit)
        {
            if (ContainsUnit(unit))
            {
                aggroItems.Remove(unit);
            }
        }

        public void addAggro(E unit, float aggro)
        {
            float oldAggro = 0;
            if (ContainsUnit(unit))
            {
                oldAggro = aggroItems[unit];
            }
            removeUnit(unit);
            addUnit(unit, oldAggro + aggro);
            sortDictionary();
        }

        public void modifyAggro(E unit, float modifier)
        {
            if (ContainsUnit(unit))
            {
                float oldAggro = aggroItems[unit];
                removeUnit(unit);
                addUnit(unit, oldAggro * modifier);
            }
        }

        public float getAggro(E unit)
        {
            return aggroItems[unit];
        }

        public E getTarget()
        {
            if (this.aggroItems.Count > 0)
            {
                return aggroItems.Last().Key;
            }
            return default(E);
        }
    }
}
