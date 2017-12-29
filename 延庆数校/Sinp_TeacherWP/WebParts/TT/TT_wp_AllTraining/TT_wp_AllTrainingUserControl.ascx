<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TT_wp_AllTrainingUserControl.ascx.cs" Inherits="Sinp_TeacherWP.WebParts.TT.TT_wp_AllTraining.TT_wp_AllTrainingUserControl" %>
<script src="/_layouts/15/Script/jquery-1.8.0.js"></script>
<script src="/_layouts/15/Script/popwin.js"></script>
<script src="/_layouts/15/Script/uploadFile.js"></script>
<link href="/_layouts/15/Style/tablelist.css" rel="stylesheet" />
<link href="/_layouts/15/Style/layout.css" rel="stylesheet" />
<link href="/_layouts/15/Style/common.css" rel="stylesheet" />
<script type="text/javascript">
    function closePages() {
        $("#mask,#maskTop").fadeOut();
    }
</script>
<div class="maindv">
    <div class="leftdv">        
        <div class="single_tit">
            <h2>
                <span class="title_left fl"><a href="#">研修计划列表</a></span>
                <span class="title_right fr">
                <asp:Panel ID="Pan_AddProject" runat="server" Visible="false">
                <a id="newProject" class="add_GL" onclick="openPage('添加研修计划','/SitePages/AddTrainProject.aspx','700','500');return false;">
                    <span>添加研修计划</span>
                </a>
                </asp:Panel>
                </span>
            </h2>
        </div>
        <div class="tabcontent">
            <asp:ListView ID="LV_Train" runat="server" OnPagePropertiesChanging="LV_Train_PagePropertiesChanging" OnPreRender="LV_Train_PreRender" OnItemCommand="LV_Train_ItemCommand">
                <EmptyDataTemplate>
                    <table class="emptydate">
                        <tr>
                            <td>
                                <img src="/_layouts/15/TeacherImages/expression.png" /><p>没有找到相关内容</p>
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <LayoutTemplate>
                    <table cellspacing="0" cellpadding="0" class="W_form">
                        <tr class="trth">
                            <th>计划标题</th>
                            <th>开始时间</th>
                            <th>结束时间</th>
                            <th>操作</th>
                        </tr>
                        <tr id="itemPlaceholder" runat="server"></tr>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr class="Single">
                        <td>
                            <a href='Discuss.aspx?itemid=<%# Eval("ID") %>'><%# Eval("Title") %></a></td>
                        <td><%# Eval("StartTime") %></td>
                        <td><%# Eval("EndTime") %></td>
                        <td>
                            <asp:HiddenField ID="DetailID" runat="server" Value='<%# Eval("ID") %>' />
                            <asp:Button ID="Btn_Edit" runat="server" Text="编辑" OnClientClick="editpdetail(this,'编辑研修计划','/SitePages/AddTrainProject.aspx?itemid=','700','500');return false;" CssClass="btn_editor" />
                            <asp:Button ID="Btn_End" runat="server" CommandName="end" CommandArgument='<%# Eval("ID") %>' Text="结束" OnClientClick="return confirm('你确定结束吗？')" CssClass="btn_editor" />
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="Double">
                        <td><a href='Discuss.aspx?itemid=<%# Eval("ID") %>'><%# Eval("Title") %></a></td>
                        <td><%# Eval("StartTime") %></td>
                        <td><%# Eval("EndTime") %></td>
                        <td>
                            <asp:HiddenField ID="DetailID" runat="server" Value='<%# Eval("ID") %>' />
                            <asp:Button ID="Btn_Edit" runat="server" Text="编辑" OnClientClick="editpdetail(this,'编辑研修计划','/SitePages/AddTrainProject.aspx?itemid=','700','500');return false;" CssClass="btn_editor" />
                            <asp:Button ID="Btn_End" runat="server" CommandName="end" CommandArgument='<%# Eval("ID") %>' Text="结束" OnClientClick="return confirm('你确定结束吗？')" CssClass="btn_editor" />
                        </td>
                    </tr>
                </AlternatingItemTemplate>
            </asp:ListView>

        </div>
        <div class="paging">

            <asp:DataPager ID="DP_Train" runat="server" PageSize="10" PagedControlID="LV_Train">
                <Fields>
                    <asp:NextPreviousPagerField
                        ButtonType="Link" ShowNextPageButton="False" ShowPreviousPageButton="true"
                        ShowFirstPageButton="true" FirstPageText="首页" PreviousPageText="上一页" />

                    <asp:NumericPagerField CurrentPageLabelCssClass="number now" NumericButtonCssClass="number" />

                    <asp:NextPreviousPagerField
                        ButtonType="Link" ShowPreviousPageButton="False" ShowNextPageButton="true"
                        ShowLastPageButton="true" LastPageText="末页" NextPageText="下一页" />

                    <asp:TemplatePagerField>
                        <PagerTemplate>
                            <span class="page">| <%# Container.StartRowIndex / Container.PageSize + 1%> / 
                            <%# (Container.TotalRowCount % Container.MaximumRows) > 0 ? Convert.ToInt16(Container.TotalRowCount / Container.MaximumRows) + 1 : Container.TotalRowCount / Container.MaximumRows%>  页
                            (共<%# Container.TotalRowCount %>项)
                            </span>
                        </PagerTemplate>
                    </asp:TemplatePagerField>
                </Fields>
            </asp:DataPager>
        </div>
    </div>
    <div class="rightdv">   
        <div class="descri">   
            <div class="single_tit">
                <h2>
                    <span class="title_left"><asp:Literal ID="Lit_GroupName" Text="语文组" runat="server"></asp:Literal>简介</span>
                    <span class="title_right"></span>
                </h2>
            </div>
            <div class="textcontent">
                <p>
                    <asp:Literal ID="Lit_Description" runat="server" Text="这里是激情的沃土，这里是希望的田野；这里飞扬着独特的气质和个性，这里弥漫着让心灵震颤、历久弥新的人性关怀。这里就是延庆县十一学校语文教研组。
                    走进语文课堂，你会如坐春风。那一个个平凡的中国方块字在语文教师讲述下散发出非凡的魅力，优美的意境和真挚的情感使一批又一批学生在此流连忘返。
                    走进文学的世界，你会发现这里的天地如此广阔。你可以在诗歌的江河中尽情漂流，从诗经到楚辞，从唐诗到宋词，一直漂流到绿柳掩映下康河的柔波中。
                    走近语文教师，你会发现，我们不仅懂得了什么是语文之美，更懂得了什么是爱，什么是真，什么是善。生活因为语文而精彩！"></asp:Literal>
                </p>

            </div>
     </div>   
        <div class="clear"></div>
     <div class="gromember">
          <div class="single_tit">
                <h2>
                    <span class="title_left"> 研修组成员</span>
                    <span class="title_right"></span>
                </h2>
            </div>
            <ul class="people">
                <asp:Repeater ID="Rep_member" runat="server">

                    <ItemTemplate>
                        <li>
                            <img src='<%#Eval("PhotoUrl") %>'><span class="name_yx"><%#Eval("MemberName") %></span>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>


            </ul>
        </div>
    </div>
</div>

