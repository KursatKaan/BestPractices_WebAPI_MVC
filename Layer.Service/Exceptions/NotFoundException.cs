using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Service.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string mesaage) : base(mesaage)
        {

        }
    }
}
