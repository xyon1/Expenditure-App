using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenditureAppViewModel.Exceptions
{
    public class ExceptionForUser : Exception
    {
        public ExceptionForUser()
        {
        }

        public ExceptionForUser(string message)
            : base(message)
        {
        }

        public ExceptionForUser(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
