using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Common
{
    public class EnumDescription : Attribute
    {
        private string _text;

        public string Text
        {
            get
            {
                return _text;
            }
        }

        public EnumDescription(string text)
        {
            _text = text;
        }

        public virtual void s() {
        }
    }
}
