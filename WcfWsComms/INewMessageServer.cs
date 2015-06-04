using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace WcfWsComms
{
    [ServiceContract(CallbackContract = typeof(INewMessageClient))]
    public interface INewMessageServer
    {
        [OperationContract(IsOneWay = true)]
        Task SendMessages();
    }
}