using SVDigitalCampus.Common;
using Microsoft.SharePoint;
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Configuration;
using Common;

namespace SVDigitalCampus.Canteen_Ordering.CO_wp_Order
{
    public partial class CO_wp_OrderUserControl : UserControl
    {
        public string OrderID = "";
        public DataTable Orderdb = new DataTable();
        public decimal TlMoney = 0;
        public int OrderCount = 0;
        public int MenuCount = 0;
        public decimal MMoney = 0;
        public int MCount = 0;
        public decimal LMoney = 0;
        public int LCount = 0;
        public decimal DMoney = 0;
        public int DCount = 0;
        public SPWeb web = SPContext.Current.Web;
        public LogCommon log = new LogCommon();
        public static GetSPWebAppSetting appsetting = new GetSPWebAppSetting();
        public static string layouturl =appsetting.Handlerurl;
        public string weekday
        {
            get
            {
                if (ViewState["weekday"] != null)
                {
                    return ViewState["weekday"].ToString();
                }
                return DateTime.Today.safeToString();
            }
            set
            {
                ViewState["weekday"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                //判断登录
                //SPWeb web = SPContext.Current.Web;
                //string groupname = appsetting.MasterGroup;
                //string Normalname =appsetting.NormalGroup;
                //if (!CheckUserPermission.JudgeUserPermission(groupname) && !CheckUserPermission.JudgeUserPermission(Normalname))
                //{
                //    string loginurl = CheckUserPermission.ToLoginUrl("Order");
                //    if (string.IsNullOrEmpty(loginurl))
                //    {
                //        Response.Redirect(loginurl);//跳转到重新登录页面
                //        return;
                //    }
                //    else
                //    {

                //        Response.Redirect(appsetting.Layoutsurl + "/SingOut.aspx");//跳转到退出登录页面
                //        return;
                //    }
                //}
                if (!string.IsNullOrEmpty(Request.QueryString["action"]) && Request.QueryString["action"].Equals("ChangeNum"))
                {
                    SPWeb sweb = SPContext.Current.Web;
                    string mealtype = Request["type"];
                    string datestr = Request["date"];
                    string id = Request["id"];
                    string num = Request["num"];

                    if (string.IsNullOrEmpty(mealtype))
                    {
                        Response.Write("0|");
                        return;
                    }
                    if (string.IsNullOrEmpty(id))
                    {
                        Response.Write("0|");
                        return;
                    }
                    if (string.IsNullOrEmpty(num))
                    {
                        Response.Write("0|");
                        return;
                    }
                    CartHandle.EditShoppingCar(mealtype, sweb.CurrentUser.Name, DateTime.Today, false, id, num, 1);
                    Response.Write("1|");
                    return;
                }
                //else if(!string.IsNullOrEmpty(Request.QueryString["action"]) && Request.QueryString["action"].Equals("SubmitOrder"))
                #region 购物车数量加

                //加
                else if (!string.IsNullOrEmpty(Request.QueryString["action"]) && Request.QueryString["action"] == "AddNum")
                {
                    SPWeb sweb = SPContext.Current.Web;
                    string mealtype = Request["type"];
                    string id = Request["id"];
                    string num = Request["num"];

                    if (string.IsNullOrEmpty(mealtype))
                    {
                        Response.Write("0|");
                        return;
                    }
                    if (string.IsNullOrEmpty(id))
                    {
                        Response.Write("0|");
                        return;
                    }
                    if (string.IsNullOrEmpty(num))
                    {
                        Response.Write("0|");
                        return;
                    }
                    CartHandle.EditShoppingCar(mealtype, sweb.CurrentUser.Name, DateTime.Today, true, id, num, 1);
                    Response.Write("1|");
                    return;
                }
                #endregion
                #region 购物车数量减


                //减
                else if (!string.IsNullOrEmpty(Request.QueryString["action"]) && Request.QueryString["action"] == "ReduceNum")
                {
                    SPWeb sweb = SPContext.Current.Web;
                    string mealtype = Request["type"];
                    string id = Request["id"];
                    string num = Request["num"];

                    if (string.IsNullOrEmpty(mealtype))
                    {
                        Response.Write("0|");
                        return;
                    }
                    if (string.IsNullOrEmpty(id))
                    {
                        Response.Write("0|");
                        return;
                    }
                    if (string.IsNullOrEmpty(num))
                    {
                        Response.Write("0|");
                        return;
                    }
                    CartHandle.EditShoppingCar(mealtype, sweb.CurrentUser.Name, DateTime.Today, true, id, num, 1);
                    Response.Write("1|");
                    return;
                }
                #endregion

                //else if (!string.IsNullOrEmpty(Request.QueryString["action"]) && Request.QueryString["action"].Equals("GetNowTime"))
                //{

                //    Response.Write(MealTypeJudge.WorkSPTime(MealTypeJudge.GetMealType()) + "|");
                //    return;
                //}
                else
                {
                     weekday = Request["WeekDate"].safeToString() == string.Empty ? DateTime.Today.safeToString() : Request["WeekDate"].safeToString();
                     DateTime weektime = DateTime.Parse(weekday);
                    btOrder.Enabled = BindListView(weektime);
                    string sptime = MealTypeJudge.WorkSPTime(MealTypeJudge.GetMealType(), weektime);
                    OrderTime.Text = sptime;
                    if (sptime.Equals("0小时0分0秒"))
                    {
                        btOrder.Enabled = false;
                    }
                }
            }
        }


        protected bool BindListView(DateTime weektime)
        {
            SPWeb sweb = SPContext.Current.Web;
            bool isshow = false;
            try
            {

                //获取绑定三餐购物车数据
                //早餐
                Orderdb = CartHandle.GetCart("1", sweb.CurrentUser.Name, weektime);
                if (Orderdb != null && Orderdb.Rows.Count > 0)
                {
                    MCount = 0; MMoney = 0;
                    foreach (DataRow item in Orderdb.Rows)
                    {
                        MCount += int.Parse(item["Number"].ToString());
                        MMoney += decimal.Parse(item["Price"].ToString()) * int.Parse(item["Number"].ToString());
                        OrderCount += int.Parse(item["Number"].ToString());
                        MenuCount++;
                        //计算合计
                        TlMoney += decimal.Parse(item["Price"].ToString()) * int.Parse(item["Number"].ToString());

                    }

                    isshow = true;

                }
                lv_MorningOrder.DataSource = Orderdb;
                lv_MorningOrder.DataBind();
                //午餐
                Orderdb = CartHandle.GetCart("2", sweb.CurrentUser.Name, weektime);
                if (Orderdb != null && Orderdb.Rows.Count > 0)
                {
                    LCount = 0; LMoney = 0;
                    foreach (DataRow item in Orderdb.Rows)
                    {
                        LCount += int.Parse(item["Number"].ToString());
                        LMoney += decimal.Parse(item["Price"].ToString()) * int.Parse(item["Number"].ToString());
                        OrderCount += int.Parse(item["Number"].ToString());
                        MenuCount++;
                        //计算合计
                        TlMoney += decimal.Parse(item["Price"].ToString()) * int.Parse(item["Number"].ToString());

                    }

                    isshow = true;

                }
                lv_LunchOrder.DataSource = Orderdb;
                lv_LunchOrder.DataBind();
                //晚餐
                Orderdb = CartHandle.GetCart("3", sweb.CurrentUser.Name, weektime);
                if (Orderdb != null && Orderdb.Rows.Count > 0)
                {
                    DCount = 0; DMoney = 0;
                    foreach (DataRow item in Orderdb.Rows)
                    {
                        DCount += int.Parse(item["Number"].ToString());
                        DMoney += decimal.Parse(item["Price"].ToString()) * int.Parse(item["Number"].ToString());
                        OrderCount += int.Parse(item["Number"].ToString());
                        MenuCount++;
                        //计算合计
                        TlMoney += decimal.Parse(item["Price"].ToString()) * int.Parse(item["Number"].ToString());

                    }

                    isshow = true;

                }
                lv_DinnerOrder.DataSource = Orderdb;
                lv_DinnerOrder.DataBind();
                //绑定三餐名称、菜品份数和金额数据
                Label ordercount = this.FindControl("Totalcount") as Label;
                Label totalmoney = this.FindControl("totalmoney") as Label;
                totalmoney.Text = TlMoney.ToString();
                ordercount.Text = OrderCount.ToString();

            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "确认订单数据绑定");
            }
            return isshow;

        }
        /// <summary>
        /// 早餐行命令
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lv_MorningOrder_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string script = string.Empty;
            bool pageMode = false;
            try
            {
                SPWeb sweb = SPContext.Current.Web;
                int itemId = Convert.ToInt32(e.CommandArgument.ToString());
                HtmlInputControl number = e.Item.FindControl("txtNumber") as HtmlInputControl;
                Label ordercount = this.FindControl("Totalcount") as Label;
                Label totalmoney = this.FindControl("totalmoney") as Label;
                HtmlInputControl price = e.Item.FindControl("price") as HtmlInputControl;
                //Label mealtype = this.FindControl("mealtype") as Label;
                string Ordertype = "1";//mealtype.Text == "早餐" ? "1" : mealtype.Text == "午餐" ? "2" : "3";
                if (e.CommandName.Equals("del"))
                {
                    Label meauCount = lv_MorningOrder.FindControl("Morningcount") as Label;
                    Label mmoney = lv_MorningOrder.FindControl("Morningmoney") as Label;
                    meauCount.Text = (int.Parse(meauCount.Text) - 1).ToString();
                    mmoney.Text = (decimal.Parse(mmoney.Text) - (int.Parse(meauCount.Text) * decimal.Parse(price.Value))).ToString();
                    //修改菜品数量，合计
                    ordercount.Text = (int.Parse(ordercount.Text) - int.Parse(number.Value)).ToString();
                    totalmoney.Text = (decimal.Parse(totalmoney.Text) - (int.Parse(number.Value) * decimal.Parse(price.Value))).ToString();

                    //删除
                    script = Delete(Ordertype, itemId.ToString(), script, DateTime.Parse(weekday));
                    btOrder.Enabled = BindListView( DateTime.Parse(weekday));
                    pageMode = true;
                }
                else if (e.CommandName.Equals("reduce"))
                {//减数量
                    if (int.Parse(number.Value) - 1 > 0 && (decimal.Parse(totalmoney.Text) - decimal.Parse(price.Value)) > 0)
                    {
                        DateTime date = DateTime.Parse(weekday);
                        CartHandle.EditShoppingCar(Ordertype, sweb.CurrentUser.Name, date, true, itemId.ToString(), "-1", 1);
                        BindListView(date);
                    }
                }
                else if (e.CommandName.Equals("add"))//加数量
                {
                    DateTime date = DateTime.Parse(weekday);
                    CartHandle.EditShoppingCar(Ordertype, sweb.CurrentUser.Name, date, true, itemId.ToString(), "1", 1);
                    BindListView(date);

                }
            }
            catch (Exception ex)
            {
                log.writeLogMessage(ex.Message, "确认订单的早餐数据操作");
                if (pageMode)
                {
                    script = "alert('删除失败！');";
                }
            }
            //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", script, true);
        }
        /// <summary>
        /// 午餐行命令
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lv_LunchOrder_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string script = string.Empty;
            bool pageMode = false;
            try
            {
                SPWeb sweb = SPContext.Current.Web;
                int itemId = Convert.ToInt32(e.CommandArgument.ToString());
                HtmlInputControl number = e.Item.FindControl("txtNumber") as HtmlInputControl;
                Label ordercount = this.FindControl("Totalcount") as Label;
                Label totalmoney = this.FindControl("totalmoney") as Label;
                HtmlInputControl price = e.Item.FindControl("price") as HtmlInputControl;
                // Label mealtype = this.FindControl("mealtype") as Label;
                string Ordertype = "2";// mealtype.Text == "早餐" ? "1" : mealtype.Text == "午餐" ? "2" : "3";
                if (e.CommandName.Equals("del"))
                {
                    Label meauCount = lv_LunchOrder.FindControl("Lunchcount") as Label;
                    Label lmoney = lv_LunchOrder.FindControl("Lunchmoney") as Label;
                    meauCount.Text = (int.Parse(meauCount.Text) - 1).ToString();
                    lmoney.Text = (decimal.Parse(lmoney.Text) - (int.Parse(meauCount.Text) * decimal.Parse(price.Value))).ToString();
                    //修改菜品数量，合计
                    ordercount.Text = (int.Parse(ordercount.Text) - int.Parse(number.Value)).ToString();
                    totalmoney.Text = (decimal.Parse(totalmoney.Text) - (int.Parse(number.Value) * decimal.Parse(price.Value))).ToString();

                    //删除
                    script = Delete(Ordertype, itemId.ToString(), script, DateTime.Parse(weekday));
                    btOrder.Enabled = BindListView(DateTime.Parse(weekday));
                    pageMode = true;
                }
                else if (e.CommandName.Equals("reduce"))
                {//减数量
                    if (int.Parse(number.Value) - 1 > 0 && (decimal.Parse(totalmoney.Text) - decimal.Parse(price.Value)) > 0)
                    {
                        DateTime date = DateTime.Parse(weekday);
                        CartHandle.EditShoppingCar(Ordertype, sweb.CurrentUser.Name, date, true, itemId.ToString(), "-1", 1);
                        BindListView(date);

                    }
                }
                else if (e.CommandName.Equals("add"))//加数量
                {
                    DateTime date = DateTime.Parse(weekday);
                    CartHandle.EditShoppingCar(Ordertype, sweb.CurrentUser.Name, date, true, itemId.ToString(), "1", 1);
                    BindListView(date);

                }
            }
            catch (Exception ex)
            {
                log.writeLogMessage(ex.Message, "确认订单的午餐数据操作");
                if (pageMode)
                {
                    script = "alert('删除失败！');";
                }
            }
            //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", script, true);
        }
        /// <summary>
        /// 晚餐行命令
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lv_DinnerOrder_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string script = string.Empty;
            bool pageMode = false;
            try
            {
                SPWeb sweb = SPContext.Current.Web;
                int itemId = Convert.ToInt32(e.CommandArgument.ToString());
                HtmlInputControl number = e.Item.FindControl("txtNumber") as HtmlInputControl;
                Label ordercount = this.FindControl("Totalcount") as Label;
                Label totalmoney = this.FindControl("totalmoney") as Label;
                HtmlInputControl price = e.Item.FindControl("price") as HtmlInputControl;
                //Label mealtype = this.FindControl("mealtype") as Label;
                string Ordertype = "3";// mealtype.Text == "早餐" ? "1" : mealtype.Text == "午餐" ? "2" : "3";
                if (e.CommandName.Equals("del"))
                {
                    Label meauCount =lv_DinnerOrder.FindControl("Dinnercount") as Label;
                    Label dmoney = lv_DinnerOrder.FindControl("Dinnermoney") as Label;
                    meauCount.Text = (int.Parse(meauCount.Text) - 1).ToString();
                    dmoney.Text = (decimal.Parse(dmoney.Text) - (int.Parse(meauCount.Text) * decimal.Parse(price.Value))).ToString();
                    //修改菜品数量，合计
                    ordercount.Text = (int.Parse(ordercount.Text) - int.Parse(number.Value)).ToString();
                    totalmoney.Text = (decimal.Parse(totalmoney.Text) - (int.Parse(number.Value) * decimal.Parse(price.Value))).ToString();

                    //删除
                    script = Delete(Ordertype, itemId.ToString(), script, DateTime.Parse(weekday));
                    btOrder.Enabled = BindListView(DateTime.Parse(weekday));
                    pageMode = true;
                }
                else if (e.CommandName.Equals("reduce"))
                {//减数量
                    if (int.Parse(number.Value) - 1 > 0 && (decimal.Parse(totalmoney.Text) - decimal.Parse(price.Value)) > 0)
                    {
                        DateTime date = DateTime.Parse(weekday);
                        CartHandle.EditShoppingCar(Ordertype, sweb.CurrentUser.Name, date, true, itemId.ToString(), "-1", 1);
                        BindListView(date);
                    }
                }
                else if (e.CommandName.Equals("add"))//加数量
                {
                    DateTime date = DateTime.Parse(weekday);
                    CartHandle.EditShoppingCar(Ordertype, sweb.CurrentUser.Name, date, true, itemId.ToString(), "1", 1);
                    BindListView(date);
                }
            }
            catch (Exception ex)
            {
                log.writeLogMessage(ex.Message, "确认订单晚餐数据操作");
                if (pageMode)
                {
                    script = "alert('操作失败！');";
                }
            }
            //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", script, true);
        }
        /// <summary>
        /// 提交订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btOrder_Click(object sender, EventArgs e)
        {
            int result = 0;
            int j = 0;
            try
            {

                for (int mealtype = 1; mealtype <= 3; mealtype++)
                {
                    //判断是否超时，若超时则删除该购物车数据
                    string type = mealtype.ToString();
                    string time = MealTypeJudge.WorkSPTime(type,DateTime.Parse(weekday));
                    if (time.Equals("0小时0分0秒"))
                    {
                        CartHandle.RemoveCart(mealtype.ToString(), SPContext.Current.Web.CurrentUser.ToString(), DateTime.Parse(weekday));
                        continue;
                    }
                    Orderdb = CartHandle.GetCart(mealtype.ToString(), web.CurrentUser.Name, DateTime.Parse(weekday));
                    decimal TotalMoney = 0;
                    int MenuCount = 0;
                    int i = 0;
                    //修改购物车数据
                    if (Orderdb != null && Orderdb.Rows.Count > 0)
                    {
                        j++;
                        foreach (DataRow item in Orderdb.Rows)
                        {
                            //获取数量
                            HtmlInputText number = new HtmlInputText();
                            switch (mealtype)
                            {
                                case 1:
                                    number = lv_MorningOrder.Items[i].FindControl("txtNumber") as HtmlInputText;
                                    break;
                                case 2:
                                    number = lv_LunchOrder.Items[i].FindControl("txtNumber") as HtmlInputText;
                                    break;
                                case 3:
                                    number = lv_DinnerOrder.Items[i].FindControl("txtNumber") as HtmlInputText;
                                    break;
                            }
                            string num = string.Empty;
                            if (int.Parse(number.Value.ToString()) > 0) { num = number.Value; }
                            else { num = "1"; }
                            MenuCount += int.Parse(num);
                            TotalMoney += int.Parse(num) * decimal.Parse(item["Price"].ToString());
                            //修改购物车
                            CartHandle.EditShoppingCar(mealtype.ToString(), web.CurrentUser.Name, DateTime.Parse(weekday), false, item["id"].ToString(), num, 1);
                            i++;
                        }
                        if (SubmitOrder(TotalMoney, MenuCount, mealtype.ToString(), DateTime.Parse(weekday)))//生成订单
                        {
                            result++;
                            //删除购物车数据
                            CartHandle.RemoveCart(mealtype.ToString(), web.CurrentUser.Name, DateTime.Parse(weekday));
                        }

                    }
                    
                }
                //判断是否成功
                if (result == j&&j!=0)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "Success();", true);
                } 
                else if (j == 0)  { this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "alert('提交失败,请返回挑选菜品！');", true); return; }
                else if (result != j)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "alert('提交成功部分菜品，存在菜品提交失败！');", true);
                }
              
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "确认订单页面的生产订单");
            }
        }
        //生成订单数据
        public bool SubmitOrder(decimal MoneyTotal, int MenuCount, string Ordertype,DateTime date)
        {
            Orderdb = CartHandle.GetCart(Ordertype, web.CurrentUser.Name, date);
            bool result = false;
            SPList list = web.Lists.TryGetList("订单");
            DataTable menudb = MenuManger.GetMenuList(true);
            if (list != null)
            {
                if (Orderdb != null && Orderdb.Rows.Count > 0)
                {
                    //新增订单记录
                    SPListItem newOrder = list.Items.Add();
                    newOrder["Title"] = CreateNumbers.NewNumber();
                    newOrder["User"] = web.CurrentUser;
                    newOrder["State"] = "成功";
                    newOrder["Type"] = Ordertype;
                    newOrder["OrderDate"] = date;
                    string menusstring = string.Empty;
                    //循环新增订单菜品记录
                    foreach (DataRow Orderitem in Orderdb.Rows)
                    {
                        foreach (DataRow menuitem in menudb.Rows)
                        {
                            if (menuitem["ID"].ToString().Equals(Orderitem["ID"].ToString()))
                            {
                                if (!string.IsNullOrEmpty(menusstring))
                                {
                                    menusstring += "|" + Orderitem["ID"].ToString() + "," + menuitem["Price"].ToString() + "," + Orderitem["Number"].ToString();
                                }
                                else
                                {
                                    menusstring = Orderitem["ID"].ToString() + "," + menuitem["Price"].ToString() + "," + Orderitem["Number"].ToString();
                                }
                                break;
                            }
                        }
                    }
                    newOrder["MenusInfo"] = menusstring;
                    newOrder.Update();
                    if (newOrder.ID > 0)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        //删除
        protected string Delete(string Ordertype, string id, string script, DateTime date)
        {
            try
            {

                if (System.Web.HttpContext.Current.Request.Cookies["Cart"] != null && System.Web.HttpContext.Current.Request.Cookies["Cart"].Values[id + "-" + Ordertype] != null)
                {
                    SPWeb sweb = SPContext.Current.Web;
                    HttpCookie cookie = HttpContext.Current.Request.Cookies["Cart"];
                    DataTable Cartdb = CartHandle.GetCart(Ordertype, sweb.CurrentUser.Name, date);
                    foreach (DataRow item in Cartdb.Rows)
                    {
                        if (id.Equals(item["id"].ToString()))
                        {
                            cookie.Values.Remove(id + "-" + Ordertype);
                            if (cookie.Values.Count == 0)
                            {
                                cookie.Expires = DateTime.Now.AddDays(-1);
                            }
                        }
                    }
                    System.Web.HttpContext.Current.Response.AppendCookie(cookie);

                    script = "alert('删除成功！');";
                }
                return script;
            }
            catch (Exception ex)
            {
                log.writeLogMessage(ex.Message, "确认订单页面的删除购物车数据");
            }
            return script;
        }
        protected void tbBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("OrderIndex.aspx");
        }
        /// <summary>
        /// 午餐项绑定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lv_LunchOrder_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            Label Lunchcount = lv_LunchOrder.FindControl("Lunchcount") as Label;
            Label Lunchmoney = lv_LunchOrder.FindControl("Lunchmoney") as Label;
            Label LunchMealName = lv_LunchOrder.FindControl("LunchMealName") as Label;

            Lunchcount.Text = LCount.ToString();
            Lunchmoney.Text = LMoney.ToString();
            LunchMealName.Text = MealTypeJudge.GetMealTypeShow("2");

        }
        /// <summary>
        /// 早餐项绑定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lv_MorningOrder_ItemDataBound(object sender, ListViewItemEventArgs e)
        {

            Label MorningMealName = lv_MorningOrder.FindControl("MorningMealName") as Label;
            Label Morningcount = lv_MorningOrder.FindControl("Morningcount") as Label;
            Label Morningmoney = lv_MorningOrder.FindControl("Morningmoney") as Label;
            MorningMealName.Text = MealTypeJudge.GetMealTypeShow("1");
            Morningcount.Text = MCount.ToString();
            Morningmoney.Text = MMoney.ToString();
        }
        /// <summary>
        /// 晚餐项绑定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lv_DinnerOrder_ItemDataBound(object sender, ListViewItemEventArgs e)
        {

            Label Dinnercount = lv_DinnerOrder.FindControl("Dinnercount") as Label;
            Label Dinnermoney = lv_DinnerOrder.FindControl("Dinnermoney") as Label;
            Label DinnerMealName = lv_DinnerOrder.FindControl("DinnerMealName") as Label;
            Dinnercount.Text = DCount.ToString();
            Dinnermoney.Text = DMoney.ToString();
            DinnerMealName.Text = MealTypeJudge.GetMealTypeShow("3");
        }

    }
}

