<%@ Page Title="" Language="C#" MasterPageFile="~/Moblie.Master" AutoEventWireup="true" CodeBehind="TeachActive.aspx.cs" Inherits="Moblie.TeachActive" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="js/layer/skin/layer.css" rel="stylesheet" />
    <link href="js/layer/skin/layer.ext.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <script src="js/layer.js"></script>
    <script src="js/OpenLayer.js"></script>
    <style>
        .kaiKeDate {
            font-size: 12px;
            font-family: "微软雅黑","宋体";
            color: #999;
        }
    </style>
    <script>

        function UpdatePwd() {

            OL_ShowLayerNo(2, "发送邮件", 370, 240, "http://61.50.119.70:1122//sites/StudentEducation/_layouts/15/YHSD.VocationalEducation.Portal/SendEmail.aspx", function (returnVal) {
            });
        }
        function Export() {
            window.open("../../../_layouts/15/YHSD.VocationalEducation.Portal/images/幼师.jpg", "_blank");
        }
        function Chate() {
            OL_ShowLayerNo(2, "班级聊天室", 570, 530, "http://61.50.119.70:1060/SelectChatRoom.aspx?name='学生一'", function (returnVal) {
        });
    }
    function CreateKnowledgeLib(text) {

        OL_ShowLayerNo(2, "查看知识点", 400, 200, "http://61.50.119.70:1122//sites/StudentEducation/_layouts/15/YHSD.VocationalEducation.Portal/KnowledgeLibAdd.aspx?itemid=" + text, function (returnVal) {
        });
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">


    <div class="student_main">
        <div class="f_l">
            <div class="classes_main">
                <div class="classes_title">
                    <div class="title"><a href="http://61.50.119.70:1122//sites/StudentEducation/SitePages/MySurveyPaper.aspx">问卷调查</a></div>
                </div>
                <div class="classes_content">
                    <div class="main_list_table" style="width: 535px">
                        <table class="list_table" cellspacing="0" cellpadding="0" border="0" style="width: 100%; text-align: center;">
                            <tr class="tab_th top_th" style="height: 40px; line-height: 40px;">
                                <th class="th_tit_left">问卷名称</th>
                                <th class="th_tit_left">问卷类别</th>
                                <th class="th_tit_left">评价类型</th>
                                <th class="th_tit_left">开始日期</th>
                                <th class="th_tit_left">截止日期</th>
                                <th class="th_tit_left">参与者</th>
                                <th class="th_tit_left">是否启用</th>
                            </tr>

                            <tr class="tab_td" style="height: 40px; line-height: 40px;">
                                <td class="td_tit_left">我要做个好人</td>
                                <td class="td_tit_left">品德调查</td>
                                <td class="td_tit_left">学生评教师</td>
                                <td class="td_tit_left">2015-11-28</td>
                                <td class="td_tit_left">2015-12-05</td>
                                <td class="td_tit_left">学生组</td>
                                <td class="td_tit_left">启用</td>
                            </tr>
                            <tr class="tab_td" style="height: 40px; line-height: 40px;">
                                <td class="td_tit_left">在线学习</td>
                                <td class="td_tit_left">在线学习看法</td>
                                <td class="td_tit_left">学生填写</td>
                                <td class="td_tit_left">2015-11-28</td>
                                <td class="td_tit_left">2015-12-05</td>
                                <td class="td_tit_left">学生组</td>
                                <td class="td_tit_left">启用</td>
                            </tr>

                        </table>
                    </div>
                </div>
            </div>
            <div class="classes_main">
                <div class="classes_title">
                    <div class="title">知识库</div>
                </div>
                <div title="维护知识库" style="padding: 10px">
                    <div id="head" class="TopToolbar">
                        <span>问题&nbsp</span>
                        <asp:TextBox ID="TB_Question" runat="server" Style="width: 198px; height: 22px; border: 1px solid #dedede;"></asp:TextBox>
                        <asp:Button ID="searchButton" CssClass="button_s" runat="server" Text="查询" OnClick="BTSearch_Click" />
                    </div>
                    <div class="main_list_table" style="width: 535px">
                        <table class="list_table" cellspacing="0" cellpadding="0" border="0" style="width: 535px">
                            <tr class="tab_th top_th">
                                <th class="th_tit">序号</th>
                                <th class="th_tit_left">问题</th>
                                <th class="th_tit_left">答案</th>
                                <th class="th_tit_left">创建时间</th>
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

                                    </tr>
                                </ItemTemplate>
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
            </div>
            <div class="all_classes last_classes">
                <div class="classes_title" style="cursor: pointer" onclick="javascript:window.location.href='http://61.50.119.70:1122//sites/StudentEducation/Lists/List6/AllItems.aspx'">
                    <div class="title"><a href="http://61.50.119.70:1122//sites/StudentEducation/Lists/List6/AllItems.aspx">进入班级论坛</a></div>
                </div>
                <div class="classes_content">
                    <div class="main_list_table" style="width: 535px">
                        <table class="list_table" cellspacing="0" cellpadding="0" border="0" style="width: 100%; text-align: center;">
                            <tr class="tab_th top_th" style="height: 40px; line-height: 40px;">
                                <th class="th_tit_left">论坛主题</th>
                                <th class="th_tit_left">创建时间</th>

                            </tr>

                            <tr class="tab_td" style="height: 40px; line-height: 40px;">
                                <td>个人成长培训心得</td>
                                <td>2015-12-1</td>
                            </tr>
                            <tr class="tab_td" style="height: 40px; line-height: 40px;">
                                <td>最新学习心得</td>
                                <td>2015-12-1</td>
                            </tr>

                        </table>
                    </div>
                    <div class="clear"></div>

                </div>
            </div>


            <div class="all_classes last_classes">
                <div class="classes_title">
                    <div class="title"><a href="http://61.50.119.70:1122/Lists/List21/AllItems.aspx">进入在线答疑</a></div>
                </div>
                <div class="classes_content">
                    <div class="main_list_table" style="width: 535px">
                        <table class="list_table" cellspacing="0" cellpadding="0" border="0" style="width: 100%; text-align: center;">
                            <tr class="tab_th top_th" style="height: 40px; line-height: 40px;">
                                <th class="th_tit_left">问题</th>
                                <th class="th_tit_left">创建时间</th>

                            </tr>

                            <tr class="tab_td" style="height: 40px; line-height: 40px;">
                                <td>数学第一节第一题</td>
                                <td>2015-12-1</td>
                            </tr>
                            <tr class="tab_td" style="height: 40px; line-height: 40px;">
                                <td>数学第一节第二题</td>
                                <td>2015-12-1</td>
                            </tr>

                        </table>
                    </div>
                    <div class="clear"></div>

                </div>
            </div>

        </div>
        <div class="right_part f_r">
            <input type="button" value="新建邮件" onclick="UpdatePwd()" style="height: 31px; line-height: 29px; background: url(../images/btn_bg.png) repeat-x; font-size: 13px; color: #49b700; font-weight: bold; border-radius: 4px; border: 1px solid #d5d5d5; cursor: pointer; padding: 0px; margin-left: 0px;" />
            <input type="button" value="班级聊天室" onclick="Chate()" style="height: 31px; line-height: 29px; background: url(../images/btn_bg.png) repeat-x; font-size: 13px; color: #49b700; font-weight: bold; border-radius: 4px; border: 1px solid #d5d5d5; cursor: pointer; padding: 0px;" />

            <div class="class_library" style="margin-top: 20px;">
                <div class="library_title">
                    <div class="title">最新作业考试</div>
                </div>
                <div class="library_main">
                    <label runat="server" id="LabelExperience" />
                </div>
            </div>
            <div class="class_library">
                <div class="library_title">
                    <div class="title">最新考试批改</div>
                </div>
                <div class="library_main">
                    <label runat="server" id="Label2" />
                </div>
            </div>
            <div class="class_library">
                <div class="library_title">
                    <div class="title">班级通知</div>
                </div>
                <div class="library_main">
                    <label runat="server" id="LabelbanjiTongZhi" />

                </div>
            </div>
        </div>
        <div class="clear"></div>
    </div>
</label>
</label>
</label>
</asp:Content>
