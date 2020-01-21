using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq; 

namespace Groceries.API.Utilities
{

    public class GroceryCsvParser<T> : ICSVParser<Grocery>
    {

        public string Serialize(IEnumerable<Grocery> items)
        {
            if (items == null) throw new ArgumentNullException("items");
            using (var writer = new StringWriter())
            using (var csv = new CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(items);
                var x= writer.GetStringBuilder();
                return x.ToString();
            } 
        }

        public IEnumerable<Grocery> Deserialize(string csvPath)
        {
            IEnumerable<Grocery> result;
            using (var reader = new System.IO.StreamReader(csvPath))
            using (var csv = new CsvReader(reader, System.Globalization.CultureInfo.CurrentCulture))
            {
                csv.Configuration.HasHeaderRecord = true;
                var records = csv.GetRecords<Grocery>().ToList();
                result= records;
            }
            return result;
        }
    }
}
