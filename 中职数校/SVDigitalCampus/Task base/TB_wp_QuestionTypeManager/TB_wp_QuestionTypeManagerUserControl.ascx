<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TB_wp_QuestionTypeManagerUserControl.ascx.cs" Inherits="SVDigitalCampus.Task_base.TB_wp_QuestionTypeManager.TB_wp_QuestionTypeManagerUserControl" %>
 <link href="../../../../_layouts/15/SVDigitalCampus/CSS/common.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/iconfont.css" rel="stylesheet" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/animate.css" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" rel="stylesheet" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/popwin.js"></script>
<script type="text/javascript">
    function close() {
        parent.location.reload(true);
    }
</script>
<div class="MenuType_information_SchoolLibrary">
    <div class="MenuType_form">
        <asp:ListView ID="lvEQType" runat="server" >
            <EmptyDataTemplate>

                <table class="D_form">
                    <tr class="trth">
                        <td colspan="3" style="text-align: center">您好，暂无试题类型记录！
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <AlternatingItemTemplate>
                <tr class="Double">

                    <td class="Account">
                        <%#Eval("Count")%>
                    </td>
                    <td class="Account">
                        <%#Eval("Title")%>
                    </td>
                    <td class="Account">
                        <%#Eval("QTypeShow")%>
                    </td>
                </tr>
            </AlternatingItemTemplate>
           
            <LayoutTemplate>

                <table class="D_form" id="itemPlaceholderContainer">
                    <tr class="trth">
                        <th class="Account">编号
                        </th>
                        <th class="Account">名称
                        </th>
                        <th class="Account">类型
                        </th>
                       <%-- <th class="Account">状态
                        </th>--%>
                    </tr>
                    <tr id="itemPlaceholder" runat="server"></tr>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr class="Single">
                    <td class="Account">
                        <%#Eval("Count")%>
                    </td>
                    <td class="Account">
                        <%#Eval("Title")%>
                    </td>
                    <td class="Account">
                        <%#Eval("QTypeShow")%>
                    </td>
                
                </tr>
            </ItemTemplate>
         
        </asp:ListView>
    </div>
</div>
