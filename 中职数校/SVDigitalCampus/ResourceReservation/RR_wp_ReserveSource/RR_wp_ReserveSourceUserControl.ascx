<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RR_wp_ReserveSourceUserControl.ascx.cs" Inherits="SVDigitalCampus.ResourceReservation.RR_wp_ReserveSource.RR_wp_ReserveSourceUserControl" %>
<link href="/_layouts/15/SVDigitalCampus/CSS/style.css" rel="stylesheet" />
<link href="/_layouts/15/SVDigitalCampus/CSS/iconfont.css" rel="stylesheet" />
<link href="/_layouts/15/SVDigitalCampus/CSS/common.css" rel="stylesheet" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/animate.css" />

<script src="/_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script src="/_layouts/15/SVDigitalCampus/Script/tz_slider.js"></script>
<script src="/_layouts/15/SVDigitalCampus/Script/popwin.js"></script>
<script src="/_layouts/15/SVDigitalCampus/Script/uploadFile2.js"></script>
<script src="/_layouts/15/SVDigitalCampus/Script/DatePicker/WdatePicker.js"></script>

<script type="text/javascript">

    var key = 0;
    var arrPos = new Array();

    function getQueryString(name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return unescape(r[2]); return null;
    }


    $(function () {
        if (document.all) {
            $("#tabcar tr th").onselectstart = function () { return false; };
        }
        else {
            $("#tabcar tr th").onmousedown = function () { return false; };
            $("#tabcar tr th").onmouseup = function () { return true; };
        }
        document.onselectstart = new Function('event.returnValue=false;');

        //顶部的三个选项卡回发定位
        var topitem = $("input[id$=Hid_topItem]").val();
        if (getQueryString("topItem") != null) {
            topitem = getQueryString("topitem");
        }
        $(".my_order").eq(topitem).show().siblings().hide();
        $(".qijin a").removeClass("Enable");
        $(".qijin a").eq(topitem).addClass("Enable");
        $(".qijin a").click(function () {
            $(".qijin a").removeClass("Enable");
            $(this).addClass("Enable");
            var ind = $(this).index();
            $("input[id$=Hid_topItem]").val(ind);
            $(".my_order").eq(ind).show().siblings().hide();
        });
        //汽车日期的回发定位
        var linum = $("input[id$=Hid_linum]").val();
        var opera = $("input[id$=Hid_opera]").val();
        if (getQueryString("linum") != null && opera == "0") {
            linum = getQueryString("linum");
        }
        $(".zyyd_day li").eq(linum).addClass("active").siblings().removeClass("active");

        

        $("#tabcar").find("tr").each(function () {
            var $tr = $(this);
            $tr.children("td").each(function () {
                var $td = $(this);
                $td.attr("rowindex", $tr.index());
                $td.attr("colindex", $td.index() - 1);

            })
        });

        //$("#tabcar").mousedown(function (e) {

        //    var x = $(e.target).attr("rowindex");
        //    var y = $(e.target).attr("colindex");
        //    arrPos.push(Array(x, y));
        //    //$("#result").html("X:" + x + ";Y:" + y)
        //    key = 1;
        //});
        $("#tabcar tr td").mousedown(function (e) {
            
            $("#tabcar tr td").removeClass("selected");
            var x = $(e.target).attr("rowindex");
            var y = $(e.target).attr("colindex");
            var select = $(this).attr("isselect");
            if (select!="true")
            {
                flag = true;
                $(e.target).addClass("selected");
                $(e.target).attr("appoint", "true");
                arrPos.push(Array(x, y));
            }
            
            
            //$("#result").html("X:" + x + ";Y:" + y)

        });
        var flag = false;
        $("#tabcar tr td").mouseover(function () {
            if (flag) {
                var x = $(this).attr("rowindex");
                var y = $(this).attr("colindex");
                var select = $(this).attr("isselect");
                if (parseInt(y) >= parseInt(arrPos[0][1]) && arrPos.length > 0 && arrPos[0][0] == x) {
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

        //    $("#tabcar").mouseup(function (e) {
        //        //移除单元格的样式
        //        $("#tabcar tr td").removeClass("selected");
        //        //鼠标抬起时的坐标
        //        var x = $(e.target).attr("rowindex");
        //        var y = $(e.target).attr("colindex");
        //        //行中选中
        //        //arrPos里面存放的鼠标按下时的坐标
        //        //保证数组里面有值，同时鼠标按下时和鼠标抬起时的坐标是同一行
        //        if (arrPos.length > 0 && arrPos[0][0] == x) {

        //            $("#tabcar tr td").each(function () {
        //                //每一个单元格的坐标
        //                var thisx = $(this).attr("rowindex");
        //                var thisy = $(this).attr("colindex");
        //                //确认当前单元格的坐标大于等于鼠标按下时的坐标并小于等于鼠标抬起时的坐标，
        //                if (thisx == x && thisy >= arrPos[0][1] && thisy <= y && 1 == key && e.target.tagName == "TD") {
        //                    $(this).addClass("selected");
        //                    $(this).attr("appoint", "true");
        //                }
        //            });
        //        }
        //        //列中选中


        //        arrPos = new Array();
        //        key = 0;
        //    });
        //})
        $("body").mouseup(function (e) {
            if (flag)
            {
                flag = false;
                arrPos = new Array();
                submitData();
            }
            
        
        })
        
    })
    function submitData() {
        var linum = $("input[id$=Hid_linum]").val();
        var para = '';
        var weekNum = $("input[id$=Hid_WeekNum]").val();
        var resid = '';
        $("#tabcar tr td").each(function () {
            var appoint = $(this).attr("appoint");
            if (appoint == "true") {
                var nowvalue = $("input[id$=Hid_AppointData]").val();
                var thisx = $(this).attr("rowindex");
                var thisy = $(this).attr("colindex");
                $("input[id$=Hid_AppointData]").val(nowvalue + "#" + thisx + "," + thisy);
                para += "B" + thisy + "A" + thisx;
                resid = $(this).parent().attr("itemid");
            }
        });
        if (para == '') {
            alert('你还没有预定！');
        }
        else {
            openPage('预定资源', '/SitePages/AddRoom.aspx?topItem=0&pagename=ResourceList&linum=' + linum + '&resid=' + resid + '&weekNum=' + weekNum + '&para=' + para, '700', '500');
        }

    }
</script>
<style type="text/css">
    .selected {
        background-color: #c2dfa5;
    }

    /*.ultit li {
        float: left;
        border: 0px;
        margin: 0 4px;
        line-height: 28px;
        font-size: 14px;
    }

    .dvtit img {
        width: 280px;
        height: 150px;
    }*/

    .zy_third {
        margin: 0 15px 0 15px;
    }

        .zy_third div {
            width: 45%;
            height: 143px;
            margin: 5px 7px 8px 7px;
            float: left;
            border: solid 1px #d9d9d9;
            box-shadow: 0 0 3px #d9d9d9;
            overflow: hidden;
        }

            .zy_third div img {
                width: 50%;
                height: 127px;
                margin: 7px 15px 5px 8px;
                float: left;
            }

            .zy_third div ul {
                width: 40%;
                height: 143px;
                float: left;
                overflow: hidden;
            }

                .zy_third div ul li {
                    margin-bottom: 10px;
                    border: 0px;
                }

                    .zy_third div ul li h2 {
                        height: 30px;
                        line-height: 30px;
                        margin: 0 37px 0 0;
                        font-size: 15px;
                        border-bottom: solid 1px #e6e6e6;
                    }
</style>
<div id="order" class="Confirmation_order">
    <!--页面名称-->
    <h1 class="Page_name">资源预定</h1>
    <!--整个展示区域-->
    <div class="Whole_display_area">
        <div class="Operation_area">
            <%-- <td>
                    <asp:Button ID="btnSure" runat="server" Text="确认订单" OnClick="btnSure_Click" /></td>--%>
            <asp:HiddenField ID="Hid_topItem" runat="server" Value="0" />
            <div class="left_choice fl">
                <ul>
                    <li class="">
                        <div class="qiehuan fl">
                            <span class="qijin">
                                <a href="#" class="Enable">公共资源</a>
                                <a href="#">资产管理</a>
                                <a href="#">专业教室</a>
                            </span>
                        </div>
                    </li>
                </ul>
            </div>
        </div>
        <div class="clear"></div>
        <div class="content">
            <div class="OrderManager_form">
                <div class="my_order">
                    <div class="Operation_area">
                        <div class="left_choice fl">
                            <ul>
                                <li class="option">
                                    <asp:DropDownList ID="DDL_Resource" AutoPostBack="true" runat="server" OnSelectedIndexChanged="DDL_Resource_SelectedIndexChanged">

                                        <asp:ListItem Value="1">车辆</asp:ListItem>
                                        <asp:ListItem Value="0">会议室</asp:ListItem>
                                    </asp:DropDownList></li>

                            </ul>
                        </div>
                        <asp:Panel ID="Pan_Car1" runat="server">
                            <div class="right_add fr">
                                <asp:LinkButton ID="LB_PreWeek" CssClass="add" OnClick="TB_PreWeek_Click" runat="server"><i class="iconfont">&#xe63b;</i>上一周</asp:LinkButton>
                                <asp:LinkButton ID="LB_NextWeek" CssClass="add" OnClick="TB_NextWeek_Click" runat="server"><i class="iconfont">&#xe61a;</i>下一周</asp:LinkButton>
                                <asp:LinkButton ID="LB_CurrentWeek" CssClass="add" OnClick="TB_CurrentWeek_Click" runat="server"><i class="iconfont">&#xe610;</i>当前周</asp:LinkButton>
                                <a class="add" href="#" onclick="submitData();"><i class="iconfont">&#xe642;</i>预定</a>


                                <asp:HiddenField ID="Hid_CurrentWeekCount" runat="server" />
                                <asp:HiddenField ID="Hid_ResId" runat="server" />
                                <asp:HiddenField ID="Hid_AppointData" runat="server" />
                                <asp:HiddenField ID="Hid_WeekNum" runat="server" />


                            </div>

                        </asp:Panel>
                    </div>
                    <asp:Panel ID="Pan_Car2" runat="server">
                        <div class="car_yudingform">
                            <div class="car_datecon">
                                <asp:HiddenField ID="Hid_linum" Value="0" runat="server" />
                                <asp:HiddenField ID="Hid_opera" Value="0" runat="server" />
                                <ul class="zyyd_day">
                                    <li>
                                        <asp:LinkButton ID="LB_Mon" CommandName="0" runat="server" OnClick="LB_Mon_Click"></asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="LB_Tue" CommandName="1" runat="server" OnClick="LB_Mon_Click"></asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="LB_Wed" CommandName="2" runat="server" OnClick="LB_Mon_Click"></asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="LB_Thu" CommandName="3" runat="server" OnClick="LB_Mon_Click"></asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="LB_Fri" CommandName="4" runat="server" OnClick="LB_Mon_Click"></asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="LB_Sat" CommandName="5" runat="server" OnClick="LB_Mon_Click"></asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="LB_Sun" CommandName="6" runat="server" OnClick="LB_Mon_Click"></asp:LinkButton></li>
                                </ul>

                            </div>

                            <div class="zyyd_table">
                                <asp:ListView ID="LV_Car" runat="server" OnPagePropertiesChanging="LV_Car_PagePropertiesChanging">
                                    <EmptyDataTemplate>
                                        <div>暂时没有数据</div>
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <table id="tabcar">
                                            <tr>
                                                <th colspan="2">车辆信息</th>
                                                <th>时间段一</th>
                                                <th>时间段二</th>
                                                <th>时间段三</th>
                                                <th>时间段四</th>
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server"></tr>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr itemid='<%#Eval("ID")%>'>
                                            <th class="car_tp">
                                                <img src='<%#Eval("imgSource")%>' alt=""></th>
                                            <th class="car_jianjie">
                                                <div><%#Eval("Place")%></div>
                                                <div><%#Eval("Area")%></div>
                                                <div><%#Eval("LimitCount")%></div>
                                            </th>
                                            <td isselect='<%#Eval("isselect")%>' style='color: white; vertical-align: middle; background-color: <%#Eval("BgColor1")%>'><%#Eval("Time1")%></td>
                                            <td isselect='<%#Eval("isselect")%>' style='color: white; vertical-align: middle; background-color: <%#Eval("BgColor2")%>'><%#Eval("Time2")%></td>
                                            <td isselect='<%#Eval("isselect")%>' style='color: white; vertical-align: middle; background-color: <%#Eval("BgColor3")%>'><%#Eval("Time3") %></td>
                                            <td isselect='<%#Eval("isselect")%>' style='color: white; vertical-align: middle; background-color: <%#Eval("BgColor4")%>'><%#Eval("Time4") %></td>
                                        </tr>
                                    </ItemTemplate>

                                </asp:ListView>

                            </div>
                            <div class="page">
                                <asp:DataPager ID="DP_Car" runat="server" PageSize="3" PagedControlID="LV_Car">
                                    <Fields>
                                        <asp:NextPreviousPagerField
                                            ButtonType="Link" ShowNextPageButton="False" ShowPreviousPageButton="true"
                                            ShowFirstPageButton="true" FirstPageText="首页" PreviousPageText="上一页" ButtonCssClass="pageup" />

                                        <asp:NumericPagerField CurrentPageLabelCssClass="number now" NumericButtonCssClass="number" />

                                        <asp:NextPreviousPagerField
                                            ButtonType="Link" ShowPreviousPageButton="False" ShowNextPageButton="true"
                                            ShowLastPageButton="true" LastPageText="末页" NextPageText="下一页" ButtonCssClass="pagedown" />

                                        <asp:TemplatePagerField>
                                            <PagerTemplate>
                                                <span class="count">| <%# Container.StartRowIndex / Container.PageSize + 1%> / 
                            <%# (Container.TotalRowCount % Container.MaximumRows) > 0 ? Convert.ToInt16(Container.TotalRowCount / Container.MaximumRows) + 1 : Container.TotalRowCount / Container.MaximumRows%>  页
                            (共<%# Container.TotalRowCount %>项)
                                                </span>
                                            </PagerTemplate>
                                        </asp:TemplatePagerField>
                                    </Fields>
                                </asp:DataPager>

                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="Pan_Classroom" runat="server">
                        <div class="zy_third">
                            <asp:ListView ID="LV_Classroom" runat="server" OnPagePropertiesChanging="LV_Classroom_PagePropertiesChanging">
                                <EmptyDataTemplate>
                                    <div>暂时没有数据</div>
                                </EmptyDataTemplate>
                                <ItemTemplate>
                                    <div>
                                        <a href='<%=ServerUrl %>/ResourceReservation/SitePages/ReserveMeetRoom.aspx?resid=<%#Eval("ID") %>'>
                                            <img src='<%#Eval("imgSource") %>' alt="" /></a>
                                        <ul>
                                            <li>
                                                <h2><%#Eval("Title") %></h2>
                                            </li>
                                            <li>地址：<%#Eval("Place") %></li>
                                            <li>面积：<%#Eval("Area") %></li>
                                            <li>开放时间：<%#Eval("OpenTime") %>~<%#Eval("CloseTime") %></li>
                                            <li>限定人数：<%#Eval("LimitCount") %>人</li>
                                        </ul>
                                    </div>
                                </ItemTemplate>
                            </asp:ListView>

                        </div>
                        <div class="page" style="clear: both;">
                            <asp:DataPager ID="DP_Classroom" runat="server" PageSize="6" PagedControlID="LV_Classroom">
                                <Fields>
                                    <asp:NextPreviousPagerField
                                        ButtonType="Link" ShowNextPageButton="False" ShowPreviousPageButton="true"
                                        ShowFirstPageButton="true" FirstPageText="首页" PreviousPageText="上一页" ButtonCssClass="pageup" />

                                    <asp:NumericPagerField CurrentPageLabelCssClass="number now" NumericButtonCssClass="number" />

                                    <asp:NextPreviousPagerField
                                        ButtonType="Link" ShowPreviousPageButton="False" ShowNextPageButton="true"
                                        ShowLastPageButton="true" LastPageText="末页" NextPageText="下一页" ButtonCssClass="pagedown" />

                                    <asp:TemplatePagerField>
                                        <PagerTemplate>
                                            <span class="count">| <%# Container.StartRowIndex / Container.PageSize + 1%> / 
                            <%# (Container.TotalRowCount % Container.MaximumRows) > 0 ? Convert.ToInt16(Container.TotalRowCount / Container.MaximumRows) + 1 : Container.TotalRowCount / Container.MaximumRows%>  页
                            (共<%# Container.TotalRowCount %>项)
                                            </span>
                                        </PagerTemplate>
                                    </asp:TemplatePagerField>
                                </Fields>
                            </asp:DataPager>

                        </div>
                    </asp:Panel>
                </div>

                <%--****************************资产管理页面开始*********************************************************--%>
                <div class="my_order" style="display: none;">
                    <div class="Display_form">
                        <div class="">
                            <div class="Whole_display_area">

                                <div class="clear"></div>
                                <div class="Schoolcon_wrap">

                                    <div class="right_dcon fr" style="width: 100%">
                                        <!--操作区域-->
                                        <div class="Operation_area">
                                            <%--<div class="left_choice fl">
                                                <ul>
                                                    <li class="Sear">
                                                        <input type="text" id="tb_search" class="search" />
                                                        <i class="iconfont" id="btnQuery" onclick="btnQuery()">&#xe62d;</i>
                                                    </li>
                                                </ul>
                                            </div>--%>
                                            <div class="right_add fr" id="opertion">
                                                <%--<a href="#" class="add" onclick="serchTime();"><i class="iconfont">&#xe62d;</i>搜索</a>--%>
                                                <div class="Batch_operation fl" id="plcz" style="display:none;">
                                                    <a href="#" class="add" onclick="openPage('添加资源','/SitePages/AddAsserts.aspx','700','500');"><i class="iconfont">&#xe612;</i>预定资产</a>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="clear"></div>
                                        <!--展示区域-->
                                        <div class="Display_form">
                                            <asp:ListView ID="LV_Assert" runat="server" OnPagePropertiesChanging="LV_Assert_PagePropertiesChanging" OnItemCommand="LV_Assert_ItemCommand" OnItemDataBound="LV_Assert_ItemDataBound">
                                                <EmptyDataTemplate>
                                                    <table>
                                                        <tr>
                                                            <td colspan="9" style="text-align: center">暂无资产！
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>
                                                <LayoutTemplate>
                                                    <table class="D_form">
                                                        <tr class="trth">
                                                            <!--表头tr名称-->
                                                            <th class="Account">编号
                                                            </th>
                                                            <th class="name">资产名称
                                                            </th>
                                                            <th class="Head">持有者
                                                            </th>
                                                            <th class="Contact">所属学校
                                                            </th>
                                                            <th class="Contact">部门
                                                            </th>
                                                            <th class="Contact">状态
                                                            </th>
                                                            <th class="Contact">登记时间
                                                            </th>
                                                            <th style="display:none;" class="Operation">操作
                                                            </th>
                                                        </tr>
                                                        <tr id="itemPlaceholder" runat="server"></tr>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr class="Single">
                                                        <td class="Account">
                                                            <%# Container.DataItemIndex + 1%>
                                                            
                                                        </td>
                                                        <td class="name">
                                                            <%#Eval("Title")%>
                                                        </td>
                                                        <td class="Account">
                                                            <%#Eval("Holder")%>
                                                        </td>
                                                        <td class="Account">
                                                            <%#Eval("BelongSchool")%>
                                                        </td>
                                                        <td class="Account"><%#Eval("Department")%></td>
                                                        <td class="Account">
                                                            <%#Eval("Status0")%>
                                                        </td>
                                                        <td class="Account">
                                                            <%#Eval("Created")%>
                                                        </td>
                                                        <td style="display:none;">
                                                            <asp:HiddenField ID="DetailID" runat="server" Value='<%# Eval("ID") %>' />


                                                            <asp:LinkButton ID="LB_Edit" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="Edit" OnClientClick="editpdetail(this,'修改资产','/SitePages/AddAsserts.aspx?itemid=','800','500');return false;"><i class="iconfont">&#xe629;</i></asp:LinkButton>


                                                            <asp:LinkButton ID="LB_Del" BorderStyle="None" CommandName="Del" CommandArgument='<%# Eval("ID") %>' runat="server" OnClientClick="return confirm('你确定删除吗？')" CssClass="btn"><i class="iconfont">&#xe64c;</i></asp:LinkButton>
                                                        </td>
                                                    </tr>

                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr class="Double">

                                                        <td class="Account">
                                                            <%# Container.DataItemIndex + 1%>
                                                            
                                                        </td>
                                                        <td class="name">
                                                            <%#Eval("Title")%>
                                                        </td>
                                                        <td class="Account">
                                                            <%#Eval("Holder")%>
                                                        </td>
                                                        <td class="Account">
                                                            <%#Eval("BelongSchool")%>
                                                        </td>
                                                        <td class="Account"><%#Eval("Department")%></td>
                                                        <td class="Account">
                                                            <%#Eval("Status0")%>
                                                        </td>
                                                        <td class="Account">
                                                            <%#Eval("Created")%>
                                                        </td>
                                                        <td style="display:none;">
                                                            <asp:LinkButton ID="LB_Edit" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="Edit" OnClientClick="editpdetail(this,'修改资产','/SitePages/AddAsserts.aspx?itemid=','800','500');return false;"><i class="iconfont">&#xe629;</i></asp:LinkButton>
                                                            <asp:LinkButton ID="LB_Del" BorderStyle="None" CommandName="Del" CommandArgument='<%# Eval("ID") %>' runat="server" OnClientClick="return confirm('你确定删除吗？')" CssClass="btn"><i class="iconfont">&#xe64c;</i></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                            </asp:ListView>
                                            <div class="page">
                                                <asp:DataPager ID="DP_Asserts" runat="server" PageSize="8" PagedControlID="LV_Assert">
                                                    <Fields>
                                                        <asp:NextPreviousPagerField
                                                            ButtonType="Link" ShowNextPageButton="False" ShowPreviousPageButton="true"
                                                            ShowFirstPageButton="true" FirstPageText="首页" PreviousPageText="上一页" ButtonCssClass="pageup" />

                                                        <asp:NumericPagerField CurrentPageLabelCssClass="number now" NumericButtonCssClass="number" />

                                                        <asp:NextPreviousPagerField
                                                            ButtonType="Link" ShowPreviousPageButton="False" ShowNextPageButton="true"
                                                            ShowLastPageButton="true" LastPageText="末页" NextPageText="下一页" ButtonCssClass="pagedown" />

                                                        <asp:TemplatePagerField>
                                                            <PagerTemplate>
                                                                <span class="count">| <%# Container.StartRowIndex / Container.PageSize + 1%> / 
                            <%# (Container.TotalRowCount % Container.MaximumRows) > 0 ? Convert.ToInt16(Container.TotalRowCount / Container.MaximumRows) + 1 : Container.TotalRowCount / Container.MaximumRows%>  页
                            (共<%# Container.TotalRowCount %>项)
                                                                </span>
                                                            </PagerTemplate>
                                                        </asp:TemplatePagerField>
                                                    </Fields>
                                                </asp:DataPager>

                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <%--****************************资产管理页面结束*********************************************************--%>
                <div class="my_order" style="display: none;">
                    <div class="zy_third">
                        <asp:ListView ID="LV_Room" runat="server" OnPagePropertiesChanging="LV_Room_PagePropertiesChanging">
                            <EmptyDataTemplate>
                                <div>暂时没有数据</div>
                            </EmptyDataTemplate>
                            <ItemTemplate>
                                <div>
                                    <a href='<%=ServerUrl %>/ResourceReservation/SitePages/ReserveRoom.aspx?resid=<%#Eval("ID") %>'>
                                        <img src='<%#Eval("imgSource") %>' alt="" /></a>
                                    <ul>
                                        <li>
                                            <h2><%#Eval("Title") %></h2>
                                        </li>
                                        <li>地址：<%#Eval("Address") %></li>
                                        <li>面积：<%#Eval("Area") %></li>
                                        <li>开放时间：<%#Eval("OpenTime") %>~<%#Eval("CloseTime") %></li>
                                        <li>限定人数：<%#Eval("LimitCount") %>人</li>
                                    </ul>
                                </div>
                            </ItemTemplate>
                        </asp:ListView>

                    </div>
                    <div class="page" style="clear: both;">
                        <asp:DataPager ID="DP_Room" runat="server" PageSize="6" PagedControlID="LV_Room">
                            <Fields>
                                <asp:NextPreviousPagerField
                                    ButtonType="Link" ShowNextPageButton="False" ShowPreviousPageButton="true"
                                    ShowFirstPageButton="true" FirstPageText="首页" PreviousPageText="上一页" ButtonCssClass="pageup" />

                                <asp:NumericPagerField CurrentPageLabelCssClass="number now" NumericButtonCssClass="number" />

                                <asp:NextPreviousPagerField
                                    ButtonType="Link" ShowPreviousPageButton="False" ShowNextPageButton="true"
                                    ShowLastPageButton="true" LastPageText="末页" NextPageText="下一页" ButtonCssClass="pagedown" />

                                <asp:TemplatePagerField>
                                    <PagerTemplate>
                                        <span class="count">| <%# Container.StartRowIndex / Container.PageSize + 1%> / 
                            <%# (Container.TotalRowCount % Container.MaximumRows) > 0 ? Convert.ToInt16(Container.TotalRowCount / Container.MaximumRows) + 1 : Container.TotalRowCount / Container.MaximumRows%>  页
                            (共<%# Container.TotalRowCount %>项)
                                        </span>
                                    </PagerTemplate>
                                </asp:TemplatePagerField>
                            </Fields>
                        </asp:DataPager>

                    </div>
                </div>
            </div>
        </div>
    </div>
</div>




