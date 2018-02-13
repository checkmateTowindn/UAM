using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Common.JQuery
{
  
        [Serializable]
        public class JQParas
        {
            public int page { get; set; }

            public int rows { get; set; }

            public string sidx { get; set; }

            public string sord { get; set; }

            public bool _search { get; set; }

            public bool export { get; set; }

            public int exportType { get; set; }

            public int exportStartPage { get; set; }

            public int exportEndPage { get; set; }

            public string exportFileName { get; set; }

            public bool treeGrid { get; set; }

            public string cSearch { get; set; }

            public IDictionary<string, string> cSearchAdvanced { get; set; }

            public object ExtFileds { get; set; }

            public object ExtContent { get; set; }
        }
    }

