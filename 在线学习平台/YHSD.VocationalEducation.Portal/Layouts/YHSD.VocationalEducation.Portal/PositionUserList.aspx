<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PositionUserList.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.PositionUserList" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <link href="css/type.css" rel="stylesheet" />
    <link href="js/Jquery.easyui/themes/icon.css" rel="stylesheet" />
<link href="js/Jquery.easyui/themes/default/easyui.css" rel="stylesheet" />
    <script src="js/Jquery.easyui/jquery.min.js"></script>
    <script src="js/Jquery.easyui/jquery.easyui.min.js"></script>
    <script src="js/layer/layer.js"></script>
    <script src="js/layer/OpenLayer.js"></script>
    <script>
        function AddStudent() {
            OL_ShowLayerNo(2, "添加角色人员", 850, 620, "UserSelect.aspx?type=role", function (returnVal) {
                if (returnVal!=null&&returnVal.length!=0)
                {
                    var postData = { UserList: JSON.stringify(returnVal), PositionId: document.getElementById("<%=this.HidPositionID.ClientID %>").value };
                    $.ajax({
                        type: "POST",
                        url: "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/PositionEdit.aspx",
                        data: postData,
                        dataType: "text",
                        success: function (returnVal) {
                            if (returnVal =="ok") {
                                LayerAlert('添加角色人员成功！！', function () { window.location.href = window.location.href; });
                               
                            }
                        },
                        error: function (errMsg) {
                            LayerAlert('添加角色人员失败！！');
                        }
                    })
                }
            });
        }

        function DeleteUserID(id)
        {
            LayerConfirmDelete("确定移除此人员吗?", function () {
                var postData = { DeleteUserID: id };
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
                                LayerAlert("移除成功!!!", function () {
                                    window.location.href = window.location.href;
                                });
                            }
                            else {

                                LayerAlert(returnVal);
                            }
                        }
                    },
                    error: function (errMsg) {
                        LayerAlert('移除失败！！');
                    }
                })
            });
        }
    </script>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
        <div>
            <asp:HiddenField ID="HidPositionID" runat="server" />
        <div id="head" class="TopToolbar">
            <span>学员姓名&nbsp</span><asp:TextBox ID="txtName" runat="server" class="long_input_part" ></asp:TextBox>&nbsp&nbsp<asp:Button ID="BtnChaXu" CssClass="button_s" runat="server" Text="查询" OnClick="BtnChaXu_Click" />
        </div>
        <div id="main"  class="main_list_table">
            <table id="tab" class="list_table" cellspacing="0" cellpadding="0" border="0">
                <thead>
                    <tr class="tab_th top_th">
                        <th class="th_tit">序号</th>
                        <th class="th_tit_left">姓名</th>
                        <th class="th_tit_left">性别</th>
                        <th class="th_tit_left">电话</th>
                        <th class="th_tit_left">邮箱</th>
                        <th class="th_tit_left">操作</th>
                    </tr>
                </thead>
                <tbody>
                       <asp:Repeater ID="RepeaterList" runat="server">
            <ItemTemplate>
                <tr class="tab_td">
                              <td class="td_tit">
<%# Container.ItemIndex + 1 + (this.AspNetPageCurriculum.CurrentPageIndex -1)*this.AspNetPageCurriculum.PageSize %>
              </td>
                    <td class="td_tit_left"><%#Eval("Name") %></td>
                    <td class="td_tit_left"><%#Eval("Sex") %></td>
                    <td class="td_tit_left"> <%#Eval("Telephone") %></td>
                    <td class="td_tit_left"> <%#Eval("Email") %></td>
                     <td class="td_tit_left">
                        <a title="移除人员" onclick="DeleteUserID('<%#Eval("Id") %>')" target="_self"><img class="DelImg" />
                </a></td>
                    </tr>
      
                </ItemTemplate>
                     <AlternatingItemTemplate>
                            <tr class="tab_td td_bg">
                                          <td class="td_tit">
<%# Container.ItemIndex + 1 + (this.AspNetPageCurriculum.CurrentPageIndex -1)*this.AspNetPageCurriculum.PageSize %>
              </td>
                    <td class="td_tit_left"><%#Eval("Name") %></td>
                    <td class="td_tit_left"><%#Eval("Sex") %></td>
                    <td class="td_tit_left"> <%#Eval("Telephone") %></td>
                    <td class="td_tit_left"> <%#Eval("Email") %></td>    
                    <td class="td_tit_left">
                        
                        <a title="移除人员" onclick="DeleteUserID('<%#Eval("Id") %>')" target="_self"><img class="DelImg" />
                </a>
                    </td>   
                    </tr>
                     </AlternatingItemTemplate>
                </asp:Repeater>
                </tbody>
            </table>
        </div>
                                 <div style="text-align:center;"> 
    <div id="containerdiv" style="overflow: hidden; display: inline-block;">
                               <webdiyer:AspNetPager ID="AspNetPageCurriculum" CssClass="paginator" ShowFirstLast="false" PrevPageText="<<上一页" NextPageText="下一页>>"  runat="server" Width="100%"  CurrentPageButtonClass="paginatorPn" AlwaysShow="true"  HorizontalAlign="center"  OnPageChanged="AspNetPageCurriculum_PageChanged">
        </webdiyer:AspNetPager>
         </div>
                     </div>
        <div class="main_button_part">
            <input type="button" value="添加成员" class="button_s" onclick="AddStudent()" /><input type="button" value="返回" onclick="    javascript: window.history.back()"  class="button_s"/>
        </div>
    </div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
</asp:Content>
