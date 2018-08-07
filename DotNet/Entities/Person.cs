using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Entities
{
    [ProtoContract]
    public class Person { 
    
       [ProtoMember(1)]
       public int Id { get; set; }
       [ProtoMember(2)]
       public string Name { get; set; }
       [ProtoMember(3)]
       public Address Address { get; set; }
       public override string ToString()
        {
            return string.Format("ID:{0}; Name:{1}; AddressPhone:{2},Zip:{3}, Country:{4}", this.Id.ToString(), this.Name, this.Address?.Phone, this.Address?.Zip, this.Address?.Country);
        }
    }

    [ProtoContract]
    public class Address {
        [ProtoMember(1)]
        public string Phone { get; set; }
        [ProtoMember(2)]
        public string Zip { get; set; }
        [ProtoMember(3)]
        public string Country { get; set; }
    }
}
