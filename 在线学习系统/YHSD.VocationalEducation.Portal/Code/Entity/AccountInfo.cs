using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YHSD.VocationalEducation.Portal.Code.Entity
{
    public class AccountInfo
    {
        public string ID { get; set; }
        public string CurriculumID { get; set; }  //课程id
        public string CurriculumName { get; set; }  //课程名称
        public string ResourceID { get; set; }   //课程分类id
        public string ResourceName { get; set; }  //课程分类名称
        public string PayUserID { get; set; }    //付费学员id
        public string Price { get; set; }       //付费金额
        public string Remarks { get; set; }     //备注
        public string Status { get; set; }      //0:待审批 1：已审批 
        public string PayStartTime { get; set; } //查询开始时间    
        public string PayEndTime { get; set; }   //查询结束时间
        public string PayTime { get; set; }      //付费时间
        public string IsDelete { get; set; }     //是否删除  0未删除  1已删除
    }
}
