<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RR_wp_AllRoomUserControl.ascx.cs" Inherits="SVDigitalCampus.ResourceReservation.RR_wp_AllRoom.RR_wp_AllRoomUserControl" %>
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
<style type="text/css">
    .selected {
        background-color: #666;
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
    <%--<h1 class="Page_name">资源预定</h1>--%>
    <div class="kc_yylc_sp">
        <div class="kc_yylc_spxian"></div>
        <div class="kc_yylc_spbiao">
            <table>
                <tbody>
                    <tr>

                        <td  class="kc_yylc_sptd"><a onclick="Return()" class="kc_yylc_spzt kc_yylc_spon"><<返回</a></td>
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
    <!--整个展示区域-->
    <div class="Whole_display_area">

        <div class="clear"></div>
        <div class="content">
            <div class="OrderManager_form">
                <div class="my_order">
                    <div class="zy_third">
                        <asp:ListView ID="LV_Room" runat="server" OnPagePropertiesChanging="LV_Room_PagePropertiesChanging">
                            <EmptyDataTemplate>
                                <div>暂时没有数据</div>
                            </EmptyDataTemplate>
                            <ItemTemplate>
                                <div>
                                    <a href='<%#Eval("Url") %>'>
                                        <img src='<%#Eval("imgSource") %>' alt="" /></a>

                                    <%--<a href='<%=ServerUrl %>/ResourceReservation/SitePages/ReserveRoom.aspx?resid=<%#Eval("ID") %>&WeekID=<%=WeekID %>&Type=<%=TypeFlag %>&ClassID=<%=ClassID %>'>
                                       </a>--%>
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
