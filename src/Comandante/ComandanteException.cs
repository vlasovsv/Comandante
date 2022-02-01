using System;

namespace Comandante
{
    /// <summary>
    /// Inner comandante exception
    /// </summary>
    public class ComandanteException : Exception
    {
        /// <summary>
        /// Creates a new exception
        /// </summary>
        /// <param name="message">An exception message</param>
        public ComandanteException(string message)
            : base(message)
        {
            
        }
    }
}