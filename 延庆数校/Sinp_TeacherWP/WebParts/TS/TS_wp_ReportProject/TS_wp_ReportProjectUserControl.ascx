<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TS_wp_ReportProjectUserControl.ascx.cs" Inherits="Sinp_TeacherWP.WebParts.TS.TS_wp_ReportProject.TS_wp_ReportProjectUserControl" %>

<link href="/_layouts/15/Style/tablelist.css" rel="stylesheet" />
<link href="/_layouts/15/Style/layout.css" rel="stylesheet" />
<script src="/_layouts/15/Script/uploadFile1.js"></script>
<script src="/_layouts/15/Script/My97DatePicker/WdatePicker.js"></script>


<link href="/_layouts/15/Style/choosen/style.css" rel="stylesheet" />
<link href="/_layouts/15/Style/choosen/prism.css" rel="stylesheet" />
<link href="/_layouts/15/Style/choosen/chosen.css" rel="stylesheet" />

<style type="text/css" media="all">
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
<div class="listdv">
    <table>
        <tr>
            <th>课题名称：</th>
            <td>
                <asp:TextBox ID="TB_Title" runat="server"></asp:TextBox>
                <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_Title" runat="server" ControlToValidate="TB_Title"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
            </td>
        </tr>
        <tr>
            <th>课题级别：</th>
            <td>
                <asp:DropDownList ID="DDL_ProjectLevel" runat="server"></asp:DropDownList>
                <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DDL_ProjectLevel"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
            </td>
        </tr>
        <tr>
            <th>研究领域：</th>
            <td>
                <asp:TextBox ID="TB_ResearchField" runat="server"></asp:TextBox>
                <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TB_ResearchField"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
            </td>
        </tr>
        <tr>
            <th>课题负责人：</th>
            <td>
                <asp:Literal ID="Lit_ProjectDirector" runat="server"></asp:Literal>
                <%--<div style="width:60%;" class="specspan">
                    <asp:DropDownList runat="server" ClientIDMode="Static" ID="DDL_ProjectDirector" data-placeholder="选择课题负责人" class="chosen-select" style="width:100%;" tabindex="8" >
                    </asp:DropDownList>
                    <span style="color: red;">
                    <asp:RequiredFieldValidator ID="RFV_ProjectDirector" runat="server" ControlToValidate="DDL_ProjectDirector"
                        ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                    <asp:HiddenField ID="Hid_ProjectDirector" runat="server" />
                </div>--%>
            </td>
        </tr>
        <tr>
            <th>课题成员：</th>
            <td>
                <div style="width:60%;" class="specspan">
                    <asp:DropDownList runat="server" ClientIDMode="Static" ID="DDL_TeamMember" data-placeholder="选择课题成员" class="chosen-select" multiple style="width:100%;" tabindex="8" >
                    </asp:DropDownList>
                </div>
                <span style="color: red;">
                    <asp:RequiredFieldValidator ID="RFV_TeamMember" runat="server" ControlToValidate="DDL_TeamMember"
                        ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                <asp:HiddenField ID="Hid_TeamMember" runat="server" />
            </td>
        </tr>
        <tr>
            <th>开始时间：</th>
            <td>
                <asp:TextBox ID="TB_StartDate" CssClass="Wdate" onclick="javascript: WdatePicker()" runat="server"></asp:TextBox>

                <span style="color: red;">
                    <asp:RequiredFieldValidator ID="RFV_StartTime" runat="server" ControlToValidate="TB_StartDate"
                        ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
            </td>
        </tr>
        <tr>
            <th>结束时间：</th>
            <td>
                <asp:TextBox ID="TB_EndDate" CssClass="Wdate" onclick="javascript: WdatePicker()" runat="server"></asp:TextBox>
                <span style="color: red;">*
                                  <asp:RequiredFieldValidator ID="RFV_EndTime" runat="server" ControlToValidate="TB_EndDate"
                                      ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
                </span>
            </td>
        </tr>
        <tr class="areatr">
            <th>课题内容：</th>
            <td>
                <asp:TextBox ID="TB_ProjectContent" TextMode="MultiLine" Height="82px" runat="server"></asp:TextBox>
                <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TB_ProjectContent"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
            </td>
        </tr>
        <tr>
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
        </tr>
        <tr>
            <th></th>
            <td>
                <asp:Button ID="Btn_InfoSave" OnClientClick="insertvalue()" OnClick="Btn_InfoSave_Click" CssClass="save" runat="server" ValidationGroup="ProjectSubmit" Text="保存" />
                <input type="button" class="cancel" value="取消" onclick="closetaskform('0', '')" />

            </td>
        </tr>
    </table>
</div>
<script src="/_layouts/15/Script/jquery-1.8.0.js"></script>
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
    function insertvalue()
    {
        var projectDirector = $("select[id$='DDL_ProjectDirector']").val();
        $("input[id$='ProjectDirector']").val(projectDirector);
        var member = $("select[id$='DDL_TeamMember']").val();
        $("input[id$='Hid_TeamMember']").val(member);
        return true;
    }
	</script>


