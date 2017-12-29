<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ES_wp_AddExamQuestionUserControl.ascx.cs" Inherits="SVDigitalCampus.Examination_System.ES_wp_AddExamQuestion.ES_wp_AddExamQuestionUserControl" %>
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/common.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/style.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/iconfont.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/animate.css" />
<link href="../../../../_layouts/15/SVDigitalCampus/Script/KindUeditor/themes/default/default.css" rel="stylesheet" />
<script src="/_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script type="text/javascript" src="/_layouts/15/SVDigitalCampus/Script/tz_slider.js"></script>
<script src="../../_layouts/15/SVDigitalCampus/Script/popwin.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/KindUeditor/kindeditor-min.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/KindUeditor/plugins/code/prettify.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/KindUeditor/lang/zh_CN.js"></script>

<script type="text/javascript">
    $(function () {
        $("#ckoptiondiv").hide();
        $("#optiondiv").hide();
        $("#answerdiv").hide();
        $("#judgediv").hide();
    });
    var Questioneditor;
    var Analysiseditor;
    KindEditor.ready(function (K) {
        Questioneditor = K.create('#<%=Question.ClientID%>', {
            uploadJson: '<%=layoutstr%>' + '/SVDigitalCampus/ExamSystemHander/ExamSystemDataHander.aspx?action=Upload_json',
            //fileManagerJson: '<%=layoutstr%>' + '/SVDigitalCampus/ExamSystemHander/ExamSystemDataHander.aspx?action=FileManager',
            //allowFileManager: true,
            width: '600px',
            minWidth: '600px',
            items: [
						'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline',
						'removeformat', '|', 'justifyleft', 'justifycenter', 'justifyright', 'insertorderedlist',
						'insertunorderedlist', '|', 'emoticons', 'image', 'link'],
            afterBlur: function () { this.sync(); }
        });
        Analysiseditor = K.create('#<%=Analysis.ClientID%>', {
            uploadJson: '<%=layoutstr%>' + '/SVDigitalCampus/ExamSystemHander/ExamSystemDataHander.aspx?action=Upload_json',
            //fileManagerJson: '<%=layoutstr%>' + '/SVDigitalCampus/ExamSystemHander/ExamSystemDataHander.aspx?action=FileManager',
            //allowFileManager: true,
            width: '600px',
            minWidth: '600px',
            items: [
                      'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline',
                      'removeformat', '|', 'justifyleft', 'justifycenter', 'justifyright', 'insertorderedlist',
                      'insertunorderedlist', '|', 'emoticons', 'image', 'link'],
            afterBlur: function () { this.sync(); }
        });
    })
</script>

