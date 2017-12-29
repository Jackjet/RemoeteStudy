<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ES_wp_MakeExaminationUserControl.ascx.cs" Inherits="SVDigitalCampus.Examination_System.ES_wp_MakeExamination.ES_wp_MakeExaminationUserControl" %>
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/common.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/iconfont.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/animate.css" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script type="text/javascript" src="../../../../_layouts/15/SVDigitalCampus/Script/tz_slider.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/popwin.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/DatePicker/WdatePicker.js"></script>
<style type="text/css">
    .hide {
        display: none;
    }

    .show {
        display: block;
    }

    .progress {
        z-index: 2000;
    }
</style>
<script type="text/javascript">
    <%--  $(function () {
        $('#<%=ClassNames.ClientID%>').dialog({
            title: "班级列表",
            width: 400,
            height: 400,
            content: 'url:<%=SietUrl%>GetClassList.aspx?flag=worker&' + Math.random(),
            lock: true,
            parent: this,
            max: false,
            min: false
        });
    });--%>
    <%--function ShowValueModel(valueName, valueID) {
        document.getElementById('<%=ClassNames.ClientID%>').value = valueName;
        document.getElementById('<%=ClassIDs.ClientID%>').value = valueID;
    }--%>
</script>
<div class="MakeExamination">
    <!--页面名称-->
    <%--  <h1 class="Page_name">组卷</h1>--%>
    <!--整个展示区域-->
    <div class="Whole_display_area">
        <%--  <div class="Operation_area">
            <div class="left_choice fl">
                <ul>
                    <li class="">
                        <div class="qiehuan fl">
                            <span class="qijin">
                                <asp:LinkButton ID="btnMessage" runat="server" OnClick="btnMessage_Click" CssClass="Disable">试卷信息</asp:LinkButton><asp:LinkButton ID="btnAdd" runat="server" CssClass="Enable">新建试卷</asp:LinkButton></span>
                        </div>
                    </li>
                </ul>
            </div>
            <div class="clear"></div>
        </div>--%>
        <%-- <div class="S_conditions">
            <div id="selectList" class="screenBox screenBackground">
                <dl class="listIndex dlHeight" attr="terminal_brand_s">
                    <dt>*专业</dt>
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
        </div>--%>
        <div class="clear"></div>
        <!--tab切换title-->
        <div class="Order_tab">
            <div class="Order_tabheader" id="ordertitle">
                <ul>
                    <li class="selected"><a href='<%=action %>'><span class="zong_tit"><span class="num_1">1</span><span class="add_li"><%=choseshow %></span></span></a></li>
                    <li class="selected"><a href="#"><span class="zong_tit"><span class="num_2">2</span><span class="add_li">组卷</span></span></a></li>
                    <li><a href="#"><span class="zong_tit"><span class="iconfont num_3">&#xe654;</span><span class="add_li">完成</span></span></a></li>
                </ul>
            </div>
            <div class="content">
                <div class="tc" style="display: none">1</div>
                <div class="tc" style="display: block;">
                    <div class="Condition_topic">
                        <div class="C_top">
                            <span>*试卷名称： 
                                <input type="text" class="input" id="ExamName" name="ExamName" runat="server" placeholder=" 请输入试卷名称" /></span>
                            <span>*试卷时间：
                                <input type="text" id="ExamTime" name="ExamTime" class="input" onkeyup="this.value=this.value.replace(/\D/g,'')" onafterpaste="this.value=this.value.replace(/\D/g,'')" onblur="checknum(this)" runat="server" placeholder=" 请输入考试时间" />分钟</span>
                        </div>
                        <div class="clear"></div>
                        <div class="C_top">
                            <%--   <span id="classstr" runat="server">班级：--%>
                            <%-- <asp:DropDownList ID="ClassNames" runat="server">
                                    <asp:ListItem Value="0">请选择</asp:ListItem>
                                </asp:DropDownList>--%>
                            <%--   <asp:TreeView ID="tvClass" onclick="OnTreeNodeChecked()" runat="server" ShowCheckBoxes="None"
                            CssClass="treeView" ShowLines="true" ExpandDepth="0" PopulateNodesFromClient="false"
                            ShowExpandCollapse="true" EnableViewState="false">
                            <LevelStyles>
                                <asp:TreeNodeStyle Font-Underline="False" />
                                <asp:TreeNodeStyle Font-Underline="False" />
                                <asp:TreeNodeStyle Font-Underline="False" />
                                <asp:TreeNodeStyle Font-Underline="False" />
                            </LevelStyles>
                        </asp:TreeView>--%>

                            <%--</span>--%>
                            <%--<div id="classstr" runat="server"></div>--%>
                            <%--  <span>*有效时间：
                                    <input type="text" class="Wdate input" readonly="readonly" runat="server" id="WorkTime" onclick="WdatePicker()" placeholder=" 请输入有效时间" /></span>--%>
                            <span class="Select">难度： 
                                <asp:DropDownList ID="Difficulty" runat="server">
                                    <asp:ListItem Value="1">简单</asp:ListItem>
                                    <asp:ListItem Value="2">中等</asp:ListItem>
                                    <asp:ListItem Value="3">较难</asp:ListItem>
                                </asp:DropDownList>
                            </span>
                            <span class="Select">*状态：
                                    <asp:DropDownList ID="Status" runat="server">
                                        <asp:ListItem Value="1">启用</asp:ListItem>
                                        <asp:ListItem Value="2">禁用</asp:ListItem>
                                    </asp:DropDownList>
                            </span>

                        </div>
                        <div class="clear"></div>
                        <div class="C_bottom">
                            <h1>*适用范围：
                              <span>
                                  <input id="rdType1" name="rbtype" type="radio" value="1" checked="true" />标准考试</span>
                                <span>
                                    <input id="rdType2" name="rbtype" type="radio" value="2" />测验</span>
                                <span>
                                    <input id="rdType3" name="rbtype" type="radio" value="3" />作业</span>
                                <span class="all_num">*试卷总分：<em>
                                    <asp:Label ID="TotalScore" Text="0" runat="server"></asp:Label>
                                    <asp:HiddenField ID="TotalScoreVal" runat="server" Value="0" />
                                </em></span></h1>
                        </div>
                    </div>
                    <div class="clear"></div>
                    <div class="Topic_tcon">
                        <div id="slide">
                            <ul>
                                <asp:ListView ID="lv_ExamQType" runat="server">
                                    <ItemTemplate>
                                        <li>
                                            <div class="Topic">
                                                <a class="Topic_click" href="#">
                                                    <i><%#Eval("open") %></i>
                                                </a>
                                                <span class="Topic_tit">
                                                    <span class="T_name"><%#Eval("TypeName") %>
                                                        <input type="hidden" id="TypeID" runat="server" name="TypeID" value='<%#Eval("TypeID") %>' />
                                                        <input type="hidden" id="QTypeID" runat="server" name="QTypeID" value='<%#Eval("QTypeID") %>' />
                                                    </span>
                                                    <span>
                                                        <em>
                                                            <asp:Label ID="Typecount" runat="server" Text='<%#Eval("TypeCount") %>'></asp:Label></em>题,每题
                                            <em>
                                                <input id="score" name="score" type="text" value="0" onkeyup="this.value=this.value.replace(/\D/g,'1')" onafterpaste="this.value=this.value.replace(/\D/g,'1')" onblur="worksorce(this);" runat="server" />分
                                            </em>
                                                    </span>
                                                    <span>，|总<em><asp:Label ID="subTotalScore" runat="server" Text="0"></asp:Label></em>分</span></span>
                                            </div>
                                            <div class="Topic_con" style='display: <%#Eval("IsShow")%>'>
                                                <div class="Food_order">
                                                    <div class="clear"></div>
                                                    <table class="O_form">
                                                        <tr class="O_trth">
                                                            <!--表头tr名称-->
                                                            <th class="Dishes">题目</th>
                                                            <th class="Type">难度</th>
                                                            <th class="Number">排序</th>
                                                            <th class="Operation">操作</th>
                                                        </tr>

                                                        <asp:ListView ID="lv_ExamQ" runat="server" ItemPlaceholderID="QItem" OnItemCommand="lv_ExamQ_ItemCommand">
                                                            <ItemTemplate>
                                                                <tr class="Single">
                                                                    <td class="Dishes"><%#Eval("Title") %><asp:HiddenField ID="QID" runat="server" Value='<%#Eval("ID") %>' />
                                                                    </td>
                                                                    <td class="Type"><%#Eval("DifficultyShow") %></td>
                                                                    <td class="Number">
                                                                        <input id="txtOrder" type="text" onkeyup="this.value=this.value.replace(/\D/g,'1')" onafterpaste="this.value=this.value.replace(/\D/g,'1')" onblur="checknum(this);" value='<%#Eval("Count") %>' runat="server" class="order" />
                                                                    </td>
                                                                    <td class="Operation">
                                                                        <asp:LinkButton ID="btndelete" class="btn" runat="server" CommandName="del" CommandArgument='<%# Eval("ID")+"&"+Eval("QType") %>'><i class="iconfont">&#xe64c;</i></asp:LinkButton></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </table>
                                                </div>
                                            </div>
                                        </li>
                                    </ItemTemplate>
                                </asp:ListView>
                            </ul>
                            <div class="clear"></div>
                        </div>
                    </div>
                    <div class="To_submit">
                        <input type="button" id="list_back" runat="server" class="list_back" name="list_back" onclick="Toback();" value="返回选题" />
                        <asp:Button ID="btMakeExam" runat="server" CssClass="sure_list" Text="确认组卷" OnClientClick="return MarkExam(this);" OnClick="btMakeExam_Click" />
                    </div>
                </div>
                <div class="tc" id="Success" style="display: none;">
                    <p class="O_finish">
                        <span class="fl icon_o"><i class="iconfont finish_t">&#xe654;</i></span>
                        <span class="fl info_o">试卷组卷成功。<br />
                            <a href="ExamPaperReleaseList.aspx">［试卷发布］</a></span>
                    </p>
                </div>
            </div>
            <div class="clear"></div>
        </div>
    </div>
    <!--展示区域-->
