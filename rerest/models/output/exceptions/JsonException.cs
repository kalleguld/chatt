using System.Net;
using System.ServiceModel.Web;

namespace rerest.models.output.exceptions
{
    class JsonException<T> : WebFaultException<T>
        where T : JsonError
    {
        public JsonException(T error) : base(error, error.HttpStatusCode) { }
    }
}
