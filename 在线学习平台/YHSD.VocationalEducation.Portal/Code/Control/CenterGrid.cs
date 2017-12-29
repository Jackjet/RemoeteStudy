using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace YHSD.VocationalEducation.Portal.Code.Control
{
    [ToolboxData("<{0}:CenterGrid runat=\"server\" AutoGenerateColumns=\"false\"><Columns><{0}:IndexColumn HeaderText=\"序号\" /></Columns></{0}:CenterGrid>")]
    public class CenterGrid : DataGrid
    {
        private bool mSettingDefaultCSS = true;
        public bool SettingDefaultCSS
        {
            get
            {
                return mSettingDefaultCSS;
            }
            set
            {
                this.mSettingDefaultCSS = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            this.CssClass = "List";
            base.OnLoad(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnItemCreated(DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                e.Item.CssClass = "GridHeader";
                if (this.Columns[0].GetType().Name == "IndexColumn")
                    e.Item.Cells[0].CssClass = "IndexCell";
            }
            else if (e.Item.ItemType == ListItemType.Pager && this.AllowPaging == true)
            {
                e.Item.Cells[0].Controls.Clear();
                CenterGridPageNav nav = new CenterGridPageNav(this);
                nav.FirstClick += NavButton_Click;
                nav.PrevClick += NavButton_Click;
                nav.NextClick += NavButton_Click;
                nav.LastClick += NavButton_Click;
                nav.GoClick += NavButton_Click;
                e.Item.Cells[0].Controls.Add(nav);
                e.Item.CssClass = "PageNav";
            }
            else if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.EditItem || e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.SelectedItem)
            {
                if (this.Columns[0].GetType().Name == "IndexColumn")
                {
                    TableCell cell = e.Item.Cells[0];
                    cell.CssClass = "IndexCell";
                    int pageIndex = this.CurrentPageIndex;
                    int pageSize = this.PageSize;
                    cell.Text = (pageIndex * pageSize + (e.Item.ItemIndex + 1)).ToString();
                }
                if (e.Item.ItemIndex == 0)
                {
                    e.Item.CssClass = "ListFirstItem";
                }
                else if (e.Item.ItemIndex > 0 && e.Item.ItemIndex % 2 == 0)
                {
                    if (this.SettingDefaultCSS)
                    {
                        e.Item.CssClass = "ListItem2";
                    }
                }
                else
                {
                    if (this.SettingDefaultCSS)
                    {
                        e.Item.CssClass = "ListItem1";
                    }
                }
            }
            base.OnItemCreated(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="__NewIndex"></param>
        internal void OnPageIndexChanging2(int __NewIndex)
        {
            DataGridPageChangedEventArgs e = new DataGridPageChangedEventArgs(null, __NewIndex);
            base.OnPageIndexChanged(e);
        }

        internal void NavButton_Click(object sender, EventArgs e)
        {
            Button _bt = (Button)sender;
            int newIndex = 0;
            switch (_bt.CommandArgument)
            {
                case "First":
                    newIndex = 0;
                    break;
                case "Prev":
                    newIndex = this.CurrentPageIndex - 1;
                    break;
                case "Next":
                    newIndex = this.CurrentPageIndex + 1;
                    break;
                case "Last":
                    newIndex = this.PageCount - 1;
                    break;
                default:
                    newIndex = int.Parse(_bt.CommandArgument);
                    break;
            }
            this.OnPageIndexChanging2(newIndex);
        }

        protected override void OnDataBinding(EventArgs e)
        {
            DataBinding(e);
        }

        private void DataBinding(EventArgs e)
        {
            try
            {
                base.OnDataBinding(e);
            }
            catch (Exception ee)
            {
                if (ee.Message.IndexOf("CurrentPageIndex") > -1 && ee.Message.IndexOf("PageCount") > -1)
                {
                    this.CurrentPageIndex -= 1;
                    DataBinding(e);
                }
                else
                    throw ee;
            }
        }
    }
}