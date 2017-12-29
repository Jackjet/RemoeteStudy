<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RR_wp_AddRoomUserControl.ascx.cs" Inherits="SVDigitalCampus.ResourceReservation.RR_wp_AddRoom.RR_wp_AddRoomUserControl" %>
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/common.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/style.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/iconfont.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/animate.css" />
<script src="/_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script src="/_layouts/15/SVDigitalCampus/Script/popwin.js"></script>
<script type="text/javascript">
    function close() {
        var linum = 0;
        linum = $("input[id$=Hid_linum]").val();
        if (parent.location.href.indexOf("?")==-1)
        {
            parent.location.href = parent.location.href + "?linum=" + linum;
        }
        else
        {
            parent.location.href = parent.location.href + "&linum=" + linum;
        }
        
    }
    
    
</script>
<style type="text/css">
    .t_btn input{
        padding-top:0px;
    }
</style>
<div>
    <form id="form1" enctype="multipart/form-data">
        <div class="MenuDiv">
            <table class="tbEdit">

                <tr>
                    <td class="mi">
                        <span class="m">人员姓名：</span>
                    </td>
                    <td class="ku">
                        <asp:TextBox ID="TB_Title" runat="server"></asp:TextBox>
                        <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_Title" runat="server" ControlToValidate="TB_Title"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                    </td>

                </tr>
                <tr>
                    <td class="mi">
                        <span class="m">联系电话：</span>
                    </td>
                    <td class="ku">
                        <asp:TextBox ID="TB_ContactPhone" runat="server"></asp:TextBox>
                        <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_ContactPhone" runat="server" ControlToValidate="TB_ContactPhone"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="REV_ContactPhone" runat="server" ErrorMessage="请输入有效手机号"
                                    Display="Dynamic" ControlToValidate="TB_ContactPhone" ValidationExpression="^((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)$"
                                    ValidationGroup="ProjectSubmit"></asp:RegularExpressionValidator></span>
                    </td>

                </tr>
                <tr>
                    <td class="mi">
                        <span class="m">所属学校：</span>
                    </td>
                    <td class="ku">
                        <asp:TextBox ID="TB_BelongSchool" runat="server"></asp:TextBox>
                        <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_BelongSchool" runat="server" ControlToValidate="TB_BelongSchool"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                    </td>

                </tr>
                <tr>
                    <td class="mi">备注：
                    </td>
                    <td class="ku">
                        <asp:TextBox ID="TB_Description" TextMode="MultiLine" runat="server"></asp:TextBox>
                        </td>

                </tr>
                <tr>

                    <td class="mi"></td>
                    <td class="ku t_btn">

                        <asp:Button id="btnAdd" ValidationGroup="ProjectSubmit" CssClass="btn" runat="server" OnClick="btnAdd_Click" Text="提交"/>
                <asp:HiddenField ID="Hid_linum" runat="server" />

                    </td>

                </tr>

            </table>
            <%--<div class="t_btn">
                
            </div>--%>
        </div>
    </form>
</div>