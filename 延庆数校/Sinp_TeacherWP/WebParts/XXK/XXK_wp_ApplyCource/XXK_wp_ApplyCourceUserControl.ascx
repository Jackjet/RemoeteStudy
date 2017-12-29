<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="XXK_wp_ApplyCourceUserControl.ascx.cs" Inherits="Sinp_TeacherWP.WebParts.XXK.XXK_wp_ApplyCource.XXK_wp_ApplyCourceUserControl" %>
<script src="../../../../_layouts/15/Script/jquery-1.8.0.js"></script>
<link href="../../../../_layouts/15/Style/tablelist.css" rel="stylesheet" />
<link href="../../../../_layouts/15/Style/common.css" rel="stylesheet" />
<link href="../../../../_layouts/15/Style/layout.css" rel="stylesheet" />
<script src="../../../../_layouts/15/Script/uploadFile1.js"></script>
<script type="text/javascript">
    function ShowImage(url) { //展示图片        
        document.getElementById("<%=img_Pic.ClientID%>").src = url;
    }

</script>
<style type="text/css">
       .img {
        border: 1px solid #dcdcde;
        width: 160px;
        height: 120px;
    }
</style>
<div class="listdv">
    <table>
        <tr>
            <th style="width: 5%">所申请选修课</th>
            <td>
                <asp:Label ID="lbTitle" runat="server" Text=""></asp:Label>
            </td>
            <td rowspan="4" style="text-align: center; vertical-align: central;" colspan="2">
                <asp:Image ID="img_Pic" CssClass="img" runat="server" ImageUrl="../../../../_layouts/15/Style/choosen/chosen-sprite.png"/>
                <input type="file" id="fimg_Asso" name="fimg_Asso" runat="server" onchange="ShowImage(this.value);" style="width: 120px; display: none;" />
                <a href="#" onclick="$('#<%=fimg_Asso.ClientID%>').click();" style="display: block; color: blue;">更换图片</a>
            </td>
        </tr>
        <tr>
            <th>学年学期：</th>
            <td>
                <asp:Label ID="lbSection" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;上课年级：<asp:Label ID="lbGrade" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;上课周次：<asp:Label ID="lbWeekN" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <th>课程名称：</th>
            <td>
                <asp:TextBox ID="CourceName" runat="server"></asp:TextBox>
                <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_Title" runat="server" ControlToValidate="CourceName"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
            </td>

        </tr>
        <tr>
            <th>课程类别：</th>
            <td>
                <asp:DropDownList ID="ddGatogry" runat="server"></asp:DropDownList>
                <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddGatogry"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
            </td>
        </tr>
        <tr>
            <th>上课时间：</th>
            <td>
                <asp:TextBox ID="TTime" runat="server"></asp:TextBox>
                <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TTime"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
            </td>

            <th>上课地点：</th>
            <td>
                <asp:TextBox ID="TAddress" CssClass="Wdate" onclick="javascript: WdatePicker()" runat="server"></asp:TextBox>

                <span style="color: red;">
                    <asp:RequiredFieldValidator ID="RFV_StartTime" runat="server" ControlToValidate="TAddress"
                        ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
            </td>
        </tr>
        <tr>
            <th>学生范围：</th>
            <td>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox1"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
            </td>

            <th>人数上限：</th>
            <td>
                <asp:TextBox ID="MaxNum" runat="server"></asp:TextBox>

                <span style="color: red;">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="MaxNum"
                        ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
            </td>
        </tr>
        <tr>
            <th>课程简介：</th>
            <td colspan="3">

                <textarea id="Introduc" cols="20" rows="2" runat="server"></textarea>
            </td>
        </tr>
        <tr>
            <th>课程目标：</th>
            <td colspan="3">
                <textarea id="Target" cols="20" rows="2" runat="server"></textarea>
            </td>
        </tr>
        <tr>
            <th>硬件要求：</th>
            <td colspan="3">

                <textarea id="Hardware" cols="20" rows="2" runat="server"></textarea>
            </td>
        </tr>
        <tr>
            <th>课程评价标准：</th>
            <td colspan="3">
                <textarea id="Evaluate" cols="20" rows="2" runat="server"></textarea>
            </td>
        </tr>
        <tr>
            <th></th>
            <td colspan="2">
                <asp:Button ID="Btn_InfoSave" OnClick="Btn_InfoSave_Click" CssClass="save" runat="server" ValidationGroup="ProjectSubmit" Text="保存" />
                <input type="button" class="cancel" value="取消" onclick="parent.closePages();" />

            </td>
        </tr>
    </table>
</div>
