using System;
using Elasticsearch.Source.Models;
using Elasticsearch.Source.Services.Abstraction;
using Nest;

namespace Elasticsearch.Source.Services
{
    public class Executor
    {
        protected IIndexService IndexService { get; }
        protected IDocumentService DocumentService { get; }
        protected ISearchService SearchService { get; }

        protected const string IndexName = "books";

        public Executor(
            IIndexService indexService,
            IDocumentService documentService,
            ISearchService searchService)
        {
            IndexService = indexService;
            DocumentService = documentService;
            SearchService = searchService;
        }

        public void CreatingIndexExample()
        {
            DeleteIndex();

            Console.WriteLine(
                IndexService.Create<Book>(IndexName)
                    .DebugInformation
            );

            DeleteIndex();
        }

        public void DeletingIndexExample()
        {
            CreateIndex();

            IDeleteIndexResponse response = null;

            if (IndexService.IndexExists(IndexName))
                response = IndexService.Delete(IndexName);

            Console.WriteLine(
                response?.DebugInformation ??
                throw new ApplicationException($"Индекс <{IndexName}> не существует.")
            );
        }

        private void CreateIndex()
        {
            if (!IndexService.IndexExists(IndexName))
                IndexService.Create<Book>(IndexName);
        }

        private void DeleteIndex()
        {
            if (IndexService.IndexExists(IndexName))
                IndexService.Delete(IndexName);
        }
    }
}
