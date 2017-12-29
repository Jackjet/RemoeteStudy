<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SG_wp_StudentGrowthUserControl.ascx.cs" Inherits="Sinp_StudentWP.WebParts.SG.SG_wp_StudentGrowth.SG_wp_StudentGrowthUserControl" %>
<link href="/_layouts/15/Style/iconfont.css" rel="stylesheet" />
<link href="/_layouts/15/Style/layout.css" rel="stylesheet" />
<link href="/_layouts/15/Style/common.css" rel="stylesheet" />
<link href="/_layouts/15/Stu_css/StuAssociate.css" rel="stylesheet" />
<script src="/_layouts/15/Script/jquery-1.8.0.js"></script>
<script src="/_layouts/15/Script/popwin.js"></script>
<script src="/_layouts/15/Script/uploadFile.js"></script>
<script src="/_layouts/15/Script/uploadfile1.js"></script>
<script src="/_layouts/15/Script/My97DatePicker/WdatePicker.js"></script>
<script type="text/javascript">
    $(function () {
        var intvalue = $("Input[id*='HF_TabFlag']").val();
        var loadindex = $("." + intvalue).parent().index();
        $("." + intvalue).parent().addClass("selected").siblings().removeClass("selected");
        $("." + intvalue).parents(".yy_tab").find(".tc").eq(loadindex).show().siblings().hide();

        //var hidthree = $("Input[id*='HidThree']").val();
        //$("#tabThree").css("display", hidthree);

        $(".yy_tab .yy_tabheader").find("a").click(function () {
            var index = $(this).parent().index();
            $(this).parent().addClass("selected").siblings().removeClass("selected");
            $(this).parents(".yy_tab").find(".tc").eq(index).show().siblings().hide();

            $("Input[id*='HF_TabFlag']").val($(this).attr("class"));
        });
        $(".topadd").find("a").click(function () {
            $(this).parent().next().slideToggle();
            //$(".tabContent").show();
        });
        $(".J-more").click(function () {
            $(this).html($(this).html() == "更多" ? "收起" : "更多");
            $(this).parents('.boxmore').siblings('.con_text').find('.con_con').toggle();
        });

    });
    function RemovePrize(fileName, trId) {
        $("#" + trId).hide();
        var nowfile = $("input[id$=Hid_fileName3]").val()
        $("input[id$=Hid_fileName3]").val(nowfile + '#' + fileName);
    }
    function hidetab() {
        $(".tabContent").hide();
    }
    function showtab() {
        $(".tabContent").show();
    }
    function showcontrol(objid) {
        $(objid).show();
    }
    function hidecontrol(objid) {
        $(objid).hide();
    }
    function showterm() {
        $(".dvaddTerm").hide();
        $("#addterm").show();
    }
    function closePages() {
        $("#mask,#maskTop").fadeOut(function () {
            $(this).remove();
        });
    }
    //$(':input', '#myform').not(':button, :submit, :reset, :hidden').val('').removeAttr('checked').removeAttr('selected');

</script>
<style type="text/css">
    .yy_tab .yy_tabheader a {
        padding: 0px 15px;
    }
