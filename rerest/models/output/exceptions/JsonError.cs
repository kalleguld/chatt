using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.ServiceModel.Web;

namespace rerest.models.output.exceptions
{
    [DataContract]
    public class JsonError : JsonResponse
    {
        [DataMember]
        public virtual string ErrorMessage { get { return JsonErrorCode.ErrorMessage(); } }

        public JsonError(JsonErrorCode ec) : base(ec) { }

        public void Throw()
        {
            GenericThrow(this);
        }

        protected void GenericThrow<T>(T err) where T : JsonResponse
        {
            throw new WebFaultException<T>(err, err.HttpStatusCode);
        }
    }
}
