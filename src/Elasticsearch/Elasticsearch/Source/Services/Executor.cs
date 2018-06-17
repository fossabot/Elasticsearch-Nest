using System;
using System.Collections.Generic;
using Elasticsearch.Source.Models;
using Elasticsearch.Source.Services.Abstraction;
using Nest;
using Newtonsoft.Json;

namespace Elasticsearch.Source.Services
{
    public class Executor
    {
        protected IIndexService IndexService { get; }
        protected IDocumentService DocumentService { get; }
        protected ISearchService SearchService { get; }

        protected const string IndexName = "products";

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
                IndexService.Create<Product>(IndexName)
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

        public void InsertingDocumentsExample()
        {
            CreateIndex();
            AddBooks();
            DeleteIndex();
        }

        private void AddBooks()
        {
            CreateIndex();

            foreach (var book in GetProducts())
            {
                Console.WriteLine(
                    DocumentService.AddOrUpdate(IndexName, new Id(book.Id), book)
                        .DebugInformation
                );
            }
        }

        public void UpdatingDocumentsExample()
        {
            CreateIndex();
            AddBooks();

            var book = DocumentService.GetById<Product>(IndexName, new Id(1)) ??
                throw new ApplicationException("Продукт не найден.");

            SetNewValues(book);

            Console.WriteLine(
                DocumentService.AddOrUpdate(IndexName, new Id(book.Id), book)
                    .DebugInformation
            );

            DeleteIndex();
        }

        public void SearchingDocumentsExample()
        {
            CreateIndex();
            AddBooks();

            Console.WriteLine(
                DocumentService.GetAll<Product>(IndexName)
                    .Documents
                    .Count
            );

            var book = DocumentService.GetById<Product>(IndexName, new Id(1));

            Console.WriteLine(JsonConvert.SerializeObject(book, Formatting.Indented));
        }

        private static void SetNewValues(Product product)
        {
            product.Name = $"{product.Name} (NEW)";
            product.Price = new Random().Next(1000, 5000);
        }

        private void CreateIndex()
        {
            if (!IndexService.IndexExists(IndexName))
                IndexService.Create<Product>(IndexName);
        }

        private void DeleteIndex()
        {
            if (IndexService.IndexExists(IndexName))
                IndexService.Delete(IndexName);
        }

        private static IEnumerable<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product("Смартфон Apple iPhone 8 256 ГБ", 62_999, new Vendor("Apple", "Китай"))
                {
                    Id = 1,
                    Tags = new List<string> { "Apple", "iPhone", "Смартфон" }
                },

                new Product("Ноутбук Apple MacBook Pro Retina (Z0SW0009F)", 144_999, new Vendor("Apple", "Китай"))
                {
                    Id = 2,
                    Tags = new List<string> { "Apple", "MacBook", "Ноутбук" }
                }
            };
        }
    }
}
