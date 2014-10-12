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
#endregion

namespace GameLibrary.Ressourcen
{
    public class ObjectPositionComparer : IComparer<Object.Object>
    {
        public int Compare(Object.Object x, Object.Object y)
        {
            Object.Object var_Object1 = x;
            Object.Object var_Object2 = y;

            if (var_Object1.Position.Y < var_Object2.Position.Y)
            {
                return -1;
            }
            if (var_Object1.Position.Y == var_Object2.Position.Y)
            {
                if (var_Object1.Id < var_Object2.Id)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }
            if (var_Object1.Position.Y > var_Object2.Position.Y)
            {
                return 1;
            }

            return 0;
        }
    }
}
