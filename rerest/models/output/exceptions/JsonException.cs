using System.Net;
using System.ServiceModel.Web;

namespace rerest.models.output.exceptions
{
    class JsonException : WebFaultException<JsonError>
    {
        public JsonException(int httpErrorCode, int errorCode, string errorMessage) :
            base(new JsonError(
                httpErrorCode, 
                errorCode, 
                errorMessage), 
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
