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
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
#endregion

namespace Utility.Serialization
{
    public class Serializer
    {
        public static bool SerializeObject(string filename, ISerializable objectToSerialize)
        {
            using (Stream stream = File.Open(filename, FileMode.Create))
            {
                using (var gZipStream = new GZipStream(stream, CompressionMode.Compress))
                {
                    using (StreamWriter streamWriter = new StreamWriter(gZipStream))
                    {
                        streamWriter.Write(SerializeObjectToString(objectToSerialize));
                        streamWriter.Flush();
                    }
                }
            }
            return true;
        }

        public static ISerializable DeSerializeObject(string filename)
        {
            ISerializable objectToSerialize;
            using (Stream stream = File.Open(filename, FileMode.Open))
            {
                using (var gZipStream = new GZipStream(stream, CompressionMode.Decompress))
                {
                    using (StreamReader streamReader = new StreamReader(gZipStream))
                    {
                        objectToSerialize = DeserializeObjectFromString<ISerializable>(streamReader.ReadToEnd());
                    }
                }
            }
            return objectToSerialize;
        }

        public static string SerializeObjectToString(ISerializable objectToSerialize)
        {
            Newtonsoft.Json.JsonSerializerSettings settings = new Newtonsoft.Json.JsonSerializerSettings { TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All };
            return Compress(Newtonsoft.Json.JsonConvert.SerializeObject(objectToSerialize, settings));
        }

        public static T DeserializeObjectFromString<T>(string objectToDeserialize)
        {
            Newtonsoft.Json.JsonSerializerSettings settings = new Newtonsoft.Json.JsonSerializerSettings { TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All };
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(Decompress(objectToDeserialize), settings);
        }

        public static string Compress(string s)
        {
            var bytes = Encoding.Unicode.GetBytes(s);
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    msi.CopyTo(gs);
                }
                return Convert.ToBase64String(mso.ToArray());
            }
        }

        public static string Decompress(string s)
        {
            var bytes = Convert.FromBase64String(s);
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    gs.CopyTo(mso);
                }
                return Encoding.Unicode.GetString(mso.ToArray());
            }
        }

        public static Stream GenerateStreamFromString(String str)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(str);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
