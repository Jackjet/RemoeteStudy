<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ES_wp_ExamQManagerUserControl.ascx.cs" Inherits="SVDigitalCampus.Examination_System.ES_wp_ExamQManager.ES_wp_ExamQManagerUserControl" %>
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/common.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/iconfont.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/animate.css" rel="stylesheet" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/tz_slider.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/popwin.js"></script>
<link href="../../../../_layouts/15/SVDigitalCampus/Script/Enter/jquery-webox.css" rel="stylesheet" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/jquery-webox.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/win8loading.js"></script>
<style type="text/css">
    /*.hide{display:none }*/  
             .progress{z-index: 2000}
             
</style>
<script type="text/javascript">
    
    function UpdateObj(qID, type) {
        if (qID == null) {

            alert("请选择一条记录！");
            return false;

        }
        popWin.scrolling = "auto";
        //$.dialog({ id: 'Menu', title: '修改菜品', content: 'url:UpdateMenu.aspx?MenuId=' + MenuID, width: 700, height: 500, lock: true, max: false, min: false });
        popWin.showWin("750", "760", "编辑试题", '<%=SietUrl%>' + "EditQuestion.aspx?qID=" + qID + "&type=" + type);
    }

    function Addquestion() {
        popWin.scrolling = "auto";
        popWin.showWin("750", "760", "添加试题", '<%=SietUrl%>' + "AddQuestion.aspx");
    }
    function QType() {
        popWin.scrolling = "auto";
        popWin.showWin("600", "500", "试题类型管理", '<%=SietUrl%>' + "ExamQTypeManager.aspx");
    }
    function Detail(qID, type) {
      <%--  popWin.scrolling = "auto";
        popWin.showWin("750", "700", "试题详情", '<%=SietUrl%>' + "ExamQDetail.aspx?qID=" + qID + "&type=" + type);--%>
        $.webox({
            width: 750, height: 700, bgvisibel: true, title: '试题详情', iframe: '<%=SietUrl%>' + "ExamQDetail.aspx?qID=" + qID + "&type=" + type
            });
    }

    function LoadExamQ() {
      
       
    jQuery.ajax({
            url: '<%=layoutstr%>' + "/SVDigitalCampus/ExamSystemHander/ExamSystemDataHander.aspx?action=LoadExamQXml&" + Math.random(),   // 提交的页面
            type: "POST",
            async: true,
              beforeSend: function ()          // 设置表单提交前方法
              {
                  loadImg('<%=layoutstr%>' + "/SVDigitalCampus/Image/ajax-loader.gif");
              },
              error: function (request) {      // 设置表单提交出错
                  //alert("表单提交出错，请稍候再试");
                  //rebool = false;
              },
              success: function (result) {
                  var ss = result.split("|");
                  if (ss[0] == "1") {
                      //ProgressBarWin8Start();
                      close();
                      window.location.reload(true);
                  }
              }

          });
    }
