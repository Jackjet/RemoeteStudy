<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RC_wp_CourceSetUserControl.ascx.cs" Inherits="SVDigitalCampus.School_Courses.RC_wp_CourceSet.RC_wp_CourceSetUserControl" %>
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/common.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/iconfont.css" rel="stylesheet" />

<link href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" rel="stylesheet" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/popwin.js"></script>
<script type="text/javascript">
    function Set() {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));

        $.webox({
            width: 500, height: 238, bgvisibel: true, title: '添加上课时间', iframe: FirstUrl + "/SitePages/PND_wp_EditCapacity.aspx?" + Math.random
        });
    }
</script>
<div class="Enterprise_information_feedback">
     <div class="Operation_area">
        <div class="left_choice fl">
            <ul>
                <li class="Batch_operation"></li>
            </ul>
        </div>
        <div class="right_add fr">
            <a href="#" class="add" onclick="Set()">添加上课时间</a>
        </div>
    </div>
    <div class="clear"></div>
    <div class="Display_form">
        <asp:ListView ID="lvCourceset" runat="server" OnItemCommand="lvCourceset_ItemCommand"  OnItemEditing="lvCourceset_ItemEditing">
            <EmptyDataTemplate>

                <table class="D_form">
                    <tr class="trth">
                        <td colspan="3" style="text-align: center">暂未设置上课时间！
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
           
            <LayoutTemplate>

                <table class="D_form" id="itemPlaceholderContainer">
                    <tr class="trth">
                        <th class="Account">编号
                        </th>
                        <th class="Account">上课时间
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
                        <%# Container.DataItemIndex + 1%>
                    </td>
                    <td class="Account">
                        <%#Eval("Title")%>
                    </td>
                    <td class="Operation">
                        <asp:LinkButton ID="btndelete" BorderStyle="None" CommandName="Del" CommandArgument='<%# Eval("ID") %>' runat="server" OnClientClick="return confirm('你确定移除吗？')"><i class="iconfont">&#xe64c;</i></asp:LinkButton>
                    </td>
                </tr>
            </ItemTemplate>
            
        </asp:ListView>
    </div>
</div>
