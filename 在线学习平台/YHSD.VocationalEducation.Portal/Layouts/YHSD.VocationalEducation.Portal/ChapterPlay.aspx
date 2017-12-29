<%@ Assembly Name="YHSD.VocationalEducation.Portal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=2e18f1308c96fd22" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChapterPlay.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.ChapterPlay" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <script type="text/javascript" src="ckplayer/ckplayer.js" charset="utf-8"></script>
    <style>
        .details_list .teacher_list {
            margin-top: 20px;
            margin-left: -16px;
        }

        .teacher_list .TabelHome {
            padding: 15px;
        }

        .TabelHome tr {
            line-height: 30px;
        }

        .TabelHome td {
            font-size: 13px;
            font-family: "微软雅黑","宋体";
            color: #666;
            padding-right: 10px;
        }

        .TabelHome .title {
            color: #333;
        }

        .TabelHome .time {
            color: #999;
        }

        .TabelHome .red {
            color: red;
            font-size:16px;
           
        }
    </style>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div class="classes_details f_l">
        <div class="classes_nav">
            <a class="nav_link" href="<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/MyCourseList.aspx">我的课程</a>
            <span class="nav_txt">&gt;</span>
            <a class="nav_link" onclick="TiaoZhuan('<%=CurriculumID%>')">
                  <%=CurriculumTitle %>
            </a>
            <script>
                function TiaoZhuan(id) {
                    location.href = "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/MyCourseDetails.aspx?id=" + id;
                }
            </script>
            <span class="nav_txt">&gt;<%=ChapterTitle %></span>
        </div>
        <div class="details_content">
            <div id="a1" style="margin-left: auto; margin-right: auto;"></div>
            <script type="text/javascript" src="ckplayer/ckplayer.js" charset="utf-8"></script>
            <asp:HiddenField ID="HDChapterID" runat="server" />
            <div class="play_list">
                <h4 class="play_list_title">课程列表</h4>
                <div>
                    <table>
                        <tr>
                            <td style="width: 10px;">
                                <img onclick="ShangYiYe()" src="images/Zuo.png" /></td>

                            <asp:Repeater ID="RepeaterPlay" runat="server">

                                <ItemTemplate>
                                    <td style="text-align: center; cursor: pointer; width: 150px;" id="td<%#Eval("SerialNumber") %>" title="<%#Eval("WorkDescription") %>" onclick="TiaoZhuanChapter(<%#Eval("SerialNumber") %>)">

                                        <img id="img<%#Eval("SerialNumber") %>" style="width: 130px; height: 80px;" src="<%#Eval("PhotoUrl") %>" />
                                        <div style="margin: 0 auto; background-color: black; width: 130px; height: 20px; margin-top: -30px; position: relative; padding-top: 5px; filter: alpha(Opacity=40); -moz-opacity: 0.5; opacity: 0.6;"></div>
                                        <div id="divJiShu<%#Eval("SerialNumber") %>" style="color: white; margin-top: -20px; text-align: center; font-size: 12px; position: relative;">第<%#Eval("SerialNumber") %>章</div>
                                        <div id="divName<%#Eval("SerialNumber") %>" style="margin-top: 10px;"><%#Eval("Title") %></div>
                                        <input type="hidden" id="HidJiShu<%#Eval("SerialNumber") %>" value="第<%#Eval("SerialNumber") %>章" />
                                        <input type="hidden" id="HidUrl<%#Eval("SerialNumber") %>" value="<%#Eval("SPUrl") %>" />
                                        <input type="hidden" id="HidId<%#Eval("SerialNumber") %>" value="<%#Eval("Id") %>" />

                                    </td>
                                </ItemTemplate>
                            </asp:Repeater>
                            <td>
                                <img onclick="XiaYiYe()" style="cursor: pointer;" src="images/You.png" /></td>

                        </tr>
                    </table>
                </div>
            </div>
            <div class="details_list">
                
		<div class="list_left f_l" >
                    <h4 class="list_title">作业描述</h4>
		<script>
		    var DivHeight = 0;
		    $(function () {

		        if ($("#DivWorkDescription").height() > 300) {
		            DivHeight = $("#DivWorkDescription").height();
		            $("#DivWorkDescription").css("height", "300px");
		            $("#clickwork").css("display", "block")
		        }
		        else {
		            $("#DivWorkDescription").css("height", "300px");
		        }
		    });
		    function clickWorkDescription() {
		        if ($("#clickwork").text() == "查看全部▼") {
		            $("#DivWorkDescription").animate({ "height": DivHeight + "px" }, 100);
		            $("#clickwork").text("收起▲");
		        }
		        else {
		            $("#DivWorkDescription").animate({ "height": "300px" }, 100);
		            $("#clickwork").text("查看全部▼")
		        }
		    }
		</script>
                    <div class="list_right_main" id="DivWorkDescription" style="overflow: hidden;">
                                <%=WorkDescription %>
                        <div class="clear"></div>
                    </div>
			<a id="clickwork" style="text-align: center; color: blue; font-size: 15px; display: none;text-decoration:none;cursor:pointer;" onclick="clickWorkDescription()">查看全部▼</a>
                </div>
 	     <div class="list_right f_l" style="width:263px;">
                    <h4 class="list_title">课件下载</h4>
                    <div class="list_right_main">

                        <ul id="ulAttachment">
                            <label id="LabelAttachment" runat="server" />

                        </ul>
                        <div class="clear"></div>
                    </div>
                </div>
                <div class="list_left f_l">
                    <h4 class="list_title">学习心得</h4>
                    <div class="study_main">
                        <div id="fileQueue"></div>
                        <input type="file" name="uploadify" id="uploadify" />
                        <script src="js/uploadify/jquery.uploadify.min.js"></script>
                        <link href="js/uploadify/uploadify.css" rel="stylesheet" />
                        <script type="text/javascript" language="javascript">
                            $(function () {

                                var FromData = { ChapterID: document.getElementById("<%=this.HDChapterID.ClientID %>").value };
                        $("#uploadify").uploadify({
                            'auto': false,                      //是否自动上传
                            'swf': 'js/uploadify/uploadify.swf',      //上传swf控件,不可更改
                            'uploader': 'ChapterPlay.aspx',            //上传处理页面,可以aspx
                            'formData': FromData, //参数
                            //'fileTypeDesc': '',
                            // 'fileTypeExts': '*.pdf;*.jpg;*.jpeg;*.gif;*.png',   //文件类型限制,默认不受限制
                            'buttonText': '选择作业',//按钮文字
                            // 'cancelimg': 'uploadify/uploadify-cancel.png',
                            'width': 100,
                            'height': 26,
                            //最大文件数量'uploadLimit':
                            'multi': true,//单选            
                            'fileSizeLimit': '20MB',//最大文档限制
                            'queueSizeLimit': 10,  //队列限制
                            'removeCompleted': true, //上传完成自动清空
                            'removeTimeout': 1, //清空时间间隔
                            'onUploadSuccess': function (file, data, response) {
                                $('#' + file.id).find('.data').html('         作业上传完成');
                                var str = $.parseJSON(data);
                                if (str != null) {
                                    document.getElementById("<%=this.LabelStudyExperience.ClientID %>").innerHTML = "<div class='study_main_text'><div class='study_main_left f_l'><img src='" + str.CreaterUrl + "' onerror=\"this.src='/_layouts/15/YHSD.VocationalEducation.Portal/images/NoTouXiang.png'\" width='53' height='53' /></div><div class='f_l'><p class='user_name'>" + str.CreaterName + "<a onclick=\"deleteStudyExperience('" + str.Id + "')\" style='margin-left: 300px;' title='删除心得'><img class='DelImg'></a></p><p class='text_main'><a href='" + str.FilePhysicalPath + "'>" + str.FileName + "</a></p><p class='date'>" + str.CreaterTime + "</p></div><div class='clear'></div></div>" + document.getElementById("<%=this.LabelStudyExperience.ClientID %>").innerHTML;
                                 }
                                $("#DivShangChuan").css("display", "none")
                            },
                            'onSelect': function (file)
                            {
                                if ($("#DivShangChuan").css("display") == "none") {
                                    $("#DivShangChuan").css("display", "block");
                                }
                            },
                    });
                    });

                        </script>
                        <div id="DivShangChuan" style="margin-left: 10px; float:right; display:none;"><span class="study_main_button" onclick="javascript:$('#uploadify').uploadify('upload','*')">上传</span></div>
                        <br />
                        <div class="study_main_text"></div>

                        <label id="LabelStudyExperience" runat="server" />
                    </div>
                </div>
                <div class="clear"></div>
            </div>
        </div>
    </div>

    <script type="text/javascript">

        function playerstop() {
            //setTimeend();
        }

        function setTimeend() {//获取下一部视频的播放ID
            if (nowD % pageCount == 0 && nowD != count) {
                XiaYiYe();
            }
            nowD++;
            if (nowD > count) {
                nowD = 1;
            }
            TiaoZhuanChapter(nowD);
        }
        var num = "<%=PlayNum%>";

                var pageCount = "<%=PageCount%>";
        var DangQianYe = Math.ceil(num / pageCount);
        var count = "<%=PlayCount%>"
                playvideo(num); //设置开始从点击的章节开始播放
                TuPian(pageCount, DangQianYe); //每页显示5个视频
                function TuPian(n, y) {
                    var MaxID = n * y;
                    var MinID = MaxID - 5;
                    for (var i = 1; i <= count; i++) {
                        if (i <= MinID || i > MaxID) {
                            $(document.getElementById("td" + i)).css("display", "none");
                        }
                        else {
                            $(document.getElementById("td" + i)).css("display", "");
                        }
                    }
                }
                function XiaYiYe() {
                    if (DangQianYe != Math.ceil(count / pageCount)) {
                        DangQianYe++;
                        TuPian(pageCount, DangQianYe);
                    }
                }
                function ShangYiYe() {
                    if (DangQianYe != 1) {
                        DangQianYe--;
                        TuPian(pageCount, DangQianYe);
                    }
                }
                function playvideo(n) {

                    nowD = n;
                    var flashvars = {
                        f: $(document.getElementById("HidUrl" + n)).val(),
                        c: 0,
                        p: 0,
                        e: 0,
                        my_url: encodeURIComponent(window.location.href)
                    };

                    for (i = 1; i <= count; i++) {//这里是用来改变右边列表背景色


                        if (i != nowD) {
                            $(document.getElementById("img" + i)).css("border", "");
                            $(document.getElementById("divJiShu" + i)).text($(document.getElementById("HidJiShu" + i)).val());
                            $(document.getElementById("divJiShu" + i)).css("color", "white");
                            $(document.getElementById("divName" + i)).css("color", "Black");
                        }
                        else {
                            $(document.getElementById("img" + i)).css("border", "4px solid #FF6919");
                            $(document.getElementById("divJiShu" + i)).text("播放中");
                            $(document.getElementById("divJiShu" + i)).css("color", "#FF6919");
                            $(document.getElementById("divName" + i)).css("color", "#FF6919");
                        }

                    }

                    var params = { bgcolor: '#FFF', allowFullScreen: true, allowScriptAccess: 'always', wmode: 'transparent' };
                    document.getElementById("<%=this.HDChapterID.ClientID %>").value = $(document.getElementById("HidId" + n)).val();
<%--                       var postData = { SerialNumber: $(document.getElementById("HidId" + n)).val() };
                    $.ajax({
                        type: "Post",
                        url: "ChapterPlay.aspx",
                        data: postData,
                        dataType: "text",
                        success: function (returnVal) {
                            var str = $.parseJSON(returnVal);
                            if (str != null) {
                                $("#ulAttachment").empty();
                                for (var i = 0; i < str.length; i++) {
                                    $("#ulAttachment").append("<li class='main_list '><a class='txt_tip' href='" + str[i].FilePhysicalPath + "'>" + str[i].FileName

+ "</a></li>");
                                }
                            }
                        },
                        error: function (errMsg) {
                            alert('数据加载失败！');
                        }
                    })--%>
                    CKobject.embedSWF('ckplayer/ckplayer.swf', 'a1', 'ckplayer_a1', '100%', '318', flashvars, params);

                }
                function TiaoZhuanChapter(n) {
                    //alert($(document.getElementById("HidId" + n)).val());
                    window.location.href = "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/ChapterPlay.aspx?id=" + $(document.getElementById("HidId" + n)).val() + "&playid=" + n + "";
                }
               function deleteStudyExperience(id)
              {
                   LayerConfirmDelete("确定删除此心得吗?", function () {
                       var postData = { DeleteID: id };
                       var loadIndex = layer.load(2);
                       $.ajax({
                           type: "POST",
                           url: "ChapterPlay.aspx",
                           data: postData,
                           dataType: "text",
                           success: function (returnVal) {
                               layer.close(loadIndex);
                               if (returnVal != "" && returnVal != "undefined") {
                                   LayerAlert(returnVal, function () {
                                           window.location.href = window.location.href;
                                       });

                               }
                           },
                           error: function (errMsg) {
                               LayerAlert('删除失败！！');
                           }
                       })
                   });
              }

    </script>

</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
</asp:Content>
