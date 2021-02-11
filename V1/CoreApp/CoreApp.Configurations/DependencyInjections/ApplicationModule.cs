using Autofac;
using CoreApp.Common.Helpers;
using CoreApp.Service.Systems.Implementations;
using CoreApp.Service.Systems.Interfaces;
using CoreApp.Service.Bussiness.Implementations;
using CoreApp.Service.Bussiness.Interfaces;

namespace CoreApp.Configurations.DependencyInjections
{
    public class ApplicationModule : Autofac.Module
    {
        /// <summary>
        /// To configure DI for Repository module
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // SignalR
            //builder.RegisterType<HubConnectionStore>().SingleInstance();

            // Common
            builder.RegisterType<EmailSender>().As<IEmailSender>().InstancePerLifetimeScope();
            // Systems
            builder.RegisterType<CoreUserService>().As<ICoreUserService>().InstancePerLifetimeScope();
            builder.RegisterType<CoreViewService>().As<ICoreViewService>().InstancePerLifetimeScope();
            builder.RegisterType<CoreMenuService>().As<ICoreMenuService>().InstancePerLifetimeScope();
            builder.RegisterType<CoreModuleService>().As<ICoreModuleService>().InstancePerLifetimeScope();
            builder.RegisterType<CoreTableService>().As<ICoreTableService>().InstancePerLifetimeScope();
            builder.RegisterType<CorePermissionService>().As<ICorePermissionService>().InstancePerLifetimeScope();
            builder.RegisterType<CoreEnumService>().As<ICoreEnumService>().InstancePerLifetimeScope();
            builder.RegisterType<CoreConfigService>().As<ICoreConfigService>().InstancePerLifetimeScope();
            // Bussiness
            builder.RegisterType<DoAuthorService>().As<IAuthorService>().InstancePerLifetimeScope();
            builder.RegisterType<DoCatalogService>().As<ICatalogService>().InstancePerLifetimeScope();
            builder.RegisterType<DoPublishierService>().As<IPublishierService>().InstancePerLifetimeScope();
            builder.RegisterType<DoRackService>().As<IRackService>().InstancePerLifetimeScope();
            builder.RegisterType<DoFormatService>().As<IFormatService>().InstancePerLifetimeScope();
            builder.RegisterType<DoMemberGroupService>().As<IMemberGroupService>().InstancePerLifetimeScope();
            builder.RegisterType<DoBookService>().As<IBookService>().InstancePerLifetimeScope();
            builder.RegisterType<DoStatusSercice>().As<IStatusService>().InstancePerLifetimeScope();
            builder.RegisterType<DoPolicyService>().As<IPolicyService>().InstancePerLifetimeScope();
            builder.RegisterType<DoMemberService>().As<IMemberService>().InstancePerLifetimeScope();

            builder.RegisterType<DoBookItemService>().As<IBookitemService>().InstancePerLifetimeScope();

            builder.RegisterType<DoLanguageService>().As<ILanguageService>().InstancePerLifetimeScope();

        }
    }
}
