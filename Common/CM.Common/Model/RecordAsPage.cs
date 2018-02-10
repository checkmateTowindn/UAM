using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Common.Model
{
    public class RecordAsPageModel
    {
        //        SET @tbName = 'uinfo'; -- -- 表名
        //  SET @fldName='iduse,uname,email';-- -- 表的列名
        //  SET @strWhere = ''; -- -- 查询条件
        //  SET @pageIndex=1;-- -- 第几页 传入1就是显示第一页
        //SET @pageSize = 5;-- -- 一页显示几条记录
        //  SET @orderType=0; -- --0是升序 非0是降序
        //SET @sortName = 'id'; -- -- 排序字段;

        /// <summary>
        /// 表名
        /// </summary>
        public string TbName { get; set; }
        /// <summary>
        /// 查询的表的列名
        /// </summary>
        public string FldName { get; set; }
        /// <summary>
        /// 查询条件
        /// </summary>
        public string StrWhere { get; set; }
        /// <summary>
        /// 第几页 传入1就是显示第一页
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 一页显示几条记录
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 0是升序 非0是降序
        /// </summary>
        public Int16 OrderType { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public string SortName { get; set; }
    }
}
