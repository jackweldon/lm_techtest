using System.Collections.Generic;

namespace Groceries.API.Utilities
{
    public interface ICSVParser<T> where T : class
    {
        /// <summary>
        /// Returns an ienumerable of objects for the given CSV 
        /// </summary>
        /// <param name="CSV"></param>
        /// <returns></returns>
        IEnumerable<T> Deserialize(string CSV);
        /// <summary>
        /// Return a CSV list of given list of items, property names will be used as headers
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        string Serialize(IEnumerable<T> items);
    }
}
