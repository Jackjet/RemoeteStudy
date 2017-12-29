<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RR_wp_ReserveRoomUserControl.ascx.cs" Inherits="SVDigitalCampus.ResourceReservation.RR_wp_ReserveRoom.RR_wp_ReserveRoomUserControl" %>
<link href="/_layouts/15/SVDigitalCampus/CSS/style.css" rel="stylesheet" />
<link href="/_layouts/15/SVDigitalCampus/CSS/iconfont.css" rel="stylesheet" />
<link href="/_layouts/15/SVDigitalCampus/CSS/common.css" rel="stylesheet" />
<script src="/_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script src="/_layouts/15/SVDigitalCampus/Script/popwin.js"></script>
<script src="/_layouts/15/SVDigitalCampus/Script/uploadFile2.js"></script>
<script src="/_layouts/15/SVDigitalCampus/Script/DatePicker/WdatePicker.js"></script>
<script type="text/javascript">
    $(function () {
        if (GetRequest().Type == "" || GetRequest().Type == null) {
            $(".kc_yylc_sp").hide();
        }
    })
    function Return() {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("sites"));

        window.location.href = FirstUrl + "sites/ZZSZXY/CouseManage/SitePages/CaurseManage.aspx";
    }

    function GetRequest() {
        var url = location.search; //获取url中"?"符后的字串

        var theRequest = new Object();

        if (url.indexOf("?") != -1) {

            var str = url.substr(1);

            strs = str.split("&");

            for (var i = 0; i < strs.length; i++) {

                theRequest[strs[i].split("=")[0]] = (strs[i].split("=")[1]);

            }

        }
        return theRequest;
    }
