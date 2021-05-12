using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atm
{
    class NegativeException : ApplicationException
    {
        public NegativeException(string message)
            : base(message) { }

        public override string ToString()
        {
            StringBuilder retval = new StringBuilder("Warning: ").Append('\t').Append(this.Message);
            retval.Append(Environment.NewLine);

            return retval.ToString();
        }
    }
}
