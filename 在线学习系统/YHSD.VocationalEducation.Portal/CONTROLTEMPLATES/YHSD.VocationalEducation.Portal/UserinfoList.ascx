<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserinfoList.ascx.cs" Inherits="YHSD.VocationalEducation.Portal.CONTROLTEMPLATES.YHSD.VocationalEducation.Portal.UserinfoList" %>


<script>
    $(function () {
        document.getElementById("<%=this.HidDeptName.ClientID %>").value = "";
        $('#cc').combotree({
            onSelect: function (node) {
                document.getElementById("<%=this.HidDeptName.ClientID %>").value = node.id;
            }
        })
    });
        function EditUser(id) {
            OL_ShowLayerNo(2, "编辑人员", 770, 450, "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/UserInfoEdit.aspx?id=" + id, function (returnVal) {
        });

        }
        function ADDUser() {
            OL_ShowLayerNo(2, "新增人员", 770, 450, "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/UserInfoADD.aspx", function (returnVal) {
            });

        }
        function ImportUser() {
            OL_ShowLayerNo(2, "导入人员", 700, 350, "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/ImportUser.aspx", function (returnVal) {
         });

        }
</script>

<asp:HiddenField ID="DeleteID" runat="server" />

        <div>
            <asp:HiddenField ID="HidDeptName" runat="server"  />
        <div id="head" class="TopToolbar">
            <span>所属乡镇&nbsp</span><select id="cc" class="easyui-combotree" style="width:200px;height:29px;"
        data-options="url:'<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/CompanyTree.aspx',required:false"></select>
            &nbsp&nbsp
<span>姓名&nbsp</span><asp:TextBox ID="txtName" runat="server" class="long_input_part" ></asp:TextBox><asp:Button ID="BtnChaXu" CssClass="button_s" runat="server" Text="查询" OnClick="BtnChaXu_Click" />
        </div>
        <div id="main"  class="main_list_table">
            <table id="tab" class="list_table" cellspacing="0" cellpadding="0" border="0">
                <thead>
                    <tr class="tab_th top_th">
                        <th class="th_tit">序号</th>
                        <td class="th_tit_left">姓名</td>
                        <td class="th_tit_left">性别</td>
                        <td class="th_tit_left">电话</td>
                        <td class="th_tit_left">邮箱</td>
                        <td class="th_tit_left">所属乡镇</td>
                        <td class="th_tit">操作</td>
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
                    <td class="td_tit_left"><%#Eval("SexName") %></td>
                    <td class="td_tit_left"> <%#Eval("Telephone") %></td>
                    <td class="td_tit_left"> <%#Eval("Email") %></td>
                    <td class="td_tit_left"> <%#Eval("DeptName") %></td>
                     <td class="td_tit"><img  title="编辑人员" onclick="EditUser('<%#Eval("Id") %>')" class="EditImg"  />
                             </td>
                    </tr>
      
                </ItemTemplate>
                     <AlternatingItemTemplate>
                            <tr class="tab_td td_bg">
                                               <td class="td_tit">
<%# Container.ItemIndex + 1 + (this.AspNetPageCurriculum.CurrentPageIndex -1)*this.AspNetPageCurriculum.PageSize %>
              </td>
                    <td class="td_tit_left"><%#Eval("Name") %></td>
                    <td class="td_tit_left"><%#Eval("SexName") %></td>
                    <td class="td_tit_left"> <%#Eval("Telephone") %></td>
                    <td class="td_tit_left"> <%#Eval("Email") %></td>    
                     <td class="td_tit_left"> <%#Eval("DeptName") %></td>   
                                 <td class="td_tit"><img  title="编辑人员" onclick="EditUser('<%#Eval("Id") %>')" class="EditImg"  />
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
            <input type="button" value="新增人员" class="button_s" onclick="ADDUser()" />
            <input type="button" value="导入人员" class="button_s" onclick="ImportUser()" />
        </div>
    </div>