<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CO_wp_SysBaseSetUserControl.ascx.cs" Inherits="SVDigitalCampus.Canteen_Ordering.CO_wp_SysBaseSet.CO_wp_SysBaseSetUserControl" %>
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/common.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/style.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/iconfont.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/animate.css" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/DatePicker/WdatePicker.js"></script>
<script type="text/javascript" src="/_layouts/15/SVDigitalCampus/Script/tz_slider.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery.form.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery.jqtransform.js"></script>
<script type="text/javascript">
    function close() {
        //$("#mask,#maskTop", parent.document).fadeOut(function () {
        //    $(this).remove();
        //document.parentWindow.location.reload(true);
        parent.location.reload(true);
        //});

    }
</script>
                    <form id="canteenform" enctype="multipart/form-data">
<div class="ss_dialog">
    <div class="t_content">
        <!---选项卡-->
        <div class="ss_tab">
            <div class="yy_tabheader" id="titlediv">
                <ul>
                    <li class="selected"><a href="#"><span class="zong_tit"><span class="num_1">1</span><span class="add_li">食堂信息</span></span></a></li>
                    <li><a href="#"><span class="zong_tit"><span class="num_2">2</span><span class="add_li">基本项数据</span></span></a></li>
                    <li><a href="#"><span class="zong_tit"><span class="iconfont num_3">&#xe654;</span><span class="add_li">完成</span></span></a></li>
                </ul>
            </div>
            <div class="content" id="contentdiv">
                <div class="tc" style="display: block;">
                        <div class="t_message">
                            <div class="message_con">

                                <table class="m_top">
                                    <tr>
                                        <td class="mi"><span class="m">*食堂名称：</span></td>
                                        <td class="ku">
                                            <asp:TextBox runat="server" ID="txtName" CssClass="hu"></asp:TextBox>
                                            <asp:HiddenField ID="CanteenID" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="mi"><span class="m">*营业时间：</span></td>
                                        <td class="ku">
                                            <asp:DropDownList ID="ddlbegintime" runat="server"></asp:DropDownList>
                                            -
                                        <asp:DropDownList ID="ddlendtime" runat="server"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td class="mi"><span class="m">食堂图片：</span></td>
                                        <td class="ku">
                                            <input id="Img" name="Img" runat="server" type="file" onblur="shownowimg(this);" /><asp:Image ID="Imgshow" runat="server" Width="100" Height="100" /><asp:HiddenField ID="PictureID" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="mi"><span class="m">地址：</span></td>
                                        <td class="ku">
                                            <asp:TextBox runat="server" ID="txtAddress" CssClass="hu"></asp:TextBox></td>
                                    </tr>
                                </table>
                                <table class="c_line">
                                    <tr></tr>
                                </table>
                                <table class="m_top">
                                    <tr>
                                        <td class="mi"><span class="m">公告：</span></td>
                                        <td class="ku">
                                            <asp:TextBox ID="txtNotice" runat="server" TextMode="MultiLine" Columns="30" Rows="3"></asp:TextBox></td>
                                    </tr>
                                </table>

                                <%--</form>--%>
                            </div>

                        </div>
                        <div class="t_btn">
                            <%--<input type="button" value="保存" onclick="savecanteen();" />--%>
                            <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" />
                            <input type="button" value="下一步" onclick="nextstep();" />
                        </div>
                </div>
            <div class="tc" style="display: none;">
                <div class="t_message">
                    <div class="message_con">
                        <%--<form>--%>
                        <table class="tbEdit">
                            <tr>
                                <td class="mi"><span class="m"><%=SVDigitalCampus.Common.MealTypeJudge.GetMealTypeShow("1")%>下单截止时间：</span></td>
                                <td class="ku">
                                    <asp:DropDownList ID="ddlmorning" runat="server"></asp:DropDownList>
                                </td>
                                <td class="ck">
                                    <asp:ListView ID="lvMorningCk" runat="server" GroupItemCount="4" ItemPlaceholderID="lunchitemPlaceholder">
                                        <EmptyDataTemplate>
                                            亲，暂无菜品分类记录！
                                                 

                                        </EmptyDataTemplate>
                                        <GroupTemplate>
                                            <asp:PlaceHolder ID="lunchitemPlaceholder" runat="server"></asp:PlaceHolder>
                                            </td></tr><tr>
                                                <td class="mi"></td>
                                                <td class="ku"></td>
                                                <td class="ck">
                                        </GroupTemplate>
                                        <LayoutTemplate>
                                            <asp:PlaceHolder ID="GroupPlaceholder" runat="server"></asp:PlaceHolder>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ckType" runat="server" /><%#Eval("Title")%><asp:HiddenField ID="typeid" runat="server" Value='<%#Eval("ID") %>' />
                                        </ItemTemplate>
                                    </asp:ListView>
                                </td>
                            </tr>
                            <tr>
                                <td class="mi"><span class="m"><%=SVDigitalCampus.Common.MealTypeJudge.GetMealTypeShow("2")%>下单截止时间：</span></td>
                                <td class="ku">
                                    <asp:DropDownList ID="ddllunch" runat="server"></asp:DropDownList></td>
                                <td class="ck">
                                    <asp:ListView ID="lvLunchCk" runat="server" GroupItemCount="4" ItemPlaceholderID="lunchitemPlaceholder">
                                        <EmptyDataTemplate>
                                            亲，暂无菜品分类记录！
                                                 

                                        </EmptyDataTemplate>
                                        <GroupTemplate>
                                            <asp:PlaceHolder ID="lunchitemPlaceholder" runat="server"></asp:PlaceHolder>
                                            </td></tr><tr>
                                                <td class="mi"></td>
                                                <td class="ku"></td>
                                                <td class="ck">
                                        </GroupTemplate>
                                        <LayoutTemplate>
                                            <asp:PlaceHolder ID="GroupPlaceholder" runat="server"></asp:PlaceHolder>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ckType" runat="server" /><%#Eval("Title")%><asp:HiddenField ID="typeid" runat="server" Value='<%#Eval("ID") %>' />
                                        </ItemTemplate>
                                    </asp:ListView>
                                </td>
                            </tr>
                            <tr>
                                <td class="mi"><span class="m"><%=SVDigitalCampus.Common.MealTypeJudge.GetMealTypeShow("3")%>下单截止时间：</span></td>
                                <td class="ku">
                                    <asp:DropDownList ID="ddldinner" runat="server"></asp:DropDownList></td>
                                <td class="ck">
                                    <asp:ListView ID="lvDinnerCk" runat="server" GroupItemCount="4" ItemPlaceholderID="dinneritemPlaceholder">
                                        <EmptyDataTemplate>
                                            亲，暂无菜品分类记录！

                                        </EmptyDataTemplate>
                                        <GroupTemplate>
                                            <asp:PlaceHolder ID="dinneritemPlaceholder" runat="server"></asp:PlaceHolder>
                                            </td></tr><tr>
                                                <td class="mi"></td>
                                                <td class="ku"></td>
                                                <td class="ck">
                                        </GroupTemplate>
                                        <LayoutTemplate>
                                            <asp:PlaceHolder ID="GroupPlaceholder" runat="server"></asp:PlaceHolder>

                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ckType" runat="server" /><%#Eval("Title")%><asp:HiddenField ID="typeid" runat="server" Value='<%#Eval("ID") %>' />
                                        </ItemTemplate>
                                    </asp:ListView>
                                </td>
                            </tr>

                        </table>
                        <table class="c_line">
                            <tr></tr>
                        </table>
                        <table class="m_top">
                            <%--<tr>
                                    <td class="mi">早：</td>
                                    <td class="ku">
                                        <asp:TextBox ID="txtMorning" runat="server" MaxLength="2" CssClass="hu"></asp:TextBox>
                                    </td>
                                    </tr>--%><%--<tr>
                                        <td></td>
                                        <td class="ku">
                                            <asp:ListView ID="" runat="server" GroupItemCount="4" ItemPlaceholderID="morningitemPlaceholder">
                                                <EmptyDataTemplate>
                                                    亲，暂无菜品分类记录！
                                                </EmptyDataTemplate>
                                                <GroupTemplate>
                                                    <asp:PlaceHolder ID="morningitemPlaceholder" runat="server"></asp:PlaceHolder>
                                                </GroupTemplate>
                                                <LayoutTemplate>
                                                    <asp:PlaceHolder ID="GroupPlaceholder" runat="server"></asp:PlaceHolder>

                                                </LayoutTemplate>
                                                <ItemTemplate>

                                                    <asp:CheckBox ID="ckType" runat="server" /><%#Eval("Title")%><asp:HiddenField ID="typeid" runat="server" Value='<%#Eval("ID") %>' />

                                                </ItemTemplate>
                                            </asp:ListView>
                                        </td>
                                </tr>--%>
                            <%--  <tr>
                                    <td class="mi"><span class="m">中：</span></td>
                                    <td class="ku">
                                        <asp:TextBox ID="txtLunch" runat="server" CssClass="hu" MaxLength="2"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                        <td></td>
                                        <td class="ku">
                                        <asp:ListView ID="lvLunchCk" runat="server" GroupItemCount="4" ItemPlaceholderID="lunchitemPlaceholder">
                                            <EmptyDataTemplate>
                                                亲，暂无菜品分类记录！
                                                 

                                            </EmptyDataTemplate>
                                            <GroupTemplate>
                                                <asp:PlaceHolder ID="lunchitemPlaceholder" runat="server"></asp:PlaceHolder>
                                            </GroupTemplate>
                                            <LayoutTemplate>
                                                <asp:PlaceHolder ID="GroupPlaceholder" runat="server"></asp:PlaceHolder>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ckType" runat="server" /><%#Eval("Title")%><asp:HiddenField ID="typeid" runat="server" Value='<%#Eval("ID") %>' />
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </td>
                                </tr>--%>
                            <%--  <tr>
                                    <td class="mi"><span class="m">晚：</span></td>
                                    <td class="ku">
                                        <%--<asp:TextBox ID="txtDinner" runat="server" CssClass="hu" MaxLength="2"></asp:TextBox></td>--%>

                            <%-- </tr><tr>
                                        <td></td>
                                        <td class="ku">
                                            <asp:ListView ID="lvDinnerCk" runat="server" GroupItemCount="4" ItemPlaceholderID="dinneritemPlaceholder">
                                                <EmptyDataTemplate>
                                                    亲，暂无菜品分类记录！

                                                </EmptyDataTemplate>
                                                <GroupTemplate>
                                                    <asp:PlaceHolder ID="dinneritemPlaceholder" runat="server"></asp:PlaceHolder>
                                                </GroupTemplate>
                                                <LayoutTemplate>
                                                    <table>
                                                        <tr>
                                                            <asp:PlaceHolder ID="GroupPlaceholder" runat="server"></asp:PlaceHolder>
                                                        </tr>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ckType" runat="server" /><%#Eval("Title")%><asp:HiddenField ID="typeid" runat="server" Value='<%#Eval("ID") %>' />
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </td>
                                </tr>--%>
                        </table>
                        <%--</form>--%>
                    </div>

                </div>
                <div class="t_btn">
                    <input type="button" value="上一步" onclick="lastep();" />

                    <asp:Button ID="btnAdd" runat="server" Text="保存" OnClick="btnEdit_Click" />
                </div>
            </div>

            <div class="tc" style="display: none;" id="success">
                <p class="finish">
                    <span class="fl icon_f"><i class="iconfont finish_t">&#xe654;</i></span>
                    <span class="fr info_f">您已成功维护信息</span>
                </p>
            </div>
        </div>
    </div>

