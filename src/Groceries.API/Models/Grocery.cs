using CsvHelper.Configuration.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Groceries.API
{
    public class Grocery
    {
        [Key]
        [Name("fruit")]
        public string Fruit { get; set; }

        [Name("price")]
        public double Price { get; set; }

        [Name("quantity_in_stock")]
        public int QuantityInStock { get; set; }

        [Name("updated_date")]
        public DateTime? UpdatedDateTime { get; set; }
    }
}
