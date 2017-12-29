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
        $(obj).prev().html("&#xe62c;");d:\svdigitalcampus\svdigitalcampus\layouts\svdigitalcampus\script\questionadd.js
        $(obj).checked = false;
    }
}
function Add() {
    var Analysis = Analysiseditor.text();
    var Question = Questioneditor.text();
    var Major = $("select[id$='Major']").val();
    var Subject = $("select[id$='Subject']").val();
    var Chapter = $("select[id$='Chapter']").val();
    var Part = $("select[id$='Part']").val();
    var Point = $("select[id$='Point']").val();
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
                "Type": Type,
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
            if (Title== null|| Title.trim() == ""){
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
        if (Title == null|| Title.trim() == "") {
            alert('请输入唯一的标题！');
        }
        else if (Major == null || Major == "0") { alert('请选择专业！'); }
        else if (Type == null || Type == "0"||Type=="请选择") { alert('请选择试题类型！'); }
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
            data: { pid: Majorid,nodes:"1" },
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
    var Subjectid = $("select[id$='Subject']").val();
    if (Subjectid != "0") {
        jQuery.ajax({
            url: '<%=layoutstr%>' + "/SVDigitalCampus/ExamSystemHander/ExamSystemDataHander.aspx?action=GetChildNode&" + Math.random(),
            data: { pid: Subjectid, nodes: "2" },
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
