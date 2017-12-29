<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormPapers.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.FormPapers" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <link href="js/Jquery.easyui/themes/metro/easyui.css" rel="stylesheet" />
    <link href="css/edit.css" rel="stylesheet" />
    <style type="text/css">
            .questionContainer {
                width:650px;
            }
        .questionContainer .questionItem {
            border-top: 1px dashed silver;
            margin: 5px;
            padding: 5px;
            min-height: 50px;
            min-width: 400px;
        }

            .questionContainer .questionItem .qType {
                color: blue;
            }
            .questionContainer .content{
                min-height:25px;
                line-height:25px;
            }
        .questionContainer .buttomTool {
            height:25px;
                line-height:25px;
        }

            .questionContainer .buttomTool .txtScore {
                width: 60px;
            }

        .questionContainer .group {
            width: 100%;
            margin: 10px 0;
            border: 1px solid silver;
        }
        .questionContainer .group .title{
                font-size: 14px;
                font-family: "微软雅黑","宋体";
                border-bottom: 1px solid silver;
                height:30px;
                width: 100%;
        }
            .questionContainer .group .groupName {
                color: #565656;
                font-weight: bold;
                display:block;
                float:left;
                line-height:30px;
                margin-left:10px;
            }
            .questionContainer .group .groupTool {
                display:block;
                float:right;
                line-height:27px;
                margin-right:10px;
            }            
            .questionContainer .group .questionDiv {
                min-height: 53px;
                font-size: 13px;
            }

        .mainDiv {
            width: 750px;
            height: auto;
        }

            .mainDiv table {
                width: 100%;
                border: 0px solid red;
            }

                .mainDiv table .td1 {
                    width: 70px;
                    text-align: left;
                }

                .mainDiv table .td2 {
                    width: auto;
                    text-align: left;
                }

        .questionContainer a:link {
            color: black;
            font-weight: normal;
        }

        .questionContainer a:visited {
            color: black;
        }
        .questionContainer .buttomTool div{
            float:right;
            margin-top:4px;
        }
    </style>
    
    <script type="text/javascript">
        var QuestionArr = new Array();
        var EditGroupText;
        var editId = "<%=Request["EditID"]%>";
        var PaperInfo;
        if (editId) {//编辑模式
            LoadEditInfo();
        }
        function LoadEditInfo() {
            var postData = { CMD: "GetPaperInfo", EditId: editId };
            AjaxRequest("Handler/PapersMgrHandler.aspx", postData, function (returnVal) {
                var loadIndex = layer.load(2);
                PaperInfo = JSON.parse(returnVal.PaperInfo);
                var countt = PaperInfo.QuestionCount - returnVal.QuestionCount;
                if (countt > 0) {
                    LayerAlert("自上次新建试卷后,已有" + countt + "个试题被删除,请重新添加!");
                }
                $("#txtPaperName").val(PaperInfo.Title);
                $("#txtTotalScore").val(PaperInfo.TotalScore);
                $("#txtPassScore").val(PaperInfo.PassScore);
                $(JSON.parse(returnVal.GroupInfo)).each(function () {
                    AddGroup(this.GroupTile, JSON.parse(this.Questions));
                });
                layer.close(loadIndex);
            })
        }
        function RefreshCount() {
            $("#questionCount").text($("#questionContainer div[qid]").length);
        }
        function EditGroup(obj) {
            var groupItem = $(obj.parentNode.parentNode.parentNode);
            EditGroupText = $(groupItem).find(".groupName");
            $("#txtGroupName").val($(EditGroupText).text());
            OL_ShowLayer(1, '添加选项', 335, 170, $('#addGroupDialog'), function () {
                $("#txtGroupName").val("");
                EditGroupText = undefined;
            });
            $("#txtGroupName").focus();
        }
        function UpGroup(obj) {
            var groupItem = $(obj.parentNode.parentNode.parentNode);
            $(groupItem).insertBefore($(groupItem).prev());
        }
        function ButtomGroup(obj) {
            var groupItem = $(obj.parentNode.parentNode.parentNode);
            $(groupItem).insertAfter($(groupItem).next());
        }
        function DelGroup(obj) {
            if ($(obj.parentNode.parentNode.parentNode).find("div[qid]").length > 0) {
                var index = LayerConfirm("此分组下包括问题，是否删除？", function () {
                    $(obj.parentNode.parentNode.parentNode).remove();
                    OL_CloseLayer(index);
                    RefreshCount();
                })
            }
            else {
                $(obj.parentNode.parentNode.parentNode).remove();
                RefreshCount();
            }
        }
        function GetQuestionByGroup() {

        }
        function delQuestion(id) {
            $("#questionContainer div[qid='" + id + "']").remove();
            RefreshCount();
        }
        function Up(obj) {
            var questionItem = obj.parentNode.parentNode;
            $(questionItem).insertBefore($(questionItem).prev());
        }
        function Buttom(obj) {
            var questionItem = obj.parentNode.parentNode;
            $(questionItem).insertAfter($(questionItem).next());
        }
        function AddQuestion(obj) {
            var questionItem = obj.parentNode.parentNode.parentNode;
            OL_ShowLayer(2, "选择问题", 800, 660, "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/QuestionSelect.aspx", function (returnVal) {
                if (returnVal && returnVal.qArr.length > 0) {
                    for (var i = 0; i < returnVal.qArr.length; i++) {

                        var questionEntity = { ID: returnVal.qArr[i].ID, Title: returnVal.qArr[i].Title, Type: returnVal.qArr[i].QuestionType };
                        if ($("#questionContainer div[qid='" + questionEntity.ID + "']").length == 0) {//如果问题已经添加则跳过
                            var html = "<div class='questionItem' qid='" + questionEntity.ID + "'>\
                                            <div class='content'>\
                                                <span class='qType'> (" + questionEntity.Type + ") </span><span>" + questionEntity.Title + "</span>\
                                            </div>\
                                            <div class='buttomTool'>\
                                                <span>分值&nbsp</span><input class='easyui-numberbox txtScore' data-options='min:1,width:40'>\
                                                <div class='DelImg' onclick='delQuestion(\"" + questionEntity.ID + "\");' title='删除'></div>\
                                                <div class='TopImg' onclick='Up(this)' title='上移'></div>\
                                                <div class='BottomImg' onclick='Buttom(this)' title='下移'></div>\
                                            </div>\
                                        </div>";
                            $(questionItem).find(".questionDiv").append(html);
                            $.parser.parse($("#questionContainer"));
                            QuestionArr.push(questionEntity);
                        }
                    }
                    RefreshCount();
                    $("#txtTotalScore").val("点击计分");
                }
            });
        }
        function AddGroup(name, questions) {
            var html = "<div class='group'>\
                    <div class='title'>\
                        <div class='groupName'>" + name + "</div>\
                        <div class='groupTool'><a href='javascript:;' class='easyui-linkbutton' data-options='plain:true' onclick='EditGroup(this)'>修改分组</a>\
                        <a href='javascript:;' class='easyui-linkbutton' data-options='plain:true' onclick='DelGroup(this)'>删除分组</a>\
                        <a href='javascript:;' class='easyui-linkbutton' data-options='plain:true' onclick='AddQuestion(this)'>添加问题</a>\
                        <a href='javascript:;' class='easyui-linkbutton' data-options='plain:true' onclick='UpGroup(this)'>上移</a>\
                        <a href='javascript:;' class='easyui-linkbutton' data-options='plain:true' onclick='ButtomGroup(this)'>下移</a></div>\
                    </div>\
                    <div class='questionDiv'>";
            if (questions) {
                $(questions).each(function () {
                    html += "<div class='questionItem' qid='" + this.OldStoreID + "'>\
                                            <div class='content'>\
                                                <span class='qType'> (" + this.QuestionType + ") </span><span>" + this.QuestionTitle + "</span>\
                                            </div>\
                                            <div class='buttomTool'>\
                                                <span>分值&nbsp</span><input class='easyui-numberbox txtScore' value='" + this.Score + "' data-options='min:1,width:40'>\
                                                <div class='DelImg' onclick='delQuestion(\"" + this.OldStoreID + "\");' title='删除'></div>\
                                                <div class='TopImg' onclick='Up(this)' title='上移'></div>\
                                                <div class='BottomImg' onclick='Buttom(this)' title='下移'></div>\
                                            </div>\
                                        </div>";
                });
            }
            html += "</div>\
                </div>"
            $("#questionContainer").append(html);
            $.parser.parse($("#questionContainer"));
            RefreshCount();
        }
        $(function () {
            $("#txtGroupName").keypress(function () {
                EnterEvent("btnEnterGroup");
            })
            $("#btnSltPaper").click(function () {
                OL_ShowLayer(2, "选择试卷", 850, 660, "CommonSelect/PaperSelect.aspx", function (returnVal) {
                    if (returnVal) {
                        var paperId = returnVal.ID;
                        var postData = { CMD: "GetPaperInfo", EditId: paperId };
                        var count = $("#questionContainer .group").length;
                        if (count > 0) {
                            LayerConfirm("即将覆盖已选的分组及试题信息,确定?", function () {
                                $("#questionContainer").empty();
                                AjaxRequest("Handler/PapersMgrHandler.aspx", postData, function (returnVal) {
                                    var loadIndex = layer.load(2);
                                    var PaperInfo = JSON.parse(returnVal.PaperInfo);
                                    $("#txtTotalScore").val(PaperInfo.TotalScore);
                                    $("#txtPassScore").val(PaperInfo.PassScore);
                                    $(JSON.parse(returnVal.GroupInfo)).each(function () {
                                        AddGroup(this.GroupTile, JSON.parse(this.Questions));
                                    });
                                    layer.closeAll();
                                });
                            });
                        }
                        else {
                            AjaxRequest("Handler/PapersMgrHandler.aspx", postData, function (returnVal) {
                                var loadIndex = layer.load(2);
                                var PaperInfo = JSON.parse(returnVal.PaperInfo);
                                $("#txtTotalScore").val(PaperInfo.TotalScore);
                                $("#txtPassScore").val(PaperInfo.PassScore);
                                $(JSON.parse(returnVal.GroupInfo)).each(function () {
                                    AddGroup(this.GroupTile, JSON.parse(this.Questions));
                                });
                                layer.close(loadIndex);
                            });
                        }
                    }
                });
            })
            //弹出添加分组的页面
            $("#btnAddGroup").click(function () {

                OL_ShowLayer(1, '添加分组', 335, 170, $('#addGroupDialog'), function () {
                    $("#txtGroupName").val("");
                    EditGroupText = undefined;
                });
                $("#txtGroupName").focus();
            })
            //添加分组
            $("#btnEnterGroup").click(function () {
                var name = $("#txtGroupName").val().trim();
                if (name == "") {
                    LayerAlert("请输入分组名称！");
                    return;
                }
                if (EditGroupText != undefined) {
                    $(EditGroupText).text(name);
                    OL_CloseLayer();
                    return;
                }
                AddGroup(name);
                OL_CloseLayer();
            })
            $("#txtTotalScore").click(function () {

                var totalScore = 0;
                $("#questionContainer .group").each(function (j) {
                    $($(this).find("div[qid]")).each(function (k) {//循环分组下的问题
                        var score = $(this).find(":text").val();
                        if (score == "") {
                            LayerTips("请输入分值!", $(this).attr("id"));
                            return false;
                        }
                        totalScore = totalScore + parseInt(score);
                    });
                })
                $("#txtTotalScore").val(totalScore);

            });
            $("#btnSave").click(function () {
                var canSave = true;
                var name = $("#txtPaperName").val().trim();
                var totalScore = $("#txtTotalScore").val().trim();
                var passScore = $("#txtPassScore").val().trim();
                var count = $("#questionCount").text();
                var paperModel = { Title: name, QuestionCount: count, TotalScore: totalScore, PassScore: passScore };
                var pqArr = new Array();//试卷-题库 PaperQuestion
                var qgArr = new Array();//题库-分组 QuestionGroup

                if (name == "") {
                    LayerTips("试卷名称不能为空!", "txtPaperName");
                    return;
                }
                $("#questionContainer .group").each(function (j) {
                    var gtitle = $(this).find(".groupName").text();
                    qgArr.push({ ID: j, GroupTile: gtitle, OrderNum: j + 1 });

                    $($(this).find("div[qid]")).each(function (k) {//循环分组下的问题
                        var qid = $(this).attr("qid");
                        var score = $(this).find(":text").val();
                        if (score == "") {
                            canSave = false;
                            LayerTips("请给所有问题设定分数!", "txtTotalScore");
                            return false;
                        }
                        var orderNum = k + 1;
                        var PQModel = { QuestionID: qid, Score: score, OrderNum: orderNum, GroupID: j };
                        pqArr.push(PQModel);
                    });
                })
                if (totalScore == "点击计分" || totalScore == "") {
                    LayerTips("请给所有问题设定分数!", "txtTotalScore");
                    return;
                }
                if (isNaN(totalScore) && parseInt(totalScore) <= 0) {
                    LayerTips("试卷总分必须为大于0的数字!", "txtTotalScore");
                    return;
                }
                if (passScore == "") {
                    LayerTips("试卷合格分不能为空!", "txtPassScore");
                    return;
                }
                if (isNaN(passScore)) {
                    LayerTips("试卷合格分必须为数字!", "txtPassScore");
                    return;
                }
                if (parseInt(passScore) > parseInt(totalScore)) {
                    LayerTips("合格分不能超过总分!", "txtPassScore");
                    return;
                }
                if (qgArr.length == 0) {
                    LayerAlert("请先添加分组,然后再添加问题!");
                    return;
                }
                if (pqArr.length == 0) {
                    LayerAlert("请添加问题!");
                    return;
                }
                if (!canSave)
                    return;
                var postData;
                if (!editId) {
                    postData = { CMD: "AddPaper", PaperModel: JSON.stringify(paperModel), PQArr: JSON.stringify(pqArr), QGArr: JSON.stringify(qgArr) };
                }
                else {
                    //处于编辑模式
                    paperModel.ID = editId;
                    paperModel.CreateUser = PaperInfo.CreateUser;
                    paperModel.CreateTime = PaperInfo.CreateTime;
                    postData = { CMD: "EditPaper", PaperModel: JSON.stringify(paperModel), PQArr: JSON.stringify(pqArr), QGArr: JSON.stringify(qgArr) };
                }

                AjaxRequest("Handler/PapersMgrHandler.aspx", postData, function (returnVal) {
                    LayerAlert("操作成功!", function () {
                        window.history.back();
                    })
                });
            });
            $("#btnCancel").click(function () {
                window.history.back();
            })
        });
    </script>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div class="mainDiv">
        <table class="tableEdit">
            <tr>
                <th class="td1">试卷名称&nbsp;<span style="color: Red">*</span></th><td class="td2">
                    <input type="text" id="txtPaperName" style="width:400px;" class="inputPart"/></td>
            </tr>
            <tr>
                <th class="td1">总分&nbsp;<span style="color: Red">*</span></th><td>
                    <input type="text" id="txtTotalScore" style="width:400px;" class="inputPart" readonly="readonly" value="点击计分"/></td>
            </tr>
            <tr>
                <th class="td1">合格分&nbsp;<span style="color: Red">*</span></th><td>
                    <input type="text" id="txtPassScore"  style="width:400px;" class="inputPart"/></td>
            </tr>
            <tr>
                <th class="td1">题目&nbsp;<span style="color: Red">*</span></th><td> 已有<span id="questionCount">0</span>道题
                    <input type="button" id="btnAddGroup" value="添加分组" class="button_s" /></td>
            </tr>
            <tr>
                <td colspan="2" style="height:60px;">
                    <input type="button" value="保存" class="button_s" id="btnSave" style="margin-left:0px;"/>
                    <input type="button" value="选择试卷" id="btnSltPaper" class="button_n" />
                    <input type="button" value="取消" class="button_n" id="btnCancel" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div class='questionContainer' id='questionContainer'>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div class="hiddenDiv">
        <div id="addGroupDialog" style="padding:10px; display:none;text-align:center;"><br />
            <span>名称&nbsp;<span style="color: Red">*</span>
            <input type="text" class="input_part" id="txtGroupName" style="width:240px;"/></span><br /><br />
            <input type="button" value="确定" id="btnEnterGroup" onclick="" class="button_s" /><input type="button" value="取消" id="btnCancelGroup" onclick="                    OL_CloseLayer()" class="button_n" />
        </div>
    </div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    应用程序页
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    我的应用程序页
</asp:Content>
