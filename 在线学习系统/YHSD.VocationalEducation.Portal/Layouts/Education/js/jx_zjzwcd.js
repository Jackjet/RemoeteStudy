
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
                "subCaption": "章节掌握程度",
                "xAxisName": "姓名",
                "yAxisName": "掌握程度",
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
                    "value": "85%"
                },
                {
                    "label": "李善武",
                    "value": "70%"
                },
                {
                    "label": "王小明",
                    "value": "96%"
                },
                {
                    "label": "柳依依",
                    "value": "76%"
                },
                {
                    "label": "柳依依",
                    "value": "85%"
                },
                {
                    "label": "李善武",
                    "value": "70%"
                },
                {
                    "label": "王小明",
                    "value": "96%"
                },
                {
                    "label": "柳依依",
                    "value": "86%"
                },
                 {
                     "label": "柳依依",
                     "value": "76%"
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