<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChapterGuanLi.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.ChapterGuanLi" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <link href="css/type.css" rel="stylesheet" />
    <script type="text/javascript">
        function CreaterChapter() {
            var url = "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/ChapterNew.aspx?CurriculumID=" + document.getElementById("<%=this.HDCurriculumID.ClientID %>").value;
            window.location.href = url;
            return false;
        }
        function DeleteChapter(id) {
            // LayerAlert('不能删除角色!!');
            LayerConfirmDelete("确定删除此知识点吗?", function () {
                var postData = { DeleteID: id };
                var loadIndex = layer.load(2);
                $.ajax({
                    type: "POST",
                    url: "ChapterEdit.aspx",
                    data: postData,
                    dataType: "text",
                    success: function (returnVal) {
                        layer.close(loadIndex);
                        if (returnVal != "" && returnVal != "undefined") {
                            if (returnVal == "ok") {
                                LayerAlert("删除成功!!!", function () {
                                    window.location.href = window.location.href;
                                });
                            }
                            else {

                                LayerAlert(returnVal);
                            }
                        }
                    },
                    error: function (errMsg) {
                        LayerAlert('删除失败！！');
                    }
                })
            });
        }
 </script>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
          <asp:HiddenField ID="HDCurriculumID" runat="server" />
<div class="main_list_table">
<table class="list_table" cellspacing="0" cellpadding="0" border="0">
        <tr class="tab_th top_th">
            <th class="th_tit">知识点序号</th>
            <th class="th_tit_left">知识点名称</th>
            <th class="th_tit">操作</th>
        </tr>
                        
                <asp:Repeater ID="RepeaterList" runat="server">
            <ItemTemplate>
                <tr class="tab_td">
               <td class="td_tit">
              第<%#Eval("SerialNumber") %>知识点
              </td>
              <td class="td_tit_left"><%#Eval("Title") %></td>
              <td class="td_tit"><a title="编辑知识点" href="<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/ChapterEdit.aspx?id=<%#Eval("Id")%>" target="_self"><img  class="EditImg" /></a><a title="删除知识点" onclick="DeleteChapter('<%#Eval("Id") %>')"><img class="DelImg"  /></a></td>
                    </tr>
                </ItemTemplate>
                           <AlternatingItemTemplate>
                               <tr class="tab_td td_bg">
               <td class="td_tit">
              第<%#Eval("SerialNumber") %>章
              </td>

              <td class="td_tit_left"><%#Eval("Title") %></td>
              <td class="td_tit"><a title="编辑知识点" href="<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/ChapterEdit.aspx?id=<%#Eval("Id")%>" target="_self"><img class="EditImg" /></a><a title="删除知识点" onclick="DeleteChapter('<%#Eval("Id") %>')"><img class="DelImg" /></a></td>
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
     <div class="main_button_part">
          <asp:Button ID="BTAdd" runat="server" Text="添加知识点" CssClass="button_s"   OnClientClick="return CreaterChapter()"/>
         </div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">

</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >

</asp:Content>
