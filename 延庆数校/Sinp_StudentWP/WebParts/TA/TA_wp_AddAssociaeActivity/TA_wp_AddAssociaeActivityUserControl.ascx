<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TA_wp_AddAssociaeActivityUserControl.ascx.cs" Inherits="Sinp_StudentWP.WebParts.TA.TA_wp_AddAssociaeActivity.TA_wp_AddAssociaeActivityUserControl" %>
<link href="/_layouts/15/Style/common.css" rel="stylesheet" />
<link href="/_layouts/15/Stu_css/StuAssociate.css" rel="stylesheet" type="text/css" />
<script src="/_layouts/15/Script/My97DatePicker/WdatePicker.js"></script>
<style type="text/css">
    td {
        vertical-align: middle;
    }
</style>
<script type="text/javascript">
    function Preview(file) { //展示图片
        var prevImg = document.getElementById('Imgshow');
        if (file.files && file.files[0]) {
            var reader = new FileReader();
            reader.onload = function (evt) {
                prevImg.src = evt.target.result;
            }
            reader.readAsDataURL(file.files[0]);
        }
        else {
            prevImg.style.display = "none";
            var divImg = document.getElementById('divImg');
            divImg.style.display = "block";
            divImg.filters.item("DXImageTransform.Microsoft.AlphaImageLoader").src = file.value;
        }
    }
</script>
<div>
    <form id="form_add" enctype="multipart/form-data">
        <div class="windowDiv">
            <table class="winTable">
                <tr>
                    <td class="mi">活动名称：
                    </td>
                    <td class="ku">
                        <input runat="server" id="txtTitle" name="txtTitle" type="text" />
                        <span class="wstar">*
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtTitle"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                    </td>
                    <td rowspan="3" style="width: 145px; padding-right: 20px; text-align: center;">
                        <img id="Imgshow" class="imgAssoci" src="/_layouts/15/Stu_images/nopic.png" />
                        <div id="divImg" class="imgAssoci" style="display: none;filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale,src=/_layouts/15/Stu_images/nopic.png); "></div>
                        <input type="file" id="fimg_Asso" name="fimg_Asso" runat="server" onchange="Preview(this);" style="width: 120px; display: none;" />
                        <a href="#" onclick="document.getElementById('<%=fimg_Asso.ClientID%>').click();" style="color: #1193f3; display: block;">更换照片</a>
                    </td>
                </tr>
                <tr>
                    <td class="mi">活动时间：
                    </td>
                    <td class="ku">
                        <input type="text" class="Wdate" readonly="readonly" runat="server" id="dtStartTime" name="dtStartTime" onclick="WdatePicker()" style="width: 97px;" />至<input type="text" class="Wdate" readonly="readonly" runat="server" id="dtEndTime" name="dtEndTime" onclick="    WdatePicker()" style="width: 97px;" />
                    </td>
                </tr>
                <tr>
                    <td class="mi">活动地址：
                    </td>
                    <td class="ku">
                        <input id="txtAddress" name="txtAddress" runat="server" type="text" />
                        <span class="wstar">*
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtAddress"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                    </td>
                </tr>
                <tr>
                    <td class="mi">活动内容：</td>
                    <td class="ku" colspan="2">
                        <table class="innerTable">
                            <tr>
                                <td style="padding: 0px;">
                                    <textarea id="txtContent" name="txtContent" class="colwidth content" runat="server"></textarea></td>
                                <td style="width: 35px; padding: 0px;"><span class="wstar">*<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtContent"
                                    ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="mi">附件：</td>
                    <td class="ku">
                        <input type="file" id="file_activity" name="file_activity" runat="server" />
                    </td>
                </tr>
            </table>
            <div class="t_btn">
                <asp:Button ID="btnAdd" CssClass="btn btn_sure" runat="server" OnClick="btnAdd_Click" ValidationGroup="ProjectSubmit" Text="发布" />
                <input type="button" class="btn btn_cancel" value="取消" onclick="parent.closePages();" />
            </div>
        </div>
    </form>
</div>
