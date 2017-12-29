using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Common
{
    public static class LogCommon
    {
        public static Object txtLock = new object();
        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="error1">错误信息</param>
        /// <param name="site">出错位置</param>
        private static void writeLog(string path, string error, string position)
        {
            StringBuilder ErrMess = new StringBuilder();
            ErrMess.AppendLine("———————————————错误开始———————————————");
            ErrMess.AppendLine("出错时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            ErrMess.AppendLine("错误信息：" + error);
            ErrMess.AppendLine("出错位置：" + position);
            ErrMess.AppendLine("———————————————错误结束———————————————");
            lock (txtLock)
            {
                if (File.Exists(path))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(ErrMess);
                    StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.GetEncoding("GB2312"));
                    sw.WriteLine(sb.ToString());
                    sw.Close();
                }
                else
                {
                    FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.GetEncoding("GB2312"));//通过指定字符编码方式可以实现对汉字的支持，否则在用记事本打开查看会出现乱码
                    sw.WriteLine(ErrMess);
                    sw.Close();
                }
            }
        }
        private static void writeLog(string path, string error1)
        {
            StringBuilder ErrMess = new StringBuilder();
            ErrMess.AppendLine("———————————————开始———————————————");
            ErrMess.AppendLine("创建时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            ErrMess.AppendLine("具体信息：");
            ErrMess.AppendLine(error1);
            ErrMess.AppendLine("———————————————结束———————————————");
            lock (txtLock)
            {
                if (File.Exists(path))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(ErrMess);
                    StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.GetEncoding("GB2312"));
                    sw.WriteLine(sb.ToString());
                    sw.Close();
                }
                else
                {
                    FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.GetEncoding("GB2312")); 
                    sw.WriteLine(ErrMess);
                    sw.Close();
                }
            }
        }
        /// <summary>
        /// 输出错误日志-用户中心
        /// </summary>
        /// <param name="error1">错误信息</param>
        /// <param name="PageName">出错位置</param>
        public static void writeLogUserCenter(string error1, string position)
        {
            writeLog(@"c:\\UserCenter.txt", error1, position);
        }
        /// <summary>
        /// 输出错误日志-注册
        /// </summary>
        /// <param name="error1">错误信息</param>
        /// <param name="PageName">出错位置</param>
        public static void writeLogRegister(string error1, string position)
        {
            writeLog(@"c:\\Register.txt", error1, position);
        }
        /// <summary>
        /// 输出步骤日志-注册人员详情
        /// </summary>
        public static void WriteRegisterLogStep(string error1)
        {
            writeLog(@"c:\\RegisterLogStep.txt", error1);
        }
        /// <summary>
        /// 输出AD域注册错误信息
        /// </summary>
        /// <param name="error1"></param>
        public static void WriteADRegisterErrorLog(string error1)
        {
            writeLog(@"c:\\ADRegisterErrorLog.txt", error1);
        }
        /// <summary>
        /// 输出错误日志-用户中心接口
        /// </summary>
        /// <param name="error1">错误信息</param>
        /// <param name="PageName">出错位置</param>
        public static void writeLogWebService(string error1, string position)
        {
            writeLog(@"c:\\WebService.txt", error1, position);
        }
    }
}
