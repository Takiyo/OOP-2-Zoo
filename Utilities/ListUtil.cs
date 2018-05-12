using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    /// <summary>
    /// The class used to repesent a list utility.
    /// </summary>
    [Serializable]
    public static class ListUtil
    {
        /// <summary>
        /// Flattens the list.
        /// </summary>
        /// <param name="list">List to be flattened.</param>
        /// <param name="separator">Separates list.</param>
        /// <returns>The flattened list.</returns>
        public static string Flatten(IEnumerable<string> list, string separator)
        {
            string result = null;

            list.ToList().ForEach(s => result += result == null ? s : separator + s);

            return result;
        }
    }
}
