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

        public void InsertingDocumentsExample()
        {
            CreateIndex();
            AddBooks();
            DeleteIndex();
        }

        private void AddBooks()
        {
            CreateIndex();

            foreach (var book in GetBooks())
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

            var book = DocumentService.GetById<Book>(IndexName, new Id(1)) ??
                throw new ApplicationException("Книга не найдена.");

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
                DocumentService.GetAll<Book>(IndexName)
                    .Documents
                    .Count
            );

            var book = DocumentService.GetById<Book>(IndexName, new Id(1));

            Console.WriteLine(JsonConvert.SerializeObject(book, Formatting.Indented));
        }

        private static void SetNewValues(Book book)
        {
            book.Name = $"{book.Name} (NEW)";
            book.Author.Name = $"{book.Author.Name} (NEW)";
            book.Author.BirthDate = DateTime.Now;
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

        private static IEnumerable<Book> GetBooks()
        {
            return new List<Book>
            {
                new Book("CLR via C#", 1_500, 900)
                {
                    Id = 1,
                    Author = new Author("Джеффри Рихтер"),
                },
                new Book("C# 5.0 и платформа .NET", 2_000, 1_200)
                {
                    Id = 2,
                    Author = new Author("Эндрю Троелсен")
                }
            };
        }
    }
}