<div class="Question">
    <table class="qtbEdit">
        <tr>
            <td>*专业：</td>
            <td>
                <select id="Major" name="Major" runat="server" onchange="bindSubject();">
                    <option value="0">请选择</option>
                </select></td>
            <td>学科：</td>
            <td>
                <select id="Subject" name="Subject" runat="server" onchange="bindChapter();">
                    <option value="0">请选择</option>
                </select></td>
            <td>章：</td>
            <td>
                <select id="Chapter" name="Chapter" runat="server" onchange="bindPart();">
                    <option value="0">请选择</option>
                </select></td>
            <td>节：</td>
            <td>
                <select id="Part" name="Part" runat="server" onchange="bindPoint();">
                    <option value="0">请选择</option>
                </select></td>
        </tr>
        <tr>
            <td>知识点：</td>
            <td>
                <select id="Point" name="Point" runat="server">
                    <option value="0">请选择</option>
                </select></td>
            <td>*题型：</td>
            <td>
                <select id="Type" name="Type" runat="server" onchange="BindOption();">
                    <option value="0">请选择</option>
                    <option value="01">测试一</option>
                    <option value="02">测试二</option>
                    <option value="03">测试三</option>
                </select></td>
            <td>*难易程度：</td>
            <td>
                <select id="Difficulty" name="Difficulty" runat="server">
                    <option value="0">请选择</option>
                    <option value="1">简单</option>
                    <option value="2">中等</option>
                    <option value="3">较难</option>
                </select></td>
            <td>*状态：</td>
            <td>
                <select id="Status" name="Status" runat="server">
                    <option value="1">启用</option>
                    <option value="2">禁用</option>
                </select></td>
        </tr>
        <tr>
            <td>*标题：</td>
            <td colspan="7" class="ku">
                <input id="Title" name="Title" type="text" placeholder="请输入标题" runat="server" /></td>
        </tr>
        <tr>
            <td>*题文：</td>
            <td colspan="7">
                <textarea id="Question" name="Question" class="questionarea" placeholder="请输入题目" runat="server"></textarea>
            </td>
        </tr>
        <tr>
            <td></td>
            <td colspan="7" class="ku">
                <div id="optiondiv">
                    <i class="iconfont tishi fault_t">&#xe62c;</i><input id="rdoOptionA" name="answer" type="radio" onclick="oneicochange(this);" value="A" /><input id="OptionA" type="text" name="OptionA" placeholder="请输入选项" /><br />
                    <i class="iconfont tishi fault_t">&#xe62c;</i><input id="rdoOptionB" name="answer" type="radio" onclick="oneicochange(this);" value="B" /><input id="OptionB" type="text" name="OptionB" placeholder="请输入选项" /><br />
                    <i class="iconfont tishi fault_t">&#xe62c;</i><input id="rdoOptionC" name="answer" type="radio" onclick="oneicochange(this);" value="C" /><input id="OptionC" type="text" name="OptionC" placeholder="请输入选项" /><br />
                    <i class="iconfont tishi fault_t">&#xe62c;</i><input id="rdoOptionD" name="answer" type="radio" onclick="oneicochange(this);" value="D" /><input id="OptionD" type="text" name="OptionD" placeholder="请输入选项" /><br />
                    <i class="iconfont tishi fault_t">&#xe62c;</i><input id="rdoOptionE" name="answer" type="radio" onclick="oneicochange(this);" value="E" /><input id="OptionE" type="text" name="OptionE" placeholder="请输入选项" /><br />
                    <i class="iconfont tishi fault_t">&#xe62c;</i><input id="rdoOptionF" name="answer" type="radio" onclick="oneicochange(this);" value="F" /><input id="OptionF" type="text" name="OptionF" placeholder="请输入选项" />
                </div>
                <div id="ckoptiondiv">
                    <i class="iconfont tishi fault_t">&#xe62c;</i><input id="cbOptionA" name="danswer" type="checkbox" onclick="duoicochange(this);" value="A" /><input id="ckOptionA" type="text" name="OptionA" placeholder="请输入选项" /><br />
                    <i class="iconfont tishi fault_t">&#xe62c;</i><input id="cbOptionB" name="danswer" type="checkbox" onclick="duoicochange(this);" value="B" /><input id="ckOptionB" type="text" name="OptionB" placeholder="请输入选项" /><br />
                    <i class="iconfont tishi fault_t">&#xe62c;</i><input id="cbOptionC" name="danswer" type="checkbox" onclick="duoicochange(this);" value="C" /><input id="ckOptionC" type="text" name="OptionC" placeholder="请输入选项" /><br />
                    <i class="iconfont tishi fault_t">&#xe62c;</i><input id="cbOptionD" name="danswer" type="checkbox" onclick="duoicochange(this);" value="D" /><input id="ckOptionD" type="text" name="OptionD" placeholder="请输入选项" /><br />
                    <i class="iconfont tishi fault_t">&#xe62c;</i><input id="cbOptionE" name="danswer" type="checkbox" onclick="duoicochange(this);" value="E" /><input id="ckOptionE" type="text" name="OptionE" placeholder="请输入选项" /><br />
                    <i class="iconfont tishi fault_t">&#xe62c;</i><input id="cbOptionF" name="danswer" type="checkbox" onclick="duoicochange(this);" value="F" /><input id="ckOptionF" type="text" name="OptionF" placeholder="请输入选项" />
                </div>
                <div id="answerdiv">
                    <asp:TextBox ID="canswer" CssClass="Analysisarea" TextMode="MultiLine" placeholder="参考答案" runat="server"></asp:TextBox>
                </div>
                <div id="judgediv">
                    <i class="iconfont tishi fault_t">&#xe62c;</i><input id="rdoOpA" name="panswer" type="radio" onclick="panduanchanage(this);" value="A" />正确<br />
                    <i class="iconfont tishi fault_t">&#xe62c;</i><input id="rdoOpB" name="panswer" type="radio" onclick="panduanchanage(this);" value="B" />错误<br />
                </div>
            </td>
        </tr>
        <tr>
            <td rowspan="2">解析：</td>
            <td colspan="7">
                <input id="rdoisshowY" name="isshowanalysis" value="1" type="radio" />显示
                     <input id="rdoisshowN" name="isshowanalysis" value="2" type="radio" checked="true" />不显示
            </td>
        </tr>
        <tr>
            <td colspan="7">
                <textarea id="Analysis" name="Analysis" class="Analysisarea" placeholder="请输入解析" runat="server"></textarea>
            </td>
        </tr>

    </table>
    <div class="t_btn">
        <input id="btnAdd" class="btn" onclick="Add(this)" type="button" value="新增"/>
    </div>
