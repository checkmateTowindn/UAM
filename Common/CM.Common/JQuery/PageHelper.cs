using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using CM.Common.MySQL;

namespace CM.Common.JQuery
{
    /// <summary>
    /// PageHelperParas
    /// </summary>
    public class PageHelperParas
    {
        /// <summary>
        /// 类型,1分页查询，2jqgrid-treegrid
        /// </summary>
        public int types { get; set; }
        /// <summary>
        /// 当前页码
        /// </summary>
        public int page { get; set; }
        /// <summary>
        /// 每页记录数
        /// </summary>
        public int rows { get; set; }
        /// <summary>
        /// 开始页码
        /// </summary>
        public int start_page { get; set; }
        /// <summary>
        /// 结束页码
        /// </summary>
        public int end_page { get; set; }
        /// <summary>
        /// 排序sql部分
        /// </summary>
        public string SortUnion { get; set; }
        /// <summary>
        /// 数据查询sql
        /// </summary>
        public string dataSql { get; set; }
        /// <summary>
        /// 总页码查询sql
        /// </summary>
        public string countSql { get; set; }
        /// <summary>
        /// 是否导出
        /// </summary>
        public bool export { get; set; }
        /// <summary>
        /// 导出时生成的路径，完整路径
        /// </summary>
        public string exportFileName { get; set; }
        /// <summary>
        /// 导出显示名，与dataSql查询的列一一对应
        /// </summary>
        public List<string> exportColumnName { get; set; }
        /// <summary>
        /// 只包含的列
        /// </summary>
        public List<string> exportColumnNameCols { get; set; }
        /// <summary>
        /// 导出页码：true所有，false表示从start_page页到end_page页
        /// </summary>
        public Boolean exportAll { get; set; }

        /// <summary>
        /// 忽略导出的列，与dataSql查询的列一一对应
        /// </summary>
        public List<string> exportIgnoreColumnName { get; set; }
    }

    public class PageHelper
    {
        /// <summary>
        /// 分面查询
        /// </summary>
        /// <typeparam name="T">数据返回的类型</typeparam>
        /// <param name="ph_paras">参数</param>
        /// <param name="result">返回的数据</param>
        /// <param name="DbParas">DB参数</param>
        /// <param name="beforSql">分页查询之前执行的sql，不能有表的输出</param>
        /// <param name="afterSql">分布查询之后的查询</param>
        /// <param name="timeout">超时</param>
        public static void Query<T>(PageHelperParas ph_paras, ref JQResult<T> result, Dictionary<string, object> DbParas = null, string beforSql = null, string afterSql = null, int timeout = 120)
            where T : class
        {
            if (ph_paras == null) ph_paras = new PageHelperParas();
            try
            {
                if (ph_paras == null || !new List<int> { 1, 2 }.Contains(ph_paras.types))
                    throw new Exception("ph_paras is null or ph_paras.types is unknown");

                StringBuilder SQL_last = new StringBuilder();

                //if (!string.IsNullOrWhiteSpace(beforSql))
                //    SQL_last.Append(beforSql);

                string SortUnionStr="",tmpSortUnion=ph_paras.SortUnion.eString().ToLower();
                if (!string.IsNullOrWhiteSpace(tmpSortUnion) 
                    && !tmpSortUnion.Contains("__tmp__id")
                    && !tmpSortUnion.Contains(" crownum "))
                        SortUnionStr=tmpSortUnion;

                //SQL_last.AppendFormat("SELECT {2} SUB1.* FROM (SELECT (@CM_ROWNO:=@CM_ROWNO+1) cRowNum,SUB.* FROM ({0}) SUB,(SELECT   @CM_ROWNO:=0) CM_ROWNOTB {1}) SUB1", ph_paras.dataSql, SortUnionStr, (ph_paras.export || ph_paras.types == 2) ? "" : "SQL_CALC_FOUND_ROWS");
                SQL_last.AppendFormat("SELECT {2} SUB1.* FROM (SELECT 1 cRowNum,SUB.* FROM ({0}) SUB,(SELECT   1) CM_ROWNOTB {1}) SUB1", ph_paras.dataSql, SortUnionStr, (ph_paras.export || ph_paras.types == 2) ? "" : "SQL_CALC_FOUND_ROWS");
                //SQL_last.AppendFormat("SELECT {2} SUB1.* FROM (SELECT (@BC_TT_ROWNO:=@BC_TT_ROWNO+1) cRowNum,SUB.* FROM ({0}) SUB,(SELECT   @BC_TT_ROWNO:=0) BC_TT_ROWNOTB {1}) SUB1", ph_paras.dataSql, SortUnionStr, (ph_paras.export || ph_paras.types == 2) ? "" : "SQL_CALC_FOUND_ROWS");
                if (ph_paras.export || ph_paras.types == 2)
                {//导出及treegrid
                    if (!ph_paras.exportAll)//按页导出
                        SQL_last.AppendFormat(" LIMIT {0},{1};", ph_paras.rows * (ph_paras.start_page - 1), ph_paras.rows);
                }
                else
                { //分页查询
                    SQL_last.AppendFormat(" LIMIT {0},{1};", ph_paras.rows * (ph_paras.page - 1), ph_paras.rows);
                    //总记录数
                    SQL_last.Append("SELECT FOUND_ROWS();");
                }

                if (!string.IsNullOrWhiteSpace(afterSql)) SQL_last.Append(afterSql);

                DataSet ds = MySqlHelper.ExecuteDataSet(SQL_last.ToString(), CommandType.Text,DbParas);

                if (ph_paras.export || ph_paras.types == 2)
                {//导出及treegrid
                    if (ds == null || ds.Tables.Count < 1) throw new Exception("查询错误");

                    if (ph_paras.types == 2)//treegrid
                        result.rows = ds.Tables[0].e_DataTableToIList<T>();
                }
                else
                { //分页查询
                    if (ds == null || ds.Tables.Count < 2) throw new Exception("查询错误");

                    result.records = ds.Tables[1].Rows[0][0].eLong();
                    result.rows = ds.Tables[0].e_DataTableToIList<T>();
                }
            }
            catch (Exception err)
            {
                result.records = 0;
                throw err;
            }
            finally
            {
                if (ph_paras != null && ph_paras.types == 1)
                    //计算总页数
                    result.total = Convert.ToInt32(Math.Ceiling(result.records * 1.0 / ph_paras.rows));
            }
        }
    }
}
