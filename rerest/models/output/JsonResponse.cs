using System.Net;
using System.Runtime.Serialization;
using rerest.models.output.exceptions;

namespace rerest.models.output
{
    [DataContract]
    public class JsonResponse
    {
        [DataMember]
        public int HttpResponseCode { get { return (int)HttpStatusCode; } }

        [DataMember]
        public int ErrorCode { get { return (int)JsonErrorCode; } }

        public HttpStatusCode HttpStatusCode { get { return JsonErrorCode.HttpStatusCode(); } }

        public JsonErrorCode JsonErrorCode { get; protected set; }

        public JsonResponse(JsonErrorCode ec)
        {
            JsonErrorCode = ec;
        }

        public JsonResponse() : this(JsonErrorCode.None) { }
    }
}
