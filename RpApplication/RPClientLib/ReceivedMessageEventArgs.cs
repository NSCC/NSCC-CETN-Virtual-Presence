using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPClientLib
{
    public class ReceivedMessageEventArgs
    {
        /// <summary>
        /// Constructor for the receievd message arguments.
        /// </summary>
        /// <param name="message">The message that has been received.</param>
        public ReceivedMessageEventArgs(String message)
        {
            Message = message;
        }


        /// <summary>
        /// The received message.
        /// </summary>
        public String Message { get; }
    }
}