</div>

<script type="text/javascript">
    //单选图标改变
    function oneicochange(obj) {
        $("input[name$='answer']").each(function () { $(this).prev().attr("class", "iconfont tishi fault_t"); $(this).prev().html("&#xe62c;"); });

        $(obj).prev().attr("class", "iconfont tishi true_t");
        $(obj).prev().html("&#xe62b;");
    }
    function panduanchanage(obj) {
        $("input[name$='panswer']").each(function () { $(this).prev().attr("class", "iconfont tishi fault_t"); $(this).prev().html("&#xe62c;"); });
        $(obj).prev().attr("class", "iconfont tishi true_t");
        $(obj).prev().html("&#xe62b;");
    }
    //多选图标改变
    function duoicochange(obj) {
        if ($(obj).prev().attr("class") == "iconfont tishi fault_t") {
            $(obj).prev().attr("class", "iconfont tishi true_t");
            $(obj).prev().html("&#xe62b;");
            $(obj).checked = true;
        } else {
            $(obj).prev().attr("class", "iconfont tishi fault_t");
            $(obj).prev().html("&#xe62c;");
            $(obj).checked = false;
        }
    }
    function Add(obj) {
        var Analysis = Analysiseditor.text();
        var Question = Questioneditor.text();
        var Major = $("select[id$='Major']").val();
        var Subject = $("select[id$='Subject']").val();
        var Chapter = $("select[id$='Chapter']").val();
        var Part = $("select[id$='Part']").val();
        var Point = $("select[id$='Point']").val();
        var Majortit = $("select[id$='Major']").text();
        var Subjecttit = $("select[id$='Subject']").find("option:selected").text();
        var Chaptertit = $("select[id$='Chapter']").find("option:selected").text();
        var Parttit = $("select[id$='Part']").find("option:selected").text();
        var Pointtit = $("select[id$='Point']").find("option:selected").text();
        var Typetit = $("select[id$='Type']").find("option:selected").text();
        var Type = $("select[id$='Type']").val();
        var Difficulty = $("select[id$='Difficulty']").val();
        var OptionA = "";
        var OptionB = "";
        var OptionC = "";
        var OptionD = "";
        var OptionE = "";
        var OptionF = "";
        var Answer = "";
        var isshowanalysis = $('input[name="isshowanalysis"]:checked').val();
        //获取选项
        if ($("#optiondiv").css("display") == "block") {
            OptionA = $("input[id='OptionA']").val();
            OptionB = $("input[id='OptionB']").val();
            OptionC = $("input[id='OptionC']").val();
            OptionD = $("input[id='OptionD']").val();
            OptionE = $("input[id='OptionE']").val();
            OptionF = $("input[id='OptionF']").val();
            Answer = $("#optiondiv input[name='answer']:checked").val();
        } else if ($("#ckoptiondiv").css("display") == "block") {
            OptionA = $("input[id='ckOptionA']").val();
            OptionB = $("input[id='ckOptionB']").val();
            OptionC = $("input[id='ckOptionC']").val();
            OptionD = $("input[id='ckOptionD']").val();
            OptionE = $("input[id='ckOptionE']").val();
            OptionF = $("input[id='ckOptionF']").val();
            //拼接答案
            $("input[name$='danswer']:checked").each(function () {
                if (Answer == "") {
                    Answer = $(this).val();
                }
                else {
                    Answer = Answer + "&" + $(this).val();
                }
            });
        } else if ($("#judgediv").css("display") == "block")//判断
        {
            OptionA = "正确";
            OptionB = "错误";
            Answer = $("input[name='panswer']:checked").val();
        }

        var CAnswer = $("input[name$='canswer']").val();
        var Status = $("select[id$='Status']").val();
        var Title = $("input[id$='Title']").val();
        if (CheckNull()) {
            obj.disabled = true;
            jQuery.ajax({
                url: '<%=layoutstr%>' + "/SVDigitalCampus/ExamSystemHander/ExamSystemDataHander.aspx?action=AddExamQuestion&" + Math.random(),   // 提交的页面
                type: "POST",                   // 设置请求类型为"POST"，默认为"GET"
                data: {
                    "Title": Title,
                    "Major": Major,
                    "Subject": Subject,
                    "Chapter": Chapter,
                    "Part": Part,
                    "Point": Point,
                    "Majortit": Majortit,
                    "Subjecttit": Subjecttit,
                    "Chaptertit": Chaptertit,
                    "Parttit": Parttit,
                    "Pointtit": Pointtit,
                    "Type": Type,
                    "Typetit": Typetit,
                    "Difficulty": Difficulty,
                    "Question": Question,
                    "OptionA": OptionA,
                    "OptionB": OptionB,
                    "OptionC": OptionC,
                    "OptionD": OptionD,
                    "OptionE": OptionE,
                    "OptionF": OptionF,
                    "Answer": Answer,
                    "CAnswer": CAnswer,
                    "Analysis": Analysis,
                    "Status": Status,
                    "isshowanalysis": isshowanalysis
                },
                beforeSend: function ()          // 设置表单提交前方法
                {
                    //alert("准备提交数据");


                },
                error: function (request) {      // 设置表单提交出错
                    //alert("表单提交出错，请稍候再试");
                    //rebool = false;
                },
                success: function (data) {
                    var ss = data.split("|");
                    if (ss[0] == "1") {
                        alert("新增成功！");
                        parent.location.href = parent.location.href;
                        //document.parentWindow.location.href = document.parentWindow.location.href;
                        //window.parent.location.href = window.parent.location.href;
                        //window.parent.location.reload(true);
                    }
                    else {
                        alert(ss[1]);
                    }
                }

            });
        }
    }
    //判断非空（专业/题型/）
    function CheckNull() {
        var result = false;
        var Major = $("select[id$='Major']").val();
        var Type = $("select[id$='Type']").val();
        var Question = Questioneditor.text();
        var Title = $("input[id$='Title']").val();
        var Difficulty = $("select[id$='Difficulty']").val();
        //客观(单选)
        if ($("#optiondiv").css("display") == "block") {
            var Answer = $("input[name='answer']:checked").val();
            var OptionA = $("input[id='OptionA']").val();
            if (Title != null && Title.trim() != "" && Major != null && Major != "0" && Type != null && Type != "请选择" && Type != "0" && Question != null && Question.trim() != "" && OptionA != null && OptionA.trim() != "" && Answer != null && Answer != "" && Difficulty != "0") {
                result = true;

            } else {
                if (Title == null || Title.trim() == "") {
                    alert('请输入唯一的标题！');
                }
                else if (Major == null || Major == "0") { alert('请选择专业！'); }
                else if (Type == null || Type == "0" || Type == "请选择") { alert('请选择试题类型！'); }
                else if (Question == null || Question.trim() == "") { alert('请输入题文！'); }
                else if (OptionA == null || OptionA.trim() == "") { alert('请输入一个以上选项,从选项A开始！'); }
                else if (Answer == null || Answer == "") { alert('请选择答案！'); }
                else if (Difficulty == null || Difficulty == "0") { alert('请选择试题难度！'); }
            }
        }//主观
        else if ($("#answerdiv").css("display") == "block" && $("#ckoptiondiv").css("display") == "none" && $("#optiondiv").css("display") == "none") {
            if (Title != null && Title.trim() != "" && Major != null && Major != "0" && Type != null && Type != "0" && Question != null && Question.trim() != "" && Difficulty != "0") {
                result = true;
            } else {
                if (Title != null && Title.trim() != "") {
                    alert('请输入唯一的标题！');
                }
                else if (Major == null || Major == "0") { alert('请选择专业！'); }
                else if (Type == null || Type == "0" || Type == "请选择") { alert('请选择试题类型！'); }
                else if (Question == null || Question.trim() == "") { alert('请输入题文！'); }
                else if (Difficulty == null || Difficulty == "0") { alert('请选择试题难度！'); }
            }
        }
            //客观（多选）
        else if ($("#answerdiv").css("display") == "none" && $("#ckoptiondiv").css("display") == "block" && $("#optiondiv").css("display") == "none") {
            var Answer = "";
            $("input[name$='danswer']:checked").each(function () {
                if (Answer == "") {
                    Answer = $(this).val();
                }
                else {
                    Answer = Answer + "&" + $(this).val();
                }
            });
            var OptionA = $("input[id$='ckOptionA']").val();
            if (Title != null && Title.trim() != "" && Major != null && Major != "0" && Type != null && Type != "0" && Question != null && Question.trim() != "" && OptionA != null && OptionA.trim() != "" && Answer != null && Answer != "" && Difficulty != "0") {
                result = true;
            } else {
                if (Title == null || Title.trim() == "") {
                    alert('请输入唯一的标题！');
                }
                else if (Major == null || Major == "0") { alert('请选择专业！'); }
                else if (Type == null || Type == "0" || Type == "请选择") { alert('请选择试题类型！'); }
                else if (Question == null || Question.trim() == "") { alert('请输入题文！'); }
                else if (OptionA == null || OptionA.trim() == "") { alert('请输入一个以上选项,从选项A开始！'); }
                else if (Answer == null || Answer == "") { alert('请选择答案！'); }
                else if (Difficulty == null || Difficulty == "0") { alert('请选择试题难度！'); }
            }
        }
            //判断
        else if ($("#judgediv").css("display") == "block") {
            var Answer = $("input[name='panswer']:checked").val();
            if (Title != null && Title.trim() != "" && Major != null && Major != "0" && Type != null && Type != "0" && Question != null && Question.trim() != "" && Answer != null && Answer != "" && Difficulty != "0") {
                result = true;
            } else {
                if (Title == null || Title.trim() == "") {
                    alert('请输入唯一的标题！');
                }
                else if (Major == null || Major == "0") { alert('请选择专业！'); }
                else if (Type == null || Type == "0" || Type == "请选择") { alert('请选择试题类型！'); }
                else if (Question == null || Question.trim() == "") { alert('请输入题文！'); }
                else if (Answer == null || Answer == "") { alert('请选择答案！'); }
                else if (Difficulty == null || Difficulty == "0") { alert('请选择试题难度！'); }
            }
        } else {
            if (Title == null || Title.trim() == "") {
                alert('请输入唯一的标题！');
            }
            else if (Major == null || Major == "0") { alert('请选择专业！'); }
            else if (Type == null || Type == "0" || Type == "请选择") { alert('请选择试题类型！'); }
            else if (Question == null || Question.trim() == "") { alert('请输入题文！'); }
            else if (Difficulty == null || Difficulty == "0") { alert('请选择试题难度！'); }
        }
        return result;
    }
    function bindSubject() {
        var Majorid = $("select[id$='Major']").val();
        if (Majorid != "0") {
            jQuery.ajax({
                url: '<%=layoutstr%>' + "/SVDigitalCampus/ExamSystemHander/ExamSystemDataHander.aspx?action=GetChildNode&" + Math.random(),
            data: { pid: Majorid, nodes: "1" },
            type: "POST",
            success: function (data) {
                $("select[id$='Subject']").children().remove();
                $("select[id$='Subject']").append("<option selected=\"selected\" value=\"0\">请选择</option>" + data);
                if ($("select[id$='Subject'] option:selected").val() != null && $("select[id$='Subject'] option:selected").val() != 0) {
                    //$("#county").show();
                }
            }
        });
    }
}
function bindChapter() {
    var Majorid = $("select[id$='Major']").val();
    var Subjectid = $("select[id$='Subject']").val();
    if (Majorid != "0" && Subjectid != "0") {
        jQuery.ajax({
            url: '<%=layoutstr%>' + "/SVDigitalCampus/ExamSystemHander/ExamSystemDataHander.aspx?action=GetChildNode&" + Math.random(),
            data: { pid: Majorid + Subjectid, nodes: "2" },
            type: "POST",
            success: function (data) {
                $("select[id$='Chapter']").children().remove();
                $("select[id$='Chapter']").append("<option selected=\"selected\" value=\"0\">请选择</option>" + data);
                if ($("select[id$='Chapter'] option:selected").val() != null && $("select[id$='Chapter'] option:selected").val() != 0) {
                    //$("#county").show();
                }
            }
        });
    }
}
function bindPart() {
    var ChapterID = $("select[id$='Chapter']").val();
    if (ChapterID != "0") {
        jQuery.ajax({
            url: '<%=layoutstr%>' + "/SVDigitalCampus/ExamSystemHander/ExamSystemDataHander.aspx?action=GetChildNode&" + Math.random(),
            data: { pid: ChapterID, nodes: "3" },
            type: "POST",
            success: function (data) {
                $("select[id$='Part']").children().remove();
                $("select[id$='Part']").append("<option selected=\"selected\" value=\"0\">请选择</option>" + data);
                if ($("select[id$='Part'] option:selected").val() != null && $("select[id$='Part'] option:selected").val() != 0) {
                    //$("#county").show();
                }
            }
        });
    }
}
function bindPoint() {
    var PartID = $("select[id$='Part']").val();
    if (PartID != "0") {
        jQuery.ajax({
            url: '<%=layoutstr%>' + "/SVDigitalCampus/ExamSystemHander/ExamSystemDataHander.aspx?action=GetChildNode&" + Math.random(),
            data: { pid: PartID, nodes: "4" },
            type: "POST",
            success: function (data) {
                $("select[id$='Point']").children().remove();
                $("select[id$='Point']").append("<option selected=\"selected\" value=\"0\">请选择</option>" + data);
                if ($("select[id$='Point'] option:selected").val() != null && $("select[id$='Point'] option:selected").val() != 0) {
                    //$("#county").show();
                }
            }
        });
    }
}
function BindOption() {
    var TypeID = $("select[id$='Type'] option:selected").val();
    if (TypeID != "0") {
        jQuery.ajax({
            url: '<%=layoutstr%>' + "/SVDigitalCampus/ExamSystemHander/ExamSystemDataHander.aspx?action=GetOption&" + Math.random(),
            data: { tid: TypeID },
            type: "POST",
            success: function (result) {
                var ss = result.split("|");
                if (ss[0] == "1") {
                    if (ss[1] == "2") {//单选

                        $("#optiondiv").show();
                        $("#ckoptiondiv").hide();
                        $("#answerdiv").hide();
                        $("#judgediv").hide();
                        //清空答案
                        $("input[name$='danswer']").attr("checked", false);
                        $("input[name$='answer']").attr("checked", false);
                        //清空选项
                        $("input[name$='OptionA']").val("");
                        $("input[name$='OptionB']").val("");
                        $("input[name$='OptionC']").val("");
                        $("input[name$='OptionD']").val("");
                        $("input[name$='OptionE']").val("");
                        $("input[name$='OptionF']").val("");
                        //清空答案选中图标为未选中
                        $("input[name$='answer']").each(function () { $(this).prev().attr("class", "iconfont tishi fault_t"); $(this).prev().html("&#xe62c;"); });
                        $("input[name$='danswer']").each(function () { $(this).prev().attr("class", "iconfont tishi fault_t"); $(this).prev().html("&#xe62c;"); });

                    } else if (ss[1] == "3") {//多选

                        $("#ckoptiondiv").show();
                        $("#optiondiv").hide();
                        $("#answerdiv").hide();
                        $("#judgediv").hide();
                        //清空答案
                        $("input[id$='canswer']").val("");
                        $("input[name$='danswer']").attr("checked", false);
                        $("input[name$='answer']").attr("checked", false);
                        //清空选项
                        $("input[name$='OptionA']").val("");
                        $("input[name$='OptionB']").val("");
                        $("input[name$='OptionC']").val("");
                        $("input[name$='OptionD']").val("");
                        $("input[name$='OptionE']").val("");
                        $("input[name$='OptionF']").val("");
                        //清空选中图标为未选中
                        $("input[name$='answer']").each(function () { $(this).prev().attr("class", "iconfont tishi fault_t"); $(this).prev().html("&#xe62c;"); });
                        $("input[name$='danswer']").each(function () { $(this).prev().attr("class", "iconfont tishi fault_t"); $(this).prev().html("&#xe62c;"); });
                        //icochange();
                    } else if (ss[1] == "1")//判断
                    {
                        $("#judgediv").show();
                        $("#answerdiv").hide();
                        $("#ckoptiondiv").hide();
                        $("#optiondiv").hide();
                        //清空选项
                        $("input[name$='OptionA']").val("");
                        $("input[name$='OptionB']").val("");
                        $("input[name$='OptionC']").val("");
                        $("input[name$='OptionD']").val("");
                        $("input[name$='OptionE']").val("");
                        $("input[name$='OptionF']").val("");
                        //清空答案
                        $("input[name$='danswer']").attr("checked", false);
                        $("input[name$='answer']").attr("checked", false);
                        //清空选中图标为未选中
                        $("input[name$='answer']").each(function () { $(this).prev().attr("class", "iconfont tishi fault_t"); $(this).prev().html("&#xe62c;"); });
                        $("input[name$='danswer']").each(function () { $(this).prev().attr("class", "iconfont tishi fault_t"); $(this).prev().html("&#xe62c;"); });

                    }
                    else if (ss[1] == "4") {//文本
                        $("#answerdiv").show();
                        $("#ckoptiondiv").hide();
                        $("#optiondiv").hide();
                        $("#judgediv").hide();
                        //清空答案
                        $("input[id$='canswer']").val("");
                        //清空选中图标为未选中
                        $("input[name$='answer']").each(function () { $(this).prev().attr("class", "iconfont tishi fault_t"); $(this).prev().html("&#xe62c;"); });
                        $("input[name$='danswer']").each(function () { $(this).prev().attr("class", "iconfont tishi fault_t"); $(this).prev().html("&#xe62c;"); });
                    } else {
                        $("#judgediv").hide();
                        $("#optiondiv").hide();
                        $("#ckoptiondiv").hide();
                        $("#answerdiv").hide();
                        $("input[id$='canswer']").val("");
                        $("input[name$='answer']").attr("checked", false);
                        $("input[name$='OptionA']").val("");
                        $("input[name$='OptionB']").val("");
                        $("input[name$='OptionC']").val("");
                        $("input[name$='OptionD']").val("");
                        $("input[name$='OptionE']").val("");
                        $("input[name$='OptionF']").val("");
                    }
                } else {
                    $("#judgediv").hide();
                    $("#optiondiv").hide();
                    $("#ckoptiondiv").hide();
                    $("#answerdiv").hide();
                    $("input[id$='canswer']").val("");
                    $("input[name$='answer']").attr("checked", false);
                    $("input[name$='OptionA']").val("");
                    $("input[name$='OptionB']").val("");
                    $("input[name$='OptionC']").val("");
                    $("input[name$='OptionD']").val("");
                    $("input[name$='OptionE']").val("");
                    $("input[name$='OptionF']").val("");
                }
            }
        });
    }

    else {
        $("#judgediv").hide();
        $("#optiondiv").hide();
        $("#ckoptiondiv").hide();
        $("#answerdiv").hide();
        $("input[id$='canswer']").val("");
        $("input[name$='answer']").attr("checked", false);
        $("input[name$='OptionA']").val("");
        $("input[name$='OptionB']").val("");
        $("input[name$='OptionC']").val("");
        $("input[name$='OptionD']").val("");
        $("input[name$='OptionE']").val("");
        $("input[name$='OptionF']").val("");
    }
}

</script>
