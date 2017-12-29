<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SE_wp_UseSurveyPaperUserControl.ascx.cs" Inherits="Sinp_StudentWP.WebParts.SE.SE_wp_UseSurveyPaper.SE_wp_UseSurveyPaperUserControl" %>
<script src="/_layouts/15/Script/jquery-1.8.0.js"></script>
<link href="/_layouts/15/Style/common.css" rel="stylesheet" />
<link href="/_layouts/15/Stu_css/list_nr.css" rel="stylesheet" >
<style type="text/css">
    ol {
        margin:auto;width:650px;
    }
    ol li {
        display: block;
        float: left;
        line-height: 45px;
        text-align: center;
        font-size: 16px;
        color: #757575;
        font-family: microsoft yahei ui;
    }
    ol li a span.wenzi {
        color: #10a7ed;
        font-size: 14px;
        margin-bottom: 72px;
        display: block;
    }
    /*li a 当前状态*/
    ol li a.tab_l {
        display: block;
        float: left;
        background: url(/_layouts/15/TeacherImages/tab_l.png) no-repeat center;
        width: 214px;
    }
    ol li a.tab_c {
        display: block;
        float: left;
        background: url(/_layouts/15/TeacherImages/tab_c_d.png) no-repeat center;
        width: 214px;
    }
    ol li a.tab_r {
        display: block;
        float: left;
        background: url(/_layouts/15/TeacherImages/tab_r_d.png) no-repeat center;
        width: 214px;
    }
    /*li a  选中状态 */
    ol li.selected a.tab_l {
        display: block;
        float: left;
        background: url(/_layouts/15/TeacherImages/tab_l.png) no-repeat center;
        width: 214px;
    }
    ol li.selected a.tab_c {
        display: block;
        float: left;
        background: url(/_layouts/15/TeacherImages/tab_c.png) no-repeat center;
        width: 214px;
    }
    ol li.selected a.tab_r {
        display: block;
        float: left;
        background: url(/_layouts/15/TeacherImages/tab_r.png) no-repeat center;
        width: 214px;
    }
</style>
<script type="text/javascript">
    function getAnswer() {
        var answer = ""; var count = 0; 
        var li = $(".shijuan_nr").find("li");
        var liNaN = new Array(li.length);
        for (var i = 0; i < li.length; i++) {
            var type = $(li[i]).closest('ul').attr("id");
            if (type == "xz1"){
                var singleChk = $(li[i]).find("input:checked");
                if (singleChk.length > 0) {
                    answer += (i + 1) + "," + singleChk[0].value + ";";
                    count += parseInt($(singleChk[0]).parent().find("span").text());
                } else {
                    liNaN[i] = li[i];
                }
            }
            else if(type == "xz2"){
                var mutiChk = $(li[i]).find("input:checked");
                if (mutiChk.length > 0) {
                    answer += (i + 1) + ",";
                    for (var j = 0; j < mutiChk.length; j++) {
                        answer += mutiChk[j].value;
                        count += parseInt($(mutiChk[j]).parent().find("span").text());
                    }
                    answer += ";";
                } else {
                    liNaN[i] = li[i];
                }
            }
            else {
                var questext = $(li[i]).find("textarea").text();
                if (questext != "") {
                    answer += (i + 1) + "," + questext + ";";
                } else {
                    liNaN[i] = li[i];
                }
            }
        }
        var isok=true;
        for (var m = 0; m < liNaN.length; m++) {
            if (liNaN[m]) {
                $(liNaN[m]).addClass("red");
                isok = false;
            }
            else {
                $(liNaN[m]).removeClass("red");
            }
        }
        if (isok) {
            $("#hfAnswer").val(answer + "#" + count);
            $("#submit").click();
        }
        else {
            alert("您还有题目没有答完");
        }
    }
