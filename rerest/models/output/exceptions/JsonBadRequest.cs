using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rerest.models.output.exceptions
{
    class JsonBadRequest : JsonException
    {
        public JsonBadRequest(string developerMessage) : base(400, 400, "", developerMessage)
        {
        }
    }
}
