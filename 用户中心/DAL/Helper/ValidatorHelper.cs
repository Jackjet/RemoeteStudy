using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using Microsoft.VisualBasic;
using System.Reflection;

namespace DAL
{
    public class ValidatorHelper
    {
        /// <summary>
        /// 判断字符串是否与指定正则表达式匹配
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <param name="regularExp">正则表达式</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsMatch(string input, string regularExp)
        {
            return Regex.IsMatch(input, regularExp);
        }

        /// <summary>
        /// 验证非负整数（正整数 + 0）
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsUnMinusInt(string input)
        {
            return Regex.IsMatch(input, RegularExp.UnMinusInteger);
        }

        /// <summary>
        /// 验证正整数
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsPlusInt(string input)
        {
            return Regex.IsMatch(input, RegularExp.PlusInteger);
        }

        /// <summary>
        /// 验证非正整数（负整数 + 0） 
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsUnPlusInt(string input)
        {
            return Regex.IsMatch(input, RegularExp.UnPlusInteger);
        }

        /// <summary>
        /// 验证负整数
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsMinusInt(string input)
        {
            return Regex.IsMatch(input, RegularExp.MinusInteger);
        }

        /// <summary>
        /// 验证整数
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsInt(string input)
        {
            return Regex.IsMatch(input, RegularExp.Integer);
        }

        /// <summary>
        /// 验证非负浮点数（正浮点数 + 0）
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsUnMinusFloat(string input)
        {
            return Regex.IsMatch(input, RegularExp.UnMinusFloat);
        }

        /// <summary>
        /// 验证正浮点数
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsPlusFloat(string input)
        {
            return Regex.IsMatch(input, RegularExp.PlusFloat);
        }

        /// <summary>
        /// 验证非正浮点数（负浮点数 + 0）
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsUnPlusFloat(string input)
        {
            return Regex.IsMatch(input, RegularExp.UnPlusFloat);
        }

        /// <summary>
        /// 验证负浮点数
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsMinusFloat(string input)
        {
            return Regex.IsMatch(input, RegularExp.MinusFloat);
        }

        /// <summary>
        /// 验证浮点数
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsFloat(string input)
        {
            return Regex.IsMatch(input, RegularExp.Float);
        }

        /// <summary>
        /// 验证由26个英文字母组成的字符串
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsLetter(string input)
        {
            return Regex.IsMatch(input, RegularExp.Letter);
        }

        /// <summary>
        /// 验证由中文组成的字符串
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsChinese(string input)
        {
            return Regex.IsMatch(input, RegularExp.Chinese);
        }

        /// <summary>
        /// 验证由26个英文字母的大写组成的字符串
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsUpperLetter(string input)
        {
            return Regex.IsMatch(input, RegularExp.UpperLetter);
        }

        /// <summary>
        /// 验证由26个英文字母的小写组成的字符串
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsLowerLetter(string input)
        {
            return Regex.IsMatch(input, RegularExp.LowerLetter);
        }

        /// <summary>
        /// 验证由数字和26个英文字母组成的字符串
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsNumericOrLetter(string input)
        {
            return Regex.IsMatch(input, RegularExp.NumericOrLetter);
        }

        /// <summary>
        /// 验证由数字组成的字符串
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsNumeric(string input)
        {
            return Regex.IsMatch(input, RegularExp.Numeric);
        }
        /// <summary>
        /// 验证由数字和26个英文字母或中文组成的字符串
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsNumericOrLetterOrChinese(string input)
        {
            return Regex.IsMatch(input, RegularExp.NumbericOrLetterOrChinese);
        }

        /// <summary>
        /// 验证由数字、26个英文字母或者下划线组成的字符串
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsNumericOrLetterOrUnderline(string input)
        {
            return Regex.IsMatch(input, RegularExp.NumericOrLetterOrUnderline);
        }

        /// <summary>
        /// 验证email地址
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsEmail(string input)
        {
            return Regex.IsMatch(input, RegularExp.Email);
        }

        /// <summary>
        /// 验证URL
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsUrl(string input)
        {
            return Regex.IsMatch(input, RegularExp.Url);
        }

        /// <summary>
        /// 验证电话号码
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsTelephone(string input)
        {
            return Regex.IsMatch(input, RegularExp.Telephone);
        }

        /// <summary>
        /// 验证手机号码
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsMobile(string input)
        {
            return Regex.IsMatch(input, RegularExp.Mobile);
        }

        /// <summary>
        /// 通过文件扩展名验证图像格式
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsImageFormat(string input)
        {
            return Regex.IsMatch(input, RegularExp.ImageFormat);
        }

