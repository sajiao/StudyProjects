using Fleck;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    

    class Program
    {
        static void Main(string[] args)
        {
            var pBuf = new ProtocolBuffer();
            pBuf.Test1();
            Console.ReadLine();
        }

    }
}
