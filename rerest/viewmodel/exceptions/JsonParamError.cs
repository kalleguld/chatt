using System.Runtime.Serialization;
using rerest.jsonBase;
using rerest.viewmodel;

namespace rerest.viewmodel.exceptions
{
    [DataContract]
    public abstract class JsonParamError : JsonError
    {
        [DataMember]
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

        public override void Throw()
        {
            GenericThrow(this);
        }
    }

    [DataContract]
    public class JsonMissingParameter : JsonParamError
    {
        public JsonMissingParameter(string parameterName)
            : base(JsonResponseCode.MissingParameter, parameterName) { }

        public override void Throw()
        {
            GenericThrow(this);
        }
    }

    [DataContract]
    public class JsonWrongParameterType : JsonParamError
    {
        [DataMember]
        public string RequiredType { get; private set; }

        public JsonWrongParameterType(string parameterName, string requiredType)
            : base(JsonResponseCode.WrongParameterType, parameterName)
        {
            RequiredType = requiredType;
            ErrorMessage += " Required Type: " + requiredType + ".";
        }

        public override void Throw()
        {
            GenericThrow(this);
        }
    }
}