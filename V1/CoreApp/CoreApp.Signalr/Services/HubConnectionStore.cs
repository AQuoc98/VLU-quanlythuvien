using CoreApp.Signalr.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace CoreApp.Signalr.Services
{
    public class HubConnectionStore
    {
        #region Fields

        #endregion

        #region Contructors
        public HubConnectionStore()
        {
            _connecting = new ConcurrentDictionary<string, HubConnectionInfo>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Add or Update connection info
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns>True: updated, Fasle: added</returns>
        public bool AddOrUpdate(string connectionId)
        {
            var isExisted = _connecting.ContainsKey(connectionId);

            var newConnection = new HubConnectionInfo { ConnectionId = connectionId };
            _connecting.AddOrUpdate(connectionId, newConnection, (key, value) => newConnection);

            return isExisted;
        }

        /// <summary>
        /// Remove connection 
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>True: removed, False: Cannot remove</returns>
        public bool Remove(string key)
        {
            return _connecting.TryRemove(key, out HubConnectionInfo connectionInfo);
        }

        /// <summary>
        /// Get all connection except current connection
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public IEnumerable<HubConnectionInfo> GetAllConnectionExceptThis(string connectionId)
        {
            return _connecting.Values.Where(p => p.ConnectionId != connectionId);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Saved all connecting 
        /// </summary>
        private ConcurrentDictionary<string, HubConnectionInfo> _connecting { get; set; }
        #endregion
    }
}
