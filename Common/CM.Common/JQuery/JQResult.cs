using System;
using System.Collections.Generic;
using System.Text;

namespace CM.Common.JQuery
{
    [Serializable]
    public class JQResult<T>
    {
        public int page { get; set; }

        public int total { get; set; }

        public long records { get; set; }

        public IList<T> rows { get; set; }

        public object userdata { get; set; }

        public bool success { get; set; }

        public string code { get; set; }

        public string message { get; set; }

        public JQResult()
        {
            this.rows = (IList<T>)new List<T>();
        }
    }
}
