using System.Runtime.Serialization;
using System.ServiceModel.Web;
using rerest.jsonBase;
using rerest.viewmodel;

namespace rerest.viewmodel.exceptions
{
    [DataContract]
    public abstract class JsonParamError : JsonError
    {
        [DataMember(Name = "parameterName")]
        public string ParameterName { get; private set; }


        protected JsonParamError(JsonResponseCode ec, string parameterName)
            : base(ec)
        {
            ParameterName = parameterName;
            ErrorMessage += " Parameter name: " + parameterName + ".";
        }
    }

    [DataContract]
    public class JsonMalformedParameter : JsonParamError
    {
        public JsonMalformedParameter(string parameterName)
            : base(JsonResponseCode.MissingParameter, parameterName) { }

        public new WebFaultException<JsonMalformedParameter> GetException()
        {
            return GetException(this);
        }
    }

    [DataContract]
    public class JsonMissingParameter : JsonParamError
    {
        public JsonMissingParameter(string parameterName)
            : base(JsonResponseCode.MissingParameter, parameterName) { }

        public new WebFaultException<JsonMissingParameter> GetException()
        {
            return GetException(this);
        }
    }

    [DataContract]
    public class JsonWrongParameterType : JsonParamError
    {
        [DataMember(Name = "requiredType")]
        public string RequiredType { get; private set; }

        public JsonWrongParameterType(string parameterName, string requiredType)
            : base(JsonResponseCode.WrongParameterType, parameterName)
        {
            RequiredType = requiredType;
            ErrorMessage += " Required Type: " + requiredType + ".";
        }

        public new WebFaultException<JsonWrongParameterType> GetException()
        {
            return GetException(this);
        }

    }
}