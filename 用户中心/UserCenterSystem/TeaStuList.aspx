<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeaStuList.aspx.cs" Inherits="UserCenterSystem.TeaStuList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>师生名单</title>
    <link type="text/css" href="css/page.css" rel="stylesheet" />
    <%--<link href="Scripts/wbox/wbox.css" rel="stylesheet" />--%>
    <script src="Scripts/jquery-1.8.3.min.js"></script>
    <%--<script src="Scripts/wbox.js"></script>--%>
    <style type="text/css">
        #tvDepartment a {
            color: #33abdf;
        }
    </style>
</head>
<body>
    <form id="AccountManageForm" runat="server">
        <div class="con_wrap">
            <div class="divHeader" style="text-align: center">
                <h3>师生名单</h3>
            </div>
            <div class="divMain">
                <div id="container" style="text-align:center;">
                    <div style="display:none;">
                    <span style="margin-left: 10px;">姓名：</span>
                    <asp:TextBox ID="txtXM" runat="server" Width="150px"></asp:TextBox>
                    <span>账号：</span>
                    <asp:TextBox ID="txtZH" runat="server" Width="150px"></asp:TextBox>
                    <span>身份证：</span>
                    <asp:TextBox ID="txtSFZJH" runat="server" Width="150px"></asp:TextBox>
                    
                    <span>状态：</span>
                    <asp:DropDownList ID="ddlYHZT" runat="server" Style="height: 29px;">
                        <asp:ListItem Text="全部" Value="全部"></asp:ListItem>
                        <asp:ListItem Text="启用" Value="启用"></asp:ListItem>
                        <asp:ListItem Text="禁用" Value="禁用"></asp:ListItem>
                    </asp:DropDownList>
                        
                    <span>身份：</span>
                    <asp:DropDownList ID="ddlSF" runat="server" Style="height: 29px;">
                        <asp:ListItem Text="全部" Value="全部"></asp:ListItem>
                        <asp:ListItem Text="教师" Value="教师"></asp:ListItem>
                        <asp:ListItem Text="学生" Value="学生"></asp:ListItem>
                    </asp:DropDownList>
                        </div>
                    <span>班级：</span>
                    <asp:DropDownList ID="ddlBJ" runat="server" Style="height: 29px;">
                        
                    </asp:DropDownList>
                    <span>类型</span>
                    <asp:DropDownList ID="DropDownList1" runat="server" Style="height: 29px;">
                        <asp:ListItem Text="全部" Value="全部"></asp:ListItem>
                        <asp:ListItem Text="当前" Value="教师"></asp:ListItem>
                        <asp:ListItem Text="历史" Value="学生"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btnSearch" runat="server" Text="查询" OnClick="btnSearch_Click" BackColor="Wheat" Style="margin-left: 5px;" />&nbsp;
                    <asp:Button ID="btnCreateAccount" runat="server" Text="生成账号" OnClick="btnCreateAccount_Click" Style="background-color: wheat;display:none;" />
                    <%--<div style="margin: 5px 0 0 10px;">
                        
                    </div>--%>
                </div>
                <div class="con_list">
                    <div style="width:49%;float:left;">
                    <asp:ListView ID="lvPeriod" runat="server" DataKeyNames="SFZJH"
                         OnPagePropertiesChanging="lvPeriod_PagePropertiesChanging" OnPreRender="lvPeriod_PreRender">
                                        
                        <LayoutTemplate>
                            <table style="width: 100%; border: 1px #F3F3F3 solid">
                                <tr><th colspan="6">学员</th></tr>
                                <tr class="b_txt alt-row">
                                    <th >姓名</th>
                                    <th >账号</th>
                                    <th >身份证号</th>
                                    <th >性别</th>
                                    <%--<th >身份</th>--%>
                                    <%--<th >状态</th>--%>
                                    <%--<th >操作</th>--%>
                                </tr>
                                <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr style="text-align: center;">
                                <td>
                                    <%# Eval("XM") %> 
                                </td>
                                <td>
                                    <%# Eval("YHZH") %>
                                </td>
                                <td>
                                    <%# Eval("SFZJH") %>
                                </td>
                                <td>
                                    <div style="height:28px;">
                                    <%# Eval("XBM") %>
                                        </div>
                                </td>
                                <%--<td>
                                    <%# Eval("ZHSF")%>
                                </td>--%>
                               <%-- <td>
                                    <%# Eval("YHZT") %>
                                </td>--%>
                                <%--<td>
                                    
                                    <asp:Button ID="Button1" runat="server" CommandName="CK" CommandArgument="查看信息" Text="查看信息" class="BtnBackGroundColorCss" ></asp:Button>
                                    
                                    <asp:HiddenField ID="hfSFZJH" runat="server" Value='<%# Eval("SFZJH") %>' />
                                    <asp:HiddenField ID="hfYHZH" runat="server" Value='<%# Eval("YHZH") %>' />
                                    <asp:HiddenField ID="hfYHZT" runat="server" Value='<%# Eval("YHZT") %>' />
                                    <asp:HiddenField ID="hfSF" runat="server" Value='<%# Eval("ZHSF") %>' />
                                </td>--%>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr class="alt-row">
                                <td>
                                    <%# Eval("XM") %> 
                                </td>
                                <td>
                                    <%# Eval("YHZH") %>
                                </td>
                                <td>
                                    <%# Eval("SFZJH") %>
                                </td>
                                <td>
                                    <div style="height:28px;">
                                    <%# Eval("XBM") %>
                                        </div>
                                </td>
                                <%--<td>
                                    <%# Eval("ZHSF") %>
                                </td>--%>
                                <%--<td>
                                    <%# Eval("YHZT") %>
                                </td>--%>
                                <%--<td>
                                    <asp:Button ID="Button1" runat="server" CommandName="CK" CommandArgument="查看信息" Text="查看信息" class="BtnBackGroundColorCss" ></asp:Button>
                                    
                                    <asp:HiddenField ID="hfSFZJH" runat="server" Value='<%# Eval("SFZJH") %>' />
                                    <asp:HiddenField ID="hfYHZH" runat="server" Value='<%# Eval("YHZH") %>' />
                                    <asp:HiddenField ID="hfYHZT" runat="server" Value='<%# Eval("YHZT") %>' />
                                    <asp:HiddenField ID="hfSF" runat="server" Value='<%# Eval("ZHSF") %>' />
                                </td>--%>
                            </tr>
                        </AlternatingItemTemplate>
                        <EmptyDataTemplate>
                            <table runat="server" style="width: 100%; border: 1px #F3F3F3 solid">
                                <tr>
                                    <td>没有符合条件的信息</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:ListView>
                    <div class="pagination" style="text-align: center; padding-top: 10px;">
                <asp:DataPager ID="DPTeacher" runat="server" PageSize="10" PagedControlID="lvPeriod">
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
                    </div>
                    <div style="width:2%;float:left;"></div>
                    <div style="width:49%;float:left;">
                    <asp:ListView ID="ListView1" runat="server" DataKeyNames="SFZJH" OnItemCommand="lvPeriod_ItemCommand"
                         OnPagePropertiesChanging="ListView1_PagePropertiesChanging" OnPreRender="ListView1_PreRender">
                                        
                        <LayoutTemplate>
                            <table style="width: 100%; border: 1px #F3F3F3 solid">
                                <tr><th colspan="6">教师</th></tr>
                                <tr class="b_txt alt-row">
                                    <th >姓名</th>
                                    <th >账号</th>
                                    <th >身份证号</th>
                                    <th >性别</th>
                                    <%--<th >身份</th>--%>
                                    <%--<th >状态</th>--%>
                                    <th >操作</th>
                                </tr>
                                <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr style="text-align: center;">
                                <td>
                                    <%# Eval("XM") %> 
                                </td>
                                <td>
                                    <%# Eval("YHZH") %>
                                </td>
                                <td>
                                    <%# Eval("SFZJH") %>
                                </td>
                                <td>
                                    <%# Eval("XBM") %>
                                </td>
                                <%--<td>
                                    <%# Eval("ZHSF")%>
                                </td>--%>
                               <%-- <td>
                                    <%# Eval("YHZT") %>
                                </td>--%>
                                <td>
                                    
                                    <asp:Button ID="Button1" runat="server" CommandName="CK" CommandArgument="查看信息" Text="查看信息" class="BtnBackGroundColorCss" ></asp:Button>
                                    
                                    <asp:HiddenField ID="hfSFZJH" runat="server" Value='<%# Eval("SFZJH") %>' />
                                    <asp:HiddenField ID="hfYHZH" runat="server" Value='<%# Eval("YHZH") %>' />
                                    <asp:HiddenField ID="hfYHZT" runat="server" Value='<%# Eval("YHZT") %>' />
                                    <%--<asp:HiddenField ID="hfSF" runat="server" Value='<%# Eval("ZHSF") %>' />--%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr class="alt-row">
                                <td>
                                    <%# Eval("XM") %> 
                                </td>
                                <td>
                                    <%# Eval("YHZH") %>
                                </td>
                                <td>
                                    <%# Eval("SFZJH") %>
                                </td>
                                <td>
                                    <%# Eval("XBM") %>
                                </td>
                               <%-- <td>
                                    <%# Eval("ZHSF") %>
                                </td>--%>
                                <%--<td>
                                    <%# Eval("YHZT") %>
                                </td>--%>
                                <td>
                                    <asp:Button ID="Button1" runat="server" CommandName="CK" CommandArgument="查看信息" Text="查看信息" class="BtnBackGroundColorCss" ></asp:Button>
                                    
                                    <asp:HiddenField ID="hfSFZJH" runat="server" Value='<%# Eval("SFZJH") %>' />
                                    <asp:HiddenField ID="hfYHZH" runat="server" Value='<%# Eval("YHZH") %>' />
                                    <asp:HiddenField ID="hfYHZT" runat="server" Value='<%# Eval("YHZT") %>' />
                                    <%--<asp:HiddenField ID="hfSF" runat="server" Value='<%# Eval("ZHSF") %>' />--%>
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                        <EmptyDataTemplate>
                            <table runat="server" style="width: 100%; border: 1px #F3F3F3 solid">
                                <tr>
                                    <td>没有符合条件的信息</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:ListView>
                    <div class="pagination" style="text-align: center; padding-top: 10px;">
                <asp:DataPager ID="DataPager1" runat="server" PageSize="10" PagedControlID="ListView1">
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
                    </div>
                </div>
                
            </div>
        </div>
    </form>
</body>
</html>
