<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SG_wp_StuGrowthDetailUserControl.ascx.cs" Inherits="Sinp_StudentWP.WebParts.SG.SG_wp_StuGrowthDetail.SG_wp_StuGrowthDetailUserControl" %>
<link href="/_layouts/15/Style/iconfont.css" rel="stylesheet" />
<link href="/_layouts/15/Style/layout.css" rel="stylesheet" />
<link href="/_layouts/15/Style/common.css" rel="stylesheet" />
<link href="/_layouts/15/Style/tablelist.css" rel="stylesheet" />
<link href="/_layouts/15/Stu_css/StuAssociate.css" rel="stylesheet" />
<script src="/_layouts/15/Script/jquery-1.8.0.js"></script>
<script type="text/javascript">
    $(function () {
        $(".J-more").click(function () {
            $(this).html($(this).html() == "更多" ? "收起" : "更多");
            $(this).parents('.boxmore').siblings('.con_text').find('.con_con').toggle();
        });
    });
</script>
<style type="text/css">
    .exit{
        width:70%;
        margin:auto;
        margin-top:30px;
    }
    .exit table{
        width:100%;
        border:0px;
    }
    .exit table tr{
        height:30px;
        line-height:30px;

    }
    .exit table tr th{
        width:5%;
    }
    .exit table tr td{
        width:10%;
    }
    .dialog_wrap .showterm{width:66%;}
    .showterm table tr td{width:auto;}
</style>
<div class="writingform">
<div class="dialog_wrap">
  <div class="showterm">       
    <div class="content">
      <div class="container" id="Div_Normal" runat="server">  
      <ul class="list">
          <asp:ListView ID="LV_Project" runat="server" OnPagePropertiesChanging="LV_Project_PagePropertiesChanging">
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
                  <%# Eval("LiContent") %>
              </ItemTemplate>
          </asp:ListView>         
      </ul>
      <div class="paging">
          <asp:DataPager ID="DP_Project" runat="server" PageSize="7" PagedControlID="LV_Project">
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
        <div class="containerScore" runat="server" id="Div_StuScore">
                    <ul class="list">
                        <asp:ListView ID="LV_StudentScore" runat="server" OnPagePropertiesChanging="LV_StudentScore_PagePropertiesChanging">
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
                                        <div class="left_con fl"><asp:Label ID="lb_Learnyear" Text='<%# Eval("LearnYear") %>' CssClass="times" runat="server"></asp:Label><span class="con_details"></span></div>
                                    </div>
                                    <div class="con_text">
                                        <div class="con_con">
                                            <asp:ListView ID="LV_ExamInfo" runat="server">
                                                <EmptyDataTemplate>
                                                    暂无考试信息 
                                                </EmptyDataTemplate>
                                                <LayoutTemplate>
                                                    <table class="O_form">
                                                        <tr class="O_trth">
                                                            <!--表头tr名称-->
                                                            <td>考试类型</td>
                                                            <td>语文</td>
                                                            <td>数学</td>
                                                            <td>英语</td>
                                                            <td>生物</td>
                                                            <td>物理</td>
                                                            <td>化学</td>
                                                            <td>总成绩</td>
                                                            <td>平均成绩</td>                                                            
                                                        </tr>
                                                        <asp:PlaceHolder runat="server" ID="itemPlaceholder" />

                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr class="Single">
                                                        <td><%#Eval("Title") %></td>
                                                        <td><%#Eval("Chinese") %></td>
                                                        <td><%#Eval("Math") %></td>
                                                        <td><%#Eval("English") %></td>
                                                        <td><%#Eval("Biology") %></td>
                                                        <td><%#Eval("Physics") %></td>
                                                        <td><%#Eval("Chemistry") %></td>
                                                        <td><%#Eval("TotalScore") %></td>
                                                        <td><%#Eval("AverageScore") %></td> 
                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr class="Double">
                                                        <td><%#Eval("Title") %></td>
                                                        <td><%#Eval("Chinese") %></td>
                                                        <td><%#Eval("Math") %></td>
                                                        <td><%#Eval("English") %></td>
                                                        <td><%#Eval("Biology") %></td>
                                                        <td><%#Eval("Physics") %></td>
                                                        <td><%#Eval("Chemistry") %></td>
                                                        <td><%#Eval("TotalScore") %></td>
                                                        <td><%#Eval("AverageScore") %></td>                                                      
                                                    </tr>
                                                </AlternatingItemTemplate>
                                            </asp:ListView>
                                        </div>
                                    </div>
                                    <div class="boxmore"><a href="#"><span class="J-more">更多</span></a> </div>
                                </li>
                            </ItemTemplate>
                        </asp:ListView>
                    </ul>
                    <div class="paging">
                        <asp:DataPager ID="DP_StudentScore" runat="server" PageSize="5" PagedControlID="LV_StudentScore">
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
</div>