﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CM.UM.Model
{
    [Serializable]
    public class UC_UserExtend
    {
        public string Id { get; set; }
        public string QQ { get; set; }
        public string OpenId { get; set; }
        public string RealName { get; set; }
        public string AddRess { get; set; }
    }
}
