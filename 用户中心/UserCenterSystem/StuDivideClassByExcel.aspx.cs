using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using BLL;
using Model;
using System.IO;
using Aspose.Cells;
using Common;

namespace UserCenterSystem
{
    public partial class StuDivideClassByExcel : BaseInfo
    {
        Base_StudentBLL stuBll = new Base_StudentBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    //根据用户登录账号返回所有校级组织机构，strLoginName是当前用户登录账号
                    Base_Teacher teacher = Session[UCSKey.SESSION_LoginInfo] as Base_Teacher;
                    Base_DepartmentBLL deptBll = new Base_DepartmentBLL();
                    DataTable deptList = deptBll.SelectXJByLoginName(teacher);
                    if (deptList != null)
                    {
                        dp_DepartMent.DataTextField = "JGMC";
                        dp_DepartMent.DataValueField = "XXZZJGH";
                        dp_DepartMent.DataSource = deptList;
                        dp_DepartMent.DataBind();
                    }
                    dp_DepartMent.Items.Insert(0, new ListItem("--学校--", ""));
                }
                catch (Exception ex)
                {
                    DAL.LogHelper.WriteLogError(ex.ToString());
                }
            }
        }
        /// <summary>
        /// 生成模板并下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDownExcel_Click(object sender, EventArgs e)
        {
            try
            {
                //记入操作日志
                Base_LogBLL.WriteLog(LogConstants.xsxxgl, ActionConstants.xzmb);
                //根据所选择条件学校、年级生成 列名： 姓名 身份证号 年级 班级 excel模板
                string strdpartMentID = dp_DepartMent.SelectedItem.Value;
                if (string.IsNullOrEmpty(strdpartMentID))
                {
                    Response.Write("<script>alert('请选择学校!')</script>");
                    return;
                }
                DataTable dtstuclass = stuBll.GetStuAndClassForExcel(strdpartMentID, "stu");

                if (dtstuclass.Rows.Count > 0)
                {
                    string SheetName = "";
                    bool isbool = false;
                    Workbook workbook = new Workbook();


                    //打开模版文件  
                    string path = System.Web.HttpContext.Current.Server.MapPath("~");
                    path += @"\Template\学生分班信息.xls";
                    workbook.Open(path);

                    //打开sheet  
                    // workbook.Worksheets.Clear();
                    Worksheet worksheet = workbook.Worksheets.GetSheetByCodeName("班级人员信息录入");
                    worksheet = workbook.Worksheets[0];


                    Cells cells = worksheet.Cells;
                    for (int i = 0; i < dtstuclass.Rows.Count; i++)
                    {
                        int num = i + 1;
                        worksheet.Cells[num, 0].PutValue(dtstuclass.Rows[i]["xm"].ToString());
                        worksheet.Cells[num, 1].PutValue(dtstuclass.Rows[i]["sfzjh"].ToString());
                        worksheet.Cells[num, 2].PutValue(dtstuclass.Rows[i]["bh"].ToString());
                        
                    }

                    DataTable dtbj = stuBll.GetStuAndClassForExcel(strdpartMentID, "class");

                    for (int i = 0; i< dtbj.Rows.Count; i++)
                    {

                            worksheet.Cells[i+1, 3].PutValue(dtbj.Rows[i]["njmc"].ToString());
                            worksheet.Cells[i+1, 4].PutValue(dtbj.Rows[i]["bj"].ToString());
                            worksheet.Cells[i+1, 5].PutValue(dtbj.Rows[i]["bjbh"].ToString());
                    }
                    worksheet.AutoFitColumns();
                    HttpResponse res1 = System.Web.HttpContext.Current.Response;
                    res1.ContentType = "application/octet-stream";
                    //通知浏览器下载文件而不是打开
                    // res1.AddHeader("Content-Disposition", "attachment;  filename=" + HttpUtility.UrlEncode("学生分班信息.xls", System.Text.Encoding.UTF8));
                    res1.BufferOutput = true;
                    workbook.Save(res1, HttpUtility.UrlEncode("学生分班信息.xls", System.Text.Encoding.UTF8), ContentDisposition.Attachment, new OoxmlSaveOptions(SaveFormat.Excel97To2003));

                }
                else
                {
                    // Response.Write("<script>alert('该学校下暂无学生信息，请先录入再分班!')</script>");
                    this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('该学校下没分班的学生为零，请先录入再分班！');</script>");
                }
            }
            catch (Exception ex)
            {
                DAL.LogHelper.WriteLogError(ex.ToString());
            }
        }
        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExcelSave_Click(object sender, EventArgs e)
        {
            //延时一秒，让用户看到长在上传的提示
            System.Threading.Thread.Sleep(1000);
            string strToFile = string.Empty;
            try
            {
                //记入操作日志
                Base_LogBLL.WriteLog(LogConstants.xsxxgl, ActionConstants.excelfb);
                // 显示在页面上的提示信息			
                string wrongSum = "";
                var aa = HttpContext.Current.Request.Files[0];
                string strExtName = aa.FileName.Substring(aa.FileName.LastIndexOf(".") + 1).ToUpper();
                // 判断上传格式是否正确
                if (strExtName != "XLS" && strExtName != "XLS")
                {
                    //this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('Excel格式有误导入失败[提示：.xls]！');</script>");
                    tipInfo("只能导入Excel格式的文件，[格式提示：xls或xlsx]", "red");
                    return;
                }
                #region 第一 上传excel
                // 保存的路径
                string strToFilePath = "/Upload/temp/";

                // 物理完整路径                    
                string strToFileFullPath = MapPath(strToFilePath);

                // 检查是否有该路径  没有就创建
                if (!Directory.Exists(strToFileFullPath))
                {
                    Directory.CreateDirectory(strToFileFullPath);
                }

                string fileName = "StudentClassInfo" + DateTime.Now.ToString("yyyyMMddHHmmssfffffff") + ".xls";

                // 将要保存的完整文件名                
                strToFile = strToFileFullPath + fileName;

                aa.SaveAs(strToFile);
                #endregion

                // 获取Excel的內容
                DataTable tableTemp = DAL.ExcelHelper.GetTableFromExcel(strToFile); //ExcelCommon.GetTableFromExcel(strToFile);//
                try
                {

                    #region 第二 检查Excel格式
                    // 检核Excel格式是否正确
                    if (tableTemp.Columns[0].ColumnName != "姓名" || tableTemp.Columns[1].ColumnName != "证件号(勿改)" || tableTemp.Columns[2].ColumnName != "班级编号(参考右侧填写班号)")
                    {
                        // 刪除服务器上的文件
                        File.Delete(strToFile);
                        //this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('Excel的标题格式有误，请核实！');</script>");
                        tipInfo("Excel的标题格式有误，请核实！", "red");
                        return;
                    }
                    #endregion

                    #region 第三：循环验证 必填项
                    //获取Excel 到DateTable中的行数
                    if (tableTemp.Rows.Count == 0)
                    {
                        // 刪除服务器上的文件
                        File.Delete(strToFile);
                        //this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('导入信息内容不为空，请核实');</script>");
                        tipInfo("导入信息内容不为空，请核实！", "red");
                        return;

                    }


                    int currentRow = 1;
                    foreach (DataRow tr in tableTemp.Rows)
                    {

                        //当前行的索引				
                        currentRow = currentRow + 1;
                        //if (currentRow > tableTemp.Rows.Count)
                        //{
                        //    //空行不再循环
                        //}
                        //else
                        // {
                        if (!(tr[0].ToString() == "" && tr[1].ToString() == "" && tr[2].ToString() == ""))
                        {
                            //验证班号不为空
                            if (tr[1].ToString() == "")
                            {
                                wrongSum += "第[" + currentRow + "]行,第2列(证件号),不能为空，请核实！<br/>";
                            }
                            //验证年级不为空
                            if (tr[2].ToString() == "")
                            {
                                wrongSum += "第[" + currentRow + "]行,第3列(班级编号),不能为空，请核实！<br/>";
                            }
                        }
                    }
                    #endregion
                }
                catch (InvalidOperationException ex)
                {
                    tipInfo("请联系管理员安装 Microsoft.ACE.OLEDB.12.0", "red");
                    // 刪除服务器上的文件
                    File.Delete(strToFile);
                    return;
                }
                catch (Exception)
                {
                    // 刪除服务器上的文件
                    File.Delete(strToFile);
                    tipInfo("导入失败，联系管理员！", "red");
                    return;
                }
                //输入页面到页面DIV中的提示信息			
                if (!string.IsNullOrEmpty(wrongSum))
                {
                    // 刪除服务器上的文件
                    File.Delete(strToFile);
                    tipInfo(wrongSum, "red");
                    return;
                }

                int sum = 0;
                #region   第四 循环存储DB
                foreach (DataRow tr in tableTemp.Rows)
                {
                    Base_Student stu = new Base_Student();
                    int iresult = 0;
                    if (!(tr[1].ToString() == "" && tr[2].ToString() == ""))
                    {
                        stu.SFZJH = tr[1].ToString();
                        stu.BH = tr[2].ToString();
                        stu.XXZZJGH = this.dp_DepartMent.SelectedItem.Value; //校组织机构号 
                        Base_StudentBLL stuBll = new Base_StudentBLL();
                        iresult = stuBll.DivideClassMore(stu.XXZZJGH, "", stu.BH, stu.SFZJH);
                    }
                    if (iresult > 0)
                    {
                        sum++;
                        Session["strTreeNode"] = stu.XXZZJGH;
                    }
                }
                if (sum > 0)
                {
                    this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('分班成功！');parent.window.location=\"StudentList.aspx?xxzzjgh=" + Session["strTreeNode"].ToString() + "\";</script>");
                }
                else
                {
                    // 刪除服务器上的文件
                    File.Delete(strToFile);
                    //this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('导入失败，数据有误，请核实！');</script>");
                    tipInfo("导入失败，数据有误，请核实！", "red");
                }
                #endregion
            }
            catch (Exception ex)
            {
                // 刪除服务器上的文件
                if (!string.IsNullOrWhiteSpace(strToFile))
                {
                    File.Delete(strToFile);
                }
                DAL.LogHelper.WriteLogError(ex.ToString());
            }
        }

        /// <summary>
        /// 加载提示信息
        /// </summary>
        /// <param name="info"></param>
        /// <param name="istrue"></param>
        public void tipInfo(string info, string FontColor)
        {
            Msg.InnerHtml = "";
            Msg.Style.Add("color", FontColor);
            Msg.InnerHtml = info;
        }
    }
}