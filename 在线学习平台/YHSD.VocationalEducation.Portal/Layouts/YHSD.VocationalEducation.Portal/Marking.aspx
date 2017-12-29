<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Marking.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.Marking" %>

<html>
<head>
    <title></title>

    <script src="js/layer/jquery.min.js"></script>
    <script src="js/layer/layer.js"></script>
    <script src="js/layer/OpenLayer.js"></script>
    <script src="js/FormatUtil.js"></script>
    <script src="js/json.js"></script>
    <link href="css/type.css" rel="stylesheet" />
    <style type="text/css">
        .paperInfo {
            text-align: center;
        }

        .paperInfo_tab {
            width: 600px;
            border: 0;
        }

            .paperInfo_tab td {
                width: 200px;
            }

        .orange {
            color: #df7600;
        }

        .questionContainer {
            text-align: left;
        }

        .qu_content {
            margin: 20px 0;
        }

        .q_answer {
            margin: 10px 0;
        }

        .q_score {
            margin: 10px 0;
        }

        .bottom_Toolbar {
            text-align: center;
        }

        .txtScore {
            width: 50px;
        }

        body {
            font-family: "Microsoft YaHei UI","Microsoft YaHei","微软雅黑",SimSun,"宋体", sans-serif;
            font-size: 0.9em;
            font-weight: 200;
        }
        .q_group{
            border:0px solid silver;
            margin:5px 0;
            padding:20px 0;
        }

        .q_groupTitle{
            border-bottom:1px solid silver;
            padding:5px 0;
            font-weight:bold;
            width:100%;
            cursor:pointer;
            text-align:left;
            font-size:18px;
        }
        .q_title{
            font-size:20px;
        }
        .q_option{
            font-size:18px;
            margin-right:20px;
        }
        .q_oAnswer{
            margin-right:10px;
            font-size:16px;
        }
    </style>
    <script type="text/javascript">
        var erid = "<%=Request["ERID"]%>";
        function NumToFlag(num) {
            return num == "1" ? "正确" : "错误";
        }

        function BuildChecked(stuAnswer, option) {
            var arr = stuAnswer.split(",");
            var flag = arr.indexOf(option.toString()) > -1;//如果选择了该选项
            return flag ? "checked='checked'" : "";
        }

        function StrsToLetter(strs) {
            var arr = strs.split(",");
            for (var i = 0; i < arr.length; i++) {
                arr[i] = IntegerToLetter(arr[i]);
            }
            return arr.join();
        }

        function BuildOptionAnswer(model, type) {
            var optionHtml;
            var answerHtml;
            if (model.OptionA) {
                optionHtml = "<span class='q_option'>A、" + model.OptionA + "</span>";
                answerHtml = "<span class='q_oAnswer'><input type='" + type + "' disabled='disabled' name='" + model.ID + "' value='0' " + BuildChecked(model.Correct, 0) + "/>A</span>";
            }
            if (model.OptionB) {
                optionHtml += "<span class='q_option'>B、" + model.OptionB + "</span>";
                answerHtml += "<span class='q_oAnswer'><input type='" + type + "' disabled='disabled' name='" + model.ID + "' value='1' " + BuildChecked(model.Correct, 1) + "/>B</span>";
            }
            if (model.OptionC) {
                optionHtml += "<span class='q_option'>C、" + model.OptionC + "</span>";
                answerHtml += "<span class='q_oAnswer'><input type='" + type + "' disabled='disabled' name='" + model.ID + "' value='2' " + BuildChecked(model.Correct, 2) + "/>C</span>";
            }
            if (model.OptionD) {
                optionHtml += "<span class='q_option'>D、" + model.OptionD + "</span>";
                answerHtml += "<span class='q_oAnswer'><input type='" + type + "' disabled='disabled' name='" + model.ID + "' value='3' " + BuildChecked(model.Correct, 3) + "/>D</span>";
            }
            if (model.OptionE) {
                optionHtml += "<span class='q_option'>E、" + model.OptionE + "</span>";
                answerHtml += "<span class='q_oAnswer'><input type='" + type + "' disabled='disabled' name='" + model.ID + "' value='4' " + BuildChecked(model.Correct, 4) + "/>E</span>";
            }
            if (model.OptionF) {
                optionHtml += "<span class='q_option'>F、" + model.OptionF + "</span>";
                answerHtml += "<span class='q_oAnswer'><input type='" + type + "' disabled='disabled' name='" + model.ID + "' value='5' " + BuildChecked(model.Correct, 5) + "/>F</span>";
            }
            if (model.OptionG) {
                optionHtml += "<span class='q_option'>J、" + model.OptionG + "</span>";
                answerHtml += "<span class='q_oAnswer'><input type='" + type + "' disabled='disabled' name='" + model.ID + "' value='6' " + BuildChecked(model.Correct, 6) + "/>G</span>";
            }
            if (model.OptionH) {
                optionHtml += "<span class='q_option'>H、" + model.OptionH + "</span>";
                answerHtml += "<span class='q_oAnswer'><input type='" + type + "' disabled='disabled' name='" + model.ID + "' value='7' " + BuildChecked(model.Correct, 7) + "/>H</span>";
            }
            if (model.OptionI) {
                optionHtml += "<span class='q_option'>I、" + model.OptionI + "</span>";
                answerHtml += "<span class='q_oAnswer'><input type='" + type + "' disabled='disabled' name='" + model.ID + "' value='8' " + BuildChecked(model.Correct, 8) + "/>I</span>";
            }
            if (model.OptionJ) {
                optionHtml += "<span class='q_option'>J、" + model.OptionJ + "</span>";
                answerHtml += "<span class='q_oAnswer'><input type='" + type + "' disabled='disabled' name='" + model.ID + "' value='9' " + BuildChecked(model.Correct, 9) + "/>J</span>";
            }
            return { Option: optionHtml, Answer: answerHtml };
        }

        function BuildChooseForSingle(singleModel, oi) {
            var questionId = oi.QuestionID;
            var temp = singleModel.Correct;
            singleModel.Correct = oi.StuAnswer;
            var oa = BuildOptionAnswer(singleModel, "radio");


            var str = "<div class='qu_content' questionId='" + questionId + "'>\
                            <h3>\
                                <span class='q_orderNum'>" + oi.OrderNum + "、</span><span class='q_title'>" + singleModel.Title + "</span><span class='q_score'>(" + oi.Score + "分)</sapn>\
                            </h3>\
                            <div>";
            str += oa.Option;
            str += "        </div>\
                            <div class='q_answer'>";
            str += oa.Answer;
            str += "        </div>\
                            <div class='qu_correct'>\
                            <span>正确答案：</span><span>" + StrsToLetter(temp) + "</span>\
                            </div>\
                            <div class='q_score'>\
                                <span>得分：<input type='text' class='txtScore' disabled='disabled' value='" + oi.AnswerScore + "'/></span>\
                            </div>\
                        </div>";

            return str;
        }

        function BuildChooseForMulti(multiModel, oi) {
            var questionId = oi.QuestionID;
            var temp = multiModel.Correct;
            multiModel.Correct = oi.StuAnswer;
            var oa = BuildOptionAnswer(multiModel, "checkbox");


            var str = "<div class='qu_content' questionId='" + questionId + "'>\
                        <h3>\
                            <span class='q_orderNum'>" + oi.OrderNum + "、</span><span class='q_title'>" + multiModel.Title + "</span><span class='q_score'>(" + oi.Score + "分)</sapn>\
                        </h3>\
                        <div>";
            str += oa.Option;
            str += "</div>\
                        <div class='q_answer'>";
            str += oa.Answer;
            str += "</div>\
                            <div class='qu_correct'>\
                            <span>正确答案：</span><span>" + StrsToLetter(temp) + "</span>\
                            </div>\
                            <div class='q_score'>\
                                <span>得分：<input type='text' class='txtScore' disabled='disabled' value='" + oi.AnswerScore + "'/></span>\
                            </div>\
                    </div>";

            return str;
        }

        function BuildChooseForJudge(judgeModel, oi) {
            var questionId = oi.QuestionID;
            var oa = BuildOptionAnswer(judgeModel, "radio");


            var str = "<div class='qu_content' questionId='" + questionId + "'>\
                        <h3>\
                            <span class='q_orderNum'>" + oi.OrderNum + "、</span><span class='q_title'>" + judgeModel.Title + "</span><span class='q_score'>(" + oi.Score + "分)</sapn>\
                        </h3>";
            str += "<div class='q_answer'>";
            if (oi.StuAnswer == "1") {
                str += "<span><input type='radio' value='1' disabled='disabled' name='" + judgeModel.ID + "' checked='checked'/>正确  <input type='radio' disabled='disabled' value='0' name='" + judgeModel.ID + "' />错误</span>";
            }
            else if (oi.StuAnswer == "1") {
                str += "<span><input type='radio' value='1' disabled='disabled' name='" + judgeModel.ID + "' />正确  <input type='radio' disabled='disabled' value='0' name='" + judgeModel.ID + "'  checked='checked'/>错误</span>";
            }
            else {
                str += "<span><input type='radio' value='1' disabled='disabled' name='" + judgeModel.ID + "' />正确  <input type='radio' disabled='disabled' value='0' name='" + judgeModel.ID + "'/>错误</span>";
            }
            str += "</div>\
                            <div class='qu_correct'>\
                            <span>正确答案：</span><span>" + NumToFlag(judgeModel.Correct) + "</span>\
                            </div>\
                            <div class='q_score'>\
                                <span>得分：<input type='text' class='txtScore' disabled='disabled' value='" + oi.AnswerScore + "'/></span>\
                            </div>\
                    </div>";

            return str;
        }

        function BuildEssay(essayModel, oi) {
            var questionId = oi.QuestionID;
            var str = "\<div class='qu_content' questionId='" + questionId + "' qtype='subjective'>\
                            <h3>\
                                        <span class='q_orderNum'>" + oi.OrderNum + "、</span><span class='q_title'>" + essayModel.Title + "</span><span class='q_score'>(" + oi.Score + "分)</sapn>\
                            </h3>\
                            <div class='q_answer'>\
                                <span>考生答案：</span><pre name='" + essayModel.ID + "'>" + oi.StuAnswer + "</pre>\
                            </div>\
                            <div class='qu_correct'>\
                            <span>正确答案：</span><span>" + essayModel.Correct + "</span>\
                            </div>\
                            <div class='q_score'>\
                                <span>打分<input type='text' class='txtScore' value='" + oi.AnswerScore + "'/></span>\
                            </div>\
                        </div>";
            //<textarea cols='50' rows='10' disabled='disabled' name='" + essayModel.ID + "'>" + oi.StuAnswer + "</textarea>\
            return str;
        }

        $(function () {
            if (erid == "") {
                parent.LayerAlert("异常操作！", function () {
                    OL_CloseLayerIframe(false);
                });
                return;
            }
            var loadIndex = layer.load(2);
            var postData = { CMD: "Marking", ERID: erid };
            AjaxRequest("Handler/TestMgrHandler.aspx", postData, function (returnVal) {
                var GroupInfo = JSON.parse(returnVal.GroupInfo);
                var ERInfo = JSON.parse(returnVal.ExamResult);
                var OrderInfo = JSON.parse(returnVal.OrderInfo);
                var Chooses = JSON.parse(returnVal.Chooses);
                var Essays = JSON.parse(returnVal.Essays);

                $("#hPaperTitle").text(ERInfo.PaperName);//试卷标题
                $("#sCreateUser").text(ERInfo.UserName);//考生姓名
                $("#sTotalScore").text(ERInfo.Score);//得分
                //$("#sTimer").text(ERInfo.LengthOfTime);//所用时长
                $("#sCreateTime").text(ERInfo.CreateTime);//考试时间
                for (var i = 0; i < GroupInfo.length; i++) {
                    var groupHtml = "<div class='q_group' gid='" + GroupInfo[i].ID + "'>\
                                                <div class='q_groupTitle'>" + GroupInfo[i].GroupTile + "</div>\
                                                <div class='q_groupContainer'>\
                                                </div>\
                                            </div>";
                    $(groupHtml).appendTo("#questionContainer");
                }
                for (var i = 0; i < OrderInfo.length; i++) {
                    var questionHtml = "";
                    var type = OrderInfo[i].QuestionType;
                    switch (type) {
                        case "单选题":
                            questionHtml = BuildChooseForSingle(ArrayFind(Chooses, "ID", OrderInfo[i].StoreID), OrderInfo[i]);
                            break;
                        case "多选题":
                            questionHtml = BuildChooseForMulti(ArrayFind(Chooses, "ID", OrderInfo[i].StoreID), OrderInfo[i]);
                            break;
                        case "判断题":
                            questionHtml = BuildChooseForJudge(ArrayFind(Chooses, "ID", OrderInfo[i].StoreID), OrderInfo[i]);
                            break;
                        case "简答题":
                            questionHtml = BuildEssay(ArrayFind(Essays, "ID", OrderInfo[i].StoreID), OrderInfo[i]);
                            break;
                    }
                    $(".q_group[gid='" + OrderInfo[i].GroupID + "'] .q_groupContainer").append(questionHtml);
                }
            });

            $("#btnSubmit").click(function () {
                var qContent = $(".qu_content[qtype='subjective'] .txtScore");
                var answerArr = new Array();
                $.each(qContent, function (k) {
                    var num = $(this.parentNode.parentNode.parentNode).find(".q_orderNum").text().replace("、", "");
                    var val = $(this).val();
                    if (isNaN(val)) {
                        parent.LayerAlert("分值必须是数字！", function () {
                            score = NaN;
                        });
                        return false;
                    }
                    else if (val == "" || val == null || val == undefined) {
                        parent.LayerAlert("第" + num + "题还未打分！");
                        return false;
                    }
                    else {
                        var score = parseInt($(this).val());
                        var qid = $(this.parentNode.parentNode.parentNode).attr("questionId");
                        answerArr.push({ ERID: erid, QuestionID: qid, AnswerScore: score });
                    }
                });
                if (answerArr.length != qContent.length) {
                    return;
                }

                var postData = { CMD: "SubmitScore", SubjectiveArr: JSON.stringify(answerArr), ERID: erid };
                AjaxRequest("Handler/TestMgrHandler.aspx", postData, function (returnVal) {
                    parent.LayerAlert("打分成功！", function () {
                        OL_CloseLayerIframe(true);
                    });
                })
            });
            $("#btnClose").click(function () {
                parent.LayerConfirm("所做更改将不会保存，确定取消阅卷？", function () {
                    OL_CloseLayerIframe(false);
                })
            })
        });
    </script>
</head>
<body>
    <div class="paperInfo">
        <h1 id="hPaperTitle"></h1>
        <h3 style="margin-top: 10px;">考生：<span id="sCreateUser" class="orange"></span>  当前得分：<span id="sTotalScore" class="orange"></span>分  考试时间：<span id="sCreateTime" class="orange"></span> <%--所用时长:<span id="sTimer" class="orange"></span>分钟--%></h3>
    </div>
    <hr />
    <div class="questionContainer" id="questionContainer">
    </div>
    <div class="bottom_Toolbar">
        <input type="button" value="打分" id="btnSubmit" class="button_s" /><input type="button" value="取消" id="btnClose" class="button_n" />
    </div>
</body>
</html>
