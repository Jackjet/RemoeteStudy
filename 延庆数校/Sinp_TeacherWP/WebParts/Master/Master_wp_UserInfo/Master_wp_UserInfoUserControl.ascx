<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Master_wp_UserInfoUserControl.ascx.cs" Inherits="Sinp_TeacherWP.WebParts.Master.Master_wp_UserInfo.Master_wp_UserInfoUserControl" %>
<style type="text/css">
    .col{
        color:#00a1ec;
    }
</style>
<div class="login fl">
    <a href="#"><em>
        <asp:Image ID="Img_TeacherLittleInfo" ImageUrl="/_layouts/15/TeacherImages/photo1.jpg" runat="server" />
        </em><i><asp:Literal ID="Lit_UserName" runat="server"></asp:Literal></i></a>
</div>
<div class="qiehuan fl" style="color:blue;">
    <asp:LinkButton CssClass="col" ID="LB_ChangeUser" runat="server" OnClick="LB_ChangeUser_Click">注销</asp:LinkButton>
</div>
<div class="email fl">
    <ul class="xinfeng">

        <li>
            <dl class="slidedown">
                <dt><a href="#"><em class="e_mail iconfont">&#xe612;</em><em class="e_shuzi">46</em></a></dt>
                <%--<dd>
                    <div class="slidecon">
                        <ul class="s_con">
                            <li><a href="#">教师成长管理系统</a></li>
                            <li><a href="#">教师科研管理系统</a></li>
                            <li><a href="#">教师研修管理系统</a></li>
                        </ul>
                    </div>
                </dd>--%>
            </dl>
        </li>

    </ul>

</div>
