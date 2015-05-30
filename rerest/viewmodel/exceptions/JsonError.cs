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
        [DataMember]
        public string ErrorMessage { get; protected set; }

        public JsonError(JsonResponseCode ec) : base(ec)
        {
            ErrorMessage = ec.ErrorMessage();
        }

        public virtual void Throw()
        {
            GenericThrow(this);
        }

        protected void GenericThrow<T>(T err) where T : JsonResponse
        {
            throw new WebFaultException<T>(err, err.HttpStatusCode);
        }
    }
}
