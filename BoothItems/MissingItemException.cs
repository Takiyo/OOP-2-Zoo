using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoothItems
{
    /// <summary>
    /// The class used to represent a custom missing item exception.
    /// </summary>
    [Serializable]
    public class MissingItemException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the MissingItemException class.
        /// </summary>
        /// <param name="message">The message given when the exception is thrown.</param>
        public MissingItemException(string message)
        {

        }
    }
}
