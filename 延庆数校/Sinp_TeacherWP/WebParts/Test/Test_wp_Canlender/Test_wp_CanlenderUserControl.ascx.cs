

using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using DayPilot.Web.Ui;
using DayPilot.Web.Ui.Conflict;
using DayPilot.Web.Ui.Data;
using DayPilot.Web.Ui.Enums;
using DayPilot.Web.Ui.Events;
using DayPilot.Web.Ui.Recurrence;
using DayPilot.Web.Ui.Events.Bubble;
using DayPilot.Web.Ui.Events.Calendar;

namespace Sinp_TeacherWP.WebParts.Test.Test_wp_Canlender
{
    public partial class Test_wp_CanlenderUserControl : UserControl
    {
        private DataTable table;
        protected void Page_Load(object sender, EventArgs e)
        {
            #region Data loading initialization
            //if (Session["AllFeatures"] == null)
            //{
            //    Session["AllFeatures"] = DataGeneratorCalendar.GetData();
            //}
            table = (DataTable)Session["AllFeatures"];
            DayPilotCalendar1.DataSource = Session["AllFeatures"];
            DayPilotNavigator1.DataSource = Session["AllFeatures"];
            #endregion

            if (!IsPostBack)
            {
                DataBind();
                DayPilotCalendar1.UpdateWithMessage("Welcome!");
            }
        }

        protected void DayPilotCalendar1_EventMove(object sender, EventMoveEventArgs e)
        {

            #region Simulation of database update

            //DataRow dr = table.Rows.Find(e.Id);
            //if (dr != null)
            //{
            //    dr["start"] = e.NewStart;
            //    dr["end"] = e.NewEnd;
            //    table.AcceptChanges();
            //}

            #endregion

            //DayPilotCalendar1.DataBind();
            //DayPilotCalendar1.UpdateWithMessage("Event moved.");
            //DayPilotCalendar1.Update();

        }


        protected void DayPilotCalendar1_EventMenuClick(object sender, EventMenuClickEventArgs e)
        {
            if (e.Command == "Delete")
            {
                //#region Simulation of database update
                //DataRow dr = table.Rows.Find(e.Id);

                //if (dr != null)
                //{
                //    table.Rows.Remove(dr);
                //    table.AcceptChanges();
                //}
                //#endregion

                //DayPilotCalendar1.DataBind();
                //DayPilotCalendar1.Update();
            }
        }


        protected void DayPilotCalendar1_TimeRangeSelected(object sender, TimeRangeSelectedEventArgs e)
        {
            
            #region Simulation of database update

            DataRow dr = table.NewRow();
            dr["start"] = e.Start;
            dr["end"] = e.End;
            dr["id"] = Guid.NewGuid().ToString();
            dr["name"] = "New event";

            table.Rows.Add(dr);
            table.AcceptChanges();
            #endregion

            DayPilotCalendar1.DataBind();
            DayPilotCalendar1.UpdateWithMessage("New event created.");
        }

        protected void DayPilotCalendar1_EventResize(object sender, EventResizeEventArgs e)
        {
            // not allowed to resize event no 7
            //if (e.Id == "7")
            //{
            //    DayPilotCalendar1.DataBind();
            //    DayPilotCalendar1.UpdateWithMessage("It's not allowed to resize this event. It was forbidded in the EventResize handler on the server side.");
            //    return;
            //}

            #region Simulation of database update

            //DataRow dr = table.Rows.Find(e.Id);
            //if (dr != null)
            //{
            //    dr["start"] = e.NewStart;
            //    dr["end"] = e.NewEnd;
            //    table.AcceptChanges();
            //}

            #endregion

            DayPilotCalendar1.DataBind();
            DayPilotCalendar1.UpdateWithMessage("Event resized");
        }

        protected void DayPilotCalendar1_EventClick(object sender, EventClickEventArgs e)
        {
            #region Simulation of database update

            //DataRow dr = table.Rows.Find(e.Id);
            //if (dr != null)
            //{
            //    dr["name"] = "Event clicked at " + DateTime.Now;
            //    table.AcceptChanges();
            //}

            #endregion

            DayPilotCalendar1.DataBind();
            DayPilotCalendar1.Update();
        }
        protected void DayPilotCalendar1_EventEdit(object sender, EventEditEventArgs e)
        {
            #region Simulation of database update

            //DataRow dr = table.Rows.Find(e.Id);
            //if (dr != null)
            //{
            //    dr["name"] = e.NewText;
            //    table.AcceptChanges();
            //}

            #endregion

            DayPilotCalendar1.DataBind();
            DayPilotCalendar1.UpdateWithMessage("Event text changed.");
        }
        protected void DayPilotCalendar1_BeforeEventRender(object sender, BeforeEventRenderEventArgs e)
        {
            //e.BubbleHtml = e.Start.ToString();
        }

        protected void DayPilotCalendar1_EventDelete(object sender, EventDeleteEventArgs e)
        {
            #region Simulation of database update

            //DataRow dr = table.Rows.Find(e.Id);
            //if (dr != null)
            //{
            //    table.Rows.Remove(dr);
            //    table.AcceptChanges();
            //}

            #endregion

            DayPilotCalendar1.DataBind();
            DayPilotCalendar1.UpdateWithMessage("Event deleted.");
        }

        protected void DayPilotCalendar1_EventSelect(object sender, DayPilotEventArgs e)
        {
            DayPilotCalendar1.Update();
        }

        protected void DayPilotCalendar1_BeforeHeaderRender(object sender, BeforeHeaderRenderEventArgs e)
        {
        }

