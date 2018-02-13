using CM.Common;
using CM.UM.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CM.UM.IService
{
    public interface IUserExtendService
    {
        AjaxMsgResult Add(string userId);
    }
}
