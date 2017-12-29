<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuestionAddForSelect.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.QuestionAddForSelect" %>
<html>
<head>
    <title>QuestionAdd For Selection</title>
    <link href="css/edit.css" rel="stylesheet" />

    <style type="text/css">
        #optionList div {
            width: 450px;
            word-break: break-all;
            margin: 5px 0px;
        }

        .answerItem {
            cursor: pointer;
            display: block;
            margin-right: 5px;
            float: left;
        }

        #optionList .content {
            cursor: pointer;
        }

        .tabs-panels, .tabs-header {
            border-width: 0px !important;
        }

        #optionListSingle, #optionList {
            margin-left: 10px;
            padding: 5px 0;
        }
        body{
            margin:0;
            font-size:12px;
            font-family:"微软雅黑","宋体";
        }
        .tableEdit .MendEdit{
            text-decoration:none;
        }
    </style>
    <link href="css/type.css" rel="stylesheet" />
    <link href="js/Jquery.easyui/themes/icon.css" rel="stylesheet" />
    <link href="js/Jquery.easyui/themes/metro/easyui.css" rel="stylesheet" />
    <script type="text/javascript" src="js/layer/jquery.min.js"></script>
    <script type="text/javascript" src="js/Jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="js/layer/layer.js"></script>
    <script type="text/javascript" src="js/FormatUtil.js"></script>
    <script type="text/javascript" src="js/layer/OpenLayer.js"></script>
    <script type="text/javascript">
        function CancelAdd() {
            parent.OL_CloseLayer();
        }
        function CloseLayer() {
            layer.closeAll();
        }
        function TabBack() {
            parent.SltTab(0);
        }
    </script>
    <%--多选题--%>
    <script type="text/javascript">
        var editIndex;
        var ChooseArr = new Array();
        var AnswerArr = new Array();

        function sltAnswer(sa_index) {
            var find_index = AnswerArr.indexOf(sa_index);
            if (find_index != -1) {
                AnswerArr.remove(find_index);
            }
            else {
                AnswerArr.push(sa_index);
            }
            AnswerArr.sort();
            BuildAnswer();
        }

        function BuildAnswer() {
            $("#answerList").empty();
            for (var i = 0; i < AnswerArr.length; i++) {
                var answer = "<span class='answerItem'>" + IntegerToLetter(AnswerArr[i]) + "</span>";
                $(answer).appendTo("#answerList").show();
            }
        }

        function BuildOption() {
            $("#optionList").empty();
            for (var i = 0; i < ChooseArr.length; i++) {
                var optionHtml = "<div><a href='javascript:void(0)' onclick='sltAnswer(" + i + ")'><span>正确答案<span></a>&nbsp<a href='javascript:void(0)' onclick='editOption(" + i + ")'><span>编辑<span></a>&nbsp<a href='javascript:void(0)' onclick='deleteOption(" + i + ")'><span>删除<span></a>&nbsp<span class='content' onclick='sltAnswer(" + i + ")'>" + IntegerToLetter(i) + "</span>.<span class='content' onclick='sltAnswer(" + i + ")'>" + ChooseArr[i] + "</span>&nbsp</div>";
                $(optionHtml).appendTo("#optionList");
            }
        }

        function deleteOption(del_index) {
            ChooseArr.remove(del_index);
            BuildOption();
        }

        function editOption(edit_index) {
            $("#txtChooseContent").text(ChooseArr[edit_index]);
            editIndex = edit_index;
            ShowLayer();
        }

        function IntegerToLetter(n) {
            return String.fromCharCode(65 + parseInt(n));
        }

        function ShowLayer() {
            OL_ShowNoShadeLayer(1, '添加选项', 400, 230, $('#addOption'), function () {
                $("#txtChooseContent").val("");
            });
            $("#txtChooseContent").focus();
        }

        function GetChooseModel() {
            var gcm_Model = {};
            gcm_Model.Title = $("#txtChooseTitle").val();
            gcm_Model.Correct = AnswerArr.join();
            return gcm_Model;
        }
        function SaveMulti(check) {

            if (ChooseArr == null || ChooseArr.length == 0) {
                parent.LayerAlert("请添加选项!");
                return;
            }
            var CfId = $("#CfId").val();
            if (CfId == null || CfId == '') {
                parent.LayerAlert("请选择分类!");
                return;
            }
            if (AnswerArr == null || AnswerArr.length == 0) {
                parent.LayerAlert("请添加答案!");
                return;
            }
            var postData = { CMD: "AddQuestion" };
            var model = GetChooseModel();
            if (model != null) {
                postData.Entity = JSON.stringify(model);
            }
            postData.IDs = JSON.stringify(ChooseArr);

            postData.ClassificationID = $("#CfId").val();
            postData.QuestionType = "多选题";
            AjaxRequest("Handler/QuestionStoreHandler.aspx", postData, function (returnVal) {
                if (ErrorJudge(returnVal)) {
                    parent.LayerAlert("添加失败:" + ErrorJudge(returnVal));
                    return;
                }
                if (check && returnVal.ID) {
                    parent.AddCheck(returnVal.ID);
                }
                else {
                    TabBack();
                }
                parent.ReLoadLayer();
            })
        }
        $(function () {
            //显示添加选项的DIV
            $("#btnShowOption").click(function () {
                ShowLayer();
            });
            $("#txtChooseContent").keypress(function () {
                EnterEvent("btnAddOption");
            })
            //添加选项
            $("#btnAddOption").click(function () {
                var content = $("#txtChooseContent").val();
                if ($.trim(content) != "") {
                    if (editIndex != null) {
                        ChooseArr[editIndex] = content;
                        editIndex = null;
                    }
                    else {
                        if (ChooseArr.length < 10) {
                            ChooseArr.push(content);
                        }
                        else {
                            LayerAlert("选项最多为10个！");
                        }
                    }
                    BuildOption();
                }
                layer.closeAll();
            });

            $("#txtCfName").click(function () {
                $("#txtCfName").blur();
                OL_ShowNoShadeLayer(2, "选择分类", 360, 430, "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/CurriculumInfoSelect.aspx", function (data) {
                    if (data) {
                        $("#txtCfName").css({ color: "#444" });
                        $("#CfId").val(data.Id);
                        $("#txtCfName").val(data.Name);
                    }
                });
            });
            //$("#btnSltCf").click(function () {
            //    OL_ShowNoShadeLayer(2, "选择分类", 360, 430, "CurriculumInfoSelect.aspx", function (data) {
            //        if (data) {
            //            $("#CfId").val(data.Id);
            //            $("#CfName").text(data.Name);
            //        }
            //    });
            //});
        });
    </script>
    <%--单选题--%>
    <script type="text/javascript">
        var editIndexSingle;
        var ChooseArrSingle = new Array();
        var AnswerArrSingle = new Array();

        function sltAnswerSingle(sa_index) {
            AnswerArrSingle = [sa_index];
            BuildAnswerSingle();
        }

        function BuildAnswerSingle() {
            $("#answerListSingle").empty();
            for (var i = 0; i < AnswerArrSingle.length; i++) {
                var answer = "<span class='answerItem'>" + IntegerToLetter(AnswerArrSingle[i]) + "</span>";
                $(answer).appendTo("#answerListSingle").show();
            }
        }

        function BuildOptionSingle() {
            $("#optionListSingle").empty();
            for (var i = 0; i < ChooseArrSingle.length; i++) {
                var optionHtml = "<div><a href='javascript:void(0)' onclick='sltAnswerSingle(" + i + ")'><span>正确答案<span></a>&nbsp<a href='javascript:void(0)' onclick='editOptionSingle(" + i + ")'><span>编辑<span></a>&nbsp<a href='javascript:void(0)' onclick='deleteOptionSingle(" + i + ")'><span>删除<span></a>&nbsp<span class='content' onclick='sltAnswerSingle(" + i + ")'>" + IntegerToLetter(i) + "</span>.<span class='content' onclick='sltAnswerSingle(" + i + ")'>" + ChooseArrSingle[i] + "</span>&nbsp</div>";
                $(optionHtml).appendTo("#optionListSingle");
            }
        }

        function deleteOptionSingle(del_index) {
            ChooseArrSingle.remove(del_index);
            BuildOptionSingle();
        }

        function editOptionSingle(edit_index) {
            $("#txtChooseContentSingle").text(ChooseArrSingle[edit_index]);
            editIndexSingle = edit_index;
            ShowLayerSingle();
        }

        function IntegerToLetter(n) {
            return String.fromCharCode(65 + parseInt(n));
        }

        function ShowLayerSingle() {
            OL_ShowNoShadeLayer(1, '添加选项', 400, 230, $('#addOptionSingle'), function () {
                $("#txtChooseContentSingle").val("");
            });
            $("#txtChooseContentSingle").focus();
        }

        function GetChooseModelSingle() {
            var gcm_Model = {};
            gcm_Model.Title = $("#txtChooseTitleSingle").val();
            gcm_Model.Correct = AnswerArrSingle.join();
            return gcm_Model;
        }
        function SaveSingle(check) {
            if (ChooseArrSingle == null || ChooseArrSingle.length == 0) {
                parent.LayerAlert("请添加选项!");
                return;
            }
            if (AnswerArrSingle == null || AnswerArrSingle.length == 0) {
                parent.LayerAlert("请添加答案!");
                return;
            }
            var CfId = $("#CfIdSingle").val();
            if (CfId == null || CfId == '') {
                parent.LayerAlert("请选择分类!");
                return;
            }
            var postData = { CMD: "AddQuestion" };
            var model = GetChooseModelSingle();
            if (model != null) {
                postData.Entity = JSON.stringify(model);
            }
            postData.IDs = JSON.stringify(ChooseArrSingle);
            postData.ClassificationID = $("#CfIdSingle").val();
            postData.QuestionType = "单选题";
            AjaxRequest("Handler/QuestionStoreHandler.aspx", postData, function (returnVal) {
                if (ErrorJudge(returnVal)) {
                    parent.LayerAlert("添加失败:" + ErrorJudge(returnVal));
                    return;
                }
                if (check && returnVal.ID) {
                    parent.AddCheck(returnVal.ID);
                }
                else {
                    TabBack();
                }
                parent.ReLoadLayer();
            })
        }

        $(function () {
            //显示添加选项的DIV
            $("#btnShowOptionSingle").click(function () {
                ShowLayerSingle();
            });
            $("#txtChooseContentSingle").keypress(function () {
                EnterEvent("btnAddOptionSingle");
            })
            //添加选项
            $("#btnAddOptionSingle").click(function () {
                var content = $("#txtChooseContentSingle").val(); if ($.trim(content) != "") {
                    if (editIndexSingle != null) {
                        ChooseArrSingle[editIndexSingle] = content;
                        editIndexSingle = null;
                    }
                    else {
                        if (ChooseArrSingle.length < 10) {
                            ChooseArrSingle.push(content);
                        }
                        else {
                            LayerAlert("选项最多为10个！");
                        }
                    }
                    BuildOptionSingle();
                }
                layer.closeAll();
            });

            $("#txtCfSingleName").click(function () {
                $("#txtCfSingleName").blur();
                OL_ShowNoShadeLayer(2, "选择分类", 360, 430, "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/CurriculumInfoSelect.aspx", function (data) {
                    if (data) {
                        $("#CfIdSingle").val(data.Id);
                        $("#txtCfSingleName").css({ color: "#444" });
                        $("#txtCfSingleName").val(data.Name);
                    }
                });
            });
            //$("#btnSltCfSingle").click(function () {
            //    OL_ShowNoShadeLayer(2, "选择分类", 360, 430, "CurriculumInfoSelect.aspx", function (data) {
            //        if (data) {
            //            $("#CfIdSingle").val(data.Id);
            //            $("#CfNameSingle").text(data.Name);
            //        }
            //    });
            //});
        });
    </script>
    <%--简答题--%>
    <script type="text/javascript">
        function SaveEssay(check) {
            var title = $("#txtEssayTitle").val();
            var CfId = $("#CfIdEssay").val();
            var correct = $("#txtEssayAnswer").val();
            if (!title) {
                parent.LayerAlert("请填入标题!");
                return;
            }
            if (CfId == null || CfId == '') {
                parent.LayerAlert("请选择分类!");
                return;
            }
            var postData = { CMD: "AddQuestionForEssay" };
            var model = { Title: title, Correct: correct };
            if (model != null) {
                postData.Entity = JSON.stringify(model);
            }
            postData.ClassificationID = CfId;
            postData.QuestionType = "简答题";
            AjaxRequest("Handler/QuestionStoreHandler.aspx", postData, function (returnVal) {
                if (ErrorJudge(returnVal)) {
                    parent.LayerAlert("添加失败:" + ErrorJudge(returnVal));
                    return;
                }
                if (check && returnVal.ID) {
                    parent.AddCheck(returnVal.ID);
                }
                else {
                    TabBack();
                }
                parent.ReLoadLayer();
            })
        }
        $(function () {
            $("#txtCfEssayName").click(function () {
                $("#txtCfEssayName").blur();
                OL_ShowNoShadeLayer(2, "选择分类", 360, 430, "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/CurriculumInfoSelect.aspx", function (data) {
                    if (data) {
                        $("#CfIdEssay").val(data.Id);
                        $("#txtCfEssayName").css({ color: "#444" });
                        $("#txtCfEssayName").val(data.Name);
                    }
                });
            });
            //$("#btnSltCfEssay").click(function () {
            //    OL_ShowNoShadeLayer(2, "选择分类", 360, 430, "CurriculumInfoSelect.aspx", function (data) {
            //        if (data) {
            //            $("#CfIdEssay").val(data.Id);
            //            $("#CfNameEssay").text(data.Name);
            //        }
            //    });
            //});
        })
    </script>
    <%--判断题--%>
    <script type="text/javascript">
        function SaveJudge(check) {
            var title = $("#txtJudgeTitle").val();
            var CfId = $("#CfIdJudge").val();
            var correct = $(":radio[name='judgeOption']:checked").val();
            if (!title) {
                parent.LayerAlert("请填入标题!");
                return;
            }
            if (CfId == null || CfId == '') {
                parent.LayerAlert("请选择分类!");
                return;
            }
            if (!correct) {
                parent.LayerAlert("请选择答案!");
                return;
            }
            var postData = { CMD: "AddQuestion" };
            var model = { Title: title, Correct: correct };
            if (model != null) {
                postData.Entity = JSON.stringify(model);
            }
            var JudgeArr = new Array();
            JudgeArr.push("正确");
            JudgeArr.push("错误");
            postData.IDs = JSON.stringify(JudgeArr);
            postData.ClassificationID = CfId;
            postData.QuestionType = "判断题";
            AjaxRequest("Handler/QuestionStoreHandler.aspx", postData, function (returnVal) {
                if (ErrorJudge(returnVal)) {
                    parent.LayerAlert("添加失败:" + ErrorJudge(returnVal));
                    return;
                }
                if (check && returnVal.ID) {
                    parent.AddCheck(returnVal.ID);
                }
                else {
                    TabBack();
                }
                parent.ReLoadLayer();
            })
        }
        $(function () {
            $("#txtCfJudgeName").click(function () {
                $("#txtCfJudgeName").blur();
                OL_ShowNoShadeLayer(2, "选择分类", 360, 430, "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/CurriculumInfoSelect.aspx", function (data) {
                    if (data) {
                        $("#CfIdJudge").val(data.Id);
                        $("#txtCfJudgeName").css({ color: "#444" });
                        $("#txtCfJudgeName").val(data.Name);
                    }
                });
            });
            //$("#btnSltCfJudge").click(function () {
            //    OL_ShowNoShadeLayer(2, "选择分类", 360, 430, "CurriculumInfoSelect.aspx", function (data) {
            //        if (data) {
            //            $("#CfIdJudge").val(data.Id);
            //            $("#CfNameJudge").text(data.Name);
            //        }
            //    });
            //});
        })
    </script>
