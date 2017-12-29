<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginFrequency.aspx.cs" Inherits="Moblie.AccessRate.LoginFrequency" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
   <!--Favicon shortcut link-->

    <link href="../images/splash/favicon.ico" rel="shortcut icon" type="image/x-icon">

    <link href="../images/splash/favicon.ico" rel="icon" type="image/x-icon">

    <!--Declare page as mobile friendly -->

    <meta name="viewport" content="user-scalable=no, initial-scale=1.0, maximum-scale=1.0">

    <!-- Declare page as iDevice WebApp friendly -->

    <meta name="apple-mobile-web-app-capable" content="yes">

    <!-- iDevice WebApp Splash Screen, Regular Icon, iPhone, iPad, iPod Retina Icons -->

    <link href="../images/splash-icon.png" rel="apple-touch-icon" sizes="114x114">

    <link href="../images/splash-screen.png" rel="apple-touch-startup-image" media="screen and (max-device-width: 320px)">

    <link href="../images/splash-screen@2x.png" rel="apple-touch-startup-image" media="(max-device-width: 480px) and (-webkit-min-device-pixel-ratio: 2)">

    <link href="../images/splash-screen@3x.png" rel="apple-touch-startup-image" sizes="640x1096">



    <!-- Page Title -->

    <title>登陆频率</title>

    <!-- Stylesheet Load -->

    <link href="/css/style.css" rel="stylesheet" type="text/css">

    <link href="../css/framework-style.css" rel="stylesheet" type="text/css">

    <link href="../css/framework.css" rel="stylesheet" type="text/css">

    <link href="../css/icons.css" rel="stylesheet" type="text/css">

    <link href="../css/retina.css" rel="stylesheet" type="text/css" media="only screen and (-webkit-min-device-pixel-ratio: 2)">



    <!--Page Scripts Load -->

    <script src="../js/jquery.min.js" type="text/javascript"></script>

    <script src="../js/jquery-ui-min.js" type="text/javascript"></script>

    <script src="../js/colorbox.js" type="text/javascript"></script>

    <script src="../js/hammer.js" type="text/javascript"></script>

    <script src="../js/subscribe.js" type="text/javascript"></script>

    <script src="../js/contact.js" type="text/javascript"></script>

    <script src="../js/swipe.js" type="text/javascript"></script>

    <script src="../js/swipebox.js" type="text/javascript"></script>

    <script src="../js/retina.js" type="text/javascript"></script>

    <script src="../js/custom.js" type="text/javascript"></script>

    <script src="../js/framework.js" type="text/javascript"></script>

</head>
<body>

    <div id="preloader" style="display: none;">

        <div id="status" style="display: none;">

            <p class="center-text">
                加载中…...

           

                <em>加载速度取决于你的网速快慢!</em>

            </p>

        </div>

    </div>

    <div class="header">

        <a class="deploy-left-sidebar" href="#"></a>

        <a class="deploy-right-sidebar" href="#"></a>

        <a class="top-logo" href="#">
            <img width="125" class="replace-2x" alt="img" src="/images/logo.png"></a>

    </div>

    <div class="content-box">

        <div class="content">

            
            <div id="divPieChart">
            </div>
            <script type="text/javascript" src="../js/FusionChart/FusionCharts.js"></script>
            <script type="text/javascript">

                //画图 (以指定 xml格式字符串为数据源)
                function DrawChart(divId, flashFileName, width, height) {
                    var myChartId = new Date().getTime();
                    var myChart = new FusionCharts("../js/FusionChart/" + flashFileName, myChartId, width, height);
                    myChart.setDataXML('<chart caption="登录频率" numberPrefix=""><set value="25" label="9月" color="AFD8F8" /><set value="17" label="10月" color="F6BD0F" /><set value="23" label="11月" color="8BBA00" isSliced="1" /></chart>');
                    myChart.addParam("wmode", "Opaque");
                    myChart.render(divId);
                }

                DrawChart("divPieChart", "Column3D.swf", "100%", "250");

            </script>
            <div class="decoration"></div>
            <p class="center-text uppercase footer-text">
                Powered by <a href="" target="_blank"></a>Copyright © 2015 - 2016
            </p>
        </div>

    </div>


    <div id="cboxOverlay" style="display: none;"></div>
    <div id="colorbox" style="display: none;">
        <div id="cboxWrapper">
            <div>
                <div id="cboxTopLeft" style="float: left;"></div>
                <div id="cboxTopCenter" style="float: left;"></div>
                <div id="cboxTopRight" style="float: left;"></div>
            </div>
            <div style="clear: left;">
                <div id="cboxMiddleLeft" style="float: left;"></div>
                <div id="cboxContent" style="float: left;">
                    <div id="cboxTitle" style="float: left;"></div>
                    <div id="cboxCurrent" style="float: left;"></div>
                    <div id="cboxNext" style="float: left;"></div>
                    <div id="cboxPrevious" style="float: left;"></div>
                    <div id="cboxSlideshow" style="float: left;"></div>
                    <div id="cboxClose" style="float: left;"></div>
                </div>
                <div id="cboxMiddleRight" style="float: left;"></div>
            </div>
            <div style="clear: left;">
                <div id="cboxBottomLeft" style="float: left;"></div>
                <div id="cboxBottomCenter" style="float: left;"></div>
                <div id="cboxBottomRight" style="float: left;"></div>
            </div>
        </div>
        <div style="width: 9999px; display: none; visibility: hidden; position: absolute;"></div>
    </div>
</body>
</html>
















