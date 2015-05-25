using System.Runtime.Serialization;

namespace rerest.models.output.exceptions
{
    [DataContract]
    class ErrorMessage : JsonResponse
    {
        [DataMember]
        public string DeveloperMessage { get; private set; }
        [DataMember]
        public string UserMessage { get; private set; }
        

        public ErrorMessage(int httpResponseCode, int errorCode, string userMessage, string developerMessage) : 
            base(httpResponseCode, errorCode)
        {
            UserMessage = userMessage;
            DeveloperMessage = developerMessage;
        }
    }
}
