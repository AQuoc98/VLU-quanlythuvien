using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Signalr.Hubs
{
    public interface IChangeProxyClient
    {
        /// <summary>
        /// This function to call client continue order after change Proxy Info
        /// </summary>
        /// <returns></returns>
        Task ContinueOrderProcess();

        /// <summary>
        /// To send client connection id after join
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        Task JoinResult(string connectionId);
    }
}