</script>
<script type="text/javascript">

    var arrPos = new Array();
    function getQueryString(name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return unescape(r[2]); return null;
    }
    $(document).ready(function () {
        //禁止th中德文本被选中
        if (document.all) {
            $("#tab tr th").onselectstart = function () { return false; };
        }
        else {
            $("#tab tr th").onmousedown = function () { return false; };
            $("#tab tr th").onmouseup = function () { return true; };
        }
        document.onselectstart = new Function('event.returnValue=false;');



        //$("td").css("height", "20px");
        //鼠标移动事件
        var flag = false;
        $("#tab tr td").mouseover(function () {
            if (flag) {
                var x = $(this).attr("rowindex");
                var y = $(this).attr("colindex");
                var select = $(this).attr("isselect");
                if (parseInt(x) >= parseInt(arrPos[0][0]) && arrPos.length > 0 && arrPos[0][1] == y) {
                    if (select == "true") {
                        flag = false;
                    }
                    else {
                        $(this).addClass("selected");
                        $(this).attr("appoint", "true");
                    }
                }
            }

        });
        $("body").mouseup(function (e) {
            if (flag)
            {
                flag = false;
                arrPos = new Array();
                submitData();
            }
            
        })

        //鼠标按下事件
        $("#tab tr td").mousedown(function (e) {
            
            $("#tab tr td").removeClass("selected");
            var x = $(e.target).attr("rowindex");
            var y = $(e.target).attr("colindex");
            var select = $(this).attr("isselect");
            if(select!="true")
            {
                flag = true;
                $(e.target).addClass("selected");
                $(e.target).attr("appoint", "true");
                arrPos.push(Array(x, y));
            }
            
            
            //$("#result").html("X:" + x + ";Y:" + y)

        });
        //鼠标抬起事件
        //$("#tab").mouseup(function (e) {
        //    //移除单元格的样式
        //    $("#tab tr td").removeClass("selected");
        //    //鼠标抬起时的坐标
        //    var x = $(e.target).attr("rowindex");
        //    var y = $(e.target).attr("colindex");
        //                    //行中选中
        //                    //arrPos里面存放的鼠标按下时的坐标
        //                    //保证数组里面有值，同时鼠标按下时和鼠标抬起时的坐标是同一行
        //                    //if (arrPos.length > 0 && arrPos[0][0] == x) {

        //                    //    $("#tab tr td").each(function () {
        //                    //        //每一个单元格的坐标
        //                    //        var thisx = $(this).attr("rowindex");
        //                    //        var thisy = $(this).attr("colindex");
        //                    //        //确认当前单元格的坐标大于等于鼠标按下时的坐标并小于等于鼠标抬起时的坐标，
        //                    //        if (thisx == x && thisy >= arrPos[0][1] && thisy <= y && 1 == key && e.target.tagName == "TD") {
        //                    //            $(this).addClass("selected");
        //                    //        }
        //                    //    });
        //                    //}
        //    //列中选中
        //    if (arrPos.length > 0 && arrPos[0][1] == y) {

        //        $("#tab tr td").each(function () {
        //            //每一个单元格的坐标
        //            var thisx = $(this).attr("rowindex");
        //            var thisy = $(this).attr("colindex");
        //            var select = $(this).attr("isselect");


        //            //确认在同一列中，&当前单元格的X坐标大于等于鼠标按下时的X坐标,&小于等于鼠标抬起时的坐标，
        //            if (parseInt(thisy) == parseInt(y) && parseInt(thisx) >= parseInt(arrPos[0][0]) && parseInt(thisx) <= parseInt(x) && 1 == key && e.target.tagName == "TD") {
        //                //如果有被选中的单元格，跳出循环
        //                if (select == "true") {
        //                    return false;
        //                }
        //                $(this).addClass("selected");
        //                $(this).attr("appoint", "true");
        //            }
        //        });
        //        //}
        //    }

        //    arrPos = new Array();
        //    key = 0;
        //});
    })
    function submitData() {
        var para = '';
        var weekNum = $("input[id$=Hid_WeekNum]").val();
        var resid = $("input[id$=Hid_ResId]").val();
        $("#tab tr td").each(function () {
            var appoint = $(this).attr("appoint");
            if (appoint == "true") {
                var nowvalue = $("input[id$=Hid_AppointData]").val();
                var thisx = $(this).attr("rowindex");
                var thisy = $(this).attr("colindex");
                $("input[id$=Hid_AppointData]").val(nowvalue + "#" + thisx + "," + thisy);
                para += "B" + thisx + "A" + thisy;
            }
        });
        if (para == '') {
            alert("你还没有预定！");
        }
        else {
            openPage('预定资源', '/SitePages/AddRoom.aspx?pagename=ReserveRoom&resid=' + resid + '&WeekID=' + getQueryString('WeekID') + '&Type=' + getQueryString('Type') + '&ClassID=' + getQueryString('ClassID') + '&weekNum=' + weekNum + '&para=' + para, '700', '500');
        }

    }
</script>
<style type="text/css">
    td {
        border: 1px solid #ACCFF5;
        height: 35px;
        text-align: center;
        line-height: 35px;
    }

    th {
        border: 1px solid black;
        height: 35px;
        text-align: center;
        line-height: 35px;
        font-weight: bold;
    }

    .selected {
        background-color: #c2dfa5;
    }

    #tab {
        width: 96%;
        border-collapse: collapse;
        margin: auto;
    }

        #tab tr th {
            background-color: #5593d7;
        }

    .toptr {
        background-color: #5593d7;
    }
