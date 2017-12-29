<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClassGateway.ascx.cs" Inherits="YHSD.VocationalEducation.Portal.CONTROLTEMPLATES.YHSD.VocationalEducation.Portal.ClassGateway" %>
<script type="text/javascript">  
    $(function () {
        $("#ClassTabs").tabs("select", $("#hid_Tabsid").val());     
    });
    $("#ClassTabs").tabs({
        onSelect: function (title, index) {
            $("#hid_Tabsid").val(index);
        }
    });
    function CreateNotification() {
        OL_ShowLayerNo(2, "发布通知", 400, 200, "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/NotificationAdd.aspx", function (returnVal) {
        });
    }
    function CreatePapar() {
        OL_ShowLayerNo(2, "新建问卷", 700, 600, "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/SitePages/EditSurveyPaper.aspx", function (returnVal) {
        });
       return false;
    }
    function CreateAnalysis() {
        var url = "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/SitePages/SurveyResult.aspx";
        window.location.href = url;
        return false;
    }    
    function CreatePaperMgr() {
        var url = "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/SitePages/PapersMgr.aspx";
        window.location.href = url;
        return false;
    }
    function CreateMarking() {
        var url = "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/SitePages/Marking.aspx";
        window.location.href = url;
        return false;
    }
    function CreateKnowledgeLib(text) {
        if (text == "add") {
            OL_ShowLayerNo(2, "新建知识点", 400, 200, "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/KnowledgeLibAdd.aspx", function (returnVal) {
            });
        } else {
            OL_ShowLayerNo(2, "编辑知识点", 400, 200, "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/KnowledgeLibAdd.aspx?itemid="+text, function (returnVal) {
            });
        }       
    }
    function DeleteKnowledge(delId) {
        LayerConfirm("您确定要删除吗?", function () {
            AjaxRequest("<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/Handler/CommonHandler.aspx", { CMD: "DelModel", TypeName: "KnowledgeLib", DelId: delId }, function (returnVal) {
                location.href = this.location.href;
                layer.closeAll();
            })
        });
    }   
