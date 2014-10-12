using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;
using Microsoft.Xna.Framework;

namespace Lidgren
{
    public class MonoGame
    {
        public static Vector3 ReadVector3(NetIncomingMessage im)
        {
            float var_X = im.ReadFloat();
            float var_Y = im.ReadFloat();
            float var_Z = im.ReadFloat();
            return new Vector3(var_X, var_Y, var_Z);
        }

        public static void WriteVector3(Vector3 _Vector3, NetOutgoingMessage om)
        {
            om.Write(_Vector3.X);
            om.Write(_Vector3.Y);
            om.Write(_Vector3.Z);
        }
    }
}
