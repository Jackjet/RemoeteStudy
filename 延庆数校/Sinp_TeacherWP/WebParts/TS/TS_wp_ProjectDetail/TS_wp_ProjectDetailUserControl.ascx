<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TS_wp_ProjectDetailUserControl.ascx.cs" Inherits="Sinp_TeacherWP.WebParts.TS.TS_wp_ProjectDetail.TS_wp_ProjectDetailUserControl" %>
<link href="/_layouts/15/Style/layout.css" rel="stylesheet" />
<link href="/_layouts/15/Style/common.css" rel="stylesheet" />
<link href="/_layouts/15/Style/tablelist.css" rel="stylesheet" />
<script src="/_layouts/15/Script/jquery-1.8.0.js"></script>
<script src="/_layouts/15/Script/popwin.js"></script>
<script src="/_layouts/15/Script/uploadFile.js"></script>
<script type="text/javascript">
    $(function () {
        $(".J-more").click(function () {
            $(this).html($(this).html() == "更多" ? "收起" : "更多");
            $(this).parents('.boxmore').siblings('.con_text').find('.con_con').slideToggle();
        });
        $(".phasetit").click(function () {
            //$(".phasetit").next().hide();
            $(this).find("a").toggleClass("up");
            
            $(this).next().slideToggle();
        });
    })
    function RemoveCurrent(fileName, trId) {
        $("#" + trId).hide();
        var nowfile = $("input[id$=Hid_fileName]").val()
        $("input[id$=Hid_fileName]").val(nowfile + '#' + fileName);
    }
    //function showzhunbei() {
    //    $(".listdv").slideToggle();
    //}
    function hideform() {
        $(".listdv").hide();
    }
    function shownext() {
        $(".listdv").slideToggle();
        return fasle;
    }
    function showform(){
        $(".listdv").show();
    }
    function closePages() {
        $("#mask,#maskTop").fadeOut();
    }