        protected void DayPilotBubble1_RenderContent(object sender, RenderEventArgs e)
        {
            if (e is RenderEventBubbleEventArgs)
            {
                RenderEventBubbleEventArgs re = e as RenderEventBubbleEventArgs;

                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("<b>{0}</b><br />", re.Text);
                sb.AppendFormat("Start: {0}<br />", re.Start);
                sb.AppendFormat("End: {0}<br />", re.End);

                //re.InnerHTML = "<b>Event details</b><br />Here is the right place to show details about the event with ID: " + re.Value + ". This text is loaded dynamically from the server.<br/>";
                re.InnerHTML = sb.ToString();
            }
            else if (e is RenderTimeBubbleEventArgs)
            {
                RenderTimeBubbleEventArgs re = e as RenderTimeBubbleEventArgs;
                e.InnerHTML = "<b>Time header details</b><br />From:" + re.Start + "<br />To: " + re.End;
            }
            else if (e is RenderCellBubbleEventArgs)
            {
                RenderCellBubbleEventArgs re = e as RenderCellBubbleEventArgs;
                e.InnerHTML = "<b>Cell details</b><br />Column:" + re.ResourceId + "<br />From:" + re.Start + "<br />To: " + re.End;
            }
        }

        protected void DayPilotCalendar1_TimeRangeDoubleClick(object sender, TimeRangeDoubleClickEventArgs e)
        {

            #region Simulation of database update
            DataRow dr = table.NewRow();
            dr["start"] = e.Start;
            dr["end"] = e.End;
            dr["id"] = Guid.NewGuid().ToString();
            dr["name"] = "New event";

            table.Rows.Add(dr);
            table.AcceptChanges();
            #endregion

            DayPilotCalendar1.DataBind();
            DayPilotCalendar1.UpdateWithMessage("New event created.");
        }

        protected void DayPilotCalendar1_TimeRangeMenuClick(object sender, TimeRangeMenuClickEventArgs e)
        {
            if (e.Command == "Insert")
            {
                #region Simulation of database update

                DataRow dr = table.NewRow();
                dr["start"] = e.Start;
                dr["end"] = e.End;
                dr["id"] = Guid.NewGuid().ToString();
                dr["name"] = "New event";

                table.Rows.Add(dr);
                table.AcceptChanges();
                #endregion

                DayPilotCalendar1.DataBind();
                DayPilotCalendar1.Update();
            }

        }
        //protected void DayPilotCalendar1_Command(object sender, CommandEventArgs e)
        //{
        //    switch (e.Command)
        //    {
        //        case "navigate":
        //            DateTime start = (DateTime)e.Data["start"];
        //            DateTime end = (DateTime)e.Data["end"];
        //            DateTime day = (DateTime)e.Data["day"]; // clicked day

        //            DayPilotCalendar1.StartDate = start;
        //            DayPilotCalendar1.DataBind();
        //            DayPilotCalendar1.UpdateWithMessage("Date changed. You clicked: " + day);
        //            break;
        //        case "refresh":
        //            DayPilotCalendar1.DataBind();
        //            DayPilotCalendar1.UpdateWithMessage("Refreshed.");
        //            break;
        //        case "paste":
        //            DateTime pasteHere = (DateTime)e.Data["start"];
        //            string id = (string)e.Data["id"];

        //            DataRow dr = table.Rows.Find(id);
        //            if (dr != null)
        //            {
        //                TimeSpan duration = ((DateTime)dr["end"]) - ((DateTime)dr["start"]);

        //                DataRow drNew = table.NewRow();
        //                drNew["start"] = pasteHere;
        //                drNew["end"] = pasteHere + duration;
        //                drNew["id"] = Guid.NewGuid().ToString();
        //                drNew["name"] = "Copy of " + dr["name"];

        //                table.Rows.Add(drNew);
        //                table.AcceptChanges();
        //            }
        //            DayPilotCalendar1.DataBind();
        //            DayPilotCalendar1.UpdateWithMessage("Event copied.");
        //            break;
        //        case "test":
        //            DayPilotCalendar1.CellDuration = 60;
        //            DayPilotCalendar1.DataBind();
        //            DayPilotCalendar1.UpdateWithMessage("Updated");
        //            break;

        //    }
        //}

        protected void DayPilotNavigator1_VisibleRangeChanged(object sender, EventArgs e)
        {
            DayPilotNavigator1.DataBind();
        }

        protected void DayPilotCalendar1_BeforeCellRender(object sender, BeforeCellRenderEventArgs e)
        {
            if (e.Start.Day % 2 == 0)
            {
                e.CssClass = "even";
            }
            else
            {
                e.CssClass = "odd";
            }
        }

        protected void DayPilotCalendar1_HeaderClick(object sender, HeaderClickEventArgs e)
        {
        }
        protected void DayPilotNavigator1_TimeRangeSelected(object sender, TimeRangeSelectedEventArgs e)
        {
            //throw new Exception(e.Start.ToString());
        }
        protected void DayPilotCalendar1_BeforeTimeHeaderRender(DayPilot.Web.Ui.Events.Calendar.BeforeTimeHeaderRenderEventArgs ea)
        {
        }

        protected void DayPilotNavigator1_BeforeCellRender(object sender, DayPilot.Web.Ui.Events.Navigator.BeforeCellRenderEventArgs e)
        {
            /*
            if (e.Start.DayOfWeek == DayOfWeek.Friday)
            {
                e.CssClass = "weekend";
            }
             * */
        }

        protected void DayPilotCalendar1_OnEventDoubleClick(object sender, EventClickEventArgs e)
        {
            DayPilotCalendar1.UpdateWithMessage("Doubleclick");
        }
    }
}
