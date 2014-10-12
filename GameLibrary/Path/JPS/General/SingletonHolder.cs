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

namespace GameLibrary.Path.JPS.General
{
    public class SingletonHolder<T> where T : new()
    {
        private static T m_instance;

        private SingletonHolder()
        {
        }

        public static T Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new T();
                }
                return m_instance;
            }
        }
    }
}