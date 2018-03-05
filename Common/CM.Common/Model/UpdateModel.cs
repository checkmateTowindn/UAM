using System;
using System.Collections.Generic;

namespace CM.Common.Model
{
    public class UpdateModel<T> where T: class
    {
       public T Model { get; set; }
        public List<string> Ids { get; set; }
        public string CreateUser { get; set; }
    }
}
