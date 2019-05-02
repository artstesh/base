using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace C2c.Services.Converters
{
    public class ObjectByteConverter
    {
        public static byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
                return null;

            var bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);

                return ms.ToArray();
            }
        }

        public static T ByteArrayToObject<T>(byte[] arrBytes)
        {
            try
            {
                using (var memStream = new MemoryStream())
                {
                    var binForm = new BinaryFormatter();
                    memStream.Write(arrBytes, 0, arrBytes.Length);
                    memStream.Seek(0, SeekOrigin.Begin);
                    return (T) binForm.Deserialize(memStream);
                }
            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }
}