<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserManage.aspx.cs" Inherits="UserCenterSystem.UserManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <link type="text/css" href="css/page.css" rel="stylesheet" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>学生管理中心</title>
</head>
<body>
         <form id="form1" runat="server">
      <table >
        <colgroup>
            <col width="1100px" valign="top" />
            <col width="80%" />
        </colgroup>
         <tr>
            <td colspan="2" >
                <div>
                     
                </div>
            </td>
        </tr>
        <tr>
            <td valign="top" style="vertical-align: top;width:15%;">
                <!-- Add a <div> element where the tree should appear: -->
                 <div id="TreeDivHead">
                   <%-- <div>
                        <asp:DropDownList ID="dp_DepartMent" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dp_DepartMent_SelectedIndexChanged"> 
                            
                            <asp:ListItem Value="110229100001"  >-延庆一中-</asp:ListItem>
                            <asp:ListItem Value="110229100009"  >-延庆十一中-</asp:ListItem>
                        </asp:DropDownList>
                    </div>--%>
                    <div ></div>
                    <div >
                        <asp:TreeView ID="tvOU" runat="server" NodeWrap="True" SelectedNodeStyle-BackColor="#99ccff"  ShowLines="True" >
          </asp:TreeView>
                    </div>
                </div>
                
            </td>
           
            <td valign="top" style="vertical-align: top;">
                <iframe src="StudentList.aspx?xxzzjgh=<%=strDepartMentID %>" name="contentFrame" id="contentFrame" width="100%" height="1200"   scrolling="no" marginheight="0"  marginwidth="0" frameborder="0" allowtransparency="true"></iframe>

            </td>
        </tr>
    </table>
               </form>
</body>
</html>