</script>
<dl class="my_kc">
    <dt class="ty_biaoti">
        <span class="active">问卷调查</span>
    </dt> 
    <dd runat="server" id="flag" visible="false">  
        <ol>
            <li class="selected"><a href="#" class="tab_l"><span class="wenzi">完善信息</span></a></li>
            <li class="selected"><a href="#" class="tab_c"><span class="wenzi">选题组卷</span></a></li>
            <li class="selected"><a href="#" class="tab_r"><span class="wenzi">完成预览</span></a></li>
        </ol>
    </dd>
    <dd class="shijuan_nr">
        <h1><asp:Literal runat="server" ID="lt_Title" /></h1>
        <div class="fubiaoti">
            <p>（问卷类别：<asp:Literal runat="server" ID="lt_Type" /></p>
            <p>评分类型：<asp:Literal runat="server" ID="lt_Target" /></p>
            <p>时间：<asp:Literal runat="server" ID="lt_Date" /></p>
            <p>被评价人：<asp:DropDownList ID="DDL_Informant" runat="server"/>）</p>
        </div>
        <asp:ListView ID="lvSingle" runat="server">
            <EmptyDataTemplate>
                <p style="text-align: center">此问卷无单选题目</p>
            </EmptyDataTemplate>
            <LayoutTemplate>
                <div class="zhubiaoti">
                    <h2>第一部分单选题</h2>
                </div>
                <ul class="zhubiaoti_nr" id="xz1">
                    <li id="itemPlaceholder" runat="server"></li>
                </ul>
            </LayoutTemplate>
            <ItemTemplate>
                <li>
                    <p><%#Eval("SortID")%>、<%#Eval("Title")%></p>
                    <label><input type="radio" name="xz<%#Eval("SortID")%>" value="A">A、<%#Eval("AnswerA")%>（<span><%#Eval("AScore")%></span>分）</label>
                    <label><input type="radio" name="xz<%#Eval("SortID")%>" value="B">B、<%#Eval("AnswerB")%>（<span><%#Eval("BScore")%></span>分）</label>
                    <label><input type="radio" name="xz<%#Eval("SortID")%>" value="C">C、<%#Eval("AnswerC")%>（<span><%#Eval("CScore")%></span>分）</label>
                    <label><input type="radio" name="xz<%#Eval("SortID")%>" value="D">D、<%#Eval("AnswerD")%>（<span><%#Eval("DScore")%></span>分）</label>
                </li>
            </ItemTemplate>
        </asp:ListView>
        <asp:ListView ID="lvMuti" runat="server">
            <EmptyDataTemplate>
                <p style="text-align: center">此问卷无多选题目</p>
            </EmptyDataTemplate>
            <LayoutTemplate>
                <div class="zhubiaoti">
                    <h2>第二部分多选题</h2>
                </div>
                <ul class="zhubiaoti_nr" id="xz2">
                    <li id="itemPlaceholder" runat="server"></li>
                </ul>
            </LayoutTemplate>
            <ItemTemplate>
                <li>
                    <p><%#Eval("SortID")%>、<%#Eval("Title")%></p>
                    <label><input type="checkbox" value="A">A、<%#Eval("AnswerA")%>（<span><%#Eval("AScore")%></span>分）</label>
                    <label><input type="checkbox" value="B">B、<%#Eval("AnswerB")%>（<span><%#Eval("BScore")%></span>分）</label>
                    <label><input type="checkbox" value="C">C、<%#Eval("AnswerC")%>（<span><%#Eval("CScore")%></span>分）</label>
                    <label><input type="checkbox" value="D">D、<%#Eval("AnswerD")%>（<span><%#Eval("DScore")%></span>分）</label>
                </li>
            </ItemTemplate>
        </asp:ListView>  
        <asp:ListView ID="lvQuestion" runat="server">
            <EmptyDataTemplate>
                <p style="text-align: center">此问卷无问答题目</p>
            </EmptyDataTemplate>
            <LayoutTemplate>      
                <div class="zhubiaoti">
                    <h2>第三部分问答题</h2>
                </div>
                <ul class="zhubiaoti_nr" id="wd">
                    <li id="itemPlaceholder" runat="server"></li>
                </ul>
            </LayoutTemplate>
            <ItemTemplate>
                <li>
                    <p><%#Eval("SortID")%>、<%#Eval("Title")%></p>
                    <textarea style="border-radius: 3px; border: 1px solid #ccc; width: 100%; line-height: 18px;" rows="2" cols="20"></textarea>
                </li>
            </ItemTemplate>
        </asp:ListView>
        <div class="sj_tijiao">
            <a href="javascript:void(0)" onclick="getAnswer()" runat="server" id="aSubmit">提交</a>
        </div>
        <div style="display:none">
            <asp:HiddenField runat="server" ClientIDMode="Static" ID="hfAnswer" />
            <asp:Button runat="server" ClientIDMode="Static" ID="submit" Text="提交" OnClientClick="" OnClick="btnSubmit_Click" />
        </div>
    </dd>
</dl>