</style>
<asp:HiddenField ID="HF_TabFlag" Value="first" runat="server" />
<div class="yy_tab">
    <div class="yy_tabheader">
        <ul class="tab_tit">
            <li class="selected"><a href="#StudentInfo" class="first">个人信息</a></li>
            <li><a href="#StudentScore" class="second">学生成绩</a></li>
            <li><a href="#MoralEduInfo" class="three">德育信息</a></li>
            <li><a href="#StudentActivity" class="four">学生活动</a></li>
            <li><a href="#ResearchStudy" class="five">研究性学习</a></li>
            <li><a href="#ElectiveClass" class="six">校本选修课</a></li>
            <li><a href="#AssociateInfo" class="seven">社团信息</a></li>
            <li><a href="#PrizeInfo" class="eight">获奖信息</a></li>
            <li><a href="#PhysicalHealth" class="nine">体质健康</a></li>
            <li><a href="#PracticeActivity" class="ten">实践信息</a></li>
            <li><a href="#PersonalPlan" class="eleven">个人规划</a></li>
        </ul>
    </div>
    <div class="content">
        <!--个人信息-->
        <div class="tc" style="display: none;" id="first">
            <div class="Personal_info">
                <asp:Image ID="Img_StudentInfo" runat="server" />
                <a onclick="showcontrol('#dvload')" id="asitechage" href="#">更换网站头像</a>
                <a onclick="showcontrol('#dvupload')" id="achage" href="#">更换照片</a>
            </div>
            <div id="dvupload">
                <asp:FileUpload ID="zpUpload" runat="server" />
                <asp:Button ID="Btn_ChangePic" runat="server" Text="上传照片" CssClass="btn_save" OnClick="Btn_ChangePic_Click" />
                <input type="button" value="取消" id="uploadCancel" onclick="hidecontrol('#dvupload')" />
            </div>
            <div id="dvload">
                <asp:FileUpload ID="zpload" runat="server" />
                <asp:Button ID="Btn_ChangeLittlePic" runat="server" Text="上传网站头像" CssClass="btn_save" OnClick="Btn_ChangeLittlePic_Click" />
                <input type="button" value="取消" id="loadCancel" onclick="hidecontrol('#dvload')" />
            </div>
            <asp:Panel ID="Pan_AddInfo" runat="server">
                <div class="formdv">
                    <table class="tabContent" style="display: block;">
                        <tr>
                            <th>个人格言：</th>
                            <td>
                                <asp:TextBox ID="TB_Maxim" runat="server"></asp:TextBox>
                                <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_Maxim" runat="server" ControlToValidate="TB_Maxim"
                                ErrorMessage="必填" ValidationGroup="InfoSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                            </td>
                        </tr>
                        <tr>
                            <th>爱好：</th>
                            <td>
                                <asp:TextBox ID="TB_Hobbies" runat="server"></asp:TextBox>
                                <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_Hobbies" runat="server" ControlToValidate="TB_Hobbies"
                                ErrorMessage="必填" ValidationGroup="InfoSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                            </td>
                        </tr>
                        <tr class="areatr">
                            <th>自我简介：</th>
                            <td>
                                <asp:TextBox ID="TB_SelfIntroduction" TextMode="MultiLine" Height="82px" runat="server"></asp:TextBox>
                                <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_SelfIntroduction" runat="server" ControlToValidate="TB_SelfIntroduction"
                                ErrorMessage="必填" ValidationGroup="InfoSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                            </td>
                        </tr>
                        <tr class="btntr">
                            <th></th>
                            <td>
                                <asp:Button ID="Btn_InfoSave" CssClass="save" runat="server" ValidationGroup="InfoSubmit" Text="保存" OnClick="Btn_InfoSave_Click" />
                                <asp:Button ID="Btn_InfoCancel" CssClass="cancel" runat="server" Text="取消" OnClick="Btn_InfoCancel_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>

            <asp:Panel ID="Pan_ShowInfo" runat="server">
                <div class="formdv">
                    <table class="tabContent" style="display: block;">
                        <tr>
                            <td>个人格言：</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="Lit_Maxim" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>爱好：</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="Lit_Hobbies" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>自我简介：</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="Lit_SelfIntroduction" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <asp:HiddenField ID="Hid_InfoId" runat="server" />
                                <asp:Button ID="Btn_EditInfo" CssClass="save" runat="server" Text="编辑个人信息" OnClick="Btn_EditInfo_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
        </div>

        <!--学生成绩-->
        <div class="tc" style="display: none;" id="second">
            <div class="formdv">
                <div class="topadd"><a href="#"><i class="iconfont">&#xe623;</i>添加学生成绩</a></div>
                <table class="tabContent">
                    <tr>
                        <th>考试名称：</th>
                        <td>
                            <asp:TextBox ID="TB_ExamTitle" runat="server"></asp:TextBox>
                            <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_ExamTitle" runat="server" ControlToValidate="TB_ExamTitle"
                                ErrorMessage="必填" ValidationGroup="ScoreSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                    <tr>
                        <th>语文：</th>
                        <td>
                            <asp:TextBox ID="TB_Chinese" runat="server"></asp:TextBox>
                            <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_Chinese" runat="server" ControlToValidate="TB_Chinese"
                                ErrorMessage="必填" ValidationGroup="ScoreSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                    <tr>
                        <th>数学：</th>
                        <td>
                            <asp:TextBox ID="TB_Math" runat="server"></asp:TextBox>
                            <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_Math" runat="server" ControlToValidate="TB_Math"
                                ErrorMessage="必填" ValidationGroup="ScoreSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                    <tr>
                        <th>英语：</th>
                        <td>
                            <asp:TextBox ID="TB_English" runat="server"></asp:TextBox>
                            <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_English" runat="server" ControlToValidate="TB_English"
                                ErrorMessage="必填" ValidationGroup="ScoreSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                    <tr>
                        <th>生物：</th>
                        <td>
                            <asp:TextBox ID="TB_Biology" runat="server"></asp:TextBox>
                            <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_Biology" runat="server" ControlToValidate="TB_Biology"
                                ErrorMessage="必填" ValidationGroup="ScoreSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                    <tr>
                        <th>物理：</th>
                        <td>
                            <asp:TextBox ID="TB_Physics" runat="server"></asp:TextBox>
                            <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_Physics" runat="server" ControlToValidate="TB_Physics"
                                ErrorMessage="必填" ValidationGroup="ScoreSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                    <tr>
                        <th>化学：</th>
                        <td>
                            <asp:TextBox ID="TB_Chemistry" runat="server"></asp:TextBox>
                            <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_Chemistry" runat="server" ControlToValidate="TB_Chemistry"
                                ErrorMessage="必填" ValidationGroup="ScoreSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                    <tr class="btntr">
                        <th></th>
                        <td>
                            <asp:Button ID="Btn_ScoreSave" CssClass="save" runat="server" ValidationGroup="ScoreSubmit" Text="保存" OnClick="Btn_ScoreSave_Click" />
                            <asp:Button ID="Btn_ScoreCancel" CssClass="cancel" runat="server" Text="取消" OnClick="Btn_ScoreCancel_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="content">
                <div class="container">
                    <ul class="list">
                        <asp:ListView ID="LV_StudentScore" runat="server" OnPagePropertiesChanging="LV_StudentScore_PagePropertiesChanging">
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
                                <li id="itemPlaceholder" runat="server"></li>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <li class="li_list">
                                    <div class="top_remarks">
                                        <div class="left_con fl">
                                            <asp:Label ID="lb_Learnyear" Text='<%# Eval("LearnYear") %>' CssClass="times" runat="server"></asp:Label><span class="con_details"></span></div>
                                    </div>
                                    <div class="con_text">
                                        <div class="con_con">
                                            <asp:ListView ID="LV_ExamInfo" runat="server" OnItemCommand="LV_ExamInfo_ItemCommand" OnItemEditing="LV_ExamInfo_ItemEditing">
                                                <EmptyDataTemplate>
                                                    暂无考试信息 
                                                </EmptyDataTemplate>
                                                <LayoutTemplate>
                                                    <table class="O_form">
                                                        <tr class="O_trth">
                                                            <!--表头tr名称-->
                                                            <td>考试类型</td>
                                                            <td>语文</td>
                                                            <td>数学</td>
                                                            <td>英语</td>
                                                            <td>生物</td>
                                                            <td>物理</td>
                                                            <td>化学</td>
                                                            <td>总成绩</td>
                                                            <td>平均成绩</td>
                                                            <td>操作</td>
                                                        </tr>
                                                        <asp:PlaceHolder runat="server" ID="itemPlaceholder" />

                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr class="Single">
                                                        <td><%#Eval("Title") %></td>
                                                        <td><%#Eval("Chinese") %></td>
                                                        <td><%#Eval("Math") %></td>
                                                        <td><%#Eval("English") %></td>
                                                        <td><%#Eval("Biology") %></td>
                                                        <td><%#Eval("Physics") %></td>
                                                        <td><%#Eval("Chemistry") %></td>
                                                        <td><%#Eval("TotalScore") %></td>
                                                        <td><%#Eval("AverageScore") %></td>
                                                        <td>
                                                            <div class="right_edit"><i class="iconfont">&#xe60a;</i><asp:LinkButton OnClientClick="shouwtab();" CommandName="Edit" CommandArgument='<%# Eval("ID") %>' ID="LB_Edit" runat="server">编辑</asp:LinkButton>|<i class="iconfont">&#xe62f;</i><asp:LinkButton CommandName="Del" CommandArgument='<%# Eval("ID") %>' ID="LB_Del" runat="server" OnClientClick="return confirm('你确定要删除吗？')">删除</asp:LinkButton></div>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr class="Double">
                                                        <td><%#Eval("Title") %></td>
                                                        <td><%#Eval("Chinese") %></td>
                                                        <td><%#Eval("Math") %></td>
                                                        <td><%#Eval("English") %></td>
                                                        <td><%#Eval("Biology") %></td>
                                                        <td><%#Eval("Physics") %></td>
                                                        <td><%#Eval("Chemistry") %></td>
                                                        <td><%#Eval("TotalScore") %></td>
                                                        <td><%#Eval("AverageScore") %></td>
                                                        <td>
                                                            <div class="right_edit"><i class="iconfont">&#xe60a;</i><asp:LinkButton OnClientClick="shouwtab();" CommandName="Edit" CommandArgument='<%# Eval("ID") %>' ID="LB_Edit" runat="server">编辑</asp:LinkButton>|<i class="iconfont">&#xe62f;</i><asp:LinkButton CommandName="Del" CommandArgument='<%# Eval("ID") %>' ID="LB_Del" runat="server" OnClientClick="return confirm('你确定要删除吗？')">删除</asp:LinkButton></div>
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                            </asp:ListView>
                                        </div>
                                    </div>
                                    <div class="boxmore"><a href="#"><span class="J-more">更多</span></a> </div>
                                </li>
                            </ItemTemplate>
                        </asp:ListView>
                    </ul>
                    <div class="paging">
                        <asp:DataPager ID="DP_StudentScore" runat="server" PageSize="5" PagedControlID="LV_StudentScore">
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
                            <%# (Container.TotalRowCount % Container.MaximumRows) > 0 ? Convert.ToInt32(Container.TotalRowCount / Container.MaximumRows) + 1 : Container.TotalRowCount / Container.MaximumRows%>  页
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

        <!--德育信息-->
        <div class="tc" style="display: none;" id="three">
            <div class="content">
                <div class="container">
                    <ul class="list">
                        <asp:ListView ID="LV_MoralEduInfo" runat="server" OnPagePropertiesChanging="LV_MoralEduInfo_PagePropertiesChanging">
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
                                <li id="itemPlaceholder" runat="server"></li>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <li class="li_list">
                                    <div class="top_remarks">
                                        <div class="left_con fl">
                                            <span class="times"><a href='#'><%# Eval("Title") %></a></span>
                                            <span class="con_details"><em><%# Eval("Score") %></em></span>
                                        </div>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:ListView>
                    </ul>
                    <div class="paging">
                        <asp:DataPager ID="DP_MoralEduInfo" runat="server" PageSize="5" PagedControlID="LV_MoralEduInfo">
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
                            <%# (Container.TotalRowCount % Container.MaximumRows) > 0 ? Convert.ToInt32(Container.TotalRowCount / Container.MaximumRows) + 1 : Container.TotalRowCount / Container.MaximumRows%>  页
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

        <!--学生活动-->
        <div class="tc" style="display: none;" id="four">
            <div class="content">
                <div class="container">
                    <ul class="list">
                        <asp:ListView ID="LV_StudentActivity" runat="server" OnPagePropertiesChanging="LV_StudentActivity_PagePropertiesChanging">
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
                                <li id="itemPlaceholder" runat="server"></li>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <li class="allxxk_list" style="float: left;">
                                    <ul>
                                        <li class="sb_fz01">
                                            <img src='<%# Eval("Attachment") %>' alt=""></li>
                                        <li class="sb_fz02">
                                            <div class="lf_tp">
                                                <img src='<%# Eval("Attachment") %>' alt="">
                                                <h3><%# Eval("Author") %></h3>
                                            </div>
                                            <div class="rf_xq">
                                                <h3><a href='#' style="color: #fff;"><%# Eval("Title") %></a></h3>
                                                <p><%# Eval("Introduction") %></p>
                                            </div>
                                        </li>
                                        <li class="sb_fz03">
                                            <h2><a href="#"><%# Eval("Title") %></a></h2>
                                        </li>
                                    </ul>
                                </li>
                            </ItemTemplate>
                        </asp:ListView>
                    </ul>
                    <div class="paging">
                        <asp:DataPager ID="DP_StudentActivity" runat="server" PageSize="5" PagedControlID="LV_StudentActivity">
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
                            <%# (Container.TotalRowCount % Container.MaximumRows) > 0 ? Convert.ToInt32(Container.TotalRowCount / Container.MaximumRows) + 1 : Container.TotalRowCount / Container.MaximumRows%>  页
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

        <!--研究性学习-->
        <div class="tc" style="display: none;" id="five">
            <div class="content">
                <div class="container">
                    <ul class="list">
                        <asp:ListView ID="LV_ResearchStudy" runat="server" OnPagePropertiesChanging="LV_ResearchStudy_PagePropertiesChanging">
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
                                <li id="itemPlaceholder" runat="server"></li>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <li class="li_list">
                                    <div class="top_remarks">
                                        <div class="left_con fl">
                                            <span class="times"><a href='#'><%# Eval("Title") %></a></span>
                                            <span class="con_details"><em><%# Eval("SubjectType") %></em>|<em><%# Eval("InstructorTea") %></em>|<em><%# Eval("BeginDate") %>至  <%# Eval("EndDate") %></em></span>
                                        </div>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:ListView>
                    </ul>
                    <div class="paging">
                        <asp:DataPager ID="DP_ResearchStudy" runat="server" PageSize="5" PagedControlID="LV_ResearchStudy">
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
                            <%# (Container.TotalRowCount % Container.MaximumRows) > 0 ? Convert.ToInt32(Container.TotalRowCount / Container.MaximumRows) + 1 : Container.TotalRowCount / Container.MaximumRows%>  页
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

        <!--校本选修课-->
        <div class="tc" style="display: none;" id="six">
            <div style="margin: 0 auto; min-height: 715px;">
                <div class="container">
                    <ul class="list">
                        <asp:ListView ID="LV_ElectiveClass" runat="server">
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
                                <li id="itemPlaceholder" runat="server"></li>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <li class="li_list">
                                    <div class="music_kc">
                                        <img src="/_layouts/15/Stu_images/zs28.jpg" alt="">
                                        <div class="music_nr">
                                            <h2><%# Eval("Title") %></h2>
                                            <div>
                                                <span>课程类别：<%# Eval("CourseType") %></span>
                                                <span>上课场地：<%# Eval("CourseAddress") %></span>
                                                <span>上课时间：<%# Eval("CourseDate") %></span>
                                                <span>硬件要求：音响、多媒体</span>
                                            </div>
                                            <p><%# Eval("CourseDescription") %></p>
                                            <div>附件：<a href="">课程评价标准.doc</a></div>
                                        </div>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:ListView>
                    </ul>
                </div>
            </div>
        </div>

        <!--社团信息-->
        <div class="tc" style="display: none;" id="seven">
            <div class="content">
                <div class="container">
                    <ul class="list">
                        <asp:ListView ID="LV_AssociateInfo" runat="server" OnPagePropertiesChanging="LV_AssociateInfo_PagePropertiesChanging">
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
                                <li id="itemPlaceholder" runat="server"></li>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <li class="allxxk_list" style="float: left;">
                                    <ul>
                                        <li class="sb_fz01">
                                            <img src='<%# Eval("Attachment") %>' alt=""></li>
                                        <li class="sb_fz02">
                                            <div class="lf_tp">
                                                <img src='<%# Eval("Attachment") %>' alt="">
                                                <h3><%# Eval("Leader") %></h3>
                                            </div>
                                            <div class="rf_xq">
                                                <h3><a href='<%=AssociateUrl %>/SitePages/AssociationShow.aspx?itemid=<%# Eval("ID") %>' style="color: #fff;"><%# Eval("Title") %></a></h3>
                                                <p><%# Eval("Introduce") %></p>
                                            </div>
                                        </li>
                                        <li class="sb_fz03">
                                            <h2><a href='<%=AssociateUrl %>/SitePages/AssociationShow.aspx?itemid=<%# Eval("ID") %>'><%# Eval("Title") %></a></h2>
                                        </li>
                                    </ul>
                                </li>
                            </ItemTemplate>
                        </asp:ListView>
                    </ul>
                    <div class="paging">
                        <asp:DataPager ID="DP_AssociateInfo" runat="server" PageSize="5" PagedControlID="LV_AssociateInfo">
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
                            <%# (Container.TotalRowCount % Container.MaximumRows) > 0 ? Convert.ToInt32(Container.TotalRowCount / Container.MaximumRows) + 1 : Container.TotalRowCount / Container.MaximumRows%>  页
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

        <!--获奖信息-->
        <div class="tc" style="display: none;" id="eight">
            <div class="formdv">
                <div class="topadd"><a href="#"><i class="iconfont">&#xe623;</i>添加获奖信息</a></div>
                <table class="tabContent">
                    <tr>
                        <th>获奖名称：</th>
                        <td>
                            <asp:TextBox ID="TB_Title" runat="server"></asp:TextBox>
                            <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_Title" runat="server" ControlToValidate="TB_Title"
                                ErrorMessage="必填" ValidationGroup="PrizeSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                    <tr>
                        <th>获奖日期：</th>
                        <td>
                            <asp:TextBox ID="TB_PrizeDate" CssClass="Wdate" onclick="javascript: WdatePicker()" runat="server"></asp:TextBox>
                            <span style="color: red;">*<asp:RequiredFieldValidator ID="RFV_PrizeDate" runat="server" ControlToValidate="TB_PrizeDate"
                                ErrorMessage="必填" ValidationGroup="PrizeSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                    <tr>
                        <th>获奖类型：</th>
                        <td>
                            <asp:DropDownList ID="DDL_PrizeType" runat="server"></asp:DropDownList>

                            <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_PrizeType" runat="server" ControlToValidate="DDL_PrizeType"
                                ErrorMessage="必填" ValidationGroup="PrizeSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>

                    <tr>
                        <th>获奖级别：</th>
                        <td>
                            <asp:DropDownList ID="DDL_PrizeGrade" runat="server"></asp:DropDownList>

                            <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_PrizeGrade" runat="server" ControlToValidate="DDL_PrizeGrade"
                                ErrorMessage="必填" ValidationGroup="PrizeSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                    <tr>
                        <th>获奖等级：</th>
                        <td>
                            <asp:DropDownList ID="DDL_PrizeLevel" runat="server"></asp:DropDownList>

                            <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_PrizeLevel" runat="server" ControlToValidate="DDL_PrizeLevel"
                                ErrorMessage="必填" ValidationGroup="PrizeSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                    <tr>
                        <th>颁奖单位：</th>
                        <td>
                            <asp:TextBox ID="TB_PrizeUnit" runat="server"></asp:TextBox>
                            <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_PrizeUnit" runat="server" ControlToValidate="TB_PrizeUnit"
                                ErrorMessage="必填" ValidationGroup="PrizeSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                    <tr class="areatr">
                        <th>获奖感言：</th>
                        <td>
                            <asp:TextBox ID="TB_PrizeThankful" TextMode="MultiLine" Height="82px" runat="server"></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <th>附件：</th>
                        <td>
                            <asp:Label ID="lbAttach3" runat="server">
                            </asp:Label>
                            <asp:HiddenField ID="Hid_fileName3" runat="server" />
                            <table id="idAttachmentsTable3" style="display: block;">
                                <asp:Literal ID="Lit_Bind3" runat="server"></asp:Literal>
                            </table>
                            <table style="width: 100%; display: block;">
                                <tr>
                                    <td id="att3">&nbsp;
                                            <input id="filePrize0" name="filePrize0" style="width: 80%;" type="file" />
                                        <%--<asp:FileUpload ID="FU_TermFile" runat="server" />--%>
                                    </td>
                                    <td>
                                        <input type="button" onclick="UploadFile('filePrize', 'idAttachmentsTable3', 'att3')" style="width: 70px;" value="上传" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr class="btntr">
                        <th></th>
                        <td>
                            <asp:Button ID="Btn_PrizeSave" CssClass="save" runat="server" ValidationGroup="PrizeSubmit" Text="保存" OnClick="Btn_PrizeSave_Click" />
                            <asp:Button ID="Btn_PrizeCancel" CssClass="cancel" runat="server" Text="取消" OnClick="Btn_PrizeCancel_Click" />

                        </td>
                    </tr>
                </table>
            </div>
            <div class="content">
                <div class="container">
                    <ul class="list">
                        <asp:ListView ID="LV_Prize" runat="server" OnPagePropertiesChanging="LV_Prize_PagePropertiesChanging" OnItemCommand="LV_Prize_ItemCommand" OnItemEditing="LV_Prize_ItemEditing">
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
                                <li id="itemPlaceholder" runat="server"></li>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <li class="li_list">
                                    <div class="top_remarks">
                                        <div class="left_con fl"><span class="times"><%# Eval("PrizeDate") %></span><span class="con_details"><em><%# Eval("PrizeGrade") %></em>|<em><%# Eval("PrizeLevel") %></em>|<em><%# Eval("PrizeUnit") %></em></span></div>
                                        <div class="right_edit fr"><i class="iconfont">&#xe60a;</i><asp:LinkButton OnClientClick="shouwtab();" CommandName="Edit" CommandArgument='<%# Eval("ID") %>' ID="LB_Edit" runat="server">编辑</asp:LinkButton>|<i class="iconfont">&#xe62f;</i><asp:LinkButton CommandName="Del" CommandArgument='<%# Eval("ID") %>' ID="LB_Del" runat="server" OnClientClick="return confirm('你确定要删除吗？')">删除</asp:LinkButton></div>
                                    </div>
                                    <div class="con_text">
                                        <h2><%# Eval("Title") %>  (<%#Eval("ExamineStatus")%>)</h2>
                                        <div class="con_con">
                                            <p>
                                                <%# Eval("PrizeThankful") %>
                                            </p>
                                            <div class="attachment">附件：<%# Eval("Attachment") %></div>
                                            <%--<div class="attachment"><a href="">附件：<%# Eval("Attachment") %></a></div>--%>
                                        </div>
                                    </div>
                                    <div class="boxmore"><a href="#"><span class="J-more">更多</span></a> </div>
                                </li>
                            </ItemTemplate>
                        </asp:ListView>
                    </ul>
                    <div class="paging">
                        <asp:DataPager ID="DP_Prize" runat="server" PageSize="5" PagedControlID="LV_Prize">
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
                            <%# (Container.TotalRowCount % Container.MaximumRows) > 0 ? Convert.ToInt32(Container.TotalRowCount / Container.MaximumRows) + 1 : Container.TotalRowCount / Container.MaximumRows%>  页
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

        <!--体质健康-->
        <div class="tc" style="display: none;" id="nine">
            <div class="formdv">
                <div class="topadd"><a href="#"><i class="iconfont">&#xe623;</i>添加体质健康</a></div>
                <table class="tabContent">
                    <tr>
                        <th>身体状况：</th>
                        <td>
                            <asp:DropDownList ID="DDL_Contiditon" runat="server"></asp:DropDownList>

                            <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_Contiditon" runat="server" ControlToValidate="DDL_Contiditon"
                                ErrorMessage="必填" ValidationGroup="HealthSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                    <tr>
                        <th>身高：</th>
                        <td>
                            <asp:TextBox ID="TB_Height" runat="server"></asp:TextBox>
                            <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_Height" runat="server" ControlToValidate="TB_Height"
                                ErrorMessage="必填" ValidationGroup="HealthSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                    <tr>
                        <th>体重：</th>
                        <td>
                            <asp:TextBox ID="TB_Weight" runat="server"></asp:TextBox>
                            <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_Weight" runat="server" ControlToValidate="TB_Weight"
                                ErrorMessage="必填" ValidationGroup="HealthSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                    <tr>
                        <th>视力：</th>
                        <td>
                            <asp:TextBox ID="TB_Eyesight" runat="server"></asp:TextBox>
                            <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_Eyesight" runat="server" ControlToValidate="TB_Eyesight"
                                ErrorMessage="必填" ValidationGroup="HealthSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                    <tr>
                        <th>血压：</th>
                        <td>
                            <asp:TextBox ID="TB_BloodPress" runat="server"></asp:TextBox>
                            <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_BloodPress" runat="server" ControlToValidate="TB_BloodPress"
                                ErrorMessage="必填" ValidationGroup="HealthSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                    <tr>
                        <th>肺活量：</th>
                        <td>
                            <asp:TextBox ID="TB_VitalCapacity" runat="server"></asp:TextBox>
                            <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_VitalCapacity" runat="server" ControlToValidate="TB_VitalCapacity"
                                ErrorMessage="必填" ValidationGroup="HealthSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>

                    <tr>
                        <th>有无遗传病史：</th>
                        <td>
                            <asp:RadioButton ID="RB_NoHis" GroupName="DiseaseHis" Checked="true" runat="server" Text="无" />
                            <asp:RadioButton ID="RB_HasHis" GroupName="DiseaseHis" runat="server" Text="有" />
                        </td>
                    </tr>
                    <tr class="areatr">
                        <th>身体状况总结：</th>
                        <td>
                            <asp:TextBox ID="TB_HealthSummary" TextMode="MultiLine" Height="82px" runat="server"></asp:TextBox>

                        </td>
                    </tr>
                    <tr class="btntr">
                        <th></th>
                        <td>
                            <asp:Button ID="Btn_HealthSave" CssClass="save" runat="server" ValidationGroup="HealthSubmit" Text="保存" OnClick="Btn_HealthSave_Click" />
                            <asp:Button ID="Btn_HealthCancel" CssClass="cancel" runat="server" Text="取消" OnClick="Btn_HealthCancel_Click" />

                        </td>
                    </tr>
                </table>
            </div>
            <div class="content">
                <div class="container">
                    <ul class="list">
                        <asp:ListView ID="LV_PhysicalHealth" runat="server" OnPagePropertiesChanging="LV_PhysicalHealth_PagePropertiesChanging" OnItemCommand="LV_PhysicalHealth_ItemCommand" OnItemEditing="LV_PhysicalHealth_ItemEditing">
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
                                <li id="itemPlaceholder" runat="server"></li>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <li class="li_list">
                                    <div class="top_remarks">
                                        <div class="left_con fl"><span class="times"><%# Eval("LearnYear") %></span><span class="con_details"><em><%# Eval("Contiditon") %></em></span></div>
                                        <div class="right_edit fr"><i class="iconfont">&#xe60a;</i><asp:LinkButton OnClientClick="shouwtab();" CommandName="Edit" CommandArgument='<%# Eval("ID") %>' ID="LB_Edit" runat="server">编辑</asp:LinkButton></div>
                                    </div>
                                    <div class="con_text">
                                        <div class="con_con">
                                            <table class="health_table">
                                                <tr>
                                                    <td>身高</td>
                                                    <td><%# Eval("Height") %></td>
                                                    <td>体重</td>
                                                    <td><%# Eval("Weight") %></td>
                                                    <td>视力</td>
                                                    <td><%# Eval("Eyesight") %></td>
                                                </tr>
                                                <tr>
                                                    <td>血压</td>
                                                    <td><%# Eval("BloodPress") %></td>
                                                    <td>肺活量</td>
                                                    <td><%# Eval("VitalCapacity") %></td>
                                                    <td>有无遗传病史</td>
                                                    <td><%# Eval("HasDiseaseHis") %></td>
                                                </tr>
                                                <tr>
                                                    <td>身体状况总结</td>
                                                    <td colspan="5" style="text-align: left;">
                                                        <p><%# Eval("HealthSummary") %>   </p>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="boxmore"><a href="#"><span class="J-more">更多</span></a> </div>
                                </li>
                            </ItemTemplate>
                        </asp:ListView>
                    </ul>
                    <div class="paging">
                        <asp:DataPager ID="DP_PhysicalHealth" runat="server" PageSize="5" PagedControlID="LV_PhysicalHealth">
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
                            <%# (Container.TotalRowCount % Container.MaximumRows) > 0 ? Convert.ToInt32(Container.TotalRowCount / Container.MaximumRows) + 1 : Container.TotalRowCount / Container.MaximumRows%>  页
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

        <!--实践信息-->
        <div class="tc" style="display: none;" id="ten">
            <div class="formdv">
                <div class="topadd"><a href="#"><i class="iconfont">&#xe623;</i>添加实践活动</a></div>
                <table class="tabContent">
                    <tr>
                        <th>活动名称：</th>
                        <td>
                            <asp:TextBox ID="TB_ActTitle" runat="server"></asp:TextBox>
                            <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_ActTitle" runat="server" ControlToValidate="TB_ActTitle"
                                ErrorMessage="必填" ValidationGroup="PracticeSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                    <tr>
                        <th>活动时间：</th>
                        <td>
                            <asp:TextBox ID="TB_ActiveDate" CssClass="Wdate" onclick="javascript: WdatePicker()" runat="server"></asp:TextBox>
                            <span style="color: red;">*<asp:RequiredFieldValidator ID="RFV_ActiveDate" runat="server" ControlToValidate="TB_ActiveDate"
                                ErrorMessage="必填" ValidationGroup="PracticeSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                    <tr>
                        <th>活动地址：</th>
                        <td>
                            <asp:TextBox ID="TB_Address" runat="server"></asp:TextBox>
                            <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_Address" runat="server" ControlToValidate="TB_Address"
                                ErrorMessage="必填" ValidationGroup="PracticeSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                    <tr class="areatr">
                        <th>活动内容：</th>
                        <td>
                            <asp:TextBox ID="TB_ActiveContent" TextMode="MultiLine" Height="82px" runat="server"></asp:TextBox>
                            <span style="color: red;">*<asp:RequiredFieldValidator ID="RFV_ActiveContent" runat="server" ControlToValidate="TB_ActiveContent"
                                ErrorMessage="必填" ValidationGroup="PracticeSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                    <tr class="btntr">
                        <th></th>
                        <td>
                            <asp:Button ID="Btn_PracticeSave" CssClass="save" runat="server" ValidationGroup="PracticeSubmit" Text="保存" OnClick="Btn_PracticeSave_Click" />
                            <asp:Button ID="Btn_PracticeCancel" CssClass="cancel" runat="server" Text="取消" OnClick="Btn_PracticeCancel_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="content">
                <div class="container">
                    <ul class="list">
                        <asp:ListView ID="LV_PracticeActivity" runat="server" OnPagePropertiesChanging="LV_PracticeActivity_PagePropertiesChanging" OnItemCommand="LV_PracticeActivity_ItemCommand" OnItemEditing="LV_PracticeActivity_ItemEditing">
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
                                <li id="itemPlaceholder" runat="server"></li>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <li class="li_list">
                                    <div class="top_remarks">
                                        <div class="left_con fl"><span class="times"><%# Eval("Title") %></span><span class="con_details"><em> <%# Convert.ToDateTime(Eval("ActiveDate")).ToString("yyyy-MM-dd")%></em>|<em><%# Eval("Address") %></em></span></div>
                                        <div class="right_edit fr"><i class="iconfont">&#xe60a;</i><asp:LinkButton OnClientClick="shouwtab();" CommandName="Edit" CommandArgument='<%# Eval("ID") %>' ID="LB_Edit" runat="server">编辑</asp:LinkButton>|<i class="iconfont">&#xe62f;</i><asp:LinkButton CommandName="Del" CommandArgument='<%# Eval("ID") %>' ID="LB_Del" runat="server" OnClientClick="return confirm('你确定要删除吗？')">删除</asp:LinkButton></div>
                                    </div>
                                    <div class="con_text">
                                        <div class="con_con">
                                            活动内容：<p>
                                                <%# Eval("ActiveContent") %>
                                            </p>
                                            教师评价：<p>
                                                <%# Eval("EvaluateContent") %>
                                            </p>
                                        </div>
                                    </div>
                                    <div class="boxmore"><a href="#"><span class="J-more">更多</span></a> </div>
                                </li>
                            </ItemTemplate>
                        </asp:ListView>
                    </ul>
                    <div class="paging">
                        <asp:DataPager ID="DP_PracticeActivity" runat="server" PageSize="5" PagedControlID="LV_PracticeActivity">
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
                            <%# (Container.TotalRowCount % Container.MaximumRows) > 0 ? Convert.ToInt32(Container.TotalRowCount / Container.MaximumRows) + 1 : Container.TotalRowCount / Container.MaximumRows%>  页
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

        <!--个人规划-->
        <div class="tc" style="display: none;" id="eleven">
            <div class="Query">
                <ul>
                    <li>
                        <asp:DropDownList CssClass="option" ID="DDL_LearnYear" runat="server"></asp:DropDownList>
                    </li>
                    <li>
                        <asp:Button ID="Btn_Search" runat="server" Text="搜索" OnClick="Btn_Search_Click" CssClass="sea" />
                    </li>
                </ul>
            </div>
            <div class="formdv">
                <div class="topadd"><a href="#"><i class="iconfont">&#xe623;</i>添加个人规划</a></div>
                <table class="tabContent">
                    <tr>
                        <th>计划名称：</th>
                        <td>
                            <asp:TextBox ID="TB_PlanTitle" runat="server"></asp:TextBox>
                            <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_PlanTitle" runat="server" ControlToValidate="TB_PlanTitle"
                                ErrorMessage="必填" ValidationGroup="PlanSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                    <tr class="areatr">
                        <th>计划内容：</th>
                        <td>
                            <asp:TextBox ID="TB_PlanContent" TextMode="MultiLine" Height="82px" runat="server"></asp:TextBox>
                            <span style="color: red;">*<asp:RequiredFieldValidator ID="RFV_PlanContent" runat="server" ControlToValidate="TB_PlanContent"
                                ErrorMessage="必填" ValidationGroup="PlanSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                    <tr class="btntr">
                        <th></th>
                        <td>
                            <asp:Button ID="Btn_PlanSave" CssClass="save" runat="server" ValidationGroup="PlanSubmit" Text="保存" OnClick="Btn_PlanSave_Click" />
                            <asp:Button ID="Btn_PlanCancel" CssClass="cancel" runat="server" Text="取消" OnClick="Btn_PlanCancel_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="content">
                <div class="container">
                    <ul class="list">
                        <asp:ListView ID="LV_PersonalPlan" runat="server" OnPagePropertiesChanging="LV_PersonalPlan_PagePropertiesChanging" OnItemCommand="LV_PersonalPlan_ItemCommand" OnItemEditing="LV_PersonalPlan_ItemEditing">
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
                                <li id="itemPlaceholder" runat="server"></li>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <li class="li_list">
                                    <div class="top_remarks">
                                        <div class="left_con fl"><span class="times"><%# Eval("LearnYear") %></span><span class="con_details"><em><%# Eval("Title") %></em>|<em> <%# Convert.ToDateTime(Eval("Created")).ToString("yyyy-MM-dd")%></em>|<em><asp:Label ID="LB_Status" runat="server" Text='<%# Eval("SubmitStatus") %>'></asp:Label></em></span></div>
                                    </div>
                                    <div class="con_text">
                                        <div class="top_remarks" style="background: none;">
                                            <div class="right_edit fr">
                                                <i class="iconfont">&#xe60a;</i><asp:LinkButton OnClientClick="shouwtab();" CommandName="Edit" CommandArgument='<%# Eval("ID") %>' ID="LB_Edit" runat="server">编辑</asp:LinkButton>
                                                <asp:Label runat="server" ID="lb_IsDel">|<i class="iconfont">&#xe62f;</i><asp:LinkButton CommandName="Del" CommandArgument='<%# Eval("ID") %>' ID="LB_Del" runat="server" OnClientClick="return confirm('你确定要删除吗？')">删除</asp:LinkButton></asp:Label>
                                                <asp:Label runat="server" ID="lb_IsSub">|<i class="iconfont">&#xe65a;</i><asp:LinkButton CommandName="Sub" CommandArgument='<%# Eval("ID") %>' ID="LB_Sub" runat="server" OnClientClick="return confirm('提交后将不能删除，你确定要提交给老师吗？')">提交</asp:LinkButton></asp:Label>
                                                <asp:HiddenField ID="DetailID" runat="server" Value='<%# Eval("ID") %>' />
                                                |<i class="iconfont">&#xe625;</i><asp:LinkButton CommandName="Record" CommandArgument='<%# Eval("ID") %>' ID="LB_Record" runat="server" OnClientClick="editpdetail(this,'个人计划记录','/SitePages/AddPlanRecord.aspx?itemid=','600','320');return false;">个人纪录</asp:LinkButton>
                                                |<i class="iconfont">&#xe639;</i><asp:LinkButton CommandName="Summary" CommandArgument='<%# Eval("ID") %>' ID="LB_Summary" runat="server" OnClientClick="editpdetail(this,'计划总结','/SitePages/PlanOperate.aspx?flag=stuSum&itemid=','600','470');return false;">个人总结</asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="con_con">
                                            计划内容：<p>
                                                <%# Eval("PlanContent") %>
                                            </p>
                                            个人纪录：<asp:ListView ID="LV_PlanRecord" runat="server">
                                                <EmptyDataTemplate>
                                                    <p>暂无个人纪录</p>
                                                </EmptyDataTemplate>
                                                <LayoutTemplate>
                                                    <table class="O_form">
                                                        <tr class="O_trth">
                                                            <!--表头tr名称-->
                                                            <td>文字记录</td>
                                                            <td>附件</td>                                                                                                                
                                                        </tr>
                                                        <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr class="Single">
                                                        <td><%#Eval("WordRecord") %></td>
                                                        <td><%#Eval("Attachment") %></td>                                                        
                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr class="Double">
                                                        <td><%#Eval("WordRecord") %></td>
                                                        <td><%#Eval("Attachment") %></td>                                                                                            
                                                    </tr>
                                                </AlternatingItemTemplate>
                                            </asp:ListView>
                                            教师点评：<p>
                                                <%# Eval("CommentContent") %>
                                            </p>
                                        </div>
                                    </div>
                                    <div class="boxmore"><a href="#"><span class="J-more">更多</span></a> </div>
                                </li>
                            </ItemTemplate>
                        </asp:ListView>
                    </ul>
                    <div class="paging">
                        <asp:DataPager ID="DP_PersonalPlan" runat="server" PageSize="5" PagedControlID="LV_PersonalPlan">
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
                            <%# (Container.TotalRowCount % Container.MaximumRows) > 0 ? Convert.ToInt32(Container.TotalRowCount / Container.MaximumRows) + 1 : Container.TotalRowCount / Container.MaximumRows%>  页
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

