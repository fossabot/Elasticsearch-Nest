using System;
using Castle.Windsor;
using Elasticsearch.Source.Services;

namespace Elasticsearch.Source
{
    internal class Program
    {
        private static void Main()
        {
            var container = new WindsorContainer()
                .Install(new Installer());

            var executor = container.Resolve<Executor>();

            executor.CreatingIndexExample();
            executor.DeletingIndexExample();
            executor.InsertingDocumentsExample();
            executor.UpdatingDocumentsExample();
            executor.SearchingDocumentsExample();

            Console.ReadKey();
        }
    }
}
