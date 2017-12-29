<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RR_wp_AddResourceUserControl.ascx.cs" Inherits="SVDigitalCampus.ResourceReservation.RR_wp_AddResource.RR_wp_AddResourceUserControl" %>
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/common.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/style.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/iconfont.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/animate.css" />
<script src="/_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script src="/_layouts/15/SVDigitalCampus/Script/DatePicker/WdatePicker.js"></script>
<script type="text/javascript">
    function close() {
        //$("#mask,#maskTop", parent.document).fadeOut(function () {
        //    $(this).remove();
        //document.parentWindow.location.reload(true);
        parent.location.reload(true);
        //});
        //parent.location.reload();
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
<style type="text/css">
    .t_btn input {
        padding-top: 0px;
    }
</style>
<div>
    <form id="form1" enctype="multipart/form-data">
        <div class="MenuDiv">
            <table class="tbEdit">

                <tr>
                    <td class="mi">
                        <span class="m">资源类型：</span>
                    </td>
                    <td class="ku">
                        <asp:Literal ID="Lit_Type" runat="server"></asp:Literal>
                        <asp:HiddenField ID="Hid_TypeId" runat="server" />
                    </td>

                </tr>
                <tr>
                    <td class="mi">
                        <asp:Literal ID="Lit_Title" runat="server" Text="会议室名称："></asp:Literal>
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
                        <asp:Literal ID="Lit_Place" runat="server" Text="地点："></asp:Literal>
                    </td>
                    <td class="ku">
                        <asp:TextBox ID="TB_Place" runat="server"></asp:TextBox>
                        <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_Place" runat="server" ControlToValidate="TB_Place"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                    </td>
                </tr>
                <tr>
                    <td class="mi">
                        <asp:Literal ID="Lit_Area" runat="server" Text="面积："></asp:Literal></td>
                    <td class="ku">

                        <asp:TextBox ID="TB_Area" runat="server"></asp:TextBox>
                        <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_Area" runat="server" ControlToValidate="TB_Area"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                    </td>
                </tr>
                <tr>
                    <td class="mi">
                        <asp:Literal ID="Lit_LimitCount" runat="server" Text="容纳人数："></asp:Literal>
                    </td>
                    <td class="ku">

                        <asp:TextBox ID="TB_LimitCount"  runat="server"></asp:TextBox>
                        <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_LimitCount" runat="server" ControlToValidate="TB_LimitCount"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
                                </span>
                    </td>
                </tr>
                <asp:Panel ID="Pan_show" Visible="false" runat="server">
                    <tr>
                        <td class="mi">
                            <asp:Literal ID="Lit_OpenTime" runat="server" Text="开放时间："></asp:Literal>
                        </td>
                        <td class="ku">

                            <asp:TextBox CssClass="Wdate" onfocus="WdatePicker({skin:'whyGreen',dateFmt:'H:mm:ss'})"  ID="TB_OpenTime" runat="server"></asp:TextBox>
                            <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_OpenTime" runat="server" ControlToValidate="TB_OpenTime"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>

                    </tr>
                    <tr>
                        <td class="mi">
                            <asp:Literal ID="Lit_CloseTime" runat="server" Text="关闭时间："></asp:Literal>
                        </td>
                        <td class="ku">

                            <asp:TextBox CssClass="Wdate" onfocus="WdatePicker({skin:'whyGreen',dateFmt:'H:mm:ss'})" ID="TB_CloseTime" runat="server"></asp:TextBox>
                            <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_CloseTime" runat="server" ControlToValidate="TB_CloseTime"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                </asp:Panel>
                <tr>
                    <td class="mi">相关图片：
                    </td>
                    <td class="ku">
                        <asp:FileUpload ID="zpload" runat="server" />


                    </td>
                </tr>
                <tr>
                    <td class="mi"></td>
                    <td class="ku t_btn">
                        <asp:Button ID="btnAdd" ValidationGroup="ProjectSubmit" CssClass="btn" runat="server" OnClick="btnAdd_Click" Text="提交" />
                    </td>
                </tr>
            </table>
            
        </div>
    </form>
</div>
