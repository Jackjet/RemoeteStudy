<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClassificationMgr.ascx.cs" Inherits="YHSD.VocationalEducation.Portal.CONTROLTEMPLATES.YHSD.VocationalEducation.Portal.ClassificationMgr" %>
<style type="text/css">
    #dialogDiv ul li {
        list-style: none;
        margin-top: 5px;
    }

    .leftDiv {
        float: left;
        margin: 5px;
        /*min-height:300px;*/
    }


    .dialogDiv {
        width: 100%;
    }

    .mainDiv {
        height: auto;
        width: 100%;
        float: left;
    }

    .combo-p {
        z-index: 29891014 !important; /*避免被layer层遮盖*/
    }

    .tableEdit {
    }
</style>
<script type="text/javascript">
    var EditNode = { id: 0 };
    var oldFolderPath;//原路径
    var CMDType = 0;//0-Add/1-Edit

    function CancelEdit() {
        $('#RName').val('');
        layer.closeAll();
    }

    function EnterEdit() {
        var rName = $("#RName").val();
        if ($.trim(rName) == "") {
            LayerTips("请输入分类名称!", "RName");
            return;
        }
        if ($.trim(rName).length > 25) {
            LayerTips("分类名称不能超过25个汉字!", "RName");
            return;
        }
        if (CMDType == 0) {
            var node = $('#cc').combotree("tree").tree("getSelected");
            var grade = GetLevel(node.target);
            var path = GetPath(node);

            var postData = { CMD: "AddCF", CFName: rName, Pid: node.id, Grade: grade, Path: path };

            AjaxRequest("<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/Handler/ClassificationMgrHandler.aspx", postData, function (returnVal) {
                LoadTree();//重新加载数据
                CancelEdit();
            });
        }
        else {
            var treeNode = $('#cc').combotree("tree").tree("getSelected");//下拉框选择的节点
            var newFolderPath = GetPath(treeNode) + "/" + rName;
            //alert("Old:"+oldFolderPath + "\n" + "New:"+newFolderPath);

            var oldNode = $('#cc').combotree("tree").tree("find", EditNode.id);//原节点
            var childs = $('#cc').combotree("tree").tree("getChildren", oldNode.target);//原节点的所有子节点
            var isChild = false;//判断原节点是否在子节点中
            if (EditNode.id == treeNode.id)//不能把自己设置为上级分类
                isChild = true;
            for (var i = 0; i < childs.length; i++) {
                if (isChild)
                    break;
                if (childs[i].id == treeNode.id) {//如果子节点中包含新选择的上级节点
                    isChild = true;
                }
            }
            if (isChild) {
                LayerTips("上级分类不能为本身或者本身的子级分类!", "DivCCTip");
                return;
            }
            var postData = { CMD: "EditCF", ID: EditNode.id, PID: treeNode.id, CFName: rName, OldPath: oldFolderPath, NewPath: newFolderPath };
            AjaxRequest("<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/Handler/ClassificationMgrHandler.aspx", postData, function (returnVal) {
                LoadTree();//重新加载数据
                CancelEdit();
            });
        }
    }

    function GetLevel(target) {
        return $(target).parentsUntil("ul.tree", "ul").length + 1;
    }

    function GetPath(node, tree) {
        var nodePath = "";
        if (node.id != 0) {
            nodePath = "/" + node.text;
        }
        function RecursivePath(target) {
            var parentNode;
            if (tree == null)
                parentNode = $('#cc').combotree('tree').tree("getParent", target);
            else
                parentNode = tree.tree("getParent", target);

            if (parentNode != null && parentNode.id != 0) {
                nodePath = "/" + parentNode.text + nodePath;
                RecursivePath(parentNode.target);
            }
        }
        RecursivePath(node.target);
        return nodePath;
    }

    function LoadTree() {
        var loadIndex = layer.load(2);
        var count = 0;
        function CloseLoad() {
            if (count >= 2)
                layer.close(loadIndex);
        }
        try {
            $("#ResourceCF").tree({
                method: 'post',
                animate: true,
                lines: false,
                url: '<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/CurriculumInfoTree.aspx?isEdit=1',
                onClick: function (node) {
                    EditNode = node;
                },
                onLoadSuccess: function () {
                    count += 1;
                    CloseLoad();
                }
            });
            $('#cc').combotree({
                url: '<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/CurriculumInfoTree.aspx?isEdit=1',
                required: false,
                onSelect: function (node) {
                    var id = node.id;
                },
                onLoadSuccess: function () {
                    count += 1;
                    CloseLoad();
                }
            })
            EditNode = { id: 0 };
        } catch (e) {
            console.error("加载树失败:" + e.message);
            layer.close(loadIndex);
        }
    }

    $(function () {
        LoadTree();
        $("#RName").keypress(function () {
            EnterEvent(EnterEdit);
        })
        $("#btnAddCF").click(function () {
            CMDType = 0;
            $('#cc').combotree("setValue", EditNode.id);
            $("#RName").focus();
            OL_ShowLayer(1, '添加分类', 400, 230, $('#EditDiv'));
        })
        $("#btnMoveToTop").click(function () {
            var sltNode = $("#ResourceCF").tree("getSelected");
            if (!sltNode) {
                return;
            }
            var preId = sltNode.id;
            if ($(sltNode.target.parentNode).prev()[0] == undefined) {
                return;
            }
            var nextTarget = $(sltNode.target.parentNode).prev()[0].firstChild;
            var nextNode = $("#ResourceCF").tree("getNode", nextTarget);
            var nextId = nextNode.id;
            var postData = { CMD: "UpdateOrderNum", PreID: preId, NextID: nextId };
            AjaxRequest("<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/Handler/ClassificationMgrHandler.aspx", postData, function (returnVal) {
                $(sltNode.target.parentNode).insertBefore($(sltNode.target.parentNode).prev());
            });
        })
        $("#btnMoveToButtom").click(function () {
            var sltNode = $("#ResourceCF").tree("getSelected");
            if (!sltNode) {
                return;
            }
            var preId = sltNode.id;
            if ($(sltNode.target.parentNode).next()[0] == undefined) {
                return;
            }
            var nextTarget = $(sltNode.target.parentNode).next()[0].firstChild;
            var nextNode = $("#ResourceCF").tree("getNode", nextTarget);
            var nextId = nextNode.id;
            var postData = { CMD: "UpdateOrderNum", PreID: preId, NextID: nextId };
            AjaxRequest("<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/Handler/ClassificationMgrHandler.aspx", postData, function (returnVal) {
                $(sltNode.target.parentNode).insertAfter($(sltNode.target.parentNode).next());
            });
        })
        $("#btnEdit").click(function () {
            CMDType = 1;//编辑模式
            if (EditNode.id == 0) {
                return;
            }
            var parentNode = $("#ResourceCF").tree("getParent", EditNode.target);
            if (parentNode != null)
                $('#cc').combotree("setValue", parentNode.id);
            else
                $('#cc').combotree("setValue", 0);
            oldFolderPath = GetPath(EditNode, $("#ResourceCF"));

            $('#RName').val(EditNode.text);
            $("#RName").focus();
            //$('#cc').combotree('disable');
            OL_ShowLayer(1, '编辑分类', 400, 230, $('#EditDiv'), function () {
                //$('#cc').combotree('enable');
            });

        })
        $("#btnDel").click(function () {
            if (EditNode.id == 0) {
                return;
            }
            var childs = $("#ResourceCF").tree("getChildren", EditNode.target);
            if (childs.length > 0) {
                LayerAlert("请先删除子级分类!");
                return;
            }

            LayerConfirm("确定删除?", function () {
                var sltId = EditNode.id;
                var path = GetPath(EditNode);
                var postData = { CMD: "DelCF", DelID: sltId, Path: path };

                AjaxRequest("<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/Handler/ClassificationMgrHandler.aspx", postData, function (returnVal) {
                    LoadTree();//重新加载数据
                    EditNode = { id: 0 };
                    CancelEdit();
                });
            });
        })
    })
