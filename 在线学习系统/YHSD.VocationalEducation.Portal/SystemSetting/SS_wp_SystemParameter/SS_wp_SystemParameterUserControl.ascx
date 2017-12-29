<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SS_wp_SystemParameterUserControl.ascx.cs" Inherits="YHSD.VocationalEducation.Portal.SystemSetting.SS_wp_SystemParameter.SS_wp_SystemParameterUserControl" %>
<link href="/_layouts/15/Style/common.css" rel="stylesheet" />
<link href="/_layouts/15/Style/layout.css" rel="stylesheet" />
<link href="/_layouts/15/Style/allst.css" rel="stylesheet" >
<link href="/_layouts/15/Style/StuAssociate.css" rel="stylesheet" />
<script src="/_layouts/15/Script/jquery-1.8.2.min.js"></script>
<script type="text/javascript">
    function Preview(file) { //展示图片        
        var prevImg = document.getElementById("<%=img_Pic.ClientID%>");
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
<div class="writingform">
    <div class="writing_title">
        <h2>
            <span class="title_left fl">系统参数管理</span>
        </h2>
    </div>
    <div class="writing_form" style="margin:auto;width:650px">
        <table class="winTable" style="margin-left:90px">
            <tr>
                <th class="mi">名称：</th>
                <td class="ku">
                    <asp:TextBox ID="TB_Title" runat="server"></asp:TextBox>
                    <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_Title" runat="server" ControlToValidate="TB_Title"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                </td>
                <td rowspan="3" style="width: 145px; padding-right: 20px; text-align: center;">
                <asp:Image ID="img_Pic"  CssClass="imgAssoci" runat="server" ImageUrl="/_layouts/15/Stu_images/nopic.png" />
                 <div id="divImg" class="imgAssoci" style="display: none;filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale,src=/_layouts/15/Stu_images/nopic.png); "></div>
                <input type="file" id="fimg_Asso" name="fimg_Asso" runat="server" onchange="Preview(this);" style="width: 120px; display: none;" />
                <a href="#" onclick="$('#<%=fimg_Asso.ClientID%>').click();" style="color: #1193f3;display:block;">更换logo照片</a>
            </td>
            </tr>
            <tr class="areatr">
                <th class="mi" style="padding-top: 10px; vertical-align: top;">注册须知：</th>
                <td colspan="2" class="ku">
                    <table class="innerTable">
                        <tr>
                            <td style="padding: 0px;">
                                <asp:TextBox ID="TB_Content" TextMode="MultiLine" Height="300px" class="colwidth content" runat="server"></asp:TextBox></td>
                            <td style="width: 35px; padding: 0px;"><span style="color: red;">*<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TB_Content"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div style="margin:20px 0px 20px 250px">
            <asp:Button ID="Btn_InfoSave" OnClick="Btn_InfoSave_Click" CssClass="btn btn_sure" runat="server" ValidationGroup="ProjectSubmit" Text="提交" />
            <input type="button" class="btn btn_cancel" value="取消" onclick="parent.closePages();" />
        </div>
    </div>
</div>