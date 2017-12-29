<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CO_wp_OrderInfoReportUserControl.ascx.cs" Inherits="SVDigitalCampus.Canteen_Ordering.CO_wp_OrderInfoReport.CO_wp_OrderInfoReportUserControl" %>
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/common.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/style.css" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/echarts.js"></script>
<div id="order" class="orderdiv">
    <div class="Confirmation_order">
        <!--页面名称-->
        <h1 class="Page_name">订单管理</h1>
        <!--整个展示区域-->
        <div class="Whole_display_area">
            <div class="Operation_area">
                <div class="left_choice fl">
                    <ul>
                        <li class="">
                            <div class="qiehuan fl">
                                <span class="qijin">
                                    <asp:LinkButton ID="btnToday" runat="server" OnClick="btnToday_Click" CssClass="Disable">今日订单</asp:LinkButton><asp:LinkButton ID="btnOld" runat="server" CssClass="Disable" OnClick="btnOld_Click">历史订单</asp:LinkButton><asp:LinkButton ID="btnAllOrder" runat="server" OnClick="btnAllOrder_Click" CssClass="Disable">所有订单</asp:LinkButton><asp:LinkButton ID="btnTotilByPic" runat="server" CssClass="Enable">图表统计</asp:LinkButton></span>
                            </div>
                        </li>
                    </ul>
                </div>
                <div class="right_add fr">
                </div>
                <div class="clear"></div>
            </div>
            <div id="test1" style="height: 400px;"></div>
            <div id="test2" style="height: 400px;"></div>
            <div id="test3" style="height: 400px;"></div>
            <div id="test4" style="height: 400px;"></div>
            <div id="test5" style="height: 400px;"></div>

        </div>
    </div>
</div>

<!--今日三餐金额统计-->

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
          var myChart = ec.init(document.getElementById('test1'));
          option = {
              title: {
                  text: '今日三餐统计',
                  x: 'center'
              },
              tooltip: {
                  trigger: 'item',
                  formatter: "{a} <br/>{b} : {c} ({d}%)"
              },
              legend: {
                  orient: 'vertical',
                  x: 'left',
                  data: ['早餐','午餐','晚餐']
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
                      name: '下单金额',
                      type: 'pie',
                      radius: '55%',
                      center: ['50%', '60%'],
                      data: [
                          { value:<%=MorningMoney%>, name: '早餐' },
                              { value:<%=LunchMoney%>, name: '午餐' },
                              { value:<%=DinnerMoney%>, name: '晚餐' }
                          ]
                    
                  }
              ]
          };

          myChart.setOption(option);
      }
  );
</script>

<!--今日早餐菜品金额统计-->
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
          <%if(!string.IsNullOrEmpty(MorningMenus)){%>
          // 基于准备好的dom，初始化echarts图表
          var myChart = ec.init(document.getElementById('test2'));
          var option = {
              title: {
                  text: '今日早餐菜品统计'
              },
              tooltip: {
                  trigger: 'axis'
              },
              legend: {
                  show: true,
                  data: ['下单金额']
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
                      data: <%="["+MorningMenus+"]"%>
                      }
              ],
              yAxis: [
                  {
                      type: 'value'
                  }
              ],
              series: [
                  {
                      name: '金额',
                      type: 'bar',
                      data: <%="["+MorningMenusMoney+"]"%>
                      }
              ]
          };

          myChart.setOption(option);
          <%}else{%>
          $("#test2").removeAttr("style");
          <%}%>
      }
  );
</script>

<!--今日午餐菜品金额统计-->
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
          <%if (!string.IsNullOrEmpty(LunchMenus))
            {%>
          // 基于准备好的dom，初始化echarts图表
          var myChart = ec.init(document.getElementById('test3'));
          var option = {
              title: {
                  text: '今日午餐菜品统计'
              },
              tooltip: {
                  trigger: 'axis'
              },
              legend: {
                  show: true,
                  data: ['下单金额']
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
                      data: <%="["+LunchMenus+"]"%>//[辣子鸡，白菜，面条,炒时蔬....]
                  }
              ],
             yAxis: [
                 {
                     type: 'value'
                 }
             ],
             series: [
                 {
                     name: '下单金额',
                     type: 'bar',
                     data: <%="["+LunchMenusMoney+"]"%>//[2,4,6,3,....]
                   
                  }
              ]
         };

          myChart.setOption(option);
          <%}else{%>
          $("#test3").removeAttr("style");
          <%}%>
      }
  );
</script>
<!--今日晚餐菜品金额统计-->
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
    
          <%if (!string.IsNullOrEmpty(DinnerMenus))
            {%>
          // 基于准备好的dom，初始化echarts图表
          var myChart = ec.init(document.getElementById('test4'));
          var  option = {
              title: {
                  text: '今日晚餐菜品统计'
              },
              tooltip: {
                  trigger: 'axis'
              },
              legend: {
                  show: true,
                  data: ['下单金额']
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
                      data: <%="["+DinnerMenus+"]"%>//[辣子鸡，白菜，面条,炒时蔬....]
                  }
              ],
            yAxis: [
                {
                    type: 'value'
                }
            ],
            series: [
                {
                    name: '下单金额',
                    type: 'bar',
                    data: <%="["+DinnerMenusMoney+"]"%>//[2,4,6,3,....]
                     
                  }
              ]
        };

          myChart.setOption(option);
          <%}else{%>
          $("#test4").removeAttr("style");
          <%}%>
      }
  );
</script>

<!--本周订单金额统计-->
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
    var myChart = ec.init(document.getElementById('test5'));
    var option = {
        title: {
            text: '本周下单情况'
        },
        tooltip: {
            trigger: 'axis'
        },
        legend: {
            data: ['下单金额']
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
                data: ['周一', '周二', '周三', '周四', '周五']
            }
        ],
        yAxis: [
            {
                type: 'value'
            }
        ],
        series: [
            {
                name: '最高金额',
                type: 'line',
                data: <%="["+WeekdayMoney+"]"%>,
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
