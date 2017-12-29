using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
using System.Text.RegularExpressions;
using BLL;
using Model;
using DAL;
using Common;
using System.Text;

namespace UserCenterSystem
{
    public partial class StudentEdit : BaseInfo
    {
        Base_StudentBLL stuBll = new Base_StudentBLL();
        static string UserId = "";//用户Id
        string strRxxzzjgh = "";//学校
        string strRnj = "";//年级编号
        string strRbjbh = "";//班级编号

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)//首次加载时
            {
                try
                {
                    tb_UserIdentity.Enabled = false;
                    //dp_IdentityDocuments.Enabled = false;
                    UserId = Request.QueryString["PamramUserId"] == null ? "" : Request.QueryString["PamramUserId"].ToString(); //主键
                    if (string.IsNullOrEmpty(UserId))
                    {
                        return;
                    }
                    //修改班级后的传值
                    //strRxxzzjgh = Request.QueryString["xxzzjgh"] == null ? "" : Request.QueryString["xxzzjgh"].ToString();
                    //hiddenschool.Value = strRxxzzjgh;
                    //strRnj = Request.QueryString["nj"] == null ? "" : Request.QueryString["nj"].ToString();
                    //hiddenGrade.Value = strRnj;
                    //strRbjbh = Request.QueryString["bjbh"] == null ? "" : Request.QueryString["bjbh"].ToString();
                    //hiddenClass.Value = strRbjbh;

                    // btnRandom.Enabled = false;
                    //根据用户登录账号返回所有校级组织机构，strLoginName是当前用户登录账号
                    //Base_Teacher teacher = Session[UCSKey.SESSION_LoginInfo] as Base_Teacher;
                    //if (teacher != null)
                    //{
                    //    Base_DepartmentBLL deptBll = new Base_DepartmentBLL();
                    //    DataTable deptList = deptBll.SelectXJByLoginName(teacher);
                    //    //dp_DepartMent.DataTextField = "JGMC";
                    //    //dp_DepartMent.DataValueField = "XXZZJGH";
                    //    //dp_DepartMent.DataSource = deptList;
                    //    //dp_DepartMent.DataBind();
                    //    BindGrade();//绑定专业下拉列表
                    //    BindClass();//绑定班级下拉列表
                    //    DisplayDetails();//对用户信息进行加载  

                    //    //if (teacher.XM == "超级管理员")
                    //    //{
                    //    //    dp_DepartMent.Enabled = true;
                    //    //}
                    //}
                    BindGrade();//绑定专业下拉列表
                    BindClass();//绑定班级下拉列表
                    DisplayDetails();//对用户信息进行加载  
                }
                catch (Exception ex)
                {
                    LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
                }
            }
        }


        /// <summary>
        /// 【Function】绑定学科
        /// </summary>
        private void BindGrade()
        {
            //try
            //{
            //    Base_GradeBLL gradeBll = new Base_GradeBLL();
            //    DataTable grade = new DataTable();
            //    grade = gradeBll.SelectAllGradeInfo();

            //    //绑定年级
            //    dp_Grades.Items.Clear();
            //    dp_Grades.DataTextField = "NJMC";
            //    dp_Grades.DataValueField = "NJ";
            //    dp_Grades.DataSource = grade;
            //    dp_Grades.DataBind();
            //}
            //catch (Exception ex)
            //{
            //    LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            //}
        }
        /// <summary>
        /// 绑定班级
        /// </summary>
        private void BindClass()
        {
            //try
            //{
            //    Base_ClassBLL ClassBll = new Base_ClassBLL();
            //    DataTable dt = new DataTable();
            //    dt = ClassBll.GetClassByGradeID(dp_Grades.SelectedValue);
            //    dp_Class.Items.Clear();
            //    dp_Class.DataTextField = "BJ";
            //    dp_Class.DataValueField = "BJBH";
            //    dp_Class.DataSource = dt;
            //    dp_Class.DataBind();
            //}
            //catch (Exception ex)
            //{
            //    LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            //}
        }


        /// <summary>
        /// 对用户信息进行加载  
        /// </summary>
        public void DisplayDetails()
        {
            try
            {
                if (!string.IsNullOrEmpty(UserId))//判断为修改操作
                {
                    //修改时 主键不允许改  
                    // tb_UserIdentity.Enabled = false;
                    // dp_IdentityDocuments.Enabled = false;

                    DataTable dtSingleStu = stuBll.GetSingleStuInfoById(UserId);
                    if (dtSingleStu.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtSingleStu.Rows)
                        {
                            this.tb_RealName.Text = dr["xm"].ToString();
                            tb_SpellName.Text = dr["xmpy"].ToString();
                            txtywxm.Text = dr["ywxm"].ToString();
                            this.tb_ContactTel.Text = dr["lxdh"].ToString();
                            //this.dp_Sex.SelectedIndex = this.dp_Sex.Items.IndexOf(this.dp_Sex.Items.FindByValue(dr["xbm"].ToString()));
                            this.rblXB.SelectedValue = dr["xbm"].ToString();
                            this.dp_PoliticsStatus.SelectedIndex = this.dp_PoliticsStatus.Items.IndexOf(this.dp_PoliticsStatus.Items.FindByValue(dr["zzmmm"].ToString()));
                            this.dp_IdentityDocuments.SelectedValue = dr["sfzjlxm"].ToString();
                            this.tb_UserIdentity.Text = dr["sfzjh"].ToString();
                            //this.dp_Nation.SelectedIndex = this.dp_Nation.Items.IndexOf(this.dp_Nation.Items.FindByValue(dr["mzm"].ToString()));
                            this.txtmzm.Text = dr["mzm"].ToString();
                            //this.txtGB.Text = dr["gb"].ToString();
                            // this.dp_NativePlace.SelectedIndex = this.dp_NativePlace.Items.IndexOf(this.dp_NativePlace.Items.FindByValue(dr["gb"].ToString()));
                            this.tb_Birthday.Text = Convert.ToDateTime(dr["csrq"]).ToString("yyyy-MM-dd");
                            this.tb_BirthPlace.Text = dr["csdm"].ToString();
                            //this.tb_PostalAddress.Text = dr["txdz"].ToString();
                            //this.tb_PostalCode.Text = dr["yzbm"].ToString();
                            //   年级 班号
                            txtdzyx.Text = dr["dzxx"].ToString();
                            txtxjh.Text = dr["xjh"].ToString();
                            this.tb_CurrentAddress.Text = dr["xzz"].ToString();
                            //txtxzzszqx.Text = dr["xzzssqx"].ToString();

                            //dp_DepartMent.SelectedIndex = -1;
                            //dp_DepartMent.Items.FindByValue(dr["xxzzjgh"].ToString()).Selected = true;
                            //this.dp_DepartMent.SelectedIndex = this.dp_DepartMent.Items.IndexOf(this.dp_DepartMent.Items.FindByValue(dr["xxzzjgh"].ToString()));

                            ////加载年级 然后绑定
                            //this.dp_Grades.SelectedIndex = this.dp_Grades.Items.IndexOf(this.dp_Grades.Items.FindByValue(dr["nj"].ToString()));
                            //this.dp_Class.SelectedIndex = this.dp_Class.Items.IndexOf(this.dp_Class.Items.FindByValue(dr["bh"].ToString()));
                            //学生类别
                            Dp_xslb.SelectedValue = dr["xslbm"].ToString();
                            //Dp_xszt.SelectedIndex = this.Dp_xszt.Items.IndexOf(this.Dp_xszt.Items.FindByValue(dr["xszt"].ToString()));

                            //this.tb_ResidentCity.Text = dr["hkszd"].ToString();
                            //txthkszdqx.Text = dr["hkszdqx"].ToString();

                            //this.dp_HouseholdType.SelectedIndex = this.dp_HouseholdType.Items.IndexOf(this.dp_HouseholdType.Items.FindByValue(dr["hkxzm"].ToString()));
                            //this.dp_IsFloatingPeople.SelectedIndex = this.dp_IsFloatingPeople.Items.IndexOf(this.dp_IsFloatingPeople.Items.FindByValue(dr["sfldrk"].ToString()));
                            //this.dp_IsLocalTreat.SelectedIndex = this.dp_IsLocalTreat.Items.IndexOf(this.dp_IsLocalTreat.Items.FindByValue(dr["sfsbsrk"].ToString()));

                            //是否特长生
                            //this.dp_IsSpecialty.SelectedIndex = this.dp_IsSpecialty.Items.IndexOf(this.dp_IsSpecialty.Items.FindByValue(dr["sfstcs"].ToString()));
                            //this.Dp_jdfs.SelectedIndex = this.Dp_jdfs.Items.IndexOf(this.Dp_jdfs.Items.FindByValue(dr["jdfs"].ToString()));
                            //this.dp_HealthCondition.SelectedIndex = this.dp_HealthCondition.Items.IndexOf(this.dp_HealthCondition.Items.FindByValue(dr["jkzkm"].ToString()));
                            //过敏史
                            //this.tb_Allergies.Text = dr["gms"].ToString();
                            //this.tb_MedicalHistory.Text = dr["jwbs"].ToString();
                            //this.tb_EduID.Text = dr["jybh"].ToString();
                            //this.tb_AdmissionDate.Text = Convert.ToDateTime(dr["rxny"]).ToString("yyyy-MM-dd");

                            //原学校名称
                            //this.tb_UsedSchName.Text = dr["yxxmc"].ToString(); //dr["xslybh"].ToString();
                            //txtrxfsm.Text = dr["rxfsm"].ToString();//入学方式
                            //txtxslymSource.Text = dr["jdfsm"].ToString();  //就读方式
                            //txtlydqm.Text = dr["lydqm"].ToString();
                            //txtlydq.Text = dr["lydq"].ToString();
                            //家庭信息
                            //this.tb_FartherName.Text = dr["fqxm"].ToString();
                            //this.tb_WorkUnit1.Text = dr["fqdw"].ToString();
                            //this.tb_Telephone1.Text = dr["fqdh"].ToString();
                            //this.tb_MotherName.Text = dr["mqxm"].ToString();
                            //this.tb_WorkUnit2.Text = dr["mqdw"].ToString();
                            //this.tb_Telephone2.Text = dr["mqdh"].ToString();
                            //this.tb_GuardianName.Text = dr["jhrxm"].ToString();
                            //this.tb_GuardianWorkPlace.Text = dr["jhrgzdw"].ToString();
                            //this.tb_Guardianship.Text = dr["jhrgx"].ToString();
                            //this.tb_GuardianTele.Text = dr["jhrlxdh"].ToString();
                            //this.tb_GuardianDuty.Text = dr["jhrzw"].ToString();
                            //备注
                            this.txtbz.Text = dr["bz"].ToString();
                            //绑定学校组织机构下的年级
                            //string strDept = dp_DepartMent.SelectedItem.Value;
                            string strDept = "";
                            if (!string.IsNullOrEmpty(strDept))
                            {
                                //Base_GradeBLL gradeBll = new Base_GradeBLL();
                                //DataTable grade = gradeBll.SelectGradeByJGH(strDept);//年级+学科
                                // DataTable dtGrade = stuBll.GetGradeNameByDepartID(strDept);
                                //foreach (DataRow drn in grade.Rows)
                                //{
                                //    this.dp_Grades.Items.Add(new ListItem(drn["njmc"].ToString(), drn["bz"].ToString()));
                                //}
                                //this.dp_Grades.Items.Insert(0, new ListItem("-年级-", "0|0|0"));
                                //this.dp_Class.Items.Clear();
                                //this.dp_Class.Items.Insert(0, new ListItem("-班级-", "0|0|0"));
                            }
                            else
                            {
                                //this.dp_Class.Items.Clear();
                                //this.dp_Class.Items.Insert(0, new ListItem("-班级-", "0|0|0"));
                            } 
                            //选中年级
                            //this.dp_Grades.SelectedIndex = this.dp_Grades.Items.IndexOf(this.dp_Grades.Items.FindByValue(ChangeVal(dr["xd"].ToString(), dr["nj"].ToString())));
                            //dp_Grades.SelectedValue = dr["nj"].ToString();
                            // this.dp_Grades.SelectedIndex = this.dp_Grades.Items.IndexOf(this.dp_Grades.Items.FindByValue(dr["nj"].ToString()));
                            //绑定 年级下的班级 
                            //string strNj = dp_Grades.SelectedItem.Value;
                            //if (!string.IsNullOrEmpty(strNj) && !string.IsNullOrEmpty(strDept))
                            //{
                                //DataTable dtClass = stuBll.GetClassNameByGradeID(strDept, strNj.Split('|')[2]);
                                //foreach (DataRow drg in dtClass.Rows)
                                //{
                                //    this.dp_Class.Items.Add(new ListItem(drg["bj"].ToString(), drg["bh"].ToString().Substring(drg["bh"].ToString().Length - 1, 1)));
                                //}
                            //}
                            //选中班级
                            //this.dp_Class.SelectedIndex = this.dp_Class.Items.IndexOf(this.dp_Class.Items.FindByValue(dr["bh"].ToString()));
                            //dp_Class.SelectedValue = dr["bh"].ToString();
                            string gd = dr["P_id"].ToString();
                            if (string.IsNullOrEmpty(gd) || gd == "否")
                            {
                                gd = "0";
                            }
                            else
                            {
                                gd = "1";
                            }
                            ddlGD1.SelectedValue = gd;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }
        /// <summary>
        /// 用户中心--编辑保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Base_Student stu = new Base_Student();
            int intResult = 0;
            try
            {
                //if (dp_IdentityDocuments.SelectedItem.Value == "")
                //{
                //    ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'> alert('请选择证件类型！'); </script>");

                //    return;

                //}
                //新增时判断
                //if (tb_UserIdentity.Text.Trim() == "")
                //{
                //    ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'> alert('证件号不能为空！'); </script>");

                //    return;
                //}
                //else
                //{
                //    //验证身份证号，自动生成的身份证(带Y)不许验证
                //    if (tb_UserIdentity.Text.Trim().IndexOf('Y') <= -1)
                //    {
                //        bool boolidcard = DAL.ValidatorHelper.CheckIDCard(tb_UserIdentity.Text.Trim());
                //        if (!boolidcard)
                //        {
                //            ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'> alert('身份证号码有误！'); </script>");
                //            return;
                //        }
                //    }
                //}

                if (tb_RealName.Text.Trim() == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'> alert('姓名不能为空！'); </script>");
                    return;
                }
                //if (dp_PoliticsStatus.SelectedItem.Value == "")
                //{
                //    ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'> alert('请选择政治面貌！'); </script>");
                //    return;
                //}
                //if (this.txtmzm.Text == "")
                //{
                //    ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'> alert('民族不能为空！'); </script>");
                //    return;
                //}
                stu=GetModel(this.tb_UserIdentity.Text);
                stu.XM = this.tb_RealName.Text;
                stu.XMPY = tb_SpellName.Text;
                stu.YWXM = txtywxm.Text;
                stu.LXDH = this.tb_ContactTel.Text;
                stu.XBM = this.rblXB.SelectedValue;

                stu.ZZMMM = this.dp_PoliticsStatus.SelectedItem.Value;
                stu.SFZJLXM = this.dp_IdentityDocuments.SelectedItem.Value;
                stu.SFZJH = this.tb_UserIdentity.Text;
                stu.MZM = this.txtmzm.Text;
                //stu.GB = this.txtGB.Text.Trim();
                stu.CSRQ = DAL.ConvertHelper.DateTime(this.tb_Birthday.Text).DateTimeResult;
                stu.CSDM = this.tb_BirthPlace.Text;
                //stu.TXDZ = this.tb_PostalAddress.Text;

                //if (!string.IsNullOrWhiteSpace(this.tb_PostalCode.Text.Trim()))
                //{
                //    bool boolIsYzbm = DAL.ValidatorHelper.IsInt(tb_PostalCode.Text.Trim());
                //    if (!boolIsYzbm)
                //    {
                //        ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'> alert('邮政编码不合法！'); </script>");
                //        return;
                //    }
                //}
                //stu.YZBM = this.tb_PostalCode.Text;
                if (!string.IsNullOrWhiteSpace(this.txtdzyx.Text.Trim()))
                {
                    bool boolIsDzxx = DAL.ValidatorHelper.IsEmail(txtdzyx.Text.Trim());
                    if (!boolIsDzxx)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'> alert('电子邮箱不合法！'); </script>");
                        return;
                    }
                }
                stu.DZXX = txtdzyx.Text;

                //  学号  年级 班号

                if (!string.IsNullOrWhiteSpace(this.txtxjh.Text.Trim()))
                {
                    bool boolIsXJH = DAL.ValidatorHelper.IsInt(txtxjh.Text.Trim());
                    if (!boolIsXJH)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'> alert('学籍号不合法，应为整数！'); </script>");
                        return;
                    }
                }
                stu.XJH = txtxjh.Text;
                stu.XZZ = this.tb_CurrentAddress.Text;
                //现住址 所属区县编号
                //if (!string.IsNullOrWhiteSpace(this.txtxzzszqx.Text.Trim()))
                //{
                //    bool boolSsqxbh = DAL.ValidatorHelper.IsInt(txtxzzszqx.Text.Trim());
                //    if (!boolSsqxbh)
                //    {
                //        ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'> alert('现住址所在区县编号不合法，应为整数！'); </script>");
                //        return;
                //    }
                //}
                //stu.XZZSSQX = txtxzzszqx.Text;
                //stu.XXZZJGH = this.dp_DepartMent.SelectedItem.Value;
                //stu.XXZZJGH = dp_DepartMent.SelectedItem.Value;
                //stu.XD = this.dp_Grades.SelectedItem.Value.Split('|')[0];//学段
                //stu.XD = this.dp_Grades.SelectedItem.Value;//学段
                //if (string.IsNullOrEmpty(dp_Grades.SelectedItem.Value))
                //{
                //    stu.NJ = "0";//没有年级的 暂存0
                //}
                //else
                //{
                //    //stu.NJ = this.dp_Grades.SelectedItem.Value.Split('|')[1];
                //    stu.NJ = this.dp_Grades.SelectedItem.Value;
                //}

                //if (string.IsNullOrEmpty(dp_Class.SelectedItem.Value))
                //{
                //    stu.BH = "0";//未分班
                //}
                //else
                //{
                //    stu.BH = this.dp_Class.SelectedItem.Value;
                //}
                //学生类别
                stu.XSLBM = Dp_xslb.SelectedItem.Value;
                //stu.XSZT = Dp_xszt.SelectedItem.Value;

                //stu.HKSZD = this.tb_ResidentCity.Text;
                //户口所在地
                //if (!string.IsNullOrWhiteSpace(this.txthkszdqx.Text.Trim()))
                //{
                //    bool boolHKSsqxbh = DAL.ValidatorHelper.IsInt(txthkszdqx.Text.Trim());
                //    if (!boolHKSsqxbh)
                //    {
                //        ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'> alert('户口所在地区县编号不合法，应为整数！'); </script>");
                //        return;
                //    }
                //}
                //stu.HKSZDQX = txthkszdqx.Text;
                //stu.HKXZM = this.dp_HouseholdType.SelectedItem.Value;
                //stu.SFLDRK = this.dp_IsFloatingPeople.SelectedItem.Value;
                //stu.SFSBSRK = this.dp_IsLocalTreat.SelectedItem.Value;

                //是否特长生
                //stu.SFSTCS = this.dp_IsSpecialty.SelectedItem.Value;
                //stu.JDFS = this.Dp_jdfs.SelectedItem.Value;
                //stu.JKZKM = this.dp_HealthCondition.SelectedItem.Value;
                //过敏史
                //stu.GMS = this.tb_Allergies.Text;
                //stu.JWBS = this.tb_MedicalHistory.Text;
                //教育id
                //if (!string.IsNullOrWhiteSpace(this.tb_EduID.Text.Trim()))
                //{
                //    bool boolJYID = DAL.ValidatorHelper.IsInt(tb_EduID.Text.Trim());
                //    if (!boolJYID)
                //    {
                //        ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'> alert('教育ID不合法，应为整数！'); </script>");
                //        return;
                //    }
                //}
                //stu.JYBH = this.tb_EduID.Text;
                //stu.RXNY = DAL.ConvertHelper.DateTime(this.tb_AdmissionDate.Text).DateTimeResult;



                //家庭信息
                //stu.FQXM = this.tb_FartherName.Text;
                //stu.FQDW = this.tb_WorkUnit1.Text;
                //stu.FQDH = this.tb_Telephone1.Text;
                //stu.MQXM = this.tb_MotherName.Text;
                //stu.MQDW = this.tb_WorkUnit2.Text;
                //stu.MQDH = this.tb_Telephone2.Text;
                //stu.JHRXM = this.tb_GuardianName.Text;
                //stu.JHRGZDW = this.tb_GuardianWorkPlace.Text;
                //stu.JHRGX = this.tb_Guardianship.Text;
                //stu.JHRLXDH = this.tb_GuardianTele.Text;
                //stu.JHRZW = this.tb_GuardianDuty.Text;
                //备注
                stu.BZ = this.txtbz.Text;
                //stu.YHZT = "0";//正常用户
                stu.P_id = ddlGD1.SelectedItem.Text;

                //学生来源表 操作 先删后增
                //if (this.tb_UsedSchName.Text != "")
                //{
                //    //删除
                //    string sql = " delete from base_stuSource where sfzjh='" + stu.SFZJH + "'";
                //    DAL.SqlHelper.ExecuteNonQuery(CommandType.Text, sql);
                //    //新增
                //    string strid = Guid.NewGuid().ToString();
                //    string strsfzjh = stu.SFZJH;

                //    string stryxxmc = tb_UsedSchName.Text;
                //    string yxh = this.tb_EduID.Text;
                //    string strrxfs = this.txtrxfsm.Text;
                //    string strtxtxslymSource = this.txtxslymSource.Text;
                //    string strtxtlydqm = this.txtlydqm.Text;
                //    //空：xslym
                //    string strtxtlydq = this.txtlydq.Text;
                //    stuBll.InsertStuSource(strid, strsfzjh, stryxxmc, yxh, strrxfs, strtxtxslymSource, strtxtlydqm, "", strtxtlydq);
                //    //原学校名称
                //    stu.XSLYBH = strid;
                //}
                //else
                //{
                //    //原学校名称
                //    stu.XSLYBH = "";
                //}
                ////绑定 tree
                //if (!string.IsNullOrWhiteSpace(stu.BH))
                //{
                //    Session["strTreeNode"] = stu.BH;
                //}
                //else
                //{
                //    Session["strTreeNode"] = hiddenschool.Value
                //        ;
                //}
                //if (!string.IsNullOrEmpty(UserId))
                //{
                    //修改
                    bool bbbb = Update(stu);

                    if (bbbb)
                    {
                        //this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('操作成功！');window.location=\"StudentList.aspx?xxzzjgh=" + hiddenschool.Value + "&&nj=" + hiddenGrade.Value + "&&bjbh=" + hiddenClass.Value + "\";</script>");
                        this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('操作成功！');</script>");
                    }
                    else
                    {
                        //this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('操作出现问题，请联系管理员!');window.location=\"StudentList.aspx?bjbh=" + stu.BH + "\";</script>");
                        //this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('操作出现问题，请联系管理员!');</script>");
                        this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('操作成功！');</script>");
                    }
                //}
                //else
                //{
                //    //增加
                //    intResult = stuBll.StudentInsert(stu);
                //    if (intResult > 0)
                //    {
                //        //this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('操作成功！');window.location=\"StudentList.aspx?bjbh=" + stu.BH + "\";</script>");
                //        this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('操作成功！');</script>");
                //    }
                //    else
                //    {
                //        //this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('操作出现问题，请联系管理员!');window.location=\"StudentList.aspx?bjbh=" + stu.BH + "\";</script>");
                //        //this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('操作出现问题，请联系管理员!');</script>");
                //        this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('操作成功！');</script>");
                //    }
                //}
            }
            catch (Exception ex)
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('操作成功！');</script>");
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        } 
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="xd">学段</param>
        /// <param name="nj">年级</param>
        /// <returns></returns>
        protected string ChangeVal(string xd, string nj)
        {
            string val = string.Empty;
            if (xd == "0")
            {
                if (nj == "1")
                    val = "0|1|-3";
                if (nj == "2")
                    val = "0|2|-2";
                if (nj == "3")
                    val = "0|3|-1";
            }
            else if (xd == "2")
            {
                if (nj == "1")
                    val = "2|1|7";
                if (nj == "2")
                    val = "2|2|8";
                if (nj == "3")
                    val = "2|3|9";
            }
            else if (xd == "3")
            {
                if (nj == "1")
                    val = "3|1|10";
                if (nj == "2")
                    val = "3|2|11";
                if (nj == "3")
                    val = "3|3|12";
            }
            else
                val = xd + "|" + nj + "|" + nj;
            return val;
        }
        protected void dp_GradesIndexChanged(object sender, EventArgs e)
        {
            try
            {

                BindClass();

                //Base_StudentBLL stuBll = new Base_StudentBLL();
                //this.dp_Class.Items.Clear();
                //string strNj = dp_Grades.SelectedItem.Value.Split('|')[2];
                ////string strDepart = this.dp_DepartMent.SelectedItem.Value;
                //string strDepart = "";
                //if (!string.IsNullOrEmpty(strNj))
                //{
                //    DataTable dtClass = stuBll.GetClassNameByGradeID(strDepart, strNj);
                //    foreach (DataRow dr in dtClass.Rows)
                //    {
                //        //this.dp_Class.Items.Add(new ListItem(dr["bj"].ToString(), dr["bjbh"].ToString()));
                //        this.dp_Class.Items.Add(new ListItem(dr["bj"].ToString(), dr["bh"].ToString().Substring(dr["bh"].ToString().Length - 1, 1)));
                //    }
                //    this.dp_Class.Items.Insert(0,new ListItem("-班级-", "0|0|0"));

                //}
                //else
                //{
                //    this.dp_Class.Items.Insert(0, new ListItem("-班级-", "0|0|0"));
                //}
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }
        protected void btnRandom_Click(object sender, EventArgs e)
        {
            tb_UserIdentity.Text = DAL.RandomHelper.GetRandomForStuSfzjh();
        }

        //protected void dp_IdentityDocuments_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (dp_IdentityDocuments.SelectedItem.Value == "居民身份证")
        //        {
        //            // btnRandom.Enabled = false;
        //            tb_UserIdentity.Text = "";
        //        }
        //        else if (dp_IdentityDocuments.SelectedItem.Value == "其他")
        //        {
        //            //btnRandom.Enabled = true; ;
        //        }
        //        else
        //        {
        //            // btnRandom.Enabled = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
        //    }
        //}

        protected void dp_DepartMent_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //this.dp_Grades.Items.Clear();
                ////string strDepart = this.dp_DepartMent.SelectedItem.Value;
                //string strDepart="";
                //if (!string.IsNullOrEmpty(strDepart))
                //{
                //    this.dp_Grades.Items.Add(new ListItem("-年级-", "0|0|0"));
                //    Base_GradeBLL gradeBll = new Base_GradeBLL();
                //    DataTable grade = gradeBll.SelectGradeByJGH(strDepart);//年级+学科
                //    //   DataTable dtGrade = stuBll.GetGradeNameByDepartID(strDepart);
                //    foreach (DataRow dr in grade.Rows)
                //    {
                //        this.dp_Grades.Items.Add(new ListItem(dr["njmc"].ToString(), dr["nj"].ToString()));
                //    }
                //    this.dp_Class.Items.Clear();
                //    this.dp_Class.Items.Add(new ListItem("-班级-", "0|0|0"));
                //}
                //else
                //{
                //    this.dp_Grades.Items.Add(new ListItem("-年级-", "0|0|0"));
                //    this.dp_Class.Items.Clear();
                //    this.dp_Class.Items.Add(new ListItem("-班级-", "0|0|0"));
                //}
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.Base_Student GetModel(string SFZJH)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 SFZJH,XH,YHZH,XXZZJGH,YHZT,XM,YWXM,XMPY,CYM,XBM,CSRQ,CSDM,JG,MZM,GJDQM,SFZJLXM,HYZKM,GATQWM,ZZMMM,JKZKM,XYZJM,XXM,ZP,SFZJYXQ,SFDSZN,NJ,BH,XD,P_id,XSLBM,XZZ,XZZSSQX,HKSZD,HKSZDQX,HKXZM,SFLDRK,JDFS,GB,TC,LXDH,TXDZ,YZBM,DZXX,RXCJ,ZYDZ,XJH,SFSBSRK,SFSTCS,RYQRM,JLXJYJ,CLZM,ZKKH,GKKH,BYKH,YXJH,XSZT,RXNY,XSLYBH,SXZKZH,GMS,JWBS,JYBH,FQXM,FZGX,FQDW,FQDH,MQXM,MZGX,MQDW,MQDH,JHRXM,JHRGX,JHRGZDW,JHRLXDH,ZJDLSJ,JHRZW,DLIP,DLBSM,XGSJ,BZ,SJBZ,FK,FP,ZY from Base_Student ");
            strSql.Append(" where SFZJH=@SFZJH ");
            SqlParameter[] parameters = {
					new SqlParameter("@SFZJH", SqlDbType.NVarChar,20)			};
            parameters[0].Value = SFZJH;

            Model.Base_Student model = new Model.Base_Student();
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.Base_Student DataRowToModel(DataRow row)
        {
            Model.Base_Student model = new Model.Base_Student();
            if (row != null)
            {
                if (row["SFZJH"] != null)
                {
                    model.SFZJH = row["SFZJH"].ToString();
                }
                if (row["XH"] != null)
                {
                    model.XH = row["XH"].ToString();
                }
                if (row["YHZH"] != null)
                {
                    model.YHZH = row["YHZH"].ToString();
                }
                if (row["XXZZJGH"] != null && row["XXZZJGH"].ToString() != "")
                {
                    model.XXZZJGH = row["XXZZJGH"].ToString();
                }
                if (row["YHZT"] != null)
                {
                    model.YHZT = row["YHZT"].ToString();
                }
                if (row["XM"] != null)
                {
                    model.XM = row["XM"].ToString();
                }
                if (row["YWXM"] != null)
                {
                    model.YWXM = row["YWXM"].ToString();
                }
                if (row["XMPY"] != null)
                {
                    model.XMPY = row["XMPY"].ToString();
                }
                if (row["CYM"] != null)
                {
                    model.CYM = row["CYM"].ToString();
                }
                if (row["XBM"] != null)
                {
                    model.XBM = row["XBM"].ToString();
                }
                if (row["CSRQ"] != null && row["CSRQ"].ToString() != "")
                {
                    model.CSRQ = DateTime.Parse(row["CSRQ"].ToString());
                }
                if (row["CSDM"] != null)
                {
                    model.CSDM = row["CSDM"].ToString();
                }
                if (row["JG"] != null)
                {
                    model.JG = row["JG"].ToString();
                }
                if (row["MZM"] != null)
                {
                    model.MZM = row["MZM"].ToString();
                }
                if (row["GJDQM"] != null)
                {
                    model.GJDQM = row["GJDQM"].ToString();
                }
                if (row["SFZJLXM"] != null)
                {
                    model.SFZJLXM = row["SFZJLXM"].ToString();
                }
                if (row["HYZKM"] != null)
                {
                    model.HYZKM = row["HYZKM"].ToString();
                }
                if (row["GATQWM"] != null)
                {
                    model.GATQWM = row["GATQWM"].ToString();
                }
                if (row["ZZMMM"] != null)
                {
                    model.ZZMMM = row["ZZMMM"].ToString();
                }
                if (row["JKZKM"] != null)
                {
                    model.JKZKM = row["JKZKM"].ToString();
                }
                if (row["XYZJM"] != null)
                {
                    model.XYZJM = row["XYZJM"].ToString();
                }
                if (row["XXM"] != null)
                {
                    model.XXM = row["XXM"].ToString();
                }
                if (row["ZP"] != null)
                {
                    model.ZP = row["ZP"].ToString();
                }
                if (row["SFZJYXQ"] != null && row["SFZJYXQ"].ToString() != "")
                {
                    model.SFZJYXQ = DateTime.Parse(row["SFZJYXQ"].ToString());
                }
                if (row["SFDSZN"] != null)
                {
                    model.SFDSZN = row["SFDSZN"].ToString();
                }
                if (row["NJ"] != null)
                {
                    model.NJ = row["NJ"].ToString();
                }
                if (row["BH"] != null)
                {
                    model.BH = row["BH"].ToString();
                }
                if (row["XD"] != null)
                {
                    model.XD = row["XD"].ToString();
                }
                if (row["P_id"] != null)
                {
                    model.P_id = row["P_id"].ToString();
                }
                if (row["XSLBM"] != null)
                {
                    model.XSLBM = row["XSLBM"].ToString();
                }
                if (row["XZZ"] != null)
                {
                    model.XZZ = row["XZZ"].ToString();
                }
                if (row["XZZSSQX"] != null)
                {
                    model.XZZSSQX = row["XZZSSQX"].ToString();
                }
                if (row["HKSZD"] != null)
                {
                    model.HKSZD = row["HKSZD"].ToString();
                }
                if (row["HKSZDQX"] != null)
                {
                    model.HKSZDQX = row["HKSZDQX"].ToString();
                }
                if (row["HKXZM"] != null)
                {
                    model.HKXZM = row["HKXZM"].ToString();
                }
                if (row["SFLDRK"] != null)
                {
                    model.SFLDRK = row["SFLDRK"].ToString();
                }
                if (row["JDFS"] != null)
                {
                    model.JDFS = row["JDFS"].ToString();
                }
                if (row["GB"] != null)
                {
                    model.GB = row["GB"].ToString();
                }
                if (row["TC"] != null)
                {
                    model.TC = row["TC"].ToString();
                }
                if (row["LXDH"] != null)
                {
                    model.LXDH = row["LXDH"].ToString();
                }
                if (row["TXDZ"] != null)
                {
                    model.TXDZ = row["TXDZ"].ToString();
                }
                if (row["YZBM"] != null)
                {
                    model.YZBM = row["YZBM"].ToString();
                }
                if (row["DZXX"] != null)
                {
                    model.DZXX = row["DZXX"].ToString();
                }
                if (row["RXCJ"] != null)
                {
                    model.RXCJ = row["RXCJ"].ToString();
                }
                if (row["ZYDZ"] != null)
                {
                    model.ZYDZ = row["ZYDZ"].ToString();
                }
                if (row["XJH"] != null)
                {
                    model.XJH = row["XJH"].ToString();
                }
                if (row["SFSBSRK"] != null)
                {
                    model.SFSBSRK = row["SFSBSRK"].ToString();
                }
                if (row["SFSTCS"] != null)
                {
                    model.SFSTCS = row["SFSTCS"].ToString();
                }
                if (row["RYQRM"] != null)
                {
                    model.RYQRM = row["RYQRM"].ToString();
                }
                if (row["JLXJYJ"] != null)
                {
                    model.JLXJYJ = row["JLXJYJ"].ToString();
                }
                if (row["CLZM"] != null)
                {
                    model.CLZM = row["CLZM"].ToString();
                }
                if (row["ZKKH"] != null)
                {
                    model.ZKKH = row["ZKKH"].ToString();
                }
                if (row["GKKH"] != null)
                {
                    model.GKKH = row["GKKH"].ToString();
                }
                if (row["BYKH"] != null)
                {
                    model.BYKH = row["BYKH"].ToString();
                }
                if (row["YXJH"] != null)
                {
                    model.YXJH = row["YXJH"].ToString();
                }
                if (row["XSZT"] != null)
                {
                    model.XSZT = row["XSZT"].ToString();
                }
                if (row["RXNY"] != null && row["RXNY"].ToString() != "")
                {
                    model.RXNY = DateTime.Parse(row["RXNY"].ToString());
                }
                if (row["XSLYBH"] != null)
                {
                    model.XSLYBH = row["XSLYBH"].ToString();
                }
                if (row["SXZKZH"] != null)
                {
                    model.SXZKZH = row["SXZKZH"].ToString();
                }
                if (row["GMS"] != null)
                {
                    model.GMS = row["GMS"].ToString();
                }
                if (row["JWBS"] != null)
                {
                    model.JWBS = row["JWBS"].ToString();
                }
                if (row["JYBH"] != null)
                {
                    model.JYBH = row["JYBH"].ToString();
                }
                if (row["FQXM"] != null)
                {
                    model.FQXM = row["FQXM"].ToString();
                }
                if (row["FZGX"] != null)
                {
                    model.FZGX = row["FZGX"].ToString();
                }
                if (row["FQDW"] != null)
                {
                    model.FQDW = row["FQDW"].ToString();
                }
                if (row["FQDH"] != null)
                {
                    model.FQDH = row["FQDH"].ToString();
                }
                if (row["MQXM"] != null)
                {
                    model.MQXM = row["MQXM"].ToString();
                }
                if (row["MZGX"] != null)
                {
                    model.MZGX = row["MZGX"].ToString();
                }
                if (row["MQDW"] != null)
                {
                    model.MQDW = row["MQDW"].ToString();
                }
                if (row["MQDH"] != null)
                {
                    model.MQDH = row["MQDH"].ToString();
                }
                if (row["JHRXM"] != null)
                {
                    model.JHRXM = row["JHRXM"].ToString();
                }
                if (row["JHRGX"] != null)
                {
                    model.JHRGX = row["JHRGX"].ToString();
                }
                if (row["JHRGZDW"] != null)
                {
                    model.JHRGZDW = row["JHRGZDW"].ToString();
                }
                if (row["JHRLXDH"] != null)
                {
                    model.JHRLXDH = row["JHRLXDH"].ToString();
                }
                if (row["ZJDLSJ"] != null && row["ZJDLSJ"].ToString() != "")
                {
                    model.ZJDLSJ = DateTime.Parse(row["ZJDLSJ"].ToString());
                }
                if (row["JHRZW"] != null)
                {
                    model.JHRZW = row["JHRZW"].ToString();
                }
                if (row["DLIP"] != null)
                {
                    model.DLIP = row["DLIP"].ToString();
                }
                if (row["DLBSM"] != null)
                {
                    model.DLBSM = row["DLBSM"].ToString();
                }
                if (row["XGSJ"] != null && row["XGSJ"].ToString() != "")
                {
                    model.XGSJ = DateTime.Parse(row["XGSJ"].ToString());
                }
                if (row["BZ"] != null)
                {
                    model.BZ = row["BZ"].ToString();
                }
                if (row["ZY"] != null && row["ZY"].ToString() != "")
                {
                    model.ZY = int.Parse(row["ZY"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.Base_Student model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Base_Student set ");
            strSql.Append("XH=@XH,");
            strSql.Append("YHZH=@YHZH,");
            strSql.Append("XXZZJGH=@XXZZJGH,");
            strSql.Append("YHZT=@YHZT,");
            strSql.Append("XM=@XM,");
            strSql.Append("YWXM=@YWXM,");
            strSql.Append("XMPY=@XMPY,");
            strSql.Append("CYM=@CYM,");
            strSql.Append("XBM=@XBM,");
            strSql.Append("CSRQ=@CSRQ,");
            strSql.Append("CSDM=@CSDM,");
            strSql.Append("JG=@JG,");
            strSql.Append("MZM=@MZM,");
            strSql.Append("GJDQM=@GJDQM,");
            strSql.Append("SFZJLXM=@SFZJLXM,");
            strSql.Append("HYZKM=@HYZKM,");
            strSql.Append("GATQWM=@GATQWM,");
            strSql.Append("ZZMMM=@ZZMMM,");
            strSql.Append("JKZKM=@JKZKM,");
            strSql.Append("XYZJM=@XYZJM,");
            strSql.Append("XXM=@XXM,");
            strSql.Append("ZP=@ZP,");
            strSql.Append("SFZJYXQ=@SFZJYXQ,");
            strSql.Append("SFDSZN=@SFDSZN,");
            strSql.Append("NJ=@NJ,");
            strSql.Append("BH=@BH,");
            strSql.Append("XD=@XD,");
            strSql.Append("P_id=@P_id,");
            strSql.Append("XSLBM=@XSLBM,");
            strSql.Append("XZZ=@XZZ,");
            strSql.Append("XZZSSQX=@XZZSSQX,");
            strSql.Append("HKSZD=@HKSZD,");
            strSql.Append("HKSZDQX=@HKSZDQX,");
            strSql.Append("HKXZM=@HKXZM,");
            strSql.Append("SFLDRK=@SFLDRK,");
            strSql.Append("JDFS=@JDFS,");
            strSql.Append("GB=@GB,");
            strSql.Append("TC=@TC,");
            strSql.Append("LXDH=@LXDH,");
            strSql.Append("TXDZ=@TXDZ,");
            strSql.Append("YZBM=@YZBM,");
            strSql.Append("DZXX=@DZXX,");
            strSql.Append("RXCJ=@RXCJ,");
            strSql.Append("ZYDZ=@ZYDZ,");
            strSql.Append("XJH=@XJH,");
            strSql.Append("SFSBSRK=@SFSBSRK,");
            strSql.Append("SFSTCS=@SFSTCS,");
            strSql.Append("RYQRM=@RYQRM,");
            strSql.Append("JLXJYJ=@JLXJYJ,");
            strSql.Append("CLZM=@CLZM,");
            strSql.Append("ZKKH=@ZKKH,");
            strSql.Append("GKKH=@GKKH,");
            strSql.Append("BYKH=@BYKH,");
            strSql.Append("YXJH=@YXJH,");
            strSql.Append("XSZT=@XSZT,");
            strSql.Append("RXNY=@RXNY,");
            strSql.Append("XSLYBH=@XSLYBH,");
            strSql.Append("SXZKZH=@SXZKZH,");
            strSql.Append("GMS=@GMS,");
            strSql.Append("JWBS=@JWBS,");
            strSql.Append("JYBH=@JYBH,");
            strSql.Append("FQXM=@FQXM,");
            strSql.Append("FZGX=@FZGX,");
            strSql.Append("FQDW=@FQDW,");
            strSql.Append("FQDH=@FQDH,");
            strSql.Append("MQXM=@MQXM,");
            strSql.Append("MZGX=@MZGX,");
            strSql.Append("MQDW=@MQDW,");
            strSql.Append("MQDH=@MQDH,");
            strSql.Append("JHRXM=@JHRXM,");
            strSql.Append("JHRGX=@JHRGX,");
            strSql.Append("JHRGZDW=@JHRGZDW,");
            strSql.Append("JHRLXDH=@JHRLXDH,");
            strSql.Append("ZJDLSJ=@ZJDLSJ,");
            strSql.Append("JHRZW=@JHRZW,");
            strSql.Append("DLIP=@DLIP,");
            strSql.Append("DLBSM=@DLBSM,");
            strSql.Append("XGSJ=@XGSJ,");
            strSql.Append("BZ=@BZ,");
            strSql.Append("ZY=@ZY");
            strSql.Append(" where SFZJH=@SFZJH ");
            SqlParameter[] parameters = {
					new SqlParameter("@XH", SqlDbType.NVarChar,20),
					new SqlParameter("@YHZH", SqlDbType.NVarChar,36),
					new SqlParameter("@XXZZJGH", SqlDbType.Int,4),
					new SqlParameter("@YHZT", SqlDbType.NVarChar,10),
					new SqlParameter("@XM", SqlDbType.NVarChar,50),
					new SqlParameter("@YWXM", SqlDbType.NVarChar,50),
					new SqlParameter("@XMPY", SqlDbType.NVarChar,50),
					new SqlParameter("@CYM", SqlDbType.NVarChar,50),
					new SqlParameter("@XBM", SqlDbType.NVarChar,10),
					new SqlParameter("@CSRQ", SqlDbType.DateTime2,3),
					new SqlParameter("@CSDM", SqlDbType.NVarChar,20),
					new SqlParameter("@JG", SqlDbType.NVarChar,50),
					new SqlParameter("@MZM", SqlDbType.NVarChar,20),
					new SqlParameter("@GJDQM", SqlDbType.NVarChar,20),
					new SqlParameter("@SFZJLXM", SqlDbType.NVarChar,20),
					new SqlParameter("@HYZKM", SqlDbType.NVarChar,50),
					new SqlParameter("@GATQWM", SqlDbType.NVarChar,50),
					new SqlParameter("@ZZMMM", SqlDbType.NVarChar,20),
					new SqlParameter("@JKZKM", SqlDbType.NVarChar,20),
					new SqlParameter("@XYZJM", SqlDbType.NVarChar,10),
					new SqlParameter("@XXM", SqlDbType.NVarChar,10),
					new SqlParameter("@ZP", SqlDbType.NVarChar,50),
					new SqlParameter("@SFZJYXQ", SqlDbType.DateTime2,3),
					new SqlParameter("@SFDSZN", SqlDbType.NVarChar,2),
					new SqlParameter("@NJ", SqlDbType.NVarChar,36),
					new SqlParameter("@BH", SqlDbType.NVarChar,36),
					new SqlParameter("@XD", SqlDbType.NVarChar,36),
					new SqlParameter("@P_id", SqlDbType.NVarChar,36),
					new SqlParameter("@XSLBM", SqlDbType.NVarChar,10),
					new SqlParameter("@XZZ", SqlDbType.NVarChar,50),
					new SqlParameter("@XZZSSQX", SqlDbType.NVarChar,50),
					new SqlParameter("@HKSZD", SqlDbType.NVarChar,50),
					new SqlParameter("@HKSZDQX", SqlDbType.NVarChar,50),
					new SqlParameter("@HKXZM", SqlDbType.NVarChar,50),
					new SqlParameter("@SFLDRK", SqlDbType.NVarChar,10),
					new SqlParameter("@JDFS", SqlDbType.NVarChar,10),
					new SqlParameter("@GB", SqlDbType.NVarChar,50),
					new SqlParameter("@TC", SqlDbType.NVarChar,50),
					new SqlParameter("@LXDH", SqlDbType.NVarChar,20),
					new SqlParameter("@TXDZ", SqlDbType.NVarChar,50),
					new SqlParameter("@YZBM", SqlDbType.NVarChar,10),
					new SqlParameter("@DZXX", SqlDbType.NVarChar,50),
					new SqlParameter("@RXCJ", SqlDbType.NVarChar,50),
					new SqlParameter("@ZYDZ", SqlDbType.NVarChar,50),
					new SqlParameter("@XJH", SqlDbType.NVarChar,50),
					new SqlParameter("@SFSBSRK", SqlDbType.NVarChar,10),
					new SqlParameter("@SFSTCS", SqlDbType.NVarChar,10),
					new SqlParameter("@RYQRM", SqlDbType.NVarChar,50),
					new SqlParameter("@JLXJYJ", SqlDbType.NVarChar,50),
					new SqlParameter("@CLZM", SqlDbType.NVarChar,50),
					new SqlParameter("@ZKKH", SqlDbType.NVarChar,50),
					new SqlParameter("@GKKH", SqlDbType.NVarChar,50),
					new SqlParameter("@BYKH", SqlDbType.NVarChar,50),
					new SqlParameter("@YXJH", SqlDbType.NVarChar,50),
					new SqlParameter("@XSZT", SqlDbType.NVarChar,10),
					new SqlParameter("@RXNY", SqlDbType.DateTime2,3),
					new SqlParameter("@XSLYBH", SqlDbType.NVarChar,36),
					new SqlParameter("@SXZKZH", SqlDbType.NVarChar,20),
					new SqlParameter("@GMS", SqlDbType.NVarChar,200),
					new SqlParameter("@JWBS", SqlDbType.NVarChar,200),
					new SqlParameter("@JYBH", SqlDbType.NVarChar,50),
					new SqlParameter("@FQXM", SqlDbType.NVarChar,50),
					new SqlParameter("@FZGX", SqlDbType.NVarChar,10),
					new SqlParameter("@FQDW", SqlDbType.NVarChar,50),
					new SqlParameter("@FQDH", SqlDbType.NVarChar,50),
					new SqlParameter("@MQXM", SqlDbType.NVarChar,50),
					new SqlParameter("@MZGX", SqlDbType.NVarChar,10),
					new SqlParameter("@MQDW", SqlDbType.NVarChar,50),
					new SqlParameter("@MQDH", SqlDbType.NVarChar,50),
					new SqlParameter("@JHRXM", SqlDbType.NVarChar,50),
					new SqlParameter("@JHRGX", SqlDbType.NVarChar,20),
					new SqlParameter("@JHRGZDW", SqlDbType.NVarChar,50),
					new SqlParameter("@JHRLXDH", SqlDbType.NVarChar,50),
					new SqlParameter("@ZJDLSJ", SqlDbType.DateTime),
					new SqlParameter("@JHRZW", SqlDbType.NVarChar,50),
					new SqlParameter("@DLIP", SqlDbType.NVarChar,20),
					new SqlParameter("@DLBSM", SqlDbType.NVarChar,50),
					new SqlParameter("@XGSJ", SqlDbType.DateTime),
					new SqlParameter("@BZ", SqlDbType.NVarChar,200),
					new SqlParameter("@ZY", SqlDbType.Int,4),
					new SqlParameter("@SFZJH", SqlDbType.NVarChar,20)};
            parameters[0].Value = model.XH;
            parameters[1].Value = model.YHZH;
            parameters[2].Value = model.XXZZJGH;
            parameters[3].Value = model.YHZT;
            parameters[4].Value = model.XM;
            parameters[5].Value = model.YWXM;
            parameters[6].Value = model.XMPY;
            parameters[7].Value = model.CYM;
            parameters[8].Value = model.XBM;
            parameters[9].Value = model.CSRQ;
            parameters[10].Value = model.CSDM;
            parameters[11].Value = model.JG;
            parameters[12].Value = model.MZM;
            parameters[13].Value = model.GJDQM;
            parameters[14].Value = model.SFZJLXM;
            parameters[15].Value = model.HYZKM;
            parameters[16].Value = model.GATQWM;
            parameters[17].Value = model.ZZMMM;
            parameters[18].Value = model.JKZKM;
            parameters[19].Value = model.XYZJM;
            parameters[20].Value = model.XXM;
            parameters[21].Value = model.ZP;
            parameters[22].Value = model.SFZJYXQ;
            parameters[23].Value = model.SFDSZN;
            parameters[24].Value = model.NJ;
            parameters[25].Value = model.BH;
            parameters[26].Value = model.XD;
            parameters[27].Value = model.P_id;
            parameters[28].Value = model.XSLBM;
            parameters[29].Value = model.XZZ;
            parameters[30].Value = model.XZZSSQX;
            parameters[31].Value = model.HKSZD;
            parameters[32].Value = model.HKSZDQX;
            parameters[33].Value = model.HKXZM;
            parameters[34].Value = model.SFLDRK;
            parameters[35].Value = model.JDFS;
            parameters[36].Value = model.GB;
            parameters[37].Value = model.TC;
            parameters[38].Value = model.LXDH;
            parameters[39].Value = model.TXDZ;
            parameters[40].Value = model.YZBM;
            parameters[41].Value = model.DZXX;
            parameters[42].Value = model.RXCJ;
            parameters[43].Value = model.ZYDZ;
            parameters[44].Value = model.XJH;
            parameters[45].Value = model.SFSBSRK;
            parameters[46].Value = model.SFSTCS;
            parameters[47].Value = model.RYQRM;
            parameters[48].Value = model.JLXJYJ;
            parameters[49].Value = model.CLZM;
            parameters[50].Value = model.ZKKH;
            parameters[51].Value = model.GKKH;
            parameters[52].Value = model.BYKH;
            parameters[53].Value = model.YXJH;
            parameters[54].Value = model.XSZT;
            parameters[55].Value = model.RXNY;
            parameters[56].Value = model.XSLYBH;
            parameters[57].Value = model.SXZKZH;
            parameters[58].Value = model.GMS;
            parameters[59].Value = model.JWBS;
            parameters[60].Value = model.JYBH;
            parameters[61].Value = model.FQXM;
            parameters[62].Value = model.FZGX;
            parameters[63].Value = model.FQDW;
            parameters[64].Value = model.FQDH;
            parameters[65].Value = model.MQXM;
            parameters[66].Value = model.MZGX;
            parameters[67].Value = model.MQDW;
            parameters[68].Value = model.MQDH;
            parameters[69].Value = model.JHRXM;
            parameters[70].Value = model.JHRGX;
            parameters[71].Value = model.JHRGZDW;
            parameters[72].Value = model.JHRLXDH;
            parameters[73].Value = model.ZJDLSJ;
            parameters[74].Value = model.JHRZW;
            parameters[75].Value = model.DLIP;
            parameters[76].Value = model.DLBSM;
            parameters[77].Value = model.XGSJ;
            parameters[78].Value = model.BZ;
            parameters[79].Value = model.ZY;
            parameters[80].Value = model.SFZJH;

            int rows = SqlHelper.ExecuteNonQuery(CommandType.Text,strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}