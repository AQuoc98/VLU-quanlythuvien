using Autofac;
using CoreApp.Repository;

namespace CoreApp.Configurations.DependencyInjections
{
    public class RepositoryModule : Autofac.Module
    {
        /// <summary>
        /// To configure DI for Repository module
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
        }
    }
}
