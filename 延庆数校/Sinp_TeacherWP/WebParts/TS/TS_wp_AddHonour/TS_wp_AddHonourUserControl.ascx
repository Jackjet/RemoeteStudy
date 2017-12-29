<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TS_wp_AddHonourUserControl.ascx.cs" Inherits="Sinp_TeacherWP.WebParts.TS.TS_wp_AddHonour.TS_wp_AddHonourUserControl" %>
<link href="/_layouts/15/Style/layout.css" rel="stylesheet" />
<script src="/_layouts/15/Script/jquery-1.8.0.js"></script>
<script src="/_layouts/15/Script/uploadFile.js"></script>
<div class="listdv">
            <table>
                <tr>
                    <th>荣誉名称：</th>
                    <td>
                        <asp:HiddenField ID="Hid_PhaseName" runat="server" />
                        <asp:TextBox ID="TB_Title" runat="server"></asp:TextBox>
                        <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_Title" runat="server" ControlToValidate="TB_Title"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                    </td>
                </tr>

                <tr class="areatr">
                    <th>荣誉内容：</th>
                    <td>
                        <asp:TextBox ID="TB_Content" TextMode="MultiLine" Height="82px" runat="server"></asp:TextBox>
                        <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_Content" runat="server" ControlToValidate="TB_Content"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                    </td>
                </tr>
                <tr>
                    <th>附件：</th>
                    <td style="vertical-align: middle;">
                        <asp:Label ID="lbAttach" runat="server">
                        </asp:Label>
                        <asp:HiddenField ID="Hid_fileName" runat="server" />
                        <table id="idAttachmentsTable" style="display: block;">
                            <asp:Literal ID="Lit_Bind" runat="server"></asp:Literal>
                        </table>
                        <table style="width: 100%; display: block;">
                            <tr>
                                <td id="att1">&nbsp;
                                            <input id="fileupload0" name="fileupload0" type="file" />
                                </td>
                                <td>
                                    <input onclick="AddFile()" style="width: 70px;" type="button" value="上传" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <th></th>
                    <td>
                        <asp:Button ID="Btn_InfoSave" CssClass="save" runat="server" OnClick="Btn_InfoSave_Click" ValidationGroup="ProjectSubmit" Text="保存" />
                        <input type="button" class="cancel" value="取消" onclick="parent.closePages();" />

                    </td>
                </tr>
            </table>
        </div>