<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CO_wp_MenuManagerUserControl.ascx.cs" Inherits="SVDigitalCampus.Canteen_Ordering.CO_wp_MenuManager.CO_wp_MenuManagerUserControl" %>
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/common.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/style.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/iconfont.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/animate.css" />
<script src="/_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script type="text/javascript" src="/_layouts/15/SVDigitalCampus/Script/tz_slider.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/popwin.js"></script>
<script type="text/javascript">
    function UpdateObj(MenuID) {
        if (MenuID == null) {

            alert("请选择一条记录！");
            return false;

        }
        //$.dialog({ id: 'Menu', title: '修改菜品', content: 'url:UpdateMenu.aspx?MenuId=' + MenuID, width: 700, height: 500, lock: true, max: false, min: false });
        popWin.showWin("600", "480", "编辑菜品", '<%=SietUrl%>' + "UpdateMenu.aspx?MenuID=" + MenuID);
    }

    function Addmenu() {
        popWin.showWin("600", "480", "添加菜品", '<%=SietUrl%>' + "AddMenu.aspx");
    }
   
    function MenuType() {
        popWin.scrolling = "auto";
        popWin.showWin("600", "500", "菜品分类管理", '<%=SietUrl%>' + "MenuTypeManager.aspx");
    }
    function Weekmeal() {
        window.location.href = "WeekMealManager.aspx";
    }
