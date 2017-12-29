using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace Common
{
    public class TreeViewHelp
    {
        /// <summary>
        /// 寻找点击的子节点
        /// </summary>
        /// <param name="tnodes"></param>
        /// <param name="Selectnode"></param>
        public void ExpandNodes(TreeNodeCollection tnodes, TreeNode Selectnode)
        {
            try
            {
                string Path = "";
                if (!string.IsNullOrWhiteSpace(Selectnode.ValuePath))
                {
                    if (Selectnode.ValuePath.ToString().Substring(0, 1) != "0")
                    {
                        Path = "0" + Selectnode.ValuePath.ToString();
                    }
                    else
                    {
                        Path = Selectnode.ValuePath.ToString();
                    }
                }
                foreach (TreeNode node in tnodes)
                {
                    if (node.ValuePath.ToString() == Path)//比较判断，所以需要知道所选节点的父节点名字：node.Parent.Text
                    {
                        node.Select();
                        ExpandParentNodes(node);//调用下一个递归方法
                        return;
                    }
                    ExpandNodes(node.ChildNodes, Selectnode);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 展开父级节点
        /// </summary>
        /// <param name="tnodes"></param>
        public void ExpandParentNodes(TreeNode tnodes)
        {
            TreeNode parentNode;
            parentNode = tnodes;
            if (parentNode.Parent != null)
            {
                parentNode.Parent.Expand();
                if (parentNode.Parent.Parent != null)//判断父节点的父节点是否为空，如果已经达到根节点就是null
                {
                    ExpandParentNodes(parentNode.Parent);
                }
                else
                    return;//已经达到根节点，退出
            }
        }
    }
}
