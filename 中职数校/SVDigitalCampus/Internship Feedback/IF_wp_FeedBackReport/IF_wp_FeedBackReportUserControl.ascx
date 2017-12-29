<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IF_wp_FeedBackReportUserControl.ascx.cs" Inherits="SVDigitalCampus.Internship_Feedback.IF_wp_FeedBackReport.IF_wp_FeedBackReportUserControl" %>
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/common.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" rel="stylesheet" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/echarts.js"></script>
<div class="Enterprise_information_feedback">
    <h1 class="Page_name">反馈信息管理</h1>

    <div class="Whole_display_area">
        <div class="Operation_area">

            <div class="left_choice fl">
                <ul>
                    <li class="">
                        <div class="qiehuan fl">
                            <span class="qijin"><a class="Disable" href='<%=rootUrl %>/SitePages/FeedList.aspx'>反馈详情</a>
                                <a class="Enable" href="#">图表统计</a></span>
                        </div>
                    </li>
                </ul>
            </div>

        </div>
        <div id="test1" style="height: 400px;"></div>
        <div id="test2" style="height: 400px;"></div>

    </div>
</div>



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
                  text: '学生分配报表',
                  //subtext: '测试数据',
                  x: 'center'
              },
              tooltip: {
                  trigger: 'item',
                  formatter: "{a} <br/>{b} : {c} ({d}%)"
              },
              legend: {
                  orient: 'vertical',
                  x: 'left',
                  data: ['已分配', '未分配','已评价']
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
              calculable: true,
              series: [
                  {
                      name: '评价比重',
                      type: 'pie',
                      radius: '55%',
                      center: ['50%', '60%'],
                      data: [
                          { value:<%=FeededNum%>, name: '已分配' },
                          { value:<%=FeedingNum%>, name: '未分配' },
                           { value:<%=endNum%>, name: '已评价' },
                      ]
                  }
              ]
          };

          myChart.setOption(option);
      }
  );