        /// <summary>
        /// 验证IP
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsIP(string input)
        {
            return Regex.IsMatch(input, RegularExp.IP);
        }

        /// <summary>
        /// 验证日期（YYYY-MM-DD）
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsDate(string input)
        {
            if (Regex.IsMatch(input, RegularExp.Date))
                return true;
            else
            {
                Result rt = ConvertHelper.DateTime(input);
                if (rt.Success)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 验证日期和时间（YYYY-MM-DD HH:MM:SS）
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsDateTime(string input)
        {
            if (Regex.IsMatch(input, RegularExp.DateTime))
                return true;
            else
            {
                Result rt = ConvertHelper.DateTime(input);
                if (rt.Success)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 验证颜色（#ff0000）
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsColor(string input)
        {
            return Regex.IsMatch(input, RegularExp.Color);
        }


        /// <summary>
        /// 验证颜色分数
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsScore(string input)
        {
            return Regex.IsMatch(input, RegularExp.Scores);
        }

        /// <summary>
        /// 身份证验证
        /// </summary>
        /// <param name="Id">身份证号</param>
        /// <returns></returns>
        public static bool CheckIDCard(string Id)
        {
            if (Id.Length == 18)
            {
                bool check = CheckIDCard18(Id);
                return check;
            }
            else if (Id.Length == 15)
            {
                bool check = CheckIDCard15(Id);
                return check;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 18位身份证验证
        /// </summary>
        /// <param name="Id">身份证号</param>
        /// <returns></returns>
        private static bool CheckIDCard18(string Id)
        {
            long n = 0;
            if (long.TryParse(Id.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证
            }
            string birth = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = Id.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            string AA=Id.Substring(17, 1).ToLower();
            string BB = arrVarifyCode[y];
            int Y = y;
            if (arrVarifyCode[y] != Id.Substring(17, 1).ToLower())
            {
                return false;//校验码验证
            }
            return true;//符合GB11643-1999标准
        }
        /// <summary>
        /// 15位身份证验证
        /// </summary>
        /// <param name="Id">身份证号</param>
        /// <returns></returns>
        private static bool CheckIDCard15(string Id)
        {
            long n = 0;
            if (long.TryParse(Id, out n) == false || n < Math.Pow(10, 14))
            {
                return false;//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证
            }
            string birth = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            return true;//符合15位身份证标准
        }
    }
    public struct RegularExp
    {
        public const string Chinese = @"^[\u4E00-\u9FA5\uF900-\uFA2D]+$";
        public const string Color = "^#[a-fA-F0-9]{6}";
        public const string Date = @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$";
        public const string DateTime = @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-)) (20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d$";
        public const string Email = @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$";
        public const string Float = @"^(-?\d+)(\.\d+)?$";
        public const string ImageFormat = @"\.(?i:jpg|bmp|gif|ico|pcx|jpeg|tif|png|raw|tga)$";
        public const string Integer = @"^-?\d+$";
        public const string IP = @"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$";
        public const string Letter = "^[A-Za-z]+$";
        public const string LowerLetter = "^[a-z]+$";
        public const string MinusFloat = @"^(-(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*)))$";
        public const string MinusInteger = "^-[0-9]*[1-9][0-9]*$";
        public const string Mobile = "^0{0,1}13[0-9]{9}$";
        public const string NumbericOrLetterOrChinese = @"^[A-Za-z0-9\u4E00-\u9FA5\uF900-\uFA2D]+$";
        public const string Numeric = "^[0-9]+$";
        public const string NumericOrLetter = "^[A-Za-z0-9]+$";
        public const string NumericOrLetterOrUnderline = @"^\w+$";
        public const string PlusFloat = @"^(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$";
        public const string PlusInteger = "^[0-9]*[1-9][0-9]*$";
        public const string Telephone = @"(\d+-)?(\d{4}-?\d{7}|\d{3}-?\d{8}|^\d{7,8})(-\d+)?";
        public const string UnMinusFloat = @"^\d+(\.\d+)?$";
        public const string UnMinusInteger = @"\d+$";
        public const string UnPlusFloat = @"^((-\d+(\.\d+)?)|(0+(\.0+)?))$";
        public const string UnPlusInteger = @"^((-\d+)|(0+))$";
        public const string UpperLetter = "^[A-Z]+$";
        public const string Url = @"^http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$";
        // 分数（含两位小数）
        public const string Scores = @"^[0-9]+(.[0-9]{2})?$";
    }
}
