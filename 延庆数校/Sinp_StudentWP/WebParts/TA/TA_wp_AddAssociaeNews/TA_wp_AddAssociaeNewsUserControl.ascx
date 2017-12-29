<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TA_wp_AddAssociaeNewsUserControl.ascx.cs" Inherits="Sinp_StudentWP.WebParts.TA.TA_wp_AddAssociaeNews.TA_wp_AddAssociaeNewsUserControl" %>
<link href="/_layouts/15/Style/layout.css" rel="stylesheet" />
<link rel="stylesheet" href="/_layouts/15/Stu_css/allst.css">
<link rel="stylesheet" href="/_layouts/15/Stu_css/ico/iconfont.css">	
<link href="/_layouts/15/Stu_css/StuAssociate.css" rel="stylesheet" type="text/css" />
<script src="/_layouts/15/Script/jquery-1.8.0.js"></script>
<%--<script src="/_layouts/15/Script/uploadFile1.js"></script>--%>
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
<div class="windowDiv">
    <table class="winTable">
        <tr>
            <th class="mi">名称：</th>
            <td  class="ku">
                <asp:TextBox ID="TB_Title" runat="server"></asp:TextBox>
                <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_Title" runat="server" ControlToValidate="TB_Title"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
            </td>
            <td style="width: 145px; padding-right: 20px; text-align: center;">
                        <img id="Imgshow" class="imgAssoci" src="/_layouts/15/Stu_images/nopic.png" />
                        <div id="divImg" class="imgAssoci" style="display: none;filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale,src=/_layouts/15/Stu_images/nopic.png); "></div>
                        <input type="file" id="fimg_Asso" name="fimg_Asso" runat="server" onchange="Preview(this);" style="width: 120px; display: none;" />
                        <a href="#" onclick="document.getElementById('<%=fimg_Asso.ClientID%>').click();" style="color: #1193f3; display: block;">更换照片</a>
                    </td>
        </tr>
        <tr class="areatr">
            <th class="mi">内容：</th>
            <td colspan="2"  class="ku">
                <table class="innerTable">
                    <tr>
                        <td style="padding: 0px;">
                            <asp:TextBox ID="TB_Content" TextMode="MultiLine" Height="130px" class="colwidth content" runat="server"></asp:TextBox></td>
                        <td style="width:35px; padding: 0px;"><span style="color: red;">*<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TB_Content"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <%--<tr>
            <th>附件：</th>
            <td>
                <asp:Label ID="lbAttach" runat="server">
                </asp:Label>
                <asp:HiddenField ID="Hid_fileName" runat="server" />
                <table id="idAttachmentsTable0" style="display: block;">
                    <asp:Literal ID="Lit_Bind" runat="server"></asp:Literal>
                </table>
                <table style="width: 100%; display: block;">
                    <tr>
                        <td id="att0">&nbsp;
                                <input id="fileupload0" name="fileupload0" style="width: 80%;" type="file" />
                        </td>
                        <td>
                            <input type="button" onclick="UploadFile('fileupload', 'idAttachmentsTable0', 'att0')" style="width: 70px;" value="上传" /></td>
                    </tr>
                </table>
            </td>
        </tr>--%>
    </table>
    <div class="t_btn">
        <asp:Button ID="Btn_InfoSave" OnClick="Btn_InfoSave_Click" CssClass="btn btn_sure" runat="server" ValidationGroup="ProjectSubmit" Text="提交" />
        <input type="button"  class="btn btn_cancel" value="取消" onclick="parent.closePages();" />
    </div>
</div>