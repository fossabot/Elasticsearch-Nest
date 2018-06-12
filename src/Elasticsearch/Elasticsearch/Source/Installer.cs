using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Elasticsearch.Source.Core.Abstraction;
using Elasticsearch.Source.Core.Implementation;
using Elasticsearch.Source.Services;

namespace Elasticsearch.Source.Installers
{
    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes
                .FromAssembly(typeof(Program).Assembly)
                .Where(type => type.Name.EndsWith("Service"))
                .WithService.DefaultInterfaces()
                .LifestyleTransient());

            container.Register(Component
                .For<IElastic>()
                .ImplementedBy<Elastic>());

            container.Register(Component.For<Executor>());
        }
    }
}
