<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuestionSelect.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.QuestionSelect" %>

<html>
<head>
    <title>添加人员</title>
    <link href="css/Pager.css" rel="stylesheet" />
    <link href="../../../_layouts/15/YHSD.VocationalEducation.Portal/js/Jquery.easyui/themes/metro/easyui.css" rel="stylesheet" />
    <link href="../../../_layouts/15/YHSD.VocationalEducation.Portal/js/Jquery.easyui/themes/icon.css" rel="stylesheet" />
    <script type="text/javascript" src="../../../_layouts/15/YHSD.VocationalEducation.Portal/js/Jquery.easyui/jquery.min.js"></script>
    <script type="text/javascript" src="../../../_layouts/15/YHSD.VocationalEducation.Portal/js/Jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="js/layer/layer.js"></script>
    <script type="text/javascript" src="js/layer/OpenLayer.js"></script>
    <script type="text/javascript" src="js/Pager.js"></script>
    <script type="text/javascript" src="js/json.js"></script>
    <script type="text/javascript" src="js/FormatUtil.js"></script>
    <link href="css/type.css" rel="stylesheet" />
    <style type="text/css">
        .list_table tbody tr {
            cursor: pointer;
        }
        .nav {
            width: 240px;
            height: 472px;
            border: 1px solid #dedede;
            float: left;
        }

        .workzone {
            float: left;
        }

        .tools {
            margin-left: 5px;
        }

        body {
            margin: 0px;
            overflow: hidden;
            font-size: 12px;
            font-family: "微软雅黑","宋体";
        }
    </style>
    <script type="text/javascript">
        var classificationId;//资源分类ID
        var classificationName;//资源分类名称
        var cfId;
        var questionArr = new Array();
        var newQuestionID;//保存新添加的ID
        var LoadFrameIndex;//加载frame的load index
        function CloseLoadLayer() {
            if (LoadFrameIndex) {
                layer.close(LoadFrameIndex);
                LoadFrameIndex = undefined;
            }
        }
        function ReLoadLayer() {
            ShowData(1);
            LoadFrameIndex = layer.load(2);
            $("#IframeQAdd").prop("src", "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/QuestionAddForSelect.aspx");
        }
        function SltTab(index) {
            $("#tabs").tabs("select", index);
        }
        function SltQuestion(obj) {
            var title = $(obj).parent().parent().find("td")[1].val();
            OL_CloseLayerIframe(title);
        }
        function Check(ckbox, flag) {
            var qModel = JSON.parse($(ckbox).attr("model"));
            var title = qModel.Title;
            var dbid = qModel.ID;
            if (flag == undefined)//如果未传值，则为true
                flag = $(ckbox).prop("checked");
            if (flag) {
                questionArr.push(qModel);
            }
            else {
                questionArr = ArrayRemove(questionArr, "ID", qModel.ID);
            }
            $(ckbox).prop("checked", flag);
            stopBubble(window.event);
        }
        function AddCheck(dbid) {
            newQuestionID = dbid;
        }
        function GetChecked(dbid) {
            return ArrayFind(questionArr, "ID", dbid) != undefined ? "checked='checked'" : "";
        }
        function TrClick(tr) {
            var chk = $(tr).find(":checkbox[class=cb]");
            var flag = chk.prop("checked");
            Check(chk, !flag);
        }
        function BindDataToTab(bindData) {
            $("#tab tbody").empty();
            $(bindData).each(function (k) {
                if (this.ID == newQuestionID) {
                    questionArr.push(this);
                    newQuestionID = null;
                }
                var tr = "<tr class='" + GetTrClass(k) + "' onclick='TrClick(this)'>\
                            <td class='td_tit'>\
                                <input type='checkbox' onclick='Check(this)' " + GetChecked(this.ID) + " class='cb' name='ck' dbid='" + this.ID + "' title='" + this.Title + "' model='" + JSON.stringify(this) + "'/>\
                            </td>\
                            <td class='td_tit'>" + GetTrNum(k, pageIndex) + "</td>\
                            <td class='td_tit_left' title='" + this.Title + "'>" + SubText(this.Title, 15) + "</td>\
                            <td class='td_tit_left'>" + this.ClassificationName + "</td>\
                            <td class='td_tit_left'>" + this.QuestionType + "</td>\
                          </tr>";
                $("#tab tbody").append(tr);
            });
        }
        function stopBubble(e) {
            if (e && e.stopPropagation)
                e.stopPropagation();
            else
                window.event.cancelBubble = true;
        }
        function ShowData(index) {
            var postData = { CMD: "FullTab", TypeName: "QuestionStore", PageSize: pageSize, PageIndex: index };
            var model = {};
            var title = $.trim($("#txtTitle").val());
            if (title != "") {
                model.Title = title;
            }
            var type = $.trim($("#txtQuestionType").val());
            if (type != "") {
                model.QuestionType = type;
            }
            if (cfId) {
                model.ClassificationID = cfId;
            }
            if (PropertyCount(model) > 0) {
                postData.ConditionModel = JSON.stringify(model);
            }
            AjaxRequest("Handler/CommonHandler.aspx", postData, function (returnVal) {
                PagerRefresh(index, returnVal.PageCount);
                BindDataToTab($.parseJSON(returnVal.Data));
            })
        }

        $(function () {
            $("#ResourceCF").tree({
                method: 'post',
                animate: true,
                lines: true,
                url: 'CurriculumInfoTree.aspx',
                onClick: function (node) {
                    if (cfId == node.id) {
                        cfId = null;
                        $(node.target).removeClass("tree-node-selected");
                    }
                    else {
                        cfId = node.id;
                    }
                    ShowData(1);
                }
            });
            $("#EasyuiQt").combobox({
                onSelect: function (item) {
                    $("#txtQuestionType").val(item.value);
                    ShowData(1);
                }
            })
            $('#EasyuiQt').combobox('setValue', $("#txtQuestionType").val());

            LoadPageControl(ShowData, "pageDiv");
            ShowData(1);

            $("#btnSearch").click(function () {
                ShowData(1);
            })
            $("#txtTitle,#txtQuestionType").keypress(function () {
                EnterEvent("btnSearch");
            })
            $("#checkAll").click(function () {
                $("#tab :checkbox[class=cb]").each(function (k) {
                    Check(this, $("#checkAll").prop("checked"));
                });
            });
            $("#btnEnter").click(function () {
                OL_CloseLayerIframe({ qArr: questionArr });
            });
            $("#btnCancel").click(function () {
                OL_CloseLayerIframe();
            });
            $("#btnAdd").click(function () {
                SltTab(1);
            })
            $("#tabs").tabs({
                onSelect: function (title, index) {
                    if (index == 1) {
                        var src = $("#IframeQAdd").prop("src");
                        if (!src) {
                            LoadFrameIndex = layer.load(2);
                            $("#IframeQAdd").prop("src", "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/QuestionAddForSelect.aspx");
                        }
                    }
                }
            })
        })
    </script>
