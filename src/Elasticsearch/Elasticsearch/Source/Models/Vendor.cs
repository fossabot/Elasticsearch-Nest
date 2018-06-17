namespace Elasticsearch.Source.Models
{
    /// <summary>
    /// Поставщик.
    /// </summary>
    public class Vendor
    {
        /// <summary>
        /// Наименование.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Страна.
        /// </summary>
        public string Сountry { get; set; }

        /// <summary>
        /// Инициализирует экземпляр класса <see cref="Vendor" />.
        /// </summary>
        /// <param name="name">Наименование поставщика.</param>
        /// <param name="country">Страна поставщика.</param>
        public Vendor(string name, string country)
        {
            Name = name;
            Сountry = country;
        }
    }
}