</script>
<div class="Question">
    <!--页面名称-->
    <h1 class="Page_name">试题信息管理</h1>
    <!--整个展示区域-->
    <div class="Whole_display_area">
        <div class="S_conditions">
            <div id="selectList" class="screenBox screenBackground">
                <dl class="listIndex dlHeight" attr="terminal_brand_s">
                    <dt>专业</dt>
                    <dd>
                        <asp:ListView ID="lvMajor" runat="server" ItemPlaceholderID="itemPlaceholder" OnItemCommand="lvMajor_ItemCommand">
                            <LayoutTemplate>
                                <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <label><asp:LinkButton ID="majoritem" runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="showSubject" CssClass='<%#Eval("class") %>'><%#Eval("Title") %></asp:LinkButton> </label>
                            </ItemTemplate>
                        </asp:ListView>
                        <span class="more">展开</span>
                    </dd>
                </dl>
                <dl class="listIndex" attr="terminal_brand_s" id="subjectdl" runat="server">
                    <dt>学科</dt>
                    <dd>
                        <asp:ListView ID="lvSubject" runat="server" ItemPlaceholderID="subitemPlaceHolder" OnItemCommand="lvSubject_ItemCommand">
                            <LayoutTemplate>
                                <asp:PlaceHolder ID="subitemPlaceHolder" runat="server"></asp:PlaceHolder>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <label>
                                    <asp:LinkButton ID="SubjectItem" runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="SubjectSearch" CssClass='<%#Eval("class") %>'><%#Eval("Title") %></asp:LinkButton></label>
                            </ItemTemplate>
                        </asp:ListView>
                    </dd>
                </dl>
            </div>
        </div>
        <div class="clear"></div>
        <!--操作区域-->
        <div class="Operation_area">
            <div class="left_choice fl">
                <ul>
                    <li class="Select">
                        <asp:DropDownList ID="ddlType" runat="server" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Text="所有" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </li>
                    <li class="Batch_operation">
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="option" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Text="所有" Value="0"></asp:ListItem>
                            <asp:ListItem Text="启用" Value="1"></asp:ListItem>
                            <asp:ListItem Text="禁用" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </li>
                    <li class="Sear">
                        <input type="text" placeholder=" 请输入关键字" id="txtkeywords" class="search" name="search" runat="server" /><asp:LinkButton ID="btnSearch" runat="server" OnClick="btnSearch_Click"><i class="iconfont">&#xe62d;</i></asp:LinkButton>
                    </li>
                </ul>
            </div>
            <div class="right_add fr">
               <a href="#" onclick="LoadExamQ();" class="add">加载试题数据</a>
          <%--   <asp:LinkButton ID="lbLoadQList" runat="server" OnClientClick="LoadExamQ();" CssClass="add">加载试题数据</asp:LinkButton>--%>
                <a href="javascript:void(0);" onclick="Addquestion();" class="add"><i class="iconfont">&#xe630;</i>添加试题</a>
                <a href="javascript:void(0);" onclick="QType();" class="add">试题分类管理</a>
            </div>
        </div>
        <div class="clear"></div>
        <!--展示区域-->
        <div class="Display_form">
            <asp:ListView ID="lvExamQ" runat="server" OnPagePropertiesChanging="lvExamQ_PagePropertiesChanging" OnItemCommand="lvExamQ_ItemCommand">
                <EmptyDataTemplate>
                    <table>
                        <tr>
                            <td colspan="8" style="text-align: center">亲，暂无试题记录！
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <LayoutTemplate>
                    <table class="D_form">
                        <tr class="trth">
                            <!--表头tr名称-->
                            <th class="Account">编号
                            </th>
                            <th class="name">试题名称
                            </th>
                            <th class="Head">类型
                            </th>
                            <th class="Contact">难度
                            </th>
                            <th class="Contact">发布人
                            </th>
                            <th class="Contact">发布时间
                            </th>
                            <th class="State">状态
                            </th>
                            <th class="Operation">操作
                            </th>
                        </tr>
                        <tr id="itemPlaceholder" runat="server"></tr>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr class="Single">
                        <td class="Count">
                            <%#Eval("Count")%>
                        </td>
                        <td class="Title">
                            <%#Eval("Title")%>
                        </td>
                        <td class="Type">
                            <%#Eval("Type")%>
                        </td>
                        <td class="Difficulty">
                            <%#Eval("DifficultyShow")%>
                        </td>
                        <td class="Author"><%#Eval("Author")%></td>
                        <td class="Created">
                            <%#Eval("Created")%>  
                        </td>
                        <td class="State">
                            <span class="qijin"><a href="javascript:void(0);" onclick="Enused(<%#"this,'" +Eval("ID")+"','"+Eval("QType")+"'" %>);" class='<%#Eval("qStatus")%>'>启用</a><a href="javascript:void(0);" onclick="Disused(<%#"this,'" +Eval("ID")+"','"+Eval("QType")+"'" %>);" class='<%#Eval("jStatus")%>'>禁用</a></span>
                        </td>
                        <td class="Operation">
                            <a href="javascript:void(0);" onclick="Detail('<%# Eval("ID") %>','<%#Eval("TypeID")%>');" style="color: blue;" title="详情"><i class="iconfont">&#xe60c;</i></a>
                            <a href="javascript:void(0);" onclick="UpdateObj('<%# Eval("ID") %>','<%#Eval("TypeID")%>');" style="color: blue;" title="编辑"><i class="iconfont">&#xe629;</i></a>
                            <asp:LinkButton ID="btndelete" BorderStyle="None" CommandName="del" CommandArgument='<%# Eval("ID")+","+Eval("QType")%>' runat="server" OnClientClick="return confirm('你确定删除吗？')" CssClass="btn" ToolTip="删除"><i class="iconfont">&#xe64c;</i></asp:LinkButton>

                        </td>
                    </tr>

                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="Double">

                        <td class="Count">
                            <%#Eval("Count")%>
                        </td>
                        <td class="Title">
                            <%#Eval("Title")%>
                        </td>
                        <td class="Type">
                            <%#Eval("Type")%>
                        </td>
                        <td class="Difficulty">
                            <%#Eval("DifficultyShow")%>
                        </td>
                        <td class="Author"><%#Eval("Author")%></td>
                        <td class="Created">
                            <%#Eval("Created")%>  
                        </td>
                        <td class="State">
                            <span class="qijin"><a href="javascript:void(0);" onclick="Enused(<%#"this,'" +Eval("ID")+"','"+Eval("QType")+"'" %>);" class='<%#Eval("qStatus")%>'>启用</a><a href="javascript:void(0);" onclick="Disused(<%#"this,'" +Eval("ID")+"','"+Eval("QType")+"'" %>);" class='<%#Eval("jStatus")%>'>禁用</a></span>
                        </td>
                        <td class="Operation">
                              <a href="javascript:void(0);" onclick="Detail('<%# Eval("ID") %>','<%#Eval("TypeID")%>');" style="color: blue;" title="详情"><i class="iconfont">&#xe60c;</i></a>
                            <a href="javascript:void(0);" onclick="UpdateObj('<%# Eval("ID") %>','<%#Eval("TypeID")%>');" style="color: blue;" title="编辑"><i class="iconfont">&#xe629;</i></a>
                            <asp:LinkButton ID="btndelete" BorderStyle="None" CommandName="del" CommandArgument='<%# Eval("ID")+","+Eval("TypeID")%>' runat="server" OnClientClick="return confirm('你确定删除吗？')" CssClass="btn" ToolTip="删除"><i class="iconfont">&#xe64c;</i></asp:LinkButton>

                        </td>
                    </tr>
                </AlternatingItemTemplate>
            </asp:ListView>
            <div class="page">
                <asp:DataPager ID="DPExamQ" runat="server" PageSize="15" PagedControlID="lvExamQ">
                    <Fields>
                        <asp:NextPreviousPagerField
                            ButtonType="Link" ShowNextPageButton="False" ShowPreviousPageButton="true"
                            ShowFirstPageButton="true" FirstPageText="首页" PreviousPageText="上一页" ButtonCssClass="pageup" />

                        <asp:NumericPagerField CurrentPageLabelCssClass="number now" NumericButtonCssClass="number" />

                        <asp:NextPreviousPagerField
                            ButtonType="Link" ShowPreviousPageButton="False" ShowNextPageButton="true"
                            ShowLastPageButton="true" LastPageText="末页" NextPageText="下一页" ButtonCssClass="pagedown" />

                        <asp:TemplatePagerField>
                            <PagerTemplate>
                                <span class="count">| <%# Container.StartRowIndex / Container.PageSize + 1%> / 
                            <%# (Container.TotalRowCount % Container.MaximumRows) > 0 ? Convert.ToInt16(Container.TotalRowCount / Container.MaximumRows) + 1 : Container.TotalRowCount / Container.MaximumRows%>  页
                            (共<%# Container.TotalRowCount %>项)
                                </span>
                            </PagerTemplate>
                        </asp:TemplatePagerField>
                    </Fields>
                </asp:DataPager>

            </div>
        </div>
    </div>
</div>
<script type="text/javascript">
    function Enused(obj, qid, tid) {
        if (qid != null && qid != "" && tid != null && tid!="") {
            jQuery.ajax({
                url: '<%=layoutstr%>' + "/SVDigitalCampus/ExamSystemHander/ExamSystemDataHander.aspx?action=ChangeStatus&" + Math.random(),   // 提交的页面
                type: "POST",                   // 设置请求类型为"POST"，默认为"GET"
                data: { "qid": qid, "tid": tid, "Status": "1" },
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
                        $(obj).next().attr("class", "Disable");
                        $(obj).attr("class", "Enable");
                        //GetNewQXml();
                    }
                }

            });
        }
    }
    function GetNewQXml() {
        jQuery.ajax({
            url: '<%=layoutstr%>' + "/SVDigitalCampus/ExamSystemHander/ExamSystemDataHander.aspx?action=NewQXml&" + Math.random(),   // 提交的页面
            type: "POST",                   // 设置请求类型为"POST"，默认为"GET"
            async: false,
            success: function (result) {
            }
        });
    }
    function Disused(obj, qid, tid) {
        if (qid != null && qid != "" && tid != null && tid != "") {
            jQuery.ajax({
                url: '<%=layoutstr%>' + "/SVDigitalCampus/ExamSystemHander/ExamSystemDataHander.aspx?action=ChangeStatus&" + Math.random(),   // 提交的页面
                type: "POST",                   // 设置请求类型为"POST"，默认为"GET"
                data: { "qid": qid, "tid": tid, "Status": "2" },
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
                        $(obj).prev().attr("class", "Disable");
                        $(obj).attr("class", "Enable");
                        //GetNewQXml();
                    }
                }

            });
        }
    }
</script>
