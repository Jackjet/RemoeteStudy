using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YHSD.VocationalEducation.Portal.Code.Entity
{
    public class CertificateInfo
    {
        public string ID { get; set; }
        public string StuID { get; set; }        //学员id
        public string StuName { get; set; }        //学员姓名
        public string CurriculumID { get; set; }  //课程id
        public string CurriculumName { get; set; }  //课程名称
        public string ResourceID { get; set; }   //课程分类id
        public string ResourceName { get; set; }  //课程分类名称
        public string GraduationNo { get; set; }   //结业证书号
        public string GraduationDate { get; set; } //结业时间
        public string AwardUnit { get; set; }     //发证单位
        public string QueryURL { get; set; }      //证书查询网址
        public string CreateUser { get; set; }      //创建者
        public string CreateTime { get; set; }      //创建时间
        public string IsDelete { get; set; }     //是否删除  0未删除  1已删除
    }
}
