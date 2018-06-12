using Elasticsearch.Source.Models.Base;

namespace Elasticsearch.Source.Models
{
    /// <summary>
    /// Книга.
    /// </summary>
    public class Book : Model
    {
        /// <summary>
        /// Название.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Цена.
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// Количество страниц.
        /// </summary>
        public int Pages { get; set; }

        /// <summary>
        /// Автор.
        /// </summary>
        public Author Author { get; set; } = new Author();

        /// <summary>
        /// Инициализирует экземпляр класса <see cref="Book" />.
        /// </summary>
        /// <param name="name">Название книги.</param>
        /// <param name="price">Цена книги.</param>
        /// <param name="pages">Количество страниц.</param>
        public Book(string name, int price, int pages)
        {
            Name = name;
            Price = price;
            Pages = pages;
        }
    }
}
