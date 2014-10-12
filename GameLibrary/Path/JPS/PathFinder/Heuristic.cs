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

namespace GameLibrary.Path.JPS.EpPathFinding
{
    public enum HeuristicMode
    {
        MANHATTAN,
        EUCLIDEAN,
        CHEBYSHEV,
        
    };

    public class Heuristic
    {
      public static float Manhattan(int iDx, int iDy)
      {
          return (float)iDx + iDy;
      }

      public static float Euclidean(int iDx, int iDy)
      {
          float tFdx = (float)iDx;
          float tFdy = (float)iDy;
          return (float) Math.Sqrt((double)(tFdx * tFdx + tFdy * tFdy));
      }

      public static float Chebyshev(int iDx, int iDy)
      {
          return (float)Math.Max(iDx, iDy);
      }

    }


}
