using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneralUseClasses.Exceptions;

namespace GeneralUseClasses.EventArgs
{
    public class ExceptionForUserEventArgs : System.EventArgs
    {
        public ExceptionForUser exception;

        public ExceptionForUserEventArgs(ExceptionForUser exception)
        {
            this.exception = exception;
        }
    }
}
