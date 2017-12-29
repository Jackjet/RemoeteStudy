<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ES_wp_ExaminationUserControl.ascx.cs" Inherits="SVDigitalCampus.Examination_System.ES_wp_Examination.ES_wp_ExaminationUserControl" %>
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/common.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/iconfont.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/animate.css" rel="stylesheet" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script src="../../_layouts/15/SVDigitalCampus/Script/tz_slider.js"></script>
<script type="text/javascript">

    //$("body:first").append(inntHtml);
    var int;
    function Start() {
        var examtime = '<%=ExamTimes%>';
        <%AnswerBeginTime = DateTime.Now;%>
        if (parseFloat(examtime) > 0) {
            int = setInterval("workNewTime();", 1000);

        }
    }
    function workNewTime() {
        var minute = parseInt($('#<%=Mins.ClientID%>').text());
        var sec = parseInt($('#<%=Seconds.ClientID%>').text());
        if (parseInt(sec) - parseInt(1) >= parseInt(0)) {
            sec = parseInt(sec) - parseInt(1);
        } else if (parseInt(minute) - parseInt(1) >= parseInt(0)) {
            minute = parseInt(minute) - parseInt(1);
            sec = 59;
        }
        $('#<%=Mins.ClientID%>').text(minute);
        $('#<%=Seconds.ClientID%>').text(sec);
        if (minute == 0 && sec == 0) {
            int = window.clearInterval(int);//停止刷新时间
            if (confirm("答题时间已结束,是否提交试卷？")) {
                //$('#<%=btnOk.ClientID%>').trigger("OnClick");
                __doPostBack('SubmitExam', 'OnClick');
            }

        }
    }
    function chose(obj, ID) {
        $("*[id$=hifanswer]").each(function () {

            if ($(this).val() == ID) {
                $(this).prev().css("background", "none"); $(this).prev().attr("title", "");
                var ckcount = false;
                $("input[name=ckanswer" + ID + "]:checked").each(function () {
                    ckcount = true;
                });
                $("input[name=answer" + ID + "]:checked").each(function () {
                    ckcount = true;
                });
                if (ckcount) {
                    $(this).prev().css("background-color", "#5493d7"); $(this).prev().attr("title", "已答");
                }

            }

        });
        var count = 0;
        var answercount = 0;
        $("*[id$=hifanswer]").each(
            function () {
                if ($(this).prev().attr("title") != null && $(this).prev().attr("title") != "")
                { answercount++; }
            });
        count = '<%=ExamQdb==null?0:ExamQdb.Rows.Count%>';
        var over = Math.ceil(parseFloat(answercount / parseInt(count)) * 100) + "%";
        $("#overnumber").html(over);
    }
