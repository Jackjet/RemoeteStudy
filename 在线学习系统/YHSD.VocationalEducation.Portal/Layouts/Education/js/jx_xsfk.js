
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
                "caption": "2015年",
                "subCaption": "学生反馈",
                "xAxisName": "姓名",
                "yAxisName": "反馈信息统计",
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
                    "label": "李潇洒",
                    "value": "80%"
                },
                {
                    "label": "钱晓晓",
                    "value": "60%"
                },
                {
                    "label": "王小明",
                    "value": "80%"
                },
                {
                    "label": "李潇洒",
                    "value": "80%"
                },
                {
                    "label": "钱晓晓",
                    "value": "65%"
                },
                {
                    "label": "王小明",
                    "value": "90%"
                },
                {
                    "label": "李潇洒",
                    "value": "91%"
                },
                {
                    "label": "钱晓晓",
                    "value": "75%"
                },
                {
                    "label": "王小明",
                    "value": "87%"
                }

            ]
        }
    });
    revenueChart.render();
});
}//]]> 