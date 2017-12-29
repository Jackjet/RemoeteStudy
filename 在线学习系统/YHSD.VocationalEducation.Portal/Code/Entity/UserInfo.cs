using System;

namespace YHSD.VocationalEducation.Portal.Code.Entity
{
    public class UserInfo
    {
        private String id;
        public String Id { get { return id; } set { id = value; } }

        private String code;
        public String Code { get { return code; } set { code = value; } }

        private String name;
        public String Name { get { return name; } set { name = value; } }

        private String domainAccount;
        public String DomainAccount { get { return domainAccount; } set { domainAccount = value; } }

        private String sex;
        public String Sex { get { return sex; } set { sex = value; } }

        private String birthday;
        public String Birthday { get { return birthday; } set { birthday = value; } }

        private String employedDate;
        public String EmployedDate { get { return employedDate; } set { employedDate = value; } }

        private String workDate;
        public String WorkDate { get { return workDate; } set { workDate = value; } }

        private String nationality;
        public String Nationality { get { return nationality; } set { nationality = value; } }

        private String party;
        public String Party { get { return party; } set { party = value; } }

        private String degree;
        public String Degree { get { return degree; } set { degree = value; } }

        private String houseHold;
        public String HouseHold { get { return houseHold; } set { houseHold = value; } }

        private String mobile;
        public String Mobile { get { return mobile; } set { mobile = value; } }

        private String telephone;
        public String Telephone { get { return telephone; } set { telephone = value; } }

        private String mSN;
        public String MSN { get { return mSN; } set { mSN = value; } }

        private String qQ;
        public String QQ { get { return qQ; } set { qQ = value; } }

        private String email;
        public String Email { get { return email; } set { email = value; } }

        private String emergencyContact;
        public String EmergencyContact { get { return emergencyContact; } set { emergencyContact = value; } }

        private String emergencyTel;
        public String EmergencyTel { get { return emergencyTel; } set { emergencyTel = value; } }

        private String address;
        public String Address { get { return address; } set { address = value; } }

        private String zipCode;
        public String ZipCode { get { return zipCode; } set { zipCode = value; } }

        private String photo;
        public String Photo { get { return photo; } set { photo = value; } }

        private String immediateSupervisor;
        public String ImmediateSupervisor { get { return immediateSupervisor; } set { immediateSupervisor = value; } }

        private String isDelete;
        public String IsDelete { get { return isDelete; } set { isDelete = value; } }

        private String nativePlace;
        public String NativePlace { get { return nativePlace; } set { nativePlace = value; } }

        private String health;
        public String Health { get { return health; } set { health = value; } }

        private String cardID;
        public String CardID { get { return cardID; } set { cardID = value; } }

        private String professional;
        public String Professional { get { return professional; } set { professional = value; } }

        private String staffLevel;
        public String StaffLevel { get { return staffLevel; } set { staffLevel = value; } }

        private String levelClass;
        public String LevelClass { get { return levelClass; } set { levelClass = value; } }

        private String probationEndDate;
        public String ProbationEndDate { get { return probationEndDate; } set { probationEndDate = value; } }

        private String laborContractStartDate;
        public String LaborContractStartDate { get { return laborContractStartDate; } set { laborContractStartDate = value; } }

        private String laborContractEndDate;
        public String LaborContractEndDate { get { return laborContractEndDate; } set { laborContractEndDate = value; } }

        private String laborContractType;
        public String LaborContractType { get { return laborContractType; } set { laborContractType = value; } }

        private String specialty;
        public String Specialty { get { return specialty; } set { specialty = value; } }

        private String photoOne;
        public String PhotoOne { get { return photoOne; } set { photoOne = value; } }

        private String photoTwo;
        public String PhotoTwo { get { return photoTwo; } set { photoTwo = value; } }
        private String sexName;
        public String SexName { get { return sexName; } set { sexName = value; } }
        private String deptName;
        public String DeptName { get { return deptName; } set { deptName = value; } }
        /// <summary>
        /// 用户角色 老师、学生...
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// 需要显示的角色ID
        /// </summary>
        public string RoleID { get; set; }
        /// <summary>
        /// 不需要显示的角色ID
        /// </summary>
        public string NotInRoleID { get; set; }
        /// <summary>
        /// 需要显示的班级ID
        /// </summary>
        public string ClassID { get; set; }
        /// <summary>
        /// 班级名称
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 不需要显示的班级ID
        /// </summary>
        public string NotInClassID { get; set; }
        /// <summary>
        /// 乡镇ID
        /// </summary>
        public string DeptID { get; set; }

        /// <summary>
        /// 学习进度
        /// </summary>
        public string Percentage { get; set; }

        /// <summary>
        /// 作业进度
        /// </summary>
        public string Worktage { get; set; }
        /// <summary>
        /// 批改进度
        /// </summary>
        public string Hometage { get; set; }
        /// <summary>
        /// 班级与学生的关联ID
        /// </summary>
        public string CUID { get; set; }

        /// <summary>
        /// 保存用户信息可以访问的系统的url,如 /SystemStudent,/SystemTeacher
        /// </summary>
        public string SystemStr { get; set; }
    }
}
