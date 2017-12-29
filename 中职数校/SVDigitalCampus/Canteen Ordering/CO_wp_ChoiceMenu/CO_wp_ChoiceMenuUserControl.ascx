<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CO_wp_ChoiceMenuUserControl.ascx.cs" Inherits="SVDigitalCampus.Canteen_Ordering.CO_wp_ChoiceMenu.CO_wp_ChoiceMenuUserControl" %>
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/common.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/style.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/iconfont.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/animate.css" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/DatePicker/WdatePicker.js"></script>
<script type="text/javascript" src="/_layouts/15/SVDigitalCampus/Script/tz_slider.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/popwin.js"></script>

<div class="Allot_Meal">
    <div class="Whole_display_area">
        <!--tab切换title-->
        <div class="Allot_tab">
            <div class="Allot_tabheader" id="weekdate">
                <ul>
                    <asp:ListView ID="lvMenuTypeTop" runat="server" ItemPlaceholderID="typetopitemplaceholder">
                        <EmptyDataTemplate>
                            亲,暂无菜品分类记录，请新增数据！
                        </EmptyDataTemplate>
                        <LayoutTemplate>

                            <asp:PlaceHolder runat="server" ID="typetopitemplaceholder"></asp:PlaceHolder>

                        </LayoutTemplate>
                        <ItemTemplate>
                            <li class="<%#Eval("classstr") %>"><a href="#<%#Eval("lvID") %>"><%#Eval("Type") %></a></li>
                        </ItemTemplate>
                    </asp:ListView>
                </ul>
            </div>
            <div class="content">
                <asp:ListView ID="lv_MenuType" runat="server" ItemPlaceholderID="typeitemPlaceholder">
                    <EmptyDataTemplate>
                        <div id="tab1" class="tc">
                            亲，暂无菜品记录！
                        </div>
                    </EmptyDataTemplate>
                    <LayoutTemplate>
                        <asp:PlaceHolder runat="server" ID="typeitemPlaceholder"></asp:PlaceHolder>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <div id='#<%#Eval("lvID") %>' class="tc" style='display: <%#Eval("display")%>;'>
                            <div class="Allot_form">
                                <asp:HiddenField ID="Type" runat="server" Value='<%#Eval("TypeID") %>' />
                                <div class="Food_Allot">
                                    <div class="clear"></div>
                                    <asp:ListView ID="lvmenu" runat="server" GroupItemCount="6" ItemPlaceholderID="itemPlaceholder" GroupPlaceholderID="GroupPlaceholder">
                                        <EmptyDataTemplate>
                                            亲，暂无该分类菜品记录！
                                        </EmptyDataTemplate>
                                        <GroupTemplate>
                                            <td id="itemPlaceholder" runat="server" style="padding: 0; border-spacing: 0; width: 300px;"></td>
                                        </GroupTemplate>
                                        <LayoutTemplate>

                                            <table class="A_form">
                                                <tr>
                                                    <asp:PlaceHolder ID="GroupPlaceholder" runat="server"></asp:PlaceHolder>
                                                </tr>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <td>
                                               <%-- <input id='<%#"ckMenu"+Eval("ID") %>' name="ckMenu" type="checkbox" value='<%#Eval("ID") %>' <%#Eval("Isshowchecked") %> /><%#Eval("Title")%> --%>
                                                <asp:CheckBox ID="ckMenu" runat="server" /><%#Eval("Title")%><asp:HiddenField ID="menuid" runat="server" Value='<%#Eval("ID") %>' />
                                            </td>

                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </div>
            <div class="btndiv">
                <%--<input type="button" class="btn" id="save" value="保存" onclick="savemenu()" />--%>
                <asp:Button ID="btnsave" CssClass="btn" runat="server" Text="保存" OnClick="btnKeep_Click" />
                <asp:Button ID="btnClear" CssClass="btn" runat="server" Text="清空"  OnClientClick="clearall();" />
                <input id="mealtype" type="hidden" value='<%=mealType %>' /><input id="mealdate" type="hidden" value='<%=DateOfWeek %>' />
            </div>
        </div>
    </div>
</div>
<script type="text/javascript">
    function clearall() {
        $("*[id$='ckMenu']").each(function () {
            this.checked = false;

        });
    }
    function savemenu() {
        var ckmenu = document.getElementsByName("ckMenu");
        var weekmealvalue = "";
        $("*[name='ckMenu']").each(function(){
            if($(this).is(":checked")){
                if (weekmealvalue != "") {
                    weekmealvalue=weekmealvalue+","+$(this).val();
                } else {   weekmealvalue = $(this).val();
                }
            }
        });
        var mealtype = $("#mealtype").val();
        var mealdate = $("#mealdate").val();
        if (weekmealvalue!=""&&mealtype != null && mealtype != "" && mealdate != null && mealdate != "") {
            $.ajax({
                url: '<%=layouturl%>' + "CommDataHandler.aspx?action=SaveWeekMeal&" + Math.random(),   // 提交的页面
                type: "POST",                   // 设置请求类型为"POST"，默认为"GET"
                async: false,
                data: { mealtype: mealtype, mealdate: mealdate, weekmeal: weekmealvalue },
                beforeSend: function ()          // 设置表单提交前方法
                {
                    //alert("准备提交数据");


                },
                error: function (request) {      // 设置表单提交出错
                    //alert("表单提交出错，请稍候再试");
                    //rebool = false;
                },
                success: function (data) {
                    var ss = data.split("|");
                    if (ss[0] == "1") {
                        alert("保存成功！");
                        parent.location.href = parent.location.href + "?Isnote=true";
                       // parent.location.href = parent.location.href + "&Isnote=true";
                    } else {
                        alert(ss[1]);
                    }

                }
            });
        }

    }
    //$(function () {
    //    var i = 1;
    //    $("div[name='tab']").each(function () {
    //        $(this).attr("id", "tab" + i);
    //        i++;
    //    });
    //});
   
    function close() {

        //$("#mask,#maskTop", parent.document).fadeOut(function () {
        //    $(this).remove();
        //document.parentWindow.location.reload(true);
        document.parentWindow.location.href = document.parentWindow.location.href + "&Isnote=true";
        //document.parentWindow.location.reload();
        //});
        //parent.location.reload();

        //$("#iframbox").attr("src", $("#iframbox").attr("src") + "?Isnote=true");
    }
   
</script>
