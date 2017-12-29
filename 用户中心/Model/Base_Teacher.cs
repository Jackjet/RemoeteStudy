using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 教师实体类
    /// </summary>
    public class Base_Teacher
    {
        /// <summary>
        /// 身份证号-主键
        /// </summary>
        public string SFZJH { get; set; }
        /// <summary>
        /// 用户账号
        /// </summary>
        public string YHZH { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string XM { get; set; }
        /// <summary>
        /// 最近登陆时间
        /// </summary>
        public DateTime? ZJDLSJ { get; set; }
        /// <summary>
        /// 登陆IP
        /// </summary>
        public string DLIP { get; set; }
        /// <summary>
        /// 登陆标识码
        /// </summary>
        public string DLBSM { get; set; }
        /// <summary>
        /// 学校组织机构号
        /// </summary>
        public string XXZZJGH { get; set; }
        /// <summary>
        /// 组织机构号
        /// </summary>
        public string ZZJGH { get; set; }
        /// <summary>
        /// 用户状态 
        /// </summary>
        public string YHZT { get; set; }
        /// <summary>
        /// 工号
        /// </summary>
        public string GH { get; set; }
        /// <summary>
        /// 英文姓名
        /// </summary>
        public string YWXM { get; set; }
        /// <summary>
        /// 姓名拼音
        /// </summary>
        public string XMPY { get; set; }
        /// <summary>
        /// 曾用名
        /// </summary>
        public string CYM { get; set; }
        /// <summary>
        /// 性别码
        /// </summary>
        public string XBM { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? CSRQ { get; set; }
        /// <summary>
        /// 出生地码
        /// </summary>
        public string CSDM { get; set; }
        /// <summary>
        /// 籍贯
        /// </summary>
        public string JG { get; set; }
        /// <summary>
        /// 民族码
        /// </summary>
        public string MZM { get; set; }
        /// <summary>
        /// 国籍/地区码
        /// </summary>
        public string GJDQM { get; set; }
        /// <summary>
        /// 身份证件类型码
        /// </summary>
        public string SFZJLXM { get; set; }
        /// <summary>
        /// 婚姻状况码
        /// </summary>
        public string HYZKM { get; set; }
        /// <summary>
        /// 港澳台侨外码
        /// </summary>
        public string GATQWM { get; set; }
        /// <summary>
        /// 政治面貌码
        /// </summary>
        public string ZZMMM { get; set; }
        /// <summary>
        /// 健康状况码
        /// </summary>
        public string JKZKM { get; set; }
        /// <summary>
        /// 信仰宗教码
        /// </summary>
        public string XYZJM { get; set; }
        /// <summary>
        /// 血型码
        /// </summary>
        public string XXM { get; set; }
        /// <summary>
        /// 照片
        /// </summary>
        public string ZP { get; set; }
        /// <summary>
        /// 身份证件有效期 
        /// </summary>
        public string SFZJYXQ { get; set; }
        /// <summary>
        /// 家庭住址
        /// </summary>
        public string JTZZ { get; set; }
        /// <summary>
        /// 现住址
        /// </summary>
        public string XZZ { get; set; }
        /// <summary>
        /// 户口所在地
        /// </summary>
        public string HKSZD { get; set; }
        /// <summary>
        /// 户口性质码
        /// </summary>
        public string HKXZM { get; set; }
        /// <summary>
        /// 学历码
        /// </summary>
        public string XLM { get; set; }
        /// <summary>
        /// 参加工作年月
        /// </summary>
        public DateTime? GZNY { get; set; }
        /// <summary>
        /// 来校年月
        /// </summary>
        public DateTime? LXNY { get; set; }
        /// <summary>
        /// 从教年月
        /// </summary>
        public DateTime? CJNY { get; set; }
        /// <summary>
        /// 编制类别码
        /// </summary>
        public string BZLBM { get; set; }
        /// <summary>
        /// 档案编号
        /// </summary>
        public string DABH { get; set; }
        /// <summary>
        /// 档案文本
        /// </summary>
        public string DAWB { get; set; }
        /// <summary>
        /// 通信地址
        /// </summary>
        public string TXDZ { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string LXDH { get; set; }
        /// <summary>
        /// 邮政编码
        /// </summary>
        public string YZBM { get; set; }
        /// <summary>
        /// 电子信箱
        /// </summary>
        public string DZXX { get; set; }
        /// <summary>
        /// 主页地址
        /// </summary>
        public string ZYDZ { get; set; }
        /// <summary>
        /// 特长
        /// </summary>
        public string TC { get; set; }
        /// <summary>
        /// 岗位职业码
        /// </summary>
        public string GWZYM { get; set; }
        /// <summary>
        /// 主要任课学段
        /// </summary>
        public string ZYRKXD { get; set; }
        /// <summary>
        /// 参加工作时间
        /// </summary>
        public DateTime? CJGZSJ { get; set; }
        /// <summary>
        /// 系列
        /// </summary>
        public string XL { get; set; }
        /// <summary>
        /// 职称
        /// </summary>
        public string ZC { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        public string JB { get; set; }
        /// <summary>
        /// 评定时间
        /// </summary>
        public DateTime? PDSJ { get; set; }
        /// <summary>
        /// 工资序列
        /// </summary>
        public string GZXL { get; set; }
        /// <summary>
        /// 等级
        /// </summary>
        public string DJ { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        public string LB { get; set; }
        /// <summary>
        /// 现岗位工资时间
        /// </summary>
        public DateTime? XGWGZSJ { get; set; }
        /// <summary>
        /// 现具体岗位
        /// </summary>
        public string XJTGW { get; set; }
        /// <summary>
        /// 兼课或兼职
        /// </summary>
        public string JKHJZ { get; set; }
        /// <summary>
        /// 周总课时数
        /// </summary>
        public string ZZKSH { get; set; }
        /// <summary>
        /// 骨干类别
        /// </summary>
        public string GGLB { get; set; }
        /// <summary>
        /// 教师资格证类别
        /// </summary>
        public string JSZKZLB { get; set; }
        /// <summary>
        /// 身份
        /// </summary>
        public string SF { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string BZ { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? XGSJ { get; set; }
        /// <summary>
        /// 年龄-自动计算
        /// </summary>
        public int AGE { get; set; }
        /// <summary>
        /// 家庭电话
        /// </summary>
        public string JTDH { get; set; }
        /// <summary>
        /// 机构号
        /// </summary>
        public string JGH { get; set; }
        /// <summary>
        /// 组织机构名称
        /// </summary>
        public string JGMC { get; set; }
        /// <summary>
        /// 组织机构简称
        /// </summary>
        public string JGJC { get; set; }
        /// <summary>
        ///教师负责的年级 + 课程
        /// </summary>
        public string GradeID { get; set; }
        /// <summary>
        /// 教师担任的学科
        /// </summary>
        public string SubjectID { get; set; }
        /// <summary>
        /// 教师 班主任  年级
        /// </summary>
        public string NJ { get; set; }
        /// <summary>
        /// 教师 班主任 班级
        /// </summary>
        public string BH { get; set; }
        //是否是管理员的标示
        public string QXBH { get; set; }
        public string XXMC { get; set; }
    }
}