</head>
<body onload="parent.CloseLoadLayer();">
    <div id="addOption" style="padding: 10px; display: none">
        <span style="margin-left: 10px;">内容</span><br />
        <textarea rows="10" cols="50" id="txtChooseContent" class="input_part" style="height: 100px; width: 350px; overflow-x: hidden; margin: 10px;"></textarea>
        <input type="button" value="确定" id="btnAddOption" onclick="" class="button_s" /><input type="button" value="取消" id="btnCancel" onclick="                CloseLayer()" class="button_s" />
    </div>
    <div id="addOptionSingle" style="padding: 10px; display: none">
        <span style="margin-left: 10px;">内容</span><br />
        <textarea rows="10" cols="50" id="txtChooseContentSingle" class="input_part" style="height: 100px; width: 350px; overflow-x: hidden; margin: 10px;"></textarea>
        <input type="button" value="确定" id="btnAddOptionSingle" onclick="" class="button_s" /><input type="button" value="取消" id="btnCancelSingle" onclick="                CloseLayer()" class="button_s" />
    </div>

    <div class="easyui-tabs" data-options="tabWidth:140" style="width: 582px; height: 450px">
        <div title="单选题" style="padding: 10px">
            <div style="overflow:hidden;">
                <table class="tableEdit MendEdit" style="width: 100%;">
                    <tr>
                        <th>标题&nbsp;<span style="color:Red">*</span></th>
                        <td>
                            <textarea rows="20" cols="50" id="txtChooseTitleSingle" class="inputPart" style="height: 60px; width: 400px;overflow:auto; "></textarea>
                        </td>
                    </tr>
                    <tr>
                        <th>分类&nbsp;<span style="color:Red">*</span></th>
                        <td>
                            <input type="hidden" id="CfIdSingle" />
                            <input type="text" id="txtCfSingleName" class="inputPart" style="cursor:pointer;color:blue; width: 400px;" value="点击选择分类" readonly="readonly"/>
                            <span id="CfNameSingle" style="display:none;">未选择</span>
                            <input type="button" value="选择" id="btnSltCfSingle" class="button_s" style="display:none;" /></td>
                    </tr>
                    <tr>
                        <th>选项&nbsp;<span style="color:Red">*</span></th>
                        <td>
                            <div id="optionListSingle"></div>
                            <input type="button" value="添加选项" id="btnShowOptionSingle" class="button_s" />
                        </td>
                    </tr>
                    <tr>
                        <th>答案&nbsp;<span style="color:Red">*</span></th>
                        <td>
                            <div id="answerListSingle"></div>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <input type="button" value="添加" id="btnSaveSingle" class="button_s" onclick="SaveSingle(false)"/>
                <input type="button" value="添加并勾选" onclick="SaveSingle(true)" class="button_n" />
            </div>
        </div>
        <div title="多选题" style="padding: 10px">
            <div>
                <table class="tableEdit MendEdit" style="width: 100%;">
                    <tr>
                        <th>标题&nbsp;<span style="color:Red">*</span></th>
                        <td>
                            <textarea rows="20" cols="50" id="txtChooseTitle" class="inputPart" style="height: 60px; width: 400px; overflow:auto;"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <th>分类&nbsp;<span style="color:Red">*</span></th>
                        <td>
                            <input type="text" id="txtCfName" class="inputPart" style="cursor:pointer;color:blue; width: 400px;" value="点击选择分类" readonly="readonly"/>
                            <input type="hidden" id="CfId" />
                            <span id="CfName" style="display:none;">未选择</span>
                            <input type="button" style="display:none;" value="选择" id="btnSltCf" class="button_s" /></td>
                    </tr>
                    <tr>
                        <th>选项&nbsp;<span style="color:Red">*</span></th>
                        <td>
                            <div id="optionList"></div>
                            <input type="button" value="添加选项" id="btnShowOption" class="button_s" />
                        </td>
                    </tr>
                    <tr>
                        <th>答案&nbsp;<span style="color:Red">*</span></th>
                        <td>
                            <div id="answerList"></div>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <input type="button" value="添加" id="btnSaveMulti" class="button_s" onclick="SaveMulti(false)"/>
                <input type="button" value="添加并勾选" onclick="SaveMulti(true)" class="button_n" />
            </div>
        </div>
        <div title="判断题" style="padding: 10px">
            <div>
                <table class="tableEdit MendEdit" style="width: 100%;">
                    <tr>
                        <th>标题&nbsp;<span style="color:Red">*</span></th>
                        <td>
                            <textarea rows="10" cols="50" id="txtJudgeTitle" class="inputPart" style="height: 60px; width: 400px; overflow:auto;"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <th>分类&nbsp;<span style="color:Red">*</span></th>
                        <td>
                            <input type="hidden" id="CfIdJudge" />
                            <input type="text" id="txtCfJudgeName" class="inputPart" style="cursor:pointer;color:blue; width: 400px;" value="点击选择分类" readonly="readonly"/>
                            <span id="CfNameJudge" style="display:none">未选择</span><input type="button" value="选择" style="display:none;" id="btnSltCfJudge" class="button_s" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                        <th>答案&nbsp;<span style="color:Red">*</span></th>
                        <td style="line-height:13px;" id="tdJudgeCorrect">
                            <input type="radio" value="1" name="judgeOption" /><span>正确</span>&nbsp&nbsp
                            <input type="radio" value="0" name="judgeOption" /><span>错误</span>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <input type="button" value="添加" id="btnSaveJudge" class="button_s" onclick="SaveJudge(false)"/>
                <input type="button" value="添加并勾选" onclick="SaveJudge(true)" class="button_n" />
            </div>
        </div>
        <div title="简答题" style="padding: 10px">
            <div>
                <table class="tableEdit MendEdit" style="width: 100%;">
                    <tr>
                        <th>标题&nbsp;<span style="color:Red">*</span></th>
                        <td>
                            <textarea rows="10" cols="50" id="txtEssayTitle" class="inputPart" style="height: 60px; width: 400px; overflow:auto;"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <th>分类&nbsp;<span style="color:Red">*</span></th>
                        <td>
                            <input type="text" id="txtCfEssayName" class="inputPart" style="cursor:pointer;color:blue; width: 400px;" value="点击选择分类" readonly="readonly"/>
                            <input type="hidden" id="CfIdEssay" />
                            <span id="CfNameEssay" style="display:none;">未选择</span>
                            <input type="button" value="选择" id="btnSltCfEssay" class="button_s"  style="display:none;"/></td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                        <th>答案&nbsp;<span style="color:Red">*</span></th>
                        <td>
                            <textarea rows="10" cols="50" id="txtEssayAnswer" class="inputPart" style="height: 100px; width: 400px; overflow:auto;"></textarea>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <input type="button" value="添加" id="btnSaveEssay" class="button_s"  onclick="SaveEssay(false)"/>
                <input type="button" value="添加并勾选" onclick="SaveEssay(true)" class="button_n" />
            </div>
        </div>
    </div>
</body>
</html>