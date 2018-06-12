using System;
using Elasticsearch.Source.Models.Base;

namespace Elasticsearch.Source.Models
{
    /// <summary>
    /// Автор.
    /// </summary>
    public class Author : Model
    {
        /// <summary>
        /// Имя.
        /// </summary>
        public string Name { get; set; } = "Аноним";

        /// <summary>
        /// Дата рождения.
        /// </summary>
        public DateTime BirthDate { get; set; }

        public Author() { }

        /// <summary>
        /// Инициализирует экземпляр класса <see cref="Author" />.
        /// </summary>
        /// <param name="name">Имя автораа.</param>
        public Author(string name)
        {
            Name = name;
            BirthDate = DateTime.Now.AddYears(30);
        }
    }
}
