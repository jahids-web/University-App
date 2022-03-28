using System;
using System.Collections.Generic;
using System.Text;

namespace Utility.Exceptions
{
    public class ApplicationValidationExcetion : Exception
    {
        public ApplicationValidationExcetion(string massage): base (massage)
        {
                
        }
    }
}