</script>

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
    var myChart = ec.init(document.getElementById('test2'));
    var option = {
        //标题，每个图表最多仅有一个标题控件，每个标题控件可设主副标题  
        title: {
            //主标题文本，'\n'指定换行  
            text: '企业评价情况',
            //主标题文本超链接  
            //link: 'http://www.tqyb.com.cn/weatherLive/climateForecast/2014-01-26/157.html',
            //副标题文本，'\n'指定换行  
            //subtext: 'www.stepday.com',
            //副标题文本超链接  
            //sublink: 'http://www.stepday.com/myblog/?Echarts',
            //水平安放位置，默认为左侧，可选为：'center' | 'left' | 'right' | {number}（x坐标，单位px）  
            x: 'center',
            //垂直安放位置，默认为全图顶端，可选为：'top' | 'bottom' | 'center' | {number}（y坐标，单位px）  
            y: 'top'
        },
        //提示框，鼠标悬浮交互时的信息提示  
        tooltip: {
            //触发类型，默认（'item'）数据触发，可选为：'item' | 'axis'  
            trigger: 'axis'
        },
        //图例，每个图表最多仅有一个图例  
        legend: {
            orient: 'vertical',
            //显示策略，可选为：true（显示） | false（隐藏），默认值为true  
            show: true,
            //水平安放位置，默认为全图居中，可选为：'center' | 'left' | 'right' | {number}（x坐标，单位px）  
            x: 'left',
            //垂直安放位置，默认为全图顶端，可选为：'top' | 'bottom' | 'center' | {number}（y坐标，单位px）  
            y: 'top',
            //legend的data: 用于设置图例，data内的字符串数组需要与sereis数组内每一个series的name值对应  
            data: ['分配学生数量', '评价学生数量']
        },
        //工具箱，每个图表最多仅有一个工具箱  
        toolbox: {
            //显示策略，可选为：true（显示） | false（隐藏），默认值为false  
            show: true,
            //启用功能，目前支持feature，工具箱自定义功能回调处理  
            feature: {
                //辅助线标志  
                mark: { show: true },
                //dataZoom，框选区域缩放，自动与存在的dataZoom控件同步，分别是启用，缩放后退  
                dataZoom: {
                    show: true,
                    title: {
                        dataZoom: '区域缩放',
                        dataZoomReset: '区域缩放后退'
                    }
                },
                //数据视图，打开数据视图，可设置更多属性,readOnly 默认数据视图为只读(即值为true)，可指定readOnly为false打开编辑功能  
                dataView: { show: true, readOnly: true },
                //magicType，动态类型切换，支持直角系下的折线图、柱状图、堆积、平铺转换  
                magicType: { show: true, type: ['line', 'bar'] },
                //restore，还原，复位原始图表  
                restore: { show: true },
                //saveAsImage，保存图片（IE8-不支持）,图片类型默认为'png'  
                saveAsImage: { show: true }
            }
        },
        //是否启用拖拽重计算特性，默认关闭(即值为false)  
        calculable: true,
        //直角坐标系中横轴数组，数组中每一项代表一条横轴坐标轴，仅有一条时可省略数值  
        //横轴通常为类目型，但条形图时则横轴为数值型，散点图时则横纵均为数值型  
        xAxis: [
            {
                //显示策略，可选为：true（显示） | false（隐藏），默认值为true  
                show: true,
                //坐标轴类型，横轴默认为类目型'category'  
                type: 'category',
                //类目型坐标轴文本标签数组，指定label内容。 数组项通常为文本，'\n'指定换行  
                data:<%=Enter%> //['公司1', '公司2', '公司3', '公司4', '公司5', '公司6', '公司7', '公司8']
                }
        ],
        //直角坐标系中纵轴数组，数组中每一项代表一条纵轴坐标轴，仅有一条时可省略数值  
        //纵轴通常为数值型，但条形图时则纵轴为类目型  
        yAxis: [
            {
                //显示策略，可选为：true（显示） | false（隐藏），默认值为true  
                show: true,
                //坐标轴类型，纵轴默认为数值型'value'  
                type: 'value',
                //分隔区域，默认不显示  
                splitArea: { show: true }
            }
        ],

        //sereis的数据: 用于设置图表数据之用。series是一个对象嵌套的结构；对象内包含对象  
        series: [
            {
                //系列名称，如果启用legend，该值将被legend.data索引相关  
                name: '评价学生数量',
                //图表类型，必要参数！如为空或不支持类型，则该系列数据不被显示。  
                type: 'bar',
                //系列中的数据内容数组，折线图以及柱状图时数组长度等于所使用类目轴文本标签数组axis.data的长度，并且他们间是一一对应的。数组项通常为数值  
                data:<%=EnterY%>,// [2.0, 4.9, 7.0, 23.2, 25.6, 76.7, 135.6, 162.2],
                //系列中的数据标注内容  
                markPoint: {
                    data: [
                        { type: 'max', name: '最大值' },
          { type: 'min', name: '最小值' }
                    ]
                },
                //系列中的数据标线内容  
                markLine: {
                    data: [
                        { type: 'average', name: '平均值' }
                    ]
                }
            },
            {
                //系列名称，如果启用legend，该值将被legend.data索引相关  
                name: '待评价学生数量',
                //图表类型，必要参数！如为空或不支持类型，则该系列数据不被显示。  
                type: 'bar',
                //系列中的数据内容数组，折线图以及柱状图时数组长度等于所使用类目轴文本标签数组axis.data的长度，并且他们间是一一对应的。数组项通常为数值  
                data: <%=EnterN%>,
                //系列中的数据标注内容  
                markPoint: {
                    data: [
                        { type: 'max', name: '最大值' },
                { type: 'min', name: '最小值' }
                    ]
                },
                //系列中的数据标线内容  
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
