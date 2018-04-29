using System;
using System.Collections.Generic;
using System.Text;

namespace BashSoft.Exceptions
{
    public class InvalidFileNameException : Exception
    {

        public InvalidFileNameException()
            :base(ForbiddenSymbolsContainedInName)
        {

        }

        private const string ForbiddenSymbolsContainedInName = "The given name contains symbols that" +
            "are not allowed to be used in names of files or folders";

        public InvalidFileNameException(string message)
            : base(message)
        {

        }

    }
}
