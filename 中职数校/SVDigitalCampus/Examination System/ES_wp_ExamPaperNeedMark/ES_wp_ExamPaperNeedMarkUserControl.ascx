<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ES_wp_ExamPaperNeedMarkUserControl.ascx.cs" Inherits="SVDigitalCampus.Examination_System.ES_wp_ExamPaperNeedMark.ES_wp_ExamPaperNeedMarkUserControl" %>
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/common.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/style.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/iconfont.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/animate.css" />
<script src="/_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script type="text/javascript" src="/_layouts/15/SVDigitalCampus/Script/tz_slider.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/popwin.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/DatePicker/WdatePicker.js"></script>
<script type="text/javascript">
    function MarkObj(EPID, EID) {
        if (EID == null) {

            alert("请选择一条记录！");
            return false;

        }
        popWin.scrolling = "auto";
        //$.dialog({ id: 'Menu', title: '修改菜品', content: 'url:UpdateMenu.aspx?MenuId=' + MenuID, width: 700, height: 500, lock: true, max: false, min: false });
        popWin.showWin("750", "760", "阅卷", '<%=SietUrl%>' + "ExamPaperMark.aspx?qID=" + EPID + "&ExamID=" + EID);
    }
    function Look(EPID, Examid) {
        if (Examid == null) {

            alert("请选择一条记录！");
            return false;

        }
        popWin.scrolling = "auto";
        popWin.showWin("750", "760", "试卷分析", '<%=SietUrl%>' + "ExaminationDetail.aspx?ExamPaperID=" + EPID + "&ExamID=" + Examid);
    }
