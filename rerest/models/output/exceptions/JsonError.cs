using System.Runtime.Serialization;

namespace rerest.models.output.exceptions
{
    [DataContract]
    class JsonError : JsonResponse
    {
        [DataMember]
        public string ErrorMessage { get; private set; }
        
        

        public JsonError(int httpResponseCode, int errorCode, string errorMessage) : 
            base(httpResponseCode, errorCode)
        {
            ErrorMessage = errorMessage;
        }
    }
}
