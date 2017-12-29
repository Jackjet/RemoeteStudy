<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectCurriculum.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.SelectCurriculum"  %>

<html>
<head id="Head1" runat="server">
    <meta http-equiv="expires" content="0"/>
	<meta http-equiv="pragma" content="no-cache"/>
	<base target="_self" />
    <title>选择课程</title>
    <link href="css/Progressbar.css" rel="stylesheet" />
         <link href="css/edit.css" rel="stylesheet" />
    <link href="css/type.css" rel="stylesheet" />
<script src="js/Jquery.easyui/jquery.min.js"></script>
    <link href="js/Jquery.easyui/themes/default/easyui.css" rel="stylesheet" />
<script src="js/Jquery.easyui/jquery.easyui.min.js"></script>
      <script src="js/layer/layer.js"></script>
      <script src="js/layer/OpenLayer.js"></script>

    <script>
        function confirm() {
            var id = "";
            var name = "";
            $("input[name='Curriculum']:checked").each(function () {
                id = id + this.value + ";";
                name = name + $(this).parent().next().next().html() + ";";
            });
            if (id == ""||id==null)
            {
            }
            var organization = new Object();

            organization.Id = id;
            organization.Name = name;

            window.returnValue = organization;
            OL_CloseLayerIframe(organization);
        }

        function confirmClose()
        {
            OL_CloseLayerIframe();
        }
    </script>
        <style>
      .main_list_table {
    width: 635px;
    margin: 16px;
    border: 1px solid #dedede;
    }
      .main_list_table .list_table {
    width: 635px;
    }


    </style>
</head>
<body>
<form id="Form" runat="server">
    <table class="tableEdit">
      <tr>
        <td><span style="font-size:12px;">课程分类&nbsp;</span></td>
        <td><asp:TextBox ID="ResourceName" runat ="server"  CssClass="inputPart"></asp:TextBox></td>
        <td><span style="font-size:12px;">课程名称&nbsp;</span></td>
        <td><asp:TextBox ID="CurriculumName" runat ="server" CssClass="inputPart"></asp:TextBox></td>
       
        <td  style="text-align:right;"> <asp:Button ID="searchButton" CssClass="button_s"  runat="server" Text="查询"  OnClick="BTSearch_Click"/></td>
      </tr>
    </table>
      <div id="main"  class="main_list_table">
          <asp:Repeater ID="RepeaterCurriculum" runat="server">
              <HeaderTemplate>
                <table  id="tab" class="list_table" cellspacing="0" cellpadding="0" border="0" >
		        <thead>
                         <tr class="tab_th top_th">
			        <th class="th_tit">选择</th>
			        <th  class="th_tit">序号</th>
                    <th class="th_tit_left">课程名称</th>
			        <th class="th_tit_left">课程分类</th>
                             </tr>
                </thead>
            </HeaderTemplate>
               <ItemTemplate>
                   <tr class="tab_td">
                       <td  class="td_tit"><input type="checkbox" name="Curriculum"  value="<%# Eval("Id")%>"/></td>
                       <td class="td_tit"><%# Container.ItemIndex + 1 + (this.AspNetPageCurriculum.CurrentPageIndex -1)*this.AspNetPageCurriculum.PageSize %></td>
                       <td class="td_tit_left"><%# Eval("Title")%></td>
                       <td class="td_tit_left"><%# Eval("ResourceName")%></td>
                   </tr>
                   </ItemTemplate>
              <AlternatingItemTemplate>
                  <tr class="tab_td td_bg">
                       <td  class="td_tit"><input type="checkbox" name="Curriculum"  value="<%# Eval("Id")%>"/></td>
                       <td class="td_tit"><%# Container.ItemIndex + 1 + (this.AspNetPageCurriculum.CurrentPageIndex -1)*this.AspNetPageCurriculum.PageSize %></td>
                       <td class="td_tit_left"><%# Eval("Title")%></td>
                       <td class="td_tit_left"><%# Eval("ResourceName")%></td>
                   </tr>
              </AlternatingItemTemplate>
              <FooterTemplate>
                  </table>
              </FooterTemplate>
              </asp:Repeater>
         
      </div>
       <div style="text-align:center;"> 
    <div id="containerdiv" style="overflow: hidden; display: inline-block;">
                               <webdiyer:AspNetPager ID="AspNetPageCurriculum" CssClass="paginator" ShowFirstLast="false" PrevPageText="<<上一页" NextPageText="下一页>>"  runat="server" Width="100%"  CurrentPageButtonClass="paginatorPn" AlwaysShow="true"  HorizontalAlign="center" PageSize="2" OnPageChanged="AspNetPageCurriculum_PageChanged">
        </webdiyer:AspNetPager>
         </div>
                     </div>
       <table style="width:100%;text-align:center;">
    <tr>
        <td style="vertical-align:top;">
           
            <span >
                <input type="button" value="确定" onclick="confirm()"  class="button_s" />
                <input type="button" value="取消" onclick="confirmClose();"  class="button_n" />
            </span>
        </td>
    </tr>
</table>
</form>
</body>
</html>