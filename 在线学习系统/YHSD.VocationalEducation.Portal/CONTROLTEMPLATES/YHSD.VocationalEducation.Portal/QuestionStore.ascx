<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QuestionStore.ascx.cs" Inherits="YHSD.VocationalEducation.Portal.CONTROLTEMPLATES.YHSD.VocationalEducation.Portal.QuestionStore" %>
<script type="text/javascript">
    function BindDataToTab(bindData) {
        $("#tab tbody").empty();
        $(bindData).each(function (k) {
            var tr = "<tr class='" + GetTrClass(k) + "'>\
                            <td class='td_tit'>" + GetTrNum(k, pageIndex) + "</td>\
                            <td class='td_tit_left' title='" + this.Title + "' style='width:360px;'>" + SubText(this.Title, 25) + "</td>\
                            <td class='td_tit_left'>" + this.ClassificationName + "</td>\
                            <td class='td_tit_left'>" + this.QuestionType + "</td>\
                            <td class='td_tit_left'>" + this.UserName + "</td>\
                            <td class='td_tit'>" + this.CreateTime + "</td>\
                            <td class='td_tit'>\
                                <a title='编辑问题' onclick='EditQuestion(\"" + this.ID + "\")'><img class='EditImg' /></a>\
                                <a title='删除问题' onclick='DeleteQuestion(\"" + this.ID + "\")'><img class='DelImg'/></a>\
                            </td>\
                        </tr>";
            $("#tab tbody").append(tr);
        });
    }
    function AddQuestion() {
        location.href = "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/QuestionAdd.aspx";
    }
    function EditQuestion(editId, type) {
        location.href = "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/QuestionAdd.aspx?EditID=" + editId;
    }
    function DeleteQuestion(id) {
        LayerConfirm("确定删除?", function () {
            AjaxRequest("<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/Handler/QuestionStoreHandler.aspx", { CMD: "DeleteQuestion", ID: id }, function (returnVal) {
                ShowData(1);
                layer.closeAll();
            });
        });
    }
    function ShowData(index) {
        var postData = { CMD: "FullTab", TypeName: "QuestionStore", PageSize: pageSize, PageIndex: index };
        var questionType = $("#txtQuestionType").val();
        var title = $("#txtTitle").val();
        var classificationName = $("#txtClassification").val();
        var model = {};
        if (title != "") {
            model.Title = title;
        }
        if (classificationName != "") {
            model.ClassificationName = classificationName;
        }
        if (questionType != "") {
            model.QuestionType = questionType;
        }
        if (PropertyCount(model) > 0) {
            postData.ConditionModel = JSON.stringify(model);
        }
        AjaxRequest("<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/Handler/CommonHandler.aspx", postData, function (returnVal) {
            PagerRefresh(index, returnVal.PageCount);
            BindDataToTab($.parseJSON(returnVal.Data));
        })
    }

    $(function () {
        LoadPageControl(ShowData, "pageDiv");
        ShowData(1);
        $("#btnQuery").click(function () {
            ShowData(1);
        })
        $("#txtTitle,#txtClassification,#txtQuestionType").keypress(function () {
            EnterEvent("btnQuery");
        })
        $("#EasyuiQt").combobox({
            onSelect: function (item) {
                $("#txtQuestionType").val(item.value);
                ShowData(1);
            }
        })
        $('#EasyuiQt').combobox('setValue', $("#txtQuestionType").val());
    })
</script>
<div class="TopToolbar">
    <span>标题&nbsp</span><input type="text" class="input_part" style="width: 150px;" id="txtTitle" />&nbsp&nbsp
    <span>分类&nbsp</span><input type="text" class="input_part" style="width: 150px;" id="txtClassification" />&nbsp&nbsp
    <span>题型&nbsp</span><input type="text" class="input_part" style="width: 150px;display:none;" id="txtQuestionType"/>
    <select class="easyui-combobox" name="state" id="EasyuiQt" data-options="editable:false" panelHeight="120" style="width:150px;height:28px;">
        <option value="">所有</option>
        <option value="单选题">单选题</option>
        <option value="多选题">多选题</option>
        <option value="判断题">判断题</option>
        <option value="简答题">简答题</option>
    </select>
    <input type="button" value="查询" id="btnQuery" class="button_s" />
</div>
<div class="main_list_table">
    <table id="tab" class="list_table" cellspacing="0" cellpadding="0" border="0">
        <thead>
            <tr class="tab_th top_th">
                <th class="th_tit">序号</th>
                <th class="th_tit_left">标题</th>
                <th class="th_tit_left">分类</th>
                <th class="td_tit_left">题型</th>
                <th class="td_tit_left">出题人</th>
                <th class="th_tit">出题时间</th>
                <th class="th_tit">操作</th>
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>
</div>
<div style="text-align:center;"> 
    <div id="containerdiv" style="overflow: hidden; display: inline-block;">
        <div id="pageDiv" class="pageDiv">
        </div>
    </div>
</div>
<div class="main_button_part">
    <input type="button" value="出题" id="btnAdd" onclick="AddQuestion()" class="button_s" />
</div>
