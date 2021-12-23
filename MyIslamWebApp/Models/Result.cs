using System.Collections.Generic;

namespace MyIslamWebApp.Models
{
    public class Result<T>
    {
        public List<T> Response { get; set; }
        public int TotalCount { get; set; }
    }
}