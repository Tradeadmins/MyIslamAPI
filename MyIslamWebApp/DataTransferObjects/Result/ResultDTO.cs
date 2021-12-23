using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyIslamWebApp.DataTransferObjects.Result
{
    public class ResultDTO<T>
    {
      public List<T> Response { get; set; }
      public int TotalCount { get; set; }
    }
}