</script>
<input type="hidden" id="hid_Tabsid" value="0" />
<div class="easyui-tabs" id="ClassTabs" data-options="tabWidth:100" style="width: 836px;">
    <div title="通知" style="padding: 10px">
        <div class="main_list_table">
            <table class="list_table" cellspacing="0" cellpadding="0" border="0">
                <tr class="tab_th top_th">
                    <th class="th_tit">序号</th>
                    <th class="th_tit_left">通知内容</th>
                    <th class="th_tit_left">接收人</th>
                </tr>
                <asp:Repeater ID="Rep_Notification" runat="server">
                    <ItemTemplate>
                        <tr class="tab_td">
                            <td class="td_tit">
                                <%# Container.ItemIndex + 1 + (this.AspNetPageNotification.CurrentPageIndex -1)*this.AspNetPageNotification.PageSize %>
                            </td>
                            <td class="td_tit_left"><%#Eval("Content") %></td>
                            <td class="td_tit_left"><%#Eval("SendPerson") %></td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="tab_td td_bg">
                            <td class="td_tit">
                                <%# Container.ItemIndex + 1 + (this.AspNetPageNotification.CurrentPageIndex -1)*this.AspNetPageNotification.PageSize %>
                            </td>
                            <td class="td_tit_left"><%#Eval("Content") %></td>
                            <td class="td_tit_left"><%#Eval("SendPerson") %></td>
                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div style="text-align: center;">
            <div id="containerdiv" style="overflow: hidden; display: inline-block;">
                <webdiyer:AspNetPager ID="AspNetPageNotification" CssClass="paginator" ShowFirstLast="false" PrevPageText="<<上一页" NextPageText="下一页>>" runat="server" Width="100%" CurrentPageButtonClass="paginatorPn" AlwaysShow="true" HorizontalAlign="center" PageSize="2" OnPageChanged="AspNetPageNotification_PageChanged">
                </webdiyer:AspNetPager>
            </div>
        </div>
        <div class="main_button_part">
            <input type="button" value="发布通知" class="button_s" onclick="CreateNotification()" />            
        </div>
    </div>
    <div title="问卷调查" style="padding: 10px">        
        <div class="main_list_table">
            <table class="list_table" cellspacing="0" cellpadding="0" border="0">
                <tr class="tab_th top_th">
                    <th class="th_tit">序号</th>
                    <th class="th_tit_left">问卷名称</th>
                    <th class="th_tit_left">问卷类别</th>
                    <th class="th_tit_left">评价类型</th>
                    <th class="th_tit_left">开始日期</th>
                    <th class="th_tit_left">截止日期</th>
                    <th class="th_tit_left">参与者</th>
                    <th class="th_tit_left">是否启用</th>
                </tr>
                <asp:Repeater ID="Rep_SurveyPaper" runat="server">
                    <ItemTemplate>
                        <tr class="tab_td">
                            <td class="td_tit">
                                <%# Container.ItemIndex + 1 + (this.AspNetPagerSurveyPaper.CurrentPageIndex -1)*this.AspNetPagerSurveyPaper.PageSize %>
                            </td>
                            <td class="td_tit_left"><%#Eval("Title") %></td>
                            <td class="td_tit_left"><%#Eval("Type") %></td>
                            <td class="td_tit_left"><%#Eval("Target") %></td>
                            <td class="td_tit_left"><%#Eval("StartDate") %></td>
                            <td class="td_tit_left"><%#Eval("EndDate") %></td>
                            <td class="td_tit_left"><%#Eval("Ranger") %></a></td>
                            <td class="td_tit_left"><%#Eval("Status") %></a></td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="tab_td td_bg">
                            <td class="td_tit">
                                <%# Container.ItemIndex + 1 + (this.AspNetPagerSurveyPaper.CurrentPageIndex -1)*this.AspNetPagerSurveyPaper.PageSize %>
                            </td>
                            <td class="td_tit_left"><%#Eval("Title") %></td>
                            <td class="td_tit_left"><%#Eval("Type") %></td>
                            <td class="td_tit_left"><%#Eval("Target") %></td>
                            <td class="td_tit_left"><%#Eval("StartDate") %></td>
                            <td class="td_tit_left"><%#Eval("EndDate") %></td>
                            <td class="td_tit_left"><%#Eval("Ranger") %></a></td>
                            <td class="td_tit_left"><%#Eval("Status") %></a></td>
                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>

            </table>
        </div>
        <div style="text-align: center;">
            <div style="overflow: hidden; display: inline-block;">
                <webdiyer:AspNetPager ID="AspNetPagerSurveyPaper" CssClass="paginator" ShowFirstLast="false" PrevPageText="<<上一页" NextPageText="下一页>>" runat="server" Width="100%" CurrentPageButtonClass="paginatorPn" AlwaysShow="true" HorizontalAlign="center" PageSize="2" OnPageChanged="AspNetPagerSurveyPaper_PageChanged">
                </webdiyer:AspNetPager>
            </div>
        </div>
        <div class="main_button_part">
            <input type="button" value="新建问卷" class="button_s" onclick="CreatePapar()" />
            <input type="button" value="统计分析" class="button_s" onclick="CreateAnalysis()" />
        </div>
    </div>
    <div title="论坛" style="padding: 10px">
        <iframe style="min-height:800px;width:100%;" src="http://61.50.119.70:1122/Lists/List19/AllItems.aspx?isDlg=1"></iframe>          
    </div>
    <div title="聊天室" style="padding: 10px"> 
        <iframe style="height:100%;width:100%;" src="http://61.50.119.70:1060/ChatManage.aspx"></iframe>                          
    </div>
    <div title="电子邮件" style="padding: 10px;">
        <iframe style="height:230px;width:100%;" src="<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/SendEmail.aspx"></iframe>         
    </div>
    <div title="作业考试" style="padding: 10px">
         <div class="main_button_part">
            <input type="button" value="作业考试维护" class="button_s" onclick="CreatePaperMgr()" />      
            <input type="button" value="作业考试批改" class="button_s" onclick="CreateMarking()" />   
        </div> 
    </div>
    <div title="在线答疑栏目" style="padding: 10px">
        <iframe style="min-height:800px;width:100%;" src="http://61.50.119.70:1122/Lists/List21/AllItems.aspx?isDlg=1"></iframe>          
    </div>
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
        <div class="main_button_part">
            <input type="button" value="新建知识点" class="button_s" onclick="CreateKnowledgeLib('add')" />
        </div>
    </div>
</div>
