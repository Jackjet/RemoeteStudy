using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using System.Reflection;

namespace Common
{
    /// <summary>
    /// This attribute is used to declare an explicit mapping
    /// between a column in a SharePoint list and a data member
    /// of a custom class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class CamlField : Attribute, ICamlField
    {
        string m_fieldName = string.Empty;

        /// <summary>
        /// Constructor that accepts the SharePoint field name to which
        /// the member is to be mapped.
        /// </summary>
        /// <param name="fieldName"></param>
        public CamlField(string fieldName)
        {
            m_fieldName = fieldName;
        }

        #region ICamlField Members

        /// <summary>
        /// Specifies the target site column name.
        /// </summary>
        public String FieldName
        {
            get
            {
                return m_fieldName;
            }
        }

        /// <summary>
        /// Updates the value of the associated field.
        /// </summary>
        /// <param name="target">the object containing the field to be set</param>
        /// <param name="listItem">the list item containing the field value</param>
        /// <param name="fieldInfo">the field metadata</param>
        /// <returns></returns>
        public bool SetValue(object target, SPListItem listItem, FieldInfo fieldInfo)
        {
            bool result = false;
            try
            {
                fieldInfo.SetValue(target, listItem[FieldName]);
                result = true;
            }
            catch
            {
            }
            return result;
        }

        #endregion
    }
}
