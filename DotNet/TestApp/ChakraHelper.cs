using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    public static class ChakraHelper
    {
        public static ChakraContext CreateChakra()
        {

            ChakraRuntime runtime = ChakraRuntime.Create();
            ChakraContext context = runtime.CreateContext(true);

            return context;
        }
    }
}
