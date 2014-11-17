using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace DM.PetShop
{
    [DataContract(Namespace = "urn:dm:wcf:labs:architecure")]
    public class Order
    {
        [DataMember]
        public string Product { get; set; }

        [DataMember]
        public int Quantity { get; set; }
    }
}
