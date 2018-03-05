using System;
using System.Collections.Generic;
using System.Text;

namespace CM.Common.Model
{
   public class CreateModel<T> where T :class
    {
        public T Model { get; set; }
        public string UserId { get; set; }
    }
}
