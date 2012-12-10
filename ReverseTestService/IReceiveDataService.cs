using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ReverseTestService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IReceiveDataService
    {
        [OperationContract]
        [WebInvoke(UriTemplate="/Add",
            Method="POST", 
            RequestFormat = WebMessageFormat.Json, 
            ResponseFormat= WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        void StoreData(string data);

        [OperationContract]
        [WebInvoke(UriTemplate="/{id}",
            Method="GET", 
            ResponseFormat= WebMessageFormat.Json)]
        string HelloWorld(string id);
    }
}
