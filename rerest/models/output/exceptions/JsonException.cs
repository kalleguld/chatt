using System.Net;
using System.ServiceModel.Web;

namespace rerest.models.output.exceptions
{
    class JsonException : WebFaultException<ErrorMessage>
    {
        public JsonException(int httpErrorCode, int errorCode, string userMessage, string developerMessage) :
            base(new ErrorMessage(
                httpErrorCode, 
                errorCode, 
                userMessage, 
                developerMessage), 
            GetHttpStatusCode(httpErrorCode)) 
        { }

        private static HttpStatusCode GetHttpStatusCode(int number)
        {
            switch (number)
            {
                case 200:
                    return HttpStatusCode.OK;
                case 400:
                    return HttpStatusCode.BadRequest;
                case 403:
                    return HttpStatusCode.Forbidden;
                case 404:
                    return HttpStatusCode.NotFound;
                default:
                    return HttpStatusCode.InternalServerError;
            }
        }
    }
}
