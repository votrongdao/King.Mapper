﻿namespace King.Mapper.Data
{
    using System.Collections.Generic;
    using System.Data;

    /// <summary>
    /// Loader interface to enable mocking frameworks for unit testing
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    public interface ILoader<T>
    {
        #region IDbCommand
        IList<T> LoadObjects(IDbCommand cmd, ActionFlags action = ActionFlags.Load);
        T LoadObject(IDbCommand cmd, ActionFlags action = ActionFlags.Load);
        #endregion

        #region IDataReader
        IList<T> LoadObjects(IDataReader reader, ActionFlags action = ActionFlags.Load);
        T LoadObject(IDataReader reader, ActionFlags action = ActionFlags.Load);
        #endregion

        #region DataTable
        IList<T> LoadObjects(DataTable data, ActionFlags action = ActionFlags.Load);
        T LoadObject(DataTable data, ActionFlags action = ActionFlags.Load);
        #endregion

        #region DataSet
        IList<T> LoadObjects(DataSet data, ActionFlags action = ActionFlags.Load);
        T LoadObject(DataSet data, ActionFlags action = ActionFlags.Load);
        #endregion
    }

    #region IStoredProc
    /// <summary>
    /// Stored Procedure
    /// </summary>
    public interface IStoredProc
    {
        #region Properties
        /// <summary>
        /// Stored Procedure Name
        /// </summary>
        string Name
        {
            get;
        }
        #endregion
    }
    #endregion
}