</script>

<div id="main" class="mainDiv">
    <div class="leftDiv">
        <div class="nav">
            <ul id="ResourceCF" class="easyui-tree"></ul>
        </div>
    </div>
</div>
<div id="toolbar">
    <input type="button" class="button_s" id="btnAddCF" value="添加分类" />
    <input type="button" class="button_s" id="btnMoveToTop" value="上移" />
    <input type="button" class="button_s" id="btnMoveToButtom" value="下移" />
    <input type="button" class="button_s" id="btnEdit" value="编辑" />
    <input type="button" class="button_n" id="btnDel" value="删除" />
</div>

<div id="EditDiv" style="display: none; margin-top: 10px;">
    <table class="tableEdit" style="margin-top: 20px; width: 100%;">
        <tr>
            <th style="padding: 10px;">上级分类</th>
            <td>
                <div id="DivCCTip">
                    <select id="cc" class="easyui-combotree" style="width: 300px; height: 29px;"></select>
                </div>
            </td>
        </tr>
        <tr>
            <th style="padding: 10px;">分类名称</th>
            <td>
                <input type="text" class="input_part" id="RName" style="width: 288px !important; height: 25px;" />
        </tr>
        <tr>
            <td colspan="2" style="text-align: center; padding: 20px;">
                <input type="button" class="button_s" value="确定" onclick="EnterEdit()" /><input type="button" class="button_n" value="取消" onclick="    CancelEdit()" /></td>
        </tr>
    </table>
</div>
