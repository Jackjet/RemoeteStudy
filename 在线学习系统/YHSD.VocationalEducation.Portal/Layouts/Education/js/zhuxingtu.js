
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
                "subCaption": "本站注册总人数",
                "xAxisName": "月份",
                "yAxisName": "人数",
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
                    "label": "1月",
                    "value": "42"
                },
                {
                    "label": "2月",
                    "value": "81"
                },
                {
                    "label": "3月",
                    "value": "72"
                },
                {
                    "label": "4月",
                    "value": "55"
                },
                {
                    "label": "5月",
                    "value": "91"
                },
                {
                    "label": "6月",
                    "value": "51"
                },
                {
                    "label": "7月",
                    "value": "68"
                },
                {
                    "label": "8月",
                    "value": "62"
                },
                {
                    "label": "9月",
                    "value": "61"
                },
                {
                    "label": "10月",
                    "value": "49"
                },
                {
                    "label": "11月",
                    "value": "90"
                },
                {
                    "label": "12月",
                    "value": "73"
                }
            ]
        }
    });
    revenueChart.render();
});
}//]]> 