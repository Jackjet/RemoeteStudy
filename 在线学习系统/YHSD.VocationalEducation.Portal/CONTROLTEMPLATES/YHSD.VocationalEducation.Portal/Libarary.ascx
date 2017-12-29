<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Libarary.ascx.cs" Inherits="YHSD.VocationalEducation.Portal.CONTROLTEMPLATES.YHSD.VocationalEducation.Portal.Libarary" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

    
    <div title="维护知识库" style="padding: 10px">
        <div id="head" class="TopToolbar">
        <span>问题&nbsp</span>
        <asp:TextBox ID="TB_Question" runat ="server" style="width:198px;height:22px;border:1px solid #dedede;"></asp:TextBox>
        <asp:Button ID="searchButton" CssClass="button_s" runat="server" Text="查询" OnClick="BTSearch_Click" />
    </div>
        <div class="main_list_table">
            <table class="list_table" cellspacing="0" cellpadding="0" border="0">
                <tr class="tab_th top_th">
                    <th class="th_tit">序号</th>
                    <th class="th_tit_left">问题</th>
                    <th class="th_tit_left">答案</th>
                    <th class="th_tit_left">创建时间</th> 
                    <th class="th_tit">操作</th>                  
                </tr>
                <asp:Repeater ID="Rep_KnowledgeLib" runat="server">
                    <ItemTemplate>
                        <tr class="tab_td">
                            <td class="td_tit">
                                <%# Container.ItemIndex + 1 + (this.AspNetPagerKnowledgeLib.CurrentPageIndex -1)*this.AspNetPagerKnowledgeLib.PageSize %>
                            </td>
                            <td class="td_tit_left"><%#Eval("Question") %></td>
                            <td class="td_tit_left"><%#Eval("Answer") %></td>
                            <td class="td_tit_left"><%#Eval("CreateTime") %></td>   
                            <td class="td_tit"><a title="编辑" onclick="CreateKnowledgeLib('<%#Eval("Id") %>')"><img class="EditImg" /></a>
                              <a  onclick="DeleteKnowledge('<%#Eval("Id") %>')" title="删除"><img class="DelImg" /></a></td>                        
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="tab_td td_bg">
                            <td class="td_tit">
                                <%# Container.ItemIndex + 1 + (this.AspNetPagerKnowledgeLib.CurrentPageIndex -1)*this.AspNetPagerKnowledgeLib.PageSize %>
                            </td>
                            <td class="td_tit_left"><%#Eval("Question") %></td>
                            <td class="td_tit_left"><%#Eval("Answer") %></td>
                            <td class="td_tit_left"><%#Eval("CreateTime") %></td> 
                             <td class="td_tit"><a title="编辑" onclick="CreateKnowledgeLib('<%#Eval("Id") %>')"><img class="EditImg" /></a>
                                <a  onclick="DeleteKnowledge('<%#Eval("Id") %>')" title="删除"><img class="DelImg" /></a>         
                             </td>                                     
                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>

            </table>
        </div>
        <div style="text-align: center;">
            <div style="overflow: hidden; display: inline-block;">
                <webdiyer:AspNetPager ID="AspNetPagerKnowledgeLib" CssClass="paginator" ShowFirstLast="false" PrevPageText="<<上一页" NextPageText="下一页>>" runat="server" Width="100%" CurrentPageButtonClass="paginatorPn" AlwaysShow="true" HorizontalAlign="center" PageSize="2" OnPageChanged="AspNetPagerKnowledgeLib_PageChanged">
                </webdiyer:AspNetPager>
            </div>
        </div>
       
    </div>

