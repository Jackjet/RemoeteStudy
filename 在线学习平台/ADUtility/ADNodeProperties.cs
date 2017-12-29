using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Centerland.ADUtility
{
    /// <summary>
    /// AD属性
    /// </summary>
    public class ADNodeProperties
    {
        public ADNodeProperties() { }
        public ADNodeProperties(EProperties __Propertie, string __Value) 
        {
            Propertie = __Propertie;
            if (!string.IsNullOrEmpty(__Value))
                Value = __Value;
            else
                Value = " ";
        }
        /// <summary>
        /// 属性名
        /// </summary>
        public EProperties Propertie
        { get; set; }
        /// <summary>
        /// 属性值
        /// </summary>
        public string Value
        { get; set; }
    }
    /// <summary>
    /// AD属性集
    /// </summary>
    public class ADNodePropertiesCollection: CollectionBase
	{
		public ADNodeProperties this[ int nIndex]
		{
			get
			{
				return( (ADNodeProperties) List[nIndex]);
			}
			set  
			{
				List[nIndex] = value;
			}
		}
        public ADNodeProperties this[EProperties Propertie]
        {
            get
            {
                for (int i = 0; i < List.Count; i++)
                {
                    if (((ADNodeProperties)List[i]).Propertie == Propertie)
                        return (ADNodeProperties)List[i];
                }
                return null;
            }
            set {
                for (int i = 0; i < List.Count; i++)
                {
                    if (((ADNodeProperties)List[i]).Propertie == Propertie)
                        List[i] = value;
                }
            }
        }

        public int Add(ADNodeProperties __user)
        {
            if (!List.Contains(__user))
                return List.Add(__user);
            else
                return 0;
        }

		public void Insert(int __nIndex, ADNodeProperties __user)  
		{
			List.Insert(__nIndex, __user);
		}

		public int IndexOf(ADNodeProperties __user)  
		{
			return List.IndexOf(__user);
		}	

		public void Remove(ADNodeProperties __user)  
		{
			List.Remove(__user);
		}

		public bool Contains(ADNodeProperties __user)  
		{
			return List.Contains(__user);
		}	
	}

}
