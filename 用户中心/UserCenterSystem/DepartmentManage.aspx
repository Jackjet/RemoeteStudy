<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartmentManage.aspx.cs" Inherits="UserCenterSystem.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="Scripts/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="Scripts/calendar.js"></script>
    <link type="text/css" href="css/page.css" rel="stylesheet" />
    <script src="Scripts/My97DatePicker/WdatePicker.js"></script>
    <link href="css/Validform.css" rel="stylesheet" />
    <link href="Scripts/wbox/wbox.css" rel="stylesheet" />
    <script src="Scripts/wbox.js"></script>
    <title>组织机构管理</title>
    <script type="text/javascript">
        function postBackByObject() {
            var o = window.event.srcElement;
            if (o.tagName == "A") {
                __doPostBack("panelMain", "");
            }
        }
        function setvalue(myitem) {

            if (myitem.checked) {
                //勾选复选框 

                $("#cbXJ").val("是");
                $("#Tr_StratDate").show();
                $("#Tr_XXD").show();
                $("#Tr_Order").show();
            }
            else {
                //取消选择

                $("#cbXJ").val("否");
                $("#Tr_StratDate").hide();
                $("#Tr_XXD").hide();
                $("#Tr_Order").hide();
            }
        }
        //将日期存到隐藏域
        function InintDate() {

            $("#Txt_SchoolDate").val($("#DMstarDate").val());
            if ($("#chbchoose").is(':checked')) {
                if ($.trim($("#tbTitle").val()).length == 0) {
                    alert("机构名称不能为空");
                    return false;
                }
                else if ($.trim($("#TbNo").val()).length == 0) {
                    alert("组织机构号不能为空");
                    return false;
                }
                else if ($.trim($("#Txt_XXD").val()).length == 0) {
                    alert("学校地址不能为空");
                    return false;
                }
                else if ($.trim($("#DMstarDate").val()).length == 0) {
                    alert("建校年月不能为空");
                    return false;
                }

            }
            else {
                if ($.trim($("#tbTitle").val()).length == 0) {
                    alert("机构名称不能为空");
                    return false;
                }
                else if ($.trim($("#TbNo").val()).length == 0) {
                    alert("组织机构号不能为空");
                    return false;
                }
            }
        }

        var wbox;
        function funExcelImportStu(id) {
            wbox = $("#lbsort").wBox({
                iframeWH: {
                    width: 400, height: 150
                },
                requestType: "iframe",
                title: "编辑",
                target: "DepartmentManageOrder.aspx?id=" + id + "&t=" + new Date().getTime()
            });
            wbox.showBox();
        }
    </script>

    <style type="text/css">
        .closePerm {
            background-color: #FC5050;
            color: white;
            border: none;
        }

        .openPerm {
            background-color: #65E55D;
            color: white;
            border: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="cbXJ" runat="server" Value="否" />
        <asp:HiddenField ID="HfdID" runat="server" Value="" />
        <div class="con_wrap">
            <div class="divHeader" style="text-align: center; width: 100%">
                <h3>组织机构管理</h3>
            </div>
            <div class="divMain">
                <asp:Panel ID="panelMain" runat="server">
                    <table>
                        <tr>
                            <td style="vertical-align: top;">
                                <div class="divLeft" style="height: 490px; overflow-y: auto; overflow-x: hidden;">


                                    <asp:Panel ID="panelDepartment" runat="server" Visible="false">


                                        <%--                          <asp:TreeView ID="tvDepartment" runat="server" ShowLines="true" Width="250px"
                                            SelectedNodeStyle-BackColor="#99ccff"   OnSelectedNodeChanged="tvDepartment_SelectedNodeChanged">
                                        </asp:TreeView>--%>
                                        <asp:TreeView ID="tvDepartment" runat="server" OnSelectedNodeChanged="tvDepartment_SelectedNodeChanged" NodeWrap="True"
                                            SelectedNodeStyle-BackColor="#99ccff" ShowLines="True" Width="200px" Style="height: 470px; overflow-y: auto; overflow-x: hidden;">
                                            <SelectedNodeStyle BackColor="#0066FF" />
                                        </asp:TreeView>

                                    </asp:Panel>

                                    <asp:HiddenField ID="hidclickID" runat="server" Value="0" />
                                    <!--存储点击树节点的id-->
                                </div>
                            </td>
                            <td style="width: 100%;" valign="top">
                                <asp:Panel ID="panelAddButton" runat="server" Visible="false">

                                    <span style="margin-left: 10px;">组织机构号：</span>
                                    <asp:TextBox ID="txtZZJGH" runat="server" Width="150px"></asp:TextBox>
                                    <span>机构名称：</span>
                                    <asp:TextBox ID="txtJGMC" runat="server" Width="150px"></asp:TextBox>
                                    <%--<span>校长姓名：</span>
                                    <asp:TextBox ID="txtXZXM" runat="server" Width="150px"></asp:TextBox>--%>
                                    <div style="margin-top: 10px;">
                                        <%--<span style="margin-left: 10px;">是否是校级：</span>
                                        <asp:DropDownList ID="ddlsfsxj" runat="server" Style="height: 29px; width: 80px; width: 80px; border: 1px solid #ccc; border-radius: 3px; color: #888; display: inline-block; height: 28px; padding: 0 4px; box-shadow: 0 1px 1px rgba(0, 0, 0, 0.075) inset; transition: border 0.2s linear 0s, box-shadow 0.2s linear 0s;">
                                            <asp:ListItem>所有</asp:ListItem>
                                            <asp:ListItem>是</asp:ListItem>
                                            <asp:ListItem>否</asp:ListItem>
                                        </asp:DropDownList>--%>
                                        <asp:Button ID="btnSearch" runat="server" Text="查询" BackColor="Wheat" Style="margin-left: 5px;" OnClick="btnSearch_Click" />
                                        <asp:Button ID="btnAdd" runat="server" Text="添加" OnClick="btnAdd_Click" BackColor="Wheat" Style="margin-left: 10px;" />
                                        &nbsp; &nbsp;
                                    <asp:Button ID="btnExport" runat="server" Text="导出" Style="margin-left: -8px;" OnClick="btnExport_Click" BackColor="Wheat" OnClientClick="Javascript:return confirm('确定要导出吗？');" />
                                        &nbsp; &nbsp;
                                    <asp:Button ID="btnAuth" runat="server" Text="管理员设置" OnClick="btnAuth_Click" BackColor="Wheat" Style="margin-left: -9px;" />
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="panelRight" Style="width: 100%; float: right;" runat="server">
                                    <asp:Panel ID="panelDisp" runat="server" Visible="false" CssClass="panelDisp">

                                        <div class="con_list">
                                            <asp:ListView ID="lvDisp" runat="server" OnItemCommand="lvDisp_ItemCommand" OnItemEditing="lvDisp_ItemEditing"
                                                OnItemDeleting="lvDisp_ItemDeleting" OnPagePropertiesChanging="lvDisp_PagePropertiesChanging">
                                                <EmptyDataTemplate>
                                                    <table runat="server" style="width: 100%;">
                                                        <tr>
                                                            <td>没有符合条件的信息</td>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>
                                                <ItemTemplate>
                                                    <tr style="text-align: center;">
                                                        <td><%#Eval("XXZZJGH") %></td>
                                                        <td><%#Eval("JGMC") %></td>
                                                        <%--<td><%#Eval("XZXM") %></td>--%>
                                                        <%--<td><%#Eval("SFSXJ") %></td>--%>
                                                        <td>
                                                            <input id="lbsort" type="button" value="排序" style="background-color: wheat;" onclick="funExcelImportStu(<%#Eval("XXZZJGH") %>)" />
                                                            <asp:Button ID="lbEdit" runat="server" CommandName="edit" Text="修改" BackColor="Wheat"></asp:Button>
                                                            <asp:Button ID="lbDel" runat="server" CommandName="delete" Text="删除" BackColor="Wheat"></asp:Button>
                                                            <asp:HiddenField ID="hfJgh" runat="server" Value='<%#Eval("XXZZJGH") %>' />
                                                            <asp:HiddenField ID="hfIsXj" runat="server" Value='<%#Eval("SFSXJ") %>' />
                                                            <asp:HiddenField ID="XXZZJGH" runat="server" Value='<%#Eval("XXZZJGH") %>' />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr class="alt-row">
                                                        <td><%#Eval("XXZZJGH") %></td>
                                                        <td><%#Eval("JGMC") %></td>
                                                        <%--<td><%#Eval("XZXM") %></td>--%>
                                                        <%--<td><%#Eval("SFSXJ") %></td>--%>
                                                        <td>
                                                            <input id="lbsort" type="button" value="排序" style="background-color: wheat;" onclick="funExcelImportStu(<%#Eval("XXZZJGH") %>)" />
                                                            <asp:Button ID="lbEdit" runat="server" CommandName="edit" Text="修改" BackColor="Wheat"></asp:Button>
                                                            <asp:Button ID="lbDel" runat="server" CommandName="delete" Text="删除" BackColor="Wheat"></asp:Button>
                                                            <asp:HiddenField ID="hfJgh" runat="server" Value='<%#Eval("XXZZJGH") %>' />
                                                            <asp:HiddenField ID="hfIsXj" runat="server" Value='<%#Eval("SFSXJ") %>' />
                                                            <asp:HiddenField ID="XXZZJGH" runat="server" Value='<%#Eval("XXZZJGH") %>' />
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                                <LayoutTemplate>
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td>
                                                                <table id="itemPlaceholderContainer" runat="server" style="width: 100%;">
                                                                    <tr class="b_txt alt-row">
                                                                        <th>组织机构号</th>
                                                                        <th>机构名称</th>
                                                                        <%--<th>校长姓名</th>--%>
                                                                        <%--<th>是否是校级</th>--%>
                                                                        <th>操作</th>
                                                                    </tr>
                                                                    <tr id="itemPlaceholder" runat="server">
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </LayoutTemplate>
                                            </asp:ListView>
                                        </div>
                                        <table style="width: 100%;">
                                            <tr style="line-height: 40px; color: #333333; text-align: center;">
                                                <td>
                                                    <div class="pagination">
                                                        <asp:DataPager ID="DPTeacher" runat="server" PageSize="10" PagedControlID="lvDisp">
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
                                                </td>
                                            </tr>

                                        </table>


                                    </asp:Panel>
                                    <asp:Panel ID="panelAdd" runat="server" Visible="false">
                                        <div style="display: none">
                                            <asp:LinkButton ID="lbDeptBack" runat="server" Text="返回" OnClick="lbDeptBack_Click" CausesValidation="false"></asp:LinkButton>
                                            &nbsp;&nbsp;
                                        <asp:LinkButton ID="btContinueAdd" runat="server" Text="继续添加" Visible="false"
                                            OnClick="btContinueAdd_Click" CausesValidation="false"></asp:LinkButton>
                                        </div>
                                        <asp:Panel ID="panelAddNote" runat="server" Visible="false" Style="width: 394px; margin-left: 209px;">当前选中机构节点是“<asp:Label ID="lbSelectedDept" runat="server" ForeColor="Orange"></asp:Label>”，将会在该节点下添加机构信息。</asp:Panel>
                                        <asp:Panel ID="panelNXJAdd" runat="server">
                                            <div style="margin: 0 auto; width: 460px;">
                                                <table class="BoxCss" style="width: 100%;">
                                                    <%--<tr>
                                                        <td>是否是校级
                                                        </td>
                                                        <td>

                                                            <input id="chbchoose" type="checkbox"
                                                                onchange="setvalue(this)" />
                                                        </td>
                                                    </tr>--%>
                                                    <tr>
                                                        <td>机构名称<span style="color: Red;">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="tbTitle" runat="server"></asp:TextBox>

                                                            <span id="requiredTitle" style="color: red; display: none;">机构名称不能为空</span>
                                                            <asp:HiddenField ID="hfTitle" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Lb_DepartMentNO" runat="server" Text="组织机构码" Visible="true"></asp:Label>
                                                            <span style="color: Red;">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TbNo" runat="server" Visible="true"></asp:TextBox>
                                                            <span id="RequiredFieldValidator1" style="color: red; display: none;">机构号不能为空</span>
                                                            <asp:HiddenField ID="hfNo" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr id="Tr_XXD" style="display: none;">
                                                        <td>学校地址<span style="color: Red">*</span></td>
                                                        <td>
                                                            <asp:TextBox ID="Txt_XXD" runat="server"></asp:TextBox>


                                                            <span id="RequiredFieldValidator2" style="color: red; display: none;">学校地址不能为空</span>
                                                        </td>
                                                    </tr>
                                                    <tr id="Tr_StratDate" style="display: none;">
                                                        <td>建校年月<span style="color: Red">*</span></td>
                                                        <td>
                                                            <%-- <asp:TextBox ID="Txt_SchoolDate" runat="server" onclick="return Calendar('Txt_SchoolDate');"></asp:TextBox>--%>
                                                            <input class="Wdate" type="text" id="DMstarDate" onfocus="WdatePicker()" style="height: 28px; border: 1px solid #ccc;" />
                                                            <asp:HiddenField ID="Txt_SchoolDate" runat="server" />
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red"
                                                             ErrorMessage="建校年月不能为空"></asp:RequiredFieldValidator> --%>
                                                            <span id="RequiredFieldValidator3" style="color: red; display: none;">建校年月不能为空</span>
                                                        </td>
                                                    </tr>
                                                    <tr id="Tr_Order" style="display: none">
                                                        <td>显示顺序</td>
                                                        <td>
                                                            <input type="text" id="Txt_Order" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Panel ID="panelOption" runat="server" Style="margin: 0px auto; width: 179px;">
                                                                <asp:Button ID="btSave" runat="server" Text="保存" class="btnsave mr40" OnClick="btSave_Click" OnClientClick="return InintDate()" />&nbsp;&nbsp;
                                                        <asp:Button ID="btCancel" runat="server" Text="取消" OnClick="btCancel_Click" CausesValidation="false" Style="width: 60px;" />
                                                                &nbsp;&nbsp;
                                                        <asp:Label ID="lbMessage" runat="server" ForeColor="Green"></asp:Label>&nbsp;&nbsp;
                                                        <asp:LinkButton ID="lbSchoolMessage" runat="server" Font-Underline="true" Visible="false" OnClick="lbSchoolMessage_Click">查看学校基本信息</asp:LinkButton>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </asp:Panel>
                                    </asp:Panel>
                                </asp:Panel>

                                <asp:Panel ID="panelSchool" runat="server" Visible="false">
                                    <div style="display: none">
                                        <asp:LinkButton ID="lbSchoolBack" runat="server" Text="返回" OnClick="lbSchoolBack_Click" CausesValidation="false"></asp:LinkButton>
                                    </div>
                                    <asp:Panel ID="panelSchoolAdd" runat="server" Visible="false">
                                        <table style="width: 100%;" id="schooladddiv">
                                            <tr>
                                                <td>组织机构码<span style="color: Red">*</span></td>
                                                <td>
                                                    <asp:TextBox ID="tbZZJGM" runat="server" OnTextChanged="tbZZJGM_TextChanged" MaxLength="10" onkeydown="if(this.value.length >=10){ this.value=this.value.substr(0,this.value.length-1); }"></asp:TextBox>
                                                    <%--<asp:Label ID="lbExistZZJGM" runat="server" ForeColor="Red"></asp:Label>--%>
                                                    <asp:RequiredFieldValidator ID="lbExistZZJGM" runat="server" ForeColor="Red"
                                                        ControlToValidate="tbZZJGM" ErrorMessage="组织机构码不能为空"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>学校名称<span style="color: Red">*</span></td>
                                                <td>
                                                    <asp:TextBox ID="tbXXMC" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="requiredXXMC" runat="server" ForeColor="Red"
                                                        ControlToValidate="tbXXMC" ErrorMessage="学校名称不能为空"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td>机构简称</td>
                                                <td>
                                                    <asp:TextBox ID="txtjgjc" runat="server" MaxLength="10" onkeydown="if(this.value.length >=10){ this.value=this.value.substr(0,this.value.length-1); }"></asp:TextBox>
                                                </td>
                                                <td>负责人证件号</td>
                                                <td>
                                                    <asp:TextBox ID="txtfzrzjh" runat="server" AutoPostBack="true" CausesValidation="false" MaxLength="18" onkeydown="if(this.value.length >=18){ this.value=this.value.substr(0,this.value.length-1); }"></asp:TextBox>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td>学校英文名称</td>
                                                <td>
                                                    <asp:TextBox ID="tbXXYWMC" runat="server"></asp:TextBox>
                                                </td>
                                                <td>学校代码</td>
                                                <td>
                                                    <asp:TextBox ID="tbXXDM" runat="server" AutoPostBack="true" CausesValidation="false" MaxLength="10" onkeydown="if(this.value.length >=10){ this.value=this.value.substr(0,this.value.length-1); }"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>学校主管部门码</td>
                                                <td>
                                                    <asp:TextBox ID="tbXXZGBMM" runat="server" MaxLength="3" onkeydown="if(this.value.length >=3){ this.value=this.value.substr(0,this.value.length-1); }"></asp:TextBox></td>
                                                <td>法定代表人号</td>
                                                <td>
                                                    <asp:TextBox ID="tbFDDBRH" runat="server"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>法人证书号</td>
                                                <td>
                                                    <asp:TextBox ID="tbFRZSH" runat="server"></asp:TextBox></td>
                                                <td>党委负责人号</td>
                                                <td>
                                                    <asp:TextBox ID="tbDWFZRH" runat="server"></asp:TextBox></td>

                                            </tr>
                                            <tr>
                                                <td>校长姓名</td>
                                                <td>
                                                    <asp:TextBox ID="tbXZXM" runat="server"></asp:TextBox></td>
                                                <td>校长工号</td>
                                                <td>
                                                    <asp:TextBox ID="tbXZGH" runat="server"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>联系电话</td>
                                                <td>
                                                    <asp:TextBox ID="tbLXDH" runat="server"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="regularLXDH" runat="server" ControlToValidate="tbLXDH" ForeColor="Red"
                                                        ValidationExpression="^((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)"
                                                        ErrorMessage="联系电话格式不正确"></asp:RegularExpressionValidator>
                                                </td>
                                                <td>学校地址<span style="color: Red">*</span></td>
                                                <td>
                                                    <asp:TextBox ID="tbXXDZ" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="requiredtbXXDZ" runat="server" ForeColor="Red"
                                                        ControlToValidate="tbXXDZ" ErrorMessage="学校地址不能为空"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>学校邮政编码</td>
                                                <td>
                                                    <asp:TextBox ID="tbXXYZBM" runat="server" MaxLength="10" onkeydown="if(this.value.length >=10){ this.value=this.value.substr(0,this.value.length-1); }"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="regularXXYZBM" runat="server" ControlToValidate="tbXXYZBM" ForeColor="Red"
                                                        ValidationExpression="^[1-9][0-9]{5}$" ErrorMessage="邮政编码格式不正确"></asp:RegularExpressionValidator>
                                                </td>
                                                <td>行政区划码</td>
                                                <td>
                                                    <asp:TextBox ID="tbXZQHM" runat="server" MaxLength="6" onkeydown="if(this.value.length >=6){ this.value=this.value.substr(0,this.value.length-1); }"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="regularXZQHM" runat="server" ControlToValidate="tbXZQHM" ForeColor="Red"
                                                        ValidationExpression="^[1-9][0-9]{5}$" ErrorMessage="行政区划码格式不正确"></asp:RegularExpressionValidator>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td>传真电话</td>
                                                <td>
                                                    <asp:TextBox ID="tbCZDH" runat="server"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="regularCZDH" runat="server" ControlToValidate="tbCZDH" ForeColor="Red"
                                                        ValidationExpression="^((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)"
                                                        ErrorMessage="传真电话格式不正确"></asp:RegularExpressionValidator>
                                                </td>
                                                <td>电子信箱</td>
                                                <td>
                                                    <asp:TextBox ID="tbDZXX" runat="server"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="regularDZXX" runat="server" ControlToValidate="tbDZXX" ForeColor="Red"
                                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="电子信箱格式不正确"></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>主页地址</td>
                                                <td>
                                                    <asp:TextBox ID="tbZYDZ" runat="server"></asp:TextBox></td>
                                                <td>学校办别码</td>
                                                <td>
                                                    <asp:TextBox ID="tbXXBBM" runat="server" MaxLength="2" onkeydown="if(this.value.length >=2){ this.value=this.value.substr(0,this.value.length-1); }"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>所属主管单位码</td>
                                                <td>
                                                    <asp:TextBox ID="tbSSZGDWM" runat="server" MaxLength="6" onkeydown="if(this.value.length >=6){ this.value=this.value.substr(0,this.value.length-1); }"></asp:TextBox></td>
                                                <td>所在地城乡类型码</td>
                                                <td>
                                                    <asp:TextBox ID="tbSZDCXLXM" runat="server"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>所在地经济属性码</td>
                                                <td>
                                                    <asp:TextBox ID="tbSZDJJSXM" runat="server" MaxLength="1" onkeydown="if(this.value.length >=1){ this.value=this.value.substr(0,this.value.length-1); }"></asp:TextBox></td>
                                                <td>所在地民族属性</td>
                                                <td>
                                                    <asp:TextBox ID="tbSZDMZSX" runat="server" MaxLength="1" onkeydown="if(this.value.length >=1){ this.value=this.value.substr(0,this.value.length-1); }"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>小学入学年龄</td>
                                                <td>
                                                    <asp:TextBox ID="tbXXRXNL" runat="server" MaxLength="2" onkeydown="if(this.value.length >=2){ this.value=this.value.substr(0,this.value.length-1); }"></asp:TextBox></td>
                                                <td>初中入学年龄</td>
                                                <td>
                                                    <asp:TextBox ID="tbCZRXNL" runat="server" MaxLength="2" onkeydown="if(this.value.length >=2){ this.value=this.value.substr(0,this.value.length-1); }"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td><%--学校学制--%></td>
                                                <td>
                                                    <%--<asp:CheckBoxList ID="XXXZ" runat="server" RepeatDirection="Horizontal" Width="333px">
                                                        <asp:ListItem Text="幼儿园" Value="幼儿园"></asp:ListItem>
                                                        <asp:ListItem Text="小学学制" Value="小学"></asp:ListItem>
                                                        <asp:ListItem Text="初中学制" Value="初中"></asp:ListItem>
                                                        <asp:ListItem Text="高中学制" Value="高中"></asp:ListItem>
                                                    </asp:CheckBoxList></td>--%>
                                                <td>主教学语言码</td>
                                                <td>
                                                    <asp:TextBox ID="tbZJXYYM" runat="server" MaxLength="3" onkeydown="if(this.value.length >=2){ this.value=this.value.substr(0,this.value.length-1); }"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>辅教学语言码</td>
                                                <td>
                                                    <asp:TextBox ID="tbFJXYYM" runat="server" MaxLength="3" onkeydown="if(this.value.length >=3){ this.value=this.value.substr(0,this.value.length-1); }"></asp:TextBox></td>
                                                <td>招生半径</td>
                                                <td>
                                                    <asp:TextBox ID="tbZSBJ" runat="server"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>建校年月<span style="color: Red">*</span></td>
                                                <td>
                                                    <asp:TextBox ID="tbJXNY" runat="server" onclick="return Calendar('tbJXNY');"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="requiredJXNY" runat="server" ForeColor="Red"
                                                        ControlToValidate="tbJXNY" ErrorMessage="建校年月不能为空"></asp:RequiredFieldValidator>


                                                </td>
                                                <td>校庆日</td>
                                                <td>
                                                    <asp:TextBox ID="tbXQR" runat="server"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>学校办学类型码</td>
                                                <td>
                                                    <asp:TextBox ID="tbXXBXLXM" runat="server" MaxLength="3" onkeydown="if(this.value.length >=3){ this.value=this.value.substr(0,this.value.length-1); }"></asp:TextBox></td>
                                                <td>学校举办者码</td>
                                                <td>
                                                    <asp:TextBox ID="tbXXJBZM" runat="server" MaxLength="3" onkeydown="if(this.value.length >=3){ this.value=this.value.substr(0,this.value.length-1); }"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>显示顺序
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Txt_OrderNum" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>历史沿革</td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="tbLSYG" runat="server" TextMode="MultiLine" Rows="8" Width="590px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>备注</td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="tbBZ" runat="server" TextMode="MultiLine" Rows="6" Width="590px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="text-align: center;">
                                                    <asp:Button ID="btSchoolSave" runat="server" Text="保存" class="btnsave mr40" OnClick="btSchoolSave_Click" />
                                                    <asp:Button ID="btSchoolCancel" runat="server" Text="取消" OnClick="btSchoolCancel_Click" CausesValidation="false" />&nbsp;&nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>

                <asp:Panel ID="panelPermission" runat="server" Visible="false">
                    <div>
                        当前所选机构名称是“<asp:Label ID="lbAuthDept" runat="server" ForeColor="Orange"></asp:Label>”，在此页可以对此机构赋管理权限（管理当前机构及其所有下级机构）。
                        <asp:LinkButton ID="lbBack" runat="server" Text="返回" OnClick="lbBack_Click" CausesValidation="false" Style="background-color: #F33434; padding: 3px; width: 50px; display: inline-block; text-align: center; color: white;"></asp:LinkButton>
                    </div>


                    <div style="margin-top: 10px;">
                        <table style="width: 100%;">
                            <tr>
                                <td></td>
                                <td>教师姓名：<asp:TextBox ID="tbName" runat="server"></asp:TextBox>&nbsp;&nbsp;
                                    <asp:Button ID="btSearch" runat="server" Text="搜索" OnClick="btSearch_Click" class="BtnBackGroundColorCss" />
                                    <asp:Panel ID="panelPerDisp" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="width: 50px;">
                                    <asp:TreeView ID="tvTeacherDept" runat="server" ShowLines="true"
                                        OnSelectedNodeChanged="tvTeacherDept_SelectedNodeChanged"
                                        Width="200px" SelectedNodeStyle-BackColor="#99ccff" Style="height: 500px; overflow-x: hidden; overflow-y: auto;">
                                    </asp:TreeView>
                                </td>
                                <td valign="top">
                                    <asp:ListView ID="lvPermission" runat="server"
                                        OnItemCommand="lvPermission_ItemCommand" OnPagePropertiesChanging="lvPermission_PagePropertiesChanging">
                                        <EmptyDataTemplate>
                                            <table runat="server" style="border-collapse: collapse; width: 100%;" class="BoxCssTwo">
                                                <tr>
                                                    <td>没有符合条件的信息</td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                        <ItemTemplate>
                                            <tr style="text-align: center;">
                                                <td><%#Eval("XM") %></td>
                                                <td><%#Eval("YHZH") %></td>
                                                <td><%#Eval("XJTGW") %></td>

                                                <td>
                                                    <asp:Button ID="lbAuth" runat="server" CommandName="auth" Text="授予权限" class="BtnBackGroundColorCss openPerm" Visible='<%# Eval("QXBH")==null %>'></asp:Button>
                                                    <asp:Button ID="lbcloseAuth" runat="server" CommandName="close" Text="关闭权限" class="BtnBackGroundColorCss closePerm" Visible='<%# Eval("QXBH")!=null %>' OnClientClick="return confirm('您确定要删除该用户的权限吗?')"></asp:Button>

                                                    <asp:HiddenField ID="hfSFZJH" runat="server" Value='<%#Eval("SFZJH") %>' />

                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                            <tr style="text-align: center;">
                                                <td><%#Eval("XM") %></td>
                                                <td><%#Eval("YHZH") %></td>
                                                <td><%#Eval("XJTGW") %></td>
                                                <td>
                                                    <%--<asp:Button ID="lbAuth" runat="server" CommandName="auth" Text="授予权限" class="BtnBackGroundColorCss"></asp:Button>--%>

                                                    <asp:Button ID="lbAuth" runat="server" CommandName="auth" Text="授予权限" class="BtnBackGroundColorCss openPerm" Visible='<%# Eval("QXBH")==null %>'></asp:Button>
                                                    <asp:Button ID="lbcloseAuth" runat="server" CommandName="close" Text="关闭权限" class="BtnBackGroundColorCss closePerm" Visible='<%# Eval("QXBH")!=null %>' OnClientClick="return confirm('您确定要删除该用户的权限吗?')"></asp:Button>
                                                    <asp:HiddenField ID="hfSFZJH" runat="server" Value='<%#Eval("SFZJH") %>' />
                                                </td>
                                            </tr>
                                        </AlternatingItemTemplate>
                                        <LayoutTemplate>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table id="itemPlaceholderContainer" runat="server" class="BoxCssTwo" style="width: 100%; text-align: center;">
                                                            <tr>
                                                                <th>教师姓名</th>
                                                                <th>账号</th>
                                                                <th>现具体岗位</th>
                                                                <th>操作</th>
                                                            </tr>
                                                            <tr id="itemPlaceholder" runat="server">
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </LayoutTemplate>
                                    </asp:ListView>
                                    <table style="width: 100%;">
                                        <tr style="line-height: 40px; color: #333333; text-align: center;">
                                            <td>
                                                <asp:DataPager ID="DataPager2" runat="server" PageSize="10" PagedControlID="lvPermission">
                                                    <Fields>
                                                        <asp:NextPreviousPagerField
                                                            ButtonType="Link" ShowNextPageButton="False" ShowPreviousPageButton="true"
                                                            ShowFirstPageButton="true" FirstPageText="首页" PreviousPageText="上一页" />
                                                        <asp:NumericPagerField CurrentPageLabelCssClass="ListView_a" />
                                                        <asp:NextPreviousPagerField
                                                            ButtonType="Link" ShowPreviousPageButton="False" ShowNextPageButton="true"
                                                            ShowLastPageButton="true" LastPageText="末页" NextPageText="下一页" />
                                                        <asp:TemplatePagerField>
                                                            <PagerTemplate>
                                                                <span>| <%# Container.StartRowIndex / Container.PageSize + 1%> / 
                                                            <%# (Container.TotalRowCount % Container.MaximumRows) > 0 ? Convert.ToInt16(Container.TotalRowCount / Container.MaximumRows) + 1 : Container.TotalRowCount / Container.MaximumRows%>  页
                                                            (共<%# Container.TotalRowCount %>项)
                                                                </span>
                                                            </PagerTemplate>
                                                        </asp:TemplatePagerField>
                                                    </Fields>
                                                </asp:DataPager>
                                            </td>
                                        </tr>

                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
                <asp:HiddenField ID="hfDelete" runat="server" Value="0" />
                <asp:HiddenField ID="hfSchoolDel" runat="server" Value="" />
            </div>
            <%--以下是测试部分--%>
            <%--<div style="width: 100%; display: none;">
                <asp:GridView ID="gv" runat="server"></asp:GridView>
                <asp:Label ID="lbTest" runat="server"></asp:Label>
                <div>
                    用户登录账号：<asp:TextBox ID="tbLoginName" runat="server"></asp:TextBox>
                    <asp:Button ID="btGetDept" runat="server" Text="获取组织机构" OnClick="btGetDept_Click" />
                    <asp:Label ID="lbBool" runat="server"></asp:Label>
                    <asp:GridView ID="GridView1" runat="server"></asp:GridView>
                </div>
            </div>--%>
        </div>
    </form>
</body>

</html>
