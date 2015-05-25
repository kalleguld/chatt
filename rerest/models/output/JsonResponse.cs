using System.Runtime.Serialization;

namespace rerest.models.output
{
    [DataContract]
    public class JsonResponse
    {
        [DataMember]
        public int HttpResponseCode { get; set; }

        [DataMember]
        public int ErrorCode { get; set; }

        public JsonResponse() : this(200, 0)
        { }

        public JsonResponse(int httpResponseCode, int errorCode)
        {
            HttpResponseCode = httpResponseCode;
            ErrorCode = errorCode;
        }
    }
}
