<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StuDivideClass.aspx.cs" Inherits="UserCenterSystem.StuDivideClass" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link type="text/css" href="css/page.css" rel="stylesheet" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
          <link type="text/css" href="css/page.css" rel="stylesheet" />
<script src="Scripts/jquery-1.6.min.js"></script>
  
<body>
    <form id="form1" runat="server">
        <div style="margin-left:30px">
           <table style=" text-align:center;width:100%">
               <tr>
                   <td>
            学校：
                       </td>
                   <td><asp:DropDownList ID="dp_DepartMent" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dp_DepartMent_SelectedIndexChanged" width="100px">
               
            </asp:DropDownList>
                       </td>
                   </tr>
              <tr>
                  <td>
            年级：</td>
                  <td><asp:DropDownList ID="dp_Grades" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dp_GradesIndexChanged" width="100px">
                <asp:ListItem Value="" Selected="True">-年级-</asp:ListItem>
            </asp:DropDownList>
                      </td>
                  </tr>
                <tr>
                    <td>
            班级：</td>
                    <td><asp:DropDownList ID="dp_Class" runat="server"  AutoPostBack="True" width="100px">
                <asp:ListItem Value="" Selected="True">-班级-</asp:ListItem>
            </asp:DropDownList>
                        </td>
               </tr>

            <tr >
                <td colspan="2" >
                     <asp:Button ID="btnDivide" runat="server" onclientclick="return CheckSelected();" OnClick="btnDivide_Click" Text="确定"  class="btnsave mr40"/>
                    <input  type="button" class="wBox_close on btn_concel"  value="取消"/>
                    </td>
                
                </tr>
               <tr>

                   <td colspan="2" style="color:red">
                       <br />
               用户提示：<br />
               根据选择的学校、年级、班级进行分班<br />
              
                   </td>
               </tr>
              </table>
           

            


           
             <asp:HiddenField ID="hiddenSfzjhm" runat="server" />
            <asp:HiddenField ID="hiddenschool" runat="server" />
            <asp:HiddenField ID="hiddenGrade" runat="server" />
            <asp:HiddenField ID="hiddenClass" runat="server" />

            


        </div>
    </form>
</body>
</html>
<script type="text/javascript">
    //判断是否选择学校
    function CheckSelected() {

        if ($("#dp_DepartMent option:selected").val() == "") {
            alert("请选择学校！");
            return false;
        }


        if ($("#dp_Grades option:selected").val() == "") {
            alert("请选择年级！");
            return false;
        }


        if ($("#dp_Class option:selected").val() == "") {
            alert("请选择班级！");
            return false;
        }
    }
</script>
