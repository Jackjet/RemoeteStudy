<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SA_wp_AddActivityUserControl.ascx.cs" Inherits="Sinp_StudentWP.WebParts.SA.SA_wp_AddActivity.SA_wp_AddActivityUserControl" %>
<link href="/_layouts/15/Style/iconfont.css" rel="stylesheet"/>
<link href="/_layouts/15/Style/common.css" rel="stylesheet" />
<link href="/_layouts/15/Stu_css/StuAssociate.css" rel="stylesheet" type="text/css" />
<link href="/_layouts/15/Style/choosen/prism.css" rel="stylesheet" />
<link href="/_layouts/15/Style/choosen/chosen.css" rel="stylesheet" />
<script src="/_layouts/15/Script/jquery-1.8.0.js"></script>
<script src="/_layouts/15/Script/My97DatePicker/WdatePicker.js"></script>
<script src="/_layouts/15/Script/uploadFile.js"></script>
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
    function AddProject() {        
        var $proObj = $("#<%=TB_Project.ClientID%>");
        var proName = $proObj.val();
        var isexist = false;
        if (proName.trim().length > 0) {
            $.each($("#ul_project").find("li").find("span"),function () {
                if ($(this).html() == proName) { isexist = true;}
            });
            if (!isexist) {
                if (proName.indexOf("㊣") == -1) {
                    var proLi = "<li class='pro_li'><span style='margin-left:7px'>" + proName + "</span><a herf='#' onclick='$(this).parent().remove();'><i class='iconfont'>&#xe65c;</i></a></li>";
                    $("#ul_project").prepend(proLi);
                    $proObj.val('');
                    $("#sp_warn").html('');
                }
                else { SetWarning($("#sp_warn"), "名称中不能包含<q>㊣</q>字符"); }
            } else {
                SetWarning($("#sp_warn"),"该项目已存在!");               
            }
        }
    }  
    function SetWarning($obj,text) {
        $obj.html(text);
        setTimeout(function () { $obj.html(''); }, 3000);
    }
