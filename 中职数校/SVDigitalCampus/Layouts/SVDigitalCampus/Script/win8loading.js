﻿// 圆点元素类（js中没有类的概念，这里模拟而已）
function ProgressBarWin8() {
    // 圆心坐标
    this.fixed = {
        left: 0,
        top: 0
    };
    // html标签元素坐标
    this.position = {
        left: 0,
        top: 0
    };
    this.radius = 30; // 圆半径
    this.angle = 270; // 角度,默认270
    this.delay = 30; // 定时器延迟毫秒
    this.timer = null; // 定时器时间对象
    this.dom = null; // html标签元素
    this.divdom = null;
    // html标签元素样式， position需设置成absolute
    this.style = {
        position: "absolute",
        width: "10px",
        height: "10px",
        background: "#5493D8",
        "border-radius": "5px"
    };
    
}
// js中每个函数都有个prototype对象属性，每个实例都可以访问
ProgressBarWin8.prototype = {
    // 运行方法
    run: function () {
        if (this.timer) {
            clearTimeout(this.timer);
        }

        // 设置html标签元素坐标，即计算圆上的点x,y轴坐标
        this.position.left = Math.cos(Math.PI * this.angle / 180) * this.radius + this.fixed.left;
        this.position.top = Math.sin(Math.PI * this.angle / 180) * this.radius + this.fixed.top;
        this.dom.style.left = this.position.left + "px";
        this.dom.style.top =this.position.top + "px";

        // 改变角度
        this.angle++;

        // 判断元素x与圆心x坐标大小，设置定时器延迟时间
        if (this.position.left < this.fixed.left) {
            this.delay += .5;
        } else {
            this.delay -= .5;
        }

        var scope = this;
        // 定时器，循环调用run方法，有点递归的感觉
        this.timer = setTimeout(function () {
            // js中函数的调用this指向调用者，当前this是window
            scope.run();
        }, this.delay);
    },
    // html标签元素初始设置
    defaultSetting: function () {
        // 创建一个span元素
        this.dom = document.createElement("span");
        // 设置span元素的样式，js中对象的遍历是属性
        for (var property in this.style) {
            // js中对象方法可以用.操作符，也可以通过键值对的方式
            this.dom.style[property] = this.style[property];
        }
        // innerWidth innerHeight窗口中文档显示区域的宽度，不包括边框和滚动条,该属性可读可写。
        // 设置圆心x,y轴坐标，当前可视区域的一般，即中心点
        this.fixed.left = window.innerWidth / 2;
        this.fixed.top = window.innerHeight / 2;
        // 设置span元素的初始坐标
        this.position.left = Math.cos(Math.PI * this.angle / 180) * this.radius + this.fixed.left;
        this.position.top = Math.sin(Math.PI * this.angle / 180) * this.radius + this.fixed.top;
        this.dom.style.left = this.position.left + "px";
        this.dom.style.top = this.position.top + "px";
        // 把span标签添加到documet里面
        document.body.appendChild(this.dom);
        
        // 返回当前对象
        return this;
    }
};


// 不明白的，把后面的代码注释掉，先测试一个圆点运行情况
//new ProgressBarWin8().defaultSetting().run();
function loadImg(url) {
    var inntHtml = "<div id='progress'><img id='progressImgage' style='top: 50%;left:50%;margin-top:-95%;margin-left: 40%;'  class='progress' alt='' src='" + url + "'/></div>";
    inntHtml += '<div id="mask" style="width:100%; height:100%; position:fixed; top:0; left:0; z-index:1000;background:#cccccc; filter:alpha(opacity=50); -moz-opacity:0.5; -khtml-opacity: 0.5; opacity:0.5;"></div>';
    $("body:first").append(inntHtml);
}
function ProgressBarWin8Start() {
    var inntHtml = '<div id="mask" style="width:100%; height:100%; position:fixed; top:0; left:0; z-inde:1999;background:#cccccc; filter:alpha(opacity=50); -moz-opacity:0.5; -khtml-opacity: 0.5; opacity:0.5;"></div>';
    //inntHtml += " <img id='progressImgage' class='progress hide' alt='' src='/Images/ajax-loader.gif'/>";
        inntHtml += "<div id='progress'>";
        $("body:first").append(inntHtml);
    var progressArray = [], // 用于存放每个圆点元素对象数组，js中数组大小可变，类似于list集合
    tempArray = [], // 用于存放progressArray抛出来的每个对象，窗口大小改变后，重置每个对象的圆心坐标
    timer = 200; // 每隔多少毫秒执行一个元素对象run方法的定时器

    // 创建圆点元素对象，存入数组中，这里创建5个对象
    for (var i = 0; i < 5; ++i) {
        progressArray.push(new ProgressBarWin8().defaultSetting());
    }

    // 扩展数组each方法，c#中的lambda大都是这样实现
    Array.prototype.each = function (fn) {
        for (var i = 0, len = this.length; i < len;) {
            // 通过call(object,arg1,arg2,...)/apply(object,[arg1,arg2,...])方法改变函数fn内this的作用域， 以此可用于继承
            fn.call(this[i++], arguments);// 或者：fn.apply(this[i++],arguments)
        }
    };

    // 窗口大小改变事件，重置每个元素对象的圆心坐标
    window.onresize = function () {
        tempArray.each(function () {
            this.fixed.left = window.innerWidth / 2;
            this.fixed.top = window.innerHeight / 2;
        });
    };

    // 每个多少毫秒执行数组集合的元素对象run方法
    timer = setInterval(function () {
        if (progressArray.length <= 0) {
            // 清除此定时器，不然会一直执行（setTimeOut：延迟多少毫秒执行，一次；setInterval：每隔多少毫秒执行，多次）
            clearInterval(timer);
        } else {
            // shift() 方法用于把数组的第一个元素从其中删除，并返回第一个元素的值。
            var entity = progressArray.shift();
            tempArray.push(entity);
            // 执行每个元素对象的run方法
            entity.run();
        }
    }, timer);
}


function close() {
    $("#mask,#progress").fadeOut(function () {
        $(this).remove();
        //document.parentWindow.location.href = document.parentWindow.location.href;
        //document.parentWindow.location.reload(true);

    });
}