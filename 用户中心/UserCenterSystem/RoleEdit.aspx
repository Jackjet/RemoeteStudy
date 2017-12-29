<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleEdit.aspx.cs" Inherits="UserCenterSystem.RoleEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
    <link type="text/css" href="css/page.css" rel="stylesheet" />
<body>
    <form id="form1" runat="server">
    <div style="margin-left:100px">
                  <div id="editpart" class="editpart">
            <table style="width:100%;">
            <tr>
                <td>
                <asp:Label ID="lb_RoleName" runat="server" Text="角色名称："></asp:Label> 
                    </td>
                <td><span style="color:Red;">*</span>
                <asp:TextBox ID="tb_RoleName" runat="server"></asp:TextBox>
               </td>
                </tr>
                <tr>
          <td> <asp:Label ID="Label1" runat="server" Text="所在学校："></asp:Label> 
              </td>
                    <td><span style="color:Red;">*</span>
         <asp:DropDownList ID="dp_DepartMent" runat="server" AutoPostBack="True" Width="140px">
                <asp:ListItem Value="" Selected="True" >-学校-</asp:ListItem>
                
            </asp:DropDownList>
                        </td>
       </tr>
                <tr>
                    <td>
                <asp:Label ID="lb_Remark" runat="server" Text="备注："></asp:Label>  </td>
                    <td>
                <asp:TextBox ID="tb_Remark" runat="server" MaxLength="60" Rows="5" TextMode="MultiLine" Width="360px"></asp:TextBox>
                        </td>
        </tr>
                <tr>
                    <td colspan="2">
            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="保存"  class="btnsave mr40"/>
            &nbsp;
          <input  type="button" onclick="javascript: history.back(1);" value="取消" class="btn_concel" />
                        </td>
            </tr>
                </table>
    </div>
    </form>
</body>
</html>
