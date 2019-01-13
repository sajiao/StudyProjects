using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
   public class FixExtension
    {
        public string Meaning { get; set; }

        public List<FixWord> Words { get; set; }
    }

    public class FixWord
    {
        public string Word { get; set; }

        public string ZhDesc { get; set; }
       
        public string PhoneticSymbolUK { get; set; }

        public string PhoneticSymbolUKUrl { get; set; }
       
        public string PhoneticSymbolUS { get; set; }

        public string PhoneticSymbolUSUrl { get; set; }
    }
}
