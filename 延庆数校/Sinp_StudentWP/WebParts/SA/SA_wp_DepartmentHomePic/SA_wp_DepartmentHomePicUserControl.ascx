<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SA_wp_DepartmentHomePicUserControl.ascx.cs" Inherits="Sinp_StudentWP.WebParts.SA.SA_wp_DepartmentHomePic.SA_wp_DepartmentHomePicUserControl" %>
<link href="/_layouts/15/Style/common.css" rel="stylesheet" />
<link href="/_layouts/15/Stu_css/StuAssociate.css" rel="stylesheet" type="text/css"/>
<script src="/_layouts/15/Script/jquery-1.8.0.js"></script>
<script type="text/javascript">
    $(function () {
        $("input:radio[name='arrange']").click(function () {
            $(".homeshowimg").css("background-repeat", this.id.replace("ra", ""));
        });
        $("input:radio[name='align']").click(function () {
            $(".homeshowimg").css("background-position-y", this.id.replace("ra", ""));
        });
    })
    function Preview(file) { //展示图片        
        var prevImg = document.getElementById('<%=Imgshow.ClientID%>');
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
    function UploadfileClick() {
        document.getElementById('<%=fimg_Asso.ClientID%>').click();
    }
</script>
<div class="windowDiv">
    <table style="border-bottom: 1px solid #eaeaea;width:100%;">
        <tr>
            <td colspan="2">          
                <img id="Imgshow" runat="server" src="/_layouts/15/Stu_images/homepic.jpeg"  class="homeshowimg"/> 
                <div id="divImg" class="homeshowimg" style="display: none;filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale,src=/_layouts/15/Stu_images/nopic.png); "></div>
            </td>
        </tr>
        <tr>
            <td style="text-align:center;">支持5M以内的JPG、GIF、PNG格式</td>
            <td style="padding-left:15px;">排列：<input type="radio" name="arrange" id="rarepeat-x"/>横向平铺
                <input type="radio" name="arrange" id="rarepeat-y"/>纵向平铺</td>
        </tr>
        <tr>
            <td style="text-align:center;">
                <input type="file" id="fimg_Asso" name="fimg_Asso" runat="server" onchange="Preview(this)" style="width:120px;display:none;"/>                                           
                <input type="button" class="btn btn_cancel" value="上传" onclick="UploadfileClick();" />
            </td>
            <td style="padding-left:15px;">对齐：<input type="radio" id="ratop" name="align"/>居上
                <input type="radio" name="align" id="racenter"/>居中<input type="radio" name="align" id="rabottom"/>居下</td>
        </tr>
        <tr><td colspan="2" style="padding:5px 2px;"><span style="color:red;">注：图片宽度与高度比例最好在3：1左右，防止图片严重拉伸变形</span></td></tr>
    </table>
    <div class="t_btn">
    <asp:Button ID="btnAdd" CssClass="btn btn_sure" runat="server" Text="保存设置" OnClick="btnAdd_Click" />
    <input type="button" class="btn btn_cancel" value="取消保存" onclick="parent.closePages();" />
</div>
</div>

