<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ME_wp_MoralEduInfoUserControl.ascx.cs" Inherits="Sinp_StudentWP.WebParts.ME.ME_wp_MoralEduInfo.ME_wp_MoralEduInfoUserControl" %>
<script src="/_layouts/15/Script/jquery-1.8.0.js"></script>
<link href="/_layouts/15/Style/common.css" rel="stylesheet" />
<link rel="stylesheet" href="/_layouts/15/Stu_css/allst.css">
<link href="/_layouts/15/Style/layout.css" rel="stylesheet" />
<link href="/_layouts/15/Style/tablelist.css" rel="stylesheet" />
<style type="text/css">
    .table { text-align:center;width:100%;line-height:36px }
    input {border-style:none;text-align:center;height:32px}
</style>
<script type="text/javascript">
    $(function () {
        $(":text").change(function () {
            var id = $(this).attr("id");
            var value = $(this).val();
            $("#itemIds").val(id + "#" + value);
            $("#btnOK").click();
        });
    })
</script>
<div class="writingform">
    <div class="writing_title">
        <h2>
            <span class="title_left fl">德育分数管理</span>
        </h2>
    </div>
    <div class="writing_form">
        <div style="margin: 10px; height: 30px; line-height: 30px">
            <span style="float: left">
                <asp:DropDownList CssClass="option" ID="DDL_Type" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDL_Type_SelectedIndexChanged" />
                <asp:DropDownList CssClass="option" ID="DDL_Temp" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDL_Temp_SelectedIndexChanged" />
            </span>
            <span style="float: right">
                <a class="add_GL" onclick="setItem();"><i class="iconfont"></i>确定</a>
            </span>
        </div>
        <div class="content_list">
            <asp:DataGrid runat="server" ID="DG_MoralEdu" CssClass="table">
                <HeaderStyle BackColor="#f1f1f1" Height="36px" />
                <ItemStyle Height="36px" BorderStyle="Solid" BorderWidth="1px" BorderColor="#e1e1e1" />
            </asp:DataGrid>
            <div style="display: none">
                <asp:HiddenField runat="server" ClientIDMode="Static" ID="itemIds" />
                <asp:Button runat="server" ClientIDMode="Static" ID="btnOK" OnClick="btnOK_Click" />
            </div>
        </div>
    </div>
</div>