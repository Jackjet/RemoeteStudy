<%@ Page Title="" Language="C#" MasterPageFile="~/Moblie.Master" AutoEventWireup="true" CodeBehind="blog.aspx.cs" Inherits="Moblie.blog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <style>
    .kaiKeDate {
        font-size: 12px;
        font-family: "微软雅黑","宋体";
        color: #999;
    }
</style>
<script>

    function EditUser(id) {
        OL_ShowLayerNo(2, "编辑个人信息", 770, 400, "~/_layouts/15/YHSD.VocationalEducation.Portal/UpdateUser.aspx?id=" + id, function (returnVal) {
        });

    }
    function Export() {
        window.open("../../../_layouts/15/YHSD.VocationalEducation.Portal/images/幼师.jpg", "_blank");
    }
</script>
<div class="student_main">
    <div class="f_l">
        <div class="i-tab-title">
                <h2 class="title current_title">我的课程</h2>
                <div class="tab-btn" id="tab1">
                        <p class="mg-auto clearfix">
                            <a href="javascript:;" id="ZaiXue"  class="tab-cur">在学</a>
                            <a href="javascript:;" class="" id="YiXue">学完</a>
                        </p>
                    </div>
            </div>
        <div class="all_classes_content">
           <asp:Repeater ID="RepeaterCourseList" runat="server">
                    <ItemTemplate>

                        <div class="classes_part_list f_l" onclick="<%#Eval("HomepageTiaoZhuan") %>" style="margin-top: 15px; margin-right: 5px; margin-left: 5px; background: #fbfbfb; border: 1px solid #c7c7c7; cursor: pointer;">
                            <div>
                                <img src="http://61.50.119.70:1122/<%#Eval("ImgUrl") %>" title="<%#Eval("Description") %>" onerror='this.src=""' width="170" height="108" />
                            </div>
                            <p class="list_title" title="<%#Eval("Title")%>"><span class="text"><%#GetRemoveString(Eval("Title").ToString())%></span></p>
                            <div class="progress" title="<%#Eval("PercentageDescription") %>">
                                <span class="green" style="width: <%#Eval("Percentage") %>%;"><span><%#Eval("Percentage") %>%</span></span>
                            </div>

                        </div>

                    </ItemTemplate>

                </asp:Repeater>
            <div class="clear"></div>

        </div>


        <div class="all_classes last_classes">
            <div class="classes_title">
                <div class="title title2 clearfix area mg-auto"><h2>我的证书</h2></div>
            </div>
            <div class="classes_content">
                <table class="list_table" cellspacing="0" cellpadding="0" border="0" style="width:100%">
                    <tr class="tab_th top_th" style=" height: 36px;
            line-height: 36px;
            cursor: pointer;">
                        <th class="th_tit_left">结业证书号</th>
                        <th class="th_tit_left">发证单位</th>
                        <th class="th_tit_left">证书查询网址</th>
                        <th class="th_tit_left">学员姓名</th>
                        <th class="th_tit_left">结业时间</th>
                         <th class="th_tit_left">操作</th>
                    </tr>

                    <asp:Repeater ID="RepeaterList" runat="server">
                        <ItemTemplate>
                            <tr class="tab_td" style="height:40px; line-height:40px;">
                                <td><%#Eval("GraduationNo") %></td>
                                <td><%#Eval("AwardUnit") %></td>
                                <td><%#Eval("QueryURL") %></td>
                                <td><%#Eval("CurriculumName") %></td>
                                <td><%#Eval("GraduationDate") %></td>
                                <td><a onclick="Export()"> 导出证书</a></td>
                            </tr>
                        </ItemTemplate>

                    </asp:Repeater>

                </table>
                <div class="clear"></div>

            </div>
        </div>

        <div class="all_classes last_classes">
            <div class="classes_title">
                <div class="title title2 clearfix area mg-auto"><h2>档案信息</h2></div>
            </div>
            <div class="all_classes_content">
                <table class="list_table" cellspacing="0" cellpadding="0" border="0" style="width:100%">
                    <tr class="tab_th top_th" style=" height: 36px;
            line-height: 36px;
            cursor: pointer;">
                        <th class="th_tit_left">结业证书号</th>
                        <th class="th_tit_left">发证单位</th>
                        <th class="th_tit_left">证书查询网址</th>
                        <th class="th_tit_left">学员姓名</th>
                        <th class="th_tit_left">结业时间</th>
                    </tr>

                    <asp:Repeater ID="Repeater1" runat="server">
                        <ItemTemplate>
                            <tr class="tab_td" style="height:40px; line-height:40px;">
                                <td><%#Eval("GraduationNo") %></td>
                                <td><%#Eval("AwardUnit") %></td>
                                <td><%#Eval("QueryURL") %></td>
                                <td><%#Eval("GraduationDate") %></td>
                                <td><%#Eval("CreateTime") %></td>
                            </tr>
                        </ItemTemplate>

                    </asp:Repeater>

                </table>
            </div>
        </div>
    </div>
    <div class="right_part f_r">

        <%--<div class="class_library">
            <div class="library_title">
                <div class="title">社区动态</div>
            </div>
            <div class="library_main">
                <h4 class='title_name next_title'><span style='cursor: pointer;' onclick="javascript:window.location.href='/_layouts/15/YHSD.VocationalEducation.Portal/ChapterGuanLi.aspx?id=1">围棋第三期晋级赛</span>
                    <p class='kaiKeDate'>(2016年01月15日教学楼南楼A3102上午10点开始)</p>
                </h4>
                <h4 class='title_name next_title'><span style='cursor: pointer;' onclick="javascript:window.location.href='/_layouts/15/YHSD.VocationalEducation.Portal/ChapterGuanLi.aspx?id=1">围棋校级友谊赛</span>
                    <p class='kaiKeDate'>(2016年01月20日教学楼南楼A3102上午10点开始)</p>
                </h4>
            </div>
        </div>--%>
        <div class="class_library">
            <div class="library_title">
                <div class="title title2 clearfix area mg-auto"><h2><a href="AccountManagement.aspx">账户管理</a></h2></div>
            </div>
           
        </div>
    </div>
    <div class="right_part f_r">

        <%--<div class="class_library">
            <div class="library_title">
                <div class="title">社区动态</div>
            </div>
            <div class="library_main">
                <h4 class='title_name next_title'><span style='cursor: pointer;' onclick="javascript:window.location.href='/_layouts/15/YHSD.VocationalEducation.Portal/ChapterGuanLi.aspx?id=1">围棋第三期晋级赛</span>
                    <p class='kaiKeDate'>(2016年01月15日教学楼南楼A3102上午10点开始)</p>
                </h4>
                <h4 class='title_name next_title'><span style='cursor: pointer;' onclick="javascript:window.location.href='/_layouts/15/YHSD.VocationalEducation.Portal/ChapterGuanLi.aspx?id=1">围棋校级友谊赛</span>
                    <p class='kaiKeDate'>(2016年01月20日教学楼南楼A3102上午10点开始)</p>
                </h4>
            </div>
        </div>--%>
        <div class="class_library">
            <div class="library_title">
                <div class="title title2 clearfix area mg-auto"><h2>我的通知</h2></div>
            </div>
            <div class="library_main">
                <label runat="server" id="LabelKaoShiTongZhi" />
                <h4 class='title_name next_title'><span style='cursor: pointer;' onclick="">微积分选修</span>
                    <p class='kaiKeDate'>(2016年01月15日开课)</p>
                </h4>
                <h4 class='title_name next_title'><span style='cursor: pointer;' onclick="">德育选修课</span>
                    <p class='kaiKeDate'>(2016年01月25日第八节课所有学生必须参加)</p>
                </h4>
            </div>
        </div>
    </div>
    <div class="clear"></div>
</div>
</label>
</asp:Content>
