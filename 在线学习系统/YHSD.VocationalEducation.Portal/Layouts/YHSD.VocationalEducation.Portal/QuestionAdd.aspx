<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuestionAdd.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.QuestionAdd" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:content id="PageHead" contentplaceholderid="PlaceHolderAdditionalPageHead" runat="server">
    <link href="../../../_layouts/15/YHSD.VocationalEducation.Portal/js/Jquery.easyui/themes/metro/easyui.css" rel="stylesheet" />
    <link href="css/edit.css" rel="stylesheet" />
    <style type="text/css">
        #optionList div, #optionListSingle div {
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
            border-width: 0px;
        }

        #optionListSingle, #optionList {
            margin-left: 10px;
            padding: 5px 0;
            width: 440px;
        }

        .tabs li a.tabs-inner, .tabs li.tabs-selected a.tabs-inner {
            background: none;
        }

        .easyui-tabs {
            margin: 16px;
        }
    </style>
    <script type="text/javascript">
        var editId = "<%=Request["EditID"]%>";
        var storeId;
        $(function () {
            if (editId) {
                try {
                    $("#QuestionTabs").tabs("hideHeader");
                    var model;
                    AjaxRequest("Handler/QuestionStoreHandler.aspx", { CMD: "GetQuestionModel", ID: editId }, function (returnVal) {
                        var cfName = returnVal.ClassificationName;
                        var questionModel = JSON.parse(returnVal.Model);
                        var cfModel = JSON.parse(returnVal.CFModel);
                        storeId = questionModel.ID;
                        switch (returnVal.Type) {
                            case Enum_QuestionSingle:
                                LoadSingleInfo(questionModel, cfModel);
                                break;
                            case Enum_QuestionMulti:
                                LoadMultiInfo(questionModel, cfModel);
                                break;
                            case Enum_QuestionJudge:
                                LoadJudgeInfo(questionModel, cfModel);
                                break;
                            case Enum_QuestionEssay:
                                LoadEssayInfo(questionModel, cfModel);
                                break;
                            default:
                                throw new Error("未知题型:" + returnVal.Type);
                                break;
                        }
                        $("#QuestionTabs").tabs("select", returnVal.Type);
                    });
                } catch (e) {
                    LayerAlert("加载试题信息失败!");
                    console.error(e.message);
                }
            }
        })
    </script>
    <%--多选题--%>
    <script type="text/javascript">
        var editIndex;
        var ChooseArr = new Array();
        var AnswerArr = new Array();

        function sltAnswer(sa_index) {
            console.log(sa_index);
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
        function LoadMultiInfo(model, cfModel) {
            $("#txtChooseTitle").val(model.Title);
            $("#txtCfName").val(cfModel.Title);
            $("#txtCfName").css({ color: "#444" });
            $("#CfId").val(cfModel.ID);
            $("#CfIdSingle").val(cfModel.ID);
            var temp_answerArr = model.Correct.split(',');
            var i = 0;
            for (var item in model) {
                if (item.indexOf("Option") > -1 && model[item] != "") {
                    ChooseArr.push(model[item]);
                    if (temp_answerArr.indexOf(i) > -1)
                        sltAnswer(i);
                    i += 1;
                }
            }

            BuildOption();
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
                var optionHtml = "<div><a href='javascript:void(0)' onclick='sltAnswer(" + i + ")'>正确答案</a>&nbsp<a href='javascript:void(0)' onclick='editOption(" + i + ")'>编辑</a>&nbsp<a href='javascript:void(0)' onclick='deleteOption(" + i + ")'>删除</a>&nbsp<span class='content' onclick='sltAnswer(" + i + ")'>" + IntegerToLetter(i) + "</span>.<span class='content' onclick='sltAnswer(" + i + ")'>" + ChooseArr[i] + "</span>&nbsp</div>";
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

        function CloseLayer() {
            layer.closeAll();
        }

        function ShowLayer() {
            OL_ShowLayer(1, '添加选项', 400, 230, $('#addOption'), function () {
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

        $(function () {

            //tabs.set("nav", "menu_con");
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
                CloseLayer();
            });

            $("#txtCfName").click(function () {
                $("#txtCfName").blur();
                OL_ShowLayer(2, "选择分类", 360, 430, "CurriculumInfoSelect.aspx", function (data) {
                    if (data) {
                        $("#txtCfName").css({ color: "#444" });
                        $("#CfId").val(data.Id);
                        $("#txtCfName").val(data.Name);
                    }
                });
            });

            $("#btnSaveMulti").click(function () {
                if ($("#txtChooseTitle").val().trim() == "") {
                    LayerTips("请输入标题!", "txtChooseTitle");
                    return;
                }
                var CfId = $("#CfId").val();
                if (CfId == null || CfId == '') {
                    LayerTips("请选择分类!", "txtCfName");
                    return;
                }
                if (ChooseArr == null || ChooseArr.length == 0) {
                    LayerTips("请添加选项!", "btnShowOption");
                    return;
                }
                if (AnswerArr == null || AnswerArr.length == 0) {
                    LayerTips("请选择答案!", "optionList");
                    return;
                }

                if (ChooseArr == null || ChooseArr.length == 0) {
                    LayerAlert("请添加选项!");
                    return;
                }
                var postData = { CMD: "AddQuestion" };
                var model = GetChooseModel();
                if (editId) {
                    postData.CMD = "EditQuestion";
                    postData.EditId = editId;
                    model.ID = storeId;
                }
                if (model != null) {
                    postData.Entity = JSON.stringify(model);
                }
                postData.IDs = JSON.stringify(ChooseArr);

                postData.ClassificationID = $("#CfId").val();
                postData.QuestionType = Enum_QuestionMulti;
                AjaxRequest("Handler/QuestionStoreHandler.aspx", postData, function (returnVal) {
                    LayerAlert('操作成功!', function () {
                        window.history.back();
                    });
                });
            });
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
        function LoadSingleInfo(model, cfModel) {
            $("#txtChooseTitleSingle").val(model.Title);
            $("#txtCfSingleName").val(cfModel.Title);
            $("#txtCfSingleName").css({ color: "#444" });
            $("#CfIdSingle").val(cfModel.ID);

            for (var item in model) {
                if (item.indexOf("Option") > -1 && model[item] != "") {
                    ChooseArrSingle.push(model[item]);
                }
            }

            BuildOptionSingle();
            sltAnswerSingle(model.Correct);
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
                var optionHtml = "<div><a href='javascript:void(0)' onclick='sltAnswerSingle(" + i + ")'>正确答案</a>&nbsp<a href='javascript:void(0)' onclick='editOptionSingle(" + i + ")'>编辑</a>&nbsp<a href='javascript:void(0)' onclick='deleteOptionSingle(" + i + ")'>删除</a>&nbsp<span class='content' onclick='sltAnswerSingle(" + i + ")'>" + IntegerToLetter(i) + "</span>.<span class='content' onclick='sltAnswerSingle(" + i + ")'>" + ChooseArrSingle[i] + "</span>&nbsp</div>";
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

        function CloseLayer() {
            layer.closeAll();
        }

        function ShowLayerSingle() {
            OL_ShowLayer(1, '添加选项', 400, 230, $('#addOptionSingle'), function () {
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
                CloseLayer();
            });

            $("#txtCfSingleName").click(function () {
                $("#txtCfSingleName").blur();
                OL_ShowLayer(2, "选择分类", 360, 430, "CurriculumInfoSelect.aspx", function (data) {
                    if (data) {
                        $("#CfIdSingle").val(data.Id);
                        $("#txtCfSingleName").css({ color: "#444" });
                        $("#txtCfSingleName").val(data.Name);
                    }
                });
            });

            $("#btnSaveSingle").click(function () {
                if ($("#txtChooseTitleSingle").val().trim() == "") {
                    LayerTips("请输入标题!", "txtChooseTitleSingle");
                    return;
                }
                var CfId = $("#CfIdSingle").val();
                if (CfId == null || CfId == '') {
                    LayerTips("请选择分类!", "txtCfSingleName");
                    return;
                }
                if (ChooseArrSingle == null || ChooseArrSingle.length == 0) {
                    LayerTips("请添加选项!", "btnShowOptionSingle");
                    return;
                }
                if (AnswerArrSingle == null || AnswerArrSingle.length == 0) {
                    LayerTips("请选择答案!", "optionListSingle");
                    return;
                }
                var postData = { CMD: "AddQuestion" };
                var model = GetChooseModelSingle();
                if (editId) {
                    postData.CMD = "EditQuestion";
                    postData.EditId = editId;
                    model.ID = storeId;
                }
                if (model != null) {
                    postData.Entity = JSON.stringify(model);
                }
                postData.IDs = JSON.stringify(ChooseArrSingle);
                postData.ClassificationID = $("#CfIdSingle").val();
                postData.QuestionType = Enum_QuestionSingle;
                AjaxRequest("Handler/QuestionStoreHandler.aspx", postData, function (returnVal) {
                    LayerAlert('操作成功!', function () {
                        window.history.back();
                    });
                })
            });
        });
    </script>
    <%--简答题--%>
    <script type="text/javascript">
        function LoadEssayInfo(model, cfModel) {
            $("#txtEssayTitle").val(model.Title);
            $("#txtCfEssayName").val(cfModel.Title);
            $("#txtCfEssayName").css({ color: "#444" });
            $("#CfIdEssay").val(cfModel.ID);
            $("#txtEssayAnswer").val(model.Correct);
        }
        $(function () {
            $("#txtCfEssayName").click(function () {
                $("#txtCfEssayName").blur();
                OL_ShowLayer(2, "选择分类", 360, 430, "CurriculumInfoSelect.aspx", function (data) {
                    if (data) {
                        $("#CfIdEssay").val(data.Id);
                        $("#txtCfEssayName").css({ color: "#444" });
                        $("#txtCfEssayName").val(data.Name);
                    }
                });
            });
            $("#btnSaveEssay").click(function () {
                var title = $("#txtEssayTitle").val().trim();
                var CfId = $("#CfIdEssay").val();
                var correct = $("#txtEssayAnswer").val().trim();
                if (!title) {
                    LayerTips("请输入标题!", "txtEssayTitle");
                    return;
                }
                if (CfId == null || CfId == '') {
                    LayerTips("请选择分类!", "txtCfEssayName");
                    return;
                }
                if (correct == "") {
                    LayerTips("请输入答案!", "txtEssayAnswer");
                    return;
                }
                if (correct.length >= 500) {
                    LayerTips("答案最多支持500个字符!", "txtEssayAnswer");
                    return;
                }
                var postData = { CMD: "AddQuestionForEssay" };
                var model = { Title: title, Correct: correct };
                if (editId) {
                    postData.CMD = "EditQuestionForEssay";
                    postData.EditId = editId;
                    model.ID = storeId;
                }
                if (model != null) {
                    postData.Entity = JSON.stringify(model);
                }
                postData.ClassificationID = CfId;
                postData.QuestionType = Enum_QuestionEssay;
                AjaxRequest("Handler/QuestionStoreHandler.aspx", postData, function (returnVal) {
                    LayerAlert('操作成功!', function () {
                        window.history.back();
                    });
                })
            });
        })
    </script>
    <%--判断题--%>
    <script type="text/javascript">
        function LoadJudgeInfo(model, cfModel) {
            $("#txtJudgeTitle").val(model.Title);
            $("#txtCfJudgeName").val(cfModel.Title);
            $("#txtCfJudgeName").css({ color: "#444" });
            $("#CfIdJudge").val(cfModel.ID);
            $(":radio[name='judgeOption'][value='" + model.Correct + "']").attr("checked", "checked");
        }
        $(function () {
            $("#txtCfJudgeName").click(function () {
                $("#txtCfJudgeName").blur();
                OL_ShowLayer(2, "选择分类", 360, 430, "CurriculumInfoSelect.aspx", function (data) {
                    if (data) {
                        $("#CfIdJudge").val(data.Id);
                        $("#txtCfJudgeName").css({ color: "#444" });
                        $("#txtCfJudgeName").val(data.Name);
                    }
                });
            });
            $("#btnSaveJudge").click(function () {
                var title = $("#txtJudgeTitle").val();
                var CfId = $("#CfIdJudge").val();
                var correct = $(":radio[name='judgeOption']:checked").val();
                if (!title) {
                    LayerTips("请输入标题!", "txtJudgeTitle");
                    return;
                }
                if (CfId == null || CfId == '') {
                    LayerTips("请选择分类!", "txtCfJudgeName");
                    return;
                }
                if (!correct) {
                    LayerTips("请选择答案!", "tdJudgeCorrect");
                    return;
                }
                var postData = { CMD: "AddQuestion" };
                var model = { Title: title, Correct: correct };
                if (editId) {
                    postData.CMD = "EditQuestion";
                    postData.EditId = editId;
                    model.ID = storeId;
                }
                if (model != null) {
                    postData.Entity = JSON.stringify(model);
                }

                var JudgeArr = new Array();
                JudgeArr.push("正确");
                JudgeArr.push("错误");
                postData.IDs = JSON.stringify(JudgeArr);
                postData.ClassificationID = CfId;
                postData.QuestionType = Enum_QuestionJudge;
                AjaxRequest("Handler/QuestionStoreHandler.aspx", postData, function (returnVal) {
                    LayerAlert('操作成功!', function () {
                        window.history.back();
                    });
                })
            });
        })
    </script>
</asp:content>

<asp:content id="Main" contentplaceholderid="PlaceHolderMain" runat="server">
    <div id="addOption" style="padding: 10px; display: none">
        <span style="margin-left:10px;">内容</span><br />
        <textarea rows="10" cols="50" id="txtChooseContent" class="inputPart" style="height: 80px; width: 350px; overflow-x: hidden;margin:10px;border:1px solid silver;"></textarea>
        <input type="button" value="确定" id="btnAddOption" onclick="" class="button_s" /><input type="button" value="取消" id="btnCancel" onclick="                CloseLayer()" class="button_n" />
    </div>
    <div id="addOptionSingle" style="padding: 10px; display: none">
        <span style="margin-left:10px;">内容</span><br />
        <textarea rows="10" cols="50" id="txtChooseContentSingle" class="inputPart" style="height: 80px; width: 350px; overflow-x: hidden;margin:10px;border:1px solid silver;"></textarea>
        <input type="button" value="确定" id="btnAddOptionSingle" onclick="" class="button_s" /><input type="button" value="取消" id="btnCancelSingle" onclick="                CloseLayer()" class="button_n" />
    </div>

    <div class="easyui-tabs" id="QuestionTabs" data-options="tabWidth:140" style="width: 582px; height: 450px">
        <div title="单选题" style="padding: 10px">
            <div style="overflow:hidden;">
                <table class="tableEdit MendEdit" style="width: 100%;">
                    <tr>
                        <th>标题&nbsp;<span style="color:Red">*</span></th>
                        <td>
                            <textarea rows="20" cols="50" id="txtChooseTitleSingle" class="inputPart" style="height: 60px; width: 400px; overflow-x: hidden"></textarea>
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
                <input type="button" value="保存" id="btnSaveSingle" class="button_s" /><input type="button" value="返回" onclick="javascript: window.history.back();" class="button_n" />
            </div>
        </div>
        <div title="多选题" style="padding: 10px">
            <div>
                <table class="tableEdit MendEdit" style="width: 100%;">
                    <tr>
                        <th>标题&nbsp;<span style="color:Red">*</span></th>
                        <td>
                            <textarea rows="20" cols="50" id="txtChooseTitle" class="inputPart" style="height: 60px; width: 400px; overflow-x: hidden"></textarea>
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
                <input type="button" value="保存" id="btnSaveMulti" class="button_s" /><input type="button" value="返回" onclick="javascript: window.history.back();" class="button_n" />
            </div>
        </div>
        <div title="判断题" style="padding: 10px">
            <div>
                <table class="tableEdit MendEdit" style="width: 100%;">
                    <tr>
                        <th>标题&nbsp;<span style="color:Red">*</span></th>
                        <td>
                            <textarea rows="10" cols="50" id="txtJudgeTitle" class="inputPart" style="height: 60px; width: 400px; overflow-x: hidden"></textarea>
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
                            <input type="radio" value="1" name="judgeOption" />正确&nbsp&nbsp
                            <input type="radio" value="0" name="judgeOption" />错误
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <input type="button" value="保存" id="btnSaveJudge" class="button_s" /><input type="button" value="返回" onclick="javascript: window.history.back();" class="button_n" />
            </div>
        </div>
        <div title="简答题" style="padding: 10px">
            <div>
                <table class="tableEdit MendEdit" style="width: 100%;">
                    <tr>
                        <th>标题&nbsp;<span style="color:Red">*</span></th>
                        <td>
                            <textarea rows="10" cols="50" id="txtEssayTitle" class="inputPart" style="height: 60px; width: 400px; overflow-x: hidden"></textarea>
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
                            <textarea rows="10" cols="50" id="txtEssayAnswer" class="inputPart" style="height: 100px; width: 400px; overflow-x: hidden"></textarea></td>
                    </tr>
                </table>
            </div>
            <div>
                <input type="button" value="保存" id="btnSaveEssay" class="button_s" /><input type="button" value="返回" onclick="javascript: window.history.back();" class="button_n" />
            </div>
        </div>
    </div>
</asp:content>

<asp:content id="PageTitle" contentplaceholderid="PlaceHolderPageTitle" runat="server">
    添加题目
</asp:content>

<asp:content id="PageTitleInTitleArea" contentplaceholderid="PlaceHolderPageTitleInTitleArea" runat="server">
    添加题目
</asp:content>