</script>
<div class="MenuDiv">
    <!--页面名称-->
    <h1 class="Page_name">菜品信息管理</h1>
    <!--整个展示区域-->
    <div class="Whole_display_area">
        <!--操作区域-->
        <div class="Operation_area">
            <div class="left_choice fl">
                <ul>
                    <li class="Select">
                        <asp:DropDownList ID="ddlType" runat="server" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" AutoPostBack="true" >
                            <asp:ListItem Text="所有" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </li>
                    <li class="Batch_operation">
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="option" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" >
                            <asp:ListItem Text="所有" Value="0"></asp:ListItem>
                            <asp:ListItem Text="启用" Value="1"></asp:ListItem>
                            <asp:ListItem Text="禁用" Value="2"></asp:ListItem>
                        </asp:DropDownList>

                    </li>
                    <li class="Sear">
                        <input type="text" placeholder=" 请输入菜品名称" id="txtMenu" class="search" name="search" runat="server" /><asp:LinkButton ID="btnSearch" runat="server" OnClick="btnSearch_Click"><i class="iconfont">&#xe62d;</i></asp:LinkButton>
                    </li>
                </ul>
            </div>
            <div class="right_add fr">
                <a href="javascript:void(0);" onclick="Addmenu();" class="add"><i class="iconfont">&#xe630;</i>添加菜品</a>
                <a href="javascript:void(0);" onclick="MenuType();" class="add"><i class="iconfont">&#xe642;</i>菜品分类管理</a>
                <a href="javascript:void(0);" onclick="Weekmeal();" class="add"><i class="iconfont">&#xe650;</i>分配菜单</a>
                <%--<a href="javascript:void(0);" onclick="SysSet();" class="add">食堂信息设置</a>--%>
            </div>
        </div>
        <div class="clear"></div>
        <!--展示区域-->
        <div class="Display_form">
            <asp:ListView ID="lvMenu" runat="server" OnPagePropertiesChanging="lvMenu_PagePropertiesChanging" OnItemCommand="lvMenu_ItemCommand">
                <EmptyDataTemplate>
                    <table>
                        <tr>
                            <td colspan="9" style="text-align: center">亲，暂无菜品记录！
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <LayoutTemplate>
                    <table class="D_form">
                        <tr class="trth">
                            <!--表头tr名称-->
                            <th class="Account">编号
                            </th>
                            <th class="name">菜品
                            </th>
                            <th class="Head">类型
                            </th>
                            <th class="Contact">辣度
                            </th>
                            <th class="Contact">价格
                            </th>
                            <th class="Contact">图片
                            </th>
                            <th class="State">状态
                            </th>
                            <th class="Operation">操作列
                            </th>
                        </tr>
                        <tr id="itemPlaceholder" runat="server"></tr>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr class="Single">
                        <td class="Account">
                            <%#Eval("Count")%>
                        </td>
                        <td class="name">
                            <%#Eval("Title")%>
                        </td>
                        <td class="Account">
                            <%#Eval("Type")%>
                        </td>
                        <td class="Account">
                            <em class='<%#Eval("Hot")%>'>&nbsp&nbsp&nbsp&nbsp&nbsp;</em>
                        </td>
                        <td class="Account">￥<%#Eval("Price")%></td>
                        <td class="Account">
                          <span class="Img_Span">  <asp:Image ID="Imgshow" runat="server" CssClass="menuimg" ImageUrl='<%#Eval("Picture")%>' /></span>
                        </td>
                        <td class="State">
                            <span class="qijin"><a href="javascript:void(0);" onclick="Enused(<%#"this,'" +Eval("ID")+"'" %>);" class='<%#Eval("qStatus")%>'>启用</a><a href="javascript:void(0);" onclick="Disused(<%#"this,'" +Eval("ID")+"'" %>);" class='<%#Eval("jStatus")%>'>禁用</a></span>
                        </td>
                        <td class="Operation">
                            <a href="javascript:void(0);" onclick="UpdateObj('<%# Eval("ID") %>');" style="color: blue;"><i class="iconfont">&#xe629;</i></a>
                            <asp:LinkButton ID="btndelete" BorderStyle="None" CommandName="del" CommandArgument='<%# Eval("ID") %>' runat="server" OnClientClick="return confirm('你确定删除吗？')" CssClass="btn"><i class="iconfont">&#xe64c;</i></asp:LinkButton>

                        </td>
                    </tr>

                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="Double">

                        <td class="Account">
                            <%#Eval("Count")%>
                        </td>
                        <td class="name">
                            <%#Eval("Title")%>
                        </td>
                        <td class="Account">
                            <%#Eval("Type")%>
                        </td>
                        <td class="Account">
                            <em class='<%#Eval("Hot")%>'>&nbsp&nbsp&nbsp&nbsp&nbsp;</em>
                        </td>
                        <td class="Account">￥<%#Eval("Price")%></td>
                        <td class="Account">
                          <span class="Img_Span">  <asp:Image ID="Imgshow" runat="server" CssClass="menuimg" ImageUrl='<%#Eval("Picture")%>' /></span>
                        </td>
                        <td class="State">
                            <span class="qijin"><a href="javascript:void(0);" onclick="Enused(<%#"this,'" +Eval("ID")+"'" %>);" class='<%#Eval("qStatus")%>'>启用</a><a href="javascript:void(0);" onclick="Disused(<%#"this,'" +Eval("ID")+"'" %>);" class='<%#Eval("jStatus")%>'>禁用</a></span>
                        </td>
                        <td class="Operation">
                            <a href="javascript:void(0);" onclick="UpdateObj('<%# Eval("ID") %>');" style="color: blue;"><i class="iconfont">&#xe629;</i></a>
                            <asp:LinkButton ID="btndelete" BorderStyle="None" CommandName="del" CommandArgument='<%# Eval("ID") %>' runat="server" OnClientClick="return confirm('你确定删除吗？')" CssClass="btn"><i class="iconfont">&#xe64c;</i></asp:LinkButton>

                        </td>
                    </tr>
                </AlternatingItemTemplate>
            </asp:ListView>
            <div class="page">
                <asp:DataPager ID="DPMenu" runat="server" PageSize="8" PagedControlID="lvMenu">
                    <Fields>
                        <asp:NextPreviousPagerField
                            ButtonType="Link" ShowNextPageButton="False" ShowPreviousPageButton="true"
                            ShowFirstPageButton="true" FirstPageText="首页" PreviousPageText="上一页" ButtonCssClass="pageup" />

                        <asp:NumericPagerField CurrentPageLabelCssClass="number now" NumericButtonCssClass="number" />

                        <asp:NextPreviousPagerField
                            ButtonType="Link" ShowPreviousPageButton="False" ShowNextPageButton="true"
                            ShowLastPageButton="true" LastPageText="末页" NextPageText="下一页" ButtonCssClass="pagedown" />

                        <asp:TemplatePagerField>
                            <PagerTemplate>
                                <span class="count">| <%# Container.StartRowIndex / Container.PageSize + 1%> / 
                            <%# (Container.TotalRowCount % Container.MaximumRows) > 0 ? Convert.ToInt16(Container.TotalRowCount / Container.MaximumRows) + 1 : Container.TotalRowCount / Container.MaximumRows%>  页
                            (共<%# Container.TotalRowCount %>项)
                                </span>
                            </PagerTemplate>
                        </asp:TemplatePagerField>
                    </Fields>
                </asp:DataPager>

            </div>
        </div>
    </div>
</div>
<script type="text/javascript">
    function Enused(obj, Menuid) {
        if (Menuid != null && Menuid != "") {
            jQuery.ajax({
                url: "MenuManager.aspx?action=ChangeStatus&" + Math.random(),   // 提交的页面
                type: "POST",                   // 设置请求类型为"POST"，默认为"GET"
                data: { "MenuID": Menuid, "Status": "1" },
                beforeSend: function ()          // 设置表单提交前方法
                {
                    //alert("准备提交数据");


                },
                error: function (request) {      // 设置表单提交出错
                    //alert("表单提交出错，请稍候再试");
                    //rebool = false;
                },
                success: function (result) {
                    var ss = result.split("|");
                    if (ss[0] == "1") {
                        $(obj).next().attr("class", "Disable");
                        $(obj).attr("class", "Enable");
                    }
                }

            });
        }
    }
    function Disused(obj, Menuid) {
        if (Menuid != null && Menuid != "") {
            jQuery.ajax({
                url: "MenuManager.aspx?action=ChangeStatus&" + Math.random(),   // 提交的页面
                type: "POST",                   // 设置请求类型为"POST"，默认为"GET"
                data: { "MenuID": Menuid, "Status": "2" },
                beforeSend: function ()          // 设置表单提交前方法
                {
                    //alert("准备提交数据");


                },
                error: function (request) {      // 设置表单提交出错
                    //alert("表单提交出错，请稍候再试");
                    //rebool = false;
                },
                success: function (result) {
                    var ss = result.split("|");
                    if (ss[0] == "1") {
                        $(obj).prev().attr("class", "Disable");
                        $(obj).attr("class", "Enable");
                    }
                }

            });
        }
    }
</script>
