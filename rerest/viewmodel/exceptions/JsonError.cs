using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.ServiceModel.Web;
using rerest.jsonBase;
using rerest.viewmodel;

namespace rerest.viewmodel.exceptions
{
    [DataContract]
    public class JsonError : JsonResponse
    {
        [DataMember(Name = "errorMessage")]
        public string ErrorMessage { get; protected set; }

        public JsonError(JsonResponseCode ec) : base(ec)
        {
            ErrorMessage = ec.ErrorMessage();
        }

        public WebFaultException<JsonError> GetException()
        {
            return GetException(this);
        } 

        protected WebFaultException<T> GetException<T>(T err) where T : JsonError
        {
            return new WebFaultException<T>(err, err.HttpStatusCode);
        } 
    }
}
