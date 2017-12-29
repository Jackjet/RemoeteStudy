using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class Result
    {
        private string _Base64StringResult;
        private bool _BooleanResult;
        private byte _ByteResult;
        private char _CharResult;
        private DateTime _DateTimeResult;
        private decimal _DecimalResult;
        private double _DoubleResult;
        private System.Exception _Exception;
        private float _FloatResult;
        private int _IntResult;
        private long _LongResult;
        private sbyte _SByteResult;
        private short _ShortResult;
        private string _StringResult;
        private bool _Success;
        private uint _UintResult;
        private ulong _UlongResult;
        private ushort _UshortResult;

        /// <summary>
        /// 功能:获取和设置转换后的结果        
        /// </summary>
        public string Base64StringResult
        {
            get
            {
                return this._Base64StringResult;
            }
            set
            {
                this._Base64StringResult = value;
            }
        }

        /// <summary>
        /// 功能:获取和设置转换后的结果        
        /// </summary>
        public bool BooleanResult
        {
            get
            {
                return this._BooleanResult;
            }
            set
            {
                this._BooleanResult = value;
            }
        }

        /// <summary>
        /// 功能:获取和设置转换后的结果
        /// </summary>
        public byte ByteResult
        {
            get
            {
                return this._ByteResult;
            }
            set
            {
                this._ByteResult = value;
            }
        }

        /// <summary>
        /// 功能:获取和设置转换后的结果        
        /// </summary>
        public char CharResult
        {
            get
            {
                return this._CharResult;
            }
            set
            {
                this._CharResult = value;
            }
        }

        /// <summary>
        /// 功能:获取和设置转换后的结果        
        /// </summary>
        public DateTime DateTimeResult
        {
            get
            {
                return this._DateTimeResult;
            }
            set
            {
                this._DateTimeResult = value;
            }
        }

        /// <summary>
        /// 功能:获取和设置转换后的结果        
        /// </summary>
        public decimal DecimalResult
        {
            get
            {
                return this._DecimalResult;
            }
            set
            {
                this._DecimalResult = value;
            }
        }

        /// <summary>
        /// 功能:获取和设置转换后的结果        
        /// </summary>
        public double DoubleResult
        {
            get
            {
                return this._DoubleResult;
            }
            set
            {
                this._DoubleResult = value;
            }
        }

        /// <summary>
        /// 功能:转换出现错误，产生的错误信息        
        /// </summary>
        public System.Exception Exception
        {
            get
            {
                return this._Exception;
            }
            set
            {
                this._Exception = value;
            }
        }

        /// <summary>
        /// 功能:获取和设置转换后的结果        
        /// </summary>
        public float FloatResult
        {
            get
            {
                return this._FloatResult;
            }
            set
            {
                this._FloatResult = value;
            }
        }

        /// <summary>
        /// 功能:获取和设置转换后的结果        
        /// </summary>
        public int IntResult
        {
            get
            {
                return this._IntResult;
            }
            set
            {
                this._IntResult = value;
            }
        }

        /// <summary>
        /// 功能:获取和设置转换后的结果        
        /// </summary>
        public long LongResult
        {
            get
            {
                return this._LongResult;
            }
            set
            {
                this._LongResult = value;
            }
        }

        /// <summary>
        /// 功能:获取和设置转换后的结果
        /// </summary>
        public sbyte SByteResult
        {
            get
            {
                return this._SByteResult;
            }
            set
            {
                this._SByteResult = value;
            }
        }

        /// <summary>
        /// 功能:获取和设置转换后的结果        
        /// </summary>
        public short ShortResult
        {
            get
            {
                return this._ShortResult;
            }
            set
            {
                this._ShortResult = value;
            }
        }

        /// <summary>
        /// 功能:获取和设置转换后的结果        
        /// </summary>
        public string StringResult
        {
            get
            {
                return this._StringResult;
            }
            set
            {
                this._StringResult = value;
            }
        }

        /// <summary>
        /// 功能:转换是否成功        
        /// </summary>
        public bool Success
        {
            get
            {
                return this._Success;
            }
            set
            {
                this._Success = value;
            }
        }

        /// <summary>
        /// 功能:获取和设置转换后的结果        
        /// </summary>
        public uint UintResult
        {
            get
            {
                return this._UintResult;
            }
            set
            {
                this._UintResult = value;
            }
        }

        /// <summary>
        /// 功能:获取和设置转换后的结果        
        /// </summary>
        public ulong UlongResult
        {
            get
            {
                return this._UlongResult;
            }
            set
            {
                this._UlongResult = value;
            }
        }

        /// <summary>
        /// 功能:获取和设置转换后的结果        
        /// </summary>
        public ushort UshortResult
        {
            get
            {
                return this._UshortResult;
            }
            set
            {
                this._UshortResult = value;
            }
        }
    }
}
