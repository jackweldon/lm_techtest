using Groceries.API;
using Groceries.API.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Groceries.Tests
{
    [TestClass]
    public class CSVParserTests
    {
        private const string filePath = "Data\\Technical Task - Grocery Prices - CSharp.csv";

        [TestMethod]
        public void ReadCsv_ReturnItems()
        {
            //arrage
            var csvParser = new GroceryCsvParser<Grocery>();
            string path = AppDomain.CurrentDomain.BaseDirectory;

            //act
            var items = csvParser.Deserialize(path + filePath);
            var result = items;


            //assert

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(items.First(), typeof(Grocery));
        }
    }
}
