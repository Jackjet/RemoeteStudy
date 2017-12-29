<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CO_wp_MenuEditUserControl.ascx.cs" Inherits="SVDigitalCampus.Canteen_Ordering.CO_wp_MenuEdit.CO_wp_MenuEditUserControl" %>
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/common.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/iconfont.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/animate.css" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script type="text/javascript">
    function close() {
        $("#mask,#maskTop", parent.document).fadeOut(function () {
            $(this).remove();
            //document.parentWindow.location.reload(true);
            parent.location.reload(true);
        });

    }
    var record = {
        num: ""
    }
    var checkDecimal = function (n) {
        var decimalReg = /^\d{0,8}\.{0,1}(\d{1,2})?$/;//var decimalReg=/^[-\+]?\d{0,8}\.{0,1}(\d{1,2})?$/; 
        if (n.value != "" && decimalReg.test(n.value)) {
            record.num = n.value;
        } else {
            if (n.value != "") {
                n.value = record.num;
            }
        }
    }
</script>
<div>
    <form id="form1" enctype="multipart/form-data">
        <div class="MenuDiv">
            <table class="tbEdit">

                <tr>
                    <td class="mi">
                        <span class="m">菜品名称：</span>
                    </td>
                    <td class="ku">
                        <asp:HiddenField ID="MenuID" runat="server" />
                        <asp:TextBox runat="server" ID="txtMenu" CssClass="disInBlock"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td class="mi">菜品分类：
                    </td>
                    <td class="ku">

                        <asp:DropDownList ID="ddlType" runat="server">
                            <asp:ListItem Text="所有" Value="0"></asp:ListItem>
                            <asp:ListItem Text="素菜" Value="1"></asp:ListItem>
                            <asp:ListItem Text="荤菜" Value="2"></asp:ListItem>
                            <asp:ListItem Text="凉菜" Value="3"></asp:ListItem>
                            <asp:ListItem Text="粥汤" Value="4"></asp:ListItem>
                        </asp:DropDownList></td>

                </tr>
                <tr>
                    <td class="mi">菜品价格：
                    </td>
                    <td class="ku">
                        <input type="text" id="txtPrice" onkeyup="checkDecimal(this);" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="mi">状态：</td>
                    <td class="ku">
                        <%--<asp:RadioButton ID="rdoStatusOn" GroupName="Status" runat="server" Text="启用" Checked="true"/><asp:RadioButton ID="rdoStatusDown" GroupName="Status" runat="server" Text="禁用" />--%>
                           <input id="rdoStatusOn" type="radio" name="Status" runat="server" checked="true" value="1" />启用<input id="rdoStatusDown" type="radio" name="Status" runat="server" value="2" />禁用
                    </td>
                </tr>
                <tr>
                    <td class="mi">菜品图片：
                    </td>
                    <td class="ku">
                        <input id="Img"  name="Img" type="file" runat="server" /><asp:Image ID="Imgshow" runat="server" Width="100" Height="100" /><asp:HiddenField ID="PictureID" runat="server" />

                    </td>
                </tr>
                <tr>
                    <td class="mi">辛辣度：
                    </td>
                    <td class="ku">
                        <asp:DropDownList ID="ddlHot" runat="server">
                            <asp:ListItem Text="无" Value="0"></asp:ListItem>
                            <asp:ListItem Text="微辣" Value="1"></asp:ListItem>
                            <asp:ListItem Text="辛辣" Value="2"></asp:ListItem>
                        </asp:DropDownList></td>
                </tr>

            </table>
            <div class="t_btn">
                <asp:Button ID="btnUpdate" CssClass="btn" runat="server" Text="修改" UseSubmitBehavior="false" OnClientClick="this.value='修改';this.disabled=true;" OnClick="btnUpdate_Click" />
               <%-- <input id="btnUpdate" type="button" class="btn" onclick="savemenu()" value="修改" />--%>
            </div>
        </div>
    </form>
</div>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery.form.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery.jqtransform.js"></script>
<script type="text/javascript">
    function savemenu() {
        $("#form1").ajaxSubmit({
            url: '<%=layouturl%>' + "CommDataHandler.aspx?action=EditMenu&" + Math.random(),   // 提交的页面
            type: "POST",                   // 设置请求类型为"POST"，默认为"GET"
            async: false,
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
                    alert("修改成功！");
                    close();
                } else {
                    alert("修改失败！");
                }

            }

        });
    }
</script>
