<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyCenter.ascx.cs" Inherits="YHSD.VocationalEducation.Portal.CONTROLTEMPLATES.YHSD.VocationalEducation.Portal.MyCenter" %>
<style>
    .kaiKeDate {
        font-size: 12px;
        font-family: "微软雅黑","宋体";
        color: #999;
    }
</style>
<script>

    function EditUser(id) {
        OL_ShowLayerNo(2, "编辑个人信息", 770, 400, "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/UpdateUser.aspx?id=" + id, function (returnVal) {
        });

    }
    function Export(id) {
        OL_ShowLayerNo(2, "打印导出证书", 400, 320, "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/Certifacat.aspx?StuID="+id, function (returnVal) {
            });
        }
</script>
<div class="student_main">
    <div class="f_l">
        <div class="classes_title">
            <a class="title current_title">我的课程</a>
        </div>
        <div class="all_classes_content">
           <asp:Repeater ID="RepeaterCourseList" runat="server">
                    <ItemTemplate>

                        <div class="classes_part_list f_l" onclick="<%#Eval("HomepageTiaoZhuan") %>" style="margin-top: 15px; margin-right: 5px; margin-left: 5px; background: #fbfbfb; border: 1px solid #c7c7c7; cursor: pointer;">
                            <div>
                                <img src="<%#Eval("ImgUrl") %>" title="<%#Eval("Description") %>" onerror='this.src="<%=YHSD.VocationalEducation.Portal.Code.Common.PublicEnum.NoTuPianUrl%>"' width="170" height="108" />
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
                <div class="title">我的证书</div>
            </div>
            <div class="classes_content">
                <table class="list_table" cellspacing="0" cellpadding="0" border="0" style="width: 100%; text-align:center">
                    <tr class="tab_th top_th" style="height: 36px; line-height: 36px; cursor: pointer;">
                        <th class="th_tit_left">结业证书号</th>
                        <th class="th_tit_left">发证单位</th>
                        <th class="th_tit_left">证书查询网址</th>
                        <th class="th_tit_left">学员姓名</th>
                        <th class="th_tit_left">结业时间</th>
                        <th class="th_tit_left">操作</th>
                    </tr>

                    <asp:Repeater ID="RepeaterList" runat="server">
                        <ItemTemplate>
                            <tr class="tab_td" style="height: 40px; line-height: 40px;">
                                <td><%#Eval("GraduationNo") %></td>
                                <td><%#Eval("AwardUnit") %></td>
                                <td><%#Eval("QueryURL") %></td>
                                <td><%#Eval("CurriculumName") %></td>
                                <td><%#Eval("GraduationDate") %></td>
                                <td><a onclick="Export('<%#Eval("StuID")%>')">打印导出证书</a></td>
                            </tr>
                        </ItemTemplate>

                    </asp:Repeater>

                </table>
                <div class="clear"></div>

            </div>
        </div>
        <div class="all_classes last_classes">
            <div class="classes_title">
                <div class="title">个人档案   <span style="float:right">
                    <asp:Button ID="Button2" runat="server" Text="导出个人档案" OnClick="Button2_Click" /></span> </div>
                
            </div>
            <div class="all_classes_content">
                <table class="list_table" cellspacing="0" cellpadding="0" border="0" style="width: 100%; text-align:center;">
                    <tr class="tab_th top_th" style="height: 36px; line-height: 36px; cursor: pointer;">
                        <th class="th_tit_left"> 姓名</th>
                        <th class="th_tit_left">电话</th>
                        <th class="th_tit_left">身份证号</th>
                        
                    </tr>

                    <asp:Repeater ID="Repeater2" runat="server">
                        <ItemTemplate>
                            <tr class="tab_td" style="height: 40px; line-height: 40px;">
                                <td><%#Eval("Name") %></td>
                                <td><%#Eval("Mobile") %></td>
                                <td><%#Eval("CardID") %></td>
                              
                            </tr>
                        </ItemTemplate>

                    </asp:Repeater>

                </table>
            </div>
        </div>
        <div class="all_classes last_classes">
            <div class="classes_title">
                <div class="title">培训档案   <span style="float:right">
                    <asp:Button ID="Button1" runat="server" Text="导出培训档案" OnClick="Button1_Click" /></span> </div>
                
            </div>
            <div class="all_classes_content">
                <table class="list_table" cellspacing="0" cellpadding="0" border="0" style="width: 100%; text-align:center;">
                    <tr class="tab_th top_th" style="height: 36px; line-height: 36px; cursor: pointer;">
                        <th class="th_tit_left"> 课程名称</th>
                        <th class="th_tit_left">课程时长</th>
                        <th class="th_tit_left">考核结果</th>
                        
                    </tr>

                    <asp:Repeater ID="Repeater1" runat="server">
                        <ItemTemplate>
                            <tr class="tab_td" style="height: 40px; line-height: 40px;">
                                <td><%#Eval("KCMC") %></td>
                                <td><%#Eval("SC") %></td>
                                <td><%#Eval("KHJG") %></td>
                              
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
                <div class="title">我的通知</div>
            </div>
            <div class="library_main">
                <label runat="server" id="LabelKaoShiTongZhi" />
                <h4 class='title_name next_title'><span style='cursor: pointer;' onclick="javascript:window.location.href='/_layouts/15/YHSD.VocationalEducation.Portal/ChapterGuanLi.aspx?id=1'">微积分选修</span>
                    <p class='kaiKeDate'>(2016年01月15日开课)</p>
                </h4>
                <h4 class='title_name next_title'><span style='cursor: pointer;' onclick="javascript:window.location.href='/_layouts/15/YHSD.VocationalEducation.Portal/ChapterGuanLi.aspx?id=1'">德育选修课</span>
                    <p class='kaiKeDate'>(2016年01月25日第八节课所有学生必须参加)</p>
                </h4>
            </div>
        </div>
    </div>
    <div class="clear"></div>
</div>
