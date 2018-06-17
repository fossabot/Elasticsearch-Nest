using System;
using System.Collections.Generic;
using Elasticsearch.Source.Models.Base;

namespace Elasticsearch.Source.Models
{
    /// <summary>
    /// Продукт.
    /// </summary>
    public class Product : Model
    {
        /// <summary>
        /// Наименование.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Цена.
        /// </summary>
        public float Price { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Поставщик.
        /// </summary>
        public Vendor Vendor { get; set; }

        /// <summary>
        /// Теги (ключевые слова).
        /// </summary>
        public List<string> Tags { get; set; }

        /// <summary>
        /// Дата создания (изготовления).
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Инициализирует экземпляр класса <see cref="Product" />.
        /// </summary>
        public Product() { }

        /// <summary>
        /// Инициализирует экземпляр класса <see cref="Product" />.
        /// </summary>
        /// <param name="name">Наименование продукта.</param>
        /// <param name="price">Цена продукта.</param>
        /// <param name="vendor">Поставщик продукта.</param>
        public Product(string name, int price, Vendor vendor)
        {
            Name = name;
            Price = price;
            Vendor = vendor;
        }
    }
}
