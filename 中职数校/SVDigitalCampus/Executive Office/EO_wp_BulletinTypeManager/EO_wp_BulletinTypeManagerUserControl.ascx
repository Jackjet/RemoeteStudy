<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EO_wp_BulletinTypeManagerUserControl.ascx.cs" Inherits="SVDigitalCampus.Executive_Office.EO_wp_BulletinTypeManager.EO_wp_BulletinTypeManagerUserControl" %>
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/common.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/iconfont.css" rel="stylesheet" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/animate.css" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" rel="stylesheet" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/popwin.js"></script>
<script type="text/javascript">
    function close() {
        //$("#mask,#maskTop", parent.document).fadeOut(function () {
        //    $(this).remove();

        //});
        //document.parentWindow.location.reload();
        parent.location.reload(true);
    }
</script>
<div class="MenuType_information_SchoolLibrary">
    <div class="MenuType_form">
        <asp:ListView ID="lvBulletinType" runat="server" OnItemCommand="lvBulletinType_ItemCommand" OnItemCanceling="lvBulletinType_ItemCanceling" OnItemInserting="lvBulletinType_ItemInserting" OnItemUpdating="lvBulletinType_ItemUpdating" EnableModelValidation="True"
            InsertItemPosition="LastItem" DataKeyNames="ID" OnItemEditing="lvBulletinType_ItemEditing" OnPreRender="lvBulletinType_PreRender" OnItemDataBound="lvBulletinType_ItemDataBound" >
            <EmptyDataTemplate>
                <table class="D_form">
                    <tr class="trth">
                        <td colspan="4" style="text-align: center">亲，暂无公告分类记录！
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
                        <%#Eval("TopTitle")%>
                    </td>
                    <td class="Operation">
                        <%--<a href="javascript:void(0);" onclick="UpdateObj('<%# Eval("ID") %>');" style="color:blue;">修改</a>--%>
                       <asp:LinkButton ID="btnEdit"  CommandName="Edit" CommandArgument='<%# Eval("ID")%>'  runat="server"  ><i class="iconfont">&#xe629;</i></asp:LinkButton>
                         <asp:LinkButton ID="btndelete"  CommandName="del" CommandArgument='<%# Eval("ID")%>'  runat="server" OnClientClick="return confirm('你确定移除吗？')"  ><i class="iconfont">&#xe64c;</i></asp:LinkButton>

                    </td>
                </tr>
            </AlternatingItemTemplate>
            <EditItemTemplate>
                <tr class="Single">

                    <td class="Account">
                        <%# Eval("Count")%>
                    </td>
                    <td class="Account">
                        <asp:TextBox ID="txtTitle" CssClass="edit"  runat="server" Text='<%# Bind("Title")%>'></asp:TextBox>
                    </td>
                    <td class="Account">
                        <asp:DropDownList ID="ddlTop" runat="server"  ></asp:DropDownList>
                        <asp:HiddenField ID="TopID" runat="server" Value='<%# Bind("TopID")%>'/>
                    </td>
                    <td class="Operation">
                        <asp:Button runat="server" ID="btnupdate"  CssClass="btn" CommandName="Update" Text="保存" />
                        <asp:Button runat="server" ID="btnCancel" CssClass="btn" CommandName="Cancel" Text="取消" />

                    </td>
                </tr>
            </EditItemTemplate>
            <LayoutTemplate>

                <table class="D_form" id="itemPlaceholderContainer">
                    <tr class="trth">
                        <th class="Account">编号
                        </th>
                        <th class="Account">类型
                        </th>
                        <th class="Account">上级分类
                        </th>
                        <th class="Operation">操作列
                        </th>
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
                        <asp:HiddenField ID="htop" runat="server" Value='<%#Bind ("TopID") %>'/>
                        <%#Eval("TopTitle")%>
                    </td>
                    <td class="Operation">
                        <%--<a href="javascript:void(0);" onclick="UpdateObj('<%# Eval("ID") %>');" style="color:blue;">修改</a>--%>
                          <asp:LinkButton ID="btnEdit" BorderStyle="None" CommandName="Edit" CommandArgument='<%# Eval("ID") %>'  runat="server"  ><i class="iconfont">&#xe629;</i></asp:LinkButton>
                         <asp:LinkButton ID="btndelete" BorderStyle="None" CommandName="del" CommandArgument='<%# Eval("ID") %>'  runat="server" OnClientClick="return confirm('你确定移除吗？')" ><i class="iconfont">&#xe64c;</i></asp:LinkButton>
                    </td>
                </tr>
            </ItemTemplate>
            <InsertItemTemplate>
                <tr class="Single">
                    <td class="Account"></td>
                    <td class="Account">
                        <asp:TextBox ID="txtTitle" runat="server" CssClass="edit" Text='<%# Bind("Title")%>'></asp:TextBox>
                    </td>
                    <td class="Account">
                          <asp:DropDownList ID="addlTop" runat="server"></asp:DropDownList>
                    </td>
                    <td class="Operation">
                        <asp:Button ID="InsertButton" runat="server" CssClass="btn" CommandName="Insert" Text="插入" />
                    </td>
                </tr>
            </InsertItemTemplate>
        </asp:ListView>
    </div>
</div>