</script>
<div class="Examination">
    <div class="timer fl">
        <p>
            <span><i class="iconfont">&#xe626;</i>考试剩余时间
            <em class="redzi">
                <asp:Label ID="Mins" runat="server" Text="0"></asp:Label></em>分
            <em class="redzi">
                <asp:Label ID="Seconds" runat="server" Text="0"></asp:Label></em>秒</span>
        </p>
        <%--  <asp:Timer ID="examtr" runat="server" Enabled="false" Interval="1000"></asp:Timer>--%>
    </div>
    <div class="centercon_e fl">
        <div class="tit">
            <h2>
                <asp:Label ID="ExamTitle" runat="server" Text=""></asp:Label></h2>
            <div class="tit_bottom">
                <span>考试科目：<asp:Label ID="Subject" runat="server" Text=""></asp:Label></span>
                <span>考试时间：<asp:Label ID="ExamTime" runat="server" Text=""></asp:Label>分钟</span>
                <span>总分：<asp:Label ID="FullScore" runat="server" Text=""></asp:Label>分</span>
                <input type="button" id="btnStart" onclick="Start();" class="start" value="开始答题" />
                <asp:HiddenField ID="ExamID" runat="server" />
            </div>
            <div class="tit_remarks">答案录入时,填空题的填空项格式为“（XXX）”,多个填空项以“,”隔开。</div>
        </div>
        <asp:ListView ID="lv_ExamQType" runat="server">
            <ItemTemplate>
                <div class="firsttit">
                    <h2><%#Eval("TypeName") %><span class="xiaozi">（<%#Eval("TypeCount") %>题，每题<%#Eval("Score") %>分，总分<%#Eval("SubScore") %>分）</span></h2>
                    <asp:HiddenField ID="TypeID" runat="server" Value='<%#Eval("TypeID") %>' />
                    <asp:HiddenField ID="Template" runat="server" Value='<%#Eval("OrderID") %>' />
                    <ul class="timu">
                        <li>
                            <asp:ListView ID="QuestionTypeOne" runat="server">
                                <EmptyDataTemplate>
                                    <h1>暂无试题</h1>
                                </EmptyDataTemplate>
                                <ItemTemplate>
                                    <p class="biaoti">
                                        <%# Eval("Count") %> .<%#Eval("Question") %>
                                        <asp:HiddenField ID="qID" runat="server" Value='<%#Eval("ID") %>' />
                    <asp:HiddenField ID="qType" runat="server" Value='<%#Eval("qType") %>' />
                                    </p>
                                    <p>
                                        <%#Eval("Analysis") %>
                                    </p>
                                    <p class="xuanxiang" style='display: <%#Eval("IsShow")%>'>
                                        <span style='display: <%#Eval("OneIsShow")%>'>

                                            <span style='display: <%#Eval("OptionAIsshow")%>'>
                                                <input type="radio" id="answerA" name='<%#"answer"+Eval("ID") %>' value="A" onclick='<%#"chose(this,"+Eval("ID")+ ");"%>' />
                                                <%#Eval("OptionA") %></span>

                                            <span style='display: <%#Eval("OptionBIsshow")%>'>
                                                <input type="radio" id="answerB" name='<%#"answer"+Eval("ID") %>' value="B" onclick='<%#"chose(this,"+Eval("ID")+ ");"%>' />
                                                <%#Eval("OptionB") %></span>

                                            <span style='display: <%#Eval("OptionCIsshow")%>'>
                                                <input type="radio" id="answerC" name='<%#"answer"+Eval("ID") %>' value="C" onclick='<%#"chose(this,"+Eval("ID")+ ");"%>' />
                                                <%#Eval("OptionC") %></span>

                                            <span style='display: <%#Eval("OptionDIsshow")%>'>
                                                <input type="radio" id="answerD" name='<%#"answer"+Eval("ID") %>' value="D" onclick='<%#"chose(this,"+Eval("ID")+ ");"%>' />
                                                <%#Eval("OptionD") %></span>

                                            <span style='display: <%#Eval("OptionEIsshow")%>'>
                                                <input type="radio" id="answerE" name='<%#"answer"+Eval("ID") %>' value="E" onclick='<%#"chose(this,"+Eval("ID")+ ");"%>' />
                                                <%#Eval("OptionE") %></span>

                                            <span style='display: <%#Eval("OptionFIsshow")%>'>
                                                <input type="radio" id="answerF" name='<%#"answer"+Eval("ID") %>' value="F" onclick='<%#"chose(this,"+Eval("ID")+ ");"%>' />
                                                <%#Eval("OptionF") %></span>
                                        </span>

                                        <span style='display: <%#Eval("moreIsShow")%>'>
                                            <span style='display: <%#Eval("OptionAIsshow")%>'>
                                                <input type="checkbox" id="CKA" name='<%#"ckanswer"+Eval("ID") %>' value='A' onclick='<%#"chose(this,"+Eval("ID")+ ");"%>' />
                                                <%#Eval("OptionA") %></span>

                                            <span style='display: <%#Eval("OptionBIsshow")%>'>
                                                <input type="checkbox" id="CKB" name='<%#"ckanswer"+Eval("ID") %>' value='B' onclick='<%#"chose(this,"+Eval("ID")+ ");"%>' />
                                                <%#Eval("OptionB") %></span>

                                            <span style='display: <%#Eval("OptionCIsshow")%>'>
                                                <input type="checkbox" id="CKC" name='<%#"ckanswer"+Eval("ID") %>' value='C' onclick='<%#"chose(this,"+Eval("ID")+ ");"%>' />
                                                <%#Eval("OptionC") %></span>

                                            <span style='display: <%#Eval("OptionDIsshow")%>'>
                                                <input type="checkbox" id="CKD" name='<%#"ckanswer"+Eval("ID") %>' value='D' onclick='<%#"chose(this,"+Eval("ID")+ ");"%>' />
                                                <%#Eval("OptionD") %></span>

                                            <span style='display: <%#Eval("OptionEIsshow")%>'>
                                                <input type="checkbox" id="CKE" name='<%#"ckanswer"+Eval("ID") %>' value='E' onclick='<%#"chose(this,"+Eval("ID")+ ");"%>' />
                                                <%#Eval("OptionE") %></span>
                                            <span style='display: <%#Eval("OptionFIsshow")%>'>
                                                <input type="checkbox" id="CKF" name='<%#"ckanswer"+Eval("ID") %>' value='F' onclick='<%#"chose(this,"+Eval("ID")+ ");"%>' />
                                                <%#Eval("OptionF") %></span>
                                        </span>
                                    </p>
                                    <p class="xuanxiangkuang" style='display: <%#Eval("kuangIsShow")%>'>
                                        <textarea id='<%#"textanswer"+Eval("ID") %>' name='<%#"textanswer"+Eval("ID") %>'></textarea>
                                    </p>
                                </ItemTemplate>
                            </asp:ListView>
                            <%-- <p class="biaoti">1.碳水化合物是指？</p>
                    <p class="xuanxiang"><span>A.二氧化碳</span><span>B.二氧化碳</span><span>C.二氧化碳</span><span>D.二氧化碳</span></p>--%>
                        </li>
                    </ul>
                </div>
            </ItemTemplate>
        </asp:ListView>
        <%--  <div class="second">
            <h2>第二部分  多选题<span class="xiaozi">（20题，每题2分，总分60分）</span></h2>
            <ul class="timu">
                <li>
                    <p class="biaoti">1.碳水化合物是指？</p>
                    <p class="xuanxiang">
                        <span><asp:RadioButton ID="answerA" GroupName="dd" runat="server" />A.二氧化碳</span><span><asp:RadioButton ID="answerB" runat="server" />B.二氧化碳</span><span><asp:RadioButton ID="answerC" runat="server" />C.二氧化碳</span><span><asp:RadioButton ID="answerD" runat="server" />D.二氧化碳</span></p>
                </li>
            </ul>
        </div> 
        <div class="third">
            <h2>第三部分  简答题<span class="xiaozi">（20题，每题2分，总分60分）</span></h2>
            <ul class="timu">
                <li>
                    <div class="defen fl">
                        <span class="defenname">得分</span><br/>
                        <span class="fenzhi">12</span>
                    </div>
                    <div class="ticon fl">
                        <p class="biaoti">1.碳水化合物是指？</p>
                        <p class="xuanxiangkuang">
                            <textarea>二氧化碳二氧化碳二氧化碳二氧化碳二氧化碳二氧化碳二氧化碳二氧化碳二氧化碳二氧化碳二氧化碳二氧化碳二氧化碳二氧化碳二氧化碳二氧化碳二氧化碳二氧化碳</textarea>
                        </p>
                    </div>
                    <div class="clear"></div>
                </li>
            </ul>
        </div>--%>
    </div>
    <div class="answercon fr">
        <div class="finishdiv">
            <asp:ListView ID="lvQCount" runat="server" GroupItemCount="5">
                <EmptyDataTemplate>暂无试题可以答题</EmptyDataTemplate>
                <LayoutTemplate>
                    <table class="answertb" id="answeronetb">
                        <caption><span class="answertit">已完成<label id="overnumber">0%</label></span></caption>
                        <thead>
                            <tr>
                                <th colspan="5">第一部分</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>
                                    <asp:PlaceHolder ID="groupPlaceholder" runat="server"></asp:PlaceHolder>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </LayoutTemplate>
                <GroupTemplate>
                    <tr>
                        <td>
                            <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                        </td>
                    </tr>
                </GroupTemplate>
                <ItemTemplate>
                    <span class="number"><%#Eval("Count") %></span>
                    <asp:HiddenField ID="hifanswer" runat="server" Value='<%#Eval("ID") %>' />
                </ItemTemplate>
            </asp:ListView>
            <asp:ListView ID="answertwo" runat="server" GroupItemCount="5">
                <LayoutTemplate>
                    <table class="answertb" id="answertwotb">
                        <thead>
                            <tr>
                                <th colspan="5">第二部分</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>
                                    <asp:PlaceHolder ID="groupPlaceholder" runat="server"></asp:PlaceHolder>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </LayoutTemplate>
                <GroupTemplate>
                    <tr>
                        <td>
                            <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                        </td>
                    </tr>
                </GroupTemplate>
                <ItemTemplate>
                    <span class="number"><%#Eval("Count") %></span>
                    <asp:HiddenField ID='hifanswer' runat="server" Value='<%#Eval("ID") %>' />
                </ItemTemplate>
            </asp:ListView>
              <asp:ListView ID="answerthree" runat="server" GroupItemCount="5">
                <LayoutTemplate>
                    <table class="answertb" id="answerthreetb">
                        <thead>
                            <tr>
                                <th colspan="5">第三部分</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>
                                    <asp:PlaceHolder ID="groupPlaceholder" runat="server"></asp:PlaceHolder>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </LayoutTemplate>
                <GroupTemplate>
                    <tr>
                        <td>
                            <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                        </td>
                    </tr>
                </GroupTemplate>
                <ItemTemplate>
                    <span class="number"><%#Eval("Count") %></span>
                    <asp:HiddenField ID='hifanswer' runat="server" Value='<%#Eval("ID") %>' />
                </ItemTemplate>
            </asp:ListView>
        </div>
    </div>
    <div class="clear"></div>
    <div class="submit_btn">
        <asp:Button ID="btnOk" runat="server" Text="提交" CssClass="submitbtn" OnClientClick="return config('您确认提交试卷？')" OnClick="btnOk_Click" />
    </div>
</div>
