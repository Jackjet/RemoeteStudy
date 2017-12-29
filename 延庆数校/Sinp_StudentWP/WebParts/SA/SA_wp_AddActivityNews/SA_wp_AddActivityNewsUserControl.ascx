<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SA_wp_AddActivityNewsUserControl.ascx.cs" Inherits="Sinp_StudentWP.WebParts.SA.SA_wp_AddActivityNews.SA_wp_AddActivityNewsUserControl" %>
<link href="/_layouts/15/Style/common.css" rel="stylesheet" />
<link href="/_layouts/15/Stu_css/StuAssociate.css" rel="stylesheet" type="text/css" />
<script src="/_layouts/15/Stu_js/KindUeditor/lang/zh_CN.js"></script>
<link href="/_layouts/15/Stu_js/KindUeditor/themes/default/default.css" rel="stylesheet" />
<script src="/_layouts/15/Stu_js/KindUeditor/kindeditor-min.js"></script>
<script type="text/javascript">
    var editor;
    KindEditor.ready(function (K) {
        editor = K.create('#<%=TB_Content.ClientID%>', {
            width:'650px',
            height: '240px',
            allowFileManager: true,
            items: [
	            'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline',"strikethrough",
	            'removeformat', '|', 'justifyleft', 'justifycenter', 'justifyright', 'insertorderedlist',
	            'insertunorderedlist', '|', 'undo', 'redo', '|', 'emoticons', 'image', 'link'],
            resizeType: 0,
            afterBlur: function () { this.sync(); }
        });
    });
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
<style type="text/css">
    td {
        vertical-align: middle;
    }
    .winTable tr td.wordsinfo{width:45px;}
</style>
<div>
    <form id="form_add" enctype="multipart/form-data">
        <div class="windowDiv">
            <table class="winTable">
                <tr>
                    <td class="wordsinfo">标题：</td>
                    <td class="ku">
                        <asp:TextBox ID="TB_Title" runat="server"></asp:TextBox>
                        <span class="wstar">*<asp:RequiredFieldValidator runat="server" ControlToValidate="TB_Title"
                            ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                    </td>
                    <td style="width: 145px; padding-right: 47px; text-align: center;">
                        <img id="Imgshow" class="imgAssoci" src="/_layouts/15/Stu_images/nopic.png" />
                        <div id="divImg" class="imgAssoci" style="display: none;filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale,src=/_layouts/15/Stu_images/nopic.png); "></div>
                        <input type="file" id="fimg_News" name="fimg_News" runat="server" onchange="Preview(this);" style="width: 120px; display: none;" />
                        <a href="#" onclick="document.getElementById('<%=fimg_News.ClientID%>').click();" style="color: #1193f3; display: block;">更换图片</a>
                    </td>
                </tr>                          
                <tr>
                    <td class="wordsinfo">内容：</td>
                    <td class="ku" colspan="2">
                        <table class="innerTable">
                            <tr>
                                <td style="padding: 0px;">                                    
                                     <asp:TextBox ID="TB_Content" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </td>
                                <td style="width: 35px; padding: 0px;"><span class="wstar">*<asp:RequiredFieldValidator runat="server" ControlToValidate="TB_Content"
                                    ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span></td>
                            </tr>
                        </table>
                    </td>
                </tr>                     
            </table>
            <div class="t_btn">
                <asp:Button ID="Btn_NewsSave" CssClass="btn btn_sure" runat="server" OnClick="Btn_NewsSave_Click" ValidationGroup="ProjectSubmit" Text="发布" />
                <input type="button" class="btn btn_cancel" value="取消" onclick="parent.closePages();" />
            </div>
        </div>
    </form>
</div>
