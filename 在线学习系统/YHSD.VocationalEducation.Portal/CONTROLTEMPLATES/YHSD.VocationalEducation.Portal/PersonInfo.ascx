<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PersonInfo.ascx.cs" Inherits="YHSD.VocationalEducation.Portal.CONTROLTEMPLATES.YHSD.VocationalEducation.Portal.PersonInfo" %>
<style>
    .kaiKeDate {
        font-size: 12px;
        font-family: "微软雅黑","宋体";
        color: #999;
    }
</style>
<script>

    function EditUser(id) {
        OL_ShowLayerNo(2, "编辑个人信息", 770, 400, "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/UpdateUser.aspx?id=" + id, function (returnVal) {
        });

    }
    function UpdatePwd() {
        OL_ShowLayerNo(2, "修改密码", 770, 600, "http://61.50.119.70:101/authorize_ChangePassWord.aspx?flag=del&_1448723986133", function (returnVal) {
        });
    }
    function PwdReset() {
        alert("请联系管理员重置密码！");
    }

</script>
<div class="student_main">
    <div class="f_l">

        <div class="all_classes last_classes">
        </div>
        <div class="class_library">
            <div class="library_title">
                <div class="title">个人信息</div>
            </div>
            <label style="text-align: center;" id="GrxxLabel" runat="server" />
            

        </div>
    </div>
    <div class="right_part f_r">
    </div>
    <div class="clear"></div>
</div>