</script>
<div class="Enterprise_information_feedback">
    <!--页面名称-->
    <h1 class="Page_name">考试管理</h1>
    <!--整个展示区域-->
    <div class="Whole_display_area">
        <div class="Operation_area">
            <div class="left_choice fl">
                <ul>
                    <li class="">
                        <div class="qiehuan fl"><span class="qijin"><a class="Enable">成绩统计</a><asp:LinkButton ID="btnReport" runat="server" OnClick="btnReport_Click" CssClass="Disable">统计分析</asp:LinkButton></span></div>
                    </li>
                </ul>
            </div>
            <div class="clear"></div>
        </div>
        <div class="S_conditions">
            <div id="selectList" class="screenBox screenBackground">
                <dl class="listIndex dlHeight" attr="terminal_brand_s">
                    <dt>专业</dt>
                    <dd>
                        <asp:ListView ID="lvMajor" runat="server" ItemPlaceholderID="itemPlaceholder" OnItemCommand="lvMajor_ItemCommand">
                            <LayoutTemplate>
                                <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <label>
                                    <asp:LinkButton ID="majoritem" runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="showSubject"><%#Eval("Title") %></asp:LinkButton>
                                </label>
                            </ItemTemplate>
                        </asp:ListView>
                        <span class="more">展开</span>
                    </dd>
                </dl>
                <dl class="listIndex" attr="terminal_brand_s" id="subjectdl" runat="server">
                    <dt>学科</dt>
                    <dd>
                        <asp:ListView ID="lvSubject" runat="server" ItemPlaceholderID="subitemPlaceHolder" OnItemCommand="lvSubject_ItemCommand">
                            <LayoutTemplate>
                                <asp:PlaceHolder ID="subitemPlaceHolder" runat="server"></asp:PlaceHolder>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <label>
                                    <asp:LinkButton ID="SubjectItem" runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="SubjectSearch"><%#Eval("Title") %></asp:LinkButton></label>
                            </ItemTemplate>
                        </asp:ListView>
                    </dd>
                </dl>
            </div>
        </div>
        <div class="clear"></div>
        <div class="Operation_area">
            <div class="left_choice fl">
                <ul>
                    <li class="">
                        <div class="qiehuan fl">
                            <span class="qijin">
                                <asp:LinkButton ID="btnMark" runat="server" CssClass="Enable">待阅试卷</asp:LinkButton>
                                <asp:LinkButton ID="btnExamList" runat="server" CssClass="Disable" OnClick="btnExamList_Click">已阅试卷</asp:LinkButton></span>
                        </div>
                    </li>
                </ul>
            </div>
        </div>
        <!--操作区域-->
        <div class="Operation_area">
            <div class="left_choice fl">
                <ul>
                    <li class="Select">
                        <asp:DropDownList ID="ddlClass" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged">
                            <asp:ListItem Text="班级" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </li>
                    <li class="times">
                        <span class="times_input">
                            <input type="text" class="times_input Wdate" readonly="readonly" runat="server" id="txtdatebegin" onclick="WdatePicker()" /><i class="iconfont"></i></span>-
                        <span class="times_input">
                            <input type="text" class="times_input Wdate" readonly="readonly" runat="server" id="txtdateend" onclick="WdatePicker()" /><i class="iconfont"></i></span>
                        <asp:Button ID="btnSearch" class="b_query" runat="server" Text="查询" OnClick="btnSearch_Click" />
                    </li>
                    <li class="Sear">
                        <asp:TextBox placeholder="请输入答题人或考试" ID="txtStuName" CssClass="search" AutoPostBack="true" OnTextChanged="txtStuName_TextChanged" runat="server"></asp:TextBox><a><i class="iconfont">&#xe62d;</i></a>
                    </li>
                </ul>
            </div>
        </div>
        <div class="clear"></div>
        <!--展示区域-->
        <div class="Display_form">
            <asp:ListView ID="lvExam" runat="server" OnPagePropertiesChanging="lvExam_PagePropertiesChanging">
                <EmptyDataTemplate>
                    <table>
                        <tr>
                            <td colspan="8" style="text-align: center">亲，暂无待阅卷试卷记录！
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <LayoutTemplate>
                    <table class="D_form">
                        <tr class="trth">
                            <!--表头tr名称-->
                            <th class="Account">编号
                            </th>
                            <th class="name">学生名称
                            </th>
                            <th class="Head">班级
                            </th>
                            <th class="Contact">考试科目
                            </th>
                            <th class="Contact">考试名称
                            </th>
                            <th class="Contact">考试时间
                            </th>
                            <th class="State">状态
                            </th>
                            <th class="Operation">操作
                            </th>
                        </tr>
                        <tr id="itemPlaceholder" runat="server"></tr>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr class="Single">
                        <td class="Count">
                            <%#Eval("Count")%>
                        </td>
                        <td class="Title">
                            <%#Eval("UserName")%>
                        </td>
                        <td class="Type">
                            <%#Eval("Class")%>
                        </td>
                        <td class="Difficulty">
                            <%#Eval("SubName")%>
                        </td>
                        <td class="Author"><%#Eval("Title")%></td>
                        <td class="Created">
                            <%#Eval("AnswerBeginTime")%>  
                        </td>
                        <td class="State">
                            <%#Eval("StatusShow")%>  
                        </td>
                        <td class="Operation">
                            <a href="javascript:void(0);" onclick="MarkObj('<%#Eval("ExampaperID") %>','<%# Eval("ID") %>');" style="color: blue;">阅卷</a>
                            <a href="javascript:void(0);" onclick="Look('<%#Eval("ExampaperID") %>','<%# Eval("ID") %>');" style="color: blue;" title="详情"><i class="iconfont">&#xe60c;</i></a>
                        </td>
                    </tr>

                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="Double">
                        <td class="Count">
                            <%#Eval("Count")%>
                        </td>
                        <td class="Title">
                            <%#Eval("UserName")%>
                        </td>
                        <td class="Type">
                            <%#Eval("Class")%>
                        </td>
                        <td class="Difficulty">
                            <%#Eval("SubName")%>
                        </td>
                        <td class="Author"><%#Eval("Title")%></td>
                        <td class="Created">
                            <%#Eval("AnswerBeginTime")%>  
                        </td>
                        <td class="Created">
                            <%#Eval("StatusShow")%>  
                        </td>
                        <td class="Operation">
                            <a href="javascript:void(0);" onclick="MarkObj('<%#Eval("ExampaperID") %>','<%# Eval("ID") %>');" style="color: blue;">阅卷</a>
                            <a href="javascript:void(0);" onclick="Look('<%#Eval("ExampaperID") %>','<%# Eval("ID") %>');" style="color: blue;" title="查看"><i class="iconfont">&#xe60c;</i></a>

                        </td>
                    </tr>
                </AlternatingItemTemplate>
            </asp:ListView>
            <div class="page">
                <asp:DataPager ID="DPExam" runat="server" PageSize="8" PagedControlID="lvExam">
                    <Fields>
                        <asp:NextPreviousPagerField
                            ButtonType="Link" ShowNextPageButton="False" ShowPreviousPageButton="true"
                            ShowFirstPageButton="true" FirstPageText="首页" PreviousPageText="上一页" ButtonCssClass="pageup" />

                        <asp:NumericPagerField CurrentPageLabelCssClass="number now" NumericButtonCssClass="number" />

                        <asp:NextPreviousPagerField
                            ButtonType="Link" ShowPreviousPageButton="False" ShowNextPageButton="true"
                            ShowLastPageButton="true" LastPageText="末页" NextPageText="下一页" ButtonCssClass="pagedown" />

                        <asp:TemplatePagerField>
                            <PagerTemplate>
                                <span class="count">| <%# Container.StartRowIndex / Container.PageSize + 1%> / 
                            <%# (Container.TotalRowCount % Container.MaximumRows) > 0 ? Convert.ToInt16(Container.TotalRowCount / Container.MaximumRows) + 1 : Container.TotalRowCount / Container.MaximumRows%>  页
                            (共<%# Container.TotalRowCount %>项)
                                </span>
                            </PagerTemplate>
                        </asp:TemplatePagerField>
                    </Fields>
                </asp:DataPager>

            </div>
        </div>
    </div>
</div>
