using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Model
{
    public class ErrorModel
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; } = "";
        public string Error { get; set; } = "";
    }
}