</script>
<div class="PD_con">
    <div class="ky_tab">
           <div class="single_tit">
            <h2><span class="title_left"><a href="#">课题阶段信息</a></span><span class="title_right"><a class="more" href="#"></a></span></h2>
            </div>
            <div class="tit_zong">
                <h2 class="zongketi">总课题：<asp:Literal ID="Lit_rootProject" runat="server"></asp:Literal></h2>
                <h3 class="ziketi">子课题：<asp:Literal ID="Lit_subProject" runat="server"></asp:Literal></h3>
                <h3 class="ziketi">课题状态：<asp:Literal ID="Lit_PhaseName" runat="server"></asp:Literal></h3>
            </div>
        <div class="ky_tabheader">
            <ul class="tab_tit">
                <li runat="server" id="liPrepare"><a href="#" class="tab_l"><span class="wenzi">准备阶段</span></a></li>
                <li runat="server" id="liEffect"><a href="#" class="tab_c"><span class="wenzi">实施阶段</span></a></li>
                <li runat="server" id="liend"><a href="#" class="tab_r"><span class="wenzi">结题阶段</span></a></li>
            </ul>
        </div>
        <div class="secondtit">
            <span class="left">
                审核状态：<asp:Literal ID="Lit_ExamSult" runat="server"></asp:Literal></span><span class="right">
                    <asp:Button ID="Btn_Songshen" runat="server" Text="发送送审通知" OnClick="Btn_Songshen_Click" />
                    <input type="button" runat="server" id="addphase" value="添加阶段信息" onclick="shownext()" />
                    <%--<asp:Button ID="Btn_AddPhase" runat="server" Text="添加阶段信息" OnClientClick="shownext(this);" />--%>                    
                </span>
        </div>
        <div class="listdv" style="display:none;">
            <table>
                <tr>
                    <th>课题名称：</th>
                    <td>
                        <asp:HiddenField ID="Hid_PhaseName" runat="server" />
                        <asp:TextBox ID="TB_Title" runat="server"></asp:TextBox>
                        <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_Title" runat="server" ControlToValidate="TB_Title"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                    </td>
                </tr>

                <tr class="areatr">
                    <th>课题内容：</th>
                    <td>
                        <asp:TextBox ID="TB_Content" TextMode="MultiLine" Height="82px" runat="server"></asp:TextBox>
                        <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_Content" runat="server" ControlToValidate="TB_Content"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                    </td>
                </tr>
                <tr>
                    <th>附件：</th>
                    <td style="vertical-align: middle;">
                        <asp:Label ID="lbAttach" runat="server">
                        </asp:Label>
                        <asp:HiddenField ID="Hid_fileName" runat="server" />
                        <table id="idAttachmentsTable" style="display: block;">
                            <asp:Literal ID="Lit_Bind" runat="server"></asp:Literal>
                        </table>
                        <table style="width: 100%; display: block;">
                            <tr>
                                <td id="att1">&nbsp;
                                            <input id="fileupload0" name="fileupload0" type="file" />
                                </td>
                                <td>
                                    <input onclick="AddFile()" style="width: 70px;" type="button" value="上传" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <th></th>
                    <td>
                        <asp:Button ID="Btn_InfoSave" CssClass="save" runat="server" OnClick="Btn_InfoSave_Click" ValidationGroup="ProjectSubmit" Text="保存" />
                        <input type="button" class="cancel" value="取消" onclick="hideform()" />

                    </td>
                </tr>
            </table>
        </div>
        <div class="content">
            <%--准备阶段信息--%>
            <div class="container" runat="server" id="dvPrepare">
                <div class="phasetit">
                    <span>准备阶段</span>
                    <a href="#" class="down"></a>
                </div>
                <div runat="server" id="dvpreparetit" class="eledisplay">
                    <ul class="list">
                        <asp:ListView ID="LV_Prepare" runat="server" OnPagePropertiesChanging="LV_Prepare_PagePropertiesChanging" OnItemCommand="LV_Prepare_ItemCommand" OnItemEditing="LV_Prepare_ItemEditing" OnItemDataBound="LV_Prepare_ItemDataBound">
                            <EmptyDataTemplate>
                                <table class="emptydate">
                                    <tr>
                                        <td>
                                            <img src="/_layouts/15/TeacherImages/expression.png" /><p>没有找到相关内容</p>
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            <LayoutTemplate>
                                <li id="itemPlaceholder" runat="server"></li>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <li class="li_list">
                                    <div class="top_remarks">
                                        <asp:HiddenField ID="Hid_Phase" Value='<%# Eval("PhaseName") %>' runat="server" />
                                        <div class="left_con fl"><span class="times"><%# Eval("Created") %><span class="con_details"></span></div>
                                        <div class="right_edit fr"><i class="iconfont">&#xe60a;</i><asp:LinkButton OnClientClick="shouwtab();" CommandName="Edit" CommandArgument='<%# Eval("ID") %>' ID="LB_Edit" runat="server">编辑</asp:LinkButton>|<i class="iconfont">&#xe621;</i><asp:LinkButton CommandName="Del" CommandArgument='<%# Eval("ID") %>' ID="LB_Del" runat="server" OnClientClick="return confirm('你确定要删除吗？')">删除</asp:LinkButton></div>
                                    </div>
                                    <div class="con_text">
                                        <h2><%# Eval("Title") %><i class="iconfont">&#xe616;</i></h2>
                                        <div class="con_con">
                                            <p>
                                                <%# Eval("Content") %>
                                            </p>
                                            <div class="attachment"><a href="">附件：<%# Eval("Attachment") %></a></div>
                                        </div>
                                    </div>
                                    <div class="boxmore"><a href="#"><span class="J-more">更多</span></a> </div>
                                </li>
                            </ItemTemplate>
                        </asp:ListView>
                    </ul>
                    <div class="paging">
                        <asp:DataPager ID="DP_Prepare" runat="server" PageSize="4" PagedControlID="LV_Prepare">
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
                            <%# (Container.TotalRowCount % Container.MaximumRows) > 0 ? Convert.ToInt32(Container.TotalRowCount / Container.MaximumRows) + 1 : Container.TotalRowCount / Container.MaximumRows%>  页
                            (共<%# Container.TotalRowCount %>项)
                                        </span>
                                    </PagerTemplate>
                                </asp:TemplatePagerField>
                            </Fields>
                        </asp:DataPager>
                    </div>
                </div>


            </div>
            <%--实施阶段信息--%>
            <div class="container" runat="server" id="dvEffect">
                <div class="phasetit">
                    <span>实施阶段</span>
                    <a href="#" class="down"></a>
                </div>
                <div runat="server" id="dveffecttit" class="eledisplay">
                    <ul class="list">
                        <asp:ListView ID="LV_Effect" runat="server" OnPagePropertiesChanging="LV_Effect_PagePropertiesChanging" OnItemCommand="LV_Prepare_ItemCommand" OnItemEditing="LV_Prepare_ItemEditing" OnItemDataBound="LV_Prepare_ItemDataBound">
                            <EmptyDataTemplate>
                                <table class="emptydate">
                                    <tr>
                                        <td>
                                            <img src="/_layouts/15/TeacherImages/expression.png" /><p>没有找到相关内容</p>
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            <LayoutTemplate>
                                <li id="itemPlaceholder" runat="server"></li>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <li class="li_list">
                                    <div class="top_remarks">
                                        <div class="left_con fl"><span class="times"><%# Eval("Created") %><span class="con_details"></span></div>
                                        <asp:HiddenField ID="Hid_Phase" Value='<%# Eval("PhaseName") %>' runat="server" />
                                        <div class="right_edit fr"><i class="iconfont">&#xe60a;</i><asp:LinkButton OnClientClick="shouwtab();" CommandName="Edit" CommandArgument='<%# Eval("ID") %>' ID="LB_Edit" runat="server">编辑</asp:LinkButton>|<i class="iconfont">&#xe621;</i><asp:LinkButton CommandName="Del" CommandArgument='<%# Eval("ID") %>' ID="LB_Del" runat="server" OnClientClick="return confirm('你确定要删除吗？')">删除</asp:LinkButton></div>
                                    </div>
                                    <div class="con_text">
                                        <h2><%# Eval("Title") %><i class="iconfont">&#xe616;</i></h2>
                                        <div class="con_con">
                                            <p>
                                                <%# Eval("Content") %>
                                            </p>
                                            <div class="attachment"><a href="">附件：<%# Eval("Attachment") %></a></div>
                                        </div>
                                    </div>
                                    <div class="boxmore"><a href="#"><span class="J-more">更多</span></a> </div>
                                </li>
                            </ItemTemplate>
                        </asp:ListView>
                    </ul>
                    <div class="paging">
                        <asp:DataPager ID="DP_Effect" runat="server" PageSize="4" PagedControlID="LV_Effect">
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
                            <%# (Container.TotalRowCount % Container.MaximumRows) > 0 ? Convert.ToInt32(Container.TotalRowCount / Container.MaximumRows) + 1 : Container.TotalRowCount / Container.MaximumRows%>  页
                            (共<%# Container.TotalRowCount %>项)
                                        </span>
                                    </PagerTemplate>
                                </asp:TemplatePagerField>
                            </Fields>
                        </asp:DataPager>
                    </div>
                </div>
            </div>

            <%--结题阶段信息--%>
            <div class="container" runat="server" id="dvEnd">
                <div class="phasetit">
                    <span>结题阶段</span>
                    <a href="#" class="down"></a>
                </div>
                <div runat="server" id="dvendtit" class="eledisplay">
                    <ul class="list">
                        <asp:ListView ID="LV_End" runat="server" OnPagePropertiesChanging="LV_End_PagePropertiesChanging" OnItemCommand="LV_Prepare_ItemCommand" OnItemEditing="LV_Prepare_ItemEditing" OnItemDataBound="LV_Prepare_ItemDataBound">
                            <EmptyDataTemplate>
                                <table class="emptydate">
                                    <tr>
                                        <td>
                                            <img src="/_layouts/15/TeacherImages/expression.png" /><p>没有找到相关内容</p>
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            <LayoutTemplate>
                                <li id="itemPlaceholder" runat="server"></li>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <li class="li_list">
                                    <div class="top_remarks">
                                        <asp:HiddenField ID="Hid_Phase" Value='<%# Eval("PhaseName") %>' runat="server" />
                                        <div class="left_con fl"><span class="times"><%# Eval("Created") %><span class="con_details"></span></div>
                                        <div class="right_edit fr"><i class="iconfont">&#xe60a;</i><asp:LinkButton OnClientClick="shouwtab();" CommandName="Edit" CommandArgument='<%# Eval("ID") %>' ID="LB_Edit" runat="server">编辑</asp:LinkButton>|<i class="iconfont">&#xe621;</i><asp:LinkButton CommandName="Del" CommandArgument='<%# Eval("ID") %>' ID="LB_Del" runat="server" OnClientClick="return confirm('你确定要删除吗？')">删除</asp:LinkButton></div>
                                    </div>
                                    <div class="con_text">
                                        <h2><%# Eval("Title") %><i class="iconfont">&#xe616;</i></h2>
                                        <div class="con_con">
                                            <p>
                                                <%# Eval("Content") %>
                                            </p>
                                            <div class="attachment"><a href="">附件：<%# Eval("Attachment") %></a></div>
                                        </div>
                                    </div>
                                    <div class="boxmore"><a href="#"><span class="J-more">更多</span></a> </div>
                                </li>
                            </ItemTemplate>
                        </asp:ListView>
                    </ul>
                    <div class="paging">
                        <asp:DataPager ID="DP_End" runat="server" PageSize="4" PagedControlID="LV_End">
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
                            <%# (Container.TotalRowCount % Container.MaximumRows) > 0 ? Convert.ToInt32(Container.TotalRowCount / Container.MaximumRows) + 1 : Container.TotalRowCount / Container.MaximumRows%>  页
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
    </div>
    <div class="ky_right">
        <div class="secondtit" >
            <div class="single_tit">
                <h2>
                    <span class="title_left"><a href="#">课题荣誉</a></span>
                    <span class="title_right fr"><a class="more" href="#"><input type="button" value="添加课题荣誉" onclick="openPage('添加课题荣誉', '/SitePages/AddHonour.aspx?proid=<%=Project%>    ', '700', '500'); return false;" /></a></span></h2>
           </div>          
        </div>
        <div runat="server" id="dvhonour">
        <ul class="list">
            <asp:ListView ID="LV_Honour" runat="server" OnPagePropertiesChanging="LV_Honour_PagePropertiesChanging" OnItemCommand="LV_Prepare_ItemCommand" OnItemEditing="LV_Prepare_ItemEditing" OnItemDataBound="LV_Prepare_ItemDataBound">
                <EmptyDataTemplate>
                    <table class="emptydate">
                        <tr>
                            <td>
                                <img src="/_layouts/15/TeacherImages/expression.png" /><p>没有找到相关内容</p>
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <LayoutTemplate>
                    <li id="itemPlaceholder" runat="server"></li>
                </LayoutTemplate>
                <ItemTemplate>
                    <li class="li_list">
                        <div class="top_remarks">
                            <div class="left_con fl"><span class="times"><%# Eval("Created") %><span class="con_details"></span></div>
                            <asp:HiddenField ID="Hid_Phase" Value='<%# Eval("PhaseName") %>' runat="server" />
                            <div class="right_edit fr"><i class="iconfont">&#xe60a;</i><asp:LinkButton OnClientClick="shouwtab();" CommandName="Edit" CommandArgument='<%# Eval("ID") %>' ID="LB_Edit" runat="server">编辑</asp:LinkButton>|<i class="iconfont">&#xe621;</i><asp:LinkButton CommandName="Del" CommandArgument='<%# Eval("ID") %>' ID="LB_Del" runat="server" OnClientClick="return confirm('你确定要删除吗？')">删除</asp:LinkButton></div>
                        </div>
                        <div class="con_text">
                            <h2><%# Eval("Title") %><i class="iconfont">&#xe616;</i></h2>
                            <div class="con_con">
                                <p>
                                    <%# Eval("Content") %>
                                </p>
                                <div class="attachment"><a href="">附件：<%# Eval("Attachment") %></a></div>
                            </div>
                        </div>
                        <div class="boxmore"><a href="#"><span class="J-more">更多</span></a> </div>
                    </li>
                </ItemTemplate>
            </asp:ListView>
        </ul>
        <div class="paging">
            <asp:DataPager ID="DP_Honour" runat="server" PageSize="4" PagedControlID="LV_Effect">
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
                            <%# (Container.TotalRowCount % Container.MaximumRows) > 0 ? Convert.ToInt32(Container.TotalRowCount / Container.MaximumRows) + 1 : Container.TotalRowCount / Container.MaximumRows%>  页
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


