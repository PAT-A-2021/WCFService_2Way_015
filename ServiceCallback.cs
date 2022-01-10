using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFService_2Way_20190140015
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single)]
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class ServiceCallback : IServiceCallback
    {
        Dictionary<IClientCallback, string> userList = new Dictionary<IClientCallback, string>();

        public void gabung(string username)
        {
            IClientCallback koneksiG = OperationContext.Current.GetCallbackChannel<IClientCallback>();
            userList[koneksiG] = username;
        }

        public void kirimPesan(string pesan)
        {
            IClientCallback koneksiP = OperationContext.Current.GetCallbackChannel<IClientCallback>();
            string user;
            if(!userList.TryGetValue(koneksiP, out user))
            {
                return;
            }
            foreach(IClientCallback other in userList.Keys)
            {
                other.pesanKirim(user,pesan);
            }
        }
    }
}
