using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;
using Common;


namespace UserCenterSystem
{
    public partial class UserManage : BaseInfo
    {
        public string strDepartMentID = "";

        Base_StudentBLL stuBll = new Base_StudentBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region //log4net测试
                //try
                //{
                //    int i = 1;
                //    int j = 0;
                //    int a = i / j;

                //}
                //catch (Exception ex)
                //{

                //    Base_Teacher teacher = Session[UCSKey.SESSION_LoginInfo] as Base_Teacher;

                //    if (!string.IsNullOrEmpty(teacher.SFZJH))
                //        if (teacher != null)
                //        {
                //            //记录错误日志
                //            LogHelper.WriteErrorLog("\r\n用户ID:" + teacher.YHZH + "\r\n用户名:" + teacher.XM + "\r\n客户机IP:" + Request.UserHostAddress + "\r\n错误地址:" + Request.Url + "\r\n异常信息:" + Server.GetLastError().Message, ex);
                //            //记录普通日志
                //           // LogHelper.WriteInfoLog("\r\n用户ID:" + teacher.YHZH + "\r\n用户名:" + teacher.XM + "\r\n客户机IP:" + Request.UserHostAddress + "\r\n错误地址:" + Request.Url + "\r\n错误时间:" + DateTime.Now);
                //            //记录debug日志
                //           // LogHelper.WriteDebugLog("\r\n用户ID:" + teacher.YHZH + "\r\n用户名:" + teacher.XM + "\r\n客户机IP:" + Request.UserHostAddress + "\r\n错误地址:" + Request.Url + "\r\n异常信息:" + Server.GetLastError().Message, ex);

                //        }
                //}

                #endregion
                BindSchoolTree();
            }
        }

        private void BindSchoolTree()
        {
            try
            {
                //-----根据LoginName查询当前登录用户权限（即其学校组织机构号) 
                Base_Teacher teacher = Session[UCSKey.SESSION_LoginInfo] as Base_Teacher;
                Base_DepartmentBLL deptBll = new Base_DepartmentBLL();
                DataTable deptList = deptBll.SelectXJByLoginName(teacher);

                tvOU.Nodes.Clear();//清空树节点
                TreeNode tnFirst = new TreeNode("延庆", "0");//初始化根节点
                tnFirst.NavigateUrl = "#";
                strDepartMentID = tnFirst.Value;
                tnFirst.Expand();//展开根节点
                tvOU.Nodes.Add(tnFirst);//TreeView添加根节点
                tvOU.ExpandAll();
                if (deptList != null)
                {
                    foreach (DataRow drs in deptList.Rows)
                    {
                        TreeNode tnSchool = new TreeNode(drs["JGMC"].ToString(), drs["XXZZJGH"].ToString());
                        tnSchool.Collapse();
                        tnSchool.NavigateUrl = Page.ResolveUrl("Studentlist.aspx?xxzzjgh=" + drs["XXZZJGH"].ToString());
                        tnSchool.Target = "contentFrame";
                        tnFirst.ChildNodes.Add(tnSchool);//TreeView添加根节点
                        DataTable dttree = stuBll.GetDepartMentTree(tnSchool.Value);
                        CreateTreeViewRecursive(tnSchool, dttree, tnSchool.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                DAL.LogHelper.WriteLogError(ex.ToString());
            }
        }


       
        /// <summary>
        /// 绑定父级--年级
        /// </summary>
        /// <param name="dt"></param>
        private void CreateTree(DataTable dt, TreeNode parentNode)
        {
            try
            {
                DataRowCollection drColls = dt.Rows;
                string strRow = string.Empty;
                foreach (DataRow dr in drColls)
                {
                    if (strRow != dr["xxzzjgh"].ToString())
                    {
                        strRow = dr["xxzzjgh"].ToString();
                        TreeNode node = new TreeNode(dr["jgjc"].ToString(), dr["xxzzjgh"].ToString());
                        node.NavigateUrl = Page.ResolveUrl("Studentlist.aspx?xxzzjgh=" + dr["xxzzjgh"].ToString());
                        node.Target = "contentFrame";
                        parentNode.ChildNodes.Add(node);
                        CreateTreeViewRecursive(node, dt, dr["xxzzjgh"].ToString());
                    }
                }
            } 
            catch (Exception ex)
            {
                DAL.LogHelper.WriteLogError(ex.ToString());
            }
        }
        /// <summary>
        /// 递归绑定 treeview 绑定年级
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="dataSource"></param>
        /// <param name="superiorId"></param>
        private void CreateTreeViewRecursive(TreeNode parentNode, DataTable dataSource, string superiorId)
        {


            DataRow[] drarr = dataSource.Select(" xxzzjgh ='" + superiorId + "'");
            TreeNode node;
            string strNJ = string.Empty;
            foreach (DataRow dr in drarr)
            {
                if (strNJ != dr["nj"].ToString())
                {
                    strNJ = dr["nj"].ToString();
                    node = new TreeNode();
                    node.Text = dr["njmc"].ToString();
                    node.Value = dr["nj"].ToString();
                    node.NavigateUrl = Page.ResolveUrl("Studentlist.aspx?xxzzjgh=" + superiorId + "&&nj=" + dr["nj"].ToString());
                    node.Target = "contentFrame";
                    parentNode.ChildNodes.Add(node);
                    CreateTreeViewRecursive2(node, dataSource, dr["nj"].ToString());
                }

            }

        }
        /// <summary>
        /// tree 循环
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="dataSource"></param>
        /// <param name="superiorId"></param>
        private void CreateTreeViewRecursive2(TreeNode parentNode, DataTable dataSource, string superiorId)
        {


            DataRow[] drarr = dataSource.Select(" nj ='" + superiorId + "'");
            TreeNode node;
            string strNJ = string.Empty;
            foreach (DataRow dr in drarr)
            {

                node = new TreeNode();
                node.Text = dr["bj"].ToString();
                node.Value = dr["bjbh"].ToString();
                node.NavigateUrl = Page.ResolveUrl("Studentlist.aspx?bjbh=" + dr["bjbh"].ToString());
                node.Target = "contentFrame";
                parentNode.ChildNodes.Add(node);


            }

        }



    }
}