</head>
<body>

    <div class="easyui-tabs" id="tabs" data-options="tabWidth:140" style="width: 100%; height: 630px">
        <div title="选择问题" style="padding: 10px; overflow: hidden;">
            <div class="nav">
                <ul id="ResourceCF" class="easyui-tree"></ul>
            </div>
            <div class="workzone">
                <div class="tools">
                    <span>标题&nbsp</span><input type="text" id="txtTitle" class="input_part" style="width: 150px; height: 30px !important;" />&nbsp&nbsp
                    <span>题型&nbsp</span><input type="text" id="txtQuestionType" class="input_part" style="width: 150px; height: 30px !important;display:none;" />
                    <select class="easyui-combobox" name="state" id="EasyuiQt" data-options="editable:false" panelHeight="120" style="width:150px;height:28px;">
                        <option value="">所有</option>
                        <option value="单选题">单选题</option>
                        <option value="多选题">多选题</option>
                        <option value="判断题">判断题</option>
                        <option value="简答题">简答题</option>
                    </select>
                    <input type="button" value="查询" id="btnSearch" class="button_s" />
                </div>
                <div class="main_list_table" style="width: 520px; height: 436px;">
                    <table id="tab" class="list_table" cellspacing="0" cellpadding="0" border="0" style="width: 520px;">
                        <thead>
                            <tr class="tab_th top_th">
                                <th class="th_tit">
                                    <input type='checkbox' id="checkAll" /></th>
                                <th class="th_tit">序号</th>
                                <th class="th_tit_left">标题</th>
                                <th class="th_tit_left">分类</th>
                                <th class="th_tit_left">题型</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>
            <div style="text-align: center;width:100%;float:left;height:32px;">
                <div id="containerdiv" style="overflow: hidden; display: inline-block;">
                    <div id="pageDiv" class="pageDiv">
                    </div>
                </div>
                <div>
                    <input type="button" value="确定" class="button_s" id="btnEnter" />
                    <input type="button" value="取消" class="button_n" id="btnCancel" />&nbsp
                    <input type="button" value="添加问题" class="button_n" id="btnAdd" />
                </div>
            </div>
        </div>
        <div title="添加问题" style="padding: 10px">
            <iframe id="IframeQAdd" style="width: 100%; height: 100%; overflow: auto; border: 0;"></iframe>
        </div>
    </div>
</body>
</html>
