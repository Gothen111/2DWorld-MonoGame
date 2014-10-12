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

namespace Utility.Random
{
    public class Random
    {
        public static System.Security.Cryptography.RNGCryptoServiceProvider c = new System.Security.Cryptography.RNGCryptoServiceProvider();

        public static int GenerateGoodRandomNumber(int min, int max)
        {
            
            // Ein integer benötigt 4 Byte
            byte[] randomNumber = new byte[4];
            // dann füllen wir den Array mit zufälligen Bytes
            c.GetBytes(randomNumber);
            // schließlich wandeln wir den Byte-Array in einen Integer um
            int result = Math.Abs(BitConverter.ToInt32(randomNumber, 0));
            // da bis jetzt noch keine Begrenzung der Zahlen vorgenommen wurde,
            // wird diese Begrenzung mit einer einfachen Modulo-Rechnung hinzu-
            // gefügt
            return result % max + min;
        }
    }
}