</div>
<div id="progress" class="hidden">
    <img id="progressImgage" style="top: 50%; left: 50%; margin-top: -95%; margin-left: 40%;" class="progress" alt='' src="../../../../_layouts/15/SVDigitalCampus/Image/ajax-loader.gif" />
</div>
<div id="mask" style="width: 100%; height: 100%; position: fixed; top: 0; left: 0; z-index: 1000; background: #cccccc; filter: alpha(opacity=50); -moz-opacity: 0.5; -khtml-opacity: 0.5; opacity: 0.5;" class="hidden"></div>
<script type="text/javascript">

    function show() {
        $("#progress").removeClass("hidden").addClass("show");
        $("#mask").removeClass("hidden").addClass("show");
    }
    function hidden() {
        $("#progress").removeClass("show").addClass("hidden");
        $("#mask").removeClass("show").addClass("hidden");
    }
    function Success() {
        $("#Success").show().siblings().hide();
        $("#ordertitle li:last").addClass("selected");
    }
    function Toback() {
        window.location.href = '<%=action%>';
    }
    function checknum(obj) {
        if ($(obj).val() == "") {
            //$(obj).val("1");
            return;
        }
        var testval = "/^\d+(\.\d+)?$/";
        if (testval.match($(obj).val())) {
            alert("请正确输入！");
            $(obj).val("");
            return;
        }
    }
    function worksorce(obj) {
        var testval = "/^\d+(\.\d+)?$/";
        var value = $(obj).val();
        if (isNaN(Number(value))) {
            alert("请正确输入！");
            $(obj).val("");
            return;
        }
        $(obj).parent().parent().next().find('span[id$=subTotalScore]').text(parseInt($(obj).val()) * parseInt($(obj).parent().parent().find('span[id$=Typecount]').text()));
        var totlascore = 0;
        $("input[name$='score']").each(function () {
            totlascore = parseInt(totlascore) + parseInt($(this).val()) * parseInt($(this).parent().parent().find('span[id$=Typecount]').text());
        });
        $('#<%=TotalScore.ClientID%>').text(totlascore);
        $('#<%=TotalScoreVal.ClientID%>').val(totlascore);
     <%--   $('#<%=TotalScore.ClientID%>').text(parseInt($('#<%=TotalScore.ClientID%>').text()) + parseInt($(obj).val()) * parseInt($(obj).parent().parent().find('span[id$=Typecount]').text()));--%>
    }
    var isclick = false;
    function MarkExam(obj) {
        if (isclick) {
            isclick = false;
        } else { isclick = true; }
        if (isclick) {
            var ExamName = $('#<%=ExamName.ClientID%>').val();
            var ExamTime = $('#<%=ExamTime.ClientID%>').val();
            var TotalScoreVal = $('#<%=TotalScoreVal.ClientID%>').val();
            if (ExamName == "") {
                alert("请输入试卷名称！");
                return false;
            }
            if (ExamTime == "") {
                alert("请输入考试时间！");
                return false;
            }
            if (TotalScoreVal == "" || TotalScoreVal == "0") {
                alert("请输入试题分值！");
                return false;
            }
            show();
            //obj.disabled = true;
            isclick = true;
            return true;

        }
    }
</script>
