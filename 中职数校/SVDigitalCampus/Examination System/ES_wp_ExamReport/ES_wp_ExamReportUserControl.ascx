<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ES_wp_ExamReportUserControl.ascx.cs" Inherits="SVDigitalCampus.Examination_System.ES_wp_ExamReport.ES_wp_ExamReportUserControl" %>
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/common.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/style.css" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/echarts.js"></script>
<div id="order" class="orderdiv">
    <div class="Confirmation_order">
        <!--页面名称-->
        <h1 class="Page_name">考试管理</h1>
        <!--整个展示区域-->
        <div class="Whole_display_area">
            <div class="Operation_area">
                <div class="left_choice fl">
                    <ul>
                        <li class="">
                            <div class="qiehuan fl"><span class="qijin">
                                <asp:LinkButton ID="btnExamAnalysis" runat="server" OnClick="btnExamAnalysis_Click"
                                    CssClass="Disable" >成绩统计</asp:LinkButton>
                                <asp:LinkButton ID="btnReport" runat="server"  CssClass="Enable">统计分析</asp:LinkButton></span></div>
                        </li>
                    </ul>
                </div>
                <div class="right_add fr">
                </div>
                <div class="clear"></div>
            </div>
            <div class="S_conditions">
                <div id="selectList" class="screenBox screenBackground">
                    <dl class="listIndex dlHeight" attr="terminal_brand_s">
                        <dt>专业</dt>
                        <dd>
                            <asp:ListView ID="lvMajor" runat="server" ItemPlaceholderID="itemPlaceholder" OnItemCommand="lvMajor_ItemCommand">
                                <LayoutTemplate>
                                    <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <label>
                                        <asp:LinkButton ID="majoritem" runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="showSubject"><%#Eval("Title") %></asp:LinkButton>
                                    </label>
                                </ItemTemplate>
                            </asp:ListView>
                            <span class="more">展开</span>
                        </dd>
                    </dl>
                    <dl class="listIndex" attr="terminal_brand_s" id="subjectdl" runat="server">
                        <dt>学科</dt>
                        <dd>
                            <asp:ListView ID="lvSubject" runat="server" ItemPlaceholderID="subitemPlaceHolder" OnItemCommand="lvSubject_ItemCommand">
                                <LayoutTemplate>
                                    <asp:PlaceHolder ID="subitemPlaceHolder" runat="server"></asp:PlaceHolder>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <label>
                                        <asp:LinkButton ID="SubjectItem" runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="SubjectSearch"><%#Eval("Title") %></asp:LinkButton></label>
                                </ItemTemplate>
                            </asp:ListView>
                        </dd>
                    </dl>
                </div>
            </div>
            <div class="clear"></div>
            <!--操作区域-->
            <div class="Operation_area">
                <div class="left_choice fl">
                    <ul>
                        <li class="Select">
                            <asp:DropDownList ID="ddlClass" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged">
                                <asp:ListItem Text="班级" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li class="Select">
                            <asp:DropDownList ID="ddlExam" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlExam_SelectedIndexChanged">
                                <asp:ListItem Text="试卷" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="clear"></div>
            <div id="JoinReport" style="height: 400px;"></div>
            <div id="NeverJoinReport" class="Display_form">
                <h1 class="Report_name">未参加答题人员名单</h1>
                <asp:ListView ID="lvNeverJoinStu" runat="server" OnPagePropertiesChanging="lvNeverJoinStu_PagePropertiesChanging">
                    <EmptyDataTemplate>
                        <table class="D_form">
                            <tr class="trth">
                                <!--表头tr名称-->
                                <th class="Account">编号
                                </th>
                                <th class="name">姓名
                                </th>
                                <th class="Head">性别
                                </th>
                                <th class="Contact">专业
                                </th>
                                <th class="Contact">班级
                                </th>
                            </tr>
                            <tr>
                                <td colspan="7" style="text-align: center">暂无未参加答题人员记录！
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
                                <th class="name">姓名
                                </th>
                                <th class="Head">性别
                                </th>
                                <th class="Contact">专业
                                </th>
                                <th class="Contact">班级
                                </th>
                            </tr>
                            <tr id="itemPlaceholder" runat="server"></tr>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr class="Single">
                            <td class="Count">
                                <%#Eval("Count")%>
                            </td>
                            <td class="Title">
                                <%#Eval("Name")%>
                            </td>
                            <td class="Type">
                                <%#Eval("Sex")%>
                            </td>
                            <td class="Difficulty">
                                <%#Eval("Major")%>
                            </td>
                            <td class="Author"><%#Eval("Class")%></td>
                        </tr>

                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="Double">

                            <td class="Count">
                                <%#Eval("Count")%>
                            </td>
                            <td class="Title">
                                <%#Eval("Name")%>
                            </td>
                                <td class="Type">
                                <%#Eval("Sex")%>
                            </td>
                            <td class="Difficulty">
                                <%#Eval("Major")%>
                            </td>
                            <td class="Author"><%#Eval("Class")%></td>
                           
                        </tr>
                    </AlternatingItemTemplate>
                </asp:ListView>
                <div class="page">
                    <asp:DataPager ID="DPNeverJoinStu" runat="server" PageSize="8" PagedControlID="lvNeverJoinStu">
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
            <div id="ScoreReport" style="height: 400px;"></div>
            <div id="ErrorReport" style="height: 400px;"></div>
            <div id="test5" style="height: 400px;"></div>

        </div>
    </div>
