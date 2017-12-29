<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TG_wp_UserGrowDetailUserControl.ascx.cs" Inherits="Sinp_TeacherWP.WebParts.TG.TG_wp_UserGrowDetail.TG_wp_UserGrowDetailUserControl" %>
<link href="/_layouts/15/Style/iconfont.css" rel="stylesheet" />
<link href="/_layouts/15/Style/layout.css" rel="stylesheet" />
<link href="/_layouts/15/Style/common.css" rel="stylesheet" />
<link href="/_layouts/15/Style/tablelist.css" rel="stylesheet" />
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
</style>
<div class="writingform">
<div class="dialog_wrap">
  <div class="showterm">       
    <div class="content">
      <div class="container">  
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
                  <li class="li_list">
                      <div class="top_remarks">
                          <div class="left_con fl">
                              <span class="times" style="margin-right:70px"><%# Eval("StartDate") %></span><span class="con_details"><em><%# Eval("LearnYear") %></em>|<em><%# Eval("ProjectDirector") %></em></span></div>
                          <div class="right_edit fr"></div>
                      </div>
                      <div class="con_text">
                          <h2><%# Eval("Title") %></h2>
                          <div class="con_con">
                              <p>
                                  <%# Eval("ProjectContent") %>
                              </p>
                              <div class="attachment"><%# Eval("Attachment") %></div>
                          </div>
                      </div>
                      <div class="boxmore"><a href="#"><span class="J-more">更多</span></a> </div>
                  </li>
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
    </div>
  </div>
</div>
</div>