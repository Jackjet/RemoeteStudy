using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class ConvertHelper
    {
        /// <summary>
        /// 功能:转换objValue byte[]为Base64String        
        /// </summary>
        /// <param name="objValue">需要转换的对象</param>
        /// <returns>返回Result对象,如果错误Result.Base64StringResult返回空字符串</returns>
        public static Result Base64String(object objValue)
        {
            Result result = new Result();
            try
            {
                result.Base64StringResult = Convert.ToBase64String((byte[])objValue);
                result.Success = true;
            }
            catch (Exception exception)
            {
                result.Exception = exception;
                result.Success = false;
                result.Base64StringResult = "";
            }
            return result;
        }

        /// <summary>
        /// 功能:转换objValue为bool        
        /// </summary>
        /// <param name="objValue">需要转换的对象</param>
        /// <returns>返回Result对象,如果错误Result.BooleanResult返回false</returns>
        public static Result Boolean(object objValue)
        {
            Result result = new Result();
            try
            {
                result.BooleanResult = Convert.ToBoolean(objValue);
                result.Success = true;
            }
            catch (Exception exception)
            {
                result.Exception = exception;
                result.Success = false;
                result.BooleanResult = false;
            }
            return result;
        }

        /// <summary>
        /// 功能:转换objValue为Byte        
        /// </summary>
        /// <param name="objValue">需要转换的对象</param>
        /// <returns>返回Result对象,如果错误Result.ByteResult返回0</returns>
        public static Result Byte(object objValue)
        {
            Result result = new Result();
            try
            {
                result.ByteResult = Convert.ToByte(objValue);
                result.Success = true;
            }
            catch (Exception exception)
            {
                result.Exception = exception;
                result.Success = false;
                result.ByteResult = 0;
            }
            return result;
        }

        /// <summary>
        /// 功能:转换objValue为Char        
        /// </summary>
        /// <param name="objValue">需要转换的对象</param>
        /// <returns>返回Result对象,如果错误Result.CharResult返回'0'</returns>
        public static Result Char(object objValue)
        {
            Result result = new Result();
            try
            {
                result.CharResult = Convert.ToChar(objValue);
                result.Success = true;
            }
            catch (Exception exception)
            {
                result.Exception = exception;
                result.Success = false;
                result.CharResult = '0';
            }
            return result;
        }

        /// <summary>
        /// 功能:转换objValue为DateTime        
        /// </summary>
        /// <param name="objValue">需要转换的对象</param>
        /// <returns>返回Result对象,如果错误Result.DateTimeResult返回System.DateTime.Now</returns>
        public static Result DateTime(object objValue)
        {
            Result result = new Result();
            try
            {
                result.DateTimeResult = Convert.ToDateTime(objValue);
                result.Success = true;
            }
            catch (Exception exception)
            {
                result.Exception = exception;
                result.Success = false;
                result.DateTimeResult = System.DateTime.Now;
            }
            return result;
        }

        /// <summary>
        /// 功能:转换objValue为DateTime,如果失败，返回默认值        
        /// </summary>
        /// <param name="objValue">需要转换的对象</param>
        /// <param name="dtDefaultDate">如果转换失败，返回默认日期</param>
        /// <returns>返回Result对象,如果错误Result.DateTimeResult返回System.DateTime.Now</returns>
        public static Result DateTime(object objValue, System.DateTime dtDefaultDate)
        {
            Result result = DateTime(objValue);
            if (!result.Success)
            {
                result.DateTimeResult = dtDefaultDate;
            }
            return result;
        }

        /// <summary>
        /// 功能:转换objValue为Decimal        
        /// </summary>
        /// <param name="objValue">需要转换的对象</param>
        /// <returns>返回Result对象,如果错误Result.DecimalResult返回0</returns>
        public static Result Decimal(object objValue)
        {
            Result result = new Result();
            try
            {
                result.DecimalResult = Convert.ToDecimal(objValue);
                result.Success = true;
            }
            catch (Exception exception)
            {
                result.Exception = exception;
                result.Success = false;
                result.DecimalResult = 0M;
            }
            return result;
        }

        /// <summary>
        /// 功能:转换objValue为Double        
        /// </summary>
        /// <param name="objValue">需要转换的对象</param>
        /// <returns>返回Result对象,如果错误Result.DoubleResult返回0</returns>
        public static Result Double(object objValue)
        {
            Result result = new Result();
            try
            {
                result.DoubleResult = Convert.ToDouble(objValue);
                result.Success = true;
            }
            catch (Exception exception)
            {
                result.Exception = exception;
                result.Success = false;
                result.DoubleResult = 0.0;
            }
            return result;
        }

        /// <summary>
        /// 功能:转换objValue为Float        
        /// </summary>
        /// <param name="objValue">需要转换的对象</param>
        /// <returns>返回Result对象,如果错误Result.FloatResult返回0</returns>
        public static Result Float(object objValue)
        {
            Result result = new Result();
            try
            {
                result.FloatResult = Convert.ToSingle(objValue);
                result.Success = true;
            }
            catch (Exception exception)
            {
                result.Exception = exception;
                result.Success = false;
                result.FloatResult = 0f;
            }
            return result;
        }

        /// <summary>
        /// 功能:转换objValue为Int32        
        /// </summary>
        /// <param name="objValue">需要转换的对象</param>
        /// <returns>返回Result对象,如果错误Result.IntResult返回0</returns>
        public static Result Int(object objValue)
        {
            Result result = new Result();
            try
            {
                result.IntResult = Convert.ToInt32(objValue);
                result.Success = true;
            }
            catch (Exception exception)
            {
                result.Exception = exception;
                result.Success = false;
                result.IntResult = 0;
            }
            return result;
        }

        /// <summary>
        /// 功能:转换objValue为Int64        
        /// </summary>
        /// <param name="objValue">需要转换的对象</param>
        /// <returns>返回Result对象,如果错误Result.LongResult返回0</returns>
        public static Result Long(object objValue)
        {
            Result result = new Result();
            try
            {
                result.LongResult = Convert.ToInt64(objValue);
                result.Success = true;
            }
            catch (Exception exception)
            {
                result.Exception = exception;
                result.Success = false;
                result.LongResult = 0L;
            }
            return result;
        }

        /// <summary>
        /// 功能:转换objValue为SByte        
        /// </summary>
        /// <param name="objValue">需要转换的对象</param>
        /// <returns>返回Result对象,如果错误Result.SByteResult返回0</returns>
        public static Result SByte(object objValue)
        {
            Result result = new Result();
            try
            {
                result.SByteResult = Convert.ToSByte(objValue);
                result.Success = true;
            }
            catch (Exception exception)
            {
                result.Exception = exception;
                result.Success = false;
                result.SByteResult = 0;
            }
            return result;
        }

        /// <summary>
        /// 功能:转换objValue为Int16        
        /// </summary>
        /// <param name="objValue">需要转换的对象</param>
        /// <returns>返回Result对象,如果错误Result.ShortResult返回0</returns>
        public static Result Short(object objValue)
        {
            Result result = new Result();
            try
            {
                result.ShortResult = Convert.ToInt16(objValue);
                result.Success = true;
            }
            catch (Exception exception)
            {
                result.Exception = exception;
                result.Success = false;
                result.ShortResult = 0;
            }
            return result;
        }

        /// <summary>
        /// 功能:转换objValue为String        
        /// </summary>
        /// <param name="objValue">需要转换的对象</param>
        /// <returns>返回Result对象,如果错误Result.StringResult返回""</returns>
        public static Result String(object objValue)
        {
            Result result = new Result();
            try
            {
                result.StringResult = Convert.ToString(objValue);
                result.Success = true;
            }
            catch (Exception exception)
            {
                result.Exception = exception;
                result.Success = false;
                result.StringResult = "";
            }
            return result;
        }

        /// <summary>
        /// 功能:转换objValue为UInt32        
        /// </summary>
        /// <param name="objValue">需要转换的对象</param>
        /// <returns>返回Result对象,如果错误Result.UintResult返回0</returns>
        public static Result Uint(object objValue)
        {
            Result result = new Result();
            try
            {
                result.UintResult = Convert.ToUInt32(objValue);
                result.Success = true;
            }
            catch (Exception exception)
            {
                result.Exception = exception;
                result.Success = false;
                result.UintResult = 0;
            }
            return result;
        }

        /// <summary>
        /// 功能:转换objValue为UInt64        
        /// </summary>
        /// <param name="objValue">需要转换的对象</param>
        /// <returns>返回Result对象,如果错误Result.UlongResult返回0</returns>
        public static Result Ulong(object objValue)
        {
            Result result = new Result();
            try
            {
                result.UlongResult = Convert.ToUInt64(objValue);
                result.Success = true;
            }
            catch (Exception exception)
            {
                result.Exception = exception;
                result.Success = false;
                result.UlongResult = 0L;
            }
            return result;
        }

        /// <summary>
        /// 功能:转换objValue为UInt16        
        /// </summary>
        /// <param name="objValue">需要转换的对象</param>
        /// <returns>返回Result对象,如果错误Result.UshortResult返回0</returns>
        public static Result Ushort(object objValue)
        {
            Result result = new Result();
            try
            {
                result.UshortResult = Convert.ToUInt16(objValue);
                result.Success = true;
            }
            catch (Exception exception)
            {
                result.Exception = exception;
                result.Success = false;
                result.UshortResult = 0;
            }
            return result;
        }
    }
}
