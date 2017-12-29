<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ES_wp_ManualMakeExamUserControl.ascx.cs" Inherits="SVDigitalCampus.Examination_System.ES_wp_ManualMakeExam.ES_wp_ManualMakeExamUserControl" %>
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/common.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/iconfont.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/animate.css" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/tz_slider.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/popwin.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/win8loading.js"></script>
<style>
    .File_type dl dd a.click {
        background: #5493d7;
        color: #fff;
        display: inline-block;
        padding: 0 8px;
    }


    .progress {
        z-index: 2000;
    }
</style>
<script type="text/javascript">

    function UpdateObj(qID, type) {
        if (qID == null) {

            alert("请选择一条记录！");
            return false;

        }
        popWin.scrolling = "auto";
        popWin.showWin("750", "760", "查看试卷", '<%=SietUrl%>' + "EditExamiationPaper.aspx?qID=" + qID + "&type=" + type);
    }

    function AddExam() {
        popWin.scrolling = "auto";
        popWin.showWin("750", "760", "组卷", '<%=SietUrl%>' + "AddExamiationPaper.aspx");
    }
    //$(function () {
    //    $(".i-menu").click(function () {
    //        showimenu(this);
    //    });
    //    $(".ici-menu").click(function () {
    //        showicomenu(this);
    //    });
    //})
    function showimenu(obj) {
        $(".i-menu").next().hide();
        var dis = $(obj).next().css("display");
        //alert(dis);
        if (dis == "block") {
            $(obj).next().hide();
        }
        else {
            $(obj).next().show();
        }

    }
    function showicomenu(obj) {
        $(".ici-menu").next().hide();
        var dis = $(obj).next().css("display");
        //alert(dis);
        if (dis == "none") {
            $(obj).next().show();
        }
        else {
            $(obj).next().hide();
        }
    }
    function LoadExamQ() {
        loadImg('<%=layoutstr%>' + "/SVDigitalCampus/Image/ajax-loader.gif");
        jQuery.ajax({
            url: '<%=layoutstr%>' + "/SVDigitalCampus/ExamSystemHander/ExamSystemDataHander.aspx?action=LoadExamQDetail&" + Math.random(),   // 提交的页面
            type: "POST",
            async: true,
            beforeSend: function ()          // 设置表单提交前方法
            {
                loadImg('<%=layoutstr%>' + "/SVDigitalCampus/Image/ajax-loader.gif");
            },
            error: function (request) {      // 设置表单提交出错
                //alert("表单提交出错，请稍候再试");
                //rebool = false;
            },
            success: function (result) {
                var ss = result.split("|");
                if (ss[0] == "1") {
                    close();
                    window.location.reload(true);
                }
            }

        });
    }
