using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace CoreApp.Signalr.Hubs
{
    public class ChangeProxyHub : Hub<IChangeProxyClient>
    {
        #region Fields
        private readonly Services.HubConnectionStore _hubConnectionStore;
        #endregion

        #region Contructors
        public ChangeProxyHub(Services.HubConnectionStore hubConnectionStore)
        {
            _hubConnectionStore = hubConnectionStore;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Join to Hub
        /// </summary>
        /// <returns></returns>
        public async Task Join()
        {
            _hubConnectionStore.AddOrUpdate(Context.ConnectionId);
            await Clients.Caller.JoinResult(Context.ConnectionId);
        }

        /// <summary>
        /// Continue order
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public async Task ContinueOrder(string connectionId)
        {
            await Clients.User(connectionId).ContinueOrderProcess();
        }

        /// <summary>
        /// On disconnected
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            _hubConnectionStore.Remove(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
        #endregion
    }
}
