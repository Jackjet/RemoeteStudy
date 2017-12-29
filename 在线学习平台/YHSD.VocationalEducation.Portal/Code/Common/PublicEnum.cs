using System;
using System.Data;
using System.Configuration;


namespace YHSD.VocationalEducation.Portal.Code.Common
{
    public class PublicEnum
    {
        /**
         * 是
         */
        public static string Yes = "1";

        /**
         * 否
         */
        public static string No = "0";


        public const string QuestionForSingle = "单选题";

        public const string QuestionForMulti = "多选题";

        public const string QuestionForJudge = "判断题";

        public const string QuestionForEssay = "简答题";
        //权限
        public static string PositionStudent = "Student";

        public static string PositionTeacher = "Teacher";

        public static string Positionadministrator = "administrator";

        public static string PositionClassTutor = "ClassTutor";

        //点击附件的TableName表名
        public static string Chapter = "Chapter";
        public static string StudyExperience = "StudyExperience";
       //全局图片路径
        public static string NoTouXiangUrl = "/_layouts/15/YHSD.VocationalEducation.Portal/images/NoTouXiang.png";

        public static string NoTuPianUrl = "/_layouts/15/YHSD.VocationalEducation.Portal/images/NoImage.png";

        public static string BoyUrl = "/_layouts/15/YHSD.VocationalEducation.Portal/images/boy.png";

        public static string GirlUrl = "/_layouts/15/YHSD.VocationalEducation.Portal/images/girl.png";
        #region Three System Const

        #region 学习中心

        /// <summary>
        /// 学习中心子站点URL
        /// </summary>
        public const string SystemStudentUrl = "/studenteducation";
        /// <summary>
        /// 学习中心子站点名称
        /// </summary>
        public const string SystemStudentWebName = "职教中心学习平台";
        #endregion

        #region 继续教育

        /// <summary>
        /// 继续教育子站点URL
        /// </summary>
        public const string SystemTeacherUrl = "/continuationeducation";
        /// <summary>
        /// 继续教育子站点名称
        /// </summary>
        public const string SystemTeacherWebName = "职教中心继续教育平台";
        #endregion

        #region 党员学习

        /// <summary>
        /// 党员学习子站点URL
        /// </summary>
        public const string SystemPartyMemberUrl = "/partymembereducation";
        /// <summary>
        /// 党员学习子站点名称
        /// </summary>
        public const string SystemPartyMemberWebName = "职教中心党员学习平台";
        #endregion

        #endregion
    }
}
