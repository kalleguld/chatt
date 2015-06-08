using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using rerest.jsonBase;

namespace rerest.controllers
{
    [ServiceContract]
    public interface IOptionController
    {
        [OperationContract]
        [WebInvoke(Method = "OPTIONS",
            UriTemplate = "",
            ResponseFormat = WebMessageFormat.Json)]
        JsonResponse GetOptions();

        [OperationContract]
        [WebInvoke(Method="OPTIONS",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "{input}")]
        JsonResponse GetNamedOptions(string input);
    }
}
