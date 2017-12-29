using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class CAML
    {
        #region Enums
        /// <summary>
        /// Use this enumeration to specify sorting of field elements.
        /// </summary>
        public enum SortType
        {
            /// <summary>
            /// Items are sorted in ascending order.
            /// </summary>
            Ascending,
            /// <summary>
            /// Items are sorted in descending order.
            /// </summary>
            Descending
        }
        /// <summary>
        /// Use this enumeration to specify membership types.
        /// </summary>
        public enum MembershipType
        {
            /// <summary>
            /// Returns all users who are either members of the site or who have browsed to the site as authenticated members of a domain group in the site.
            /// </summary>
            SPWebAllUsers,
            /// <summary>
            /// Returns groups in the site collection.
            /// </summary>
            SPGroup,
            /// <summary>
            /// Returns groups in the SharePoint web.
            /// </summary>
            SPWebGroups,
            /// <summary>
            /// Returns 
            /// </summary>
            CurrentUserGroups,
            /// <summary>
            /// Returns all users that have been explicitly added to the web.
            /// </summary>
            SPWebUsers
        }
        /// <summary>
        /// Use this enumeration to specify the base list type for cross site queries.
        /// </summary>
        public enum BaseType
        {
            /// <summary>
            /// A generic list.
            /// </summary>
            GenericList,
            /// <summary>
            /// A document library.
            /// </summary>
            DocumentLibrary,
            /// <summary>
            /// A discussion forum.
            /// </summary>
            DiscussionForum,
            /// <summary>
            /// A survey list.
            /// </summary>
            VoteOrSurvey,
            /// <summary>
            /// An issue tracking list.
            /// </summary>
            IssuesList
        }
        /// <summary>
        /// Use this enumeration to specify the scope of a site data query.
        /// </summary>
        public enum QueryScope
        {
            /// <summary>
            /// The query considers only the current SPWeb object.
            /// </summary>
            WebOnly,
            /// <summary>
            /// The query considers all Web sites that are descended from the current SPWeb object.
            /// </summary>
            Recursive,
            /// <summary>
            /// The query considers all Web sites that are in the same site collection as the current Web site.
            /// </summary>
            SiteCollection
        }
        /// <summary>
        /// Specifies how to handle automatic hyperlinks.
        /// </summary>
        public enum AutoHyperlinkType
        {
            /// <summary>
            /// Hyperlinks are ignored.
            /// </summary>
            None,
            /// <summary>
            /// Surround text with &lt;A&gt; tags if the text appears like a hyperlink (for example, www.johnholliday.net),
            /// but without HTML encoding.
            /// </summary>
            Plain,
            /// <summary>
            /// Surround text with &lt;A&gt; tags if the text appears like a hyperlink, with HTML encoding.
            /// </summary>
            HTMLEncoded
        }
        /// <summary>
        /// Specifies options for URL encoding.
        /// </summary>
        public enum UrlEncodingType
        {
            /// <summary>
            /// Special characters are not encoded.
            /// </summary>
            None,
            /// <summary>
            /// Convert special characters, such as spaces, to quoted UTF-8 format.
            /// </summary>
            Standard,
            /// <summary>
            /// Convert special characters to quoted UTF-8 format, but treats the characters as a
            /// path component of a URL so that forward slashes ("/") are not encoded.
            /// </summary>
            EncodeAsUrl
        }
        #endregion

        #region Methods
        #region Default
        /// <summary>
        /// Builds an XML string with or without attributes and attribute values.
        /// </summary>
        /// <param name="tag">the XML element tag</param>
        /// <param name="attribute">the attribute name (can be null)</param>
        /// <param name="attributeValue">the attribute value (can be null)</param>
        /// <param name="value">the element value (can be null)</param>
        /// <returns>an XML string resulting from the combined parameters</returns>
        public static string Tag(string tag, string attribute, string attributeValue, string value)
        {
            if (string.IsNullOrEmpty(attribute) || string.IsNullOrEmpty(attributeValue))
            {
                return string.IsNullOrEmpty(value) ?
                    string.Format("<{0} />", tag) :
                    string.Format("<{0}>{1}</{0}>", tag, value);
            }
            else
            {
                return string.IsNullOrEmpty(value) ?
                    string.Format("<{0} {1}=\"{2}\" />", tag, attribute, attributeValue) :
                    string.Format("<{0} {1}=\"{2}\">{3}</{0}>", tag, attribute, attributeValue, value);
            }
        }
        /// <summary>
        /// Handles an arbitrary number of attribute value pairs
        /// </summary>
        /// <param name="tag">the XML element tag</param>
        /// <param name="value">the element value</param>
        /// <param name="attributeValuePairs">an array of attribute value pairs</param>
        /// <returns>an XML string resulting from the combined parameters</returns>
        public static string Tag(string tag, string value, params object[] attributeValuePairs)
        {
            StringBuilder builder = new StringBuilder("<" + tag);
            for (int i = 0; i < attributeValuePairs.Length - 1; i += 2)
            {
                builder.AppendFormat(" {0}=\"{1}\"", attributeValuePairs[i].ToString(), attributeValuePairs[i + 1].ToString());
            }
            if (string.IsNullOrEmpty(value))
            {
                builder.Append(" />");
            }
            else
            {
                builder.AppendFormat(">{0}</{1}>", value, tag);
            }
            return builder.ToString();
        }
        #endregion

        #region A-G
        /// <summary>
        /// Specifies the logical conjunction of two CAML clauses.
        /// </summary>
        /// <param name="leftPart">the left part of the join</param>
        /// <param name="rightPart">the right part of the join</param>
        /// <returns>a new CAML And element</returns>
        public static string And(string leftPart, string rightPart) { return Tag(CamlQueryStrings.And, null, null, leftPart + rightPart); }
        /// <summary>
        /// Specifies that the value of a given field begins with the specified value.
        /// </summary>
        /// <param name="fieldRefElement">a CAML FieldRef element</param>
        /// <param name="valueElement">a CAML Value element</param>
        /// <returns>a new CAML BeginsWith element</returns>
        public static string BeginsWith(string fieldRefElement, string valueElement) { return Tag(CamlQueryStrings.BeginsWith, null, null, fieldRefElement + valueElement); }
        /// <summary>
        /// Specifies that the value of a given field contains the specified value.
        /// </summary>
        /// <param name="fieldRefElement">a CAML FieldRef element</param>
        /// <param name="valueElement">a CAML Value element</param>
        /// <returns>a new CAML Contains element</returns>
        public static string Contains(string fieldRefElement, string valueElement) { return Tag(CamlQueryStrings.Contains, null, null, fieldRefElement + valueElement); }
        /// <summary>
        /// Tests whether the dates in a recurring event overlap a specified DateTime value.
        /// </summary>
        /// <param name="fieldRefElement">a CAML FieldRef element for the target event date</param>
        /// <param name="valueElement">a CAML Value element containing the date to be tested</param>
        /// <returns>a new CAML DateRangesOverlap element</returns>
        public static string DateRangesOverlap(string fieldRefElement, string valueElement) { return Tag(CamlQueryStrings.DateRangesOverlap, null, null, fieldRefElement + CAML.FieldRef("EndDate") + CAML.FieldRef("RecurrenceID") + valueElement); }
        /// <summary>
        /// Tests the equality of two CAML clauses.
        /// </summary>
        /// <param name="leftPart">the left part of the expression</param>
        /// <param name="rightPart">the right part of expression</param>
        /// <returns>a new CAML EQ element</returns>
        public static string Eq(string leftPart, string rightPart) { return Tag(CamlQueryStrings.Eq, null, null, leftPart + rightPart); }
        /// <summary>
        /// Identifies a CAML field by reference.
        /// </summary>
        /// <param name="fieldName">the name of the referenced field</param>
        /// <returns>a new CAML FieldRef element</returns>
        public static string FieldRef(string fieldName) { return Tag(CamlQueryStrings.FieldRef, CamlQueryStrings.Name, SafeIdentifier(fieldName), null); }
        public static string FieldRefByLookupId(string fieldName) { return Tag(CamlQueryStrings.FieldRef, null, new object[] { CamlQueryStrings.Name, SafeIdentifier(fieldName), "LookupId", "TRUE" }); }
        public static string JoinFieldRef(string fieldName, string listAlias) { return Tag(CamlQueryStrings.FieldRef, null, new object[] { "RefType", "Id", CamlQueryStrings.Name, SafeIdentifier(fieldName), "List", listAlias }); }
        public static string JoinFieldRef(string fieldName) { return Tag(CamlQueryStrings.FieldRef, null, new object[] { "RefType", "Id", CamlQueryStrings.Name, SafeIdentifier(fieldName) }); }
        /// <summary>
        /// Tests whether the left expression is greater than or equal to the right.
        /// </summary>
        /// <param name="leftPart">the left expression</param>
        /// <param name="rightPart">the right expression</param>
        /// <returns>a new CAML GEQ element</returns>
        public static string Geq(string leftPart, string rightPart) { return Tag(CamlQueryStrings.Geq, null, null, leftPart + rightPart); }
        /// <summary>
        /// Identifies a field reference for grouping.
        /// </summary>
        /// <param name="fieldRefElement">a CAML FieldRef element</param>
        /// <returns>a new CAML GroupBy element</returns>
        public static string GroupBy(string fieldRefElement) { return GroupBy(fieldRefElement, false); }
        /// <summary>
        /// Identifies a field reference for grouping and specifies whether to collapse the group.
        /// </summary>
        /// <param name="fieldRefElement">a CAML FieldRef element</param>
        /// <param name="bCollapse">whether to collapse the group</param>
        /// <returns>a new CAML GroupBy element</returns>
        public static string GroupBy(string fieldRefElement, bool bCollapse) { return Tag(CamlQueryStrings.GroupBy, CamlQueryStrings.Collapse, bCollapse ? "TRUE" : "FALSE", fieldRefElement); }
        /// <summary>
        /// Tests whether the left expression is greater than the right.
        /// </summary>
        /// <param name="leftPart">the left expression</param>
        /// <param name="rightPart">the right expression</param>
        /// <returns>a new CAML GT element</returns>
        public static string Gt(string leftPart, string rightPart) { return Tag(CamlQueryStrings.Gt, null, null, leftPart + rightPart); }
        #endregion

        #region H-N
        /// <summary>
        /// Determines whether a given field contains a value.
        /// </summary>
        /// <param name="fieldRefElement">a CAML FieldRef element</param>
        /// <returns>a new CAML IsNotNull element</returns>
        public static string IsNotNull(string fieldRefElement) { return Tag(CamlQueryStrings.IsNotNull, null, null, fieldRefElement); }
        /// <summary>
        /// Determines whether a given field is null.
        /// </summary>
        /// <remarks>Converse of IsNotNull</remarks>
        /// <param name="fieldRefElement">a CAML FieldRef element</param>
        /// <returns>a new CAML IsNull element</returns>
        public static string IsNull(string fieldRefElement) { return Tag(CamlQueryStrings.IsNull, null, null, fieldRefElement); }
        /// <summary>
        /// Tests whether the left expression is less than or equal to the right.
        /// </summary>
        /// <param name="leftPart">the left expression</param>
        /// <param name="rightPart">the right expression</param>
        /// <returns>a new CAML LEQ element</returns>
        public static string Leq(string leftPart, string rightPart) { return Tag(CamlQueryStrings.Leq, null, null, leftPart + rightPart); }
        /// <summary>
        /// Allows a query to include specific lists, instead of returning all lists of a particular type.
        /// </summary>
        /// <param name="listId">identifies the lists</param>
        public static string List(Guid listId)
        {
            return Tag(CamlQueryStrings.List, "ID", listId.ToString().Replace("{", "").Replace("}", ""), null);
        }
        /// <summary>
        /// Specifies which lists to include in a query.
        /// </summary>
        /// <param name="listElements">an XML string containing individual list elements</param>
        public static string Lists(string listElements) { return Lists(BaseType.GenericList, listElements, null, false, 0); }
        /// <summary>
        /// Specifies which lists to include in a query.
        /// </summary>
        /// <param name="listElements">an XML string containing individual list elements</param>
        /// <param name="includeHiddenLists">determines whether the query will include hidden lists</param>
        public static string Lists(string listElements, bool includeHiddenLists) { return Lists(BaseType.GenericList, listElements, null, includeHiddenLists); }
        /// <summary>
        /// Specifies which lists to include in a query.
        /// </summary>
        /// <param name="listElements">an XML string containing individual list elements</param>
        /// <param name="maxListLimit">limits the query to the total number of lists specified.  By default, the limit is 1000.</param>
        public static string Lists(string listElements, int maxListLimit) { return Lists(BaseType.GenericList, listElements, null, false, maxListLimit); }
        /// <summary>
        /// Specifies which lists to include in a query.
        /// </summary>
        /// <param name="listElements">an XML string containing individual list elements</param>
        /// <param name="serverTemplate">limits the query to lists of the specified server template, specified as a number - for example '101'</param>
        /// <param name="includeHiddenLists">determines whether the query will include hidden lists</param>
        public static string Lists(string listElements, string serverTemplate, bool includeHiddenLists) { return Lists(BaseType.GenericList, listElements, serverTemplate, includeHiddenLists); }
        /// <summary>
        /// Specifies which lists to include in a query.
        /// </summary>
        /// <param name="baseType">limits the query to lists of the specified base type</param>
        /// <param name="listElements">an XML string containing individual list elements</param>
        public static string Lists(BaseType baseType, string listElements) { return Lists(baseType, listElements, null, false, 0); }
        /// <summary>
        /// Specifies which lists to include in a query.
        /// </summary>
        /// <param name="baseType">limits the query to lists of the specified base type</param>
        /// <param name="listElements">an XML string containing individual list elements</param>
        /// <param name="serverTemplate">limits the query to lists of the specified server template, specified as a number - for example '101'</param>
        public static string Lists(BaseType baseType, string listElements, string serverTemplate) { return Lists(baseType, listElements, serverTemplate, false, 0); }
        /// <summary>
        /// Specifies which lists to include in a query.
        /// </summary>
        /// <param name="baseType">limits the query to lists of the specified base type</param>
        /// <param name="listElements">an XML string containing individual list elements</param>
        /// <param name="serverTemplate">limits the query to lists of the specified server template, specified as a number - for example '101'</param>
        /// <param name="includeHiddenLists">determines whether the query will include hidden lists</param>
        public static string Lists(BaseType baseType, string listElements, string serverTemplate, bool includeHiddenLists) { return Lists(baseType, listElements, serverTemplate, includeHiddenLists, 0); }
        /// <summary>
        /// Specifies which lists to include in a query.
        /// </summary>
        /// <param name="baseType">limits the query to lists of the specified base type</param>
        /// <param name="listElements">an XML string containing individual list elements</param>
        /// <param name="serverTemplate">limits the query to lists of the specified server template, specified as a number - for example '101'</param>
        /// <param name="includeHiddenLists">determines whether the query will include hidden lists</param>
        /// <param name="maxListLimit">limits the query to the total number of lists specified.  By default, the limit is 1000.</param>
        public static string Lists(BaseType baseType, string listElements, string serverTemplate, bool includeHiddenLists, int maxListLimit)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<Lists BaseType=\"{0}\"", (int)baseType);
            if (!string.IsNullOrEmpty(serverTemplate))
            {
                sb.AppendFormat(" ServerTemplate=\"{0}\"", serverTemplate);
            }
            sb.AppendFormat(" Hidden=\"{0}\"", includeHiddenLists ? "TRUE" : "FALSE");
            sb.AppendFormat(" MaxListLimit=\"{0}\"", maxListLimit);
            sb.AppendFormat(">{0}</Lists>", listElements);
            return sb.ToString();
        }
        /// <summary>
        /// Tests whether the left expression is less than the right.
        /// </summary>
        /// <param name="leftPart">the left expression</param>
        /// <param name="rightPart">the right expression</param>
        /// <returns>a new CAML LT element</returns>
        public static string Lt(string leftPart, string rightPart) { return Tag(CamlQueryStrings.Lt, null, null, leftPart + rightPart); }
        /// <summary>
        /// Specifies the membership for a query <see cref="CAML.MembershipType"/>.
        /// </summary>
        /// <param name="type">specifies the membership type</param>
        /// <returns>a new CAML Membership element</returns>
        public static string Membership(CAML.MembershipType type) { return Membership(type, null); }
        /// <summary>
        /// Specifies the membership for a query <see cref="CAML.MembershipType"/>
        /// </summary>
        /// <param name="type">specifies the membership type</param>
        /// <param name="value">specifies the membership filter value</param>
        /// <returns>a new CAML Membership element</returns>
        public static string Membership(CAML.MembershipType type, string value)
        {
            switch (type)
            {
                case MembershipType.SPWebAllUsers:
                    return Tag(CamlQueryStrings.Membership, CamlQueryStrings.Type, CamlQueryStrings.SPWebAllUsers, value);
                case MembershipType.SPWebGroups:
                    return Tag(CamlQueryStrings.Membership, CamlQueryStrings.Type, CamlQueryStrings.SPWebGroups, value);
                case MembershipType.SPWebUsers:
                    return Tag(CamlQueryStrings.Membership, CamlQueryStrings.Type, CamlQueryStrings.SPWebUsers, value);
                case MembershipType.CurrentUserGroups:
                    return Tag(CamlQueryStrings.Membership, CamlQueryStrings.Type, CamlQueryStrings.CurrentUserGroups, value);
                case MembershipType.SPGroup:
                    return Tag(CamlQueryStrings.Membership, CamlQueryStrings.Type, CamlQueryStrings.SPGroup, value);
            }
            return Tag(CamlQueryStrings.Membership, CamlQueryStrings.Type, CamlQueryStrings.CurrentUserGroups, value);
        }
        /// <summary>
        /// Tests whether the left expression is unequal to the right.
        /// </summary>
        /// <param name="leftPart">the left expression</param>
        /// <param name="rightPart">the right expression</param>
        /// <returns>a new CAML NEQ element</returns>
        public static string Neq(string leftPart, string rightPart) { return Tag(CamlQueryStrings.Neq, null, null, leftPart + rightPart); }
        #endregion

        #region O-T
        /// <summary>
        /// Specifies the logical disjunction of two CAML expressions.
        /// </summary>
        /// <param name="leftPart">the left part of the logical join</param>
        /// <param name="rightPart">the right part of the logical join</param>
        /// <returns>a new CAML OR element</returns>
        public static string Or(string leftPart, string rightPart) { return Tag(CamlQueryStrings.Or, null, null, leftPart + rightPart); }
        /// <summary>
        /// Specifies the names of fields to be used for ordering the result set.
        /// </summary>
        /// <param name="fieldRefElements">a CAML string containing a list of CAML FieldRef elements</param>
        /// <returns>a new CAML OrderBy element</returns>
        public static string OrderBy(string fieldRefElements) { return Tag(CamlQueryStrings.OrderBy, null, null, fieldRefElements); }
        /// <summary>
        /// Builds an OrderBy element from an array of FieldRef elements.
        /// </summary>
        /// <param name="args">an array of CAML FieldRef elements</param>
        /// <returns>a new CAML OrderBy element</returns>
        public static string OrderBy(params object[] args)
        {
            string fieldRefElements = string.Empty;
            foreach (object o in args) { fieldRefElements += o.ToString(); }
            return Tag(CamlQueryStrings.OrderBy, null, null, fieldRefElements);
        }
        /// <summary>
        /// Specifies a global site property.
        /// </summary>
        /// <param name="propertyName">the name of the property to be retrieved</param>
        /// <returns>a new CAML ProjectProperty element</returns>
        public static string ProjectProperty(string propertyName) { return Tag(CamlQueryStrings.ProjectProperty, CamlQueryStrings.Select, propertyName, null); }
        /// <summary>
        /// Specifies a global site property and a default value.
        /// </summary>
        /// <param name="propertyName">the name of the property to be retrieved</param>
        /// <param name="defaultValue">the default value to use if the property is not found</param>
        /// <returns>a new CAML ProjectProperty element</returns>
        public static string ProjectProperty(string propertyName, string defaultValue)
        {
            return Tag(CamlQueryStrings.ProjectProperty, null,
                new object[] { 
                    CamlQueryStrings.Select, propertyName, 
                    CamlQueryStrings.Default, defaultValue 
                });
        }
        /// <summary>
        /// Specifies a global site property and other options.
        /// </summary>
        /// <param name="propertyName">the name of the property to be retrieved</param>
        /// <param name="defaultValue">the default value to use if the property is not found</param>
        /// <param name="autoHyperlinkType">specifies how to handle hyperlinks <see cref="CAML.AutoHyperlinkType"/></param>
        /// <param name="autoNewLine">TRUE to insert &lt;BR&gt; tags into the text stream and to replace multiple spaces with a nonbreaking space.</param>
        /// <param name="expandXML">TRUE to re-pass the rendered content through the CAML interpreter, which allows CAML to render CAML.</param>
        /// <param name="htmlEncode">TRUE to convert embedded characters so that they are displayed as text in the browser.  In other words, characters that could be confused with HTML tags are converted to entities.</param>
        /// <param name="stripWhiteSpace">TRUE to remove white space from the beginning and end of the value returned by the element.</param>
        /// <param name="urlEncodingType">specifies how to handle URL encoding <see cref="CAML.UrlEncodingType"/></param>
        /// <returns></returns>
        public static string ProjectProperty(string propertyName, string defaultValue,
            AutoHyperlinkType autoHyperlinkType, bool autoNewLine, bool expandXML, bool htmlEncode,
            bool stripWhiteSpace, UrlEncodingType urlEncodingType)
        {
            return Tag(CamlQueryStrings.ProjectProperty, null,
                new object[] { 
                    CamlQueryStrings.Select, propertyName, 
                    CamlQueryStrings.Default, defaultValue,
                    autoHyperlinkType==AutoHyperlinkType.Plain ? CamlQueryStrings.AutoHyperLinkNoEncoding : CamlQueryStrings.AutoHyperLink, 
                        autoHyperlinkType==AutoHyperlinkType.None ? "FALSE" : "TRUE",
                    CamlQueryStrings.AutoNewLine, autoNewLine ? "TRUE" : "FALSE",
                    CamlQueryStrings.HTMLEncode, htmlEncode ? "TRUE" : "FALSE",
                    CamlQueryStrings.StripWS, stripWhiteSpace ? "TRUE" : "FALSE",
                    urlEncodingType==UrlEncodingType.EncodeAsUrl ? CamlQueryStrings.URLEncodeAsURL : CamlQueryStrings.URLEncode,
                        urlEncodingType==UrlEncodingType.None ? "FALSE" : "TRUE"
                });
        }
        /// <summary>
        /// Creates a "safe" identifier for use in CAML expressions.
        /// </summary>
        /// <remarks>
        /// This method replaces blank spaces with the "_x0020_" token.
        /// </remarks>
        /// <param name="identifier">the identifier to be tokenized</param>
        /// <returns>a tokenized version of the identifier</returns>
        public static string SafeIdentifier(string identifier) { return identifier.Replace(" ", "_x0020_"); }
        #endregion

        #region U-Z
        /// <summary>
        /// Specifies a string value
        /// </summary>
        /// <param name="fieldValue">the string value to be expressed in CAML</param>
        /// <returns>a new CAML Value element</returns>
        public static string Value(string fieldValue) { return Tag(CamlQueryStrings.Value, CamlQueryStrings.Type, CamlQueryStrings.Text, fieldValue); }
        /// <summary>
        /// Specifies an integer value
        /// </summary>
        /// <param name="fieldValue">the integer value to be expressed in CAML</param>
        /// <returns>a new CAML Value element</returns>
        public static string Value(int fieldValue) { return Tag(CamlQueryStrings.Value, CamlQueryStrings.Type, CamlQueryStrings.Integer, fieldValue.ToString()); }
        /// <summary>
        /// Specifies a DateTime value
        /// </summary>
        /// <param name="fieldValue">the DateTime value to be expressed in CAML</param>
        /// <returns>a new CAML Value element</returns>
        public static string Value(DateTime fieldValue) { return Value(fieldValue, true); }
        public static string Value(DateTime fieldValue, bool includeTimeValue) { return Tag(CamlQueryStrings.Value, SPUtility.CreateISO8601DateTimeFromSystemDateTime(fieldValue), CamlQueryStrings.Type, CamlQueryStrings.DateTime, "IncludeTimeValue", includeTimeValue.ToString().ToUpper()); }
        /// <summary>
        /// Specifies a boolean value
        /// </summary>
        /// <param name="fieldValue">the boolean value to be expressed in CAML</param>
        /// <returns>a new CAML Value element</returns>
        public static string Value(bool fieldValue) { return Tag(CamlQueryStrings.Value, CamlQueryStrings.Type, CamlQueryStrings.Boolean, fieldValue ? "1" : "0"); }

        /// <summary>
        /// Specifies a value of a given type
        /// </summary>
        /// <param name="valueType">a string describing the data type</param>
        /// <param name="fieldValue">the value formatted as a string</param>
        /// <returns>a new CAML Value element</returns>
        public static string Value(string valueType, string fieldValue) { return Tag(CamlQueryStrings.Value, CamlQueryStrings.Type, valueType, fieldValue); }
        /// <summary>
        /// Specifies which fields to include in the query result set.
        /// </summary>
        /// <param name="fields">an array of CAML FieldRef elements that identify the fields to be included</param>
        /// <returns>a new CAML ViewFields element</returns>
        public static string ViewFields(params object[] fields)
        {
            string fieldRefElements = string.Empty;
            foreach (object field in fields) { fieldRefElements += field.ToString(); }
            return Tag(CamlQueryStrings.ViewFields, null, null, fieldRefElements);
        }
        public static string OrderByField(string id) { return FieldRef(id); }
        /// <summary>
        /// Identifies a CAML field and specifies a sorting.
        /// </summary>
        /// <param name="fieldName">the name of the referenced field</param>
        /// <param name="sortType">indicates how the resulting field instances shall be sorted</param>
        /// <returns>a new CAML FieldRef element with sorting</returns>
        public static string OrderByField(string id, CAML.SortType sortType) { return Tag(CamlQueryStrings.FieldRef, null, new object[] { "Ascending", sortType == SortType.Ascending ? "TRUE" : "FALSE", CamlQueryStrings.Name, SafeIdentifier(id) }); }
        public static string ViewField(string id) { return FieldRef(id); }
        public static string ViewField(string id, bool nullable) { return Tag("FieldRef", null, new object[] { "ID", SafeIdentifier(id), "Nullable", nullable.ToString().ToUpper() }); }
        /// <summary>
        /// Specifies which Web sites to include in the query as specified by the Scope attribute.
        /// </summary>
        /// <param name="scope">specifies the query scope</param>
        public static string Webs(QueryScope scope)
        {
            return Tag(CamlQueryStrings.Webs, null, CamlQueryStrings.Scope, scope.ToString());
        }
        /// <summary>
        /// Specifies the WHERE part of a query.
        /// </summary>
        /// <param name="s">a CAML string that expresses the WHERE conditions</param>
        /// <returns>a new CAML Where element</returns>
        public static string Where(string s) { return Tag(CamlQueryStrings.Where, null, null, s); }
        public static string Query(string where) { return Tag(CamlQueryStrings.Query, null, null, where); }
        public static string Query(string where, string orderBy) { return Tag(CamlQueryStrings.Query, null, null, where + orderBy); }
        public static string ProjectedFields(params object[] projectedFields)
        {
            string fieldRefElements = string.Empty;
            foreach (object field in projectedFields) { fieldRefElements += field.ToString(); }
            return Tag("ProjectedFields", null, null, fieldRefElements);
        }
        public static string ProjectedField(string name, string list, string showField)
        {
            return Tag("Field", null, new object[] { "Name", name, "Type", "Lookup", "List", list, "ShowField", showField });
        }
        public static string ProjectedField(string list, string showField)
        {
            return Tag("Field", null, new object[] { "Name", showField, "Type", "Lookup", "List", list, "ShowField", showField });
        }
        public static string Joins(params object[] joins)
        {
            string joinElements = string.Empty;
            foreach (object join in joins) { joinElements += join.ToString(); }
            return Tag("Joins", null, null, joinElements);
        }
        public static string LeftJoin(string lookupFieldName, string listAlias)
        {
            return Join("LEFT", lookupFieldName, null, listAlias);
        }
        public static string InnerJoin(string lookupFieldName, string listAlias)
        {
            return Join("INNER", lookupFieldName, null, listAlias);
        }
        public static string LeftJoin(string lookupFieldName, string firstListAlias, string secondListAlias)
        {
            return Join("LEFT", lookupFieldName, firstListAlias, secondListAlias);
        }
        public static string InnerJoin(string lookupFieldName, string firstListAlias, string secondListAlias)
        {
            return Join("INNER", lookupFieldName, firstListAlias, secondListAlias);
        }
        internal static string Join(string joinType, string lookupFieldName, string firstListAlias, string secondListAlias)
        {
            return Tag("Join",
                    Eq(
                    string.IsNullOrEmpty(firstListAlias) ? JoinFieldRef(lookupFieldName) : JoinFieldRef(lookupFieldName, firstListAlias),
                    JoinFieldRef(lookupFieldName, secondListAlias))
                    , "Type", joinType, "ListAlias", secondListAlias);

        }
        /// <summary>
        /// Special optional child of the Lists element.
        /// </summary>
        /// <remarks>When present, this element causes the query to be limited to lists
        /// with indexed fields.</remarks>
        /// <param name="fieldID">the guid of the indexed field</param>
        /// <param name="fieldValue">the matching field value</param>
        public static string WithIndex(Guid fieldID, string fieldValue)
        {
            return Tag(CamlQueryStrings.WithIndex, null, new object[]{ "FieldId", fieldID.ToString().Replace("{","").Replace("}",""),
                "Type", "Text", "Value", fieldValue});
        }
        /// <summary>
        /// Specifies a custom XML element.
        /// </summary>
        /// <param name="s">a CAML string to be embedded in the element</param>
        /// <returns>a new CAML XML element</returns>
        public static string XML(string s) { return Tag(CamlQueryStrings.XML, null, null, s); }
        #endregion
        #endregion
    }
}
