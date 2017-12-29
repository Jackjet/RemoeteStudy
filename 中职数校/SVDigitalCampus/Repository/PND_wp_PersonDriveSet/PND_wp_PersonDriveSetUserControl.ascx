<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PND_wp_PersonDriveSetUserControl.ascx.cs" Inherits="SVDigitalCampus.Repository.PND_wp_PersonDriveSet.PND_wp_PersonDriveSetUserControl" %>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/jquery-webox.js"></script>

<script src="../../../../_layouts/15/SVDigitalCampus/Script/enter/tz_slider.js"></script>

<script type="text/javascript">
    function Set() {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));

        $.webox({
            width: 600, height: 578, bgvisibel: true, title: '网盘最低容量设置', iframe: FirstUrl + "/SitePages/PND_wp_EditCapacity.aspx?" + Math.random
        });
    }
    function Edit(id) {
        $.webox({
            width: 600,
            height: 470,
            bgvisibel: true,
            title: '个人网盘容量调整',
            iframe: FirstUrl + "/SitePages/PND_wp_EditCapacity.aspx?ID=" + id + "&" + Math.random
        });
    }
</script>

<div class="Enterprise_information_feedback">
    <h1 class="Page_name">网盘配置</h1>

    <div class="Operation_area">
        <div class="left_choice fl">
            <ul>
                <li class="Batch_operation"></li>
            </ul>
        </div>
        <div class="right_add fr">
            <a href="#" class="add" onclick="Set()">网盘最低容量设置</a>
        </div>
    </div>
    <div class="clear"></div>

    <div class="Display_form">
        <asp:ListView ID="LV_TermList" runat="server">
            <EmptyDataTemplate>
                <table>
                    <tr>
                        <td colspan="3">暂无使用信息。</td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <LayoutTemplate>
                <table id="itemPlaceholderContainer" style="width: 100%;" class="D_form">
                    <tr class="trth">
                        <th>使用人</th>
                        <th>已使用</th>
                        <th>总容量</th>
                        <th>使用率</th>
                        <th>操作</th>
                    </tr>
                    <tr id="itemPlaceholder" runat="server"></tr>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr class="Single">
                    <td>
                        <%#Eval("User")%></td>
                    <td><%#Eval("Used")%></td>
                    <td><%#Eval("All")%></td>
                    <td><%#Eval("per")%></td>
                    <td class="Operation">
                        <a href="#" class="add" onclick="Edit('<%#Eval("ID")%>')"><i class="iconfont">&#xe629;</i></a>
                    </td>
                </tr>

            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr class="Double">

                    <td>
                        <%#Eval("User")%></td>
                    <td><%#Eval("Used")%></td>
                    <td><%#Eval("All")%></td>
                    <td><%#Eval("per")%></td>
                    <td class="Operation">
                        <a href="#" class="add" onclick="Edit('<%#Eval("ID")%>')"><i class="iconfont">&#xe629;</i></a>
                    </td>
                </tr>
            </AlternatingItemTemplate>
        </asp:ListView>
    </div>
</div>
