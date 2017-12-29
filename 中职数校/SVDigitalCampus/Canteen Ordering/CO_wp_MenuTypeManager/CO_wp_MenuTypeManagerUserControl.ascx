<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CO_wp_MenuTypeManagerUserControl.ascx.cs" Inherits="SVDigitalCampus.Canteen_Ordering.CO_wp_MenuTypeManager.CO_wp_MenuTypeManagerUserControl" %>
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/Ordercommd.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/common.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/iconfont.css" rel="stylesheet" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/animate.css" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" rel="stylesheet" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/popwin.js"></script>
<script type="text/javascript">
    function close() {
        //alert("1111");
        //$("#mask,#maskTop", parent.document).fadeOut(function () {
        //    $(this).remove();

        //});
        //document.parentWindow.location.reload();
        parent.location.reload(true);
    }
    function UpdateObj(TypeID) {
        if (TypeID == null) {

            alert("请选择一条记录！");
            return false;

        }
        //$.dialog({ id: 'Menu', title: '修改菜品', content: 'url:UpdateMenu.aspx?MenuId=' + MenuID, width: 700, height: 500, lock: true, max: false, min: false });
        var parent = this; popWin.showWin("400", "200", "编辑菜品分类", '<%=SietUrl%>' + "UpdateMenuType.aspx?TypeID=" + TypeID);
    }
    function AddmenuType() {
        popWin.showWin("400", "200", "添加菜品分类", '<%=SietUrl%>' + "AddMenuType.aspx");
    }
</script>
<%--<input type="button" id="add" value="录入菜品分类" onclick="AddmenuType();" />--%>
<div class="MenuType_information_SchoolLibrary">
    <div class="MenuType_form">
        <asp:ListView ID="lvMenuType" runat="server" OnItemCommand="lvMenuType_ItemCommand" OnItemCanceling="lvMenuType_ItemCanceling" OnItemInserting="lvMenuType_ItemInserting" OnItemUpdating="lvMenuType_ItemUpdating" EnableModelValidation="True"
            InsertItemPosition="LastItem" DataKeyNames="ID" OnItemEditing="lvMenuType_ItemEditing">
            <EmptyDataTemplate>

                <table class="D_form">
                    <tr class="trth">
                        <td colspan="3" style="text-align: center">亲，暂无菜品分类记录！
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
                    <td class="Operation">
                        <%--<a href="javascript:void(0);" onclick="UpdateObj('<%# Eval("ID") %>');" style="color:blue;">修改</a>--%>
                       <asp:LinkButton ID="btnEdit"  CommandName="Edit" CommandArgument='<%# Eval("ID") %>'  runat="server"  ><i class="iconfont">&#xe629;</i></asp:LinkButton>
                         <asp:LinkButton ID="btndelete"  CommandName="del" CommandArgument='<%# Eval("ID") %>'  runat="server" OnClientClick="return confirm('你确定移除吗？')"  ><i class="iconfont">&#xe64c;</i></asp:LinkButton>

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
                    <td class="Operation">
                        <asp:Button ID="InsertButton" runat="server" CssClass="btn" CommandName="Insert" Text="插入" />
                    </td>
                </tr>
            </InsertItemTemplate>
        </asp:ListView>
    </div>
</div>
