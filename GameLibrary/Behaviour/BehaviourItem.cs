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

namespace GameLibrary.Behaviour
{
 
    public class BehaviourItem <E>
    {
        protected E item;
        public E Item{
            get { return item; }
        }

        protected int value;
        public int Value{
            get{ return value; }
            set{ this.value = value; }
        }

        public void addToValue(int modifier)
        {
            value += modifier;
        }

        public BehaviourItem(E _item, int _value)
        {
            item = _item;
            value = _value;
        }
    }
}
