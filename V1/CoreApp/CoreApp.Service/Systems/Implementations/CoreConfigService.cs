using CoreApp.Domain.Systems.Interfaces;
using CoreApp.Service.Base;
using CoreApp.Service.Systems.Interfaces;
using CoreApp.EntityFramework.Models;
using CoreApp.Authentication.Jwt;
using CoreApp.EntityFramework.ViewModels;
using CoreApp.Common.Helpers;
using System.Linq;
using CoreApp.Common.Models;
using System.Collections.Generic;
using CoreApp.Common.Enums;

namespace CoreApp.Service.Systems.Implementations
{
    public sealed class CoreConfigService : BaseService<CoreConfig>, ICoreConfigService
    {
        #region Fields
        private readonly ICoreConfigDm _dm;
        #endregion

        #region Contructors
        public CoreConfigService(ICoreConfigDm dm, IUserManager userManager) : base(dm, userManager)
        {
            _dm = dm;
        }
        #endregion

        #region Utilities
        #endregion

        #region Methods
        public ConfigData GetAppConfig()
        {
            var configs = _dm.GetAll();
            // Build config data object
            var configData = new ConfigData();

            foreach (var config in configs)
            {
                if (config.IsNeedEncrypt)
                {
                    ObjectBuilder.SetValueForObject(configData, EncryptHelper.StringDecrypt(config.Value), config.Name);
                }
                else
                {
                    ObjectBuilder.SetValueForObject(configData, config.Value, config.Name);
                }
            }

            return configData;
        }

        public override ResultModel Update(IList<CoreConfig> entities)
        {
            var resultModel = new ResultModel();

            var configNeedEncrypt = entities.Where(p => p.IsNeedEncrypt);

            foreach (var item in configNeedEncrypt)
            {
                item.Value = EncryptHelper.StringEncrypt(item.Value);
            }

            var countSaved = _dm.Update(entities);
            if (countSaved == 0)
            {
                // Save Fail
                resultModel.Status = ResultStatus.Error;
                return resultModel;
            }
            resultModel.Messages = new string[] { "COMMON.MESSAGE_UPDATE_SUCCESS" };
            resultModel.Status = ResultStatus.Success;

            return resultModel;
        }
        public IList<CoreConfigGroup> GetConfigGroup()
        {
            return _dm.GetConfigGroup();
        }
        #endregion
    }
}
