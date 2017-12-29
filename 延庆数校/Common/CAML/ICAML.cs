using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint;
using System.Reflection;

namespace Common
{
    /// <summary>
    /// Used to indicate fields which are mapped to SharePoint site columns.
    /// </summary>
    public interface ICamlField
    {
        /// <summary>
        /// Specifies the target site column name.
        /// </summary>
        String FieldName { get; }
        /// <summary>
        /// Updates the value of the associated field.
        /// </summary>
        /// <param name="target">the object containing the field to be set</param>
        /// <param name="listItem">the list item containing the field value</param>
        /// <param name="fieldInfo">the field metadata</param>
        /// <returns></returns>
        bool SetValue(object target, SPListItem listItem, FieldInfo fieldInfo);
    }

    /// <summary>
    /// Describes an abstract CAML query.
    /// </summary>
    public interface ICamlQuery
    {
        /// <summary>
        /// Gets or sets the CAML query string.
        /// </summary>
        string QueryXml { get; set; }
        /// <summary>
        /// Gets or sets the CAML Lists specifier.
        /// </summary>
        string ListsXml { get; set; }
        /// <summary>
        /// Gets or sets the CAML ViewFields specifier.
        /// </summary>
        string ViewFieldsXml { get; set; }
        /// <summary>
        /// Creates an SPQuery object.
        /// </summary>
        /// <returns>An initialized SPQuery object.</returns>
        SPQuery CreateQuery();
        /// <summary>
        /// Creates a site data query object.
        /// </summary>
        /// <param name="scope">the query scope</param>
        /// <returns>a SPSiteDataQuery object containing the query definition</returns>
        SPSiteDataQuery CreateSiteDataQuery(CAML.QueryScope scope);
        /// <summary>
        /// Retrieves matching list items from all lists in all webs of a site collection.
        /// </summary>
        /// <param name="site">The site collection to which the query will be applied</param>
        /// <returns>a list of SPListItem objects</returns>
        IList<SPListItem> Fetch(SPSite site);
        /// <summary>
        /// Retrieves matching list items from within a single web.
        /// </summary>
        /// <param name="web">The web to which the query will be applied</param>
        /// <returns>a list of SPListItem objects</returns>
        IList<SPListItem> Fetch(SPWeb web);
        /// <summary>
        /// Retrieves matching list items from within a specific query scope.
        /// </summary>
        /// <param name="scope">The query scope.</param>
        /// <param name="web">The web to which the query will be applied.</param>
        /// <returns>a list of SPListItem objects</returns>
        IList<SPListItem> Fetch(SPWeb web, CAML.QueryScope scope);
        /// <summary>
        /// Retrieves matching list items from a single list.
        /// </summary>
        /// <param name="list">the list to which the query will be applied</param>
        /// <returns>a list of SPListItem objects</returns>
        IList<SPListItem> Fetch(SPList list);
        /// <summary>
        /// Retrieves matching list items from a site and binds them to objects of any type.
        /// </summary>
        /// <typeparam name="T">The type of object to which SharePoint list data will be bound.</typeparam>
        /// <param name="site">The site collection to which the query will be applied.</param>
        /// <returns>a list of objects of type <typeparamref name="T"/></returns>
        IList<T> Fetch<T>(SPSite site) where T : new();
        /// <summary>
        /// Retrieves matching list items from a web and binds them to objects of any type.
        /// </summary>
        /// <typeparam name="T">The type of object to which SharePoint list data will be bound.</typeparam>
        /// <param name="web">The SharePoint web to which the query will be applied.</param>
        /// <returns>a list of objects of type <typeparamref name="T"/></returns>
        IList<T> Fetch<T>(SPWeb web) where T : new();
        /// <summary>
        /// Retrieves matching list items from a web and binds them to objects of any type.
        /// </summary>
        /// <typeparam name="T">The type of object to which SharePoint list data will be bound.</typeparam>
        /// <param name="web">The SharePoint web to which the query will be applied.</param>
        /// <param name="scope">The query scope (site collection, web, recursive, etc.)</param>
        /// <returns>a list of objects of type <typeparamref name="T"/></returns>
        IList<T> Fetch<T>(SPWeb web, CAML.QueryScope scope) where T : new();
        /// <summary>
        /// Retrieves matching items from a list and binds them to objects of any type.
        /// </summary>
        /// <typeparam name="T">The type of object to which list items will be bound.</typeparam>
        /// <param name="list">The SharePoint list to which the query will be applied.</param>
        /// <returns>a list of objects of type <typeparamref name="T"/></returns>
        IList<T> Fetch<T>(SPList list) where T : new();
        /// <summary>
        /// Executes the query and binds the results to a grid view.
        /// </summary>
        /// <param name="gridView">the grid view to bind to</param>
        /// <param name="web">the web containing the data</param>
        /// <param name="scope">the desired query scope</param>
        /// <param name="initColumns">whether to initialize the grid view columns with the fields found in the result set</param>
        void DataBind(SPGridView gridView, SPWeb web, CAML.QueryScope scope, bool initColumns);
        /// <summary>
        /// Executes the query and binds the results to a grid view.
        /// </summary>
        /// <param name="gridView">the grid view to bind to</param>
        /// <param name="web">the web containing the data</param>
        /// <param name="scope">the desired query scope</param>
        void DataBind(SPGridView gridView, SPWeb web, CAML.QueryScope scope);
        /// <summary>
        /// Executes the query and binds the results to a grid view.
        /// </summary>
        /// <param name="gridView">the grid view to bind to</param>
        /// <param name="web">the web containing the data</param>
        void DataBind(SPGridView gridView, SPWeb web);
        /// <summary>
        /// Executes the query and binds the results to a grid view.
        /// </summary>
        /// <param name="gridView">the grid view to bind to</param>
        void DataBind(SPGridView gridView);
        /// <summary>
        /// Executes the query and binds the results to a grid view.
        /// </summary>
        /// <param name="gridView">the grid view to bind to</param>
        /// <param name="site">the site collection containing the data</param>
        void DataBind(SPGridView gridView, SPSite site);
        /// <summary>
        /// Executes the query and binds the results to a grid view.
        /// </summary>
        /// <param name="gridView">the grid view to bind to</param>
        /// <param name="site">the site collection containing the data</param>
        /// <param name="initColumns">whether to initialize the grid view columns with the fields found in the result set</param>
        void DataBind(SPGridView gridView, SPSite site, bool initColumns);
        /// <summary>
        /// Executes the query and binds the results to a grid view.
        /// </summary>
        /// <param name="gridView">the grid view to bind to</param>
        /// <param name="list">the list containing the data</param>
        void DataBind(SPGridView gridView, SPList list);
        /// <summary>
        /// Executes the query and binds the results to a grid view.
        /// </summary>
        /// <param name="gridView">the grid view to bind to</param>
        /// <param name="list">the list containing the data</param>
        /// <param name="initColumns">whether to initialize the grid view columns with the fields found in the result set</param>
        void DataBind(SPGridView gridView, SPList list, bool initColumns);
    }
}
