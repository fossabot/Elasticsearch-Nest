using System;
using System.Collections.Generic;

namespace Elasticsearch.ExampleApp.Models
{
    /// <summary>
    /// Пост.
    /// </summary>
    public class Post
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Заголовок.
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Текст (содержмиое).
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Дата создания.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Автор.
        /// </summary>
        public Author Author { get; set; }

        /// <summary>
        /// Теги.
        /// </summary>
        public List<string> Tags { get; set; }

        /// <summary>
        /// Тип.
        /// </summary>
        public PostType Type { get; set; } = PostType.NotSpecified;
    }
}
