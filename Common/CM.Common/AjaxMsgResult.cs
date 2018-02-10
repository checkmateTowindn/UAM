using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Common {
    [Serializable]
    public class AjaxMsgResult {
        /// <summary>
        /// ajax操作结果
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 返回状态
        /// </summary>
        public StateEnum State { get; set; }
        /// <summary>
        /// 返回消息文本
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 扩展信息(数据)
        /// </summary>
        public object Source { get; set; }
        public enum StateEnum
        {
            /// <summary>
            /// 无返回数据
            /// </summary>
            NoReturnData = 0,
            /// <summary>
            /// 验证失败
            /// </summary>
            VerifyFailed = 1,
            /// <summary>
            /// 已存在
            /// </summary>
            IsExist=2,
        }

    }
  

}
