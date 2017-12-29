<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IF_wp_LoginUserControl.ascx.cs" Inherits="SVDigitalCampus.Internship_Feedback.IF_wp_Login.IF_wp_LoginUserControl" %>
<style>
    .login_dl { margin:200px auto; font-family:"微软雅黑",microsoft yehei ui;}
    .logincon {  width:430px; background: #ffffff; border-radius: 3px; box-shadow: 1px 1px 8px rgba(0, 0, 0, 0.6);  margin: 0 auto;}
    .login_tit { color:#4d4d4d; font-size:24px;padding:20px;}
    .login_dl .condl{padding:20px; padding-top:0px;} 
    .login_dl .condl .contable { width:340px; margin:auto;}
    .contable tr td{ padding:4px;color: #666;  font-size: 16px;  line-height: 36px;}
    .contable tr td input { line-height:28px; border-radius:3px; border:1px solid #ccc; height:28px;padding: 0 10px;color: #666; width:190px;}
    .btn { text-align:center; padding-top:10px;}
    .btn input { cursor:pointer; border-radius:3px; border:1px solid #4782c2; color:#fff; font-size:14px; padding:2px 14px; background:#5593d7;}
    .btn input:hover { background:#427fc1; border:1px solid #4782c2; color:#fff;}
</style>

<div class="login_dl">
        <div class="logincon">
        <div class="login_tit">
            用户登录           
        </div>
        <div class="condl">
            <table class="contable">
                <tr >
                    <td class="lftd">企业名称：</td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" ></asp:TextBox>
                </tr>
                <tr >
                    <td class="lftd">用&nbsp;户&nbsp;&nbsp;名：</td>
                    <td>
                        <asp:TextBox ID="txtUser" runat="server" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="lftd">密&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;码：</td>
                    <td>
                        <input type="password" runat="server" id="txtPwd" />

                    </td>
                    
                </tr>
                
            </table>
            <div class="btn">
                <asp:Button ID="Login" runat="server" Text="登陆" BackColor="#76ACDD" Width="90px" Height="30px" OnClick="Login_Click"/>
            </div>
        </div>
    </div>

</div>
