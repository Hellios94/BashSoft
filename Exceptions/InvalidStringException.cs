using System;
using System.Collections.Generic;
using System.Text;

namespace BashSoft.Exceptions
{
    public class InvalidStringException : Exception
    {

        public InvalidStringException()
            : base(NullOrEmptyValue)
        {

        }

        public const string NullOrEmptyValue = "The value of the variable CANNOT be null or empty!";

        public InvalidStringException(string message)
            : base(message)
        {

        }

    }
}
