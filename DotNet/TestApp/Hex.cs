using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
   public class Hex
    {
        public static void TestHex()
        {
            byte[] data = { 5, 10, 15, 20, 64, 65, 88, 255 };

            byte[] data16 = { 0x05, 0x10, 0x15, 0x20, 0x64, 0x65, 0x88, 0xFF };

            string hex = BitConverter.ToString(data);
            Console.WriteLine(BitConverter.ToString(data16));
            Console.WriteLine(hex.Replace("-", " "));
            string s = string.Concat(data.Select(b => "0x" + b.ToString("X2") + " "));
            Console.WriteLine(s);

            Console.WriteLine(ToHex(data));
            Console.ReadLine();
        }

        public static string ToHex(byte[] data)
        {
            StringBuilder Result = new StringBuilder(data.Length * 2);
            string HexAlphabet = "0123456789ABCDEF";

            foreach (byte B in data)
            {
                Result.Append(HexAlphabet[(int)(B >> 4)]);
                Result.Append(HexAlphabet[(int)(B & 0xF)]);
            }

            return Result.ToString();
        }

        private static int GetNum()
        {
            int i = 0;
            try
            {
                return i;
            }
            finally
            {
                i++;
                Console.WriteLine("Finally");
            }

        }


    }
}
