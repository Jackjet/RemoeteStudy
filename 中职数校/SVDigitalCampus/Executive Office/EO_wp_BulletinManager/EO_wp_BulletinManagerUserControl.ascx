<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EO_wp_BulletinManagerUserControl.ascx.cs" Inherits="SVDigitalCampus.Executive_Office.EO_wp_BulletinManager.EO_wp_BulletinManagerUserControl" %>
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/common.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/style.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/iconfont.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/animate.css" />
<script src="/_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script type="text/javascript" src="/_layouts/15/SVDigitalCampus/Script/tz_slider.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/popwin.js"></script>

<div class="BulletinManager">
    <!--页面名称-->
    <h1 class="Page_name">通知公告管理</h1>
    <!--整个展示区域-->
    <div class="Whole_display_area">
        <!--操作区域-->
        <div class="Operation_area">
           <div class="left_choice fl">
                <ul>
                    <li class="Sear">
                       <input type="text" placeholder=" 请输入关键词" id="txtSearch" class="search" name="search" runat="server" /><asp:LinkButton ID="btnSearch" runat="server" OnClick="btnSearch_Click"><i class="iconfont">&#xe62d;</i></asp:LinkButton>
                    </li>
                </ul>
            </div>
            <div class="right_add fr">
                 <a href="javascript:void(0);" onclick="Addmenu();" class="add"><i class="iconfont">&#xe630;</i>新增</a>
                 <a ID="hlCkAll" href="#" onclick="checkall();" class="add"><i class="iconfont">&#xe657;</i>全选</a>
                <a href="javascript:void(0);" onclick="deletemenu();" class="add"><i class="iconfont">&#xe627;</i>删除</a>
                 <a href="javascript:void(0);" onclick="BulletinTypeShow();" class="add"><i class="iconfont">&#xe642;</i>分类管理</a>
            </div>
        </div>
        <div class="clear"></div>
        <!--展示区域-->
        <div class="Display_form">
            <asp:ListView ID="lvBulletin" runat="server" ItemPlaceholderID="bulletinPlaceholder" OnItemCommand="lvBulletin_ItemCommand" OnPagePropertiesChanging="lvBulletin_PagePropertiesChanging">
                <EmptyDataTemplate>
                     <table class="D_form">
                        <tr class="trth">
                             <th class="Account">选择</th>
                            <th class="Account">标题</th>
                            <th class="Account">所属类别</th>
                            <th class="Account">发布人</th>
                            <th class="Account">发布时间</th>
                            <th class="Account">操作</th>
                        </tr>
                         <tr><td colspan="6">暂无数据</td></tr>
                    </table>
                </EmptyDataTemplate>
                <LayoutTemplate>
                    <table class="D_form">
                        <tr class="trth">
                            <th class="Account">选择</th>
                            <th class="Account">标题</th>
                            <th class="Account">所属类别</th>
                            <th class="Account">发布人</th>
                            <th class="Account">发布时间</th>
                            <th class="Account">操作</th>
                        </tr>
                        <tr>
                            <asp:PlaceHolder runat="server" ID="bulletinPlaceholder"></asp:PlaceHolder>
                        </tr>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr class="Single">
                        <td class="Check"><input type="checkbox" id="ckselect" class="Check_box" name="ckselect" value='<%#Eval("ID") %>' /></td>
                        <td class="Account">
                          <%#Eval("Title") %></td>
                        <td class="Account"><%#Eval("Type") %></td>
                        <td class="Account"><%#Eval("Author") %></td>
                        <td class="Account"><%#Eval("Created") %></td>
                        <td class="Operation">
                             <a href="javascript:void(0);" onclick="showContent('<%# Eval("ID") %>');" style="color: blue;" title="详情"><i class="iconfont">&#xe60c;</i></a>
                            <a href="javascript:void(0);" onclick="<%#"Edit("+Eval("ID")+");return false;"%>" style="color:blue;"><i class="iconfont">&#xe629;</i></a>
                            <asp:LinkButton ID="btnDelete" BorderStyle="None" CommandName="del" CommandArgument='<%# Eval("ID") %>'  runat="server" OnClientClick="return confirm('你确定删除吗？')" CssClass="btn" ><i class="iconfont">&#xe64c;</i></asp:LinkButton>

                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="Double">
                        <td class="Check"><input type="checkbox" id="ckselect" class="Check_box" name="ckselect" value='<%#Eval("ID") %>' /></td>
                        <td class="Account">
                           <%#Eval("Title") %></td>
                        <td class="Account"><%#Eval("Type") %></td>
                        <td class="Account"><%#Eval("Author") %></td>
                        <td class="Account"><%#Eval("Created") %></td>
                        <td class="Operation">
                              <a href="javascript:void(0);" onclick="showContent('<%# Eval("ID") %>');" style="color: blue;" title="详情"><i class="iconfont">&#xe60c;</i></a>
                             <a href="javascript:void(0);" onclick="<%#"Edit("+Eval("ID")+");return false;"%>" style="color:blue;"><i class="iconfont">&#xe629;</i></a>
                            <asp:LinkButton ID="btnDelete" BorderStyle="None" CommandName="del" CommandArgument='<%# Eval("ID") %>'  runat="server" OnClientClick="return confirm('你确定删除吗？')" CssClass="btn" ><i class="iconfont">&#xe64c;</i></asp:LinkButton>
                        </td>
                    </tr>
                </AlternatingItemTemplate>
            </asp:ListView>
            <div class="page">

                <asp:DataPager ID="DPBulletin" runat="server" PageSize="8" PagedControlID="lvBulletin">
                    <Fields>
                        <asp:NextPreviousPagerField
                            ButtonType="Link" ShowNextPageButton="False" ShowPreviousPageButton="true"
                            ShowFirstPageButton="true" FirstPageText="首页" PreviousPageText="上一页" />

                        <asp:NumericPagerField CurrentPageLabelCssClass="number now" NumericButtonCssClass="number" />

                        <asp:NextPreviousPagerField
                            ButtonType="Link" ShowPreviousPageButton="False" ShowNextPageButton="true"
                            ShowLastPageButton="true" LastPageText="末页" NextPageText="下一页" />

                        <asp:TemplatePagerField>
                            <PagerTemplate>
                                <span class="page">| <%# Container.StartRowIndex / Container.PageSize + 1%> / 
                            <%# (Container.TotalRowCount % Container.MaximumRows) > 0 ? Convert.ToInt16(Container.TotalRowCount / Container.MaximumRows) + 1 : Container.TotalRowCount / Container.MaximumRows%>  页
                            (共<%# Container.TotalRowCount %>项)
                                </span>
                            </PagerTemplate>
                        </asp:TemplatePagerField>
                    </Fields>
                </asp:DataPager>


            </div>
            <%--<div style="display:none;" id="BulletinContent">
        <div id="bulletinhead">公告详细</div>
        <asp:Panel ID="Content" runat="server"></asp:Panel>
    </div>--%>
        </div>
    </div>
</div>
<script type="text/javascript">
    function BulletinTypeShow() {
        popWin.showWin("500", "500", "公告分类管理", '<%=SietUrl%>' + "BulletinTypeManager.aspx");
    }
    function showContent(BulletinID) {
        //showDiv("BulletinContent", "bulletinhead");
        popWin.scrolling = "auto";
        popWin.showWin("500", "600", "公告详细", '<%=SietUrl%>' + "BulletinDetail.aspx?BulletinID=" + BulletinID);
    }
    function Edit(BulletinID) {
        if (BulletinID == null) {

            alert("请选择一条记录！");
            return false;

        }
        //$.dialog({ id: 'Menu', title: '修改菜品', content: 'url:UpdateMenu.aspx?MenuId=' + MenuID, width: 700, height: 500, lock: true, max: false, min: false });
        popWin.scrolling = "auto";
        popWin.showWin("700", "600", "编辑公告", '<%=SietUrl%>' + "UpdateBulletin.aspx?BulletinID=" + BulletinID);

    }
    function Addmenu() {
        popWin.scrolling = "auto";
        popWin.showWin("700", "600", "新增公告", '<%=SietUrl%>' + "AddBulletin.aspx");
    }
    function checkall() {
        $("*[name='ckselect']").each(function () {
           
            this.checked = !this.checked;
            
        });
    }
    function deletemenu() {
        var bulletinvalue = "";
        $("*[name='ckselect']").each(function () {
            if ($(this).is(":checked")) {
                if (bulletinvalue != "") {
                    bulletinvalue = bulletinvalue + "," + $(this).val();
                } else {
                    bulletinvalue = $(this).val();
                }
            }
        });
        if (bulletinvalue != null && bulletinvalue != "") {
            jQuery.ajax({
                url: '<%=layouturl%>' + "CommDataHandler.aspx?action=deletemorebulletin&" + Math.random(),   // 提交的页面
                type: "POST",                   // 设置请求类型为"POST"，默认为"GET"
                data: { "bulletinvalue": bulletinvalue },
                beforeSend: function ()          // 设置表单提交前方法
                {
                    //alert("准备提交数据");


                },
                error: function (request) {      // 设置表单提交出错
                    //alert("表单提交出错，请稍候再试");
                    //rebool = false;
                },
                success: function (result) {
                    var ss = result.split("|");
                    if (ss[0] == "1") {
                        alert("删除成功!");
                        window.location.href = window.location.href; window.location.reload(true);
                    } else {
                        alert(ss[1]);
                    }
                }

            });
        }
    }
</script>