</script>
<div class="School_library">
    <!--页面名称-->
    <h1 class="Page_name">挑选试题</h1>
    <!--整个展示区域-->
    <div class="Whole_display_area">
        <%-- <div class="Operation_area">
           <div class="left_choice fl">
                <ul>
                    <li class="">
                        <div class="qiehuan fl">
                            <span class="qijin">
                                <asp:LinkButton ID="btnMessage" OnClick="btnMessage_Click" runat="server" CssClass="Disable">试卷信息</asp:LinkButton><asp:LinkButton ID="btnAdd" runat="server" CssClass="Enable">新建试卷</asp:LinkButton></span>
                        </div>
                    </li>
                </ul>
            </div>
            <div class="clear"></div>
        </div>--%>
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
                                    <asp:LinkButton ID="majoritem" runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="showSubject" CssClass='<%#Eval("class") %>'><%#Eval("Title") %></asp:LinkButton>
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
                                    <asp:LinkButton ID="SubjectItem" runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="SubjectSearch" CssClass='<%#Eval("class") %>'><%#Eval("Title") %></asp:LinkButton></label>
                            </ItemTemplate>
                        </asp:ListView>
                    </dd>
                </dl>
            </div>
        </div>
        <div class="clear"></div>
        <!--操作区域-->
        <div class="Operation_area markover">
            <div class="left_choice fl">
                <ul>
                    <li class="">
                        <div class="qiehuan fl">
                            <span class="qijin">
                                <asp:LinkButton ID="btnManual" runat="server" CssClass="Enable">手动组卷</asp:LinkButton><asp:LinkButton ID="btnNoopsyche" runat="server" OnClick="btnNoopsyche_Click" CssClass="Disable">智能组卷</asp:LinkButton></span>
                        </div>
                    </li>
                </ul>
            </div>
            <div class="right_add fr">
                <!--试题篮-->
                <div class="Choose_TextBasket" runat="server" id="Basket">
                    <asp:Label ID="showtbcount" CssClass="showtbcount" runat="server" Text="0"></asp:Label><a href="#" onclick="ShowTextbasket();" class="Textclick">试题篮</a>
                    <div class="textbaskt" id="Testbasket" style="display: none;">
                        <!--试题篮 数量-->
                        <h1>共<asp:Label ID="Totalcount" CssClass="question_num " runat="server" Text="0"></asp:Label>题</h1>
                        <div class="clear"></div>
                        <!--试题篮-->
                        <div class="qtype">
                            <ul class="qtype_num">
                                <asp:ListView ID="lvEQBasket" runat="server" OnItemCommand="lvEQBasket_ItemCommand">
                                    <ItemTemplate>
                                        <li>
                                            <span class="b_Type"><%#Eval("Type")%>： </span>
                                            <span class="b_Count"><%#Eval("Count") %> 题</span>
                                            <span class="b_delete">
                                                <asp:LinkButton ID="btndelete" class="btn" runat="server" CommandName="del" CommandArgument='<%# Eval("TypeID") %>'><em class="iconfont d_d">&#xe64c;</em></asp:LinkButton>
                                            </span>
                                        </li>
                                    </ItemTemplate>
                                </asp:ListView>
                            </ul>
                            <!--end 试题区域-->

                        </div>
                        <div class="clear"></div>
                        <!--清空 组卷-->
                        <div class="basket_opteration">
                            <span class="basket_clear fl">
                                <asp:LinkButton ID="ClearAll" runat="server" OnClick="ClearAll_Click">清空全部</asp:LinkButton></span><span class="basket_submit fr">
                                    <asp:LinkButton ID="ToMakeExam" runat="server" UseSubmitBehavior="false" OnClientClick="this.value='组卷';this.disabled=true;" OnClick="ToMakeExam_Click">组卷</asp:LinkButton></span>
                        </div>
                    </div>
                </div>
               <a href="#" onclick="LoadExamQ();" class="add">刷新加载试题</a>
            <%--    <asp:LinkButton ID="lbLoadExamQ" runat="server" CssClass="add" OnClientClick="LoadExamQ();">刷新加载试题</asp:LinkButton>--%>
                <asp:LinkButton ID="btnMessage" OnClick="btnMessage_Click" runat="server" CssClass="add">试卷信息</asp:LinkButton>
            </div>
        </div>
        <div class="clear"></div>

        <div class="Schoolcon_wrap">
            <div class="left_navcon fl">
                <h1>课程</h1>
                <h3 class="tit">
                    <asp:LinkButton ID="lbChapterAll" OnClick="lbChapterAll_Click" runat="server"><i class="icon"></i>全部<span id="CharptCount" runat="server" class="fr tongji">(0)</span></asp:LinkButton></h3>
                <div class="select-box">
                    <asp:ListView ID="lvChapter" runat="server" OnItemCommand="lvChapter_ItemCommand">
                        <ItemTemplate>
                            <div class="item">
                                <div class="i-menu cf" id='<%#"imenu"+Eval("ID") %>'>
                                    <span class="fl tubiao"><i class="iconfont">&#xe606;</i></span>
                                    <asp:LinkButton ID="lbChapter" runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="showPart">
                                       <span class="tit fl"><%#Eval("Title") %>
                                            <span class="fr tongji">(<%#Eval("Count") %>)</span>
                                        </span>
                                    </asp:LinkButton>
                                </div>
                                <div class="i-con">
                                    <asp:ListView ID="lvPart" runat="server" OnItemCommand="lvPart_ItemCommand">
                                        <ItemTemplate>
                                            <div class="ic-item">
                                                <div class="ici-menu cf" id='<%#"part"+Eval("ID") %>'>
                                                    <asp:LinkButton ID="lvPart" runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="showKlPoint">
                                                         <span class="fl icon"></span>
                                                    <span class="tit fl"><%#Eval("Title") %>
                                                        <span class="fr tongji">(<%#Eval("Count") %>)</span>
                                                    </span>
                                                    </asp:LinkButton>
                                                </div>
                                                <div class="ici-con cf" style='display: <%#Eval("IsShow")%>;'>
                                                    <asp:ListView ID="lvKlpoint" runat="server" OnItemCommand="lvKlpoint_ItemCommand">
                                                        <ItemTemplate>
                                                            <div class="icic-item">
                                                                <span class="icon fl"></span>
                                                                <span class="fl">
                                                                    <asp:LinkButton ID="lbKlpoint" runat="server" CommandName="Getklpoint" CommandArgument='<%#Eval("ID") %>'><%#Eval("Title") %></asp:LinkButton></span>
                                                            </div>
                                                            <div class="clear"></div>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                    <div class="clear"></div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:ListView>
                                    <div class="clear"></div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
            </div>
            <div class="right_dcon fr">
                <div class="clear"></div>
                <div class="File_type" style="display: block;">
                    <dl class="F_type">
                        <dt>题型：</dt>
                        <asp:ListView ID="lvEQType" runat="server" ItemPlaceholderID="itemPlaceholder" OnItemCommand="lvEQType_ItemCommand">
                            <LayoutTemplate>
                                <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <dd>
                                    <asp:LinkButton ID="eqtypeitem" runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="showEqtype"><%#Eval("Title") %></asp:LinkButton>
                                </dd>
                            </ItemTemplate>
                        </asp:ListView>
                    </dl>
                    <div class="clear"></div>
                    <dl class="F_time">
                        <dt>难度：</dt>
                        <dd>
                            <asp:LinkButton ID="lbDAll" runat="server" OnClick="lbDAll_Click">不限</asp:LinkButton></dd>
                        <dd>
                            <asp:LinkButton ID="lbEsay" runat="server" OnClick="lbEsay_Click">简单</asp:LinkButton></dd>
                        <dd>
                            <asp:LinkButton ID="lbMidd" runat="server" OnClick="lbMidd_Click">中等</asp:LinkButton></dd>
                        <dd>
                            <asp:LinkButton ID="lbDifficult" runat="server" OnClick="lbDifficult_Click">较难</asp:LinkButton></dd>
                    </dl>

                </div>
                <div class="clear"></div>

                <!--展示区域-->
                <div class="Display_form">
                    总计<asp:Label ID="Totil" runat="server" CssClass="red" Text="0"></asp:Label>道试题
                    <asp:ListView ID="lvExamQ" runat="server" OnPagePropertiesChanging="lvExamQ_PagePropertiesChanging" OnItemCommand="lvExamQ_ItemCommand" OnItemDataBound="lvExamQ_ItemDataBound">
                        <EmptyDataTemplate>
                            <table>
                                <tr>
                                    <td colspan="9" style="text-align: center">亲，暂无试题记录！
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <LayoutTemplate>

                            <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>

                        </LayoutTemplate>
                        <ItemTemplate>
                            <div class="ExamQ">
                                <table class="E_form">
                                    <tr class="Single">
                                        <th class="Count">编号：<%#Eval("Count")%></th>

                                        <th class="Type">题型：<%#Eval("Type")%><asp:HiddenField ID="Type" runat="server" Value='<%#Eval("TypeID")%>' />
                                        </th>
                                        <th class="Difficulty">难度：<%#Eval("DifficultyShow")%></th>
                                        <th class="Created">更新时间：<%#Eval("Created")%></th>
                                    </tr>
                                    <tr>
                                        <td colspan="5" class="question"><span class="content"><%#Eval("Question") %></span><br />

                                            <ul class="xuanxiang" style='display: <%#Eval("IsShow")%>'>
                                                <li>
                                                    <span style='display: <%#Eval("OptionAIsshow")%>'>

                                                        <%#Eval("OptionA") %></span>
                                                    <span style='display: <%#Eval("OptionBIsshow")%>'>

                                                        <%#Eval("OptionB") %></span>

                                                    <span style='display: <%#Eval("OptionCIsshow")%>'>

                                                        <%#Eval("OptionC") %></span>

                                                    <span style='display: <%#Eval("OptionDIsshow")%>'>

                                                        <%#Eval("OptionD") %></span>

                                                    <span style='display: <%#Eval("OptionEIsshow")%>'>

                                                        <%#Eval("OptionE") %></span>

                                                    <span style='display: <%#Eval("OptionFIsshow")%>'>
                                                        <%#Eval("OptionF") %></span>
                                                </li>

                                            </ul>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <asp:HiddenField ID="btnaddIsable" Value='<%#Eval("Isable") %>' runat="server" />
                                            <a href="#" class="fr" onclick="Look('<%#Eval("ID") %>','<%#Eval("TypeID") %>');">查看</a>&nbsp;<asp:Button ID="lbAdd" runat="server" Text="加入试卷" CommandArgument='<%#Eval("ID")+"&"+Eval("TypeID")+"&"+Eval("QType")%>' CssClass='<%#Eval("choice") %>' CommandName="Add" /></td>
                                    </tr>
                                </table>
                            </div>
                            <div class="clear"></div>
                        </ItemTemplate>
                    </asp:ListView>
                    <div class="page">
                        <asp:DataPager ID="DPExamQ" runat="server" PageSize="8" PagedControlID="lvExamQ">
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
    </div>
</div>
<script type="text/javascript">
    function Look(qID, type) {
        popWin.scrolling = "auto";
        popWin.showWin("750", "750", "试题详情", '<%=SietUrl%>' + "ExamQDetail.aspx?qID=" + qID + "&type=" + type);
    }
    function ShowTextbasket() {
        if ($("#Testbasket").css("display") == "none") {
            $("#Testbasket").css("display", "block");
        }
        else {
            $("#Testbasket").css("display", "none");
        }
    }
    function Disable(obj) {
        $(obj).css("background", "#ccc");
        $(obj).attr({ "disabled": "disabled" });
    }
    function Enable(obj) {
        $(obj).css("background", "#96cc66");
        $(obj).remove("disabled");
    }

</script>
