using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using BLL;
using Common;
using System.Text;

namespace UserCenterSystem
{
    public partial class StuImport : System.Web.UI.Page
    { 
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //根据用户登录账号返回所有校级组织机构，strLoginName是当前用户登录账号
                    //Base_Teacher teacher = Session[UCSKey.SESSION_LoginInfo] as Base_Teacher;
                    //Base_DepartmentBLL deptBll = new Base_DepartmentBLL();
                    //DataTable deptList = deptBll.SelectXJByLoginName(teacher);
                    //if (deptList != null)
                    //{
                    //    dpDepartMent.DataTextField = "JGMC";
                    //    dpDepartMent.DataValueField = "XXZZJGH";
                    //    dpDepartMent.DataSource = deptList;
                    //    dpDepartMent.DataBind();
                    //}
                    //dpDepartMent.Items.Insert(0, new ListItem("--学校--", ""));
                }
            }
            catch (Exception ex)
            {
                Common.LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            } 
        }
        /// <summary>
        /// 导入学生数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>不能利用SqlDataAdapter的Update()方法对datatable一次性导入，因为列名是中文的，而且还要对身份证为空的人员进行自动生成身份证，身份证是主键，必须得有，还有对单独表的维护，比如学生原学校，还要对日期为空的数据进行处理等。所以没法进行一次性导入</remarks>
        protected void btnExcelSave_Click(object sender, EventArgs e)
        {
            //1秒延时，让用户看到正在上传的提示
            System.Threading.Thread.Sleep(1000);
            //上传文件保存路径
            string strToFile = string.Empty;
            // 显示在页面上的提示信息			
            string wrongSum = "";
            //记入操作日志
            Base_LogBLL.WriteLog(LogConstants.xsxxgl, ActionConstants.xsesceldr);
            try
            {
                #region 第一 上传excel

                var a = HttpContext.Current.Request.Files[0];
                string strExtName = a.FileName.Substring(a.FileName.LastIndexOf(".") + 1).ToUpper();
                // 判断上传格式是否正确
                if (strExtName != "XLS" && strExtName != "XLSX")
                {
                    //loding.Style[HtmlTextWriterStyle.Display] = "none";//隐藏加载
                    tipInfo("只能导入Excel格式的文件，[格式提示：xls或xlsx]", "red");
                    return;
                }
                // 保存的路径
                string strToFilePath = "/Upload/temp/";
                // 物理完整路径                    
                string strToFileFullPath = MapPath(strToFilePath);
                // 检查是否有该路径  没有就创建
                if (!Directory.Exists(strToFileFullPath))
                {
                    Directory.CreateDirectory(strToFileFullPath);
                }
                string fileName = "StudentInfo" + DateTime.Now.ToString("yyyyMMddHHmmssfffffff") + ".xls";
                // 将要保存的完整文件名                
                strToFile = strToFileFullPath + fileName;
                a.SaveAs(strToFile);
                #endregion

                #region 第二、三、四
                DataTable tableTemp = new DataTable();
                try
                {
                    // 获取Excel的內容
                    tableTemp = DAL.ExcelHelper.GetTableFromExcel(strToFile);
                    #region 第二：验证模板、是否无数据
                    // 检核Excel模板格式是否正确
                    if (tableTemp.Columns[0].ColumnName.Trim() != "姓名" || tableTemp.Columns[1].ColumnName.Trim() != "证件号" || tableTemp.Columns[2].ColumnName.Trim() != "性别" || tableTemp.Columns[3].ColumnName.Trim() != "民族" || tableTemp.Columns[4].ColumnName.Trim() != "现住址" || tableTemp.Columns[5].ColumnName.Trim() != "联系电话" || tableTemp.Columns[6].ColumnName.Trim() != "备注")
                    {
                        tipInfo("Excel中的标题与模板不一致，请确认是否选错文件。", "red");
                        return;
                    }
                    //验证数据数量
                    if (tableTemp.Rows.Count == 0)
                    {
                        tipInfo("导入信息内容不能为空，请核实！", "red");
                        return;
                    }
                    #endregion

                    #region 第三：验证每条信息的规范
                    //循环验证数据规范
                    int currentRow = 1;
                    foreach (DataRow tr in tableTemp.Rows)
                    {
                        //在文件中的行号
                        currentRow = currentRow + 1;

                        bool RowNull = true;//空行
                        for (int i = 0; i < 7; i++)//一共有7列数据
                        {
                            if (!string.IsNullOrEmpty(tr[i].ToString()))
                            {
                                //如果有值就更改RowNull为false
                                RowNull = false;
                            }
                        }
                        if (RowNull)
                        {
                            //如果7列数据都为空，则下一循环
                            continue;
                        }
                        //验证姓名不为空
                        if (tr[0].ToString() == "")
                        {
                            wrongSum += "第[" + currentRow + "]行,第[1]列(姓名)不能为空，请核实！<br/>";
                        }

                        //身份证号
                        if (tr[1].ToString().Trim() == "")
                        {
                            wrongSum += "第[" + currentRow + "]行,第[2]列(证件号)不能为空，请核实！<br/>";
                        }
                        else if (!DAL.ValidatorHelper.CheckIDCard(tr[1].ToString().Trim()))
                        {
                            //记录错误的身份证号	
                            wrongSum += "第[" + currentRow + "]行,第[2]列(证件号)输入有误，请核实！<br/>";
                        }

                        //验证性别
                        if (tr[2].ToString() == "")
                        {
                            wrongSum += "第[" + currentRow + "]行,第[3]列(性别)不能为空，请核实！<br/>";
                        }
                        else if (tr[2].ToString() != "男" && tr[2].ToString() != "女")
                        {
                            wrongSum += "第[" + currentRow + "]行,第[3]列(性别)输入有误，请核实！<br/>";
                        }
                        #region MyRegion
                        
                        ////验证年级不为空
                        //if (tr[1].ToString().Trim() == "")
                        //    wrongSum += "第[" + currentRow + "]行,第[2]列(年级)不能为空，请核实！<br/>";
                        ////验证班号不为空 不做验证
                        //if (tr[2].ToString().Trim() == "")
                        //    wrongSum += "第[" + currentRow + "]行,第[3]列(班号)不能为空，请核实！<br/>";
                        ////验证姓名不为空
                        //if (tr[4].ToString().Trim() == "")
                        //    wrongSum += "第[" + currentRow + "]行,第[5]列(姓名)不能为空，请核实！<br/>";
                        ////验证性别
                        //if (tr[5].ToString().Trim() != "")
                        //{
                        //    if (tr[5].ToString().Trim() != "男" && tr[5].ToString().Trim() != "女")
                        //        wrongSum += "第[" + currentRow + "]行,第[6]列(性别)输入有误，请核实！<br/>";
                        //}
                        //if (tr[11].ToString().Trim() != "其他")
                        //{
                        //    if (!string.IsNullOrWhiteSpace(tr[6].ToString().Trim()) && tr[6].ToString().Trim().Length == 18)//不为空再去验证格式
                        //    {
                        //        if (!DAL.ValidatorHelper.CheckIDCard(tr[6].ToString().Trim()))
                        //            //记录错误的身份证号	
                        //            wrongSum += "第[" + currentRow + "]行,第[7]列(证件号)输入有误，请核实！<br/>";
                        //    }
                        //}

                        #region 注释判断
                        //验证学生类别不为空
                        // if (tr[10].ToString().Trim() == "")
                        //   wrongSum += "第[" + currentRow + "]行,第[11]列(学生类别),不能为空,请核实！<br/>";
                        //验证民族不为空
                        //if (tr[14].ToString().Trim() == "")
                        //    wrongSum += "第[" + currentRow + "]行,第[15]列(民族),不能为空,请核实！<br/>";
                        //验证国别不为空
                        //if (tr[26].ToString().Trim() == "")
                        //  wrongSum += "第[" + currentRow + "]行,第[27]列(国别),不能为空,请核实！<br/>";
                        //验证政治面貌不为空
                        //if (tr[48].ToString().Trim() == "")
                        //wrongSum += "第[" + currentRow + "]行,第[49]列(政治面貌),不能为空,请核实！<br/>";
                        //国标中 出生地码 政治面貌 身体状况 规定必填，但客户提供excel中是选填的，暂时不验证。
                        //验证出生日期不为空
                        #endregion

                        ////验证出生日期
                        //if (string.IsNullOrWhiteSpace(tr[12].ToString().Trim()))
                        //    tr[12] = DBNull.Value;
                        //else if (!IsDate(tr[12].ToString().Trim()))
                        //{
                        //    if (tr[12].ToString().Trim().Length <= 4)
                        //    {
                        //        // loding.Style[HtmlTextWriterStyle.Display] = "none";//隐藏加载
                        //        wrongSum += "第[" + currentRow + "]行,第[13]列(出生日期),必须为日期格式，如：2015/1/1<br/>";
                        //    }
                        //    else
                        //        tr[12] = HandlerLogic.HandleDate(tr[12].ToString().Trim());
                        //}
                        ////验证本记录建立日期
                        //if (string.IsNullOrWhiteSpace(tr[42].ToString().Trim()))
                        //    tr[42] = DBNull.Value;
                        //else if (!IsDate(tr[42].ToString().Trim()))
                        //{
                        //    if (tr[42].ToString().Trim().Length <= 4)
                        //    {
                        //        // loding.Style[HtmlTextWriterStyle.Display] = "none";//隐藏加载
                        //        wrongSum += "第[" + currentRow + "]行,第[43]列(本记录建立日期)，必须为日期格式，如：2015/1/1<br/>";
                        //    }
                        //    else
                        //        tr[42] = HandlerLogic.HandleDate(tr[42].ToString().Trim());
                        //}
                        ////验证 本记录建立日期
                        //if (string.IsNullOrWhiteSpace(tr[50].ToString().Trim()))
                        //{
                        //    tr[50] = DBNull.Value;
                        //}
                        //else if (!IsDate(tr[50].ToString().Trim()))
                        //{
                        //    if (tr[50].ToString().Trim().Length <= 4)
                        //    {
                        //        // loding.Style[HtmlTextWriterStyle.Display] = "none";//隐藏加载
                        //        wrongSum += "第[" + currentRow + 1 + "]行,第[51]列(入学日期)，必须为日期格式，如：2015/1/1<br/>";
                        //    }
                        //    else
                        //    {
                        //        tr[50] = HandlerLogic.HandleDate(tr[50].ToString().Trim());
                        //    }
                        //}

                        #endregion
                    }
                    #endregion

                    #region 第四 验证身份证是否重复
                    // 获取重复的考号和身份证号，输入提示信息
                    for (int i = 0; i < tableTemp.Rows.Count - 1; i++)
                    {
                        string aa = tableTemp.Rows[i]["证件号"].ToString().Trim();
                        if (string.IsNullOrEmpty(aa) || aa.Length != 18)
                        {
                            //身份证为空或不等于18位 验证下一条
                            continue;
                        }
                        for (int j = i + 1; j < tableTemp.Rows.Count; j++)
                        {
                            string bb = tableTemp.Rows[j]["证件号"].ToString().Trim();
                            if (string.IsNullOrEmpty(bb) || bb.Length != 18)
                            {
                                //身份证为空或不等于18位 验证下一条
                                continue;
                            }
                            if (aa == bb)
                            {
                                wrongSum += "第[" + (i + 2).ToString() + "]行证件号与第[" + (j + 2).ToString() + "]重复，请核实！<br/>";
                            }
                        }
                    }
                    #endregion
                }
                #endregion

                #region 第五：出错删除文件
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
                //如果提示信息不为空，则输出到页面中，并删除文件
                if (!string.IsNullOrEmpty(wrongSum))
                {
                    // 刪除服务器上的文件
                    File.Delete(strToFile);
                    tipInfo(wrongSum, "red");
                    return;
                }
                #endregion

                #region 第五 循环存储DB
                ////获取所有的学生来源身份证号 以用于判断
                //DataTable dtStuSource = DAL.SqlHelper.ExecuteDataset(CommandType.Text, "SELECT SFZJH  FROM Base_StuSource  ").Tables[0];
                //获取所有的身份证号 以用于判断
                DataTable dtUser = DAL.SqlHelper.ExecuteDataset(CommandType.Text, "select sfzjh  from base_student  ").Tables[0];

                Base_StudentBLL stuBll = new Base_StudentBLL();
                int sum = 0;
                foreach (DataRow tr in tableTemp.Rows)
                {
                    bool RowNull = true;//空行
                    for (int i = 0; i < 7; i++)//一共有7列数据
                    {
                        if (!string.IsNullOrEmpty(tr[i].ToString()))
                        {
                            //如果有值就更改RowNull为false
                            RowNull = false;
                        }
                    }
                    if (RowNull)
                    {
                        //如果7列数据都为空，则下一循环
                        continue;
                    }


                    int index = tableTemp.Rows.IndexOf(tr);
                    Base_Student stu = new Base_Student();

                    

                    stu.XM = tr[0].ToString().Trim();//姓名
                    stu.SFZJH = tr[1].ToString().Trim();//身份证号
                    stu.XBM = tr[2].ToString().Trim();//性别
                    stu.MZM = tr[3].ToString().Trim();//名族
                    stu.XZZ = tr[4].ToString().Trim();//现住址
                    stu.LXDH = tr[5].ToString().Trim();//联系电话
                    stu.BZ = tr[6].ToString().Trim();//备注

                    #region MyRegion
                    
                    //#region 赋值
                    //stu.XD = tr[0].ToString();
                    //stu.NJ = tr[1].ToString();
                    //stu.BH = tr[2].ToString();
                    //#region 【学段】注释代码
                    //////根据学段 年级 确定年级编号     学段：[0:幼儿园];[1：小学] ;[2：初中];[3：高中]
                    ////if (tr[0].ToString() == "0")
                    ////    stu.NJ = tr[1].ToString();
                    ////else if (tr[0].ToString() == "1")
                    ////    stu.NJ = tr[1].ToString();
                    ////else if (tr[0].ToString() == "2")
                    ////{
                    ////    int i = Int32.Parse(tr[1].ToString());
                    ////    switch (i)
                    ////    {
                    ////        case 1:
                    ////            stu.NJ = "7";
                    ////            break;
                    ////        case 2:
                    ////            stu.NJ = "8";
                    ////            break;
                    ////        case 3:
                    ////            stu.NJ = "9";
                    ////            break;
                    ////        default:
                    ////            stu.NJ = "0";//暂无年级
                    ////            break;
                    ////    }
                    ////}
                    ////else if (tr[0].ToString() == "3")
                    ////{
                    ////    int ii = Int32.Parse(tr[1].ToString());
                    ////    switch (ii)
                    ////    {
                    ////        case 1:
                    ////            stu.NJ = "10";
                    ////            break;
                    ////        case 2:
                    ////            stu.NJ = "11";
                    ////            break;
                    ////        case 3:
                    ////            stu.NJ = "12";
                    ////            break;
                    ////        default:
                    ////            stu.NJ = "0";//暂无年级
                    ////            break;
                    ////    }
                    ////}
                    ////else
                    ////{
                    ////    //暂无学段
                    ////    stu.NJ = "-10"; //
                    ////}
                    ////#endregion
                    ////#region 【班号  添加】
                    ////Base_ClassBLL BCB = new Base_ClassBLL();
                    ////DataSet DS = BCB.GetStudentBJBHBLL(stu.NJ + tr[2].ToString(), dpDepartMent.SelectedItem.Value);
                    ////if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)  //存在班号
                    ////    stu.BH = DS.Tables[0].Rows[0]["BJBH"].ToString();  //班级班号
                    ////else
                    ////    stu.BH = "0";   //班级  导入后 bh 置空，列表页面进入后查询未分配班的学生 edit sir 因列表查询 不存空，存0
                    //#endregion
                    //stu.P_id = tr[3].ToString();
                    //stu.XM = tr[4].ToString();
                    //stu.XBM = tr[5].ToString();
                    ////身份证生成
                    //if (tr[6].ToString().Trim() == "无" || string.IsNullOrWhiteSpace(tr[6].ToString().Trim()) || tr[6].ToString().Length < 18)
                    //{
                    //    //自动生成 yq17位码  for 存储
                    //    stu.SFZJH = DAL.RandomHelper.GetRandomForStuSfzjh().Trim();
                    //}
                    //else
                    //{
                    //    stu.SFZJH = tr[6].ToString().Trim();
                    //}
                    //stu.XJH = tr[7].ToString();
                    //stu.XMPY = tr[8].ToString();
                    //stu.CYM = tr[9].ToString();
                    //stu.XSLBM = tr[10].ToString();
                    ////stu.SFZJLXM = tr[11].ToString();
                    //stu.SFZJLXM = "居民身份证";
                    //stu.CSRQ = DAL.ConvertHelper.DateTime(tr[12].ToString()).DateTimeResult;
                    //stu.CSDM = tr[13].ToString();
                    //stu.MZM = tr[14].ToString();
                    //stu.JG = tr[15].ToString();
                    //stu.XYZJM = tr[16].ToString();
                    //stu.GATQWM = tr[17].ToString();
                    //stu.JKZKM = tr[18].ToString();
                    //stu.XZZSSQX = tr[19].ToString();
                    //stu.XZZ = tr[20].ToString(); //现住址
                    //stu.HKSZDQX = tr[21].ToString();
                    //stu.HKSZD = tr[22].ToString();
                    //stu.HKXZM = tr[23].ToString();
                    //stu.SFLDRK = tr[24].ToString();
                    //stu.JDFS = tr[25].ToString();
                    //stu.GB = tr[26].ToString();
                    //stu.TC = tr[27].ToString();
                    //stu.LXDH = tr[28].ToString();
                    //stu.TXDZ = tr[29].ToString();
                    //stu.YZBM = tr[30].ToString();   //邮政编码
                    //stu.DZXX = tr[31].ToString();
                    //stu.RXCJ = tr[32].ToString();
                    //stu.ZYDZ = tr[33].ToString();
                    //stu.BZ = tr[34].ToString();  //备注
                    //stu.SFSBSRK = tr[35].ToString();
                    //stu.SFSTCS = tr[36].ToString();
                    //stu.RYQRM = tr[37].ToString();
                    //stu.ZP = tr[38].ToString();
                    //stu.XH = tr[39].ToString();  //校内编号
                    //stu.JLXJYJ = tr[40].ToString();
                    //stu.CLZM = tr[41].ToString();  // 材料证明  
                    //stu.XGSJ = DateTime.Now;//本记录创建时间
                    //stu.ZKKH = tr[43].ToString();
                    //stu.GKKH = tr[44].ToString();
                    //stu.BYKH = tr[45].ToString();
                    //stu.YXJH = tr[46].ToString(); //原学籍号
                    //stu.XSZT = tr[47].ToString();
                    //stu.ZZMMM = tr[48].ToString();

                    //if (string.IsNullOrWhiteSpace(tr[50].ToString()))
                    //{
                    //    stu.RXNY = DateTime.Now;
                    //}
                    //else
                    //{
                    //    stu.RXNY = DateTime.Parse(tr[50].ToString().Trim());
                    //}
                    //stu.SXZKZH = tr[53].ToString();
                    //stu.GMS = tr[56].ToString();
                    //stu.JWBS = tr[57].ToString();
                    //stu.JYBH = tr[58].ToString();
                    //stu.FQXM = tr[59].ToString();
                    //stu.FZGX = tr[60].ToString();
                    //stu.FQDW = tr[61].ToString();
                    //stu.FQDH = tr[62].ToString();
                    //stu.MQXM = tr[63].ToString();
                    //stu.MZGX = tr[64].ToString();
                    //stu.MQDW = tr[65].ToString();
                    //stu.MQDH = tr[66].ToString();
                    //stu.JHRXM = tr[67].ToString();
                    //stu.JHRGX = tr[68].ToString();
                    //stu.JHRGZDW = tr[69].ToString();
                    //stu.JHRLXDH = tr[70].ToString();
                    //stu.JHRZW = tr[71].ToString();
                    //stu.YHZT = "1";    //0:正常 1：禁用
                    //stu.XXZZJGH = this.dpDepartMent.SelectedItem.Value; //校组织机构号 
                    //#endregion

                    //#region 【原学校】   【添加】
                    //if (tr[49].ToString() != "")      //  原学校名称
                    //{
                    //    string stusourceid = Guid.NewGuid().ToString();
                    //    string strsfzjh = tr[6].ToString();
                    //    string yxxmc = tr[49].ToString();  //原学校名称                               
                    //    string yxh = tr[58].ToString();//yxh =教育id
                    //    string rxfs = tr[51].ToString(); //入学方式
                    //    //string lydqm = "zanwu";//    //学生来源码 暂无
                    //    string lydqm = "";//    //学生来源码 暂无 
                    //    string lydq = tr[52].ToString();
                    //    string xslym = tr[54].ToString();//学生来处（地区） XSLYM
                    //    string jdfsm = tr[55].ToString();     //学生来源
                    //    // 验证是否存在
                    //    int ishasSFZJH = dtStuSource.Select(" SFZJH='" + tr[6].ToString().Trim() + "'").Length;

                    //    if (ishasSFZJH > 0)
                    //    {
                    //        stuBll.UpdateStuSourceBLL(stusourceid, strsfzjh, yxxmc, yxh, rxfs, lydqm, lydq, xslym, jdfsm);
                    //    }
                    //    else
                    //    {
                    //        stuBll.InsertStuSource(stusourceid, strsfzjh, yxxmc, yxh, rxfs, lydqm, lydq, xslym, jdfsm);
                    //    }

                    //    stu.XSLYBH = stusourceid;     //  原学校名称
                    //}
                    //else
                    //{
                    //    stu.XSLYBH = "";     //  原学校名称
                    //}
                    //#endregion

                    #endregion

                    // 验证是否重复
                    int ishasIDCard = dtUser.Select(" sfzjh='" + stu.SFZJH + "'").Length;

                    int iresult = 0;  //【插入】【修改】  操作结果
                    if (ishasIDCard > 0)//身份证 已存在
                    {
                        iresult = stuBll.UpdateStuTemplate(stu);        //更新学生信息
                    }
                    else 
                    {
                        iresult = stuBll.AddStudent(stu);    //添加学生信息
                    }
                    if (iresult > 0)
                    {
                        sum++;
                        Session["strTreeNode"] = stu.XXZZJGH;
                    }
                }
                if (sum > 0)
                {

                    this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('成功导入" + sum + "条学生数据！');</script>");
                }
                else
                {
                    // 刪除服务器上的文件
                    if (!string.IsNullOrWhiteSpace(strToFile))
                    {
                        File.Delete(strToFile);
                    }
                    tipInfo("导入失败，联系管理员！", "red");
                }

                #endregion
                
            }
            catch (Exception ex)
            {
                // 刪除服务器上的文件
                File.Delete(strToFile);
                // loding.Style[HtmlTextWriterStyle.Display] = "none";//隐藏加载
                tipInfo("导入时发生异常，导入失败！先确认选择的是否是学生信息模板，然后对比模板看是否缺少必要的列.", "red");
                Common.LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }
        /// <summary>
        /// 加载提示信息
        /// </summary>
        /// <param name="info"></param>
        /// <param name="istrue"></param>
        public void tipInfo(string info, string color)
        {
            Div1.InnerHtml = "";
            Div1.Style.Add("color", color);
            Div1.InnerHtml = info;
        }
        /// <summary>
        /// 判断是不是DateTime格式
        /// </summary>
        /// <param name="strDate"></param>
        /// <returns></returns>
        public bool IsDate(string strDate)
        {
            try
            {
                DateTime.Parse(strDate);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #region Excel 转换成DataTable
        /// <summary>
        /// Excel 转换成DataTable
        /// </summary>
        /// <param name="p_strFileName"></param>
        /// <returns></returns>
        public DataTable GetTableFromExcel(string p_strFileName)
        {
            const string connStrTemplate = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0;";
            DataTable dt = null;

            try
            {
                OleDbConnection conn = new OleDbConnection(string.Format(connStrTemplate, p_strFileName));
                conn.Open();

                //获取Excel的第一個sheet名称
                DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string sheetName = schemaTable.Rows[0]["TABLE_NAME"].ToString().Trim();
                //"sheet1$";
                //schemaTable.Rows[0]["TABLE_NAME"].ToString().Trim();

                // 查询Sheet中的数据
                string strSQL = "Select * From [" + sheetName + "]";
                OleDbDataAdapter da = new OleDbDataAdapter(strSQL, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dt = ds.Tables[0];

                conn.Close();

                return dt;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        /// <summary>
        /// 下载模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDownExcel_Click(object sender, EventArgs e)
        {
            //记入操作日志
            Base_LogBLL.WriteLog(LogConstants.xsxxgl, ActionConstants.xzmb);
            string fileName = "学生信息模板.xls";//客户端保存的文件名
            string filePath = Server.MapPath("Template/学生信息模板.xls");//路径

            FileInfo fileInfo = new FileInfo(filePath);
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, Encoding.UTF8).ToString());
            Response.AddHeader("Content-Length", fileInfo.Length.ToString());
            Response.AddHeader("Content-Transfer-Encoding", "binary");
            Response.ContentType = "application/octet-stream";
            Response.ContentEncoding = Encoding.Default;//System.Text.Encoding.GetEncoding("GBK");
            Response.WriteFile(fileInfo.FullName);
            Response.Flush();
            Response.End();
        }
    }
}