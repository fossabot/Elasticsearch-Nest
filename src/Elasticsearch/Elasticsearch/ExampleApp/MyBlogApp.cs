using System;
using System.Collections.Generic;
using Elasticsearch.ExampleApp.Models;
using Elasticsearch.Net;
using Nest;
using PostType = Elasticsearch.ExampleApp.Models.PostType;

namespace Elasticsearch.ExampleApp
{
    /// <summary>
    /// Приложение: Мой блог.
    /// </summary>
    public class MyBlogApp
    {
        /// <summary>
        /// Название индекса.
        /// </summary>
        public string IndexName => "blog";

        /// <summary>
        /// Клиент.
        /// </summary>
        public ElasticClient Client { get; }

        public MyBlogApp()
        {
            var settings = new ConnectionSettings()
                .DisableAutomaticProxyDetection()
                .EnableHttpCompression()
                .DisableDirectStreaming()
                .PrettyJson()
                .RequestTimeout(TimeSpan.FromMinutes(2));

            Client = new ElasticClient(settings);
        }

        /// <summary>
        /// Создает индекс.
        /// </summary>
        public MyBlogApp CreateIndex()
        {
            Client.CreateIndex(IndexName, c => c
                .Mappings(m => m.Map<Post>(mp => mp.AutoMap()))
            );

            return this;
        }

        /// <summary>
        /// Добавляет документы.
        /// </summary>
        public MyBlogApp AddDocuments()
        {
            foreach (var post in GetPosts())
            {
                Client.Index(post, i => i
                    .Index(IndexName)
                    .Type(nameof(Post))
                    .Id(post.Id)
                    .Refresh(Refresh.True)
                );
            }

            return this;
        }

        /// <summary>
        /// Осуществляет поиск документов.
        /// </summary>
        public MyBlogApp SearchDocuments()
        {
            // TODO: Реализовать поиск.

            return this;
        }

        /// <summary>
        /// Обновляет документы.
        /// </summary>
        public MyBlogApp UpdateDocuments()
        {
            // TODO: Реализовать обновление документов.

            return this;
        }

        /// <summary>
        /// Удаляет документы.
        /// </summary>
        public MyBlogApp DeleteDocuments()
        {
            // TODO: Реализовать удаление документов.

            return this;
        }

        /// <summary>
        /// Удаляет индекс.
        /// </summary>
        public MyBlogApp DeleteIndex()
        {
            Client.DeleteIndex(IndexName);

            return this;
        }

        private static IEnumerable<Post> GetPosts()
        {
            // TODO: Добавить документы.

            return new List<Post>
            {
                new Post
                {
                    Id = 1,
                    Header = "Ваш шедевр готов!",
                    Text =
                        "Разнообразный и богатый опыт рамки и место обучения кадров " +
                        "в значительной степени обуславливает создание новых предложений. " +
                        "Таким образом сложившаяся структура организации играет важную роль " +
                        "в формировании существенных финансовых и административных условий.",
                    CreatedAt = DateTime.Now.AddDays(-5),
                    Author = new Author
                    {
                        Name = "Lorem",
                        BirthDate = DateTime.Now.AddYears(-26)
                    },
                    Type = PostType.History,
                    Tags = new List<string> { "Lorem", "История" }
                }
            };
        }
    }
}
