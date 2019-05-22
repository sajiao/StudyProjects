using System;
using System.Collections.Generic;
using System.Text;

namespace DotNet.Common.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class MapNameAttribute : Attribute
    {
        public string Name { get; set; }

        public string MapName { get; set; }

        public MapNameAttribute(string name, string mapName)
        {
            this.Name = name;
            this.MapName = mapName;
        }
    }
}
