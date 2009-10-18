using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phantom
{
    public class PhantomException : Exception
    {
        public PhantomException(string message) : base(message) { }
    }
}