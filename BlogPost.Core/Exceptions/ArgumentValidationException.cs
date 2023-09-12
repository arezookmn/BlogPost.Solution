using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.Core.Exceptions
{
    public class ArgumentValidationException : Exception
    {
        public Object? Object { get;  }
        public ArgumentValidationException() { }

        public ArgumentValidationException(string message, Object obj)
            : base(message)
        {
            Object = obj;
        }

        public ArgumentValidationException(string message, Exception innerException, Object obj)
            : base(message, innerException)
        {
            Object = obj;
        }

    }
}
