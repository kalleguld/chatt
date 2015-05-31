using System.Net;
using System.Runtime.Serialization;
using rerest.viewmodel;

namespace rerest.jsonBase
{
    [DataContract]
    public class JsonResponse
    {
        [DataMember(Name = "httpResponseCode")]
        public int HttpResponseCode { get; private set; }

        [DataMember(Name = "errorCode")]
        public int ErrorCode { get; private set; }

        public HttpStatusCode HttpStatusCode { get { return JsonResponseCode.HttpStatusCode(); } }

        public JsonResponseCode JsonResponseCode { get; private set; }

        public JsonResponse(JsonResponseCode ec)
        {
            JsonResponseCode = ec;
            HttpResponseCode = (int)HttpStatusCode;
            ErrorCode = (int)ec;
        }

        public JsonResponse() : this(JsonResponseCode.None) { }
    }
}
