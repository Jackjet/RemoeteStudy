<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChapterWorkList.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.ChapterWorkList" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
            <link href="css/type.css" rel="stylesheet" />
        <link href="css/Progressbar.css" rel="stylesheet" />
          <script>

              function PiGaiHomeWork(Url) {
                  if (Url == "" || Url == null) {
                      LayerAlert("学员还未上传学习心得,不能进行批改!!!");
                      return;
                  }
                  else {
                      OL_ShowLayer(2, "批改作业", 600, 320, Url, function (returnVal) {
                      });
                  }
              }
          </script>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
        <div class="main_list_table">
<table class="list_table" cellspacing="0" cellpadding="0" border="0">
        <tr class="tab_th top_th">
            <th class="th_tit">知识点序号</th>
            <th class="th_tit">知识点名称</th>
            <th class="th_tit">作业分数</th>
            <th class="th_tit">是否优秀作业</th>
            <th class="th_tit">学习状态</th>
            <th class="th_tit">作业状态</th>
            <th class="th_tit">批改状态</th>
            <th class="th_tit">操作</th>
        </tr>
                        
                <asp:Repeater ID="RepeaterList" runat="server">
            <ItemTemplate>
              <td class="td_tit"><%#Eval("ChapterNum") %></td>
              <td class="td_tit"><%#Eval("ChapterName") %></td>
              <td class="td_tit"><%#Eval("Score") %></td>
              <td class="td_tit"><%#Eval("IsExcellentWork") %></td>
                 <td class="td_tit"><%#Eval("XueXiStater") %></td>
              <td class="td_tit"><%#Eval("WorkStater") %></td>
                 <td class="td_tit"><%#Eval("PiGaiStater") %></td>
            <td class="td_tit">
                <a title="批改作业" onclick="PiGaiHomeWork('<%#Eval("WorkUrl") %>')" target="_self"><img class="EditImg" /></a>
            </td>
                    </tr>
                </ItemTemplate>
                           <AlternatingItemTemplate>
                               <tr class="tab_td td_bg">
               <td class="td_tit"><%#Eval("ChapterNum") %></td>
               <td class="td_tit"><%#Eval("ChapterName") %></td>
              <td class="td_tit"><%#Eval("Score") %></td>
              <td class="td_tit"><%#Eval("IsExcellentWork") %></td>
               <td class="td_tit"><%#Eval("XueXiStater") %></td>
               <td class="td_tit"><%#Eval("WorkStater") %></td>
               <td class="td_tit"><%#Eval("PiGaiStater") %></td>
            <td class="td_tit">
                <a title="批改作业" onclick="PiGaiHomeWork('<%#Eval("WorkUrl") %>')" target="_self"><img class="EditImg" />
                </a></td>
                    </tr>
                               </AlternatingItemTemplate>
                </asp:Repeater>
                 
</table>
                                 
</div>
      <div style="text-align:center;"> 
    <div id="containerdiv" style="overflow: hidden; display: inline-block;">
                               <webdiyer:AspNetPager ID="AspNetPageCurriculum" CssClass="paginator" ShowFirstLast="false" PrevPageText="<<上一页" NextPageText="下一页>>"  runat="server" Width="100%"  CurrentPageButtonClass="paginatorPn" AlwaysShow="true"  HorizontalAlign="center" PageSize="2" OnPageChanged="AspNetPageCurriculum_PageChanged">
        </webdiyer:AspNetPager>
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
