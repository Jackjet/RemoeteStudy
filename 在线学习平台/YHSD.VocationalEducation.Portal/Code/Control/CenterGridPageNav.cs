using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace YHSD.VocationalEducation.Portal.Code.Control
{

    public class CenterGridPageNav : Table
    {
        private int CuIndex, MaxIndex;
        private DataGrid m_GridView;
        protected Button ButtonFirst, ButtonPrev, ButtonNext, ButtonLast, ButtonGo;
        protected TextBox Text;
        public CenterGridPageNav(DataGrid __GridView)
        {
            this.Width = new Unit(100, UnitType.Percentage);
            this.m_GridView = __GridView;
            CuIndex = this.m_GridView.CurrentPageIndex;
            MaxIndex = this.m_GridView.PageCount;
        }
        EventHandler m_FirstClick, m_PrevClick, m_NextClick, m_LastClick, m_GoClick;
        public event EventHandler FirstClick
        {
            add { m_FirstClick += value; }
            remove { m_FirstClick -= value; }
        }
        public event EventHandler PrevClick
        {
            add { m_PrevClick += value; }
            remove { m_PrevClick -= value; }
        }
        public event EventHandler NextClick
        {
            add { m_NextClick += value; }
            remove { m_NextClick -= value; }
        }
        public event EventHandler LastClick
        {
            add { m_LastClick += value; }
            remove { m_LastClick -= value; }
        }
        public event EventHandler GoClick
        {
            add { m_GoClick += value; }
            remove { m_GoClick -= value; }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            TableRow row = new TableRow();
            TableCell cell = new TableCell();
            cell.Text = "&nbsp;";
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Width = new Unit(20);
            Label lb = new Label();
            lb.Text = (CuIndex + 1) + "/" + MaxIndex;
            cell.Controls.Add(lb);
            row.Cells.Add(cell);


            cell = new TableCell();
            cell.Width = new Unit(20);
            ButtonFirst = new Button();

            ButtonFirst.CommandArgument = "First";
            ButtonFirst.Click += new EventHandler(ButtonFirst_Click);
            cell.Controls.Add(ButtonFirst);
            row.Cells.Add(cell);
            cell = new TableCell();
            cell.Width = new Unit(20);
            ButtonPrev = new Button();
            ButtonPrev.CommandArgument = "Prev";
            ButtonPrev.Click += new EventHandler(ButtonPrev_Click);
            cell.Controls.Add(ButtonPrev);
            row.Cells.Add(cell);
            if (this.CuIndex == 0)
            {
                ButtonFirst.Enabled = false;
                ButtonPrev.Enabled = false;
                ButtonFirst.CssClass = "PageNavFirst_Disabled";
                ButtonPrev.CssClass = "PageNavPrev_Disabled";
            }
            else
            {
                ButtonFirst.Enabled = true;
                ButtonPrev.Enabled = true;
                ButtonFirst.CssClass = "PageNavFirst";
                ButtonPrev.CssClass = "PageNavPrev";
            }

            cell = new TableCell();
            cell.Width = new Unit(20);
            Text = new TextBox();
            Text.CssClass = "PageNavText";
            cell.Controls.Add(Text);
            row.Cells.Add(cell);
            this.Text.Text = (CuIndex + 1).ToString();


            cell = new TableCell();
            cell.Width = new Unit(20);
            ButtonNext = new Button();
            ButtonNext.CommandArgument = "Next";
            ButtonNext.Click += new EventHandler(ButtonNext_Click);
            cell.Controls.Add(ButtonNext);
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Width = new Unit(20);
            ButtonLast = new Button();
            ButtonLast.CommandArgument = "Last";
            ButtonLast.Click += new EventHandler(ButtonLast_Click);
            cell.Controls.Add(ButtonLast);
            row.Cells.Add(cell);

            if (this.CuIndex == this.MaxIndex - 1)
            {
                ButtonNext.Enabled = false;
                ButtonLast.Enabled = false;
                ButtonNext.CssClass = "PageNavNext_Disabled";
                ButtonLast.CssClass = "PageNavLast_Disabled";
            }
            else
            {
                ButtonNext.Enabled = true;
                ButtonLast.Enabled = true;
                ButtonNext.CssClass = "PageNavNext";
                ButtonLast.CssClass = "PageNavLast";
            }
            cell = new TableCell();
            cell.Width = new Unit(20);

            ButtonGo = new Button();
            ButtonGo.Click += new EventHandler(ButtonGo_Click);
            cell.Controls.Add(ButtonGo);
            ButtonGo.CssClass = "PageNavGo";
            cell.Width = new Unit(60);
            row.Cells.Add(cell);
            this.Rows.Add(row);
        }

        void ButtonLast_Click(object sender, EventArgs e)
        {
            if (m_LastClick != null)
                m_LastClick(sender, e);
        }

        void ButtonNext_Click(object sender, EventArgs e)
        {
            if (m_NextClick != null)
                m_NextClick(sender, e);
        }

        void ButtonPrev_Click(object sender, EventArgs e)
        {
            if (m_PrevClick != null)
                m_PrevClick(sender, e);
        }

        void ButtonFirst_Click(object sender, EventArgs e)
        {
            if (m_FirstClick != null)
                m_FirstClick(sender, e);
        }

        void ButtonGo_Click(object sender, EventArgs e)
        {
            Button bt = (Button)sender;
            bt.CommandArgument = (int.Parse(this.Text.Text.Trim()) - 1).ToString();
            if (m_GoClick != null)
                m_GoClick(bt, e);
        }
    }
}
