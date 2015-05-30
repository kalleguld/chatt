using System.Runtime.Serialization;

namespace rerest.models.output.exceptions
{
    public abstract class JsonParamError : JsonError
    {
        [DataMember]
        public string ParameterName { get; private set; }

        [DataMember]
        public override string ErrorMessage
        {
            get
            {
                return JsonErrorCode.ErrorMessage()
                    + " Parameter name: " + ParameterName;
            }
        }

        protected JsonParamError(JsonErrorCode ec, string parameterName)
            : base(ec)
        {
            ParameterName = parameterName;
        }
    }

    [DataContract]
    public class JsonMalformedParameter : JsonParamError
    {
        public JsonMalformedParameter(string parameterName)
            : base(JsonErrorCode.MissingParameter, parameterName) { }
    }

    [DataContract]
    public class JsonMissingParameter : JsonParamError
    {
        public JsonMissingParameter(string parameterName)
            : base(JsonErrorCode.MissingParameter, parameterName) { }
    }

    [DataContract]
    public class JsonWrongParameterType : JsonParamError
    {
        [DataMember]
        public string RequiredType { get; private set; }

        [DataMember]
        public override string ErrorMessage
        {
            get { return base.ErrorMessage + " Required type: " + RequiredType; }
        }

        public JsonWrongParameterType(string parameterName, string requiredType)
            : base(JsonErrorCode.WrongParameterType, parameterName)
        {
            RequiredType = requiredType;
        }
    }
}