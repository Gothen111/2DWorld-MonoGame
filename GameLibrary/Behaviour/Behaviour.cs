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
    [Serializable()]
    abstract public class Behaviour<E, T> :ISerializable
    {
        protected List<BehaviourItem<E>> behaviour;

        public Behaviour()
        {
            behaviour = new List<BehaviourItem<E>>();
        }

        public Behaviour(T _type) : this()
        {
            this.type = _type;
        }

        public Behaviour(SerializationInfo info, StreamingContext ctxt)
        {
            this.type = (T)info.GetValue("type", typeof(T));
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("type", this.Type, typeof(T));
        }

        public List<BehaviourItem<E>> BehaviourMember
        {
            get { return behaviour; }
            set { behaviour = value; }
        }

        protected T type;

        public T Type
        {
            get { return type; }
            set { type = value; }
        }

        public void addItem(BehaviourItem<E> item)
        {
            behaviour.Add(item);
        }

        public int getValueForItem(E item)
        {
            foreach (BehaviourItem<E> var_Item in this.behaviour)
            {
                if (var_Item.Item.Equals(item))
                    return var_Item.Value;
            }
            return -1;
        }
    }
}
