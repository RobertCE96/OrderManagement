using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagement.Domain.Exceptions
{
    public class InvalidOperationException : Exception
    {
        public InvalidOperationException(string message) : base(message)
        {
        }
    }
}
