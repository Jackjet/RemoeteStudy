<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkGuanLi.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.WorkGuanLi" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
        <link href="css/type.css" rel="stylesheet" />
    <link href="css/Progressbar.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
       <asp:HiddenField ID="HDChapterID" runat="server" />
    <div class="main_list_table">
<table class="list_table" cellspacing="0" cellpadding="0" border="0">
        <tr class="tab_th top_th">
            <th class="th_tit">序号</th>
            <th class="th_tit">班级</th>
            <th class="th_tit">姓名</th>
            <th class="th_tit">性别</th>
            <th class="th_tit" style="width:170px;">学习进度</th>
            <th class="th_tit" style="width:170px;">作业进度</th>
            <th class="th_tit" style="width:170px;">批改进度</th>
            <th class="th_tit">作业管理</th>
        </tr>
                        
                <asp:Repeater ID="RepeaterList" runat="server">
            <ItemTemplate>
                <td class="td_tit">
<%# Container.ItemIndex + 1 + (this.AspNetPageCurriculum.CurrentPageIndex -1)*this.AspNetPageCurriculum.PageSize %>
              </td>
              <td class="td_tit"><%#Eval("ClassName") %></td>
              <td class="td_tit"><%#Eval("Name") %></td>
              <td class="td_tit"><%#Eval("Sex") %></td>
              <td class="td_tit"><div class="progress" >
                       <span class="green" style="width:<%#Eval("Percentage") %>%;"><span><%#Eval("Percentage") %>%</span></span>
    </div></td>
        <td class="td_tit"><div class="progress" >
                       <span class="green" style="width:<%#Eval("Worktage") %>%;"><span><%#Eval("Worktage") %>%</span></span>
    </div></td>
                     <td class="td_tit"><div class="progress" >
                       <span class="green" style="width:<%#Eval("Hometage") %>%;"><span><%#Eval("Hometage") %>%</span></span>
    </div></td>
              <td class="td_tit"><a href="<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/ChapterWorkList.aspx?CurriculumID=<%=CurriculumID%>&UserID=<%#Eval("Id") %>" target="_self">查看作业</a></td>
                    </tr>
                </ItemTemplate>
                           <AlternatingItemTemplate>
                               <tr class="tab_td td_bg">
                                   <td class="td_tit">
<%# Container.ItemIndex + 1 + (this.AspNetPageCurriculum.CurrentPageIndex -1)*this.AspNetPageCurriculum.PageSize %>
              </td>
              <td class="td_tit"><%#Eval("ClassName") %></td>
              <td class="td_tit"><%#Eval("Name") %></td>
              <td class="td_tit"><%#Eval("Sex") %></td>
              <td class="td_tit">                    <div class="progress" >
                       <span class="green" style="width:<%#Eval("Percentage") %>%;"><span><%#Eval("Percentage") %>%</span></span>
    </div></td>
                                           <td class="td_tit"><div class="progress" >
                       <span class="green" style="width:<%#Eval("Worktage") %>%;"><span><%#Eval("Worktage") %>%</span></span>
    </div></td>
                                        <td class="td_tit"><div class="progress" >
                       <span class="green" style="width:<%#Eval("Hometage") %>%;"><span><%#Eval("Hometage") %>%</span></span>
    </div></td>
              <td class="td_tit"><a href="<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/ChapterWorkList.aspx?CurriculumID=<%=CurriculumID%>&UserID=<%#Eval("Id") %>" target="_self">查看作业</a></td>
                    </tr>
                               </AlternatingItemTemplate>
                </asp:Repeater>
                 
</table>       
      <div style="text-align:center;"> 
    <div id="" style="overflow: hidden; display: inline-block;">
                               <webdiyer:AspNetPager ID="AspNetPageCurriculum" CssClass="paginator" ShowFirstLast="false" PrevPageText="<<上一页" NextPageText="下一页>>"  runat="server" Width="100%"  CurrentPageButtonClass="paginatorPn" AlwaysShow="true"  HorizontalAlign="center" PageSize="2" OnPageChanged="AspNetPageCurriculum_PageChanged">
        </webdiyer:AspNetPager>
         </div>
                     </div>                   
</div>
<div style="width:801px;margin-left:10px">已上传:<asp:Label runat="server" ID="lb_ysc"/>&nbsp;&nbsp;
                        未上传:<asp:Label runat="server" ID="lb_wsc"/>&nbsp;&nbsp;            
                        优秀:<asp:Label runat="server" ID="lb_youxiu"/>&nbsp;&nbsp;
                        优秀率:<asp:Label runat="server" ID="lb_yl"/>
</div>
<div class="main_list_table">
<table class="list_table" cellspacing="0" cellpadding="0" border="0">
        <tr class="tab_th top_th">
            <th class="th_tit">姓名</th>
            <th class="th_tit">章节序号</th>
            <th class="th_tit">章节名称</th>
            <th class="th_tit">作业分数</th>
            <th class="th_tit">是否优秀作业</th>
            <%--<th class="th_tit">操作</th>--%>
        </tr>
                        
                <asp:Repeater ID="RepeaterHomeWork" runat="server">
            <ItemTemplate>
              <td class="td_tit"><%#Eval("UserID") %></td>
              <td class="td_tit"><%#Eval("ChapterNum") %></td>
              <td class="td_tit"><%#Eval("ChapterName") %></td>
              <td class="td_tit"><%#Eval("Score") %></td>
              <td class="td_tit"><%#Eval("IsExcellentWork") %></td>
            <%--<td class="td_tit">
                <a title="批改作业" onclick="PiGaiHomeWork('<%#Eval("WorkUrl") %>')" target="_self"><img class="EditImg" /></a>
            </td>--%>
                    </tr>
                </ItemTemplate>
                           <AlternatingItemTemplate>
                               <tr class="tab_td td_bg">
              <td class="td_tit"><%#Eval("UserID") %></td>
               <td class="td_tit"><%#Eval("ChapterNum") %></td>
               <td class="td_tit"><%#Eval("ChapterName") %></td>
              <td class="td_tit"><%#Eval("Score") %></td>
              <td class="td_tit"><%#Eval("IsExcellentWork") %></td>
            <%--<td class="td_tit">
                <a title="批改作业" onclick="PiGaiHomeWork('<%#Eval("WorkUrl") %>')" target="_self"><img class="EditImg" />
                </a></td>--%>
                    </tr>
                               </AlternatingItemTemplate>
                </asp:Repeater>
                 
</table>
      <div style="text-align:center;"> 
    <div id="" style="overflow: hidden; display: inline-block;">
        <webdiyer:AspNetPager ID="AspNetPagerHomeWork" CssClass="paginator" ShowFirstLast="false" PrevPageText="<<上一页" NextPageText="下一页>>"  runat="server" Width="100%"  CurrentPageButtonClass="paginatorPn" AlwaysShow="true"  HorizontalAlign="center" PageSize="2" OnPageChanged="AspNetPagerHomeWork_PageChanged">
        </webdiyer:AspNetPager>
         </div>
   </div>   
</div>
    <table width="100%">
        <tr>
            <td align="center">
              <input type="button" value="返回" onclick="history.go(-1);"  class="button_n"/>
            </td>
        </tr>
    </table>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
</asp:Content>
