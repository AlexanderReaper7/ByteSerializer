using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ByteSerializerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create object
            TestPlayer tp = new TestPlayer(1,2, new List<string>{"TEST","NOTnil"});
            // Read object 
            byte[] arr = ByteSerializer.ObjectToByteArray(tp);
            // Write object
            TestPlayer ntp = (TestPlayer) ByteSerializer.ByteArrayToObject(arr);
            // Stop
            Console.ReadKey(); // PLACE BREAKPOINT HERE!
        }
    }

    [Serializable]
    public class TestPlayer
    {
        public int X;
        public int Y;
        public List<string> NameList;

        public TestPlayer(int x, int y, List<string> nameList)
        {
            X = x;
            Y = y;
            NameList = nameList;
        }
    }



    public static class ByteSerializer
    {
        /// <summary>
        /// Converts an object to a byte array
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] ObjectToByteArray(Object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Converts a byte array to an Object
        /// </summary>
        /// <param name="arrBytes"></param>
        /// <returns></returns>
        public static Object ByteArrayToObject(byte[] arrBytes)
        {
            using (var memStream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                var obj = binForm.Deserialize(memStream);
                return obj;
            }
        }
    }

}
