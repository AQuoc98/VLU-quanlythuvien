using Autofac;
using CoreApp.Domain.Systems.Implementations;
using CoreApp.Domain.Systems.Interfaces;
using CoreApp.Domain.Bussiness.Implementations;
using CoreApp.Domain.Bussiness.Interfaces;

namespace CoreApp.Configurations.DependencyInjections
{
    public class DomainModule : Autofac.Module
    {
        /// <summary>
        /// To configure DI for Repository module
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // Systems
            builder.RegisterType<CoreUserDm>().As<ICoreUserDm>().InstancePerLifetimeScope();
            builder.RegisterType<CoreViewDm>().As<ICoreViewDm>().InstancePerLifetimeScope();
            builder.RegisterType<CoreModuleDm>().As<ICoreModuleDm>().InstancePerLifetimeScope();
            builder.RegisterType<CoreMenuDm>().As<ICoreMenuDm>().InstancePerLifetimeScope();
            builder.RegisterType<CoreTableDm>().As<ICoreTableDm>().InstancePerLifetimeScope();
            builder.RegisterType<CorePermissionDm>().As<ICorePermissionDm>().InstancePerLifetimeScope();
            builder.RegisterType<CoreEnumDm>().As<ICoreEnumDm>().InstancePerLifetimeScope();
            builder.RegisterType<CoreConfigDm>().As<ICoreConfigDm>().InstancePerLifetimeScope();
            // Bussiness
            builder.RegisterType<DoAuthorDm>().As<IAuthorDm>().InstancePerLifetimeScope();
            builder.RegisterType<DoCatalogDm>().As<ICatalogDm>().InstancePerLifetimeScope();
            builder.RegisterType<DoPublishierDm>().As<IPublishierDm>().InstancePerLifetimeScope();
            builder.RegisterType<DoRackDm>().As<IRackDm>().InstancePerLifetimeScope();
            builder.RegisterType<DoMemberGroupDm>().As<IMemberGroupDm>().InstancePerLifetimeScope();
            builder.RegisterType<DoFormatDm>().As<IFormatDm>().InstancePerLifetimeScope();
            builder.RegisterType<DoBookDm>().As<IDoBookDm>().InstancePerLifetimeScope();
            builder.RegisterType<DoPolicyDm>().As<IPolicyDm>().InstancePerLifetimeScope();
            builder.RegisterType<DoMemberDm>().As<IMemberDm>().InstancePerLifetimeScope();

            builder.RegisterType<DoBookItemDm>().As<IBookitemDm>().InstancePerLifetimeScope();
            builder.RegisterType<DoStatusDm>().As<IStatusDm>().InstancePerLifetimeScope();

            builder.RegisterType<DoLanguageDm>().As<ILanguageDm>().InstancePerLifetimeScope();

        }
    }
}
