﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TA_wp_AddAssociaeUserControl.ascx.cs" Inherits="Sinp_StudentWP.WebParts.TA.TA_wp_AddAssociae.TA_wp_AddAssociaeUserControl" %>
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
            <td class="mi">社团名称：</td>
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
                <a href="#" onclick="$('#<%=fimg_Asso.ClientID%>').click();" style="color: #1193f3;display:block;">更换照片</a>
            </td>
        </tr>
        <tr>
            <td class="mi">社团口号：</td>
            <td class="ku">
                <asp:TextBox ID="TB_Slogans" runat="server"></asp:TextBox>
                <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TB_Slogans"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
            </td>
        </tr>
        <tr>
            <td class="mi">社团类型：</td>
            <td class="ku">
                <asp:DropDownList ID="DDL_Type" runat="server" CssClass="droplist"></asp:DropDownList>
                <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DDL_Type"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
            </td>
        </tr>
        <tr runat="server" id="leader">
            <td class="mi">社 团 长：</td>
            <td class="ku">
                <asp:DropDownList ID="DDL_Leader" runat="server" CssClass="droplist"></asp:DropDownList>
            </td>
        </tr>
        <tr runat="server" id="sec_leader">
            <td class="mi">副 团 长：</td>
            <td class="ku">
                <asp:DropDownList ID="DDL_SecLeader" ClientIDMode="Static" runat="server"  class="chosen-select" data-placeholder="选择副团长" multiple></asp:DropDownList><asp:HiddenField ID="Hid_SecLeader"  ClientIDMode="Static" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="mi">社团介绍：</td>
            <td colspan="2" class="ku">
                <table class="innerTable">
                    <tr>
                        <td style="padding: 0px;">
                            <asp:TextBox ID="TB_Content" TextMode="MultiLine"  class="colwidth content" runat="server"></asp:TextBox></td>
                        <td style="width:35px; padding: 0px;"><span style="color: red;">*<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TB_Content"
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
        var member = $("select[id$='DDL_SecLeader']").val();
        $("input[id$='Hid_SecLeader']").val(member);
        return true;
    }
	</script>