</script>
<style type="text/css">
    td {
        vertical-align: middle;
    }

    .winTable tr td.wordsinfo {
        width: 126px;
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
    .pro_li {float:left;height:20px;border:1px solid #0da6ec;margin:2px;padding:3px;list-style:none;line-height:20px}
    .pro_li a{cursor:pointer;margin-left:5px;}
</style>
<div>
    <form id="form_add" enctype="multipart/form-data">
        <div class="windowDiv">
            <table class="winTable">
                <tr>
                    <td class="wordsinfo">活动名称：</td>
                    <td class="ku">
                        <asp:TextBox ID="TB_Title" runat="server"></asp:TextBox>
                        <span class="wstar">*<asp:RequiredFieldValidator runat="server" ControlToValidate="TB_Title"
                            ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                    </td>
                    <td rowspan="4" style="width: 145px; padding-right: 47px; text-align: center;">
                        <asp:Image ID="img_Pic" CssClass="imgAssoci" runat="server" ImageUrl="/_layouts/15/Stu_images/zs28.jpg" />
                        <div id="divImg" class="imgAssoci" style="display: none; filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale,src=/_layouts/15/Stu_images/zs28.jpg);"></div>
                        <input type="file" id="fimg_Active" name="fimg_Active" runat="server" onchange="Preview(this);" style="width: 120px; display: none;" />
                        <a href="#" onclick="$('#<%=fimg_Active.ClientID%>').click();" style="color: #1193f3; display: block;">更换照片</a>
                    </td>
                </tr>
                <tr>
                    <td class="wordsinfo">活动范围：</td>
                    <td class="ku">
                        <asp:DropDownList ID="DDL_Range" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDL_Range_SelectedIndexChanged"></asp:DropDownList>
                        <span class="wstar">*<asp:RequiredFieldValidator runat="server" ControlToValidate="DDL_Range"
                            ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                    </td>
                </tr>
                <tr>
                    <td class="wordsinfo">活动类型：</td>
                    <td class="ku">
                        <asp:DropDownList ID="DDL_ActivityType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDL_ActivityType_SelectedIndexChanged"></asp:DropDownList>
                        <span class="wstar">*<asp:RequiredFieldValidator runat="server" ControlToValidate="DDL_ActivityType"
                            ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                    </td>
                </tr>
                 <tr>
                    <td class="wordsinfo">活动开始日期：</td>
                    <td class="ku" colspan="2">
                        <asp:TextBox ID="TB_ActBeginDate" CssClass="Wdate" onclick="javascript: WdatePicker()" runat="server"></asp:TextBox>
                        <span class="wstar">* <asp:CompareValidator runat="server" ControlToValidate="TB_ActBeginDate"
            ErrorMessage="不能小于报名截止日期" ControlToCompare="TB_EndDate" Operator="GreaterThanEqual" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:CompareValidator></span>
                    </td>
                </tr>
                <tr>
                    <td class="wordsinfo">活动结束日期：</td>
                    <td class="ku" colspan="2">
                        <asp:TextBox ID="TB_ActEndDate" CssClass="Wdate" onclick="javascript: WdatePicker()" runat="server"></asp:TextBox>
                        <span class="wstar">*
                             <asp:CompareValidator runat="server" ControlToValidate="TB_ActEndDate"
            ErrorMessage="不能小于活动开始日期" ControlToCompare="TB_ActBeginDate" Operator="GreaterThanEqual" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:CompareValidator>
                        </span>
                    </td>
                </tr>
                <tr id="TR_MaxCount" runat="server">
                    <td class="wordsinfo">最多报名项目个数：</td>
                    <td class="ku">
                        <asp:TextBox ID="TB_MaxCount" runat="server"></asp:TextBox>
                        <span class="wstar">*<asp:RequiredFieldValidator runat="server" ControlToValidate="TB_MaxCount"
                            ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                    </td>
                </tr>
                <tr>
                    <td class="wordsinfo">活动简介：</td>
                    <td class="ku" colspan="2">
                        <table class="innerTable">
                            <tr>
                                <td style="padding: 0px;">
                                    <asp:TextBox ID="TB_Introduction" TextMode="MultiLine" CssClass="colwidth content" runat="server"></asp:TextBox>
                                </td>
                                <td style="width: 35px; padding: 0px;"><span class="wstar">*<asp:RequiredFieldValidator runat="server" ControlToValidate="TB_Introduction"
                                    ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="wordsinfo">报名条件：</td>
                    <td class="ku">
                        <asp:TextBox ID="TB_Condition" runat="server"></asp:TextBox>
                        <span class="wstar">*<asp:RequiredFieldValidator runat="server" ControlToValidate="TB_Condition"
                            ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                    </td>
                </tr>
                <tr>
                    <td class="wordsinfo">报名开始日期：</td>
                    <td class="ku">
                        <asp:TextBox ID="TB_BeginDate" CssClass="Wdate" onclick="javascript: WdatePicker()" runat="server"></asp:TextBox>
                        <span class="wstar">*<asp:RequiredFieldValidator runat="server" ControlToValidate="TB_BeginDate"
                            ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                    </td>
                </tr>
                <tr>
                    <td class="wordsinfo">报名截止日期：</td>
                    <td class="ku" colspan="2">
                        <asp:TextBox ID="TB_EndDate" CssClass="Wdate" onclick="javascript: WdatePicker()" runat="server"></asp:TextBox>
                        <span class="wstar">*
                             <asp:CompareValidator runat="server" ControlToValidate="TB_EndDate"
            ErrorMessage="不能小于报名开始日期" ControlToCompare="TB_BeginDate" Operator="GreaterThanEqual" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:CompareValidator>
                        </span>
                    </td>
                </tr>                 
                <tr>
                    <td class="wordsinfo">报名地点：</td>
                    <td class="ku">
                        <asp:TextBox ID="TB_Address" runat="server"></asp:TextBox>
                        <span class="wstar">*<asp:RequiredFieldValidator runat="server" ControlToValidate="TB_Address"
                            ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                    </td>
                </tr>
                <tr id="TR_Project" runat="server">
                    <td class="wordsinfo">项目名称：</td>
                    <td class="ku" colspan="2" style="vertical-align:top;border:1px solid #ccc;height:80px;">
                        <asp:TextBox ID="TB_Project" runat="server"></asp:TextBox>                       
                        <input type="button" class="btn btn_cancel" style="margin-right:2px;" value="添加" onclick="AddProject();" /> 
                        <asp:HiddenField ID="Hid_Pros"  runat="server" />
                        <span class="wstar">* <span id="sp_warn" style="color:red;"></span></span>                                           
                        <div class="div_collpro"><ul id="ul_project"></ul></div>
                    </td>
                </tr>                               
                <tr>
                    <td class="wordsinfo">发起人：</td>
                    <td class="ku">
                        <asp:DropDownList ID="DDL_OrganizeUser" ClientIDMode="Static" runat="server"  class="chosen-select" data-placeholder="选择发起人" multiple="true"></asp:DropDownList><asp:HiddenField ID="Hid_OrganizeUser"  ClientIDMode="Static" runat="server" />
                        <span class="wstar">*<asp:RequiredFieldValidator runat="server" ControlToValidate="DDL_OrganizeUser"
                            ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                    </td>
                </tr>
                <tr>
                    <td class="wordsinfo">附 件：</td>
                    <td class="ku">
                         <asp:Label ID="lbAttach" runat="server">
                    </asp:Label>
                    <asp:HiddenField ID="Hid_fileName" runat="server" />
                    <table id="idAttachmentsTable" style="display: block;">
                        <asp:Literal ID="Lit_Bind" runat="server"></asp:Literal>
                    </table>
                    <table style="width: 100%; display: block;">
                        <tr>
                            <td id="att1">&nbsp;
                                  <input id="fileupload0" name="fileupload0" style="width: 80%;" type="file" />
                            </td>
                            <td>
                                <input onclick="AddFile()" style="width: 70px;" type="button" value="上传" /></td>
                        </tr>
                    </table>
                    </td>
                </tr>
            </table>
            <div class="t_btn">
                <asp:Button ID="Btn_ActivitySave" CssClass="btn btn_sure" runat="server" ValidationGroup="ProjectSubmit" OnClick="Btn_ActivitySave_Click" OnClientClick="insertvalue();if(!confirm(this.value+'后将不能修改，确定要立即'+this.value+'？'))return false;" Text="发布" />
                <input type="button" class="btn btn_cancel" value="取消" onclick="parent.closePages();" />
            </div>
        </div>
    </form>
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
        $("input[id$='Hid_OrganizeUser']").val($("select[id$='DDL_OrganizeUser']").val());

        var $proHid = $("#<%=Hid_Pros.ClientID%>");
        var proArray = [];    
        $.each($("#ul_project").find("li").find("span"), function () {
            proArray.push($(this).html());
        });      
        $proHid.val(proArray.join("㊣"));
        return true;        
    }
	</script>