</div>

<!--试卷参加答题人情况统计-->

<script type="text/javascript">
    require.config({
        paths: {
            echarts: '../../../../_layouts/15/SVDigitalCampus/Script'
        }
    });
    require(
      [
          'echarts',
          'echarts/chart/bar', // 使用柱状图就加载bar模块，按需加载
          'echarts/chart/pie'
      ],
      function (ec) {
          // 基于准备好的dom，初始化echarts图表
          var myChart = ec.init(document.getElementById('JoinReport'));
          option = {
              title: {
                  text: '参加考试人数统计',
                  x: 'left'
              },
              tooltip: {
                  trigger: 'item',
                  formatter: "{a} <br/>{b} : {c} ({d}%)"
              },
              legend: {
                  orient: 'vertical',
                  x: 'center',
                  data: ['已参加','未参加']
              },

              toolbox: {
                  show: true,
                  feature: {
                      mark: { show: true },
                      dataView: { show: true, readOnly: false },
                      magicType: {
                          show: true,
                          type: ['pie', 'funnel'],
                          option: {
                              funnel: {
                                  x: '25%',
                                  width: '50%',
                                  funnelAlign: 'left',
                                  max: 1548
                              }
                          }
                      },
                      restore: { show: true },
                      saveAsImage: { show: true }
                  }
              },
              //是否启用拖拽重计算特性，默认关闭(即值为false)  
              calculable: true,
              series: [
                  {
                      name: '人数统计',
                      type: 'pie',
                      radius: '55%',
                      center: ['50%', '60%'],
                      data: [
                          { value:<%=JoinNum%>, name: '参加人数' },
                              { value:<%=NeverJoinNum%>, name: '未参加人数' }
                             
                      ]
                    
                  }
              ]
          };

          myChart.setOption(option);
      }
  );
</script>

<!--成绩分布统计-->
<script type="text/javascript">
    require.config({
        paths: {
            echarts: '../../../../_layouts/15/SVDigitalCampus/Script'
        }
    });
    require(
      [
          'echarts',
          'echarts/chart/bar', // 使用柱状图就加载bar模块，按需加载
          'echarts/chart/pie'
      ],
      function (ec) {
       <%--   <%if (!string.IsNullOrEmpty(MorningMenus))
            {%>--%>
          // 基于准备好的dom，初始化echarts图表
          var myChart = ec.init(document.getElementById('ScoreReport'));
          var option = {
              title: {
                  text: '成绩分布'
              },
              tooltip: {
                  trigger: 'axis'
              },
              legend: {
                  show: true,
                  data: ['人数']
              },
              toolbox: {
                  show: true,
                  feature : {
                      mark : {show: true},
                      dataView : {show: true, readOnly: false},
                      magicType : {show: true, type: ['line', 'bar']},
                      restore : {show: true},
                      saveAsImage : {show: true}
                  }
              },
              calculable: true,
              xAxis: [
                  {
                      type: 'category',
                      data: <%="["+FullSocre+"]"%>
                      }
              ],
              yAxis: [
                  {
                      type: 'value'
                  }
              ],
              series: [
                  {
                      name: '人数',
                      type: 'bar',
                      data: <%="["+socrenum+"]"%>
                      }
              ]
          };

          myChart.setOption(option);
        <%--  <%}
            else
            {%>
          $("#test2").removeAttr("style");
          <%}%>--%>
      }
  );
</script>


<!--试卷错题分析统计-->
<script type="text/javascript">
    require.config({
        paths: {
            echarts: '../../../../_layouts/15/SVDigitalCampus/Script'
        }
    });
    require([
            //这里的'echarts'相当于'./js'  
            'echarts',
            'echarts/chart/bar',
            'echarts/chart/pie',
            'echarts/chart/line'
    ],
function (ec) {
    // 基于准备好的dom，初始化echarts图表
    var myChart = ec.init(document.getElementById('ErrorReport'));
    var option = {
        title: {
            text: '错题分析'
        },
        tooltip: {
            trigger: 'axis'
        },
        legend: {
            data: ['错题数']
        },
        toolbox: {
            show: true,
            feature: {
                mark: { show: true },
                dataView: { show: true, readOnly: false },
                magicType: { show: true, type: ['line', 'bar'] },
                restore: { show: true },
                saveAsImage: { show: true }
            }
        },
        calculable: true,
        xAxis: [
            {
                type: 'category',
                boundaryGap: false,
                data: <%="["+QType+"]"%>
            }
        ],
        yAxis: [
            {
                type: 'value'
            }
        ],
        series: [
            {
                name: '错误数',
                type: 'line',
                data: <%="["+ErrorNum+"]"%>,
                markPoint: {
                    data: [
                        { type: 'max', name: '最大值' },
                        { type: 'min', name: '最小值' }
                    ]
                },
                markLine: {
                    data: [
                        { type: 'average', name: '平均值' }
                    ]
                }
            }
        ]
    };


    myChart.setOption(option);
});

</script>
