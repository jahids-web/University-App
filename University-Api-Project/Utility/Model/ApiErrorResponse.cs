using System;
using System.Collections.Generic;
using System.Text;

namespace Utility.Model
{
    public class ApiErrorResponse
    {
        public string Message { get; set; } 
        public string Detailes { get; set; }
        public bool IssSuccess { get; set; } = false;

        public int StatusCode { get; set; }
    }
}
