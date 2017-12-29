function showDiv(divid, divhead) {
    var Idiv = document.getElementById(divid);
    var mou_head = document.getElementById(divhead);
    Idiv.style.display = "block";
    //以下部分要将弹出层居中显示
    Idiv.style.left = (document.documentElement.clientWidth - Idiv.clientWidth) / 2 + document.documentElement.scrollLeft + "px";
    Idiv.style.top = (document.documentElement.clientHeight - Idiv.clientHeight) / 2 + document.documentElement.scrollTop - 50 + "px";
    //以下部分使整个页面至灰不可点击
    var procbg = document.createElement("div"); //首先创建一个div
    procbg.setAttribute("id", "mybg"); //定义该div的id
    procbg.style.background = "#C6E0ED";
    procbg.style.width = "100%";
    procbg.style.height = "100%";
    procbg.style.position = "fixed";
    procbg.style.top = "0";
    procbg.style.left = "0";
    procbg.style.zIndex = "500";
    procbg.style.opacity = "0.6";
    procbg.style.filter = "Alpha(opacity=70)";
    //背景层加入页面
    document.body.appendChild(procbg);
    document.body.style.overflow = "hidden"; //取消滚动条

    //以下部分实现弹出层的拖拽效果
    var posX;
    var posY;
    mou_head.onmousedown = function (e) {
        if (!e) e = window.event; //IE
        posX = e.clientX - parseInt(Idiv.style.left);
        posY = e.clientY - parseInt(Idiv.style.top);
        document.onmousemove = mousemove;
    }
    document.onmouseup = function () {
        document.onmousemove = null;
    }
    function mousemove(ev) {
        if (ev == null) ev = window.event;//IE
        Idiv.style.left = (ev.clientX - posX) + "px";
        Idiv.style.top = (ev.clientY - posY) + "px";
    }
}
function closeDiv(divid) //关闭弹出层
{
    var Idiv = document.getElementById(divid);
    Idiv.style.display = "none";
    document.body.style.overflow = "auto"; //恢复页面滚动条
    var body = document.getElementsByTagName("body");
    var mybg = document.getElementById("mybg");
    body[0].removeChild(mybg);
}
function openPage(PageName) {
    window.open(PageName, '窗体名称', 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=no,resizable=yes,width=750,height=470,left=180,top=140');
}
