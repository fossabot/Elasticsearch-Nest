using System;
using Castle.Windsor;
using Elasticsearch.ExampleApp;
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

            Console.Clear();

            new MyBlogApp()
                .CreateIndex()
                .AddDocuments()
                .SearchDocuments()
                .UpdateDocuments()
                .DeleteDocuments()
                .DeleteIndex();

            Console.ReadKey();
        }
    }
}
