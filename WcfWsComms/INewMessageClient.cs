using System.ServiceModel;
using System.Threading.Tasks;

namespace WcfWsComms
{
    [ServiceContract]
    public interface INewMessageClient
    {
        [OperationContract(IsOneWay = true)]
        Task NewMessageCreated(MessageInfo message);
    }
}