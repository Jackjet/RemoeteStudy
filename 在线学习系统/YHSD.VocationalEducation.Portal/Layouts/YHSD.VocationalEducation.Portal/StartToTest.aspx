﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StartToTest.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.StartToTest" %>

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
        .q_answer textarea{
            width:98%;
        }

        .bottom_Toolbar {
            text-align: center;
            margin: 0 20px;
        }

        body {
            font-family: "Microsoft YaHei UI","Microsoft YaHei","微软雅黑",SimSun,"宋体", sans-serif;
            font-size: 0.9em;
            font-weight: 200;
        }

        .q_group {
            border: 0px solid silver;
            margin: 5px 0;
            padding: 20px 0;
        }

        .q_groupTitle {
            border-bottom: 1px solid silver;
            padding: 5px 0;
            font-weight: bold;
            width: 100%;
            cursor: pointer;
            text-align: left;
            font-size: 18px;
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
        var paperID = "<%=Request["PaperID"]%>";
        var se, m = 0, h = 0, s = 0, ss = 1;
        function second() {
            if (s > 0 && (s % 60) == 0) { m += 1; s = 0; }
            if (m > 0 && (m % 60) == 0) { h += 1; m = 0; }
            t = h + "时" + m + "分" + s + "秒";
            $("#sTimer").text(t);
            s += 1;
        }
        function startclock() { se = setInterval("second()", 1000); }
        function pauseclock() { clearInterval(se); }
        function stopclock() { clearInterval(se); ss = 1; m = h = s = 0; }
        function Find(arrPerson, objPropery, objValue) {
            var arr = $.grep(arrPerson, function (cur, i) {
                return cur[objPropery] == objValue;
            });
            if (arr && arr.length > 0)
                return arr[0];
            return undefined;
        }
        function BuildOptionAnswer(model, type) {
            var optionHtml;
            var answerHtml;
            if (model.OptionA) {
                optionHtml = "<span class='q_option'>A、" + model.OptionA + "</span>";
                answerHtml = "<span class='q_oAnswer'><input type='" + type + "' name='" + model.ID + "' value='0' />A</span>";
            }
            if (model.OptionB) {
                optionHtml += "<span class='q_option'>B、" + model.OptionB + "</span>";
                answerHtml += "<span class='q_oAnswer'><input type='" + type + "' name='" + model.ID + "' value='1' />B</span>";
            }
            if (model.OptionC) {
                optionHtml += "<span class='q_option'>C、" + model.OptionC + "</span>";
                answerHtml += "<span class='q_oAnswer'><input type='" + type + "' name='" + model.ID + "' value='2' />C</span>";
            }
            if (model.OptionD) {
                optionHtml += "<span class='q_option'>D、" + model.OptionD + "</span>";
                answerHtml += "<span class='q_oAnswer'><input type='" + type + "' name='" + model.ID + "' value='3' />D</span>";
            }
            if (model.OptionE) {
                optionHtml += "<span class='q_option'>E、" + model.OptionE + "</span>";
                answerHtml += "<span class='q_oAnswer'><input type='" + type + "' name='" + model.ID + "' value='4' />E</span>";
            }
            if (model.OptionF) {
                optionHtml += "<span class='q_option'>F、" + model.OptionF + "</span>";
                answerHtml += "<span class='q_oAnswer'><input type='" + type + "' name='" + model.ID + "' value='5' />F</span>";
            }
            if (model.OptionG) {
                optionHtml += "<span class='q_option'>J、" + model.OptionG + "</span>";
                answerHtml += "<span class='q_oAnswer'><input type='" + type + "' name='" + model.ID + "' value='6' />G</span>";
            }
            if (model.OptionH) {
                optionHtml += "<span class='q_option'>H、" + model.OptionH + "</span>";
                answerHtml += "<span class='q_oAnswer'><input type='" + type + "' name='" + model.ID + "' value='7' />H</span>";
            }
            if (model.OptionI) {
                optionHtml += "<span class='q_option'>I、" + model.OptionI + "</span>";
                answerHtml += "<span class='q_oAnswer'><input type='" + type + "' name='" + model.ID + "' value='8' />I</span>";
            }
            if (model.OptionJ) {
                optionHtml += "<span class='q_option'>J、" + model.OptionJ + "</span>";
                answerHtml += "<span class='q_oAnswer'><input type='" + type + "' name='" + model.ID + "' value='9' />J</span>";
            }
            return { Option: optionHtml, Answer: answerHtml };
        }
        function BuildChooseForSingle(singleModel, oi) {
            var questionId = oi.QuestionID;
            var oa = BuildOptionAnswer(singleModel, "radio");


            var str = "<div class='qu_content' questionId='" + questionId + "'>\
                        <h3>\
                            <span class='q_orderNum'>" + oi.OrderNum + "、</span><span class='q_title'>" + singleModel.Title + "</span><span class='q_score'>(" + oi.Score + "分)</sapn>\
                        </h3>\
                        <div>";
            str += oa.Option;
            str += "</div>\
                        <div class='q_answer'>";
            str += oa.Answer;
            str += "</div>\
                    </div>";

            return str;
        }
        function BuildChooseForMulti(multiModel, oi) {
            var questionId = oi.QuestionID;
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
            str += "<span><input type='radio' value='1' name='" + judgeModel.ID + "' />正确  <input type='radio' value='0' name='" + judgeModel.ID + "' />错误</span>";
            str += "</div>\
                    </div>";

            return str;
        }
        function BuildEssay(essayModel, oi) {
            var questionId = oi.QuestionID;
            var str = "\<div class='qu_content' questionId='" + questionId + "'>\
                            <h3>\
                                        <span class='q_orderNum'>" + oi.OrderNum + "、</span><span class='q_title'>" + essayModel.Title + "</span><span class='q_score'>(" + oi.Score + "分)</sapn>\
                            </h3>\
                            <div class='q_answer'>\
                                <textarea cols='50' rows='10' name='"+ essayModel.ID + "'></textarea>\
                            </div>\
                        </div>";
            return str;
        }
        $(function () {
            if (paperID == "") {
                parent.LayerAlert("异常操作！", function () {
                    OL_CloseLayerIframe(false);
                });
            }
            var loadIndex = layer.load(2);
            var postData = { CMD: "GetPaperInfo", PaperID: paperID };
            AjaxRequest("Handler/TestMgrHandler.aspx", postData, function (returnVal) {
                var GroupInfo = JSON.parse(returnVal.GroupInfo);
                var PaperInfo = JSON.parse(returnVal.PaperInfo);
                var OrderInfo = JSON.parse(returnVal.OrderInfo);
                var Chooses = JSON.parse(returnVal.Chooses);
                var Essays = JSON.parse(returnVal.Essays);

                $("#hPaperTitle").text(PaperInfo.Title);
                //$("#sCreateUser").text(PaperInfo.CreateUser);
                $("#sTotalScore").text(PaperInfo.TotalScore);
                $("#sPassScore").text(PaperInfo.PassScore);

                for (var i = 0; i < GroupInfo.length; i++) {//分组
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
                            questionHtml = BuildChooseForSingle(Find(Chooses, "ID", OrderInfo[i].StoreID), OrderInfo[i]);
                            break;
                        case "多选题":
                            questionHtml = BuildChooseForMulti(Find(Chooses, "ID", OrderInfo[i].StoreID), OrderInfo[i]);
                            break;
                        case "判断题":
                            questionHtml = BuildChooseForJudge(Find(Chooses, "ID", OrderInfo[i].StoreID), OrderInfo[i]);
                            break;
                        case "简答题":
                            questionHtml = BuildEssay(Find(Essays, "ID", OrderInfo[i].StoreID), OrderInfo[i]);
                            break;
                        default:
                            break;
                    }
                    //$(questionHtml).appendTo("#questionContainer");
                    $(".q_group[gid='" + OrderInfo[i].GroupID + "'] .q_groupContainer").append(questionHtml);
                }
                $(".qu_content :radio").on("click", function () {
                    $(this.parentNode.parentNode.parentNode).attr("answer", this.value);
                });
                $(".qu_content :checkbox").on("change", function () {
                    var Qcontent = this.parentNode.parentNode.parentNode;
                    var flag = $(this).prop("checked");
                    var val = this.value;
                    var answer = $(Qcontent).attr("answer");
                    var arr = new Array();
                    if (answer) {
                        arr = answer.split(',');
                    }
                    if (flag) {
                        arr.push(val);
                    }
                    else {
                        arr.remove(arr.indexOf(val));
                    }
                    arr.sort(function (a, b) { return a > b ? 1 : -1 });
                    $(Qcontent).attr("answer", arr.join(","));
                });
                $(".qu_content textarea").on("change", function () {
                    var val = $(this).val();
                    $(this.parentNode.parentNode).attr("answer", val);
                });
                //startclock();//开始计时
            });
            $("#btnSubmit").click(function () {
                var arr = new Array();
                var missCount=0;
                $("#questionContainer div[questionId]").each(function (k) {
                    var qid = $(this).attr("questionId");
                    var qa = $(this).attr("answer");
                    if (qa == undefined || qa == "") {
                        missCount+=1;
                    }
                    arr.push({ QuestionID: qid, AnswerContent: qa });
                });
                if (arr.length == missCount) {
                    parent.LayerAlert("请先做题!");
                }
                else {
                    var msg = "确定交卷?";
                    if (missCount>0) {//如果还有没做的题
                        msg = "还有" + missCount + "道试题没做完,确定交卷?";
                    }
                    var cindex = parent.LayerConfirm(msg, function () {
                        var postData = { CMD: "SubmitPaper", ExamAnswer: JSON.stringify(arr), PaperID: paperID, TotalTime: m };
                        AjaxRequest("Handler/TestMgrHandler.aspx", postData, function (returnVal) {
                            parent.LayerAlert("试卷提交成功!", function () {
                                OL_CloseLayerIframe(true);
                            });
                        }, function (errMsg) {
                            parent.layer.close(cindex);
                            parent.LayerAlert("试卷提交失败!" + errMsg);
                        })
                    }, function () {
                        layer.close(cindex);
                    });
                }
            });
            $("#btnClose").click(function () {
                parent.LayerConfirm("所做答案将不会保存,确定取消考试?", function () {
                    OL_CloseLayerIframe(false);
                })
            })

        });
    </script>
</head>
<body>
    <div class="paperInfo">
        <h1 id="hPaperTitle"></h1>
        <h3 style="margin-top: 10px;"><%--出卷人:<span id="sCreateUser" class="orange"></span>--%>  总分:<span id="sTotalScore" class="orange"></span>分  合格分:<span id="sPassScore" class="orange"></span>分 <%--计时:<span id="sTimer" class="orange"></span>--%></h3>
    </div>
    <hr />
    <div class="questionContainer" id="questionContainer">
    </div>
    <div class="bottom_Toolbar">
        <input type="button" value="交卷" id="btnSubmit" class="button_s" /><input type="button" value="取消" id="btnClose" class="button_n" />
    </div>
</body>
</html>