</style>
<div>
    <div class="kc_yylc_sp">
        <div class="kc_yylc_spxian"></div>
        <div class="kc_yylc_spbiao">
            <table>
                <tbody>
                    <tr>

                        <td class="kc_yylc_sptd"><a onclick="Return()" class="kc_yylc_spzt kc_yylc_spon">&lt;&lt;返回</a></td>

                        <td class="kc_yylc_sptd">
                            <a id="BaseData" class="kc_yylc_spzt kc_yylc_spoff">基础资料</a>
                        </td>
                        <td class="kc_yylc_sptd">
                            <a id="SelData" class="kc_yylc_spzt kc_yylc_spoff">选取资源</a>
                        </td>
                        <td class="kc_yylc_sptd">
                            <a id="Task" class="kc_yylc_spzt kc_yylc_spoff">创建任务</a>
                        </td>
                        <td class="kc_yylc_sptd">
                            <a id="Check" class="kc_yylc_spzt kc_yylc_spoff">提交审核</a>
                            <div class="weitongguo" style="display: none" id="weitongguo"><span></span></div>
                        </td>
                        <td class="kc_yylc_sptd">
                            <a id="Room" class="kc_yylc_spzt kc_yylc_spon">预约教室</a>

                        </td>
                        <td class="kc_yylc_sptd">
                            <a id="CheckStu" class="kc_yylc_spzt kc_yylc_spoff">审核报名</a>
                        </td>

                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    <div class="zy_list">
        <div>

            <asp:Image ID="Img_ImgSource" runat="server" />
            <ul>
                <li>
                    <h2>
                        <asp:Literal ID="Lit_Title" runat="server"></asp:Literal></h2>
                </li>
                <li>地址：<asp:Literal ID="Lit_Address" runat="server"></asp:Literal></li>
                <li>面积：<asp:Literal ID="Lit_Area" runat="server"></asp:Literal>平</li>
                <li>开放时间：<asp:Literal ID="Lit_OpenTime" runat="server"></asp:Literal>~<asp:Literal ID="Lit_CloseTime" runat="server"></asp:Literal></li>
                <li>限定人数：<asp:Literal ID="Lit_LimitCount" runat="server"></asp:Literal>人</li>
            </ul>
        </div>

    </div>
    <div>
        <asp:HiddenField ID="Hid_ResId" runat="server" />
        <asp:HiddenField ID="Hid_AppointData" runat="server" />
        <asp:HiddenField ID="Hid_WeekNum" runat="server" />
        <div class="right_add fr" style="margin-top: 110px; margin-right: 20px;">
            <asp:LinkButton ID="LB_PreWeek" CssClass="add" OnClick="TB_PreWeek_Click" runat="server"><i class="iconfont">&#xe63b;</i>上一周</asp:LinkButton>
            <asp:LinkButton ID="LB_NextWeek" CssClass="add" OnClick="TB_NextWeek_Click" runat="server"><i class="iconfont">&#xe61a;</i>下一周</asp:LinkButton>
            <asp:LinkButton ID="LB_CurrentWeek" CssClass="add" OnClick="TB_CurrentWeek_Click" runat="server"><i class="iconfont">&#xe610;</i>当前周</asp:LinkButton>
            <a class="add" href="#" onclick="submitData();"><i class="iconfont">&#xe642;</i>预定</a>

        </div>
        <%--<asp:TextBox ID="TB_Data" CssClass="Wdate" onclick="javascript: WdatePicker()" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="Btn_Search" runat="server" Text="查询" OnClick="Btn_Search_Click" />--%>

        <table cellspacing="0" cellpadding="0" id="tab">
            <tr class="toptr">
                <th>课节</th>
                <th>
                    <asp:Literal ID="Lit_Mon" runat="server"></asp:Literal>周一
                </th>
                <th>
                    <asp:Literal ID="Lit_Tue" runat="server"></asp:Literal>周二
                </th>
                <th>
                    <asp:Literal ID="Lit_Wed" runat="server"></asp:Literal>周三
                </th>
                <th>
                    <asp:Literal ID="Lit_Thu" runat="server"></asp:Literal>周四
                </th>
                <th>
                    <asp:Literal ID="Lit_Fri" runat="server"></asp:Literal>周五
                </th>
                <th>
                    <asp:Literal ID="Lit_Sat" runat="server"></asp:Literal>周六
                </th>
                <th>
                    <asp:Literal ID="Lit_Sun" runat="server"></asp:Literal>周日
                </th>
            </tr>
            <asp:Literal ID="Lit_result" runat="server"></asp:Literal>
        </table>

    </div>



</div>
