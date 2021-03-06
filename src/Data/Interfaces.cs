﻿namespace King.Mapper.Data
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    #region ILoader
    /// <summary>
    /// Loader interface to enable mocking frameworks for unit testing
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    public interface ILoader<T>
    {
        #region IDbCommand
        /// <summary>
        /// Generate Models from Database Command
        /// </summary>
        /// <param name="cmd">Command</param>
        /// <param name="action">Action</param>
        /// <returns>Models</returns>
        IEnumerable<T> Models(IDbCommand cmd, ActionFlags action = ActionFlags.Load);

        /// <summary>
        /// Generate Model from Database Command
        /// </summary>
        /// <param name="cmd">Command</param>
        /// <param name="action">Action</param>
        /// <returns>Model</returns>
        T Model(IDbCommand cmd, ActionFlags action = ActionFlags.Load);
        #endregion

        #region IDataReader
        /// <summary>
        /// Generate Models from Data Reader
        /// </summary>
        /// <param name="reader">Data Reader</param>
        /// <param name="action">Action</param>
        /// <returns>Models</returns>
        IEnumerable<T> Models(IDataReader reader, ActionFlags action = ActionFlags.Load);

        /// <summary>
        /// Generate Model from Data Reader
        /// </summary>
        /// <param name="reader">Data Reader</param>
        /// <param name="action">Action</param>
        /// <returns>Model</returns>
        T Model(IDataReader reader, ActionFlags action = ActionFlags.Load);
        #endregion

        #region DataTable
        /// <summary>
        /// Generate Models from Data Table
        /// </summary>
        /// <param name="data">Data Table</param>
        /// <param name="action">Action</param>
        /// <returns>Models</returns>
        IEnumerable<T> Models(DataTable data, ActionFlags action = ActionFlags.Load);

        /// <summary>
        /// Generate Model from Data Table
        /// </summary>
        /// <param name="data">Data Table</param>
        /// <param name="action">Action</param>
        /// <returns>Model</returns>
        T Model(DataTable data, ActionFlags action = ActionFlags.Load);
        #endregion

        #region DataSet
        /// <summary>
        /// Generate Models from Data Set
        /// </summary>
        /// <param name="data">Data Table</param>
        /// <param name="action">Action</param>
        /// <returns>Models</returns>
        IEnumerable<T> Models(DataSet data, ActionFlags action = ActionFlags.Load);

        /// <summary>
        /// Generate Model from Data Set
        /// </summary>
        /// <param name="data">Data Table</param>
        /// <param name="action">Action</param>
        /// <returns>Model</returns>
        T Model(DataSet data, ActionFlags action = ActionFlags.Load);
        #endregion
    }
    #endregion

    #region IDynamicLoader
    /// <summary>
    /// Dynamic Loader interface to enable mocking frameworks for unit testing
    /// </summary>
    public interface IDynamicLoader
    {
        #region IDbCommand
        /// <summary>
        /// Generate Dictionaries from Database Command
        /// </summary>
        /// <param name="cmd">Command</param>
        /// <returns>Dictionaries</returns>
        IEnumerable<IDictionary<string, object>> Dictionaries(IDbCommand cmd);

        /// <summary>
        /// Generate Dictionary from Database Command
        /// </summary>
        /// <param name="cmd">Command</param>
        /// <returns>Dictionary</returns>
        IDictionary<string, object> Dictionary(IDbCommand cmd);

        /// <summary>
        /// Generate Dynamics from Database Command
        /// </summary>
        /// <param name="cmd">Command</param>
        /// <returns>Dynamics</returns>
        IEnumerable<dynamic> Dynamics(IDbCommand cmd);

        /// <summary>
        /// Generate Dynamic from Database Command
        /// </summary>
        /// <param name="cmd">Command</param>
        /// <returns>Dynamic</returns>
        dynamic Dynamic(IDbCommand cmd);

        /// <summary>
        /// Generate Dynamics from Data Reader
        /// </summary>
        /// <param name="reader">Data Reader</param>
        /// <param name="action">Action</param>
        /// <returns>Dynamics</returns>
        IEnumerable<dynamic> Dynamics(IDataReader reader);

        /// <summary>
        /// Generate Dynamic from Data Reader
        /// </summary>
        /// <param name="reader">Data Reader</param>
        /// <param name="action">Action</param>
        /// <returns>Dynamic</returns>
        dynamic Dynamic(IDataReader reader);
        #endregion

        #region IDataReader
        /// <summary>
        /// Generate Dictionaries from Data Reader
        /// </summary>
        /// <param name="reader">Data Reader</param>
        /// <param name="action">Action</param>
        /// <returns>Dictionaries</returns>
        IEnumerable<IDictionary<string, object>> Dictionaries(IDataReader reader);

        /// <summary>
        /// Generate Dictionary from Data Reader
        /// </summary>
        /// <param name="reader">Data Reader</param>
        /// <param name="action">Action</param>
        /// <returns>Dictionary</returns>
        IDictionary<string, object> Dictionary(IDataReader reader);
        #endregion

        #region DataTable
        /// <summary>
        /// Generate Dictionary from Data Table
        /// </summary>
        /// <param name="data">Data Table</param>
        /// <returns>Dictionary</returns>
        IDictionary<string, object> Dictionary(DataTable data);

        /// <summary>
        /// Generate Dictionaries from Data Table
        /// </summary>
        /// <param name="data">Data Table</param>
        /// <returns>Dictionaries</returns>
        IEnumerable<IDictionary<string, object>> Dictionaries(DataTable data);

        /// <summary>
        /// Generate Dynamic from Data Table
        /// </summary>
        /// <param name="data">Data Table</param>
        /// <returns>Dynamic</returns>
        dynamic Dynamic(DataTable data);

        /// <summary>
        /// Generate Dynamics from Data Table
        /// </summary>
        /// <param name="data">Data Table</param>
        /// <returns>Dynamics</returns>
        IEnumerable<dynamic> Dynamics(DataTable data);
        #endregion

        #region DataSet
        /// <summary>
        /// Generate Dictionaries from Data Set
        /// </summary>
        /// <param name="data">Data Set</param>
        /// <param name="action">Action</param>
        /// <returns>Dictionaries</returns>
        IEnumerable<IDictionary<string, object>> Dictionaries(DataSet data);

        /// <summary>
        /// Generate Dictionary from Data Set
        /// </summary>
        /// <param name="data">Data Set</param>
        /// <param name="action">Action</param>
        /// <returns>Dictionary</returns>
        IDictionary<string, object> Dictionary(DataSet data);

        /// <summary>
        /// Generate Dynamics from Data Set
        /// </summary>
        /// <param name="data">Data Set</param>
        /// <param name="action">Action</param>
        /// <returns>Dynamics</returns>
        IEnumerable<dynamic> Dynamics(DataSet data);

        /// <summary>
        /// Generate Dynamic from Data Set
        /// </summary>
        /// <param name="data">Data Set</param>
        /// <param name="action">Action</param>
        /// <returns>Dynamic</returns>
        dynamic Dynamic(DataSet data);
        #endregion
    }
    #endregion

    #region IExecutor
    /// <summary>
    /// Execution Interface
    /// </summary>
    public interface IExecutor
    {
        #region Methods
        /// <summary>
        /// Execute Sproc for mocking
        /// </summary>
        /// <param name="sproc">Stored Procedure</param>
        /// <returns></returns>
        Task<DataSet> Query(IStoredProcedure sproc);

        /// <summary>
        /// Query Database with SQL Statement
        /// </summary>
        /// <param name="statement">Sql Statement</param>
        /// <returns>Data Set</returns>
        Task<DataSet> Query(string statement);

        /// <summary>
        /// Non Query sproc for mocking
        /// </summary>
        /// <param name="sproc">Stored Procedure</param>
        /// <returns></returns>
        Task<int> NonQuery(IStoredProcedure sproc);

        /// <summary>
        /// Non-Query
        /// </summary>
        /// <param name="command">Command To Execute</param>
        /// <returns>rows affected</returns>
        Task<int> NonQuery(SqlCommand command);

        /// <summary>
        /// Non-Query
        /// </summary>
        /// <param name="statement">Statement To Execute</param>
        /// <returns>rows affected</returns>
        Task<int> NonQuery(string statement);

        /// <summary>
        /// Query for Reader with IStoredProcecure
        /// </summary>
        /// <param name="sproc">Stored Procedure</param>
        /// <returns>Data Reader</returns>
        Task<IDataReader> DataReader(IStoredProcedure sproc);
        #endregion
    }
    #endregion

    #region IStoredProcedure
    /// <summary>
    /// Stored Procedure
    /// </summary>
    public interface IStoredProcedure
    {
        #region Methods
        /// <summary>
        /// Fully Qualified Stored Procedure Name
        /// </summary>
        string FullyQualifiedName();
        #endregion
    }
    #endregion
}