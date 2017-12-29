<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ES_wp_ExamQTypeManagerUserControl.ascx.cs" Inherits="SVDigitalCampus.Examination_System.ES_wp_ExamQTypeManager.ES_wp_ExamQTypeManagerUserControl" %>
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/common.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/iconfont.css" rel="stylesheet" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/animate.css" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" rel="stylesheet" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/popwin.js"></script>
<script type="text/javascript">
    function close() {
        parent.location.reload(true);
    }
</script>
<div class="MenuType_information_SchoolLibrary">
    <div class="MenuType_form">
        <asp:ListView ID="lvEQType" runat="server" OnItemCommand="lvEQType_ItemCommand" OnItemCanceling="lvEQType_ItemCanceling" OnItemInserting="lvEQType_ItemInserting" OnItemUpdating="lvEQType_ItemUpdating" EnableModelValidation="True"
            InsertItemPosition="LastItem" DataKeyNames="ID" OnItemEditing="lvEQType_ItemEditing" OnItemDataBound="lvEQType_ItemDataBound" >
            <EmptyDataTemplate>

                <table class="D_form">
                    <tr class="trth">
                        <td colspan="3" style="text-align: center">您好，暂无试题类型记录！
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <AlternatingItemTemplate>
                <tr class="Double">

                    <td class="Account">
                        <%#Eval("Count")%>
                    </td>
                    <td class="Account">
                        <%#Eval("Title")%>
                    </td>
                    <td class="Account">
                        <%#Eval("QTypeShow")%>
                    </td>
                  <%--  <td class="Account">
                        <%#Eval("TemplateShow")%>
                    </td>--%>
                    <%--<td class="Account">
                        <%#Eval("StatusShow")%>
                    </td>--%>
                   <%-- <td class="Operation">
                        <a href="javascript:void(0);" onclick="UpdateObj('<%# Eval("ID") %>');" style="color:blue;">修改</a>
                        <asp:LinkButton ID="btnEdit" CommandName="Edit" CommandArgument='<%# Eval("ID") %>' runat="server"><i class="iconfont">&#xe629;</i></asp:LinkButton>
                       <asp:LinkButton ID="btndelete" CommandName="del" CommandArgument='<%# Eval("ID") %>' runat="server" OnClientClick="return confirm('你确定移除吗？')"><i class="iconfont">&#xe64c;</i></asp:LinkButton>

                    </td>--%>
                </tr>
            </AlternatingItemTemplate>
            <%--<EditItemTemplate>
                <tr class="Single">

                    <td class="Account">
                        <%# Eval("Count")%>
                    </td>
                    <td class="Account">
                        <asp:TextBox ID="txtTitle" CssClass="edit" runat="server" Text='<%# Bind("Title")%>'></asp:TextBox>
                    </td>
                    <td class="MarkType">
                        <asp:DropDownList ID="ddlMarkType" runat="server">
                            <asp:ListItem Text="主观" Value="1" />
                            <asp:ListItem Text="客观" Value="2" />
                        </asp:DropDownList>
                        <asp:HiddenField ID="MarkType" Value='<%# Bind("QType")%>' runat="server" />
                    </td>
                       <td class="Template">
                        <asp:DropDownList ID="ddlTemplate" runat="server">
                            <asp:ListItem Text="单选" Value="1" />
                            <asp:ListItem Text="多选" Value="2" />
                            <asp:ListItem Text="判断" Value="3" />
                            <asp:ListItem Text="文本框" Value="4" />
                        </asp:DropDownList>
                         <asp:HiddenField ID="Template" Value='<%# Bind("Template")%>' runat="server" />
                    </td>
                    <td class="Status">
                        <asp:DropDownList ID="ddlStatus" runat="server">
                            <asp:ListItem Text="启用" Value="1" />
                            <asp:ListItem Text="禁用" Value="2" />
                        </asp:DropDownList>
                        <asp:HiddenField ID="Status" Value='<%# Bind("Status")%>' runat="server" />
                    </td>
                    <td class="Operation">
                        <asp:Button runat="server" ID="btnupdate" CssClass="btn" CommandName="Update" Text="保存" />
                        <asp:Button runat="server" ID="btnCancel" CssClass="btn" CommandName="Cancel" Text="取消" />

                    </td>
                </tr>
            </EditItemTemplate>--%>
            <LayoutTemplate>

                <table class="D_form" id="itemPlaceholderContainer">
                    <tr class="trth">
                        <th class="Account">编号
                        </th>
                        <th class="Account">名称
                        </th>
                        <th class="Account">类型
                        </th>
                       <%-- <th class="Account">状态
                        </th>--%>
                    </tr>
                    <tr id="itemPlaceholder" runat="server"></tr>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr class="Single">
                    <td class="Account">
                        <%#Eval("Count")%>
                    </td>
                    <td class="Account">
                        <%#Eval("Title")%>
                    </td>
                    <td class="Account">
                        <%#Eval("QTypeShow")%>
                    </td>
                 <%--   <td class="Account">
                        <%#Eval("TemplateShow")%>
                    </td>--%>
                   <%-- <td class="Account">
                        <%#Eval("StatusShow")%>
                    </td>--%>
                 <%--   <td class="Operation">
                        <a href="javascript:void(0);" onclick="UpdateObj('<%# Eval("ID") %>');" style="color:blue;">修改</a>
                        <asp:LinkButton ID="btnEdit" BorderStyle="None" CommandName="Edit" CommandArgument='<%# Eval("ID") %>' runat="server"><i class="iconfont">&#xe629;</i></asp:LinkButton>
                        <asp:LinkButton ID="btndelete" BorderStyle="None" CommandName="del" CommandArgument='<%# Eval("ID") %>' runat="server" OnClientClick="return confirm('你确定移除吗？')"><i class="iconfont">&#xe64c;</i></asp:LinkButton>
                    </td>--%>
                </tr>
            </ItemTemplate>
         <%--   <InsertItemTemplate>
                <tr class="Single">
                    <td class="Account"></td>
                    <td class="Account">
                        <asp:TextBox ID="txtTitle" runat="server" CssClass="edit" Text='<%# Bind("Title")%>'></asp:TextBox>
                    </td>
                    <td class="MarkType">
                        <asp:DropDownList ID="ddlMarkType" runat="server">
                            <asp:ListItem Text="主观" Value="1" />
                            <asp:ListItem Text="客观" Value="2" />
                        </asp:DropDownList>
                    </td>
                    <td class="Template">
                        <asp:DropDownList ID="ddlTemplate" runat="server">
                            <asp:ListItem Text="单选" Value="1" />
                            <asp:ListItem Text="多选" Value="2" />
                            <asp:ListItem Text="判断" Value="3" />
                            <asp:ListItem Text="文本框" Value="4" />
                        </asp:DropDownList>
                    </td>
                    <td class="Status">
                        <asp:DropDownList ID="ddlStatus" runat="server">
                            <asp:ListItem Text="启用" Value="1" />
                            <asp:ListItem Text="禁用" Value="2" />
                        </asp:DropDownList>
                    </td>
                    <td class="Operation">
                        <asp:Button ID="InsertButton" runat="server" CssClass="btn" CommandName="Insert" Text="插入" />
                    </td>
                </tr>
            </InsertItemTemplate>--%>
        </asp:ListView>
    </div>
</div>
