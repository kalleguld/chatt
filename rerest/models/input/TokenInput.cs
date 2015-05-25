using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace rerest.models.input
{
    [DataContract]
    public class TokenInput
    {
        [DataMember]
        public Guid Guid { get; set; }
    }
}
