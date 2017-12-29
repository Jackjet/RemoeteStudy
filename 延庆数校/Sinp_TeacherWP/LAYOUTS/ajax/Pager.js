//Pager-回调函数
var callback;
//Pager-起始页
var pageIndex = 0;
//Pager-页大小
var pageSize = 10;
//Pager-总页
var pageCount = 0;
//Pager-索引页
var indexPage = 0;
//Pager-分页控件ID
var ContentId

var indexPageCount = 10;
var totle = 0;
function LoadPageControl(fn, contentId, index, size, count, sum) {
    totle = sum;
    ContentId = contentId;
    callback = fn;
    pageIndex = index;
    pageSize = size;
    pageCount = count;
    indexPage = Math.ceil(count / pageSize);
    CreateIndexPage();
}
//生成索引页
function CreateIndexPage() {
    var cip_indexPageNum = (pageIndex % pageSize) == 0 ? Math.floor(pageIndex / pageSize) - 1 : Math.floor(pageIndex / pageSize);
    var pageHtml = "<span>";

    if (pageIndex == 1)
        pageHtml += "<a class='Disabled'>首页</a>&nbsp;<a class='Disabled'>上一页</a>&nbsp;";
    else
        pageHtml += "<a class='Eabled'  onclick='FirstIndex()'>首页</a>&nbsp;<a class='Eabled' onclick='PreIndex()'>上一页</a>&nbsp;";

    if (cip_indexPageNum > 0) {
        pageHtml += "<a class='' onclick='PrePage()'>...</a>&nbsp;";
    }
    for (var i = (cip_indexPageNum * pageSize + 1) ; (i <= (cip_indexPageNum + 1) * pageSize) && (i <= pageCount) ; i++) {
        if (pageIndex == i)
            pageHtml += "<a class='SelectPageIndex' index=" + i + ">" + i + "</a>&nbsp;";
        else
            pageHtml += "<a class='' onclick='SetPageIndex(this)' index=" + i + ">" + i + "</a>&nbsp;";
    }
    if (cip_indexPageNum < indexPage - 1) {//如果当前索引页不是最后一个索引页
        pageHtml += "<a class='' onclick='NextPage()'>...</a>&nbsp;";
    }

    if (pageCount == 0 || pageIndex == pageCount)
        pageHtml += "<a class='Disabled'>下一页</a>&nbsp;<a class='Disabled'>末页</a>";
    else
        pageHtml += "<a class='Eabled' onclick='NextIndex()'>下一页</a>&nbsp;<a class='Eabled' onclick='EndIndex()'>末页</a>";
    pageHtml += "&nbsp;&nbsp;|&nbsp;" + pageIndex + "/" + pageCount + "页(共" + totle + "项)";

    $("#" + ContentId).html(pageHtml);
    $("#" + ContentId + " div[class^='pageIndex']").each(function () {
        $(this).mouseover(function () {
            $(this).addClass("overBtn");
        }).mouseout(function () {
            $(this).removeClass("overBtn");
        })
    })
    callback(pageIndex);
}

function getIndexPageNum() {
    return (pageIndex % pageSize) == 0 ? Math.floor(pageIndex / pageSize) - 1 : Math.floor(pageIndex / pageSize);
}

function SetPageIndex(obj) {
    pageIndex = $(obj).attr("index");
    CreateIndexPage();
}
//首页
function FirstIndex() {
    pageIndex = parseInt(pageIndex);
    pageIndex = 1;
    CreateIndexPage();
}
//上一页
function PreIndex() {
    pageIndex = parseInt(pageIndex);
    pageIndex -= 1;
    CreateIndexPage();
}
//下一页
function NextIndex() {
    pageIndex = parseInt(pageIndex);
    pageIndex += 1;
    CreateIndexPage();
}
//尾页
function EndIndex() {
    pageIndex = parseInt(pageIndex);
    pageIndex = pageCount;
    CreateIndexPage();

}
//向前跳转索引页
function PrePage() {
    var ipn = getIndexPageNum();
    pageIndex = (ipn - 1) * pageSize + pageSize;
    CreateIndexPage();
}
//向后跳转索引页
function NextPage() {
    var ipn = getIndexPageNum();
    pageIndex = (ipn + 1) * pageSize + 1;
    CreateIndexPage();
}
function SetPageCount(spc_pageCount) {

}