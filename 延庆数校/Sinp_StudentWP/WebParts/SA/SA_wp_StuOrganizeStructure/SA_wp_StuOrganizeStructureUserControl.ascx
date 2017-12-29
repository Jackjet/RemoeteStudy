<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SA_wp_StuOrganizeStructureUserControl.ascx.cs" Inherits="Sinp_StudentWP.WebParts.SA.SA_wp_StuOrganizeStructure.SA_wp_StuOrganizeStructureUserControl" %>
<link rel="stylesheet" href="/_layouts/15/Stu_js/jOrgChart/css/bootstrap.min.css" />
<link rel="stylesheet" href="/_layouts/15/Stu_js/jOrgChart/css/jquery.jOrgChart.css" />
<link rel="stylesheet" href="/_layouts/15/Stu_js/jOrgChart/css/custom.css" />
<link rel="stylesheet" href="/_layouts/15/Stu_js/jOrgChart/css/prettify.css" type="text/css" />
<script type="text/javascript" src="/_layouts/15/Stu_js/jOrgChart/prettify.js"></script>
<!-- jQuery includes -->
<script type="text/javascript" src="/_layouts/15/Stu_js/jOrgChart/jquery.min.js"></script>
<script type="text/javascript" src="/_layouts/15/Stu_js/jOrgChart/jquery-ui.min.js"></script>
<script type="text/javascript" src="/_layouts/15/Stu_js/jOrgChart/jquery.jOrgChart.js"></script>
<script src="/_layouts/15/Script/popwin.js"></script>
<script type="text/javascript">
    var FirstUrl = window.location.href;
    FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))
    $(document).ready(function () {
        BindorgStructure();
        $("#show-list").click(function (e) {
            e.preventDefault();
            $('#list-html').toggle('fast', function () {
                if ($(this).is(':visible')) {
                    $('#show-list').text('Hide underlying list.');
                    $(".topbar").fadeTo('fast', 0.9);
                } else {
                    $('#show-list').text('Show underlying list.');
                    $(".topbar").fadeTo('fast', 1);
                }
            });
        });

        //$('#list-html').text($('#org').html());

        //$("#org").bind("DOMSubtreeModified", function () {
        //    $('#list-html').text('');

        //    $('#list-html').text($('#org').html());

        //    prettyPrint();
        //});
    });
    function BindorgStructure() {
        var timespan = Date.parse(new Date());
        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/Handler/OrganizeStructure.aspx?timespan=" + timespan,
            data: { method: "loadOrgStructure", listname: "学生会组织机构" },
            dataType: "text",
            success: function (returnVal) {
                returnVal = $.parseJSON(returnVal);
                if (returnVal.length > 0) {
                    var showlist = $("<ul id='org' style='display:none'></ul>");
                    showall(returnVal, showlist);
                    $(".org_page").append(showlist);
                    $("#org").jOrgChart({
                        chartElement: '#chart',
                        dragAndDrop: false
                    });
                    MenuMouse();
                } else {
                    $("#div_addfirst").show();
                }
            },
            error: function (errMsg) {
                alert('数据加载失败！');
            }
        });
    }
    function showall(menu_list, parent) { //menu_list为json数据 ,parent为要组合成html的容器      
        for (var menu in menu_list) {
            var curdepart = menu_list[menu];
            var dropMenu = "<dd><div class='lie'><ul class='liea'><li><a href='#' onclick=\"openPage('添加同级部门','/SitePages/AddDepartment.aspx?pId="+ curdepart.pId+"','650','420');return false;\">添加同级部门</a></li><li><a href='#' onclick=\"openPage('添加下级部门','/SitePages/AddDepartment.aspx?pId="+ curdepart.id+"','650','420');return false;\">添加下级部门</a></li><li><a href='#' onclick='DelDepartment(" + curdepart.id + ");'>删除</a></li></ul></div></dd>";
            if (curdepart.children.length > 0) { //如果有子节点，则遍历该子节点  
                var li = $("<li></li>");  //创建一个子节点li
                //将li的文本设置好，并马上添加一个空白的ul子节点，并且将这个li添加到父亲节点中
                $(li).append("<dl class='dropmenu'><dt> <a href='DepartmentShow.aspx?itemid=" + curdepart.id + "'>" + curdepart.name + "</a></dt>" + dropMenu + "</dl>").append("<ul></ul>").appendTo(parent);
                showall(curdepart.children, $(li).children().eq(1));  //将空白的ul作为下一个递归遍历的父亲节点传入
            }               
            else {  //如果该节点没有子节点，则直接将该节点li以及文本创建好直接添加到父亲节点中
                $("<li></li>").append("<dl class='dropmenu'><dt> <a href='DepartmentShow.aspx?itemid=" + curdepart.id + "'>" + curdepart.name + "</a></dt>" + dropMenu + "</dl>").appendTo(parent);
            }            
        }
    }
    function DelDepartment(departid) {
        if (confirm("确认删除该部门吗？")) {
            var timespan = Date.parse(new Date());
            $.ajax({
                type: "Post",
                url: FirstUrl + "/_layouts/15/Handler/OrganizeStructure.aspx?timespan=" + timespan,
                data: { method: "DelDepartment", listname: "学生会组织机构", departid: departid },
                dataType: "text",
                success: function (returnVal) {
                    if (returnVal == "1") {
                        alert("删除成功");
                    }
                    $("#org").remove();
                    $(".jOrgChart").remove();
                    BindorgStructure();
                },
                error: function (errMsg) {
                    alert('删除失败！');
                }
            });
        }
    }
    function MenuMouse()
    {
        //下拉列表
        $(".dropmenu").mouseover(function () {
            $(this).find(".lie").show();
            $(this).find("dt").addClass("hover");
            $(this).find("em").addClass("hover");
        });
        $(".dropmenu").mouseout(function () {
            $(this).find(".lie").hide();
            $(this).find("dt").removeClass("hover");
            $(this).find("em").removeClass("hover");
        });
    }
    function openPage(pageTitle, pageName, pageWidth, pageHeight) {
        var webUrl = _spPageContextInfo.webAbsoluteUrl;
        popWin.showWin(pageWidth, pageHeight, pageTitle, webUrl + pageName);
    }
    function closePages() {
        $("#mask,#maskTop").fadeOut(function () {
            $(this).remove();
        });
    }
</script>
<body onload="prettyPrint();">
    <div class="org_page">
        <div style="width: 95%; margin: auto;">
            <div class="Gl_tabheader">
                <ul class="tab_tit" style="width: 100%;">
                    <li class="selected"><a href="javascript:void(0)">学生会组织结构</a></li>
                </ul>
            </div>   
            <div id="div_addfirst" style="display:none;"><a href="#" class="addbtn" onclick="openPage('添加根部门','/SitePages/AddDepartment.aspx?pId=0','650','420');return false;">添加根部门</a></div>                   
            <div id="chart" class="orgChart"></div>
        </div>
    </div>
</body>
