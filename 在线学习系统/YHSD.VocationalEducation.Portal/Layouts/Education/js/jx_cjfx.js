
window.onload=function(){
FusionCharts.ready(function () {
    var revenueChart = new FusionCharts({
        type: 'column3d',
        renderAt: 'chart-container',
        width: '500',
        height: '300',
        dataFormat: 'json',
        dataSource: {
            "chart": {
                "caption": "2015年期中考试",
                "subCaption": "成绩分析",
                "xAxisName": "姓名",
                "yAxisName": "成绩",
                "paletteColors": "#0075c2",
                "valueFontColor": "#ffffff",
                "baseFont": "Helvetica Neue,Arial",
                "captionFontSize": "14",
                "subcaptionFontSize": "14",
                "subcaptionFontBold": "0",
                "placeValuesInside": "1",
                "rotateValues": "1",
                "showShadow": "0",
                "divlineColor": "#999999",               
                "divLineIsDashed": "1",
                "divlineThickness": "1",
                "divLineDashLen": "1",
                "divLineGapLen": "1",
                "canvasBgColor": "#ffffff"
            },

            "data": [
                {
                    "label": "柳依依",
                    "value": "92"
                },
                {
                    "label": "李善武",
                    "value": "81"
                },
                {
                    "label": "王小明",
                    "value": "72"
                },
                {
                    "label": "王小明",
                    "value": "55"
                },
                {
                    "label": "柳依依",
                    "value": "91"
                },
                {
                    "label": "王刘",
                    "value": "51"
                },
                {
                    "label": "田珊珊",
                    "value": "68"
                },
                {
                    "label": "柳依依",
                    "value": "62"
                },
                {
                    "label": "天一",
                    "value": "61"
                },
                {
                    "label": "欧阳散",
                    "value": "49"
                },
                {
                    "label": "田珊珊",
                    "value": "90"
                },
				{
                    "label": "柳依依",
                    "value": "62"
                },
                {
                    "label": "天一",
                    "value": "61"
                },
                {
                    "label": "欧阳散",
                    "value": "49"
                },
				{
                    "label": "天一",
                    "value": "61"
                },
                {
                    "label": "欧阳散",
                    "value": "49"
                },
                {
                    "label": "田珊珊",
                    "value": "90"
                },
                {
                    "label": "田珊珊",
                    "value": "90"
                }
            ]
        }
    });
    revenueChart.render();
});
}//]]> 