</div>
</div>
                    </form>
<script type="text/javascript">
    function shownowimg(obj) {
        var filesrc = $(obj).attr('src');
        $("[id$=Imgshow]").attr('src',filesrc);
    }
    function lastep() {
        $("#titlediv li:eq(0)").addClass("selected").siblings().removeClass("selected");

        $("#contentdiv .tc:eq(0)").show().siblings().hide();
    }
    function savecanteen() {

        var CanteenID = $("[id$=CanteenID]").val();
        var txtName = $("[id$=txtName]").val();
        var ddlbegintime = $("[id$=ddlbegintime]").val();
        var ddlendtime = $("[id$=ddlendtime]").val();
        if (txtName == null || $.trim(txtName) == "" || CanteenID == null || $.trim(CanteenID) == "") {
            return false;
        }
        if (ddlbegintime == null || $.trim(ddlbegintime) == "") {
            return false;
        }
        if (ddlendtime == null || $.trim(ddlendtime) == "") {
            return false;
        }
        //$('#canteenform').attr("enctype", "multipart/form-data");
        $("#canteenform").ajaxSubmit({
            url: '<%=layouturl%>' + "CommDataHandler.aspx?action=SaveCanteen&" + Math.random(),   // 提交的页面
            type: "POST",                   // 设置请求类型为"POST"，默认为"GET"
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
                if (ss[0] == "1") {//修改成功
                    alert("保存成功！");
                }
            }

        });
    }
    function nextstep() {

        $("#titlediv li:eq(1)").addClass("selected").siblings().removeClass("selected");

        $("#contentdiv .tc:eq(1)").show().siblings().hide();

    }
    function Success() {
        $("#titlediv li:last").addClass("selected").siblings().removeClass("selected");
        $("#success").show().siblings().hide();
    }
    function ckselect() {
        $("input[checkbox]").each(function () {
            if ($(this).checked) {
                $(this).addClass("select");
            };
        });
    }
</script>
