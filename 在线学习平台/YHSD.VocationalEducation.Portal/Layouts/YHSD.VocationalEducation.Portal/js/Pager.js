//Pager-回调函数
var callback;

//Pager-当前页
var pageIndex = 0;

//Pager-页大小
var pageSize = 10;

//Pager-总页数
var pageCount = 0;

//Pager-总记录数
var pageTotal = 0;

//Pager-组索引
var groupIndex = 0;

//Pager-分页控件ID
var ContentId

//Pager-每组最多页数
var groupSize = 10;

//fn：回调函数，contentId：分页DIV的ID，当前所有
function LoadPageControl(fn, contentId) {
    ContentId = contentId;
    callback = fn;
}

//重新生成页码
//index-要跳转的页,count-总记录数
function PagerRefresh(index, count) {
    pageTotal = count;//设置总记录数
    pageCount = Math.ceil(count / pageSize);//设置总页数
    pageIndex = index;
    if (index == 1) {
        groupIndex = 0;
    }
    BuildIndexPage();
}
function BuildIndexPage() {
    var cip_indexPageNum = getIndexPageNum();//一共能有多少组
    console.log("进入BuildIndexPage,cip_indexPageNum:" + cip_indexPageNum);
    var pageHtml;

    if (pageIndex == 1)//如果当前页是第一页
        pageHtml = "<div class='Disabled pageIndex PN'><<上一页</div>";
    else
        pageHtml = "<div class='pageIndex PN' onclick='PreIndex()'><<上一页</div>";

    if (groupIndex > 0) {
        pageHtml += "<div class='pageIndex' onclick='PreGroup()'>...</div>";
    }
    for (var i = (groupIndex * pageSize + 1) ; (i <= (groupIndex + 1) * pageSize) && (i <= pageCount) ; i++) {
        //console.log(i);
        if (pageIndex == i)
            pageHtml += "<div class='SelectPageIndex pageIndex' index=" + i + ">" + i + "</div>";
        else
            pageHtml += "<div class='pageIndex' onclick='SetPageIndex(this)' index=" + i + ">" + i + "</div>";
    }
    if (groupIndex < cip_indexPageNum - 1) {//如果当前索引页不是最后一个索引页
        pageHtml += "<div class='pageIndex' onclick='NextGroup()'>...</div>";
    }

    if (pageCount == 0 || pageIndex == pageCount)
        pageHtml += "<div class='Disabled pageIndex PN'> 下一页>></div>";
    else
        pageHtml += "<div class='pageIndex PN' onclick='NextIndex()'> 下一页>></div>";

    $("#" + ContentId).html(pageHtml);
    $("#" + ContentId + " div[class^='pageIndex']").each(function () {
        $(this).mouseover(function () {
            $(this).addClass("overBtn");
        }).mouseout(function () {
            $(this).removeClass("overBtn");
        })
    })
}
//得到分组总数量
function getIndexPageNum() {
    return Math.ceil(pageCount / groupSize);
}

function SetPageIndex(obj) {
    pageIndex = $(obj).attr("index");
    BuildIndexPage();
    callback(pageIndex);
}

//上一页
function PreIndex() {
    pageIndex = parseInt(pageIndex);
    pageIndex -= 1;
    if (pageIndex <= groupIndex * groupSize) {
        groupIndex -= 1;
    }
    callback(pageIndex);
}
//上一组
function PreGroup() {
    console.log("点击上一组");
    groupIndex -= 1;
    pageIndex = parseInt(pageIndex);
    pageIndex = (groupSize * (groupIndex + 1));
    callback(pageIndex);
}
//下一组
function NextGroup() {
    console.log("点击下一组");
    groupIndex += 1;
    pageIndex = parseInt(pageIndex);
    pageIndex = (groupSize * groupIndex) + 1;
    callback(pageIndex);
}
//下一页
function NextIndex() {
    console.log("点击下一页");
    pageIndex = parseInt(pageIndex);
    pageIndex += 1;
    if (pageIndex > (groupIndex + 1) * groupSize) {
        groupIndex += 1;
    }
    callback(pageIndex);
}
//向前跳转索引页
function PrePage() {
    var ipn = getIndexPageNum();
    pageIndex = (ipn - 1) * pageSize + pageSize;
    callback(pageIndex);
}
//向后跳转索引页
function NextPage() {
    var ipn = getIndexPageNum();
    pageIndex = (ipn + 1) * pageSize + 1;
    callback(pageIndex);
}
function SetPageCount(spc_pageCount) {

}