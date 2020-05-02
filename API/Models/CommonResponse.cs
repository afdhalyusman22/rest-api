using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class CommonResponse
    {
        public int status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
