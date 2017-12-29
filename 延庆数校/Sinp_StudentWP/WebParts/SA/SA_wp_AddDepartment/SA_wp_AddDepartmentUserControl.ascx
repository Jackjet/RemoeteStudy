<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SA_wp_AddDepartmentUserControl.ascx.cs" Inherits="Sinp_StudentWP.WebParts.SA.SA_wp_AddDepartment.SA_wp_AddDepartmentUserControl" %>
<script src="/_layouts/15/Script/jquery-1.8.0.js"></script>
<link href="/_layouts/15/Style/tablelist.css" rel="stylesheet" />
<link href="/_layouts/15/Style/common.css" rel="stylesheet" />
<link href="/_layouts/15/Style/layout.css" rel="stylesheet" />
<link href="/_layouts/15/Stu_css/StuAssociate.css" rel="stylesheet" type="text/css" />
<link href="/_layouts/15/Style/choosen/prism.css" rel="stylesheet" />
<link href="/_layouts/15/Style/choosen/chosen.css" rel="stylesheet" />
<script src="/_layouts/15/Script/uploadFile1.js"></script>
<style type="text/css">    
    td{
        vertical-align:middle;
    }
    .chosen-rtl .chosen-drop {
        left: -9000px;
    }
    .specspan div a span{
        color:black !important;
    }
    .specspan span{
        color:black !important;
    }
</style>
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
<div class="windowDiv">
    <table class="winTable">
        <tr>
            <td class="wordsinfo">部门名称：</td>
            <td class="ku">
                <asp:TextBox ID="TB_Title" runat="server"></asp:TextBox>
                <span style="color: red;">*
                     <asp:RequiredFieldValidator ID="RFV_Title" runat="server" ControlToValidate="TB_Title"
                      ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
            </td>
            <td rowspan="3" style="width: 145px; padding-right: 47px;text-align: center;">
                <asp:Image ID="img_Pic"  CssClass="imgAssoci" runat="server" ImageUrl="/_layouts/15/Stu_images/nopic.png" />
                 <div id="divImg" class="imgAssoci" style="display: none;filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale,src=/_layouts/15/Stu_images/nopic.png); "></div>
                <input type="file" id="fimg_Depart" name="fimg_Depart" runat="server" onchange="Preview(this);" style="width: 120px; display: none;" />
                <a href="#" onclick="$('#<%=fimg_Depart.ClientID%>').click();" style="color: #1193f3;display:block;">更换照片</a>
            </td>
        </tr>        
        <tr runat="server" id="leader">
            <td class="wordsinfo">部 长：</td>
            <td class="ku">
                <asp:DropDownList ID="DDL_Leader" ClientIDMode="Static" runat="server"  class="chosen-select" data-placeholder="选择部长"></asp:DropDownList><asp:HiddenField ID="Hid_Leader"  ClientIDMode="Static" runat="server" />
                <span style="color: red;">*
                     <asp:RequiredFieldValidator ID="RFV_Leader" runat="server" ControlToValidate="DDL_Leader"
                      ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
            </td>
        </tr>
        <tr runat="server" id="sec_leader">
            <td class="wordsinfo">副部长：</td>
            <td class="ku">
                <asp:DropDownList ID="DDL_SecLeader" ClientIDMode="Static" runat="server"  class="chosen-select" data-placeholder="选择副部长" multiple="true"></asp:DropDownList><asp:HiddenField ID="Hid_SecLeader"  ClientIDMode="Static" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="wordsinfo">部门介绍：</td>
            <td colspan="2" class="ku">
                <table class="innerTable">
                    <tr>
                        <td style="padding: 0px;">
                            <asp:TextBox ID="TB_Content" TextMode="MultiLine"  class="colwidth content" runat="server"></asp:TextBox></td>
                        <td style="width:35px; padding: 0px;"><span style="color: red;">*<asp:RequiredFieldValidator ID="RFV_Content" runat="server" ControlToValidate="TB_Content"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div class="t_btn">
        <asp:Button ID="Btn_InfoSave" CssClass="btn btn_sure" runat="server" OnClientClick="insertvalue()" OnClick="Btn_InfoSave_Click" ValidationGroup="ProjectSubmit" Text="确定" />
        <input type="button" class="btn btn_cancel" value="取消" onclick="parent.closePages();" />
    </div>
</div>
<script src="/_layouts/15/Script/choosen/chosen.jquery.js"></script>
<script src="/_layouts/15/Script/choosen/prism.js"></script>
<script type="text/javascript">
    var config = {
        '.chosen-select': {},
        '.chosen-select-deselect': { allow_single_deselect: true },
        '.chosen-select-no-single': { disable_search_threshold: 10 },
        '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
        '.chosen-select-width': { width: "95%" }
    }
    for (var selector in config) {
        $(selector).chosen(config[selector]);
    }
    function insertvalue() {        
        $("input[id$='Hid_Leader']").val($("select[id$='DDL_Leader']").val());
        $("input[id$='Hid_SecLeader']").val($("select[id$='DDL_SecLeader']").val());
        return true;
    }
	</script>
