<%@ Assembly Name="YHSD.VocationalEducation.Portal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=2e18f1308c96fd22" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PositionList.ascx.cs" Inherits="YHSD.VocationalEducation.Portal.CONTROLTEMPLATES.YHSD.VocationalEducation.Portal.PositionList" %>

<script>
    function AddPositionList() {
        var url = "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/PositionNew.aspx";
        //openDialog(url, 420, 300, closeCallback);
        OL_ShowLayer(2, "新建角色", 420, 300, url, function (returnVal) {
        });
        return false;

    }
    function EditPosition(id) {
        var url = "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/PositionEdit.aspx?id=" + id;
        //openDialog(url, 420, 300, closeCallback);
        OL_ShowLayer(2, "编辑角色", 420, 300, url,function (returnVal) {
        });
        return false;

    }
    function ChaKanUser(id) {
        
        window.location.href = "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/PositionUserList.aspx?PositionID=" + id;
    }
    function DeletePosition(id)
    {
       // LayerAlert('不能删除角色!!');
        LayerConfirmDelete("确定删除此角色吗?", function () {
            var postData = { DeleteID: id };
            var loadIndex = layer.load(2);
            $.ajax({
                type: "POST",
                url: "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/PositionEdit.aspx",
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
   <div class="main_list_table">
<%-- <label style="color:black;" id="labelPositionList" runat="server" visible="false"></label>--%>
       <table  class="list_table" cellspacing="0" cellpadding="0" border="0">
        <tr class="tab_th top_th"><th class="th_tit">序号</th><th class="th_tit_left">角色名称</th><th class="th_tit">角色成员</th><th class="th_tit">操作</th></tr>
        <asp:Repeater ID="RepeaterList" runat="server">
            <ItemTemplate>
                <tr class="tab_td">
                            <td class="td_tit">
<%# Container.ItemIndex + 1 + (this.AspNetPageCurriculum.CurrentPageIndex -1)*this.AspNetPageCurriculum.PageSize %>
              </td>
               <td class="td_tit_left">
               <%#Eval("PositionName") %>
              </td>
              <td class="td_tit">
                 <%-- <a href="/_layouts/15/YHSD.VocationalEducation.Portal/PositionUserList.aspx?PositionID=<%#Eval("Id")%>" target="_self">查看</a>--%>
                                          <img  title="查看角色人员" onclick="ChaKanUser('<%#Eval("Id") %>')" class="ViewImg" />
              </td>
                         <td class="td_tit"><img  title="编辑角色" onclick="EditPosition('<%#Eval("Id") %>')" class="EditImg" />
                             <img title="删除角色"  onclick="DeletePosition('<%#Eval("Id") %>')"class="DelImg" /></td>
                    </tr>
      
                </ItemTemplate>

                     <AlternatingItemTemplate>
                            <tr class="tab_td td_bg">
                                        <td class="td_tit">
<%# Container.ItemIndex + 1 + (this.AspNetPageCurriculum.CurrentPageIndex -1)*this.AspNetPageCurriculum.PageSize %>
              </td>
             <td class="td_tit_left">
               <%#Eval("PositionName") %>
              </td>

              <td class="td_tit">
<%--                  <a href="/_layouts/15/YHSD.VocationalEducation.Portal/PositionUserList.aspx?PositionID=<%#Eval("Id")%>" target="_self">查看</a>--%>
                                    <img  title="查看角色人员" onclick="ChaKanUser('<%#Eval("Id") %>')" class="ViewImg" />
              </td>
                         <td class="td_tit"><img title="编辑角色" onclick="EditPosition('<%#Eval("Id") %>')" class="EditImg" />
                            <img title="删除角色"  onclick="DeletePosition('<%#Eval("Id") %>')"class="DelImg"  /></td>
                    </tr>
                     </AlternatingItemTemplate>
                </asp:Repeater>
       </table>
       </div>
   <div style="text-align:center;"> 
    <div id="containerdiv" style="overflow: hidden; display: inline-block;">
                               <webdiyer:AspNetPager ID="AspNetPageCurriculum" CssClass="paginator" ShowFirstLast="false" PrevPageText="<<上一页" NextPageText="下一页>>"  runat="server" Width="100%"  CurrentPageButtonClass="paginatorPn" AlwaysShow="true"  HorizontalAlign="center"  OnPageChanged="AspNetPageCurriculum_PageChanged">
        </webdiyer:AspNetPager>
         </div>
                     </div>
<div class="main_button_part">
      <button class="button_s" OnClick="return AddPositionList()">添加角色</button>

</div>
