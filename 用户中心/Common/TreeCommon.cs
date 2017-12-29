using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Common
{
    /// <summary>
    /// Tree控件通用类
    /// </summary>
  public static class TreeCommon
    {
        /// <summary>
        /// 展开当前节点及其父节点，直到根节点
        /// </summary>
        /// <param name="node"></param>
        public static void ExpandNode(TreeNode node)
        {
            node.Expand();//展开当前节点
            TreeNode parentNode = node.Parent;
            if (parentNode != null)
            {
                parentNode.Expand();//展开父节点
                ExpandNode(parentNode);
            }
        }
        /// <summary>
        /// 绑定组织机构树
        /// </summary>
        public static void BindTree(TreeView tv,DataTable dt,string firsNodeTitle,string FirstNodeValue)
        {
            tv.Nodes.Clear();//清空树节点
            //if (ddlSchool.SelectedItem.Value != "0")
            //{
            //BindAllDept();//绑定所有的机构信息
            //TreeNode tnFirst=new TreeNode(HandlerLogic.GetAdminViewName(), "0");//初始化根节点
            TreeNode tnFirst = new TreeNode(firsNodeTitle, FirstNodeValue);//初始化根节点
            tnFirst.Expand();//展开根节点
            tv.Nodes.Add(tnFirst);//TreeView添加根节点
            BindChidNodes(tnFirst, dt);//绑定子节点
            //}
        }
        /// <summary>
        /// 绑定子节点
        /// </summary>
        /// <param name="tnParent">父节点</param>
        public static void BindChidNodes(TreeNode tnParent,DataTable dt)
        {
            DataView dv = dt.DefaultView;
            dv.RowFilter = "LSJGH='" + tnParent.Value + "'";
            DataTable childDt = dv.ToTable();
            for (int i = 0; i < childDt.Rows.Count; i++)
            {
                TreeNode childNode = new TreeNode();
                if (childDt.Rows[i]["JGMC"] != null)
                {
                    childNode.Text = childDt.Rows[i]["JGMC"].ToString();
                }
                if (childDt.Rows[i]["XXZZJGH"] != null)
                {
                    childNode.Value = childDt.Rows[i]["XXZZJGH"].ToString();
                }
                childNode.CollapseAll();
                tnParent.ChildNodes.Add(childNode);
                BindChidNodes(childNode, dt);
            }
        }
    }
}
