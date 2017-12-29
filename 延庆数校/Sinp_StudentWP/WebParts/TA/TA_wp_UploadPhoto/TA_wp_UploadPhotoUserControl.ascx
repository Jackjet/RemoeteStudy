<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TA_wp_UploadPhotoUserControl.ascx.cs" Inherits="Sinp_StudentWP.WebParts.TA.TA_wp_UploadPhoto.TA_wp_UploadPhotoUserControl" %>

<script type="text/javascript" src="/_layouts/15/Script/jquery-1.8.0.js"></script>
<link rel="stylesheet" type="text/css" href="/_layouts/15/Stu_upload/upstyle.css" />
<link rel="stylesheet" type="text/css" href="/_layouts/15/Stu_upload/webuploader.css" />
<script type="text/javascript" src="/_layouts/15/Stu_upload/webuploader.min.js"></script>
<script type="text/javascript" src="/_layouts/15/Stu_upload/upload.js"></script>
<script type="text/javascript">
function getValue() {
        var member = $("select[id$='DDL_SecLeader']").val();
        $("input[id$='Hid_SecLeader']").val(member);
        return true;
}
</script>
<asp:ScriptManager ID="ScriptManager1" runat="server" > 
</asp:ScriptManager> 
<div id="wrapper">
    <div id="container">
        <!--头部，相册选择和格式选择-->
        <div id="setting" style="width:100%; border-bottom:1px solid #dadada; text-align:center">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Always" RenderMode="Block">
                <ContentTemplate>
                    <asp:DropDownList runat="server" ID="DDP_Album" AutoPostBack="true" OnSelectedIndexChanged="DDP_Album_SelectedIndexChanged" />
                    <asp:HiddenField runat="server" ID="hid_Album" ClientIDMode="Static" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="uploader">
            <div class="queueList">
                <div id="dndArea" class="placeholder">
                    <div id="filePicker"></div>
                </div>
            </div>
            <div class="statusBar" style="display: none;">
                <div class="progress">
                    <span class="text">0%</span>
                    <span class="percentage"></span>
                </div>
                <div class="info"></div>
                <div class="btns">
                    <div id="filePicker2"></div>
                    <div class="uploadBtn">开始上传</div>
                </div>
            </div>
        </div>
    </div>
</div>
