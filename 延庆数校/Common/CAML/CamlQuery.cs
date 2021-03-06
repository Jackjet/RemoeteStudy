﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web.UI.WebControls;
using System.Reflection;

namespace Common
{
    /// <summary>
    /// A wrapper class for binding query data.
    /// </summary>
    /// <remarks>This class is implemented as an attribute so that CAML queries
    /// can be attached to other objects such as list and site definitions.</remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class CamlQuery : Attribute, ICamlQuery
    {
        #region Properties
        /// <summary>
        /// The CAML string that defines the query, for example:
        /// <![CDATA[<Where><Contains><FieldRef Name="Author"/><Value Type="Text">Holliday</Value></Contains></Where>]]>
        /// </summary>
        private string m_queryXml = string.Empty;
        /// <summary>
        /// The CAML string that defines the query scope.
        /// </summary>
        private string m_listsXml = string.Empty;
        /// <summary>
        /// The CAML string that defines the fields that should be included in the query results.
        /// </summary>
        private string m_viewFieldsXml = string.Empty;

        /// <summary>
        /// Gets or sets the query string for this instance.
        /// </summary>
        /// <remarks>
        /// The QueryXml string includes the Where clause and any OrderBy
        /// or GroupBy qualifiers.  When deriving a class from CamlQuery,
        /// you can override this property to supply a custom query string.
        /// </remarks>
        public virtual string QueryXml
        {
            get { return m_queryXml; }
            set { m_queryXml = value; }
        }
        /// <summary>
        /// Gets or sets the Lists clause for this instance.
        /// </summary>
        public virtual string ListsXml
        {
            get { return m_listsXml; }
            set { m_listsXml = value; }
        }
        /// <summary>
        /// Gets or sets the ViewFields clause for this instance.
        /// </summary>
        public virtual string ViewFieldsXml
        {
            get { return m_viewFieldsXml; }
            set { m_viewFieldsXml = value; }
        }

        public virtual int RowLimit { get; set; }
        public virtual string JoinsXml { get; set; }
        public virtual string ProjectedFieldsXml { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public CamlQuery() { }
        /// <summary>
        /// Constructor that accepts a single string argument that
        /// specifies the underlying CAML query to be used.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="query">the CAML query string</param>
        public CamlQuery(string query)
        {
            this.QueryXml = query;
        }
        /// <summary>
        /// Extended constructor that accepts the CAML query string 
        /// along with a variable length list of field references.
        /// </summary>
        /// <param name="query">the CAML query string</param>
        /// <param name="viewFields">a list of FieldRef clauses that specify which fields to include in the result set</param>
        public CamlQuery(string query, params string[] viewFields)
        {
            m_queryXml = query;
            StringBuilder sb = new StringBuilder();
            foreach (string field in viewFields)
                sb.Append(field);
            this.ViewFieldsXml = sb.ToString();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates and initializes an SPQuery object.
        /// </summary>
        public SPQuery CreateQuery()
        {
            SPQuery query = new SPQuery();
            if (this.RowLimit != 0)
                query.RowLimit = (uint)this.RowLimit;
            query.Query = this.QueryXml;
            query.ViewFields = this.ViewFieldsXml;
            query.Joins = this.JoinsXml;
            query.ProjectedFields = this.ProjectedFieldsXml;
            return query;
        }

        /// <summary>
        /// Creates and initializes an SPSiteDataQuery object for a given scope.
        /// </summary>
        public SPSiteDataQuery CreateSiteDataQuery(CAML.QueryScope scope)
        {
            SPSiteDataQuery query = new SPSiteDataQuery();
            query.Query = this.QueryXml;
            query.ViewFields = this.ViewFieldsXml;
            query.Lists = this.ListsXml;
            // Only use the Webs clause if the scope is different than the default.
            if (scope != CAML.QueryScope.WebOnly)
            {
                query.Webs = CAML.Webs(scope);
            }
            return query;
        }

        #region Fetch
        /// <summary>
        /// Executes the query against a site collection.
        /// </summary>
        public IList<SPListItem> Fetch(SPSite site)
        {
            return Fetch(site.OpenWeb(), CAML.QueryScope.SiteCollection);
        }
        /// <summary>
        /// Executes the query against a website.
        /// </summary>
        public IList<SPListItem> Fetch(SPWeb web)
        {
            return Fetch(web, CAML.QueryScope.WebOnly);
        }
        /// <summary>
        /// Retrieves a collection of list items from a SharePoint web using the attached query.
        /// </summary>
        /// <remarks>The query considers</remarks>
        /// <param name="web">the web from which to fetch the items</param>
        /// <param name="scope">specifies the desired query scope</param>
        /// <returns>a list containing the resulting items</returns>
        public IList<SPListItem> Fetch(SPWeb web, CAML.QueryScope scope)
        {
            List<SPListItem> items = new List<SPListItem>();
            try
            {
                // Create the query.
                SPSiteDataQuery query = CreateSiteDataQuery(scope);

                // Execute the query.
                DataTable table = web.GetSiteData(query);

                // Convert the resulting table into a generic list
                // of SPListItem instances.
                foreach (DataRow row in table.Rows)
                {
                    Guid listGuid = new Guid(row["ListId"].ToString());
                    int itemIndex = Int32.Parse(row["ID"].ToString()) - 1;

                    SPList itemList = null;

                    try
                    {
                        itemList = web.Lists[listGuid];
                        items.Add(itemList.Items[itemIndex]);
                    }
                    catch (Exception x)
                    {
                        string s = x.Message;
                    }
                }
            }
            catch (Exception x)
            {
                string s = x.Message;
            }
            return items;
        }
        /// <summary>
        /// Executes the query against a list.
        /// </summary>
        public IList<SPListItem> Fetch(SPList list)
        {
            List<SPListItem> items = new List<SPListItem>();
            try
            {
                // Fetch the items.
                foreach (SPListItem item in list.GetItems(CreateQuery()))
                    items.Add(item);
            }
            catch
            {
            }

            return items;
        }
        /// <summary>
        /// A static method for quickly retrieving items from a list based on a query string.
        /// </summary>
        /// <param name="list">the list containing the items</param>
        /// <param name="queryXml">the query xml</param>
        /// <returns>the collection of matching items</returns>
        public static IList<SPListItem> Fetch(SPList list, string queryXml)
        {
            CamlQuery query = new CamlQuery(queryXml);
            return query.Fetch(list);
        }
        /// <summary>
        /// Executes the query against a given site collection and then binds to a specified object class.
        /// </summary>
        public IList<T> Fetch<T>(SPSite site) where T : new()
        {
            IList<T> result = new List<T>();
            BindingFlags flags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            foreach (SPListItem item in Fetch(site))
            {
                T t = new T();
                foreach (FieldInfo info in t.GetType().GetFields(flags))
                {
                    foreach (ICamlField field in (ICamlField[])info.GetCustomAttributes(typeof(CamlField), false))
                    {
                        field.SetValue(t, item, info);
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// Executes the query against a given SPWeb and binds to a specified object class.
        /// </summary>
        public IList<T> Fetch<T>(SPWeb web) where T : new()
        {
            return Fetch<T>(web, CAML.QueryScope.WebOnly);
        }
        /// <summary>
        /// Executes the query against a website for a given scope and then binds to a specified object class.
        /// </summary>
        public IList<T> Fetch<T>(SPWeb web, CAML.QueryScope scope) where T : new()
        {
            IList<T> result = new List<T>();
            BindingFlags flags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            foreach (SPListItem item in Fetch(web, scope))
            {
                T t = new T();
                foreach (FieldInfo info in t.GetType().GetFields(flags))
                {
                    foreach (ICamlField field in (ICamlField[])info.GetCustomAttributes(typeof(CamlField), false))
                    {
                        field.SetValue(t, item, info);
                    }
                }
                result.Add(t);
            }
            return result;
        }
        /// <summary>
        /// Executes the query against a given list and then binds to a specified object class.
        /// </summary>
        public IList<T> Fetch<T>(SPList list) where T : new()
        {
            IList<T> result = new List<T>();
            BindingFlags flags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            foreach (SPListItem item in Fetch(list))
            {
                T t = new T();
                foreach (FieldInfo info in t.GetType().GetFields(flags))
                {
                    foreach (ICamlField field in (ICamlField[])info.GetCustomAttributes(typeof(CamlField), false))
                    {
                        field.SetValue(t, item, info);
                    }
                }
                result.Add(t);
            }
            return result;
        }
        #endregion

        #region DataBind
        /// <summary>
        /// Executes the query and binds the results to a gridview.
        /// </summary>
        /// <remarks>
        /// Optionally initializes the gridview columns using the fields associated
        /// with this query.
        /// </remarks>
        /// <param name="gridView">the gridView to bind to</param>
        /// <param name="web">the web containing the data</param>
        /// <param name="scope">the desired query scope</param>
        /// <param name="initColumns">whether to initialize the gridview columns</param>
        public void DataBind(SPGridView gridView, SPWeb web, CAML.QueryScope scope, bool initColumns)
        {
            BindToGridView(web, gridView, CreateSiteDataQuery(scope), initColumns);
        }
        /// <summary>
        /// Executes the query and binds the results to a gridview.
        /// </summary>
        public void DataBind(SPGridView gridView, SPWeb web, CAML.QueryScope scope)
        {
            DataBind(gridView, web, scope, true);
        }
        /// <summary>
        /// Executes the query and binds the results to a gridview.
        /// </summary>
        public void DataBind(SPGridView gridView, SPWeb web)
        {
            DataBind(gridView, web, CAML.QueryScope.SiteCollection, true);
        }
        /// <summary>
        /// Executes the query and binds the results to a gridview.
        /// </summary>
        public void DataBind(SPGridView gridView)
        {
            DataBind(gridView, SPContext.Current.Web);
        }
        /// <summary>
        /// Executes the query and binds the results to a gridview.
        /// </summary>
        public void DataBind(SPGridView gridView, SPSite site, bool initColumns)
        {
            DataBind(gridView, site.OpenWeb(), CAML.QueryScope.SiteCollection, initColumns);
        }
        /// <summary>
        /// Executes the query and binds the results to a gridview.
        /// </summary>
        public void DataBind(SPGridView gridView, SPSite site)
        {
            DataBind(gridView, site, true);
        }
        /// <summary>
        /// Executes the query and binds the results to a gridview.
        /// </summary>
        public void DataBind(SPGridView gridView, SPList list, bool initColumns)
        {
            SPSiteDataQuery query = CreateSiteDataQuery(CAML.QueryScope.WebOnly);
            query.Lists = CAML.Lists(CAML.List(list.ID));
            BindToGridView(list.ParentWeb, gridView, query, initColumns);
        }
        /// <summary>
        /// Executes the query and binds the results to a gridview.
        /// </summary>
        public void DataBind(SPGridView gridView, SPList list)
        {
            DataBind(gridView, list, true);
        }
        /// <summary>
        /// Determines whether a column should be bound to a data grid.
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        private bool IsBindableColumn(DataColumn col)
        {
            if (col.ColumnName.Equals("ListId") ||
                col.ColumnName.Equals("WebId"))
                return false;
            return true;
        }
        /// <summary>
        /// Binds this query to an SPGridView control.
        /// </summary>
        /// <param name="web"></param>
        /// <param name="gridView"></param>
        /// <param name="query"></param>
        /// <param name="initColumns"></param>
        private void BindToGridView(SPWeb web, SPGridView gridView, SPSiteDataQuery query, bool initColumns)
        {
            // Execute the query.
            DataTable table = web.GetSiteData(query);

            // Initialize the gridview columns, if requested
            if (initColumns)
            {
                gridView.AutoGenerateColumns = false;
                gridView.Columns.Clear();

                foreach (DataColumn column in table.Columns)
                {
                    if (IsBindableColumn(column))
                    {
                        BoundField col = new BoundField();
                        col.DataField = column.ColumnName;
                        col.HeaderText = string.IsNullOrEmpty(column.Caption) ? column.ColumnName : column.Caption;
                        gridView.Columns.Add(col);
                    }
                }
            }

            // Bind the data to the view.
            gridView.DataSource = table;
            gridView.DataBind();
        }
        #endregion
        #endregion
    }
}
