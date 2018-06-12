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
        /// Инициализирует экземпляр класса <see cref="Book" />.
        /// </summary>
        /// <param name="name">Название книги.</param>
        /// <param name="price">Цена книги.</param>
        public Book(string name, int price)
        {
            Name = name;
            Price = price;
        }
    }
}
